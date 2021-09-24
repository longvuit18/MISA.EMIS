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
    public interface IProviderRepository: IBaseRepository<Provider>
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
    }
}
