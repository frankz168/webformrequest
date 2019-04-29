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
    public class TR_FORM5_REPAIR_STORE_DESIGN_DETAIL_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM5_REPAIR_STORE_DESIGN_DETAIL"), CnString))
            {
                command.CommandType = CommandType.Text;
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(String ID_SD)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM5_REPAIR_STORE_DESIGN_DETAIL WHERE ID_SD = @ID_SD"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@ID_SD", ID_SD);
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM5_REPAIR_STORE_DESIGN_DETAIL WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(TR_FORM5_REPAIR_STORE_DESIGN_DETAIL trform5repairstoredesigndetail)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO TR_FORM5_REPAIR_STORE_DESIGN_DETAIL(NO_FORM_SD, TYPE_OF_WORK, PROJECT_REQUEST, PROVIDE_INFORMATION) VALUES (@NO_FORM_SD, @TYPE_OF_WORK, @PROJECT_REQUEST, @PROVIDE_INFORMATION)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID_SD", SqlDbType.Int).Value = trform5repairstoredesigndetail.ID_SD;
                    command.Parameters.Add("@NO_FORM_SD", SqlDbType.VarChar).Value = trform5repairstoredesigndetail.NO_FORM_SD;
                    command.Parameters.Add("@TYPE_OF_WORK", SqlDbType.VarChar).Value = trform5repairstoredesigndetail.TYPE_OF_WORK;
                    command.Parameters.Add("@PROJECT_REQUEST", SqlDbType.VarChar).Value = trform5repairstoredesigndetail.PROJECT_REQUEST;
                    command.Parameters.Add("@PROVIDE_INFORMATION", SqlDbType.VarChar).Value = trform5repairstoredesigndetail.PROVIDE_INFORMATION;
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

        public void Update(TR_FORM5_REPAIR_STORE_DESIGN_DETAIL trform5repairstoredesigndetail)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM5_REPAIR_STORE_DESIGN_DETAIL SET NO_FORM_SD = @NO_FORM_SD, TYPE_OF_WORK = @TYPE_OF_WORK, PROJECT_REQUEST = @PROJECT_REQUEST, PROVIDE_INFORMATION = @PROVIDE_INFORMATION WHERE ID_SD = @ID_SD ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID_SD", SqlDbType.Int).Value = trform5repairstoredesigndetail.ID_SD;
                    command.Parameters.Add("@NO_FORM_SD", SqlDbType.VarChar).Value = trform5repairstoredesigndetail.NO_FORM_SD;
                    command.Parameters.Add("@TYPE_OF_WORK", SqlDbType.VarChar).Value = trform5repairstoredesigndetail.TYPE_OF_WORK;
                    command.Parameters.Add("@PROJECT_REQUEST", SqlDbType.VarChar).Value = trform5repairstoredesigndetail.PROJECT_REQUEST;
                    command.Parameters.Add("@PROVIDE_INFORMATION", SqlDbType.VarChar).Value = trform5repairstoredesigndetail.PROVIDE_INFORMATION;
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

        public void DeleteByKey(String ID_SD)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM TR_FORM5_REPAIR_STORE_DESIGN_DETAIL  WHERE ID_SD = @ID_SD"), CnString))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@ID_SD", ID_SD);
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM TR_FORM5_REPAIR_STORE_DESIGN_DETAIL WHERE {0} ", Where), CnString))
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
