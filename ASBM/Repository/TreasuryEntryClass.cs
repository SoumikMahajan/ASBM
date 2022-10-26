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
    public class TreasuryEntryClass
    {
        string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int SubmitTreasuryDetails(int schemeId, string adviceNo, string adviceDate)
        {
            int res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = string.Empty;

                    Query = @"INSERT INTO tbl_accounts_treasury_master (treasury_advice_no, treasury_advice_date, scheme_id_fk) VALUES(@admiveNo, @adviceDate, @schemeId)";

                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@admiveNo", adviceNo);
                    cmd.Parameters.AddWithValue("@adviceDate", adviceDate);
                    cmd.Parameters.AddWithValue("@schemeId", schemeId);

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

        public List<TreasuryModel> FetchAllTreasulyList()
        {
            List<TreasuryModel> List = new List<TreasuryModel>();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"SELECT tsry.treasury_id_pk, tsry.treasury_advice_no, tsry.treasury_advice_date, scheme.scheme_name
                                            FROM tbl_accounts_treasury_master AS tsry
                                            LEFT JOIN tbl_accounts_scheme_master AS scheme ON scheme.scheme_id_pk = tsry.scheme_id_fk
                                            ";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        List = JsonConvert.DeserializeObject<List<TreasuryModel>>(JsonConvert.SerializeObject(dt));
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

        public string FetchAllSchemeName()
        {
            string result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = "select scheme_id_pk, scheme_name from tbl_accounts_scheme_master order by scheme_name";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    result += "<option value = " + 0 + " selected > -- Select -- </ option >";
                    while (dr.Read())
                    {
                        result += "<option value='" + "" + Convert.ToString(dr["scheme_id_pk"]) + "'>" + Convert.ToString(dr["scheme_name"]) + "</option>";
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