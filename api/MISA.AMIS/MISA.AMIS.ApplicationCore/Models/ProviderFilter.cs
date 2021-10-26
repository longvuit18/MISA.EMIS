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
        /// <summary>
        /// mã nhà cung cấp
        /// </summary>
        public string account_object_code { get; set; }
        /// <summary>
        /// tên nhà cung cấp
        /// </summary>
        public string account_object_name { get; set; }
        /// <summary>
        /// địa chỉ
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// mô tả
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// mã số thuế
        /// </summary>
        public string tax_code { get; set; }
        /// <summary>
        /// số điện thoại
        /// </summary>
        public string phone_number{ get; set; }
        /// <summary>
        /// só chứng minh thư
        /// </summary>
        public string personal_contact_identity_number { get; set; }
        /// <summary>
        /// tên tỉnh hoặc thành phố
        /// </summary>
        public string province_or_city { get; set; }
        /// <summary>
        /// tên huyện
        /// </summary>
        public string district { get; set; }
        /// <summary>
        /// tên xã
        /// </summary>
        public string ward_or_commune { get; set; } 
        /// <summary>
        ///nhóm nhà cung cấp
        /// </summary>
        public string account_object_group { get; set; }
        /// <summary>
        /// loại nhà cung cấp
        /// </summary>
        public int? account_object_type { get; set; }
    }
}
