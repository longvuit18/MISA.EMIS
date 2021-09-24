using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Models
{
    /// <summary>
    /// Khai báo các trường có trong route provider filter
    /// </summary>
    /// Created by: VLVU (20/9/2021)
    public class ProviderFilter
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
    }
}
