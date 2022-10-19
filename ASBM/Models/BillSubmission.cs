using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class BillSubmission
    {
        public int bill_details_id_pk { get; set; }
        public string bill_docket_no { get; set; }
        public string bill_category_id_fk { get; set; }
        public string bill_company_name { get; set; }
        //public string bill_proprietor_name { get; set; }
        public string bill_type_id_fk { get; set; }
        public string department_name { get; set; }
        public string bill_pan { get; set; }
        public string bill_gst { get; set; }
        public string fund_scheme_name { get; set; }
        public string bill_description { get; set; }
        public string bill_amount { get; set; }
        public string bill_department_id_fk { get; set; }
        public DateTime bill_CreateDate { get; set; }
    }
}