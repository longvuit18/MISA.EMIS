using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.ApplicationCore.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using MISA.AMIS.ApplicationCore;
using System.Threading.Tasks;
using MISA.AMIS.ApplicationCore.Properties;
using MISA.AMIS.ApplicationCore.Models;
using MISA.AMIS.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Cors;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]

    public class BaseController<TEntity> : ControllerBase
    {
        #region Declared
        readonly IBaseService<TEntity> _baseService;
        #endregion


        #region Initialization
        public BaseController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Api lấy danh sách toàn bộ entity
        /// </summary>
        /// <returns>danh sách entity</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _baseService.GetAll();
            return Ok(result);
        }
        /// <summary>
        /// Lấy 1 entity theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>1 entity nếu tìm được trong db hoặc lỗi</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            // convert string to guid
            var isToGuid = Guid.TryParse(id, out Guid guid);
            if (!isToGuid)
            {
                var errorResult = CreateErrorResult();
                errorResult.Code = MisaCode.Fail;
                errorResult.State = false;
                errorResult.MsgDev = Resources.IdInconrect;
                errorResult.MsgUser = Resources.IdInconrect;
                return Ok(errorResult);
            }

            var result = await _baseService.GetOne(guid);

            if (result.Data == null)
            {
                return NoContent();
            }
            return Ok(result);

        }

        /// <summary>
        /// Thêm 1 entity vào database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Trả về thành công nếu thêm thành công</returns>
        [HttpPost]
        public async Task<IActionResult> AddOne([FromBody] TEntity entity)
        {
            var serviceResult = await _baseService.AddOne(entity);
            if (!serviceResult.State)
            {
                return Ok(serviceResult);
            }
            return StatusCode(201, serviceResult);

        }
        /// <summary>
        /// Update 1 entity
        /// </summary>
        /// <param name="entity">Các trường trong entity muốn update</param>
        /// <param name="id">Id của entity</param>
        /// <returns>Trả về thành công nếu xóa thành công</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOne(TEntity entity, string id)
        {
            // name entity
            var entityName = entity.GetType().Name.ToLower();

            // convert string to guid
            var isToGuid = Guid.TryParse(id, out Guid guid);
            if (isToGuid)
            {
                entity.GetType().GetProperty($"{entityName}_id").SetValue(entity, guid);
            }
            else
            {
                var errorResult = CreateErrorResult();
                errorResult.Code = MisaCode.Fail;
                errorResult.State = false;
                errorResult.MsgDev = Resources.IdInconrect;
                errorResult.MsgUser = Resources.IdInconrect;
                return Ok(errorResult);
            }
            var serviceResult = await _baseService.UpdateOne(entity);
            if (!serviceResult.State)
            {
                return Ok(serviceResult);
            }
            return Ok(serviceResult);
        }
        /// <summary>
        /// Xóa 1 entity trong db
        /// </summary>
        /// <param name="id">id của entity</param>
        /// <returns>Trả về thành công nếu xóa thành công</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(string id)
        {
            // convert string to guid
            var isToGuid = Guid.TryParse(id, out Guid guid);
            if (!isToGuid)
                    {
                        var errorResult = CreateErrorResult();
                        errorResult.Code = MisaCode.Fail;
                        errorResult.State = false;
                        errorResult.MsgDev = Resources.IdInconrect;
                        errorResult.MsgUser = Resources.IdInconrect;
                        return Ok(errorResult);
                    }
            var serviceResult = await _baseService.DeleteOne(guid);
            if (!serviceResult.State)
            {
                return Ok(serviceResult);
            }
            return Ok(serviceResult);
        }

        private static ServiceResult CreateErrorResult()
        {
            return new()
            {
                Code = MisaCode.Exception,
                MsgUser = Resources.ExceptionError
            };
        }
        #endregion

    }
}
