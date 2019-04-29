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
    public class TR_FORM5_REPAIR_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM5_REPAIR"), CnString))
            {
                command.CommandType = CommandType.Text;
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(String NO_FORM)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM5_REPAIR WHERE NO_FORM = @NO_FORM"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@NO_FORM", NO_FORM);
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM5_REPAIR WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO TR_FORM5_REPAIR(NO_FORM, KODE_FORM, JENIS, TGL_REQUEST, TGL_REQUIRED, ID_DEPT, KD_BRAND, PIC_REQUESTER, KETERANGAN, OVER_BUDGET, DIBUAT, TGL_DIBUAT, MENYETUJUI1, TGL_MENYETUJUI1, DITERIMA_1, TGL_DITERIMA_1, DITERIMA_2, TGL_DITERIMA_2, DITERIMA_3, TGL_DITERIMA_3, DITERIMA_4, TGL_DITERIMA_4, DITERIMA_LAIN_1, TGL_DITERIMA_LAIN_1, DITERIMA_LAIN_2, TGL_DITERIMA_LAIN_2, STATUS, REVISI, USER_CURRENT, NEXT_USER, URUTAN_USER_CURRENT, URUTAN_NEXT_USER, OVER_BUDGET_REQUEST, REQUEST_FOR) VALUES (@NO_FORM, @KODE_FORM, @JENIS, @TGL_REQUEST, @TGL_REQUIRED, @ID_DEPT, @KD_BRAND, @PIC_REQUESTER, @KETERANGAN, @OVER_BUDGET, @DIBUAT, @TGL_DIBUAT, @MENYETUJUI1, @TGL_MENYETUJUI1, @DITERIMA_1, @TGL_DITERIMA_1, @DITERIMA_2, @TGL_DITERIMA_2, @DITERIMA_3, @TGL_DITERIMA_3, @DITERIMA_4, @TGL_DITERIMA_4, @DITERIMA_LAIN_1, @TGL_DITERIMA_LAIN_1, @DITERIMA_LAIN_2, @TGL_DITERIMA_LAIN_2, @STATUS, @REVISI, @USER_CURRENT, @NEXT_USER, @URUTAN_USER_CURRENT, @URUTAN_NEXT_USER, @OVER_BUDGET_REQUEST, @REQUEST_FOR)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = trform5repair.KODE_FORM;
                    command.Parameters.Add("@JENIS", SqlDbType.VarChar).Value = trform5repair.JENIS;
                    command.Parameters.Add("@TGL_REQUEST", SqlDbType.DateTime).Value = trform5repair.TGL_REQUEST;
                    command.Parameters.Add("@TGL_REQUIRED", SqlDbType.DateTime).Value = trform5repair.TGL_REQUIRED;
                    command.Parameters.Add("@ID_DEPT", SqlDbType.VarChar).Value = trform5repair.ID_DEPT;
                    command.Parameters.Add("@KD_BRAND", SqlDbType.VarChar).Value = trform5repair.KD_BRAND;
                    command.Parameters.Add("@PIC_REQUESTER", SqlDbType.VarChar).Value = trform5repair.PIC_REQUESTER;
                    command.Parameters.Add("@KETERANGAN", SqlDbType.VarChar).Value = trform5repair.KETERANGAN;
                    command.Parameters.Add("@OVER_BUDGET", SqlDbType.Decimal).Value = trform5repair.OVER_BUDGET;
                    command.Parameters.Add("@DIBUAT", SqlDbType.VarChar).Value = trform5repair.DIBUAT;
                    command.Parameters.Add("@TGL_DIBUAT", SqlDbType.DateTime).Value = trform5repair.TGL_DIBUAT;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform5repair.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform5repair.TGL_MENYETUJUI1;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform5repair.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_1;
                    command.Parameters.Add("@DITERIMA_2", SqlDbType.VarChar).Value = trform5repair.DITERIMA_2;
                    command.Parameters.Add("@TGL_DITERIMA_2", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_2;
                    command.Parameters.Add("@DITERIMA_3", SqlDbType.VarChar).Value = trform5repair.DITERIMA_3;
                    command.Parameters.Add("@TGL_DITERIMA_3", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_3;
                    command.Parameters.Add("@DITERIMA_4", SqlDbType.VarChar).Value = trform5repair.DITERIMA_4;
                    command.Parameters.Add("@TGL_DITERIMA_4", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_4;
                    command.Parameters.Add("@DITERIMA_LAIN_1", SqlDbType.VarChar).Value = trform5repair.DITERIMA_LAIN_1;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_1", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_LAIN_1;
                    command.Parameters.Add("@DITERIMA_LAIN_2", SqlDbType.VarChar).Value = trform5repair.DITERIMA_LAIN_2;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_2", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_LAIN_2;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform5repair.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;
                    command.Parameters.Add("@OVER_BUDGET_REQUEST", SqlDbType.VarChar).Value = trform5repair.OVER_BUDGET_REQUEST;
                    command.Parameters.Add("@REQUEST_FOR", SqlDbType.VarChar).Value = trform5repair.REQUEST_FOR;
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

        public void Update(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM5_REPAIR SET KODE_FORM = @KODE_FORM, JENIS = @JENIS, TGL_REQUEST = @TGL_REQUEST, TGL_REQUIRED = @TGL_REQUIRED, ID_DEPT = @ID_DEPT, PIC_REQUESTER = @PIC_REQUESTER, KETERANGAN = @KETERANGAN, OVER_BUDGET = @OVER_BUDGET, DIBUAT = @DIBUAT, TGL_DIBUAT = @TGL_DIBUAT, MENYETUJUI1 = @MENYETUJUI1, TGL_MENYETUJUI1 = @TGL_MENYETUJUI1, DITERIMA_1 = @DITERIMA_1, TGL_DITERIMA_1 = @TGL_DITERIMA_1, DITERIMA_2 = @DITERIMA_2, TGL_DITERIMA_2 = @TGL_DITERIMA_2, DITERIMA_3 = @DITERIMA_3, TGL_DITERIMA_3 = @TGL_DITERIMA_3, DITERIMA_4 = @DITERIMA_4, TGL_DITERIMA_4 = @TGL_DITERIMA_4, DITERIMA_LAIN_1 = @DITERIMA_LAIN_1, TGL_DITERIMA_LAIN_1 = @TGL_DITERIMA_LAIN_1, DITERIMA_LAIN_2 = @DITERIMA_LAIN_2, TGL_DITERIMA_LAIN_2 = @TGL_DITERIMA_LAIN_2, STATUS = @STATUS, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER, OVER_BUDGET_REQUEST = @OVER_BUDGET_REQUEST, REQUEST_FOR = @REQUEST_FOR WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = trform5repair.KODE_FORM;
                    command.Parameters.Add("@JENIS", SqlDbType.VarChar).Value = trform5repair.JENIS;
                    command.Parameters.Add("@TGL_REQUEST", SqlDbType.DateTime).Value = trform5repair.TGL_REQUEST;
                    command.Parameters.Add("@TGL_REQUIRED", SqlDbType.DateTime).Value = trform5repair.TGL_REQUIRED;
                    command.Parameters.Add("@ID_DEPT", SqlDbType.VarChar).Value = trform5repair.ID_DEPT;
                    //command.Parameters.Add("@KD_BRAND", SqlDbType.VarChar).Value = trform5repair.KD_BRAND;
                    command.Parameters.Add("@PIC_REQUESTER", SqlDbType.VarChar).Value = trform5repair.PIC_REQUESTER;
                    command.Parameters.Add("@KETERANGAN", SqlDbType.VarChar).Value = trform5repair.KETERANGAN;
                    command.Parameters.Add("@OVER_BUDGET", SqlDbType.Decimal).Value = trform5repair.OVER_BUDGET;
                    command.Parameters.Add("@DIBUAT", SqlDbType.VarChar).Value = trform5repair.DIBUAT;
                    command.Parameters.Add("@TGL_DIBUAT", SqlDbType.DateTime).Value = trform5repair.TGL_DIBUAT;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform5repair.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform5repair.TGL_MENYETUJUI1;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform5repair.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_1;
                    command.Parameters.Add("@DITERIMA_2", SqlDbType.VarChar).Value = trform5repair.DITERIMA_2;
                    command.Parameters.Add("@TGL_DITERIMA_2", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_2;
                    command.Parameters.Add("@DITERIMA_3", SqlDbType.VarChar).Value = trform5repair.DITERIMA_3;
                    command.Parameters.Add("@TGL_DITERIMA_3", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_3;
                    command.Parameters.Add("@DITERIMA_4", SqlDbType.VarChar).Value = trform5repair.DITERIMA_4;
                    command.Parameters.Add("@TGL_DITERIMA_4", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_4;
                    command.Parameters.Add("@DITERIMA_LAIN_1", SqlDbType.VarChar).Value = trform5repair.DITERIMA_LAIN_1;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_1", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_LAIN_1;
                    command.Parameters.Add("@DITERIMA_LAIN_2", SqlDbType.VarChar).Value = trform5repair.DITERIMA_LAIN_2;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_2", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_LAIN_2;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform5repair.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;
                    command.Parameters.Add("@OVER_BUDGET_REQUEST", SqlDbType.VarChar).Value = trform5repair.OVER_BUDGET_REQUEST;
                    command.Parameters.Add("@REQUEST_FOR", SqlDbType.VarChar).Value = trform5repair.REQUEST_FOR;
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

        public void UpdateBudget(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM5_REPAIR SET KODE_FORM = @KODE_FORM, JENIS = @JENIS, TGL_REQUEST = @TGL_REQUEST, TGL_REQUIRED = @TGL_REQUIRED, ID_DEPT = @ID_DEPT, PIC_REQUESTER = @PIC_REQUESTER, KETERANGAN = @KETERANGAN, OVER_BUDGET = @OVER_BUDGET, DIBUAT = @DIBUAT, TGL_DIBUAT = @TGL_DIBUAT, DITERIMA_LAIN_1 = @DITERIMA_LAIN_1, TGL_DITERIMA_LAIN_1 = @TGL_DITERIMA_LAIN_1, DITERIMA_LAIN_2 = @DITERIMA_LAIN_2, TGL_DITERIMA_LAIN_2 = @TGL_DITERIMA_LAIN_2, STATUS = @STATUS, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER, OVER_BUDGET_REQUEST = @OVER_BUDGET_REQUEST WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = trform5repair.KODE_FORM;
                    command.Parameters.Add("@JENIS", SqlDbType.VarChar).Value = trform5repair.JENIS;
                    command.Parameters.Add("@TGL_REQUEST", SqlDbType.DateTime).Value = trform5repair.TGL_REQUEST;
                    command.Parameters.Add("@TGL_REQUIRED", SqlDbType.DateTime).Value = trform5repair.TGL_REQUIRED;
                    command.Parameters.Add("@ID_DEPT", SqlDbType.VarChar).Value = trform5repair.ID_DEPT;
                    //command.Parameters.Add("@KD_BRAND", SqlDbType.VarChar).Value = trform5repair.KD_BRAND;
                    command.Parameters.Add("@PIC_REQUESTER", SqlDbType.VarChar).Value = trform5repair.PIC_REQUESTER;
                    command.Parameters.Add("@KETERANGAN", SqlDbType.VarChar).Value = trform5repair.KETERANGAN;
                    command.Parameters.Add("@OVER_BUDGET", SqlDbType.Decimal).Value = trform5repair.OVER_BUDGET;
                    command.Parameters.Add("@DIBUAT", SqlDbType.VarChar).Value = trform5repair.DIBUAT;
                    command.Parameters.Add("@TGL_DIBUAT", SqlDbType.DateTime).Value = trform5repair.TGL_DIBUAT;
                    command.Parameters.Add("@DITERIMA_LAIN_1", SqlDbType.VarChar).Value = trform5repair.DITERIMA_LAIN_1;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_1", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_LAIN_1;
                    command.Parameters.Add("@DITERIMA_LAIN_2", SqlDbType.VarChar).Value = trform5repair.DITERIMA_LAIN_2;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_2", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_LAIN_2;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform5repair.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;
                    command.Parameters.Add("@OVER_BUDGET_REQUEST", SqlDbType.VarChar).Value = trform5repair.OVER_BUDGET_REQUEST;
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

        public void UpdateDibuat(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR SET STATUS = @STATUS, DIBUAT = @DIBUAT, TGL_DIBUAT = @TGL_DIBUAT, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@DIBUAT", SqlDbType.VarChar).Value = trform5repair.DIBUAT;
                    command.Parameters.Add("@TGL_DIBUAT", SqlDbType.DateTime).Value = trform5repair.TGL_DIBUAT;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;

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

        public void UpdateDibuatSD(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR SET STATUS = @STATUS, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;

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

        public void UpdateMenyetujui1(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR SET STATUS = @STATUS, MENYETUJUI1 = @MENYETUJUI1, TGL_MENYETUJUI1 = @TGL_MENYETUJUI1, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform5repair.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform5repair.TGL_MENYETUJUI1;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;
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

        public void UpdateDiterima1(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR SET STATUS = @STATUS, DITERIMA_1 = @DITERIMA_1, TGL_DITERIMA_1 = @TGL_DITERIMA_1, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform5repair.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_1;
                    //command.Parameters.Add("@DITERIMA_2", SqlDbType.VarChar).Value = trform5repair.DITERIMA_2;
                    //command.Parameters.Add("@OVER_BUDGET", SqlDbType.Decimal).Value = trform5repair.OVER_BUDGET;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;

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

        public void UpdateDiterima2(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR SET STATUS = @STATUS, DITERIMA_2 = @DITERIMA_2, TGL_DITERIMA_2 = @TGL_DITERIMA_2, OVER_BUDGET = @OVER_BUDGET, OVER_BUDGET_REQUEST = @OVER_BUDGET_REQUEST, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@DITERIMA_2", SqlDbType.VarChar).Value = trform5repair.DITERIMA_2;
                    command.Parameters.Add("@TGL_DITERIMA_2", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_2;
                    command.Parameters.Add("@OVER_BUDGET", SqlDbType.Decimal).Value = trform5repair.OVER_BUDGET;
                    command.Parameters.Add("@OVER_BUDGET_REQUEST", SqlDbType.VarChar).Value = trform5repair.OVER_BUDGET_REQUEST;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;
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

        public void UpdateDiterima3(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR SET STATUS = @STATUS, DITERIMA_3 = @DITERIMA_3, TGL_DITERIMA_3 = @TGL_DITERIMA_3, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@DITERIMA_3", SqlDbType.VarChar).Value = trform5repair.DITERIMA_3;
                    command.Parameters.Add("@TGL_DITERIMA_3", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_3;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;
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

        public void UpdateDiterima4(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR SET STATUS = @STATUS, DITERIMA_4 = @DITERIMA_4, TGL_DITERIMA_4 = @TGL_DITERIMA_4, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@DITERIMA_4", SqlDbType.VarChar).Value = trform5repair.DITERIMA_4;
                    command.Parameters.Add("@TGL_DITERIMA_4", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_4;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;
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

        public void UpdateRevisiMenyetujui1(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR SET STATUS = @STATUS, MENYETUJUI1 = @MENYETUJUI1, TGL_MENYETUJUI1 = @TGL_MENYETUJUI1, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform5repair.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform5repair.TGL_MENYETUJUI1;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform5repair.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;
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

        public void UpdateRevisiDiterima1(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR SET STATUS = @STATUS, DITERIMA_1 = @DITERIMA_1, TGL_DITERIMA_1 = @TGL_DITERIMA_1, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform5repair.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_1;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform5repair.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;
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

        public void UpdateRevisiDiterima2(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR SET STATUS = @STATUS, DITERIMA_LAIN_2 = @DITERIMA_LAIN_2, TGL_DITERIMA_LAIN_2 = @TGL_DITERIMA_LAIN_2, REVISI = @REVISI, OVER_BUDGET = @OVER_BUDGET, OVER_BUDGET_REQUEST = @OVER_BUDGET_REQUEST, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_2", SqlDbType.VarChar).Value = trform5repair.DITERIMA_LAIN_2;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_2", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_LAIN_2;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform5repair.REVISI;
                    command.Parameters.Add("@OVER_BUDGET", SqlDbType.Decimal).Value = trform5repair.OVER_BUDGET;
                    command.Parameters.Add("@OVER_BUDGET_REQUEST", SqlDbType.VarChar).Value = trform5repair.OVER_BUDGET_REQUEST;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;
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

        //Update User Brand Manager
        public void UpdateDiterimaLain1(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR SET STATUS = @STATUS, DITERIMA_LAIN_1 = @DITERIMA_LAIN_1, TGL_DITERIMA_LAIN_1 = @TGL_DITERIMA_LAIN_1, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_1", SqlDbType.VarChar).Value = trform5repair.DITERIMA_LAIN_1;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_1", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_LAIN_1;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;
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

        public void UpdateRevisiDiterimaLain1(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR SET STATUS = @STATUS, DITERIMA_LAIN_1 = @DITERIMA_LAIN_1, TGL_DITERIMA_LAIN_1 = @TGL_DITERIMA_LAIN_1, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_1", SqlDbType.VarChar).Value = trform5repair.DITERIMA_LAIN_1;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_1", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_LAIN_1;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform5repair.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;
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


        //Update User Comercial Director

        public void UpdateDiterimaLain2(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR SET STATUS = @STATUS, DITERIMA_LAIN_2 = @DITERIMA_LAIN_2, TGL_DITERIMA_LAIN_2 = @TGL_DITERIMA_LAIN_2, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_2", SqlDbType.VarChar).Value = trform5repair.DITERIMA_LAIN_2;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_2", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_LAIN_2;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;
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

        public void UpdateRevisiDiterimaLain2(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR SET STATUS = @STATUS, DITERIMA_LAIN_2 = @DITERIMA_LAIN_2, TGL_DITERIMA_LAIN_2 = @TGL_DITERIMA_LAIN_2, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_2", SqlDbType.VarChar).Value = trform5repair.DITERIMA_LAIN_2;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_2", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_LAIN_2;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform5repair.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;
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

        //Update User Revisi Store Design
        public void UpdateRevisiDiterimaLainSD(TR_FORM5_REPAIR trform5repair)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM5_REPAIR SET STATUS = @STATUS, DITERIMA_LAIN_SD = @DITERIMA_LAIN_SD, TGL_DITERIMA_LAIN_SD = @TGL_DITERIMA_LAIN_SD, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform5repair.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform5repair.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_SD", SqlDbType.VarChar).Value = trform5repair.DITERIMA_LAIN_SD;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_SD", SqlDbType.DateTime).Value = trform5repair.TGL_DITERIMA_LAIN_SD;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform5repair.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform5repair.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform5repair.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform5repair.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform5repair.URUTAN_NEXT_USER;
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

        public void DeleteByKey(String NO_FORM)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM TR_FORM5_REPAIR  WHERE NO_FORM = @NO_FORM"), CnString))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@NO_FORM", NO_FORM);
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM TR_FORM5_REPAIR WHERE {0} ", Where), CnString))
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
