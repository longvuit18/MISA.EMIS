using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Models
{
    /// <summary>
    /// Model Các nước trên thế giới
    /// </summary>
    /// Created by: VLVU (24/7/2021)
    public class Country
    {
        /// <summary>
        /// khóa chính
        /// </summary>
        public Guid country_id { get; set; }
        /// <summary>
        /// mã nước
        /// </summary>
        public string country_code { get; set; }
        /// <summary>
        /// Tên nước
        /// </summary>
        public string country_name { get; set; }
    }
}
