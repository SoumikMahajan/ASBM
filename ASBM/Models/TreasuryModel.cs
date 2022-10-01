using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class TreasuryModel
    {
        public int treasury_scheme_id_pk { get; set; }
        public string treasury_advice_no { get; set; }
        public DateTime treasury_advice_date { get; set; }
    }
}