using Microsoft.Extensions.Configuration;
using MISA.AMIS.ApplicationCore.Interfaces;
using MISA.AMIS.ApplicationCore.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MISA.AMIS.ApplicationCore.Enums;

namespace MISA.AMIS.Infrastructure
{
    /// <summary>
    /// Base Repository có các phương thức chung thêm sửa xóa
    /// </summary>
    /// <typeparam name="TEntity">Model của table</typeparam>
    /// Created by: VLVU (30/7/2021)
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable where TEntity : BaseEntity
    {
        #region Declared
        protected IDbConnection _dbConnection;
        protected string _tableName;
        #endregion

        #region Initializaiton
        public BaseRepository(IConfiguration configuration)
        {
            _tableName = typeof(TEntity).Name;
            var connectionString = configuration.GetConnectionString("MISAAMISLocalConnectionString");
            _dbConnection = new MySqlConnection(connectionString);
        }
        #endregion

        #region Methods
        public async Task<int> AddOne(TEntity entity)
        {
            int rowCount;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                var mapEntity = MappingDbType(entity);

                rowCount = await _dbConnection.ExecuteAsync($"Proc_Insert{_tableName}", mapEntity, commandType: CommandType.StoredProcedure, transaction: transaction);
                transaction.Commit();
            }
            return rowCount;
        }

        public async Task<int> DeleteOne(string entityId)
        {
            int rowAffected;
            // add transaction
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                var parameter = new DynamicParameters();
                parameter.Add($"{_tableName}Id", entityId);
                rowAffected = await _dbConnection.ExecuteAsync($"Proc_Delete{_tableName}ById", parameter, commandType: CommandType.StoredProcedure, transaction: transaction);
                transaction.Commit();
            }
            return rowAffected;
        }


        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var entities = await _dbConnection.QueryAsync<TEntity>($"Proc_Get{_tableName}s", commandType: CommandType.StoredProcedure);
            return entities;

        }

        public async Task<TEntity> GetEntityByProperty(DynamicParameters parameters)
        {
            var entity = parameters.Get<TEntity>("@entity");
            var propertyId = entity.GetType().GetProperty($"{_tableName}Id");
            // lấy id của entity
            var entityId = propertyId.GetValue(entity);
            parameters.Add("@entityId", entityId, DbType.String);
            var entityState = entity.EntityState;
            // command sql
            var command = string.Empty;
            if (entityState == EntityState.POST)
            {
                command = $"select * from {_tableName} where {parameters.Get<string>("@propertyName")} = @propertyValue";
            }
            if (entityState == EntityState.PUT)
            {
                command = $"select * from {_tableName} where {parameters.Get<string>("@propertyName")} = @propertyValue and {_tableName}Id <> @entityId";
            }
            // query database
            var entityDb = await _dbConnection.QueryFirstOrDefaultAsync<TEntity>(command, parameters, commandType: CommandType.Text);
            return entityDb;
        }

        public async Task<TEntity> GetOne(string entityId)
        {
            var parameter = new DynamicParameters();
            parameter.Add($"{_tableName}Id", entityId);
            var entity = await _dbConnection.QueryFirstOrDefaultAsync<TEntity>($"Proc_Get{_tableName}ById", parameter, commandType: CommandType.StoredProcedure);
            return entity;
        }

        public async Task<int> UpdateOne(TEntity entity)
        {
            _dbConnection.Open();
            int rowCount;
            using (var transaction = _dbConnection.BeginTransaction())
            {
                var mapEntity = MappingDbType(entity);
                rowCount = await _dbConnection.ExecuteAsync($"Proc_Update{_tableName}", mapEntity, commandType: CommandType.StoredProcedure, transaction: transaction);
                transaction.Commit();
            }
            return rowCount;
        }
        /// <summary>
        /// Chuyển các trường có kiểu Guid về string
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>1 TEntity với các trường không còn kiểu Guid</returns>
        /// Created by: VLVU (27/7/2021)
        protected DynamicParameters MappingDbType(TEntity entity)
        {
            var properties = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                // không truyền EntityState vào parameters
                if (propertyType == typeof(EntityState))
                {
                    continue;
                }
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }

            return parameters;
        }
        public void Dispose()
        {
            if (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }

        }
        #endregion

    }

}
