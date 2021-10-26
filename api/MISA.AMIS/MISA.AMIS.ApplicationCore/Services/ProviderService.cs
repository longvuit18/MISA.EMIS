using MISA.AMIS.ApplicationCore.Constants;
using MISA.AMIS.ApplicationCore.Interfaces;
using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Services
{
    public class ProviderService : BaseService<AccountObject>, IProviderService
    {
        #region Declared

        IProviderRepository _providerRepository;

        #endregion

        #region Initialization
        public ProviderService(IProviderRepository providerRepository) : base(providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public override async Task<ServiceResult> DeleteOne(Guid entityId)
        {
            Guid[] listId = { entityId };
            var validateArising = await ValidateArising(listId);
            if (!validateArising)
            {
                return new ServiceResult
                {
                    Code = MisaCode.Success,
                    Data = null,
                    MsgUser = Properties.Resources.ArisingData,
                    State = false
                };
            }
            return await base.DeleteOne(entityId);
        }
        public async Task<ServiceResult> DeleteBatch(Guid[] listId)
        {
            var validateArising = await ValidateArising(listId);
            if (!validateArising)
            {
                return new ServiceResult
                {
                    Code = MisaCode.Success,
                    Data = null,
                    MsgUser = Properties.Resources.ArisingData,
                    State = false
                };
            }
            var rowAffect =  await _providerRepository.DeleteBatch(listId);
            if(rowAffect < 1)
            {
                return new ServiceResult
                {
                    Code = MisaCode.Success,
                    Data = rowAffect,
                    MsgUser = Properties.Resources.NothingUpdate,
                    State = false
                };
            }
            return new ServiceResult
            {
                Code = MisaCode.Success,
                Data = rowAffect,
                MsgUser = Properties.Resources.GetSuccessfully,
                State = true
            };
        }

        public async Task<string> GetNewProviderCode()
        {
            return await _providerRepository.GetNewProviderCode();
        }

        public async Task<ServiceResult> GetProviderFilterPaging(ProviderFilter filter)
        {
            var data = await _providerRepository.GetProviderFilterPaging(filter);
            return new ServiceResult
            {
                Code = MisaCode.Success,
                Data = data,
                MsgUser = Properties.Resources.GetSuccessfully,
                State = true
            };
        }
        /// <summary>
        /// Kiểm tra phát sinh của account object
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// Created by: VLVU (20/10/2021)
        private async Task<bool> ValidateArising(Guid[] listId)
        {
            var arisingEffected = await _providerRepository.CheckArising(listId);

            if (arisingEffected > 0)
            {
                return false;
            }
            return true;

        }
        #endregion
    }
}
