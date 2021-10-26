using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.ApplicationCore.Models
{
    [TableName("account_object")]
    public class AccountObject : BaseEntity
    {
        [PrimaryKey]
        public Guid account_object_id { get; set; }
        [NotDuplicate]
        [Required]
        public string account_object_code { get; set; }
        [Required]
        public string account_object_name { get; set; }
        public string address { get; set; }
        public bool is_customer { get; set; }
        public string country { get; set; }
        public string prefix { get; set; }
        public string address_detail { get; set; }
        public string account_object_group { get; set; }
        public short account_object_type { get; set; }
        public Guid employee_id { get; set; }
        public string phone_number { get; set; }
        public string website { get; set; }
        public string personal_contact_email { get; set; }
        public string personal_contact_phone { get; set; }
        public string personal_contact_telephone { get; set; }
        public string legal_representative { get; set; }
        public string personal_contact_identity_number { get; set; }
        public DateTime? personal_contact_identity_date { get; set; }
        public string personal_contact_identity_place { get; set; }
        public string organization_contact_name { get; set; }
        public string organization_contact_email { get; set; }
        public string organization_contact_phone { get; set; }
        public int due_time_debt { get; set; }
        public int maximize_debt_amount { get; set; }
        public string pay_account { get; set; }
        public Guid? payment_term_id { get; set; }
        public string bank_account_group { get; set; }
        public string province_or_city { get; set; }
        public string ward_or_commune { get; set; }
        public string district { get; set; }
        public bool is_same_address { get; set; }
        public string shipping_address { get; set; }
        public string description { get; set; }
        public string tax_code { get; set; }
        public string einvoice_contact_email { get; set; }
        public string einvoice_contact_name { get; set; }
        public string einvoice_contact_phone { get; set; }
        public string receive_account { get; set; }
    }
}
