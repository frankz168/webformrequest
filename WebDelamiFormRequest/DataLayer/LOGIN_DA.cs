using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebDelamiFormRequest.Domain;

namespace WebDelamiFormRequest.DataLayer
{
    public class LOGIN_DA
    {
        string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public List<MS_USER> getMsUserLogin(string userName, string password)
        {
            List<MS_USER> listMsUser = new List<MS_USER>();
            try
            {
                SqlConnection Connection = new SqlConnection(conString);
                using (SqlCommand command = new SqlCommand("SELECT * FROM MS_USER WHERE USERNAME = @userName AND PASSWORD = @password", Connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@userName", userName));
                    command.Parameters.Add(new SqlParameter("@password", password));
                    Connection.Open();

                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        MS_USER item = new MS_USER();
                    
                        item.ID = reader.GetInt64(0);
                        item.ID_DEPT = reader.GetString(1);
                        item.USERNAME = reader.GetString(2);
                        item.PASSWORD = reader.GetString(3);
                        item.DEPT = reader.IsDBNull(reader.GetOrdinal("DEPT")) ? "" : reader.GetString(4);
                        item.EMAIL = reader.IsDBNull(reader.GetOrdinal("EMAIL")) ? "" : reader.GetString(5);
                        item.CREATED_BY = reader.IsDBNull(reader.GetOrdinal("CREATED_BY")) ? "" : reader.GetString(6);
                        item.CREATED_DATE = reader.IsDBNull(reader.GetOrdinal("CREATED_DATE")) ? (DateTime?)null : reader.GetDateTime(7);
                        item.STATUS = reader.IsDBNull(reader.GetOrdinal("STATUS")) ? false : reader.GetBoolean(8);
                        item.FORGOT_PASSWORD = reader.IsDBNull(reader.GetOrdinal("FORGOT_PASSWORD")) ? "" : reader.GetString(9);
                        item.FORGOT_PASSWORD_TOKEN = reader.IsDBNull(reader.GetOrdinal("FORGOT_PASSWORD_TOKEN")) ? "" : reader.GetString(10);
                        item.LAST_PASSWORD_CHANGE = reader.IsDBNull(reader.GetOrdinal("LAST_PASSWORD_CHANGE")) ? (DateTime?)null : reader.GetDateTime(11);
                        item.KD_BRAND = reader.IsDBNull(reader.GetOrdinal("KD_BRAND")) ? "" : reader.GetString(12);
                        item.KD_JABATAN = reader.IsDBNull(reader.GetOrdinal("KD_JABATAN")) ? "" : reader.GetString(13);
                        item.FULL_NAME = reader.IsDBNull(reader.GetOrdinal("FULL_NAME")) ? "" : reader.GetString(14);
                        item.UserProfileId = reader.IsDBNull(reader.GetOrdinal("UserProfileId")) ? "" : reader.GetString(15);
                        listMsUser.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listMsUser;
        }

        public void changePass(MS_USER user)
        {
            SqlConnection Connection = new SqlConnection(conString);
            try
            {
                string query = String.Format("update MS_USER set PASSWORD = @password where USERNAME = @username ");
                Connection.Open();
                using (SqlCommand command = new SqlCommand(query, Connection))
                {
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = user.PASSWORD;
                    command.Parameters.Add("@username", SqlDbType.VarChar).Value = user.USERNAME;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

    }
}