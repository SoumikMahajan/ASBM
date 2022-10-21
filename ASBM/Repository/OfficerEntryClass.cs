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
    public class OfficerEntryClass
    {
        string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int SubmitOfficer(string officerName, string pan, string mobile, string gpf, int DepartmentId, string pass, int userTypeId)
        {
            int res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spAccountsOfficerInsert]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@officer_name", officerName);
                    cmd.Parameters.AddWithValue("@officer_pan", pan);
                    cmd.Parameters.AddWithValue("@officer_mobile", mobile);
                    cmd.Parameters.AddWithValue("@officer_gpf", gpf);
                    cmd.Parameters.AddWithValue("@dept_id", DepartmentId);
                    cmd.Parameters.AddWithValue("@pass", pass);
                    cmd.Parameters.AddWithValue("@user_type_id", userTypeId);

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

        public List<OfficerModel> FetchAllOfficerList()
        {
            List<OfficerModel> List = new List<OfficerModel>();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"select officer.officer_user_id_pk, officer.officer_name, officer.officer_mobile, "
                                    + "officer.officer_pan, officer.officer_gpf, dept.department_name, usertype.user_type_name " +
                                    "from [dbo].[tbl_accounts_officer_master] AS officer " +
                                    "LEFT JOIN [dbo].[tbl_accounts_department_master] AS dept ON dept.department_id_pk = officer.officer_dept_id_fk "+
                                    "LEFT JOIN [dbo].[tbl_accounts_user_login] AS login ON login.user_details_id_fk = officer.officer_user_id_pk " +
                                    "LEFT JOIN [dbo].[tbl_accounts_user_type_master] AS usertype ON usertype.user_type_id_pk = login.user_type_id_fk";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        List = JsonConvert.DeserializeObject<List<OfficerModel>>(JsonConvert.SerializeObject(dt));
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