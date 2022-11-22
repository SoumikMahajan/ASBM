using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class RandomBillGenerationModel
    {
        public int random_bill_id_pk { get; set; }
        public string random_bill_docket_no { get; set; }
        public string random_bill_name { get; set; }
        public int random_bill_dept_id_fk { get; set; }
        public int random_bill_fund_id_fk { get; set; }
        public int random_bill_type_id_fk { get; set; }
        public string random_bill_work_desc { get; set; }
        public string random_bill_mobile_no { get; set; }
        public string process_status { get; set; }
        public DateTime creation_date { get; set; }
        public int active_status { get; set; }
        public string department_name { get; set; }
        public string fund_scheme_name { get; set; }
        public string typeof_bill_name { get; set; }
    }
}