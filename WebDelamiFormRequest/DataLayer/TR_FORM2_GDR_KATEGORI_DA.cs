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
    public class TR_FORM2_GDR_KATEGORI_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID_KATEGORI, NO_FORM, KODE_KATEGORI, DETAIL, ISPILIH FROM TR_FORM2_GDR_KATEGORI"), CnString))
            {
                command.CommandType = CommandType.Text;
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(String ID_KATEGORI)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID_KATEGORI, NO_FORM, KODE_KATEGORI, DETAIL, ISPILIH FROM TR_FORM2_GDR_KATEGORI WHERE ID_KATEGORI = @ID_KATEGORI"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@ID_KATEGORI", ID_KATEGORI);
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID_KATEGORI, NO_FORM, KODE_KATEGORI, DETAIL, ISPILIH FROM TR_FORM2_GDR_KATEGORI WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(TR_FORM2_GDR_KATEGORI trform2gdrkategori)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO TR_FORM2_GDR_KATEGORI(NO_FORM, KODE_KATEGORI, DETAIL, ISPILIH) VALUES (@NO_FORM, @KODE_KATEGORI, @DETAIL, @ISPILIH)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform2gdrkategori.NO_FORM;
                    command.Parameters.Add("@KODE_KATEGORI", SqlDbType.VarChar).Value = trform2gdrkategori.KODE_KATEGORI;
                    command.Parameters.Add("@DETAIL", SqlDbType.VarChar).Value = trform2gdrkategori.DETAIL;
                    command.Parameters.Add("@ISPILIH", SqlDbType.VarChar).Value = trform2gdrkategori.ISPILIH;
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


        public void Update(TR_FORM2_GDR_KATEGORI trform2gdrkategori)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM2_GDR_KATEGORI SET NO_FORM = @NO_FORM, KODE_KATEGORI = @KODE_KATEGORI, DETAIL = @DETAIL, ISPILIH = @ISPILIH WHERE ID_KATEGORI = @ID_KATEGORI ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID_KATEGORI", SqlDbType.Int).Value = trform2gdrkategori.ID_KATEGORI;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform2gdrkategori.NO_FORM;
                    command.Parameters.Add("@KODE_KATEGORI", SqlDbType.VarChar).Value = trform2gdrkategori.KODE_KATEGORI;
                    command.Parameters.Add("@DETAIL", SqlDbType.VarChar).Value = trform2gdrkategori.DETAIL;
                    command.Parameters.Add("@ISPILIH", SqlDbType.VarChar).Value = trform2gdrkategori.ISPILIH;
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


        public void DeleteByKey(String ID_KATEGORI)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM TR_FORM2_GDR_KATEGORI  WHERE ID_KATEGORI = @ID_KATEGORI"), CnString))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@ID_KATEGORI", ID_KATEGORI);
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM TR_FORM2_GDR_KATEGORI WHERE {0} ", Where), CnString))
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
