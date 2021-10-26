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
using System.Text.Json.Serialization;
using Newtonsoft.Json;

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
            parameters.Add("@start_date", filter.StartDate);
            parameters.Add("@end_date", filter.EndDate);

            // query từ database
            var receiptPaymentFilted = await _dbConnection.QueryAsync<ReceiptPayment>($"func_get_{_tableName}_filter_paging", parameters, commandType: CommandType.StoredProcedure);

            // lấy tổng số trang và tổng số bản ghi
            var totalRecord = receiptPaymentFilted.Count();

            var offSet = (filter.PageIndex - 1) * filter.PageSize;
            decimal totalPageRaw = totalRecord / filter.PageSize;
            var totalPage = Math.Floor(totalPageRaw) + 1;

            var result = receiptPaymentFilted.Skip(offSet).Count() <= filter.PageSize
                ? receiptPaymentFilted.Skip(offSet)
                : receiptPaymentFilted.Skip(offSet).ToList().GetRange(0, filter.PageSize);
            var totalAmount = receiptPaymentFilted.Sum(item => item.total_amount);
            // trả về dưới dạng 1 object
            return new { total_page = totalPage, total_record = totalRecord, result, total_amount_payment = totalAmount};
        }
        public async Task<string> GetNewReceiptPaymentCode()
        {
            // query từ database
            var maxCode = await _dbConnection.QueryFirstOrDefaultAsync<string>($"func_get_max_payment_code", commandType: CommandType.StoredProcedure);

            if (maxCode == null)
            {
                return "NCC-00001";
            }
            var prefix = "PC-";
            var suffixes = "";

            foreach (char c in maxCode.Reverse())
            {
                if (!char.IsDigit(c))
                {
                    break;
                }
                suffixes = c.ToString() + suffixes;
            }

            var suffixesNumber = Int32.Parse(suffixes) + 1;
            var prefixZero = "";

            var lengthSuffixesNumber = suffixesNumber.ToString().Length;
            if (lengthSuffixesNumber < 5)
            {
                for (int i = 0; i < 5 - lengthSuffixesNumber; i++)
                {
                    prefixZero += "0";
                }
            }

            var result = prefix + prefixZero + suffixesNumber;

            return result;
        }

        public async Task<int> DeleteBatch(Guid[] listId)
        {
            var rowAffected = 0;
            // add transaction
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                var parameter = new DynamicParameters();
                parameter.Add($"list_id", listId);
                rowAffected += await _dbConnection.ExecuteScalarAsync<int>($"func_delete_multiple_{_tableName}_by_id", parameter, commandType: CommandType.StoredProcedure, transaction: transaction);


                transaction.Commit();

            }
            return rowAffected;
        }

        public async Task<int> InsertPayment(ReceiptPayment payment, ReceiptPaymentDetail[] paymentDetails)
        {
            var rowAffected = 0;
            // add transaction
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                var paymentDetailsStirng = JsonConvert.SerializeObject(paymentDetails);
                var parameter = MappingDbType(payment);
                parameter.Add("p_payment_detail", paymentDetailsStirng);
                rowAffected += await _dbConnection.ExecuteScalarAsync<int>($"func_insert_payment", parameter, commandType: CommandType.StoredProcedure, transaction: transaction);

                transaction.Commit();

            }
            return rowAffected;
        }

        public async Task<object> GetPaymentById(Guid paymentId)
        {
            var parameter = new DynamicParameters();
            parameter.Add($"p_{_tableName}_id", paymentId);

            var parameterDetail = new DynamicParameters();
            parameterDetail.Add($"p_refid", paymentId);
            var payment = await _dbConnection.QuerySingleOrDefaultAsync<ReceiptPayment>($"func_get_receipt_payment_by_id", parameter, commandType: CommandType.StoredProcedure);
            var paymentDetails = await _dbConnection.QueryAsync<ReceiptPaymentDetail>($"func_get_receipt_payment_detail_by_refid", parameterDetail, commandType: CommandType.StoredProcedure);
            return new { payment, payment_details = paymentDetails};
        }

        #endregion


    }
}
