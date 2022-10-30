using ASBM.Models;
using ASBM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASBM.Controllers
{
    public class FinanceController : Controller
    {
        FinanceClass fin = new FinanceClass();
        public ActionResult Index()
        {
            if (Session["UserID"] != null && Session["UserRoleId"].ToString() == "4")
            {
                MultipleModel mm = new MultipleModel();
                try
                {
                    mm = fin.Fetch_All_Vouter_Details();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return View(mm);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }            
        }

        public JsonResult Get_Vouter_Details(string voucherNo)
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm = fin.Fetch_Vouter_Details(voucherNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(mm, JsonRequestBehavior.AllowGet);            
        }

        //public string GetBankDetails(int bankId)
        //{
        //    string result = fin.FetchBankDetails(bankId);
        //    return result;
        //}

        public JsonResult Get_bank_acc_Details(int bankId)
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm = fin.Fetch_bank_acc_Details(bankId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(mm, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_Treasury_Details(int SchemeId)
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm = fin.Fetch_Treasury_Details(SchemeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(mm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int ajax_Finilize_Payment(string PaymentMode, string MemoNo, float NetAmount, string VoucherNo)
        {
            int response;
            response = fin.Finilize_Payment(PaymentMode, MemoNo, NetAmount, VoucherNo);

            return response;

        }

    }
}