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
    public interface IReceiptPaymentRepository : IBaseRepository<ReceiptPayment>
    {
        /// <summary>
        /// filter cho provider
        /// </summary>
        /// <param name="filter">object filter của phiếu thu chi</param>
        /// <returns></returns>
        /// Created by: VLVU (8/10/2021)
        Task<object> GetReceiptPaymentFilterPaging(ReceiptPaymentFilter filter);
        /// <summary>
        /// service lấy một mã nhà cung cấp mới
        /// </summary>
        /// <returns></returns>
        /// Created by: VLVU (20/9/2021)
        Task<string> GetNewReceiptPaymentCode();
        /// <summary>
        /// service xóa hàng loạt
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// Created by: VLVU (8/10/2021)
        Task<int> DeleteBatch(Guid[] listId);
    }
}
