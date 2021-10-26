using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Interfaces
{
    /// <summary>
    /// receiptPayment service interface
    /// Created by: VLVU (8/10/2021)
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
        /// <summary>
        /// Thêm 1 phiếu chi
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="paymentDetails"></param>
        /// <returns></returns>
        /// Created by: VLVU (15/10/2021)
        Task<ServiceResult> InsertPayment(ReceiptPayment payment, ReceiptPaymentDetail[] paymentDetails);
        /// <summary>
        /// Lấy 1 phiếu chi bằng id
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        Task<ServiceResult> GetPaymentById(Guid paymentId);
    }
}
