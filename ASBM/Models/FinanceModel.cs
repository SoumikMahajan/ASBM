using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class FinanceModel
    {
        public int finance_id_pk { get; set; }
        public string finance_vouter_no { get; set; }
        public string payment_voucher_no { get; set; }
        public string memo_no { get; set; }
        public string payment_mode { get; set; }
        public float total_net_amount { get; set; }
        public int payment_type_id { get; set; }
        public string bank_name { get; set; }
        public string bank_account_no { get; set; }
        public string fund_scheme_name { get; set; }
        public string scheme_name { get; set; }
        public string treasury_advice_no { get; set; }
        public string treasury_advice_date { get; set; }
        public int bank_id_fk { get; set; }
        public int fund_scheme_id_pk { get; set; }
        public int? scheme_id_pk { get; set; }
    }
}