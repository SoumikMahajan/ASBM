using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class BillingStatusModel
    {
        public string bill_docket_no { get; set; }
        public DateTime entry_time { get; set; }
        public string current_tatus { get; set; }
    }
}