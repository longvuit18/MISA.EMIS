using MISA.AMIS.ApplicationCore.Interfaces;
using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Services
{
    /// <summary>
    /// Service employee
    /// </summary>
    /// Created by: VLVU (2021)
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Declared

        IEmployeeRepository _employeeRopository;

        #endregion

        #region Initialization
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRopository = employeeRepository;
        }
        #endregion

        #region Methods
        public async Task<object> GetEmployeeFilterPaging(string employeeFilter, int pageNumber, int pageSize)
        {
            return await _employeeRopository.GetEmployeeFilterPaging(employeeFilter, pageNumber, pageSize);
        }

        public async Task<string> GetNewEmployeeCode()
        {
            return await _employeeRopository.GetNewEmployeeCode();
        }
        #endregion

    }
}
