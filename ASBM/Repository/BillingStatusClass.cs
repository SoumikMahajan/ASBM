using ASBM.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ASBM.Repository
{
    public class BillingStatusClass
    {
        string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public MultipleModel FetchBillingBySearch(string docketNo,string entryDate)
        {
            MultipleModel mm = new MultipleModel();
            try
            {
                mm.billingStatusList = check_billing_status(docketNo, entryDate);
            }
            catch (Exception)
            {

            }
            return mm;
        }

        public List<BillingStatusModel> check_billing_status(string docketNo,string entryDate)
        {
            List<BillingStatusModel> Res = new List<BillingStatusModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    //string Query = @"SELECT
                    //                 bill_allot.bill_allotement_id_pk,
                    //                 bill_allot.bill_allotement_docket_no,
                    //                 bill_allot.bill_allotement_date
                    //                FROM
                    //                 tbl_accounts_bill_allotement_details AS bill_allot
                    //                 WHERE bill_allot.bill_allotement_docket_no = @DocketNo and bill_allot.bill_allotement_date=@entryDate";

                    string Query = @"SELECT * from (SELECT bill_docket_no,convert(date,entry_time) as entry_time, 
                                    CASE
		                                WHEN approve_status = 1 THEN
			                                'Approved'
		                                WHEN approve_status = 2 THEN
			                                'Rejected'
		                                WHEN approve_status = 0 THEN
			                                'Pending'
		                                END AS current_tatus 
                                    FROM tbl_accounts_bill_details
                                    union all
                                    SELECT random_bill_docket_no,convert(date,entry_time) as entry_time, 
                                    CASE
		                                WHEN approve_status = 1 THEN
			                                'Approved'
		                                WHEN approve_status = 2 THEN
			                                'Rejected'
		                                WHEN approve_status = 0 THEN
			                                'Pending'
		                                END AS current_tatus 
                                    FROM tbl_accounts_random_bill_generation_details) as TBL 
                                    WHERE bill_docket_no=@DocketNo and entry_time=@entryDate";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@DocketNo", docketNo);
                    cmd.Parameters.AddWithValue("@entryDate", entryDate);                   
                    
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    //adp.Fill(dt);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    List = JsonConvert.DeserializeObject<List<BillingStatusModel>>(JsonConvert.SerializeObject(dt));
                    //}

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Res = JsonConvert.DeserializeObject<List<BillingStatusModel>>(JsonConvert.SerializeObject(dt));
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
    }
}