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
	                                    bill_allot.voucher_id_pk,
	                                    bill_allot.bill_allotement_docket_no,
	                                    bill_allot.bill_allotement_date
                                    FROM
	                                    tbl_accounts_voucher AS vouter
	                                    WHERE bill_allot.bill_allotement_docket_no = @DocketNo and bill_allot.bill_allotement_date=@entryDate";
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
    }
}