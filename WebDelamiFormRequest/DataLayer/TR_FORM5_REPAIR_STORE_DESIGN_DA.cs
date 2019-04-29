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
    public class TR_FORM5_REPAIR_STORE_DESIGN_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM5_REPAIR_STORE_DESIGN"), CnString))
            {
                command.CommandType = CommandType.Text;
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(String NO_FORM_SD)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM5_REPAIR_STORE_DESIGN WHERE NO_FORM_SD = @NO_FORM_SD"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@NO_FORM_SD", NO_FORM_SD);
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM5_REPAIR_STORE_DESIGN WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(TR_FORM5_REPAIR_STORE_DESIGN trform5repairstoredesign)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO TR_FORM5_REPAIR_STORE_DESIGN(NO_FORM_SD, NO_FORM, REQUIRED_DATE, REQUESTER_NAME, ID_DEPT, KD_BRAND, LOCATION, CONCEPT, STORE_TYPE, SQM_1, SQM_2, RFR_LAMPIRAN1, RFR_LAMPIRAN2, RFR_LAMPIRAN3, RFR_LAMPIRAN4, DESCRIPTION, DIBUAT_SD, TGL_DIBUAT_SD, STATUS, USER_CURRENT, NEXT_USER, URUTAN_USER_CURRENT, URUTAN_NEXT_USER) VALUES (@NO_FORM_SD, @NO_FORM, @REQUIRED_DATE, @REQUESTER_NAME, @ID_DEPT, @KD_BRAND, @LOCATION, @CONCEPT, @STORE_TYPE, @SQM_1, @SQM_2, @RFR_LAMPIRAN1, @RFR_LAMPIRAN2, @RFR_LAMPIRAN3, @RFR_LAMPIRAN4, @DESCRIPTION, @DIBUAT_SD, @TGL_DIBUAT_SD, @STATUS, @USER_CURRENT, @NEXT_USER, @URUTAN_USER_CURRENT, @URUTAN_NEXT_USER)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM_SD", SqlDbType.VarChar).Value = trform5repairstoredesign.NO_FORM_SD;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repairstoredesign.NO_FORM;
                    command.Parameters.Add("@REQUIRED_DATE", SqlDbType.DateTime).Value = trform5repairstoredesign.REQUIRED_DATE;
                    command.Parameters.Add("@REQUESTER_NAME", SqlDbType.VarChar).Value = trform5repairstoredesign.REQUESTER_NAME;
                    command.Parameters.Add("@ID_DEPT", SqlDbType.VarChar).Value = trform5repairstoredesign.ID_DEPT;
                    command.Parameters.Add("@KD_BRAND", SqlDbType.VarChar).Value = trform5repairstoredesign.KD_BRAND;
                    command.Parameters.Add("@LOCATION", SqlDbType.VarChar).Value = trform5repairstoredesign.LOCATION;
                    command.Parameters.Add("@CONCEPT", SqlDbType.VarChar).Value = trform5repairstoredesign.CONCEPT;
                    command.Parameters.Add("@STORE_TYPE", SqlDbType.VarChar).Value = trform5repairstoredesign.STORE_TYPE;
                    command.Parameters.Add("@SQM_1", SqlDbType.VarChar).Value = trform5repairstoredesign.SQM_1;
                    command.Parameters.Add("@SQM_2", SqlDbType.VarChar).Value = trform5repairstoredesign.SQM_2;
                    command.Parameters.Add("@RFR_LAMPIRAN1", SqlDbType.VarChar).Value = trform5repairstoredesign.RFR_LAMPIRAN1;
                    command.Parameters.Add("@RFR_LAMPIRAN2", SqlDbType.VarChar).Value = trform5repairstoredesign.RFR_LAMPIRAN2;
                    command.Parameters.Add("@RFR_LAMPIRAN3", SqlDbType.VarChar).Value = trform5repairstoredesign.RFR_LAMPIRAN3;
                    command.Parameters.Add("@RFR_LAMPIRAN4", SqlDbType.VarChar).Value = trform5repairstoredesign.RFR_LAMPIRAN4;
                    command.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = trform5repairstoredesign.DESCRIPTION;
                    command.Parameters.Add("@DIBUAT_SD", SqlDbType.VarChar).Value = trform5repairstoredesign.DIBUAT_SD;
                    command.Parameters.Add("@TGL_DIBUAT_SD", SqlDbType.DateTime).Value = trform5repairstoredesign.TGL_DIBUAT_SD;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repairstoredesign.STATUS;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repairstoredesign.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repairstoredesign.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repairstoredesign.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repairstoredesign.URUTAN_NEXT_USER;
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

        public void Update(TR_FORM5_REPAIR_STORE_DESIGN trform5repairstoredesign)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM5_REPAIR_STORE_DESIGN SET NO_FORM = @NO_FORM, REQUIRED_DATE = @REQUIRED_DATE, REQUESTER_NAME = @REQUESTER_NAME, ID_DEPT = @ID_DEPT, KD_BRAND = @KD_BRAND, LOCATION = @LOCATION, CONCEPT = @CONCEPT, STORE_TYPE = @STORE_TYPE, SQM_1 = @SQM_1, SQM_2 = @SQM_2, RFR_LAMPIRAN1 = @RFR_LAMPIRAN1, RFR_LAMPIRAN2 = @RFR_LAMPIRAN2, RFR_LAMPIRAN3 = @RFR_LAMPIRAN3, RFR_LAMPIRAN4 = @RFR_LAMPIRAN4, DESCRIPTION = @DESCRIPTION, DIBUAT_SD = @DIBUAT_SD, TGL_DIBUAT_SD = @TGL_DIBUAT_SD, STATUS = @STATUS, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM_SD = @NO_FORM_SD ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM_SD", SqlDbType.VarChar).Value = trform5repairstoredesign.NO_FORM_SD;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repairstoredesign.NO_FORM;
                    command.Parameters.Add("@REQUIRED_DATE", SqlDbType.DateTime).Value = trform5repairstoredesign.REQUIRED_DATE;
                    command.Parameters.Add("@REQUESTER_NAME", SqlDbType.VarChar).Value = trform5repairstoredesign.REQUESTER_NAME;
                    command.Parameters.Add("@ID_DEPT", SqlDbType.VarChar).Value = trform5repairstoredesign.ID_DEPT;
                    command.Parameters.Add("@KD_BRAND", SqlDbType.VarChar).Value = trform5repairstoredesign.KD_BRAND;
                    command.Parameters.Add("@LOCATION", SqlDbType.VarChar).Value = trform5repairstoredesign.LOCATION;
                    command.Parameters.Add("@CONCEPT", SqlDbType.VarChar).Value = trform5repairstoredesign.CONCEPT;
                    command.Parameters.Add("@STORE_TYPE", SqlDbType.VarChar).Value = trform5repairstoredesign.STORE_TYPE;
                    command.Parameters.Add("@SQM_1", SqlDbType.VarChar).Value = trform5repairstoredesign.SQM_1;
                    command.Parameters.Add("@SQM_2", SqlDbType.VarChar).Value = trform5repairstoredesign.SQM_2;
                    command.Parameters.Add("@RFR_LAMPIRAN1", SqlDbType.VarChar).Value = trform5repairstoredesign.RFR_LAMPIRAN1;
                    command.Parameters.Add("@RFR_LAMPIRAN2", SqlDbType.VarChar).Value = trform5repairstoredesign.RFR_LAMPIRAN2;
                    command.Parameters.Add("@RFR_LAMPIRAN3", SqlDbType.VarChar).Value = trform5repairstoredesign.RFR_LAMPIRAN3;
                    command.Parameters.Add("@RFR_LAMPIRAN4", SqlDbType.VarChar).Value = trform5repairstoredesign.RFR_LAMPIRAN4;
                    command.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = trform5repairstoredesign.DESCRIPTION;
                    command.Parameters.Add("@DIBUAT_SD", SqlDbType.VarChar).Value = trform5repairstoredesign.DIBUAT_SD;
                    command.Parameters.Add("@TGL_DIBUAT_SD", SqlDbType.DateTime).Value = trform5repairstoredesign.TGL_DIBUAT_SD;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repairstoredesign.STATUS;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform5repairstoredesign.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repairstoredesign.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repairstoredesign.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repairstoredesign.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repairstoredesign.URUTAN_NEXT_USER;
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

        public void UpdateDibuat(TR_FORM5_REPAIR_STORE_DESIGN trform5repairstoredesign)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR_STORE_DESIGN SET STATUS = @STATUS, DIBUAT_SD = @DIBUAT_SD, TGL_DIBUAT_SD = @TGL_DIBUAT_SD, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repairstoredesign.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repairstoredesign.STATUS;
                    command.Parameters.Add("@DIBUAT_SD", SqlDbType.VarChar).Value = trform5repairstoredesign.DIBUAT_SD;
                    command.Parameters.Add("@TGL_DIBUAT_SD", SqlDbType.DateTime).Value = trform5repairstoredesign.TGL_DIBUAT_SD;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repairstoredesign.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repairstoredesign.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repairstoredesign.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repairstoredesign.URUTAN_NEXT_USER;

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

        public void DeleteByKey(String NO_FORM_SD)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM TR_FORM5_REPAIR_STORE_DESIGN  WHERE NO_FORM_SD = @NO_FORM_SD"), CnString))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@NO_FORM_SD", NO_FORM_SD);
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM TR_FORM5_REPAIR_STORE_DESIGN WHERE {0} ", Where), CnString))
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
