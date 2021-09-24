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
    public class ProviderRepository : BaseRepository<Provider>, IProviderRepository
    {

        #region Declared

        #endregion

        #region Initialization
        public ProviderRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion

        #region Methods
        public async Task<object> GetProviderFilterPaging(ProviderFilter filter)
        {
            // Tạo và set dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@custom_filter", filter.CustomFilter, DbType.String) ;

            // query từ database
            var employeeFiltered = await _dbConnection.QueryAsync<Provider>($"func_get_{_tableName}s_filter_paging", parameters, commandType: CommandType.StoredProcedure);

            // lấy tổng số trang và tổng số bản ghi
            var totalRecord = employeeFiltered.Count();

            var offSet = (filter.PageIndex - 1) * filter.PageSize;
            decimal totalPageRaw = totalRecord / filter.PageSize;
            var totalPage = Math.Floor(totalPageRaw) + 1;

            var result = employeeFiltered.Skip(offSet).Count() <= filter.PageSize
                ? employeeFiltered.Skip(offSet)
                : employeeFiltered.Skip(offSet).ToList().GetRange(0, filter.PageSize);

            // trả về dưới dạng 1 object
            return new {  total_page = totalPage, total_record = totalRecord, result };
        }
        public async Task<string> GetNewProviderCode()
        {
            // query từ database
            var maxCode = await _dbConnection.QueryFirstOrDefaultAsync<string>($"func_get_max_{_tableName}_code", commandType: CommandType.StoredProcedure);

            var prefix = "NCC-";
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
