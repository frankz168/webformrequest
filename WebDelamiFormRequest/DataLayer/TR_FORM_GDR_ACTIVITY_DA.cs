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
    public class TR_FORM_GDR_ACTIVITY_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM_GDR_ACTIVITY"), CnString))
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM_GDR_ACTIVITY WHERE ID = @ID"), CnString))
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM_GDR_ACTIVITY WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(TR_FORM_GDR_ACTIVITY trform1gdractivity)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO TR_FORM_GDR_ACTIVITY(USERNAME, ACTIVITY_TIME, KODE_FORM, NO_FORM, STATUS, DESCRIPTION, REVISION, URUTAN, SP, USER_CURRENT, NEXT_USER, URUTAN_USER_CURRENT, URUTAN_NEXT_USER) VALUES (@USERNAME, @ACTIVITY_TIME, @KODE_FORM, @NO_FORM, @STATUS, @DESCRIPTION, @REVISION, @URUTAN, @SP, @USER_CURRENT, @NEXT_USER, @URUTAN_USER_CURRENT, @URUTAN_NEXT_USER)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    //command.Parameters.Add("@ID", SqlDbType.Int).Value = trform1gdractivity.ID;
                    command.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = trform1gdractivity.USERNAME;
                    command.Parameters.Add("@ACTIVITY_TIME", SqlDbType.DateTime).Value = trform1gdractivity.ACTIVITY_TIME;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = trform1gdractivity.KODE_FORM;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdractivity.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform1gdractivity.STATUS;
                    command.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = trform1gdractivity.DESCRIPTION;
                    command.Parameters.Add("@REVISION", SqlDbType.VarChar).Value = trform1gdractivity.REVISION;
                    command.Parameters.Add("@URUTAN", SqlDbType.Int).Value = trform1gdractivity.URUTAN;
                    command.Parameters.Add("@SP", SqlDbType.VarChar).Value = trform1gdractivity.SP;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform1gdractivity.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform1gdractivity.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform1gdractivity.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform1gdractivity.URUTAN_NEXT_USER;
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

        public void Update(TR_FORM_GDR_ACTIVITY trform1gdractivity)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM_GDR_ACTIVITY SET USERNAME = @USERNAME, ACTIVITY_TIME = @ACTIVITY_TIME, KODE_FORM = @KODE_FORM, NO_FORM = @NO_FORM, STATUS = @STATUS, DESCRIPTION = @DESCRIPTION, REVISION = @REVISION, URUTAN = @URUTAN, SP = @SP, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE ID = @ID ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = trform1gdractivity.ID;
                    command.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = trform1gdractivity.USERNAME;
                    command.Parameters.Add("@ACTIVITY_TIME", SqlDbType.DateTime).Value = trform1gdractivity.ACTIVITY_TIME;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = trform1gdractivity.KODE_FORM;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdractivity.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform1gdractivity.STATUS;
                    command.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = trform1gdractivity.DESCRIPTION;
                    command.Parameters.Add("@REVISION", SqlDbType.VarChar).Value = trform1gdractivity.REVISION;
                    command.Parameters.Add("@URUTAN", SqlDbType.Int).Value = trform1gdractivity.URUTAN;
                    command.Parameters.Add("@SP", SqlDbType.VarChar).Value = trform1gdractivity.SP;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform1gdractivity.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform1gdractivity.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform1gdractivity.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform1gdractivity.URUTAN_NEXT_USER;
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM TR_FORM_GDR_ACTIVITY  WHERE ID = @ID"), CnString))
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM TR_FORM_GDR_ACTIVITY WHERE {0} ", Where), CnString))
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
