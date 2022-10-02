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

        public int SubmitBill(string companyName, string propriterName, string CompanyCategoryName, int DepartmentId, string Pan, string Gst, int FundId, string WorkDesc, string Amount)
        {
            int res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = string.Empty;

                    Query = @"INSERT INTO tbl_accounts_bill_details (bill_company_name, bill_proprietor_name, bill_category_id_fk, bill_department_id_fk, bill_pan, bill_gst, bill_fund_id_fk, bill_description, bill_amount) VALUES(@companuName, @propName, @category_id, @dept_id, @pan, @gst, @fund_id, @work_desc, @bill_amount)";

                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@companuName", companyName);
                    cmd.Parameters.AddWithValue("@propName", propriterName);
                    cmd.Parameters.AddWithValue("@category_id", CompanyCategoryName);
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
                    string Query = "select fund_id_pk, fund_scheme_name from tbl_accounts_fund_master order by fund_scheme_name";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    result += "<option value = " + 0 + " selected > -- Select -- </ option >";
                    while (dr.Read())
                    {
                        result += "<option value='" + "" + Convert.ToString(dr["fund_id_pk"]) + "'>" + Convert.ToString(dr["fund_scheme_name"]) + "</option>";
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
                    string Query = @"SELECT	bill.bill_details_id_pk, bill.bill_category_id_fk, bill.bill_company_name, bill.bill_proprietor_name, bill.bill_type_id_fk, "+  
                                    "dept.department_name, bill.bill_pan, bill.bill_gst, fund.fund_scheme_name, bill.bill_description, bill.bill_amount, bill.bill_CreateDate " +
                                    "FROM tbl_accounts_bill_details AS bill" +
                                    "LEFT JOIN tbl_accounts_department_master as dept ON dept.department_id_pk = bill.bill_department_id_fk " +
                                    "LEFT JOIN tbl_accounts_fund_master as fund ON fund.fund_id_pk = bill.bill_fund_id_fk"
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
    }
}