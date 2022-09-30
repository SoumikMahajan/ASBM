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
    public class PayeeEntryClass
    {
        string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int SubmitPayee(string payeeName, string pan, string mobile, string gst, string accno, int deptId)
        {
            int res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = string.Empty;

                    Query = @"INSERT INTO tbl_accounts_payee_master (payee_name, payee_pan, payee_mobile, payee_gst, payee_account_no, payee_department_id_fk) VALUES(@payee_name, @payee_pan, @payee_mobile, @payee_gst, @accno, @dept_id)";

                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@payee_name", payeeName);
                    cmd.Parameters.AddWithValue("@payee_pan", pan);
                    cmd.Parameters.AddWithValue("@payee_mobile", mobile);
                    cmd.Parameters.AddWithValue("@payee_gst", gst);
                    cmd.Parameters.AddWithValue("@dept_id", deptId);
                    cmd.Parameters.AddWithValue("@accno", accno);

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

        public List<PayeeModel> FetchAllPayeeList()
        {
            List<PayeeModel> List = new List<PayeeModel>();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"select payee.payee_id_pk, payee.payee_name, payee.payee_pan, payee.payee_mobile, payee.payee_gst, payee.payee_account_no, dept.department_name from "+
                                    "tbl_accounts_payee_master AS payee " +
                                    "LEFT JOIN tbl_accounts_department_master AS dept ON dept.department_id_pk = payee.payee_department_id_fk";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        List = JsonConvert.DeserializeObject<List<PayeeModel>>(JsonConvert.SerializeObject(dt));
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