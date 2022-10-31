using ASBM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ASBM.Repository
{
    public class FinanceClass
    {
        string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public MultipleModel Fetch_All_Vouter_Details()
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm.financeList = Get_All_Vouter_Data();
            }
            catch (Exception ex)
            {

            }
            return mm;
        }

        public List<FinanceModel> Get_All_Vouter_Data()
        {
            List<FinanceModel> Res = new List<FinanceModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"SELECT
	                                    pay.payment_voucher_no 
                                    FROM
	                                    tbl_accounts_payment_details AS pay
                                    WHERE approve_status = 0
	                                ";
                    SqlCommand cmd = new SqlCommand(Query, con);

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Res = JsonConvert.DeserializeObject<List<FinanceModel>>(JsonConvert.SerializeObject(dt));
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

        public MultipleModel Fetch_Vouter_Details(string voucherNo)
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm.finance = Get_Vouter_Data(voucherNo);
            }
            catch (Exception ex)
            {

            }
            return mm;
        }

        public FinanceModel Get_Vouter_Data(string voucherNo)
        {
            FinanceModel Res = new FinanceModel();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"SELECT
	                                    pay.payment_voucher_no,
	                                    pay.payment_type_id,
                                        pay.bank_id_fk,
	                                    bank.bank_name,
	                                    pay.bank_account_no,
	                                    fund.fund_scheme_name,
	                                    scheme.scheme_name,
	                                    pay.treasury_advice_no,
	                                    pay.treasury_advice_date,
                                        pay.total_net_amount,
                                        scheme.scheme_id_pk
                                    FROM
	                                    tbl_accounts_payment_details AS pay
	                                    LEFT JOIN tbl_accounts_bank_master AS bank ON bank.bank_id_pk = pay.bank_id_fk 
	                                    LEFT JOIN tbl_accounts_fund_master as fund ON fund.fund_scheme_id_pk = pay.fund_scheme_id_fk
	                                    LEFT JOIN tbl_accounts_scheme_master as scheme ON scheme.scheme_id_pk = pay.treasury_scheme_id_fk
                                    WHERE
                                        pay.payment_voucher_no = @vouterNo
	                                ";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@vouterNo", voucherNo);
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Res = JsonConvert.DeserializeObject<List<FinanceModel>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
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

        //public string FetchBankDetails(int bankId)
        //{
        //    string result = null;
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(strcon))
        //        {
        //            string Query = "select bank_id_pk, bank_name from tbl_accounts_bank_master where bank_id_pk = @bankId";
        //            SqlCommand cmd = new SqlCommand(Query, con);
        //            cmd.Parameters.AddWithValue("@bankId", bankId);
        //            con.Open();

        //            SqlDataReader dr = cmd.ExecuteReader();

        //            result += "<option value = " + 0 + "> -- Select -- </ option >";
        //            while (dr.Read())
        //            {
        //                //if (bankId == Convert.ToInt32(dr["bank_id_pk"]))
        //                //{
        //                    result += "<option value='" + "" + Convert.ToString(dr["bank_id_pk"]) + "'selected>" + Convert.ToString(dr["bank_name"]) + "</option>";
        //                //}
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    return result;
        //}

        public MultipleModel Fetch_bank_acc_Details(int bankId)
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm.finance = Get_Bank_Acc_Data(bankId);
            }
            catch (Exception)
            {

            }
            return mm;
        }

        public FinanceModel Get_Bank_Acc_Data(int bankId)
        {
            FinanceModel Res = new FinanceModel();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"SELECT
                                            bank.bank_name,
	                                        bank.bank_id_pk,
	                                        bank.bank_account_no,
	                                        fund.fund_scheme_name,
                                            fund.fund_scheme_id_pk
	                                    FROM
		                                    tbl_accounts_bank_master AS bank		                                    
		                                    LEFT JOIN tbl_accounts_fund_master AS fund ON fund.fund_scheme_id_pk = bank.bank_fund_id_fk
	                                    WHERE
		                                    bank.bank_id_pk = @id
	                                ";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@id", bankId);
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Res = JsonConvert.DeserializeObject<List<FinanceModel>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
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

        public MultipleModel Fetch_Treasury_Details(int SchemeId)
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm.finance = Get_Treasury_Data(SchemeId);
            }
            catch (Exception)
            {

            }
            return mm;
        }

        public FinanceModel Get_Treasury_Data(int SchemeId)
        {
            FinanceModel Res = new FinanceModel();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"SELECT
	                                        scheme.scheme_id_pk, treasury.treasury_advice_no, scheme.scheme_name, 
	                                        CONVERT(VARCHAR(10), treasury.treasury_advice_date, 105) as treasury_advice_date                                     
	                                    FROM
		                                    tbl_accounts_treasury_master AS treasury		                                    
		                                    LEFT JOIN tbl_accounts_scheme_master AS scheme ON scheme.scheme_id_pk = treasury.scheme_id_fk
	                                    WHERE
		                                    scheme.scheme_id_pk = @id
	                                ";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@id", SchemeId);
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Res = JsonConvert.DeserializeObject<List<FinanceModel>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
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

        public int Finilize_Payment(string PaymentMode, string MemoNo, float NetAmount, string VoucherNo)
        {
            int res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = string.Empty;
                    string Query2 = string.Empty;
                    Query = @"INSERT INTO tbl_accounts_finance (payment_mode, memo_no, approved_amount, finance_vouter_no, approve_status) VALUES(@PaymentMode, @MemoNo, @NetAmount, @VouterNo, @activeStatus)";
                    Query2 = @"UPDATE tbl_accounts_payment_details SET approve_status = 1 WHERE payment_voucher_no = @VouterNo";

                    SqlCommand cmd = new SqlCommand(Query, con);
                    SqlCommand cmd2 = new SqlCommand(Query2, con);
                    cmd.Parameters.AddWithValue("@PaymentMode", PaymentMode);
                    cmd.Parameters.AddWithValue("@MemoNo", MemoNo);
                    cmd.Parameters.AddWithValue("@NetAmount", NetAmount);
                    cmd.Parameters.AddWithValue("@VouterNo", VoucherNo);
                    cmd.Parameters.AddWithValue("@activeStatus", 1);

                    cmd2.Parameters.AddWithValue("@VouterNo", VoucherNo);
                    con.Open();
                    int InsertedRows = cmd.ExecuteNonQuery();
                    int AffectedRows = cmd2.ExecuteNonQuery();

                    if (InsertedRows == 1 && AffectedRows == 1)
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
    }
}