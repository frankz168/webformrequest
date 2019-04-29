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
    public class TR_FORM4_GDR_IKLAN_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID_IKLAN, NO_FORM, KODE_IKLAN, NAMA_IKLAN, DETAIL, KET, ISPILIH, PIC FROM TR_FORM4_GDR_IKLAN"), CnString))
            {
                command.CommandType = CommandType.Text;
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(String ID_IKLAN)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID_IKLAN, NO_FORM, KODE_IKLAN, NAMA_IKLAN, DETAIL, KET, ISPILIH, PIC FROM TR_FORM4_GDR_IKLAN WHERE ID_IKLAN = @ID_IKLAN"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@ID_IKLAN", ID_IKLAN);
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID_IKLAN, NO_FORM, KODE_IKLAN, NAMA_IKLAN, DETAIL, KET, ISPILIH, PIC FROM TR_FORM4_GDR_IKLAN WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(TR_FORM4_GDR_IKLAN trform4gdriklan)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO TR_FORM4_GDR_IKLAN(NO_FORM, KODE_IKLAN, NAMA_IKLAN, DETAIL, KET, ISPILIH, PIC) VALUES (@NO_FORM, @KODE_IKLAN, @NAMA_IKLAN, @DETAIL, @KET, @ISPILIH, @PIC)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    //command.Parameters.Add("@ID_IKLAN", SqlDbType.Int).Value = trform4gdriklan.ID_IKLAN;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform4gdriklan.NO_FORM;
                    command.Parameters.Add("@KODE_IKLAN", SqlDbType.VarChar).Value = trform4gdriklan.KODE_IKLAN;
                    command.Parameters.Add("@NAMA_IKLAN", SqlDbType.VarChar).Value = trform4gdriklan.NAMA_IKLAN;
                    command.Parameters.Add("@DETAIL", SqlDbType.VarChar).Value = trform4gdriklan.DETAIL;
                    command.Parameters.Add("@KET", SqlDbType.VarChar).Value = trform4gdriklan.KET;
                    command.Parameters.Add("@ISPILIH", SqlDbType.VarChar).Value = trform4gdriklan.ISPILIH;
                    command.Parameters.Add("@PIC", SqlDbType.VarChar).Value = trform4gdriklan.PIC;
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


        public void Update(TR_FORM4_GDR_IKLAN trform4gdriklan)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM4_GDR_IKLAN SET NO_FORM = @NO_FORM, KODE_IKLAN = @KODE_IKLAN, NAMA_IKLAN = @NAMA_IKLAN, DETAIL = @DETAIL, KET = @KET, ISPILIH = @ISPILIH, PIC = @PIC WHERE ID_IKLAN = @ID_IKLAN ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID_IKLAN", SqlDbType.Int).Value = trform4gdriklan.ID_IKLAN;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform4gdriklan.NO_FORM;
                    command.Parameters.Add("@KODE_IKLAN", SqlDbType.VarChar).Value = trform4gdriklan.KODE_IKLAN;
                    command.Parameters.Add("@NAMA_IKLAN", SqlDbType.VarChar).Value = trform4gdriklan.NAMA_IKLAN;
                    command.Parameters.Add("@DETAIL", SqlDbType.VarChar).Value = trform4gdriklan.DETAIL;
                    command.Parameters.Add("@KET", SqlDbType.VarChar).Value = trform4gdriklan.KET;
                    command.Parameters.Add("@ISPILIH", SqlDbType.VarChar).Value = trform4gdriklan.ISPILIH;
                    command.Parameters.Add("@PIC", SqlDbType.VarChar).Value = trform4gdriklan.PIC;
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


        public void DeleteByKey(String ID_IKLAN)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM TR_FORM4_GDR_IKLAN  WHERE ID_IKLAN = @ID_IKLAN"), CnString))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@ID_IKLAN", ID_IKLAN);
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM TR_FORM4_GDR_IKLAN WHERE {0} ", Where), CnString))
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
