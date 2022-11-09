using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class VoucherModel
	{
		public int voucher_id_pk { get; set; }
		public string voucher_no { get; set; }
		public string bill_docket_no { get; set; }
		public int bill_docket_type_id { get; set; }
		public short IsCICnumber { get; set; }
		public long CICNumber { get; set; }
		public short IsParticipateTender { get; set; }
		public long ParticipateTenderNumber { get; set; }
		public short IsMed { get; set; }
		public DateTime MedDate { get; set; }
		public DateTime TenderCommitteeMeeting { get; set; }
		public DateTime ChairmanMeeting { get; set; }
		public string TenderRate { get; set; }
		public DateTime BOCSanctionMeeting { get; set; }
		public string AAFSNumber { get; set; }
		public string WorkOrderNo { get; set; }
		public DateTime WorkOrderDate { get; set; }
		public string EstimateAmount { get; set; }
		public string MBBookNo { get; set; }
		public string PageNo { get; set; }
		public string WorkRegNo { get; set; }
		public DateTime WorkRegNoDate { get; set; }
	}
}