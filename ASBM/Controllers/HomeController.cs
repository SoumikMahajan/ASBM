using Antlr.Runtime.Misc;
using ASBM.Models;
using ASBM.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        FundEntryClass fund = new FundEntryClass();
        BillEntryClass Billmst = new BillEntryClass();
        BillingStatusClass billStatus = new BillingStatusClass();
        RejectedBillClass rejBill = new RejectedBillClass();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult login(string email, string password)
        {
            try
            {
                if (email != null && password != null)
                {
                    LoginClass login = new LoginClass();
                    LoginModel UserDetails = login.Loginvalidate(email, password);
                    Session["UserId"] = UserDetails.userId;
                    Session["UserRole"] = UserDetails.userRole;
                    Session["UserRoleId"] = UserDetails.userRoleId;
                    if (UserDetails.userRoleId == 1)
                    {
                        return RedirectToAction("ActionBasedOnDept");
                    }
                    else if (UserDetails.userRoleId == 2)
                    {
                        return RedirectToAction("Index", "VoucherGenerator");
                    }
                    else if (UserDetails.userRoleId == 3)
                    {
                        return RedirectToAction("Index", "Accountant");
                    }
                    else if (UserDetails.userRoleId == 4)
                    {
                        return RedirectToAction("Index", "Finance");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }

        public ActionResult Logout()
        {
            try
            {
                Session.Remove("UserId");
                Session.Remove("UserRole");
                Session.Remove("UserRoleId");
                //string x = Session["UserId"].ToString();
                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                throw;
            }
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
            if (Session["UserID"] != null && Session["UserRoleId"].ToString() == "1")
            {
                ViewBag.Message = "Your contact page.";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /////////////// BILL SUBMISSION Start//////////////////////       

        public ActionResult ajax_BillSubmissionForm()
        {
            return PartialView("~/Views/Home/_partialBillSubmissionView.cshtml");
        }

        [HttpPost]
        public int ajax_confirm_BillSubmissionForm(string CompanyCategoryName, string CompanyName, int DepartmentId, string Pan, string Gst, int FundId, string WorkDesc, string Amount, int BillTypeId, string Mobile)
        {
            int response;
            response = bill.SubmitBill(CompanyCategoryName, CompanyName, DepartmentId, Pan, Gst, FundId, WorkDesc, Amount, BillTypeId, Mobile);
            return response;
            //return PartialView("~/Views/Home/_partialBillSubmission_view.cshtml");
        }

        [HttpPost]
        public int ajax_Delete_Bill(string id)
        {
            int response;
            response = bill.DeleteBill(id);
            return response;
            //return PartialView("~/Views/Home/_partialBillSubmission_view.cshtml");
        }

        [HttpGet]
        public JsonResult ajax_GetBillDetailsForUpdate(string id)
        {
            BillSubmission obj = new BillSubmission();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    obj = bill.GetBillDetailsById(Convert.ToInt16(id));
                }
            }
            catch (Exception ex)
            {

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
            //return PartialView("~/Views/ContentManagement/_partialBillSubmissionForm.cshtml", obj);
        }


        [HttpPost]
        public int ajax_Update_BillSubmissionForm(string BillId, string CompanyCategoryName, string CompanyName, int DepartmentId, string Pan, string Gst, int FundId, string WorkDesc, string Amount, int BillTypeId)
        {
            int response;
            response = bill.UpdateBill(BillId, CompanyCategoryName, CompanyName, DepartmentId, Pan, Gst, FundId, WorkDesc, Amount, BillTypeId);
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

        public string GetAllBillType()
        {
            string result = bill.FetchAllBillType();
            return result;
        }

        public ActionResult BillSubmissionForm()
        {
            List<BillSubmission> temp = new List<BillSubmission>();
            temp = bill.FetchAllBillSubmission();
            return View(temp);
        }

        public string GetDeptById(int deptId)
        {
            string result = bill.GetDeptById(deptId);
            return result;
        }

        public string GetFundById(int fundId)
        {
            string result = bill.GetFundById(fundId);
            return result;
        }

        public string GetBillTypeById(int billTypeId)
        {
            string result = bill.GetBillTypeById(billTypeId);
            return result;
        }

        /////////////// BILL ALLOTEMENT End//////////////////////


        /////////////// BILL ALLOTEMENT Start//////////////////////
        public ActionResult BillAllotment()
        {
            List<BillAllotementModel> temp = new List<BillAllotementModel>();
            temp = billAllote.FetchAllBillAllotementDetails();
            return View(temp);
        }

        public string GetAllDocketNoByDept(int deptId)
        {
            string result = billAllote.FetchAllDocketNoByDept(deptId);
            return result;
        }

        public string GetAllOfficer(int deptId)
        {
            string result = billAllote.FetchAllOfficer(deptId);
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

        [HttpGet]
        public ActionResult ajax_check_billing_status(string docketNo, string entryDate)
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm = billStatus.FetchBillingBySearch(docketNo, entryDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PartialView("~/Views/Home/_partialBillingStatusDataTable.cshtml", mm);

            //List<BillingStatusModel> temp = new List<BillingStatusModel>();
            //temp = billStatus.check_billing_status(docketNo, entryDate);
            //return PartialView("~/Views/Home/_partialBillingStatusDataTable.cshtml", temp);

            //return Json(billStatus.check_billing_status(docketNo, entryDate), JsonRequestBehavior.AllowGet);
        }

        /////////////// END YOUR BILLING STATUS Start //////////////////////


        /////////////// REJECTED BILL STATUS Start //////////////////////
        public ActionResult RejectedBill()
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm = rejBill.Fetch_All_Rejected_Bill_Details();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(mm);
        }

        [HttpPost]
        public int ajax_Reissue_voucher(string DocketNo)
        {
            int response;
            response = rejBill.Reissue_voucher(DocketNo);

            return response;

        }
        /////////////// REJECTED BILL STATUS End //////////////////////


        /////////////// OFFICER ENTRY Start //////////////////////
        public ActionResult OfficerEntry()
        {
            MultipleModel mm = new MultipleModel();
            //List<OfficerModel> temp = new List<OfficerModel>();
            try
            {
                mm = officer.FetchAllOfficerList();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return View(mm);
        }

        public ActionResult ajax_OfficerEntryForm()
        {
            return PartialView("~/Views/Home/_partialOfficerEntryView.cshtml");
        }

        [HttpPost]
        public int ajax_confirm_OfficerEntryForm(string officerName, string pan, string mobile, string gpf, int DeptId, string pass, int userTypeId)
        {
            int response;
            response = officer.SubmitOfficer(officerName, pan, mobile, gpf, DeptId, pass, userTypeId);
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
        public int ajax_confirm_SchemeEntryForm(string schemeNo, string schemeName)
        {
            int response;
            response = scheme.SubmitSchemeDetails(schemeNo, schemeName);
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

        public string GetAllSchemeName()
        {
            string result = trsy.FetchAllSchemeName();
            return result;
        }

        public ActionResult ajax_TreasuryEntryForm()
        {
            return PartialView("~/Views/Home/_partialTreasuryEntryView.cshtml");
        }

        [HttpPost]
        public int ajax_confirm_TreassuryEntryForm(int schemeId, string adviceNo, string adviceDate)
        {
            int response;
            response = trsy.SubmitTreasuryDetails(schemeId, adviceNo, adviceDate);
            return response;
        }
        /////////////// Treasury Master ENTRY End //////////////////////
        ///
        /////////////// FUND Master ENTRY Start //////////////////////
        public ActionResult MstFundDetails()
        {
            List<FundModel> temp = new List<FundModel>();
            temp = fund.FetchAllFundlist();
            return View(temp);
        }

        [HttpPost]
        public int ajax_confirm_FundEntryForm(string FundSchemeName)
        {
            int response;
            response = fund.SubmitFundDetails(FundSchemeName);
            return response;
        }
        /////////////// FUND Master ENTRY End //////////////////////
        
        /////////////// BILL Master ENTRY Start //////////////////////
        public ActionResult MstBillDetails()
        {
            List<BillModel> temp = new List<BillModel>();
            temp = Billmst.FetchAllBillist();
            return View(temp);
        }

        [HttpPost]
        public int ajax_confirm_BillEntryForm(string TypeOfBillName)
        {
            int response;
            response = Billmst.SubmitBillDetails(TypeOfBillName);
            return response;
        }
        /////////////// BILL Master ENTRY End //////////////////////
    }
}