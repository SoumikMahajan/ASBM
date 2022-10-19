using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class BillModel
    {
        public int typeof_bill_id_pk { get; set; }
        public string typeof_bill_name { get; set; }
        public DateTime typeof_bill_create_date { get; set; }
    }
}