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
    public class CUST_PTS_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM CUST_PTS"), CnString))
            {
                command.CommandType = CommandType.Text;
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(String kode_cust)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM CUST_PTS WHERE kode_cust = @kode_cust"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@kode_cust", kode_cust);
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataFilter(String Where)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM CUST_PTS WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(CUST_PTS custpts)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO CUST_PTS(kode_cust, Nama, City, Search_Item, Alamat) VALUES (@kode_cust, @Nama, @City, @Search_Item, @Alamat)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@kode_cust", SqlDbType.VarChar).Value = custpts.kode_cust;
                    command.Parameters.Add("@Nama", SqlDbType.VarChar).Value = custpts.Nama;
                    command.Parameters.Add("@City", SqlDbType.VarChar).Value = custpts.City;
                    command.Parameters.Add("@Search_Item", SqlDbType.VarChar).Value = custpts.Search_Item;
                    command.Parameters.Add("@Alamat", SqlDbType.VarChar).Value = custpts.Alamat;
                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
                throw ex;
            }
            finally
            {
                CnString.Close();
            }
        }


        public void Update(CUST_PTS custpts)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE CUST_PTS SET Nama = @Nama, City = @City, Search_Item = @Search_Item, Alamat = @Alamat WHERE kode_cust = @kode_cust ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@kode_cust", SqlDbType.VarChar).Value = custpts.kode_cust;
                    command.Parameters.Add("@Nama", SqlDbType.VarChar).Value = custpts.Nama;
                    command.Parameters.Add("@City", SqlDbType.VarChar).Value = custpts.City;
                    command.Parameters.Add("@Search_Item", SqlDbType.VarChar).Value = custpts.Search_Item;
                    command.Parameters.Add("@Alamat", SqlDbType.VarChar).Value = custpts.Alamat;
                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
                throw ex;
            }
            finally
            {
                CnString.Close();
            }
        }


        public void DeleteByKey(String kode_cust)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM CUST_PTS  WHERE kode_cust = @kode_cust"), CnString))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@kode_cust", kode_cust);
                    CnString.Open();

                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet, "SearchData");
                    CnString.Close();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
                throw ex;
            }
            finally
            {
                CnString.Close();
            }
        }

        public void DeleteFilter(String Where)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM CUST_PTS WHERE {0} ", Where), CnString))
                {
                    CnString.Open();

                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet, "SearchData");
                    CnString.Close();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
                throw ex;
            }
            finally
            {
                CnString.Close();
            }
        }
    }
}
