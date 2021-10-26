using MISA.AMIS.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Interfaces
{
    /// <summary>
    ///  receiptPayment repository interface
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
        /// reposirory lấy một mã nhà cung cấp mới
        /// </summary>
        /// <returns></returns>
        /// Created by: VLVU (20/9/2021)
        Task<string> GetNewReceiptPaymentCode();
        /// <summary>
        /// reposirory xóa hàng loạt
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// Created by: VLVU (8/10/2021)
        Task<int> DeleteBatch(Guid[] listId);
        /// <summary>
        /// Thêm 1 phiếu chi
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="paymentDetails"></param>
        /// <returns></returns>
        /// Created by: VLVU (15/10/2021)
        Task<int> InsertPayment(ReceiptPayment payment, ReceiptPaymentDetail[] paymentDetails);
        /// <summary>
        /// Lấy 1 phiếu chi bằng id
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        /// /// Created by: VLVU (15/10/2021)
        Task<object> GetPaymentById(Guid paymentId);
    }
}
