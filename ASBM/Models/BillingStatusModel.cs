using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class BillingStatusModel
    {
        public int bill_allotement_id_pk { get; set; }
        public string bill_allotement_docket_no { get; set; }
        public DateTime bill_allotement_date { get; set; }
    }
}