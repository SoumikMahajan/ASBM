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
    public class BillAllotementClass
    {
        string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public int SubmitBillAllotement(string DocketNo, int DepartmentId, int OfficerId, DateTime AllotedDatte)
        {
            int res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = string.Empty;

                    Query = @"INSERT INTO tbl_accounts_bill_allotement_details (bill_allotement_docket_no, bill_allotement_dept_id_fk, bill_allotement_officer_id_fk, bill_allotement_date) VALUES(@docketno, @deptid, @officerid, @alloteddate)";

                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@docketno", DocketNo);
                    cmd.Parameters.AddWithValue("@deptid", DepartmentId);
                    cmd.Parameters.AddWithValue("@officerid", OfficerId);
                    cmd.Parameters.AddWithValue("@alloteddate", AllotedDatte);

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

        public string FetchAllOfficer()
        {
            string result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = "select officer_user_id_pk, officer_name from tbl_accounts_officer_master order by officer_name";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    result += "<option value = " + 0 + " selected > -- Select -- </ option >";
                    while (dr.Read())
                    {
                        result += "<option value='" + "" + Convert.ToString(dr["officer_user_id_pk"]) + "'>" + Convert.ToString(dr["officer_name"]) + "</option>";
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public List<BillAllotementModel> FetchAllBillAllotementDetails()
        {
            List<BillAllotementModel> List = new List<BillAllotementModel>();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"SELECT
	                                    bill_allot.bill_allotement_id_pk,
	                                    bill_allot.bill_allotement_docket_no,
	                                    dept.department_name,
	                                    officer.officer_name,
	                                    bill_allot.bill_allotement_date 
                                    FROM
	                                    tbl_accounts_bill_allotement_details AS bill_allot
	                                LEFT JOIN tbl_accounts_department_master AS dept ON dept.department_id_pk = bill_allot.bill_allotement_dept_id_fk
	                                LEFT JOIN tbl_accounts_officer_master AS officer ON officer.officer_user_id_pk = bill_allot.bill_allotement_officer_id_fk";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        List = JsonConvert.DeserializeObject<List<BillAllotementModel>>(JsonConvert.SerializeObject(dt));
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