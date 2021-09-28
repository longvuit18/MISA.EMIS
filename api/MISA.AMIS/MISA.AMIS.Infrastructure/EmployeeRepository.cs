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
            parameters.Add("@employee_filter", employeeFilter);

            // query từ database
            var employeeFiltered = await _dbConnection.QueryAsync<Employee>($"func_get_{_tableName}_filter_paging", parameters, commandType: CommandType.StoredProcedure);

            // lấy tổng số trang và tổng số bản ghi
            var totalRecord = employeeFiltered.Count();

            var offSet = (pageNumber - 1) * pageSize;
            decimal totalPageRaw = totalRecord / pageSize;
            var totalPage = Math.Floor(totalPageRaw) + 1;

            var result = employeeFiltered.Skip(offSet).Count() <= pageSize 
                ? employeeFiltered.Skip(offSet)
                : employeeFiltered.Skip(offSet).ToList().GetRange(0, pageSize);

            // trả về dưới dạng 1 object
            return new {  total_page = totalPage, total_record = totalRecord, Data = result };
        }
        public async Task<string> GetNewEmployeeCode()
        {
            // query từ database
            var maxCode = await _dbConnection.QueryFirstOrDefaultAsync<string>($"func_get_max_{_tableName}_code", commandType: CommandType.StoredProcedure);

            var prefix = "NV-";
            var suffixes = "";

            foreach(char c in maxCode.Reverse())
            {
                if(!Char.IsDigit(c))
                {
                    break;
                }
                suffixes = c.ToString() + suffixes;
            }

            var result = prefix + (Int32.Parse(suffixes) + 1);

            return result;
        }
        #endregion


    }
}
