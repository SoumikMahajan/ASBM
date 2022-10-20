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
    public class BillingStatusClass
    {
        string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public List<BillingStatusModel> check_billing_status(string docketNo, string entryDate)
        {
            List<BillingStatusModel> List = new List<BillingStatusModel>();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string Query = @"SELECT
	                                    bill_allot.bill_allotement_id_pk,
	                                    bill_allot.bill_allotement_docket_no,
	                                    bill_allot.bill_allotement_date
                                    FROM
	                                    tbl_accounts_bill_allotement_details AS bill_allot
	                                    WHERE bill_allot.bill_allotement_docket_no = '" + docketNo + "' AND bill_allot.bill_allotement_date = '" + entryDate +"'";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        List = JsonConvert.DeserializeObject<List<BillingStatusModel>>(JsonConvert.SerializeObject(dt));
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