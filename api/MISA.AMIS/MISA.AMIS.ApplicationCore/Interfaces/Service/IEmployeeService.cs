using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Interfaces
{
    /// <summary>
    /// employee service interface
    /// Created by: VLVU (18/8/2021)
    /// </summary>
    public interface IEmployeeService: IBaseService<Employee>
    {
        /// <summary>
        /// service thực thiện filter và paging
        /// </summary>
        /// <param name="employeeFilter">chuỗi filter như tên, số điện thoại, mã nhân viên</param>
        /// <param name="pageNumber">Page số bao nhiêu</param>
        /// <param name="pageSize">Số lượng bản ghi trong 1 page</param>
        /// <returns></returns>
        /// Created by: VLVU (27/7/2021)
        Task<object> GetEmployeeFilterPaging(string employeeFilter, int pageNumber, int pageSize);
        /// <summary>
        /// service lấy một mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Created by: VLVU (27/7/2021)
        Task<string> GetNewEmployeeCode();
    }
}
