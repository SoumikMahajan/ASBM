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
    }
}