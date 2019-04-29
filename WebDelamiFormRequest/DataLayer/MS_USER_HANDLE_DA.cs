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
    public class MS_USER_HANDLE_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, KODE_FORM, KD_JABATAN, URUTAN, ACTION, PAGE_NAME, SP FROM MS_USER_HANDLE"), CnString))
            {
                command.CommandType = CommandType.Text;
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(String ID)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, KODE_FORM, KD_JABATAN, URUTAN, ACTION, PAGE_NAME, SP FROM MS_USER_HANDLE WHERE ID = @ID"), CnString))
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, KODE_FORM, KD_JABATAN, URUTAN, ACTION, PAGE_NAME, SP FROM MS_USER_HANDLE WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(MS_USER_HANDLE msuserhandle)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO MS_USER_HANDLE(KODE_FORM, KD_JABATAN, URUTAN, ACTION, PAGE_NAME, SP) VALUES (@KODE_FORM, @KD_JABATAN, @URUTAN, @ACTION, @PAGE_NAME, @SP)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID", SqlDbType.BigInt).Value = msuserhandle.ID;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = msuserhandle.KODE_FORM;
                    command.Parameters.Add("@KD_JABATAN", SqlDbType.VarChar).Value = msuserhandle.KD_JABATAN;
                    command.Parameters.Add("@URUTAN", SqlDbType.Int).Value = msuserhandle.URUTAN;
                    command.Parameters.Add("@ACTION", SqlDbType.VarChar).Value = msuserhandle.ACTION;
                    command.Parameters.Add("@PAGE_NAME", SqlDbType.VarChar).Value = msuserhandle.PAGE_NAME;
                    command.Parameters.Add("@SP", SqlDbType.VarChar).Value = msuserhandle.SP;
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

        public void Update(MS_USER_HANDLE msuserhandle)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE MS_USER_HANDLE SET KODE_FORM = @KODE_FORM, KD_JABATAN = @KD_JABATAN, URUTAN = @URUTAN, ACTION = @ACTION, PAGE_NAME = @PAGE_NAME, SP = @SP WHERE ID = @ID ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID", SqlDbType.BigInt).Value = msuserhandle.ID;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = msuserhandle.KODE_FORM;
                    command.Parameters.Add("@KD_JABATAN", SqlDbType.VarChar).Value = msuserhandle.KD_JABATAN;
                    command.Parameters.Add("@URUTAN", SqlDbType.Int).Value = msuserhandle.URUTAN;
                    command.Parameters.Add("@ACTION", SqlDbType.VarChar).Value = msuserhandle.ACTION;
                    command.Parameters.Add("@PAGE_NAME", SqlDbType.VarChar).Value = msuserhandle.PAGE_NAME;
                    command.Parameters.Add("@SP", SqlDbType.VarChar).Value = msuserhandle.SP;
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

        public void DeleteByKey(String ID)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM MS_USER_HANDLE  WHERE ID = @ID"), CnString))
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM MS_USER_HANDLE WHERE {0} ", Where), CnString))
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
