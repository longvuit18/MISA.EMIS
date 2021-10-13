using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Models
{
	[TableName("receipt_payment_detail")]
    public class ReceiptPaymentDetail: BaseEntity
    {
		public Guid ref_detail_id { get; set; }
		public Guid refid { get; set; }
		public string account_object_code { get; set; }
		public string account_object_name { get; set; }
		public Guid account_object_id { get; set; }
		public double amount { get; set; }
		public string credit_account { get; set; }
		public string debit_account { get; set; }
		public string description { get; set; }
		public string bank_account { get; set; }
	}
}
