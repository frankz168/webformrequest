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
    public class MS_USER_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM MS_USER"), CnString))
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM MS_USER WHERE ID = @ID"), CnString))
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

        public virtual DataSet GetDataByUserKey(String USERNAME)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM MS_USER WHERE USERNAME = @USERNAME"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@USERNAME", USERNAME);
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM MS_USER WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataUrutanUser(String USERNAME, String KODE_FORM)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT users.*, userhandle.* FROM MS_USER As users LEFT JOIN MS_USER_HANDLE As userhandle On users.KD_JABATAN = userhandle.KD_JABATAN WHERE USERNAME = @USERNAME And (@KODE_FORM = 'ALL' OR KODE_FORM = @KODE_FORM)"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@USERNAME", USERNAME);
                command.Parameters.Add("@KODE_FORM", KODE_FORM);
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataUrutanUserCustom(String USERNAME, String KODE_FORM, String ACTION)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT users.*, userhandle.* FROM MS_USER As users INNER JOIN MS_USER_HANDLE As userhandle On users.KD_JABATAN = userhandle.KD_JABATAN WHERE USERNAME = @USERNAME And (@KODE_FORM = 'ALL' OR KODE_FORM = @KODE_FORM) AND ((@ACTION = 'ALL' OR ACTION = @ACTION))"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@USERNAME", USERNAME);
                command.Parameters.Add("@KODE_FORM", KODE_FORM);
                command.Parameters.Add("@ACTION", ACTION);
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(MS_USER msuser)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO MS_USER(ID_DEPT, USERNAME, PASSWORD, DEPT, EMAIL, CREATED_BY, CREATED_DATE, STATUS, FORGOT_PASSWORD, FORGOT_PASSWORD_TOKEN, LAST_PASSWORD_CHANGE, KD_BRAND, KD_JABATAN, FULL_NAME, UserProfileId) VALUES (@ID_DEPT, @USERNAME, @PASSWORD, @DEPT, @EMAIL, @CREATED_BY, @CREATED_DATE, @STATUS, @FORGOT_PASSWORD, @FORGOT_PASSWORD_TOKEN, @LAST_PASSWORD_CHANGE, @KD_BRAND, @KD_JABATAN, @FULL_NAME, @UserProfileId)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    //command.Parameters.Add("@ID", SqlDbType.BigInt).Value = msuser.ID;
                    command.Parameters.Add("@ID_DEPT", SqlDbType.VarChar).Value = msuser.ID_DEPT;
                    command.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = msuser.USERNAME;
                    command.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = msuser.PASSWORD;
                    command.Parameters.Add("@DEPT", SqlDbType.VarChar).Value = msuser.DEPT;
                    command.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = msuser.EMAIL;
                    command.Parameters.Add("@CREATED_BY", SqlDbType.VarChar).Value = msuser.CREATED_BY;
                    command.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime).Value = msuser.CREATED_DATE;
                    command.Parameters.Add("@STATUS", SqlDbType.Bit).Value = msuser.STATUS;
                    command.Parameters.Add("@FORGOT_PASSWORD", SqlDbType.VarChar).Value = msuser.FORGOT_PASSWORD;
                    command.Parameters.Add("@FORGOT_PASSWORD_TOKEN", SqlDbType.VarChar).Value = msuser.FORGOT_PASSWORD_TOKEN;
                    command.Parameters.Add("@LAST_PASSWORD_CHANGE", SqlDbType.DateTime).Value = msuser.LAST_PASSWORD_CHANGE;
                    command.Parameters.Add("@KD_BRAND", SqlDbType.VarChar).Value = msuser.KD_BRAND;
                    command.Parameters.Add("@KD_JABATAN", SqlDbType.VarChar).Value = msuser.KD_JABATAN;
                    command.Parameters.Add("@FULL_NAME", SqlDbType.VarChar).Value = msuser.FULL_NAME;
                    command.Parameters.Add("@UserProfileId", SqlDbType.VarChar).Value = msuser.UserProfileId;
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

        public void Update(MS_USER msuser)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE MS_USER SET ID_DEPT = @ID_DEPT, USERNAME = @USERNAME, PASSWORD = @PASSWORD, DEPT = @DEPT, EMAIL = @EMAIL, STATUS = @STATUS, KD_BRAND = @KD_BRAND, KD_JABATAN = @KD_JABATAN ,FULL_NAME = @FULL_NAME, UserProfileId = @UserProfileId WHERE USERNAME = @USERNAME");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    //command.Parameters.Add("@ID", SqlDbType.BigInt).Value = msuser.ID;
                    command.Parameters.Add("@ID_DEPT", SqlDbType.VarChar).Value = msuser.ID_DEPT;
                    command.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = msuser.USERNAME;
                    command.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = msuser.PASSWORD;
                    command.Parameters.Add("@DEPT", SqlDbType.VarChar).Value = msuser.DEPT;
                    command.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = msuser.EMAIL;
                    command.Parameters.Add("@STATUS", SqlDbType.Bit).Value = msuser.STATUS;
                    command.Parameters.Add("@KD_BRAND", SqlDbType.VarChar).Value = msuser.KD_BRAND;
                    command.Parameters.Add("@KD_JABATAN", SqlDbType.VarChar).Value = msuser.KD_JABATAN;
                    command.Parameters.Add("@FULL_NAME", SqlDbType.VarChar).Value = msuser.FULL_NAME;
                    command.Parameters.Add("@UserProfileId", SqlDbType.VarChar).Value = msuser.UserProfileId;
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

        public void DeleteByKey(String USERNAME)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM MS_USER  WHERE USERNAME = @USERNAME"), CnString))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@USERNAME", USERNAME);
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM MS_USER WHERE {0} ", Where), CnString))
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
