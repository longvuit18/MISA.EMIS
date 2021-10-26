using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Models
{
    /// <summary>
    /// Form dữ liệu gửi lên từ client
    /// </summary>
    /// Created by: VLVU (18/10/2021)
    public class PaymentRequest
    {
        /// <summary>
        /// Phiếu chi master
        /// </summary>
        public ReceiptPayment payment { get; set; }
        /// <summary>
        /// Phiếu chi chi tiết
        /// </summary>
        public ReceiptPaymentDetail[] paymentDetails { get; set; }
    }
}
