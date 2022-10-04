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
        OfficerEntryClass officer = new OfficerEntryClass();
        PayeeEntryClass payee = new PayeeEntryClass();
        BankEntryClass bank = new BankEntryClass();
        DepartmentEntryClass dept = new DepartmentEntryClass();
        SchemeEntryClass scheme = new SchemeEntryClass();
        TreasuryEntryClass trsy = new TreasuryEntryClass();

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
        /////////////// REJECTED BILL STATUS End //////////////////////


        /////////////// OFFICER ENTRY Start //////////////////////
        public ActionResult OfficerEntry() 
        {
            List<OfficerModel> temp = new List<OfficerModel>();
            temp = officer.FetchAllOfficerList();
            return View(temp);
        }

        public ActionResult ajax_OfficerEntryForm()
        {
            return PartialView("~/Views/Home/_partialOfficerEntryView.cshtml");
        }

        [HttpPost]
        public int ajax_confirm_OfficerEntryForm(string officerName, string pan, string mobile, string gpf, int DeptId, string pass)
        {
            int response;
            response = officer.SubmitOfficer(officerName, pan, mobile, gpf, DeptId, pass);
            return response;
            //return PartialView("~/Views/Home/_partialBillSubmission_view.cshtml");
        }
        /////////////// OFFICER ENTRY End //////////////////////
        

        /////////////// Payee Master ENTRY Start //////////////////////
        public ActionResult PayeeEntry()
        {
            List<PayeeModel> temp = new List<PayeeModel>();
            temp = payee.FetchAllPayeeList();
            return View(temp);
        }

        public ActionResult ajax_PayeeEntryForm()
        {
            return PartialView("~/Views/Home/_partialPayeeEntryView.cshtml");
        }

        [HttpPost]
        public int ajax_confirm_PayeeEntryForm(string payeeName, string pan, string mobile, string gst, string accno, int deptId)
        {
            int response;
            response = payee.SubmitPayee(payeeName, pan, mobile, gst, accno, deptId);
            return response;
        }
        /////////////// Payee Master ENTRY End //////////////////////


        /////////////// Bank Master ENTRY Start //////////////////////
        public ActionResult MstBankDetails()
        {
            List<BankModel> temp = new List<BankModel>();
            temp = bank.FetchAllBankList();
            return View(temp);
        }

        public ActionResult ajax_BankEntryForm()
        {
            return PartialView("~/Views/Home/_partialBankEntryView.cshtml");
        }

        [HttpPost]
        public int ajax_confirm_BankEntryForm(string accNo, string accName, int fundId, string bankName, string ifsc)
        {
            int response;
            response = bank.SubmitBankDetails(accNo, accName, fundId, bankName, ifsc);
            return response;
        }
        /////////////// Bank Master ENTRY End //////////////////////


        /////////////// Department Master ENTRY Start //////////////////////
        public ActionResult MstDepartmentDetails()
        {
            List<DepartmentModel> temp = new List<DepartmentModel>();
            temp = dept.FetchAllDepartmentList();
            return View(temp);
        }

        public ActionResult ajax_DepartmentEntryForm()
        {
            return PartialView("~/Views/Home/_partialDepartmentEntryView.cshtml");
        }

        [HttpPost]
        public int ajax_confirm_DepartmentEntryForm(string deptName)
        {
            int response;
            response = dept.SubmitDepartmentDetails(deptName);
            return response;
        }
        /////////////// Department Master ENTRY End //////////////////////
        

        /////////////// Scheme Master ENTRY Start //////////////////////
        public ActionResult MstSchemeDetails()
        {
            List<SchemeModel> temp = new List<SchemeModel>();
            temp = scheme.FetchAllSchemeList();
            return View(temp);
        }

        public ActionResult ajax_SchemeEntryForm()
        {
            return PartialView("~/Views/Home/_partialSchemaEntryView.cshtml");
        }

        [HttpPost]
        public int ajax_confirm_SchemeEntryForm(string schemeName)
        {
            int response;
            response = scheme.SubmitSchemeDetails(schemeName);
            return response;
        }
        /////////////// Scheme Master ENTRY End //////////////////////
        

        /////////////// Treasury Master ENTRY Start //////////////////////
        public ActionResult MstTreasuryDetails()
        {
            List<TreasuryModel> temp = new List<TreasuryModel>();
            temp = trsy.FetchAllTreasulyList();
            return View(temp);
        }

        public ActionResult ajax_TreasuryEntryForm()
        {
            return PartialView("~/Views/Home/_partialTreasuryEntryView.cshtml");
        }

        [HttpPost]
        public int ajax_confirm_TreassuryEntryForm(string adviceNo, string adviceDate)
        {
            int response;
            response = trsy.SubmitTreasuryDetails(adviceNo, adviceDate);
            return response;
        }
        /////////////// Treasury Master ENTRY End //////////////////////
    }
}