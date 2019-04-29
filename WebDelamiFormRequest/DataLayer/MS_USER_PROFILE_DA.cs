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
    public class MS_USER_PROFILE_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT UserProfileId, Description, Active, CB, CO, MB, MO FROM MS_USER_PROFILE"), CnString))
            {
                command.CommandType = CommandType.Text;
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(String UserProfileId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT UserProfileId, Description, Active, CB, CO, MB, MO FROM MS_USER_PROFILE WHERE UserProfileId = @UserProfileId"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@UserProfileId", UserProfileId);
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT UserProfileId, Description, Active, CB, CO, MB, MO FROM MS_USER_PROFILE WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(MS_USER_PROFILE msuserprofile)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO MS_USER_PROFILE(UserProfileId, Description, Active, CB, CO, MB, MO) VALUES (@UserProfileId, @Description, @Active, @CB, @CO, @MB, @MO)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@UserProfileId", SqlDbType.VarChar).Value = msuserprofile.UserProfileId;
                    command.Parameters.Add("@Description", SqlDbType.VarChar).Value = msuserprofile.Description;
                    command.Parameters.Add("@Active", SqlDbType.VarChar).Value = msuserprofile.Active;
                    command.Parameters.Add("@CB", SqlDbType.VarChar).Value = msuserprofile.CB;
                    command.Parameters.Add("@CO", SqlDbType.DateTime).Value = msuserprofile.CO;
                    command.Parameters.Add("@MB", SqlDbType.VarChar).Value = msuserprofile.MB;
                    command.Parameters.Add("@MO", SqlDbType.DateTime).Value = msuserprofile.MO;
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

        public void Update(MS_USER_PROFILE msuserprofile)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE MS_USER_PROFILE SET Description = @Description, Active = @Active, MB = @MB, MO = @MO WHERE UserProfileId = @UserProfileId ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@UserProfileId", SqlDbType.VarChar).Value = msuserprofile.UserProfileId;
                    command.Parameters.Add("@Description", SqlDbType.VarChar).Value = msuserprofile.Description;
                    command.Parameters.Add("@Active", SqlDbType.VarChar).Value = msuserprofile.Active;
                    command.Parameters.Add("@MB", SqlDbType.VarChar).Value = msuserprofile.MB;
                    command.Parameters.Add("@MO", SqlDbType.DateTime).Value = msuserprofile.MO;
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

        public void DeleteByKey(String UserProfileId)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM MS_USER_PROFILE  WHERE UserProfileId = @UserProfileId"), CnString))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@UserProfileId", UserProfileId);
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM MS_USER_PROFILE WHERE {0} ", Where), CnString))
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
