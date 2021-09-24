using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.ApplicationCore.Interfaces;
using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    /// <summary>
    /// Controller employee
    /// </summary>
    /// Created by: VLVU (29/7/2021)
    [Route("api/v1/providers")]
    [ApiController]
    public class ProviderController : BaseController<Provider>
    {
        #region Declared

        readonly IProviderService _providerService;
        #endregion

        #region Initialization
        public ProviderController(IProviderService providerService) : base(providerService)
        {
            _providerService = providerService;
        }
        #endregion

        #region Methods

        [HttpPost("filterPaging")]
        public async Task<IActionResult> GetFilterPaging(ProviderFilter filter)
        {
            var result = await _providerService.GetProviderFilterPaging(filter);
            return Ok(result);
        }
        /// <summary>
        /// Lấy một mã nhân viên mới từ cơ sở dữ liệu
        /// </summary>
        /// <returns></returns>
        /// Created by: VLVU (20/8/2021)
        [HttpGet("newProviderCode")]
        public async Task<IActionResult> GetNewEmployeeCode()
        {
            var result = await _providerService.GetNewProviderCode();
            return Ok(result);
        }
        #endregion

    }
}
