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
    public class MS_DEPT_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, KODE_DEPT, DEPT, KET, CREATED_BY, CREATED_DATE, STATUS FROM MS_DEPT"), CnString))
            {
                command.CommandType = CommandType.Text;
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(Int32 ID)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, KODE_DEPT, DEPT, KET, CREATED_BY, CREATED_DATE, STATUS FROM MS_DEPT WHERE ID = @ID"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@ID", ID);
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, KODE_DEPT, DEPT, KET, CREATED_BY, CREATED_DATE, STATUS FROM MS_DEPT WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(MS_DEPT msdept)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO MS_DEPT(KODE_DEPT, DEPT, KET, CREATED_BY, CREATED_DATE, STATUS) VALUES (@KODE_DEPT, @DEPT, @KET, @CREATED_BY, @CREATED_DATE, @STATUS)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = msdept.ID;
                    command.Parameters.Add("@KODE_DEPT", SqlDbType.VarChar).Value = msdept.KODE_DEPT;
                    command.Parameters.Add("@DEPT", SqlDbType.VarChar).Value = msdept.DEPT;
                    command.Parameters.Add("@KET", SqlDbType.VarChar).Value = msdept.KET;
                    command.Parameters.Add("@CREATED_BY", SqlDbType.VarChar).Value = msdept.CREATED_BY;
                    command.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime).Value = msdept.CREATED_DATE;
                    command.Parameters.Add("@STATUS", SqlDbType.Bit).Value = msdept.STATUS;
                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
            }
            finally
            {
                CnString.Close();
            }
        }

        public void Update(MS_DEPT msdept)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE MS_DEPT SET KODE_DEPT = @KODE_DEPT, DEPT = @DEPT, KET = @KET, CREATED_BY = @CREATED_BY, CREATED_DATE = @CREATED_DATE, STATUS = @STATUS WHERE ID = @ID ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = msdept.ID;
                    command.Parameters.Add("@KODE_DEPT", SqlDbType.VarChar).Value = msdept.KODE_DEPT;
                    command.Parameters.Add("@DEPT", SqlDbType.VarChar).Value = msdept.DEPT;
                    command.Parameters.Add("@KET", SqlDbType.VarChar).Value = msdept.KET;
                    command.Parameters.Add("@CREATED_BY", SqlDbType.VarChar).Value = msdept.CREATED_BY;
                    command.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime).Value = msdept.CREATED_DATE;
                    command.Parameters.Add("@STATUS", SqlDbType.Bit).Value = msdept.STATUS;
                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
            }
            finally
            {
                CnString.Close();
            }
        }

        public void DeleteByKey(Int16 ID)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM MS_DEPT  WHERE ID = @ID"), CnString))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@ID", ID);
                    CnString.Open();

                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet, "SearchData");
                    CnString.Close();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM MS_DEPT WHERE {0} ", Where), CnString))
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
            }
            finally
            {
                CnString.Close();
            }
        }
    }
}
