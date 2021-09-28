using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Models
{
    /// <summary>
    /// Model tỉnh thành
    /// </summary>
    /// Created by: VLVU (24/9/2021)
    public class Province
    {
        /// <summary>
        /// id tỉnh, khóa chính
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// tên tỉnh
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// mã tỉnh
        /// </summary>
        public string code { get; set; }
    }
}
