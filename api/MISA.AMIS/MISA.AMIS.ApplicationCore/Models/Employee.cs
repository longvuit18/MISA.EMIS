using MISA.AMIS.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Models
{
    /// <summary>
    /// Ở đây chứa thông tin các trường của Employee
    /// </summary>
    /// Created by : VLVU (27/7/2021)
    public class Employee : BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>

        [PrimaryKey]
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        /// 
        [NotDuplicate]
        [Required]
        [DisplayName("Mã nhân viên")]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>

        [Required]
        [DisplayName("Họ và tên")]
        [MaxLength(100)]
        public string EmployeeName { get; set; }
        /// <summary>
        /// Mã Giới tính
        /// </summary>
        public GenderEnum? Gender { get; set; }
        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case GenderEnum.Male:
                        return "Nam";
                    case GenderEnum.FeMale:
                        return "Nữ";
                    case GenderEnum.Undefined:
                        return "Khác";
                    default:
                        return string.Empty;
                }
            }
        }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        [MaxLength(20)]
        [DisplayName("Số điện thoại cố định")]
        public string TelephoneNumber { get; set; }
        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        [MaxLength(20)]
        [DisplayName("Số điện thoại di động")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [MaxLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Số chứng minh thư
        /// </summary>
        public string IdentityNumber { get; set; }
        /// <summary>
        /// Ngày đăng kí chứng minh thư
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi đăng kí chứng minh thư
        /// </summary>
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Id đơn vị
        /// </summary>

        [Required]
        [DisplayName("Tên đơn vị")]
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string EmployeePosition { get; set; }
        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string BankAccountNumber { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// Tên tỉnh của ngân hàng
        /// </summary>
        public string BankProvinceName { get; set; }

        /// <summary>
        /// Tên chi nhánh ngân hàng
        /// </summary>
        public string BankBranchName { get; set; }
    }
}
