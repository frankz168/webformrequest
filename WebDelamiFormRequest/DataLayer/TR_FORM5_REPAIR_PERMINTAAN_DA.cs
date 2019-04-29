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
    public class TR_FORM5_REPAIR_PERMINTAAN_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM5_REPAIR_PERMINTAAN"), CnString))
            {
                command.CommandType = CommandType.Text;
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(String ID_PERBAIKAN)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM5_REPAIR_PERMINTAAN WHERE ID_PERBAIKAN = @ID_PERBAIKAN"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@ID_PERBAIKAN", ID_PERBAIKAN);
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM5_REPAIR_PERMINTAAN WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataFilterDistinct(String Where)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT DISTINCT NO_FORM, PIC FROM TR_FORM5_REPAIR_PERMINTAAN WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(TR_FORM5_REPAIR_PERMINTAAN trform5repairpermintaan)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO TR_FORM5_REPAIR_PERMINTAAN(NO_FORM, NO_PERMINTAAN, PERMINTAAN_PERBAIKAN, PERMINTAAN_PERBAIKAN_2, PIC, COMPLETE_DATE, ACTUAL_FINISH_DATE, BUDGET, UPLOAD_FILE) VALUES (@NO_FORM, @NO_PERMINTAAN, @PERMINTAAN_PERBAIKAN, @PERMINTAAN_PERBAIKAN_2, @PIC, @COMPLETE_DATE, @ACTUAL_FINISH_DATE, @BUDGET, @UPLOAD_FILE)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    //command.Parameters.Add("@ID_PERBAIKAN", SqlDbType.Int).Value = trform5repairpermintaan.ID_PERBAIKAN;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repairpermintaan.NO_FORM;
                    command.Parameters.Add("@NO_PERMINTAAN", SqlDbType.VarChar).Value = trform5repairpermintaan.NO_PERMINTAAN;
                    command.Parameters.Add("@PERMINTAAN_PERBAIKAN", SqlDbType.VarChar).Value = trform5repairpermintaan.PERMINTAAN_PERBAIKAN;
                    command.Parameters.Add("@PERMINTAAN_PERBAIKAN_2", SqlDbType.VarChar).Value = trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2;
                    command.Parameters.Add("@PIC", SqlDbType.VarChar).Value = trform5repairpermintaan.PIC;
                    command.Parameters.Add("@COMPLETE_DATE", SqlDbType.DateTime).Value = trform5repairpermintaan.COMPLETE_DATE;
                    command.Parameters.Add("@ACTUAL_FINISH_DATE", SqlDbType.DateTime).Value = trform5repairpermintaan.ACTUAL_FINISH_DATE;
                    command.Parameters.Add("@BUDGET", SqlDbType.Decimal).Value = trform5repairpermintaan.BUDGET;
                    command.Parameters.Add("@UPLOAD_FILE", SqlDbType.VarChar).Value = trform5repairpermintaan.UPLOAD_FILE;
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

        public void Update(TR_FORM5_REPAIR_PERMINTAAN trform5repairpermintaan)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM5_REPAIR_PERMINTAAN SET NO_FORM = @NO_FORM, NO_PERMINTAAN = @NO_PERMINTAAN, PERMINTAAN_PERBAIKAN = @PERMINTAAN_PERBAIKAN, PERMINTAAN_PERBAIKAN_2 = @PERMINTAAN_PERBAIKAN_2, PIC = @PIC, COMPLETE_DATE = @COMPLETE_DATE, ACTUAL_FINISH_DATE = @ACTUAL_FINISH_DATE, BUDGET = @BUDGET, UPLOAD_FILE = @UPLOAD_FILE WHERE ID_PERBAIKAN = @ID_PERBAIKAN ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID_PERBAIKAN", SqlDbType.Int).Value = trform5repairpermintaan.ID_PERBAIKAN;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repairpermintaan.NO_FORM;
                    command.Parameters.Add("@NO_PERMINTAAN", SqlDbType.VarChar).Value = trform5repairpermintaan.NO_PERMINTAAN;
                    command.Parameters.Add("@PERMINTAAN_PERBAIKAN", SqlDbType.VarChar).Value = trform5repairpermintaan.PERMINTAAN_PERBAIKAN;
                    command.Parameters.Add("@PERMINTAAN_PERBAIKAN_2", SqlDbType.VarChar).Value = trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2;
                    command.Parameters.Add("@PIC", SqlDbType.VarChar).Value = trform5repairpermintaan.PIC;
                    command.Parameters.Add("@COMPLETE_DATE", SqlDbType.DateTime).Value = trform5repairpermintaan.COMPLETE_DATE;
                    command.Parameters.Add("@ACTUAL_FINISH_DATE", SqlDbType.DateTime).Value = trform5repairpermintaan.ACTUAL_FINISH_DATE;
                    command.Parameters.Add("@BUDGET", SqlDbType.Decimal).Value = trform5repairpermintaan.BUDGET;
                    command.Parameters.Add("@UPLOAD_FILE", SqlDbType.VarChar).Value = trform5repairpermintaan.UPLOAD_FILE;
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

        public void UpdateProject(TR_FORM5_REPAIR_PERMINTAAN trform5repairpermintaan)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM5_REPAIR_PERMINTAAN SET NO_FORM = @NO_FORM, PIC = @PIC, COMPLETE_DATE = @COMPLETE_DATE, BUDGET = @BUDGET WHERE PERMINTAAN_PERBAIKAN = @PERMINTAAN_PERBAIKAN");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@PERMINTAAN_PERBAIKAN", SqlDbType.VarChar).Value = trform5repairpermintaan.PERMINTAAN_PERBAIKAN;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repairpermintaan.NO_FORM;
                    //command.Parameters.Add("@NO_PERMINTAAN", SqlDbType.VarChar).Value = trform5repairpermintaan.NO_PERMINTAAN;
                    command.Parameters.Add("@PIC", SqlDbType.VarChar).Value = trform5repairpermintaan.PIC;
                    command.Parameters.Add("@COMPLETE_DATE", SqlDbType.DateTime).Value = trform5repairpermintaan.COMPLETE_DATE;
                    command.Parameters.Add("@BUDGET", SqlDbType.Decimal).Value = trform5repairpermintaan.BUDGET;
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

        public void UpdateActualFinishDate(TR_FORM5_REPAIR_PERMINTAAN trform5repairpermintaan)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM5_REPAIR_PERMINTAAN SET NO_FORM = @NO_FORM, ACTUAL_FINISH_DATE = @ACTUAL_FINISH_DATE, MODIFIED_BY = @MODIFIED_BY, MODIFIED_ON = @MODIFIED_ON WHERE PERMINTAAN_PERBAIKAN_2 = @PERMINTAAN_PERBAIKAN_2");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    //command.Parameters.Add("@ID_PERBAIKAN", SqlDbType.Int).Value = trform5repairpermintaan.ID_PERBAIKAN;
                    command.Parameters.Add("@PERMINTAAN_PERBAIKAN_2", SqlDbType.VarChar).Value = trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repairpermintaan.NO_FORM;
                    command.Parameters.Add("@ACTUAL_FINISH_DATE", SqlDbType.DateTime).Value = trform5repairpermintaan.ACTUAL_FINISH_DATE;
                    command.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar).Value = trform5repairpermintaan.MODIFIED_BY;
                    command.Parameters.Add("@MODIFIED_ON", SqlDbType.DateTime).Value = trform5repairpermintaan.MODIFIED_ON;
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

        public void DeleteByKey(String ID_PERBAIKAN)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM TR_FORM5_REPAIR_PERMINTAAN  WHERE ID_PERBAIKAN = @ID_PERBAIKAN"), CnString))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@ID_PERBAIKAN", ID_PERBAIKAN);
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM TR_FORM5_REPAIR_PERMINTAAN WHERE {0} ", Where), CnString))
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
