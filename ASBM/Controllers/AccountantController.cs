using ASBM.Models;
using ASBM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}