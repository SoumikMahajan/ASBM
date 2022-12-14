using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace ASBM.Models
{
    public class AccountantModel
    {
        public int voucher_id_pk { get; set; }
        public string voucher_no { get; set; }
        public DateTime vouter_date { get; set; }
        public string bill_category_id_fk { get; set; }
        public string bill_company_name { get; set; }
        public string department_name { get; set; }
        public string payee_name { get; set; }
        public string fund_scheme_name { get; set; }
        public string bill_gst { get; set; }
        public string bill_description { get; set; }
        public string bill_amount { get; set; }
        public string entry_time { get; set; }
        public string mobile_no { get; set; }
        public int bank_id_pk { get; set; }
        public string bank_account_no { get; set; }
        public string treasury_advice_no { get; set; }
        public string treasury_advice_date { get; set; }
        public int fund_scheme_id_pk { get; set; }
    }
}