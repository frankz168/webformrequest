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
    public class TR_FORM2_GDR_BUDGET_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM2_GDR_BUDGET"), CnString))
            {
                command.CommandType = CommandType.Text;
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(String ID_BUDGET)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM2_GDR_BUDGET WHERE ID_BUDGET = @ID_BUDGET"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@ID_BUDGET", ID_BUDGET);
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM2_GDR_BUDGET WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(TR_FORM2_GDR_BUDGET trform1gdrbudget)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO TR_FORM2_GDR_BUDGET(NO_FORM, KODE_BUDGET, NAMA_BUDGET, DETAIL, KET, ISPILIH, PIC) VALUES (@NO_FORM, @KODE_BUDGET, @NAMA_BUDGET, @DETAIL, @KET, @ISPILIH, @PIC)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    //command.Parameters.Add("@ID_BUDGET", SqlDbType.Int).Value = trform1gdrbudget.ID_BUDGET;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdrbudget.NO_FORM;
                    command.Parameters.Add("@KODE_BUDGET", SqlDbType.VarChar).Value = trform1gdrbudget.KODE_BUDGET;
                    command.Parameters.Add("@NAMA_BUDGET", SqlDbType.VarChar).Value = trform1gdrbudget.NAMA_BUDGET;
                    command.Parameters.Add("@DETAIL", SqlDbType.VarChar).Value = trform1gdrbudget.DETAIL;
                    command.Parameters.Add("@KET", SqlDbType.VarChar).Value = trform1gdrbudget.KET;
                    command.Parameters.Add("@ISPILIH", SqlDbType.VarChar).Value = trform1gdrbudget.ISPILIH;
                    command.Parameters.Add("@PIC", SqlDbType.VarChar).Value = trform1gdrbudget.PIC;
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

        public void Update(TR_FORM2_GDR_BUDGET trform1gdrbudget)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM2_GDR_BUDGET SET NO_FORM = @NO_FORM, KODE_BUDGET = @KODE_BUDGET, NAMA_BUDGET = @NAMA_BUDGET, DETAIL = @DETAIL,  KET = @KET, ISPILIH = @ISPILIH, PIC = @PIC WHERE ID_BUDGET = @ID_BUDGET ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID_BUDGET", SqlDbType.Int).Value = trform1gdrbudget.ID_BUDGET;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdrbudget.NO_FORM;
                    command.Parameters.Add("@KODE_BUDGET", SqlDbType.VarChar).Value = trform1gdrbudget.KODE_BUDGET;
                    command.Parameters.Add("@NAMA_BUDGET", SqlDbType.VarChar).Value = trform1gdrbudget.NAMA_BUDGET;
                    command.Parameters.Add("@DETAIL", SqlDbType.VarChar).Value = trform1gdrbudget.DETAIL;
                    command.Parameters.Add("@KET", SqlDbType.VarChar).Value = trform1gdrbudget.KET;
                    command.Parameters.Add("@ISPILIH", SqlDbType.VarChar).Value = trform1gdrbudget.ISPILIH;
                    command.Parameters.Add("@PIC", SqlDbType.VarChar).Value = trform1gdrbudget.PIC;
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

        public void DeleteByKey(String ID_BUDGET)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM TR_FORM2_GDR_BUDGET  WHERE ID_BUDGET = @ID_BUDGET"), CnString))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@ID_BUDGET", ID_BUDGET);
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM TR_FORM2_GDR_BUDGET WHERE {0} ", Where), CnString))
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
