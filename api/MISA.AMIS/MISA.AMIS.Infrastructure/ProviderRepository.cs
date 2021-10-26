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
    public class ProviderRepository : BaseRepository<AccountObject>, IProviderRepository
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
            parameters.Add("@custom_filter", filter.CustomFilter, DbType.String);
            parameters.Add("@p_account_object_code", filter.provider_code, DbType.String);
            parameters.Add("@p_account_object_name", filter.provider_name, DbType.String);
            parameters.Add("@p_address", filter.address, DbType.String);
            parameters.Add("@p_description", filter.description, DbType.String);
            parameters.Add("@p_tax_code", filter.tax_code, DbType.String);
            parameters.Add("@p_phone_number", filter.phone_number, DbType.String);
            parameters.Add("@p_personal_contact_identity_number", filter.personal_contact_identity_number, DbType.String);
            parameters.Add("@p_province_or_city", filter.province_or_city, DbType.String);
            parameters.Add("@p_district", filter.district, DbType.String);
            parameters.Add("@p_ward_or_commune", filter.ward_or_commune, DbType.String);
            parameters.Add("@p_account_object_group", filter.provider_group, DbType.String);
            parameters.Add("@p_account_object_type", filter.account_object_type);

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
        public async Task<string> GetNewProviderCode()
        {
            // query từ database
            var maxCode = await _dbConnection.QueryFirstOrDefaultAsync<string>($"func_get_max_{_tableName}_code", commandType: CommandType.StoredProcedure);
            if (maxCode == null)
            {
                return "NCC-00001";
            }
            var prefix = "NCC-";
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

        #endregion


    }
}
