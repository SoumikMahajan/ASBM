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
    public class RejectedBillClass
    {
        string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public MultipleModel Fetch_All_Rejected_Bill_Details()
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm.rejectBillList = Get_All_Rejected_Bill_Data();
            }
            catch (Exception)
            {

            }
            return mm;
        }

        public List<RejectedBillModel> Get_All_Rejected_Bill_Data()
        {
            List<RejectedBillModel> Res = new List<RejectedBillModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"( SELECT
	                                    bill.bill_docket_no AS docket_no,
	                                    CONVERT ( VARCHAR ( 10 ), bill.entry_time, 105 ) AS entry_time,
	                                    dept.department_name 
	                                    FROM
		                                    tbl_accounts_bill_details AS bill
		                                    LEFT JOIN tbl_accounts_department_master AS dept ON dept.department_id_pk =                              bill.bill_department_id_fk 
	                                    WHERE
		                                    bill.approve_status = 2 
	                                ) 
                                    UNION
	                                (
	                                    SELECT
		                                    rand.random_bill_docket_no AS docket_no,
		                                    CONVERT ( VARCHAR ( 10 ), rand.entry_time, 105 ) AS entry_time,
		                                    dept.department_name 
	                                    FROM
		                                    tbl_accounts_random_bill_generation_details AS rand
		                                    LEFT JOIN tbl_department_master AS dept ON dept.department_id_pk = rand.random_bill_dept_id_fk 
	                                    WHERE
	                                    rand.approve_status = 2 
	                                )
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
                        Res = JsonConvert.DeserializeObject<List<RejectedBillModel>>(JsonConvert.SerializeObject(dt));
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

        public int Reissue_voucher(string DocketNo,string getTwoString)
        {
            int res = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    //string Query1 = null;
                    //string Query2 = null;
                    //string Query3 = null;
                    SqlCommand cmd = new SqlCommand("[dbo].[spAccountsRejectBill]", con);
                    if (getTwoString == "NB")
                    {
                        //Query1 = @"UPDATE tbl_accounts_bill_details SET approve_status = 0 AND bill_alloted_status = 0 WHERE bill_docket_no = @docketNo AND approve_status = 2";
                        //Query2 = @"UPDATE tbl_accounts_bill_allotement_details SET approve_status = 0 WHERE bill_allotement_docket_no = @docketNo";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OPERATION_ID", 1);
                        cmd.Parameters.AddWithValue("@docketNo", DocketNo);
                    }
                    if(getTwoString == "RA")
                    {
                        //Query3 = @"UPDATE tbl_accounts_random_bill_generation_details SET approve_status = 0 WHERE random_bill_docket_no = @docketNo";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OPERATION_ID", 2);
                        cmd.Parameters.AddWithValue("@docketNo", DocketNo);
                    }

                    //********* OR **************/
                                    
                    con.Open();
                    int AffectedRows = cmd.ExecuteNonQuery();
                    if (AffectedRows > 0)
                    {
                        res = 1;
                    }
                    else
                    {
                        res = 0;
                    }
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
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