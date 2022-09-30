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
    public class BankEntryClass
    {
        string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int SubmitBankDetails(string accNo, string accName, int fundId, string bankName, string ifsc)
        {
            int res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = string.Empty;

                    Query = @"INSERT INTO tbl_accounts_bank_master (bank_account_no, bank_account_name, bank_fund_id_fk, bank_name, bank_ifsc) VALUES(@accno, @accName, @funId, @bankname, @ifsc)";

                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@accno", accNo);
                    cmd.Parameters.AddWithValue("@accName", accName);
                    cmd.Parameters.AddWithValue("@funId", fundId);
                    cmd.Parameters.AddWithValue("@bankname", bankName);
                    cmd.Parameters.AddWithValue("@ifsc", ifsc);

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

        public List<BankModel> FetchAllBankList()
        {
            List<BankModel> List = new List<BankModel>();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"select bank.bank_id_pk, bank.bank_account_no, bank.bank_account_name, bank.bank_name, bank.bank_ifsc, fund.fund_scheme_name from " +
                                    "tbl_accounts_bank_master AS bank " +
                                    "LEFT JOIN tbl_accounts_fund_master AS fund ON fund.fund_id_pk = bank.bank_fund_id_fk";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        List = JsonConvert.DeserializeObject<List<BankModel>>(JsonConvert.SerializeObject(dt));
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