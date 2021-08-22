using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Constants
{
    /// <summary>
    /// Ở đây chứa những thông tin về mã code trả về người dùng
    /// </summary>
    /// Created by: VLVU (29/7/2021)
    public static class MisaCode
    {
        /// <summary>
        /// Trả về khi gặp exception
        /// </summary>
        public static readonly string Exception = "MISA-005";
        /// <summary>
        /// Trả về khi request thành công
        /// </summary>
        public static readonly string Success = "MISA-002";
        /// <summary>
        /// Trả về khi request thất bại
        /// </summary>
        public static readonly string Fail = "MISA-003";
        /// <summary>
        /// Trả về khi request no content
        /// </summary>
        public static readonly string NoContent = "MISA-004";
        
            
    }
}
