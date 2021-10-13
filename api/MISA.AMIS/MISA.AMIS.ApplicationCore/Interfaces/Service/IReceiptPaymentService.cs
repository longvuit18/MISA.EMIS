using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Interfaces
{
    /// <summary>
    /// employee service interface
    /// Created by: VLVU (18/8/2021)
    /// </summary>
    public interface IReceiptPaymentService: IBaseService<ReceiptPayment>
    {
        
        /// <summary>
        /// filter cho provider
        /// </summary>
        /// <param name="filter">object filter của phiếu thu chi</param>
        /// <returns></returns>
        /// Created by: VLVU (8/10/2021)
        Task<ServiceResult> GetReceiptPaymentFilterPaging(ReceiptPaymentFilter filter);
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
        Task<ServiceResult> DeleteBatch(Guid[] listId);
    }
}
