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
	public class Employee: BaseEntity
	{
		[PrimaryKey]
		[Required]
		public Guid employee_id { get; set; }
		[Required]
		[NotDuplicate]
		public string employee_code { get; set; }
		public string employee_name { get; set; }
		public DateTime date_of_birth { get; set; }
		public int gender { get; set; }
		public Guid department_id { get; set; }
		public string identity_number { get; set; }
		public DateTime identity_date { get; set; }
		public string identity_place { get; set; }
		public string employee_position { get; set; }
		public string address { get; set; }
		public string bank_account_number { get; set; }
		public string bank_name { get; set; }
		public string bank_branch_name { get; set; }
		public string bank_province_name { get; set; }
		public string phone_number { get; set; }
		public string telephone_number { get; set; }
		public string email { get; set; }
	}
}
