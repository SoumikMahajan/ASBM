using ASBM.Models;
using ASBM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASBM.Controllers
{
    public class HomeController : Controller
    {
        BillSubmissionClass bill = new BillSubmissionClass();
        BillAllotementClass billAllote = new BillAllotementClass();
        RandomBillGeneratorClass randomBill = new RandomBillGeneratorClass();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact() //added by koushik 
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        //public ActionResult BillSubmissionForm() //added by koushik 
        //{

        //    ViewBag.Message = "Your contact page.";
        //    return View();
        //}

        public ActionResult ActionBasedOnDept() //added by koushik 
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        /////////////// BILL SUBMISSION Start//////////////////////       

        public ActionResult ajax_BillSubmissionForm()
        {
            return PartialView("~/Views/Home/_partialBillSubmissionView.cshtml");
        }

        [HttpPost]
        public int ajax_confirm_BillSubmissionForm(string CompanyName, string PropriterName, string CompanyCategoryName, int DepartmentId, string Pan, string Gst, int FundId, string WorkDesc, string Amount) 
        {
            int response;
            response = bill.SubmitBill(CompanyName, PropriterName, CompanyCategoryName, DepartmentId, Pan, Gst, FundId, WorkDesc, Amount);
            return response;
            //return PartialView("~/Views/Home/_partialBillSubmission_view.cshtml");
        }

        public string GetAllDept()
        {
            string result = bill.FetchAllDept();
            return result;
        }
        
        public string GetAllFund()
        {
            string result = bill.FetchAllFund();
            return result;
        }

        public ActionResult BillSubmissionForm()
        {
            List<BillSubmission> temp = new List<BillSubmission>();
            temp = bill.FetchAllBillSubmission();
            return View(temp);
        }

        /////////////// BILL ALLOTEMENT End//////////////////////



        /////////////// BILL ALLOTEMENT Start//////////////////////
        public ActionResult BillAllotment()
        {
            List<BillAllotementModel> temp = new List<BillAllotementModel>();
            temp = billAllote.FetchAllBillAllotementDetails();
            return View(temp);
        }

        public string GetAllOfficer()
        {
            string result = billAllote.FetchAllOfficer();
            return result;
        }

        public ActionResult ajax_BillAllotementForm() 
        {
            return PartialView("~/Views/Home/_partialBillAllotementView.cshtml");
        }

        [HttpPost]
        public int ajax_confirm_BillAllotementForm(string DocketNo, int DepartmentId, int OfficerId, DateTime AllotedDatte)
        {
            int response;
            response = billAllote.SubmitBillAllotement(DocketNo, DepartmentId, OfficerId, AllotedDatte);
            return response;
        }

        /////////////// BILL ALLOTEMENT End //////////////////////


        /////////////// RANDOM BILL GENERATION Start //////////////////////
        public ActionResult RandomBillGeneretorForm()
        {
            List<RandomBillGenerationModel> temp = new List<RandomBillGenerationModel>();
            temp = randomBill.FetchAllRandomBill();
            return View(temp);
        }

        public ActionResult ajax_RandomBillGeneratorForm()
        {
            return PartialView("~/Views/Home/_partialRandomGeneratorView.cshtml");
        }

        [HttpPost]
        public int ajax_confirm_RandomBillGeneratorForm(string Name, int BillTypeId, int DepartmentId, int FundId, string WorkDesc, string Mobile)
        {
            int response;
            response = randomBill.SubmitRandomBill(Name, BillTypeId, DepartmentId, FundId, WorkDesc, Mobile);
            return response;
        }

        //public ActionResult RandomBillGeneratorTable()
        //{

        //}
        /////////////// RANDOM BILL GENERATION End //////////////////////
        ///


        /////////////// KNOW YOUR BILLING STATUS Start //////////////////////

        public ActionResult BillingStatus()
        {           
            return View();
        }


        /////////////// END YOUR BILLING STATUS Start //////////////////////


        /////////////// REJECTED BILL STATUS Start //////////////////////

        public ActionResult RejectedBill()
        {
            return View();
        }

    }
}