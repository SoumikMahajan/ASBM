using ASBM.Models;
using ASBM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASBM.Controllers
{
    public class VoucherGeneratorController : Controller
    {
        VouterGeneratorClass vouter = new VouterGeneratorClass();
        // GET: VoucherGenerator
        public ActionResult Index()
        {
            if (Session["UserID"] != null && Session["UserRoleId"].ToString() == "2")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public string GetAllDocketNo(int radioVal)
        {
            string result = vouter.FetchAllDocketNo(radioVal);
            return result;
        }

        [HttpPost]
        public int ajax_approved_VoucherSubmission(string BillDocketType, string billdocketno, string iscic, string cicno, string isetender, string tenno, string ismed, string medDate, string MeetingTenderCommitteeDate, string MeetingChairmanDate, string Tenderrate, string MeetingBOCDate, string AAFSNo, string WorkOrder, string WorkOrderDate, string AmountEstimate, string MBBookNo, string PageNo, string WorkRegisterNo, string WorkRegisterDate, string CertifiedBy, string CertificationDate, string IsApprovedByChairman, string chairpersonApprovalDate)
        {
            int response;
            if (medDate == "")
            {
                medDate = null;
            }
            if (MeetingTenderCommitteeDate == "")
            {
                MeetingTenderCommitteeDate = null;
            }
            if (MeetingChairmanDate == "")
            {
                MeetingChairmanDate = null;
            }
            if (MeetingBOCDate == "")
            {
                MeetingBOCDate = null;
            }
            if (WorkOrderDate == "")
            {
                WorkOrderDate = null;
            }
            if (WorkRegisterDate == "")
            {
                WorkRegisterDate = null;
            }
            if (CertificationDate == "")
            {
                CertificationDate = null;
            }
            if (chairpersonApprovalDate == "")
            {
                chairpersonApprovalDate = null;
            }

            response = vouter.ApprovedVoucher(BillDocketType, billdocketno, iscic, cicno, isetender, tenno, ismed, medDate, MeetingTenderCommitteeDate, MeetingChairmanDate, Tenderrate, MeetingBOCDate, AAFSNo, WorkOrder, WorkOrderDate, AmountEstimate, MBBookNo, PageNo, WorkRegisterNo, WorkRegisterDate, CertifiedBy, CertificationDate, IsApprovedByChairman, chairpersonApprovalDate);

            return response;

        }

        [HttpPost]
        public int ajax_reject_Voucher(int BillDocketType, string billdocketno)
        {
            int response;
            response = vouter.RejectVoucher(BillDocketType, billdocketno);

            return response;

        }

        [HttpGet]
        public JsonResult ajax_getbilldeatilsby_DocketNo(string billdocketno)
        {
            BillSubmission bs = new BillSubmission();
            try
            {
                if (!string.IsNullOrEmpty(billdocketno))
                {
                    //mm = cms.GetHomeBannerById(Convert.ToInt16(id));
                    bs = vouter.FetchNormalBillingdetails(billdocketno);
                }
            }
            catch (Exception ex)
            {

            }
            //return PartialView("~/Views/VoucherGenerator/Index.cshtml", mm);
            return Json(bs, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ajax_getbilldetailsForRandoms(string billdocketno)
        {
            RandomBillGenerationModel rn = new RandomBillGenerationModel();
            try
            {
                if (!string.IsNullOrEmpty(billdocketno))
                {
                    //mm = cms.GetHomeBannerById(Convert.ToInt16(id));
                    rn = vouter.FetchRandomBillingdetails(billdocketno);
                }
            }
            catch (Exception ex)
            {

            }
            //return PartialView("~/Views/VoucherGenerator/Index.cshtml", mm);
            return Json(rn, JsonRequestBehavior.AllowGet);
        }

    }
}