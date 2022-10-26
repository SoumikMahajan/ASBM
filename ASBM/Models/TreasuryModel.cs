using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class TreasuryModel
    {
        public int treasury_id_pk { get; set; }
        public string treasury_advice_no { get; set; }
        public DateTime treasury_advice_date { get; set; }
        public int scheme_id_fk { get; set; }
        public string scheme_name { get; set; }
    }
}