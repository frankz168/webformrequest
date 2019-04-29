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
    public class MS_USER_PROFILE_TO_FUNCTIONS_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT Id, UserProfileId, FunctionId, Permission, CB, CO, MB, MO FROM MS_USER_PROFILE_TO_FUNCTIONS"), CnString))
            {
                command.CommandType = CommandType.Text;
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(String Id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT Id, UserProfileId, FunctionId, Permission, CB, CO, MB, MO FROM MS_USER_PROFILE_TO_FUNCTIONS WHERE Id = @Id"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@Id", Id);
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT Id, UserProfileId, FunctionId, Permission, CB, CO, MB, MO FROM MS_USER_PROFILE_TO_FUNCTIONS WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(MS_USER_PROFILE_TO_FUNCTIONS msuserprofiletofunctions)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO MS_USER_PROFILE_TO_FUNCTIONS(UserProfileId, FunctionId, Permission, CB, CO, MB, MO) VALUES (@UserProfileId, @FunctionId, @Permission, @CB, @CO, @MB, @MO)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = msuserprofiletofunctions.Id;
                    command.Parameters.Add("@UserProfileId", SqlDbType.VarChar).Value = msuserprofiletofunctions.UserProfileId;
                    command.Parameters.Add("@FunctionId", SqlDbType.VarChar).Value = msuserprofiletofunctions.FunctionId;
                    command.Parameters.Add("@Permission", SqlDbType.VarChar).Value = msuserprofiletofunctions.Permission;
                    command.Parameters.Add("@CB", SqlDbType.VarChar).Value = msuserprofiletofunctions.CB;
                    command.Parameters.Add("@CO", SqlDbType.DateTime).Value = msuserprofiletofunctions.CO;
                    command.Parameters.Add("@MB", SqlDbType.VarChar).Value = msuserprofiletofunctions.MB;
                    command.Parameters.Add("@MO", SqlDbType.DateTime).Value = msuserprofiletofunctions.MO;
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

        public void Update(MS_USER_PROFILE_TO_FUNCTIONS msuserprofiletofunctions)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE MS_USER_PROFILE_TO_FUNCTIONS SET Permission = @Permission, MB = @MB, MO = @MO WHERE Id = @Id ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = msuserprofiletofunctions.Id;
                    //command.Parameters.Add("@UserProfileId", SqlDbType.VarChar).Value = msuserprofiletofunctions.UserProfileId;
                    //command.Parameters.Add("@FunctionId", SqlDbType.VarChar).Value = msuserprofiletofunctions.FunctionId;
                    command.Parameters.Add("@Permission", SqlDbType.VarChar).Value = msuserprofiletofunctions.Permission;
                    //command.Parameters.Add("@CB", SqlDbType.VarChar).Value = msuserprofiletofunctions.CB;
                    //command.Parameters.Add("@CO", SqlDbType.DateTime).Value = msuserprofiletofunctions.CO;
                    command.Parameters.Add("@MB", SqlDbType.VarChar).Value = msuserprofiletofunctions.MB;
                    command.Parameters.Add("@MO", SqlDbType.DateTime).Value = msuserprofiletofunctions.MO;
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

        public void DeleteByKey(String Id)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM MS_USER_PROFILE_TO_FUNCTIONS  WHERE Id = @Id"), CnString))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@Id", Id);
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM MS_USER_PROFILE_TO_FUNCTIONS WHERE {0} ", Where), CnString))
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
