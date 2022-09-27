using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class BillSubmission
    {
        public int bill_details_id_pk { get; set; }
        public string bill_company_name { get; set; }
        public DateTime bill_CreateDate { get; set; }
    }
}