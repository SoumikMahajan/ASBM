using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class OfficerModel
    {
        public int officer_user_id_pk { get; set; }
        public string officer_name { get; set; }
        public string officer_mobile { get; set; }
        public string officer_pan { get; set; }
        public string officer_gpf { get; set; }
        public int officer_dept_id_fk { get; set; }
        public string department_name { get; set; }
    }
}