using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Models
{
    public class ReceiptPaymentFilter
    {
        /// <summary>
        /// text custom search
        /// </summary>
        public string CustomFilter { get; set; }
        /// <summary>
        /// Trang số bao nhiêu
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// Số lượng record trong trang
        /// </summary>
        public int PageSize { get; set; } = 20;
       /// <summary>
       /// ngày bắt đầu filter
       /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// ngày kết thúc filter
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
