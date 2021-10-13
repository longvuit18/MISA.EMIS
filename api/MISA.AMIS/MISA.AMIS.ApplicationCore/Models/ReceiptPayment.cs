using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Models
{
	[TableName("receipt_payment")]
    public class ReceiptPayment: BaseEntity
    {
			[PrimaryKey]
			public Guid refid { get; set; }
			public string refno_finance { get; set; }
			public DateTime posted_date { get; set; }
			public DateTime refdate { get; set; }
			public string journal_memo { get; set; }
			public double total_amount { get; set; }
			public Guid? account_object_id { get; set; }
			public Guid? employee_id { get; set; }
			public string account_object_address { get; set; }
			public string account_object_name { get; set; }
			public string currency_id { get; set; }
			public string document_included { get; set; }
	}
}
