using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Models
{
    /// <summary>
    /// Department model
    /// Created by: VLVU (18/8/2021)
    /// </summary>
    public class Department: BaseEntity
    {
        /// <summary>
        /// id của đơn vị
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
