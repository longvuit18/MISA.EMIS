using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Interfaces
{
    /// <summary>
    /// employee repository interface
    /// </summary>
    /// Created by: VLVU (18/8/2021)
    public interface IProviderRepository: IBaseRepository<AccountObject>
    {
        /// <summary>
        /// filter cho provider
        /// </summary>
        /// <param name="filter">object filter của provider</param>
        /// <returns></returns>
        /// Created by: VLVU (20/9/2021)
        Task<object> GetProviderFilterPaging(ProviderFilter filter);
        /// <summary>
        /// service lấy một mã nhà cung cấp mới
        /// </summary>
        /// <returns></returns>
        /// Created by: VLVU (20/9/2021)
        Task<string> GetNewProviderCode();
        /// <summary>
        /// service xóa hàng loạt
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// Created by: VLVU (29/9/2021)
        Task<int> DeleteBatch(Guid[] listId);
        /// <summary>
        /// Check phát sinh dữ liệu
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// Created by: VLVU (15/10/2021)
        Task<int> CheckArising(Guid[] listId);
    }
}
