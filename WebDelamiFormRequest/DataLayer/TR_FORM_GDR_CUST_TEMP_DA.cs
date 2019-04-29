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
    public class TR_FORM_GDR_CUST_TEMP_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct FROM TR_FORM_GDR_CUST_TEMP"), CnString))
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct FROM TR_FORM_GDR_CUST_TEMP WHERE ID = @ID"), CnString))
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID, KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct FROM TR_FORM_GDR_CUST_TEMP WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(TR_FORM_GDR_CUST_TEMP trformgdrcusttemp)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO TR_FORM_GDR_CUST_TEMP(KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct) VALUES (@KODE_FORM, @NO_FORM, @kode_cust, @kode_ct, @site, @nama_cust, @nama_ct)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = trformgdrcusttemp.ID;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = trformgdrcusttemp.KODE_FORM;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trformgdrcusttemp.NO_FORM;
                    command.Parameters.Add("@kode_cust", SqlDbType.VarChar).Value = trformgdrcusttemp.kode_cust;
                    command.Parameters.Add("@kode_ct", SqlDbType.VarChar).Value = trformgdrcusttemp.kode_ct;
                    command.Parameters.Add("@site", SqlDbType.VarChar).Value = trformgdrcusttemp.site;
                    command.Parameters.Add("@nama_cust", SqlDbType.VarChar).Value = trformgdrcusttemp.nama_cust;
                    command.Parameters.Add("@nama_ct", SqlDbType.VarChar).Value = trformgdrcusttemp.nama_ct;
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

        public void Update(TR_FORM_GDR_CUST_TEMP trformgdrcusttemp)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM_GDR_CUST_TEMP SET KODE_FORM = @KODE_FORM, NO_FORM = @NO_FORM, kode_cust = @kode_cust, kode_ct = @kode_ct, site = @site, nama_cust = @nama_cust, nama_ct = @nama_ct WHERE ID = @ID ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = trformgdrcusttemp.ID;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = trformgdrcusttemp.KODE_FORM;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trformgdrcusttemp.NO_FORM;
                    command.Parameters.Add("@kode_cust", SqlDbType.VarChar).Value = trformgdrcusttemp.kode_cust;
                    command.Parameters.Add("@kode_ct", SqlDbType.VarChar).Value = trformgdrcusttemp.kode_ct;
                    command.Parameters.Add("@site", SqlDbType.VarChar).Value = trformgdrcusttemp.site;
                    command.Parameters.Add("@nama_cust", SqlDbType.VarChar).Value = trformgdrcusttemp.nama_cust;
                    command.Parameters.Add("@nama_ct", SqlDbType.VarChar).Value = trformgdrcusttemp.nama_ct;
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM TR_FORM_GDR_CUST_TEMP  WHERE ID = @ID"), CnString))
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM TR_FORM_GDR_CUST_TEMP WHERE {0} ", Where), CnString))
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

        public void Truncate()
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("TRUNCATE TABLE TR_FORM_GDR_CUST_TEMP"), CnString))
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
