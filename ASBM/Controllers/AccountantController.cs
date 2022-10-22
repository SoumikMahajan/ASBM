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
            return PartialView("~/Views/Home/_partialAccountantVouterDataTable.cshtml", mm);            
        }
    }
}