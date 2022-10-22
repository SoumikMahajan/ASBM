using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASBM.Models
{
    public class MultipleModel
    {
        public OfficerModel officerModel { get; set; }
        public List<OfficerModel> officerList { get; set; }

        public BillingStatusModel billingStatusModel { get; set; }
        public List<BillingStatusModel> billingStatusList { get; set; }

        public AccountantModel accountantModel { get; set; }
        public List<AccountantModel> AccountantList { get; set; }
    }
}
