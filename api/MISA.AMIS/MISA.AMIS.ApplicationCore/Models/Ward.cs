using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Models
{
    /// <summary>
    /// Model xã phường
    /// </summary>
    /// Created by: VLVU (24/9/2021)
    public class Ward
    {
        /// <summary>
        /// khóa chính
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// tên xã
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// tiền tố trước tên (xã, phường)
        /// </summary>
        public string prefix { get; set; }
        /// <summary>
        /// mã tỉnh
        /// </summary>
        public int province_id { get; set; }
        /// <summary>
        /// mã huyện
        /// </summary>
        public int district_id { get; set; }
    }
}
