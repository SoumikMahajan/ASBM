using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class BillAllotementModel
    {
        public int bill_allotement_id_pk { get; set; }
        public string bill_allotement_docket_no { get; set; }
        public int bill_allotement_dept_id_fk { get; set; }
        public int bill_allotement_officer_id_fk { get; set; }
        public DateTime bill_allotement_date { get; set; }
        public string process_status { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime entry_time { get; set; }
        public int active_status { get; set; }
        public string department_name { get; set; }
        public string officer_name { get; set; }

    }
}