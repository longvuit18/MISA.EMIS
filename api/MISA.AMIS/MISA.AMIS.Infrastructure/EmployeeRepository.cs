using Microsoft.Extensions.Configuration;
using MISA.AMIS.ApplicationCore.Interfaces;
using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MISA.AMIS.Infrastructure
{
    /// <summary>
    /// employee repository 
    /// </summary>
    /// Created by: VLVU (30/8/2021)
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {

        #region Declared

        #endregion

        #region Initialization
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion

        #region Methods
        public async Task<object> GetEmployeeFilterPaging(string employeeFilter, int pageNumber, int pageSize)
        {
            // Tạo và set dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeFilter", employeeFilter);
            parameters.Add("@PageIndex", pageNumber);
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@TotalPage", pageSize, direction: ParameterDirection.Output);
            parameters.Add("@TotalRecord", pageSize, direction: ParameterDirection.Output);
            // query từ database
            var employeeFiltered = await _dbConnection.QueryAsync<Employee>($"Proc_Get{_tableName}sFilterPaging", parameters, commandType: CommandType.StoredProcedure);

            // lấy tổng số trang và tổng số bản ghi
            var totalPage = parameters.Get<int>("@TotalPage");
            var totalRecord = parameters.Get<int>("@TotalRecord");

            // trả về dưới dạng 1 object
            return new { TotalPage = totalPage, TotalRecord = totalRecord, Data = employeeFiltered };
        }
        public async Task<string> GetNewEmployeeCode()
        {
            // query từ database
            var newCode = await _dbConnection.QueryFirstOrDefaultAsync<string>($"Proc_GetNew{_tableName}Code", commandType: CommandType.StoredProcedure);
            return newCode;
        }
        #endregion


    }
}
