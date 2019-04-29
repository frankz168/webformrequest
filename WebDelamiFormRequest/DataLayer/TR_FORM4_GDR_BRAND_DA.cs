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
    public class TR_FORM4_GDR_BRAND_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM4_GDR_BRAND"), CnString))
            {
                command.CommandType = CommandType.Text;
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(String ID_BRAND_REPAIR)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM4_GDR_BRAND WHERE ID_BRAND_REPAIR = @ID_BRAND_REPAIR"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@ID_BRAND_REPAIR", ID_BRAND_REPAIR);
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM4_GDR_BRAND WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(TR_FORM4_GDR_BRAND trform4gdrbrand)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO TR_FORM4_GDR_BRAND(NO_FORM, KD_BRAND, BRAND) VALUES (@NO_FORM, @KD_BRAND, @BRAND)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID_BRAND_REPAIR", SqlDbType.Int).Value = trform4gdrbrand.ID_BRAND_REPAIR;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform4gdrbrand.NO_FORM;
                    command.Parameters.Add("@KD_BRAND", SqlDbType.VarChar).Value = trform4gdrbrand.KD_BRAND;
                    command.Parameters.Add("@BRAND", SqlDbType.VarChar).Value = trform4gdrbrand.BRAND;
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


        public void Update(TR_FORM4_GDR_BRAND trform4gdrbrand)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM4_GDR_BRAND SET NO_FORM = @NO_FORM, KD_BRAND = @KD_BRAND, BRAND = @BRAND WHERE ID_BRAND_REPAIR = @ID_BRAND_REPAIR ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID_BRAND_REPAIR", SqlDbType.Int).Value = trform4gdrbrand.ID_BRAND_REPAIR;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform4gdrbrand.NO_FORM;
                    command.Parameters.Add("@KD_BRAND", SqlDbType.VarChar).Value = trform4gdrbrand.KD_BRAND;
                    command.Parameters.Add("@BRAND", SqlDbType.VarChar).Value = trform4gdrbrand.BRAND;
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


        public void DeleteByKey(String ID_BRAND_REPAIR)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM TR_FORM4_GDR_BRAND  WHERE ID_BRAND_REPAIR = @ID_BRAND_REPAIR"), CnString))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@ID_BRAND_REPAIR", ID_BRAND_REPAIR);
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM TR_FORM4_GDR_BRAND WHERE {0} ", Where), CnString))
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
