using ASBM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ASBM.Repository
{
    public class VouterGeneratorClass
    {
        string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public string FetchAllDocketNo(int radioVal)
        {
            string result = null;
            string Query = null;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (radioVal == 1)
                    {
                        Query = "select bill_details_id_pk as id, bill_docket_no as docket_no from tbl_accounts_bill_details where approve_status = 0 AND bill_alloted_status = 1 order by bill_docket_no";
                    }
                    else if (radioVal == 2)
                    {
                        Query = "select random_bill_id_pk as id, random_bill_docket_no as docket_no from tbl_accounts_random_bill_generation_details where approve_status = 0 order by random_bill_docket_no";
                    }
                    SqlCommand cmd = new SqlCommand(Query, con);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    result += "<option value = " + 0 + " selected > -- Select -- </ option >";
                    while (dr.Read())
                    {
                        result += "<option value='" + "" + Convert.ToString(dr["id"]) + "'>" + Convert.ToString(dr["docket_no"]) + "</option>";
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public int ApprovedVoucher(string BillDocketType, string billdocketno, string iscic, string cicno, string isetender, string tenno, string ismed, string medno, string MeetingTenderCommittee, string MeetingChairman, string Tenderrate, string MeetingBOC, string AAFSNo, string WorkOrder, string WorkOrderDate, string AmountEstimate, string MBBookNo, string PageNo, string WorkRegisterNo, string WorkRegisterDate)
        {
            int res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = string.Empty;

                    //        Query = @"INSERT INTO tbl_accounts_voucher (bill_docket_no,bill_docket_type_id,IsCICnumber,CICNumber,IsParticipateTender,ParticipateTenderNumber,IsMed,MedDate,TenderCommitteeMeeting,ChairmanMeeting,TenderRate,BOCSanctionMeeting,AAFSNumber,WorkOrderNo,WorkOrderDate,EstimateAmount,MBBookNo,PageNo,WorkRegNo,WorkRegNoDate) 
                    //VALUES(@billdocketno,@BillDocketType,@iscic,@cicno,@isetender,@tenno,@ismed,@medno,@MeetingTenderCommittee,@MeetingChairman,@Tenderrate,
                    //@MeetingBOC,@AAFSNo,@WorkOrder,@WorkOrderDate,@AmountEstimate,@MBBookNo,@PageNo,@WorkRegisterNo,@WorkRegisterDate)";


                    SqlCommand cmd = new SqlCommand("[dbo].[spAccountsVoucher]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@OPERATION_ID", 1);
                    cmd.Parameters.AddWithValue("@billdocketno", billdocketno);
                    cmd.Parameters.AddWithValue("@BillDocketType", BillDocketType);
                    cmd.Parameters.AddWithValue("@iscic", iscic);
                    cmd.Parameters.AddWithValue("@cicno", cicno);
                    cmd.Parameters.AddWithValue("@isetender", isetender);
                    cmd.Parameters.AddWithValue("@tenno", tenno);
                    cmd.Parameters.AddWithValue("@ismed", ismed);
                    cmd.Parameters.AddWithValue("@medno", medno);
                    cmd.Parameters.AddWithValue("@MeetingTenderCommittee", MeetingTenderCommittee);
                    cmd.Parameters.AddWithValue("@MeetingChairman", MeetingChairman);
                    cmd.Parameters.AddWithValue("@Tenderrate", Tenderrate);
                    cmd.Parameters.AddWithValue("@MeetingBOC", MeetingBOC);
                    cmd.Parameters.AddWithValue("@AAFSNo", AAFSNo);
                    cmd.Parameters.AddWithValue("@WorkOrder", WorkOrder);
                    cmd.Parameters.AddWithValue("@WorkOrderDate", WorkOrderDate);
                    cmd.Parameters.AddWithValue("@AmountEstimate", AmountEstimate);
                    cmd.Parameters.AddWithValue("@MBBookNo", MBBookNo);
                    cmd.Parameters.AddWithValue("@PageNo", PageNo);
                    cmd.Parameters.AddWithValue("@WorkRegisterNo", WorkRegisterNo);
                    cmd.Parameters.AddWithValue("@WorkRegisterDate", WorkRegisterDate);

                    con.Open();
                    int AffectedRows = cmd.ExecuteNonQuery();
                    if (AffectedRows >= 1)
                    {
                        res = 1;
                    }
                    else
                    {
                        res = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                //res = "Failed|Internal error.";
            }

            return res;
        }


        public int RejectVoucher(int BillDocketType, string billdocketno)
        {
            int res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = string.Empty;

                    if (BillDocketType == 1)
                    {
                        Query = @"UPDATE tbl_accounts_bill_details SET approve_status = 2 WHERE bill_docket_no = @billdocketno";
                    }
                    else
                    {
                        Query = @"UPDATE tbl_accounts_random_bill_generation_details SET approve_status = 2 WHERE random_bill_docket_no = @billdocketno";
                    }
                    
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@billdocketno", billdocketno);
                    con.Open();
                    int AffectedRows = cmd.ExecuteNonQuery();
                    if (AffectedRows == 1)
                    {
                        res = 1;
                    }
                    else
                    {
                        res = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                //res = "Failed|Internal error.";
            }

            return res;
        }

        //public MultipleModel FetchNormalBillingdetails(string billdocketno)
        //{
        //    MultipleModel mm = new MultipleModel();
        //    try
        //    {
        //        mm.billSubmission = getNormalBillDeatils(billdocketno);
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return mm;
        //}

        public BillSubmission FetchNormalBillingdetails(string billdocketno)
        {
            BillSubmission Res = new BillSubmission();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"select T1.bill_details_id_pk,T1.bill_docket_no,T1.bill_category_id_fk,T1.bill_company_name,T1.bill_pan,T1.bill_gst,T1.bill_description,T1.bill_amount,T2.department_name,T3.fund_scheme_name from tbl_accounts_bill_details as T1
                        left join tbl_accounts_department_master as T2 on T2.department_id_pk=T1.bill_department_id_fk
                        left join tbl_accounts_fund_master as T3 on T3.fund_scheme_id_pk=T1.bill_fund_id_fk
                        where bill_docket_no=@DocketNo";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@DocketNo", billdocketno);

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    //adp.Fill(dt);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    List = JsonConvert.DeserializeObject<List<BillingStatusModel>>(JsonConvert.SerializeObject(dt));
                    //}

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Res = JsonConvert.DeserializeObject<List<BillSubmission>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
                    }

                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Res;
        }

        public RandomBillGenerationModel FetchRandomBillingdetails(string billdocketno)
        {
            RandomBillGenerationModel Res = new RandomBillGenerationModel();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"select T1.random_bill_name,T1.random_bill_work_desc,T1.random_bill_mobile_no,T2.department_name,T3.fund_scheme_name from tbl_accounts_random_bill_generation_details as T1
                      left join tbl_accounts_department_master as T2 on T2.department_id_pk=T1.random_bill_dept_id_fk
                      left join tbl_accounts_fund_master as T3 on T3.fund_scheme_id_pk=T1.random_bill_fund_id_fk
                      where random_bill_docket_no=@DocketNo";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@DocketNo", billdocketno);

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Res = JsonConvert.DeserializeObject<List<RandomBillGenerationModel>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
                    }

                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Res;
        }
    }
}