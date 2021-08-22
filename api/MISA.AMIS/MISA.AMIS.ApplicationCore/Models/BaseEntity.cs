﻿using MISA.AMIS.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Models
{
    /// <summary>
    /// Attribute khóa chính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute { }
    /// <summary>
    /// Attibute không được phép trùng
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotDuplicate : Attribute { }
    /// <summary>
    /// Atribute Bắt buộc nhập
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute { }

    /// <summary>
    /// Atribute Số lượng kí tự tối đa
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength : Attribute {
        public int Length { get;}

        public MaxLength(int length)
        {
            Length = length;
        }
    }
    /// <summary>
    /// Base entity cho các models
    /// </summary>
    /// Created by: VLVU(8/2/2021)
    public class BaseEntity
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Trạng thái của các Request
        /// </summary>
        public EntityState EntityState { get; set; } = EntityState.POST;
    }
}
