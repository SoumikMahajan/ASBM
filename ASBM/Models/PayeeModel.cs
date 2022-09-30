using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class PayeeModel
    {
        public int payee_id_pk { get; set; }
        public string payee_type { get; set; }
        public string payee_name { get; set; }
        public string payee_pan { get; set; }
        public string payee_mobile { get; set; }
        public string payee_gst { get; set; }
        public string payee_account_no { get; set; }
        public int payee_department_id_fk { get; set; }
        public int payee_bill_id_fk { get; set; }
        public int payee_fund_id_fk { get; set; }
        public string department_name { get; set; }
    }
}