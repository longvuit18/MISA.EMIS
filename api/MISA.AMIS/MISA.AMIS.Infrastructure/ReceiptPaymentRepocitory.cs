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
    public class ReceiptPaymentRepocitory : BaseRepository<ReceiptPayment>, IReceiptPaymentRepository
    {

        #region Declared

        #endregion

        #region Initialization
        public ReceiptPaymentRepocitory(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion

        #region Methods
        public async Task<object> GetReceiptPaymentFilterPaging(ReceiptPaymentFilter filter)
        {
            // Tạo và set dynamic parameter
            var parameters = new DynamicParameters();
            parameters.Add("@custom_filter", filter.CustomFilter, DbType.String);

            // query từ database
            var employeeFiltered = await _dbConnection.QueryAsync<AccountObject>($"func_get_{_tableName}_filter_paging", parameters, commandType: CommandType.StoredProcedure);

            // lấy tổng số trang và tổng số bản ghi
            var totalRecord = employeeFiltered.Count();

            var offSet = (filter.PageIndex - 1) * filter.PageSize;
            decimal totalPageRaw = totalRecord / filter.PageSize;
            var totalPage = Math.Floor(totalPageRaw) + 1;

            var result = employeeFiltered.Skip(offSet).Count() <= filter.PageSize
                ? employeeFiltered.Skip(offSet)
                : employeeFiltered.Skip(offSet).ToList().GetRange(0, filter.PageSize);

            // trả về dưới dạng 1 object
            return new { total_page = totalPage, total_record = totalRecord, result };
        }
        public async Task<string> GetNewReceiptPaymentCode()
        {
            // query từ database
            var maxCode = await _dbConnection.QueryFirstOrDefaultAsync<string>($"func_get_max_{_tableName}_code", commandType: CommandType.StoredProcedure);

            var prefix = "NCC-";
            var suffixes = "";

            foreach (char c in maxCode.Reverse())
            {
                if (!Char.IsDigit(c))
                {
                    break;
                }
                suffixes = c.ToString() + suffixes;
            }

            var result = prefix + (Int32.Parse(suffixes) + 1);

            return result;
        }

        public async Task<int> DeleteBatch(Guid[] listId)
        {
            var rowAffected = 0;
            // add transaction
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                foreach (var item in listId)
                {
                    var parameter = new DynamicParameters();
                    parameter.Add($"p_{_tableName}_id", item);
                    rowAffected += await _dbConnection.ExecuteScalarAsync<int>($"func_delete_{_tableName}_by_id", parameter, commandType: CommandType.StoredProcedure, transaction: transaction);
                    
                }
                transaction.Commit();

            }
            return rowAffected;
        }

        #endregion


    }
}
