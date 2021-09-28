using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Models
{
    /// <summary>
    /// model huyện
    /// </summary>
    public class District
    {
        /// <summary>
        /// khóa chính
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// tên huyện
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// mã tỉnh
        /// </summary>
        public int province_id { get; set; }
        /// <summary>
        /// tiền tố trước tên (Huyện, quận)
        /// </summary>
        public string prefix { get; set; }
    }
}
