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
    public class AccountantClass
    {
        string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public MultipleModel Fetch_All_Vouter_Details()
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm.AccountantList = Get_All_Vouter_Data();
            }
            catch (Exception)
            {

            }
            return mm;
        }

        public List<AccountantModel> Get_All_Vouter_Data()
        {
            List<AccountantModel> Res = new List<AccountantModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"SELECT
	                                    vouter.voucher_id_pk,
	                                    vouter.voucher_no,
                                        CONVERT(VARCHAR(10), vouter.CreateDate, 105) as CreateDate
                                    FROM
	                                    tbl_accounts_voucher AS vouter
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
                        Res = JsonConvert.DeserializeObject<List<AccountantModel>>(JsonConvert.SerializeObject(dt));
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

        public MultipleModel Fetch_Vouter_Details(int id)
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm.accountantModel = Get_Vouter_Data(id);
            }
            catch (Exception)
            {

            }
            return mm;
        }

        public AccountantModel Get_Vouter_Data(int id)
        {
            AccountantModel Res = new AccountantModel();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"(  SELECT
	                                        vouter.voucher_no,
	                                        bill.bill_category_id_fk,
	                                        bill.bill_company_name,
	                                        dept.department_name,
	                                        payee.payee_name,
	                                        fund.fund_scheme_name,
	                                        bill.bill_gst,
	                                        bill.bill_description,
	                                        bill.bill_amount,
                                            CONVERT(VARCHAR(10), vouter.CreateDate, 105) as CreateDate,
	                                        NULL as mobile_no
	                                    FROM
		                                    tbl_accounts_voucher AS vouter
		                                    JOIN tbl_accounts_bill_details AS bill ON bill.bill_docket_no = vouter.bill_docket_no
		                                    LEFT JOIN tbl_accounts_department_master AS dept ON dept.department_id_pk = bill.bill_department_id_fk
		                                    LEFT JOIN tbl_accounts_payee_master AS payee ON payee.payee_department_id_fk = dept.department_id_pk
		                                    LEFT JOIN tbl_accounts_fund_master AS fund ON fund.fund_scheme_id_pk= bill.bill_fund_id_fk 
	                                    WHERE
		                                    vouter.voucher_id_pk = @id
	                                ) 
                                    UNION
	                                (
	                                    SELECT
		                                    vouter.voucher_no,
		                                    NULL AS bill_category_id_fk,
		                                    NULL AS bill_company_name,
		                                    dept.department_name,
		                                    payee.payee_name,
		                                    fund.fund_scheme_name,
		                                    NULL AS bill_gst,
		                                    rand.random_bill_work_desc,
		                                    NULL AS bill_amount,
                                            CONVERT(VARCHAR(10), vouter.CreateDate, 105) as CreateDate,
                                            rand.random_bill_mobile_no as mobile_no
	                                    FROM
		                                    tbl_accounts_voucher AS vouter
		                                    JOIN tbl_accounts_random_bill_generation_details AS rand ON rand.random_bill_docket_no = vouter.bill_docket_no
		                                    LEFT JOIN tbl_department_master AS dept ON dept.department_id_pk = rand.random_bill_dept_id_fk
		                                    LEFT JOIN tbl_accounts_payee_master AS payee ON payee.payee_department_id_fk = dept.department_id_pk
		                                    LEFT JOIN tbl_accounts_fund_master AS fund ON fund.fund_scheme_id_pk= rand.random_bill_fund_id_fk 
	                                    WHERE
	                                    vouter.voucher_id_pk = @id
	                                )
	                                ";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Res = JsonConvert.DeserializeObject<List<AccountantModel>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
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

        public string FetchAllBank()
        {
            string result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = "select bank_id_pk, bank_name from tbl_accounts_bank_master order by bank_name";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    result += "<option value = " + 0 + " selected > -- Select -- </ option >";
                    while (dr.Read())
                    {
                        result += "<option value='" + "" + Convert.ToString(dr["bank_id_pk"]) + "'>" + Convert.ToString(dr["bank_name"]) + "</option>";
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public MultipleModel Fetch_bank_acc_Details(int bankId)
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm.accountantModel = Get_Bank_Acc_Data(bankId);
            }
            catch (Exception)
            {

            }
            return mm;
        }

        public AccountantModel Get_Bank_Acc_Data(int bankId)
        {
            AccountantModel Res = new AccountantModel();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"SELECT
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
                        Res = JsonConvert.DeserializeObject<List<AccountantModel>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
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

        public MultipleModel Fetch_Treasury_Details(int SchemeId)
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm.accountantModel = Get_Treasury_Data(SchemeId);
            }
            catch (Exception)
            {

            }
            return mm;
        }

        public AccountantModel Get_Treasury_Data(int SchemeId)
        {
            AccountantModel Res = new AccountantModel();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"SELECT
	                                        treasury.treasury_advice_no,
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
                        Res = JsonConvert.DeserializeObject<List<AccountantModel>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
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

        public int Finilize_Payment(string VoucherNo, float BasicBill, float SgstVal, float CgstVal, float Igst, float BasicCess, float GrossAmount, float ItTds, float SdMoney, float GrossCess, float TdsCgst, float TdsSgst, float Pf, float PfAdvance, float Ptax, float CcsCount, float CcsLic, float CcsLoan, float Coop, float Gi, float Lic, float Festival, float TotalDeduction, float NetAmountBill, int PaymentTypeId, int BankId, string BankAccNo, int FundSchemeId, int TreasurySchemeId, string TreasuryAdviceNo, string TreasuryAdviceDate)
        {
            int res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spAccountsPaymentSubmission]", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@OPERATION_ID", 1);
                    cmd.Parameters.AddWithValue("@VoucherNo", VoucherNo);
                    cmd.Parameters.AddWithValue("@BasicBill", BasicBill);
                    cmd.Parameters.AddWithValue("@SgstVal", SgstVal);
                    cmd.Parameters.AddWithValue("@CgstVal", CgstVal);
                    cmd.Parameters.AddWithValue("@Igst", Igst);
                    cmd.Parameters.AddWithValue("@BasicCess", BasicCess);
                    cmd.Parameters.AddWithValue("@GrossAmount", GrossAmount);
                    cmd.Parameters.AddWithValue("@ItTds", ItTds);
                    cmd.Parameters.AddWithValue("@SdMoney", SdMoney);
                    cmd.Parameters.AddWithValue("@GrossCess", GrossCess);
                    cmd.Parameters.AddWithValue("@TdsCgst", TdsCgst);
                    cmd.Parameters.AddWithValue("@TdsSgst", TdsSgst);
                    cmd.Parameters.AddWithValue("@Pf", Pf);
                    cmd.Parameters.AddWithValue("@PfAdvance", PfAdvance);
                    cmd.Parameters.AddWithValue("@Ptax", Ptax);
                    cmd.Parameters.AddWithValue("@CcsCount", CcsCount);
                    cmd.Parameters.AddWithValue("@CcsLic", CcsLic);
                    cmd.Parameters.AddWithValue("@CcsLoan", CcsLoan);
                    cmd.Parameters.AddWithValue("@Coop", Coop);
                    cmd.Parameters.AddWithValue("@Gi", Gi);
                    cmd.Parameters.AddWithValue("@Lic", Lic);
                    cmd.Parameters.AddWithValue("@Festival", Festival);
                    cmd.Parameters.AddWithValue("@TotalDeduction", TotalDeduction);
                    cmd.Parameters.AddWithValue("@NetAmountBill", NetAmountBill);
                    cmd.Parameters.AddWithValue("@PaymentTypeId", PaymentTypeId);
                    cmd.Parameters.AddWithValue("@BankId", BankId);
                    cmd.Parameters.AddWithValue("@BankAccNo", BankAccNo);
                    cmd.Parameters.AddWithValue("@FundSchemeId", FundSchemeId);
                    cmd.Parameters.AddWithValue("@TreasurySchemeId", TreasurySchemeId);
                    cmd.Parameters.AddWithValue("@TreasuryAdviceNo", TreasuryAdviceNo);
                    cmd.Parameters.AddWithValue("@TreasuryAdviceDate", TreasuryAdviceDate);

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
    }
}