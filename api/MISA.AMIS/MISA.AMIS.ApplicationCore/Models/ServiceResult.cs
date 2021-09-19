using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Models
{
    /// <summary>
    /// Thông tin kết quả mà service trả về
    /// </summary>
    /// Created by: VLVU (28/7/2021)
    public class ServiceResult
    {
        /// <summary>
        /// Mã Misa code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Trạng thái của kết quả trả về
        /// </summary>
        public bool State { get; set; } = true;
        /// <summary>
        /// Dữ liệu kết quả trả về
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Thông báo lỗi cho dev
        /// </summary>
        public string MsgDev { get; set; }
        /// <summary>
        /// Thông báo lỗi cho người dùng
        /// </summary>
        public string MsgUser { get; set; }
    }
}
