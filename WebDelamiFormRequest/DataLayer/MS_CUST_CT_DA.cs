using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WebDelamiFormRequest.Domain;

namespace WebDelamiFormRequest.DataLayer
{
    public class MS_CUST_CT_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionStringAzure"].ConnectionString;
        SqlConnection CnStringAzure = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM MS_CUST_CT"), CnStringAzure))
            {
                command.CommandType = CommandType.Text;
                CnStringAzure.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnStringAzure.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(String ID)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM MS_CUST_CT WHERE ID = @ID"), CnStringAzure))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@ID", ID);
                CnStringAzure.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnStringAzure.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataFilter(String Where)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM MS_CUST_CT WHERE {0} ", Where), CnStringAzure))
            {
                CnStringAzure.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnStringAzure.Close();
            }
            return dataSet;
        }

    }
}