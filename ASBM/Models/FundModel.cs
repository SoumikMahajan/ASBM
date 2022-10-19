using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class FundModel
    {
        public int fund_scheme_id_pk { get; set; }
        public string fund_scheme_name { get; set; }
        public DateTime fund_createdate { get; set; }
    }
}