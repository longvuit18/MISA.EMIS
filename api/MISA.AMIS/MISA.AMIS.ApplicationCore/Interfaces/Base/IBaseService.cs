using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Interfaces
{
    /// <summary>
    /// interface của base service
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created by: VLVU (28/7/2021)
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// Lấy toàn bộ entity
        /// </summary>
        /// <returns>1 mảng entity</returns>
        /// Created by: VLVU (27/7/2021)
        Task<ServiceResult> GetAll();
        /// <summary>
        /// Lấy entity theo id
        /// </summary>
        /// <param name="id">id của entity</param>
        /// <returns></returns>
        /// Created by: VLVU (27/7/2021)
        Task<ServiceResult> GetOne(Guid id);
        /// <summary>
        /// Thêm 1 entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>trả về service result</returns>
        /// Created by: VLVU (27/7/2021)
        Task<ServiceResult> AddOne(TEntity entity);
        /// <summary>
        /// Update 1 entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: VLVU (27/7/2021)
        Task<ServiceResult> UpdateOne(TEntity entity);
        /// <summary>
        /// Xóa entity theo id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: VLVU (27/7/2021)
        Task<ServiceResult> DeleteOne(Guid entity);
    }
}
