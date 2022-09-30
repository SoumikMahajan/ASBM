using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class BankModel
    {
        public int bank_id_pk { get; set; }
        public string bank_account_no { get; set; }
        public string bank_account_name { get; set; }
        public string bank_name { get; set; }
        public string bank_ifsc { get; set; }
        public string bank_fund_id_fk { get; set; }
        public string fund_scheme_name { get; set; }
    }
}