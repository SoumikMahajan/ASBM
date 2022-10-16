using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using ASBM.Models;
using System.Web.Mvc;
using System.Configuration;

namespace ASBM.Repository
{
    public class LoginClass
    {
        string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public LoginModel Loginvalidate(string UserName, string userPass)
        {
            //ResultInfo<LoginModel> LogInResult = new ResultInfo<AdminLogIn>();
            LoginModel loginModel = new LoginModel();
            string res = "";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"SELECT	login.user_login_id, type.user_type_name, type.user_type_id_pk FROM
                                    tbl_accounts_user_login AS login 
                                    LEFT JOIN tbl_accounts_user_type_master AS type ON type.user_type_id_pk = login.user_type_id_fk 
                                    where login.user_login_id = @username AND login.user_login_password = @Pass AND login.active_status = @IsActive";

                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@username", UserName);
                    cmd.Parameters.AddWithValue("@Pass", userPass);
                    cmd.Parameters.AddWithValue("@IsActive", 1);

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            loginModel.userId = (string)dr[0];
                            loginModel.userRole = (string)dr[1];
                            loginModel.userRoleId = Convert.ToInt32(dr[2]);

                            //res = JsonConvert.SerializeObject(dt);
                            //LogInResult.Info = JsonConvert.DeserializeObject<List<AdminLogIn>>(res)[0];
                            //LogInResult.IsSuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return loginModel;
        }
    }
}