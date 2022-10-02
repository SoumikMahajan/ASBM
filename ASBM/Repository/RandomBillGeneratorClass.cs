﻿using ASBM.Models;
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

                    Query = @"INSERT INTO tbl_accounts_random_bill_generation_details (random_bill_name, random_bill_dept_id_fk, random_bill_fund_id_fk, random_bill_work_desc, random_bill_mobile_no) VALUES(@billName, @deptid, @fundid, @workDesc, @mobile)";

                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@billName", Name);
                    cmd.Parameters.AddWithValue("@deptid", BillTypeId);
                    cmd.Parameters.AddWithValue("@fundid", FundId);
                    cmd.Parameters.AddWithValue("@workDesc", WorkDesc);
                    cmd.Parameters.AddWithValue("@mobile", Mobile);

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
                    string Query = @"select random_bill_id_pk, creation_date, process_status from tbl_accounts_random_bill_generation_details";
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