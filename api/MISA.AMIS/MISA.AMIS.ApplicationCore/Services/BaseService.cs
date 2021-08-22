using Dapper;
using MISA.AMIS.ApplicationCore.Constants;
using MISA.AMIS.ApplicationCore.Enums;
using MISA.AMIS.ApplicationCore.Interfaces;
using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Services
{
    /// <summary>
    /// Base service
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created by: VLVU (2021)
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity: BaseEntity
    {
        #region Declared
        readonly IBaseRepository<TEntity> _baseRepository;
        protected string errorValidate = string.Empty;
        #endregion

        #region Initialization
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        #region Methods
        public async Task<ServiceResult> AddOne(TEntity entity)
        {
            // validate entity
            var isValid = await Validate(entity);
            var serviceResult = new ServiceResult();
            if (!isValid)
            {
                serviceResult.Code = MisaCode.Fail;
                serviceResult.State = false;
                serviceResult.MsgUser = errorValidate;
                serviceResult.MsgDev = errorValidate;
                return serviceResult;
            }
            // thưc hiện thêm data
            var rowCount = await _baseRepository.AddOne(entity);
            if (rowCount > 0)
            {
                serviceResult.Code = MisaCode.Success;
                serviceResult.Data = rowCount;
                serviceResult.MsgUser = Properties.Resources.AddedSuccessfully;
                return serviceResult;
            }
            // khi rowCount = 0 TODO
            serviceResult.Code = MisaCode.Success;
            serviceResult.State = false;
            serviceResult.Data = rowCount;
            serviceResult.MsgUser = Properties.Resources.NothingUpdate;
            return serviceResult;
        }

        public async Task<ServiceResult> DeleteOne(string entityId)
        {
            // TODO validate
            var rowCount = await _baseRepository.DeleteOne(entityId);
            var serviceResult = new ServiceResult();
            if (rowCount > 0)
            {
                serviceResult.Code = MisaCode.Success;
                serviceResult.Data = rowCount;
                serviceResult.MsgUser = Properties.Resources.DeletedSuccessfully;
                return serviceResult;
            }
            // khi rowCount = 0 TODO
            serviceResult.Code = MisaCode.Success;
            serviceResult.State = false;
            serviceResult.Data = rowCount;
            serviceResult.MsgUser = Properties.Resources.NothingUpdate;
            return serviceResult;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _baseRepository.GetAll();
        }

        public async Task<TEntity> GetOne(string entityId)
        {
            return await _baseRepository.GetOne(entityId);
        }

        public async Task<ServiceResult> UpdateOne(TEntity entity)
        {
            entity.EntityState = EntityState.PUT;
            var serviceResult = new ServiceResult();
            var isValid = await Validate(entity);
            if (!isValid)
            {
                serviceResult.Code = MisaCode.Fail;
                serviceResult.State = false;
                serviceResult.MsgUser = errorValidate;
                serviceResult.MsgDev = errorValidate;
                return serviceResult;
            }
            var rowCount = await _baseRepository.UpdateOne(entity);
            if (rowCount > 0)
            {
                serviceResult.Code = MisaCode.Success;
                serviceResult.Data = rowCount;
                serviceResult.MsgUser = Properties.Resources.UpdatedSuccessfully;
                return serviceResult;
            }
            // khi rowCount = 0 TODO
            serviceResult.Code = MisaCode.Success;
            serviceResult.State = false;
            serviceResult.Data = rowCount;
            serviceResult.MsgUser = Properties.Resources.NothingUpdate;
            return serviceResult;
        }
        /// <summary>
        /// Validate cho Base
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected async Task<bool> Validate(TEntity entity)
        {
            // set lại giá trị ban đầu cho  errorValidate
            errorValidate = string.Empty;
            // Lấy ra các properties của entity
            var properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {

                // Lấy name và value và display name của property
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyDisplayName = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;
                // Lấy các attibutes của từng property
                var attributes = property.GetCustomAttributes(true);

                foreach (var attribute in attributes)
                {
                    if (attribute is Required)
                    {
                        if (propertyValue == null || propertyValue as string == string.Empty)
                        {
                            errorValidate = string.Format(Properties.Resources.Required, $"{propertyDisplayName.DisplayName}");
                            return false;
                        }

                    }

                    if(attribute is MaxLength)
                    {
                        if (propertyValue != null && propertyValue.ToString().Length > (attribute as MaxLength).Length)
                        {
                            errorValidate = string.Format(Properties.Resources.MaxLengthError, $"{propertyDisplayName?.DisplayName ?? "trường này"}", (attribute as MaxLength).Length);
                            return false;
                        }
                    }

                    if (attribute is NotDuplicate)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@propertyName", propertyName);
                        parameters.Add("@propertyValue", propertyValue);
                        parameters.Add("@entity", entity, DbType.Object);
                        var entityDuplicate = await _baseRepository.GetEntityByProperty(parameters);
                        if (entityDuplicate != null)
                        {
                            errorValidate = string.Format(Properties.Resources.Duplicated, propertyDisplayName.DisplayName, propertyValue);
                            return false;
                        }

                    }
                }
            }

            var customValidate = CustomValidate(entity);
            return true && customValidate;
        }
        /// <summary>
        /// custom validate cho những lớp con kế thừa có thể tùy chỉnh được validate theo nghiệp
        /// vụ riêng.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool CustomValidate(TEntity entity)
        {
            return true;
        }
        #endregion
    }
}
