using Microsoft.Extensions.Configuration;
using MISA.AMIS.ApplicationCore.Interfaces.Repository;
using MISA.AMIS.ApplicationCore.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MISA.AMIS.Infrastructure
{
    public class LocationRepository : ILocationRepository
    {
        #region Declared
        protected IDbConnection _dbConnection;
        protected string _tableName;
        #endregion

        #region Initializaiton
        public LocationRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MISAAMISLocalConnectionString");
            _dbConnection = new NpgsqlConnection(connectionString);
            // (_dbConnection as NpgsqlConnection).TypeMapper.MapComposite<Employee>();
        }
        #endregion

        #region Methods

        public async Task<IEnumerable<Country>> GetAllContries()
        {
            var countries = await _dbConnection.QueryAsync<Country>("func_get_country", commandType: CommandType.StoredProcedure);
            return countries;
        }

        public async Task<IEnumerable<District>> GetDistricts(Guid countryId, int provinceId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("country_id", countryId);
            parameters.Add("p_province_id", provinceId);
            var districts = await _dbConnection.QueryAsync<District>("func_get_district", parameters, commandType: CommandType.StoredProcedure);
            return districts;
        }

        public async Task<IEnumerable<Province>> GetProvinces(Guid countryId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("country_id", countryId);
            var provinces = await _dbConnection.QueryAsync<Province>("func_get_province", parameters, commandType: CommandType.StoredProcedure);
            return provinces;
        }

        public async Task<IEnumerable<Ward>> GetWards(Guid countryId, int provinceId, int districtId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("country_id", countryId);
            parameters.Add("p_province_id", provinceId);
            parameters.Add("p_district_id", districtId);
            var wards = await _dbConnection.QueryAsync<Ward>("func_get_ward", parameters, commandType: CommandType.StoredProcedure);
            return wards;
        }
        #endregion
    }
}
