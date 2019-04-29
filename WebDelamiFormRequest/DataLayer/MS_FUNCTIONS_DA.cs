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
    public class MS_FUNCTIONS_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT FunctionId, Description, ParentFunctionId, Active, CB, CO, MB, MO, A FROM MS_FUNCTIONS"), CnString))
            {
                command.CommandType = CommandType.Text;
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(String FunctionId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT FunctionId, Description, ParentFunctionId, Active, CB, CO, MB, MO, A FROM MS_FUNCTIONS WHERE FunctionId = @FunctionId"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@FunctionId", FunctionId);
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT FunctionId, Description, ParentFunctionId, Active, CB, CO, MB, MO, A FROM MS_FUNCTIONS WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(MS_FUNCTIONS msfunctions)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO MS_FUNCTIONS(FunctionId, Description, ParentFunctionId, Active, CB, CO, MB, MO, A) VALUES (@FunctionId, @Description, @ParentFunctionId, @Active, @CB, @CO, @MB, @MO, @A)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@FunctionId", SqlDbType.VarChar).Value = msfunctions.FunctionId;
                    command.Parameters.Add("@Description", SqlDbType.VarChar).Value = msfunctions.Description;
                    command.Parameters.Add("@ParentFunctionId", SqlDbType.VarChar).Value = msfunctions.ParentFunctionId;
                    command.Parameters.Add("@Active", SqlDbType.VarChar).Value = msfunctions.Active;
                    command.Parameters.Add("@CB", SqlDbType.VarChar).Value = msfunctions.CB;
                    command.Parameters.Add("@CO", SqlDbType.DateTime).Value = msfunctions.CO;
                    command.Parameters.Add("@MB", SqlDbType.VarChar).Value = msfunctions.MB;
                    command.Parameters.Add("@MO", SqlDbType.DateTime).Value = msfunctions.MO;
                    command.Parameters.Add("@A", SqlDbType.VarChar).Value = msfunctions.A;
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


        public void Update(MS_FUNCTIONS msfunctions)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE MS_FUNCTIONS SET Description = @Description, ParentFunctionId = @ParentFunctionId, Active = @Active, CB = @CB, CO = @CO, MB = @MB, MO = @MO, A = @A WHERE FunctionId = @FunctionId ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@FunctionId", SqlDbType.VarChar).Value = msfunctions.FunctionId;
                    command.Parameters.Add("@Description", SqlDbType.VarChar).Value = msfunctions.Description;
                    command.Parameters.Add("@ParentFunctionId", SqlDbType.VarChar).Value = msfunctions.ParentFunctionId;
                    command.Parameters.Add("@Active", SqlDbType.VarChar).Value = msfunctions.Active;
                    command.Parameters.Add("@CB", SqlDbType.VarChar).Value = msfunctions.CB;
                    command.Parameters.Add("@CO", SqlDbType.DateTime).Value = msfunctions.CO;
                    command.Parameters.Add("@MB", SqlDbType.VarChar).Value = msfunctions.MB;
                    command.Parameters.Add("@MO", SqlDbType.DateTime).Value = msfunctions.MO;
                    command.Parameters.Add("@A", SqlDbType.VarChar).Value = msfunctions.A;
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


        public void DeleteByKey(String FunctionId)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM MS_FUNCTIONS  WHERE FunctionId = @FunctionId"), CnString))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@FunctionId", FunctionId);
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM MS_FUNCTIONS WHERE {0} ", Where), CnString))
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
