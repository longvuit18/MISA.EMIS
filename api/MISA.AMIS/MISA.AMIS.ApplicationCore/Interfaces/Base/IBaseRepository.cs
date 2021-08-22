using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Interfaces
{
    /// <summary>
    ///  Interface của Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created by: VLVU (27/7/2021)
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lấy toàn bộ entity
        /// </summary>
        /// <returns></returns>
        /// Created by: VLVU (27/7/2021)
        Task<IEnumerable<TEntity>> GetAll();
        /// <summary>
        /// Lấy entity theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created by: VLVU (27/7/2021)
        Task<TEntity> GetOne(string id);
        /// <summary>
        /// Thêm 1 entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: VLVU (27/7/2021)
        Task<int> AddOne(TEntity entity);
        /// <summary>
        /// Update 1 entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: VLVU (27/7/2021)
        Task<int> UpdateOne(TEntity entity);
        /// <summary>
        /// Xóa entity theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created by: VLVU (27/7/2021)
        Task<int> DeleteOne(string id);
        /// <summary>
        /// Lấy entity bằng 1 thuộc tính nào đó
        /// </summary>
        /// <param name="parameters">1 dynamic parameters có @propertyName và @propertyValue</param>
        /// <returns></returns>
        /// Created by: VLVU (27/7/2021)
        Task<TEntity> GetEntityByProperty(DynamicParameters parameters);

    }
}
