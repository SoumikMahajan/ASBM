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

        public AccountantModel accountant { get; set; }
        public List<AccountantModel> accountantList { get; set; }

        public VoucherModel voucher { get; set; }
        public List<VoucherModel> voucherList { get; set; }

        public FinanceModel finance { get; set; }
        public List<FinanceModel> financeList { get; set; }

        public RejectedBillModel rejectBill { get; set; }
        public List<RejectedBillModel> rejectBillList { get; set; }
    }
}
