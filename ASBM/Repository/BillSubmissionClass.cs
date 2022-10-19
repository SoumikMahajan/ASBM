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
    public class BillSubmissionClass
    {
        string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public int SubmitBill(string CompanyCategoryName, string companyName, int DepartmentId, string Pan, string Gst, int FundId, string WorkDesc, string Amount)
        {
            int res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    //string Query = string.Empty;

                    //Query = @"INSERT INTO tbl_accounts_bill_details (bill_category_id_fk, bill_company_name, bill_department_id_fk, bill_pan, bill_gst, bill_fund_id_fk, bill_description, bill_amount) VALUES(@category_id, @companyName,@dept_id, @pan, @gst, @fund_id, @work_desc, @bill_amount)";


                    SqlCommand cmd = new SqlCommand("[dbo].[spBillSubmission]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OPERATION_ID", 1);
                    cmd.Parameters.AddWithValue("@category_id", CompanyCategoryName);
                    cmd.Parameters.AddWithValue("@companyName", companyName);
                    cmd.Parameters.AddWithValue("@dept_id", DepartmentId);
                    cmd.Parameters.AddWithValue("@pan", Pan);
                    cmd.Parameters.AddWithValue("@gst", Gst);
                    cmd.Parameters.AddWithValue("@fund_id", FundId);
                    cmd.Parameters.AddWithValue("@work_desc", WorkDesc);
                    cmd.Parameters.AddWithValue("@bill_amount", Amount);

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

        public int DeleteBill(string id)
        {
            int res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = string.Empty;

                    Query = @"Delete from tbl_accounts_bill_details where bill_details_id_pk = @id";

                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@id", id);

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

        public BillSubmission GetBillDetailsById(int id)
        {
            BillSubmission obj = new BillSubmission();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"select bill_details_id_pk, bill_category_id_fk, bill_company_name, bill_department_id_fk, bill_pan, 
                                    bill_gst, bill_fund_id_fk, bill_description, bill_amount from [dbo].[tbl_accounts_bill_details] WHERE bill_details_id_pk =@ID";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        obj = JsonConvert.DeserializeObject<List<BillSubmission>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
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
            return obj;
        }

        public string FetchAllDept()
        {
            string result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = "select department_id_pk, department_name from tbl_accounts_department_master order by department_name";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    result += "<option value = " + 0 + " selected > -- Select -- </ option >";
                    while (dr.Read())
                    {
                        result += "<option value='" + "" + Convert.ToString(dr["department_id_pk"]) + "'>" + Convert.ToString(dr["department_name"]) + "</option>";
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public string FetchAllFund()
        {
            string result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = "select fund_scheme_id_pk, fund_scheme_name from tbl_accounts_fund_master order by fund_scheme_name";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    result += "<option value = " + 0 + " selected > -- Select -- </ option >";
                    while (dr.Read())
                    {
                        result += "<option value='" + "" + Convert.ToString(dr["fund_scheme_id_pk"]) + "'>" + Convert.ToString(dr["fund_scheme_name"]) + "</option>";
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public List<BillSubmission> FetchAllBillSubmission()
        {
            List<BillSubmission> List = new List<BillSubmission>();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"SELECT bill.bill_details_id_pk,bill.bill_docket_no, bill.bill_category_id_fk, bill.bill_company_name,bill.bill_pan,                            bill.bill_gst, " +
                                    "dept.department_name, fund.fund_scheme_name " +
                                    "FROM tbl_accounts_bill_details AS bill " +
                                    "LEFT JOIN tbl_accounts_department_master as dept ON dept.department_id_pk = bill.bill_department_id_fk " +
                                    "LEFT JOIN tbl_accounts_fund_master as fund ON fund.fund_scheme_id_pk = bill.bill_fund_id_fk"
                                    ;
                    SqlCommand cmd = new SqlCommand(Query, con);
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        List = JsonConvert.DeserializeObject<List<BillSubmission>>(JsonConvert.SerializeObject(dt));
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
            return List;
        }

        public string GetDeptById(int id)
        {
            string result = "";
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query2 = "select department_id_pk,department_name from [dbo].[tbl_accounts_department_master] order by department_name";
                    SqlCommand cmd2 = new SqlCommand(Query2, con);
                    con.Open();
                    SqlDataReader dr2 = cmd2.ExecuteReader();

                    result += "<option value = " + 0 + "> --Select-- </ option >";
                    while (dr2.Read())
                    {
                        if (id == Convert.ToInt32(dr2["department_id_pk"]))
                        {
                            result += "<option value='" + "" + Convert.ToString(dr2["department_id_pk"]) + "'selected>" + Convert.ToString(dr2["department_name"]) + "</option>";
                        }
                        else
                        {
                            result += "<option value='" + "" + Convert.ToString(dr2["department_id_pk"]) + "'>" + Convert.ToString(dr2["department_name"]) + "</option>";
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
    }
}