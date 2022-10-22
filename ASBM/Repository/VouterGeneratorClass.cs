using System;
using System.Collections.Generic;
using System.Configuration;
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
                        Query = "select bill_details_id_pk as id, bill_docket_no as docket_no from tbl_accounts_bill_details order by bill_docket_no";
                    }
                    else if (radioVal == 2)
                    {
                        Query = "select random_bill_id_pk as id, random_bill_docket_no as docket_no from tbl_accounts_random_bill_generation_details order by random_bill_docket_no";
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
    }
}