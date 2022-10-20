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
    public class RandomBillGeneratorClass
    {
        string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public int SubmitRandomBill(string Name, int BillTypeId, int DepartmentId, int FundId, string WorkDesc, string Mobile)
        {
            int res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = string.Empty;

                    Query = @"INSERT INTO tbl_accounts_random_bill_generation_details (random_bill_name, random_bill_dept_id_fk, random_bill_fund_id_fk, random_bill_work_desc, random_bill_mobile_no, random_bill_type_id_fk) VALUES(@billName, @deptid, @fundid, @workDesc, @mobile, @billType)";

                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@billName", Name);
                    cmd.Parameters.AddWithValue("@deptid", DepartmentId);
                    cmd.Parameters.AddWithValue("@fundid", FundId);
                    cmd.Parameters.AddWithValue("@workDesc", WorkDesc);
                    cmd.Parameters.AddWithValue("@mobile", Mobile);
                    cmd.Parameters.AddWithValue("@billType", BillTypeId);

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

        public List<RandomBillGenerationModel> FetchAllRandomBill()
        {
            List<RandomBillGenerationModel> List = new List<RandomBillGenerationModel>();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"SELECT
	                                    rand_bill.random_bill_id_pk,
	                                    rand_bill.random_bill_name,
	                                    dept.department_name,
                                        fund.fund_scheme_name,
	                                    rand_bill.random_bill_work_desc,
	                                    rand_bill.random_bill_mobile_no,
                                        billtype.typeof_bill_name
                                    FROM
	                                    tbl_accounts_random_bill_generation_details AS rand_bill
	                                    LEFT JOIN tbl_accounts_department_master AS dept ON dept.department_id_pk = rand_bill.random_bill_dept_id_fk
	                                    LEFT JOIN tbl_accounts_fund_master AS fund ON fund.fund_scheme_id_pk = rand_bill.random_bill_fund_id_fk
                                        LEFT JOIN tbl_accounts_bill_master AS billtype ON billtype.typeof_bill_id_pk = rand_bill.random_bill_type_id_fk";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        List = JsonConvert.DeserializeObject<List<RandomBillGenerationModel>>(JsonConvert.SerializeObject(dt));
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