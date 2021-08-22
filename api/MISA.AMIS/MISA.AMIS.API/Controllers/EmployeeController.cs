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
    [Route("api/v1/employees")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        #region Declared

        readonly IEmployeeService _employeeService;
        #endregion

        #region Initialization
        public EmployeeController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Filter và paging
        /// </summary>
        /// <param name="employeeFilter">Text filter như tên, số điện thoại, mã nhân viên</param>
        /// <param name="pageNumber">page số bao nhiêu</param>
        /// <param name="pageSize">số lượng bản ghi trong page</param>
        /// <returns></returns>
        /// Created by: VLVU (20/8/2021)
        [HttpGet("filterPaging")]
        public async Task<IActionResult> GetFilterPaging(string employeeFilter, int pageNumber, int pageSize = 20)
        {
            var result = await _employeeService.GetEmployeeFilterPaging(employeeFilter, pageNumber, pageSize);
            return Ok(result);
        }
        /// <summary>
        /// Lấy một mã nhân viên mới từ cơ sở dữ liệu
        /// </summary>
        /// <returns></returns>
        /// Created by: VLVU (20/8/2021)
        [HttpGet("newEmployeeCode")]
        public async Task<IActionResult> GetNewEmployeeCode()
        {
            var result = await _employeeService.GetNewEmployeeCode();
            return Ok(result);
        }
        #endregion

    }
}
