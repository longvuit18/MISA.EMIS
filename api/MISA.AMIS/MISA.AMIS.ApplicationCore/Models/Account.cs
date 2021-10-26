using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Models
{
	[TableName("account")]
	public class Account: BaseEntity
	{
		[PrimaryKey]
		public Guid account_id { get; set; }
		[Required]
		[DisplayName("Tên tài khoản")]
		public string account_name { get; set; }
		public string account_name_english { get; set; }
		[Required]
		[DisplayName("Số tài khoản")]
		public string account_number { get; set; }
		[Required]
		[DisplayName("Tính chất")]
		public int? account_category_kind { get; set; }
        public string description { get; set; }
        public int level { get; set; }
		public Guid? parent_id { get; set; }
		public bool is_postable_in_foreign_currency { get; set; }
		public int? account_object_type { get; set; }
		public bool detail_by_account_object { get; set; }
		public bool detail_by_bank_account { get; set; }
		public bool detail_by_contract { get; set; }
		public int? detail_by_contract_kind { get; set; }
		public bool detail_by_department { get; set; }
		public int? detail_by_department_kind { get; set; }
		public bool detail_by_expense_item { get; set; }
		public int? detail_by_expense_item_kind { get; set; }
		public bool detail_by_job { get; set; }
		public int? detail_by_job_kind { get; set; }
		public bool detail_by_list_item { get; set; }
		public int? detail_by_list_item_kind { get; set; }
		public bool detail_by_order { get; set; }
		public int? detail_by_order_kind { get; set; }
		public bool detail_by_project_work { get; set; }
		public int? detail_by_project_work_kind { get; set; }
		public bool detail_by_pu_contract { get; set; }
		public int? detail_by_pu_contract_kind { get; set; }
	}
}
