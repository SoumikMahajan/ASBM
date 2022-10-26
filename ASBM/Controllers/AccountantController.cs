using ASBM.Models;
using ASBM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASBM.Controllers
{
    public class AccountantController : Controller
    {
        AccountantClass acc = new AccountantClass();

        // GET: Accountant
        public ActionResult Index()
        {
            if (Session["UserID"] != null && Session["UserRoleId"].ToString() == "3")
            {
                //MultipleModel mm = new MultipleModel();
                //mm = acc.Fetch_All_Vouter_Details();
                //return View(mm);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Get_All_Vouter_Details()
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm = acc.Fetch_All_Vouter_Details();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView("~/Views/Accountant/_partialAccountantVouterDataTable.cshtml", mm);
        }

        public JsonResult Get_Vouter_Details(int id)
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm = acc.Fetch_Vouter_Details(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(mm, JsonRequestBehavior.AllowGet);
            //return View(mm);
            //return PartialView("~/Views/Accountant/_partialAccountantVouterDataTable.cshtml", mm);
        }

        public ActionResult AmountCalculation()
        {
            return View();
        }

        public string GetAllBank()
        {
            string result = acc.FetchAllBank();
            return result;
        }

        public JsonResult Get_bank_acc_Details(int bankId)
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm = acc.Fetch_bank_acc_Details(bankId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(mm, JsonRequestBehavior.AllowGet);
        }

        public string GetAllSchemeName()
        {
            string result = acc.FetchAllSchemeName();
            return result;
        }

        public JsonResult Get_Treasury_Details(int SchemeId)
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm = acc.Fetch_Treasury_Details(SchemeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(mm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int ajax_Finilize_Payment(string VoucherNo,float BasicBill, float SgstVal, float CgstVal, float Igst, float BasicCess, float GrossAmount, float ItTds, float SdMoney, float GrossCess, float TdsCgst, float TdsSgst, float Pf, float PfAdvance, float Ptax, float CcsCount, float CcsLic, float CcsLoan, float Coop, float Gi, float Lic, float Festival, float TotalDeduction, float NetAmountBill, int PaymentTypeId, int BankId, string BankAccNo, int FundSchemeId, int TreasurySchemeId, string TreasuryAdviceNo, string TreasuryAdviceDate)
        {
            int response;
            response = acc.Finilize_Payment(VoucherNo, BasicBill, SgstVal, CgstVal, Igst, BasicCess, GrossAmount, ItTds, SdMoney, GrossCess, TdsCgst, TdsSgst, Pf, PfAdvance, Ptax, CcsCount, CcsLic, CcsLoan, Coop, Gi, Lic, Festival, TotalDeduction, NetAmountBill, PaymentTypeId, BankId, BankAccNo, FundSchemeId, TreasurySchemeId, TreasuryAdviceNo, TreasuryAdviceDate);

            return response;

        }
    }
}