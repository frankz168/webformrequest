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
    public class TR_FORM4_GDR_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM4_GDR"), CnString))
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM4_GDR WHERE NO_FORM = @NO_FORM"), CnString))
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM4_GDR WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(TR_FORM4_GDR trform4gdr)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO TR_FORM4_GDR(NO_FORM, KODE_FORM, ID_DEPT, TGL_REQUEST, PERIODE_CAMPAIGN_FROM, PERIODE_CAMPAIGN_TO, BUDGET, RFR_LAMPIRAN1, RFR_LAMPIRAN2, RFR_LAMPIRAN3, RFR_LAMPIRAN4, RFR_LAMPIRAN5, RFR_LAMPIRAN6, RFR_LAMPIRAN7, RFR_LAMPIRAN8, RFR_LAMPIRAN9, RFR_LAMPIRAN10, RFR_LAMPIRAN11, RFR_LAMPIRAN12, RFR_LAMPIRAN13, RFR_LAMPIRAN14, RFR_LAMPIRAN15, RFR_LAMPIRAN16, RFR_LAMPIRAN17, RFR_LAMPIRAN18, RFR_LAMPIRAN19, RFR_LAMPIRAN20, RFR_LAMPIRAN21, RFR_LAMPIRAN22, RFR_LAMPIRAN23, RFR_LAMPIRAN24, RFR_LAMPIRAN25, RFR_LAMPIRAN26, RFR_LAMPIRAN27, RFR_LAMPIRAN28, RFR_LAMPIRAN29, RFR_LAMPIRAN30, CAMPAIGN, CAPTION, TARGET_LOKASI, UMUR, JENIS_KELAMIN, INTEREST_MINAT, INFORMASI_TAMBAHAN, DIBUAT, TGL_DIBUAT, MENYETUJUI1, TGL_MENYETUJUI1, DITERIMA_1, TGL_DITERIMA_1, STATUS, REVISI, USER_CURRENT, NEXT_USER, URUTAN_USER_CURRENT, URUTAN_NEXT_USER) VALUES (@NO_FORM, @KODE_FORM, @ID_DEPT, @TGL_REQUEST, @PERIODE_CAMPAIGN_FROM, @PERIODE_CAMPAIGN_TO, @BUDGET, @RFR_LAMPIRAN1, @RFR_LAMPIRAN2, @RFR_LAMPIRAN3, @RFR_LAMPIRAN4, @RFR_LAMPIRAN5, @RFR_LAMPIRAN6, @RFR_LAMPIRAN7, @RFR_LAMPIRAN8, @RFR_LAMPIRAN9, @RFR_LAMPIRAN10, @RFR_LAMPIRAN11, @RFR_LAMPIRAN12, @RFR_LAMPIRAN13, @RFR_LAMPIRAN14, @RFR_LAMPIRAN15, @RFR_LAMPIRAN16, @RFR_LAMPIRAN17, @RFR_LAMPIRAN18, @RFR_LAMPIRAN19, @RFR_LAMPIRAN20, @RFR_LAMPIRAN21, @RFR_LAMPIRAN22, @RFR_LAMPIRAN23, @RFR_LAMPIRAN24, @RFR_LAMPIRAN25, @RFR_LAMPIRAN26, @RFR_LAMPIRAN27, @RFR_LAMPIRAN28, @RFR_LAMPIRAN29, @RFR_LAMPIRAN30, @CAMPAIGN, @CAPTION, @TARGET_LOKASI, @UMUR, @JENIS_KELAMIN, @INTEREST_MINAT, @INFORMASI_TAMBAHAN, @DIBUAT, @TGL_DIBUAT, @MENYETUJUI1, @TGL_MENYETUJUI1, @DITERIMA_1, @TGL_DITERIMA_1, @STATUS, @REVISI, @USER_CURRENT, @NEXT_USER, @URUTAN_USER_CURRENT, @URUTAN_NEXT_USER)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform4gdr.NO_FORM;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = trform4gdr.KODE_FORM;
                    command.Parameters.Add("@ID_DEPT", SqlDbType.VarChar).Value = trform4gdr.ID_DEPT;
                    command.Parameters.Add("@TGL_REQUEST", SqlDbType.DateTime).Value = trform4gdr.TGL_REQUEST;
                    //command.Parameters.Add("@BRAND", SqlDbType.VarChar).Value = trform4gdr.BRAND;
                    command.Parameters.Add("@PERIODE_CAMPAIGN_FROM", SqlDbType.DateTime).Value = trform4gdr.PERIODE_CAMPAIGN_FROM;
                    command.Parameters.Add("@PERIODE_CAMPAIGN_TO", SqlDbType.DateTime).Value = trform4gdr.PERIODE_CAMPAIGN_TO;
                    command.Parameters.Add("@BUDGET", SqlDbType.Decimal).Value = trform4gdr.BUDGET;
                    command.Parameters.Add("@RFR_LAMPIRAN1", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN1;
                    command.Parameters.Add("@RFR_LAMPIRAN2", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN2;
                    command.Parameters.Add("@RFR_LAMPIRAN3", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN3;
                    command.Parameters.Add("@RFR_LAMPIRAN4", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN4;
                    command.Parameters.Add("@RFR_LAMPIRAN5", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN5;
                    command.Parameters.Add("@RFR_LAMPIRAN6", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN6;
                    command.Parameters.Add("@RFR_LAMPIRAN7", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN7;
                    command.Parameters.Add("@RFR_LAMPIRAN8", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN8;
                    command.Parameters.Add("@RFR_LAMPIRAN9", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN9;
                    command.Parameters.Add("@RFR_LAMPIRAN10", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN10;
                    command.Parameters.Add("@RFR_LAMPIRAN11", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN11;
                    command.Parameters.Add("@RFR_LAMPIRAN12", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN12;
                    command.Parameters.Add("@RFR_LAMPIRAN13", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN13;
                    command.Parameters.Add("@RFR_LAMPIRAN14", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN14;
                    command.Parameters.Add("@RFR_LAMPIRAN15", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN15;
                    command.Parameters.Add("@RFR_LAMPIRAN16", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN16;
                    command.Parameters.Add("@RFR_LAMPIRAN17", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN17;
                    command.Parameters.Add("@RFR_LAMPIRAN18", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN18;
                    command.Parameters.Add("@RFR_LAMPIRAN19", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN19;
                    command.Parameters.Add("@RFR_LAMPIRAN20", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN20;
                    command.Parameters.Add("@RFR_LAMPIRAN21", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN21;
                    command.Parameters.Add("@RFR_LAMPIRAN22", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN22;
                    command.Parameters.Add("@RFR_LAMPIRAN23", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN23;
                    command.Parameters.Add("@RFR_LAMPIRAN24", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN24;
                    command.Parameters.Add("@RFR_LAMPIRAN25", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN25;
                    command.Parameters.Add("@RFR_LAMPIRAN26", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN26;
                    command.Parameters.Add("@RFR_LAMPIRAN27", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN27;
                    command.Parameters.Add("@RFR_LAMPIRAN28", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN28;
                    command.Parameters.Add("@RFR_LAMPIRAN29", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN29;
                    command.Parameters.Add("@RFR_LAMPIRAN30", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN30;
                    command.Parameters.Add("@CAMPAIGN", SqlDbType.VarChar).Value = trform4gdr.CAMPAIGN;
                    command.Parameters.Add("@CAPTION", SqlDbType.VarChar).Value = trform4gdr.CAPTION;
                    command.Parameters.Add("@TARGET_LOKASI", SqlDbType.VarChar).Value = trform4gdr.TARGET_LOKASI;
                    command.Parameters.Add("@UMUR", SqlDbType.VarChar).Value = trform4gdr.UMUR;
                    command.Parameters.Add("@JENIS_KELAMIN", SqlDbType.VarChar).Value = trform4gdr.JENIS_KELAMIN;
                    command.Parameters.Add("@INTEREST_MINAT", SqlDbType.VarChar).Value = trform4gdr.INTEREST_MINAT;
                    command.Parameters.Add("@INFORMASI_TAMBAHAN", SqlDbType.VarChar).Value = trform4gdr.INFORMASI_TAMBAHAN;
                    command.Parameters.Add("@DIBUAT", SqlDbType.VarChar).Value = trform4gdr.DIBUAT;
                    command.Parameters.Add("@TGL_DIBUAT", SqlDbType.DateTime).Value = trform4gdr.TGL_DIBUAT;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform4gdr.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform4gdr.TGL_MENYETUJUI1;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform4gdr.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform4gdr.TGL_DITERIMA_1;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform4gdr.STATUS;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform4gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform4gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform4gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform4gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform4gdr.URUTAN_NEXT_USER;
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

        public void Update(TR_FORM4_GDR trform4gdr)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM4_GDR SET KODE_FORM = @KODE_FORM, TGL_REQUEST = @TGL_REQUEST, PERIODE_CAMPAIGN_FROM = @PERIODE_CAMPAIGN_FROM, PERIODE_CAMPAIGN_TO = @PERIODE_CAMPAIGN_TO, BUDGET = @BUDGET, RFR_LAMPIRAN1 = RFR_LAMPIRAN1 = @RFR_LAMPIRAN1, RFR_LAMPIRAN2 = @RFR_LAMPIRAN2, RFR_LAMPIRAN3 = @RFR_LAMPIRAN3, RFR_LAMPIRAN4 = @RFR_LAMPIRAN4, RFR_LAMPIRAN5 = @RFR_LAMPIRAN5, RFR_LAMPIRAN6 = @RFR_LAMPIRAN6, RFR_LAMPIRAN7 = @RFR_LAMPIRAN7, RFR_LAMPIRAN8 = @RFR_LAMPIRAN8, RFR_LAMPIRAN9 = @RFR_LAMPIRAN9, RFR_LAMPIRAN10 = @RFR_LAMPIRAN10, RFR_LAMPIRAN11 = @RFR_LAMPIRAN11, RFR_LAMPIRAN12 = @RFR_LAMPIRAN12, RFR_LAMPIRAN13 = @RFR_LAMPIRAN13, RFR_LAMPIRAN14 = @RFR_LAMPIRAN14, RFR_LAMPIRAN15 = @RFR_LAMPIRAN15, RFR_LAMPIRAN16 = @RFR_LAMPIRAN16, RFR_LAMPIRAN17 = @RFR_LAMPIRAN17, RFR_LAMPIRAN18 = @RFR_LAMPIRAN18, RFR_LAMPIRAN19 = @RFR_LAMPIRAN19, RFR_LAMPIRAN20 = @RFR_LAMPIRAN20, RFR_LAMPIRAN21 = @RFR_LAMPIRAN21, RFR_LAMPIRAN22 = @RFR_LAMPIRAN22, RFR_LAMPIRAN23 = @RFR_LAMPIRAN23, RFR_LAMPIRAN24 = @RFR_LAMPIRAN24, RFR_LAMPIRAN25 = @RFR_LAMPIRAN25, RFR_LAMPIRAN26 = @RFR_LAMPIRAN26, RFR_LAMPIRAN27 = @RFR_LAMPIRAN27, RFR_LAMPIRAN28 = @RFR_LAMPIRAN28, RFR_LAMPIRAN29 = @RFR_LAMPIRAN29, RFR_LAMPIRAN30 = @RFR_LAMPIRAN30, CAMPAIGN = @CAMPAIGN, CAPTION = @CAPTION, TARGET_LOKASI = @TARGET_LOKASI, UMUR = @UMUR, JENIS_KELAMIN = @JENIS_KELAMIN, INTEREST_MINAT = @INTEREST_MINAT, INFORMASI_TAMBAHAN = @INFORMASI_TAMBAHAN, DIBUAT = @DIBUAT, TGL_DIBUAT = @TGL_DIBUAT, MENYETUJUI1 = @MENYETUJUI1, TGL_MENYETUJUI1 = @TGL_MENYETUJUI1, DITERIMA_1 = @DITERIMA_1, TGL_DITERIMA_1 = @TGL_DITERIMA_1, STATUS = @STATUS, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform4gdr.NO_FORM;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = trform4gdr.KODE_FORM;
                    //command.Parameters.Add("@ID_DEPT", SqlDbType.VarChar).Value = trform4gdr.ID_DEPT;
                    command.Parameters.Add("@TGL_REQUEST", SqlDbType.DateTime).Value = trform4gdr.TGL_REQUEST;
                    //command.Parameters.Add("@BRAND", SqlDbType.VarChar).Value = trform4gdr.BRAND;
                    command.Parameters.Add("@PERIODE_CAMPAIGN_FROM", SqlDbType.DateTime).Value = trform4gdr.PERIODE_CAMPAIGN_FROM;
                    command.Parameters.Add("@PERIODE_CAMPAIGN_TO", SqlDbType.DateTime).Value = trform4gdr.PERIODE_CAMPAIGN_TO;
                    command.Parameters.Add("@BUDGET", SqlDbType.Decimal).Value = trform4gdr.BUDGET;
                    command.Parameters.Add("@RFR_LAMPIRAN1", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN1;
                    command.Parameters.Add("@RFR_LAMPIRAN2", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN2;
                    command.Parameters.Add("@RFR_LAMPIRAN3", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN3;
                    command.Parameters.Add("@RFR_LAMPIRAN4", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN4;
                    command.Parameters.Add("@RFR_LAMPIRAN5", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN5;
                    command.Parameters.Add("@RFR_LAMPIRAN6", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN6;
                    command.Parameters.Add("@RFR_LAMPIRAN7", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN7;
                    command.Parameters.Add("@RFR_LAMPIRAN8", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN8;
                    command.Parameters.Add("@RFR_LAMPIRAN9", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN9;
                    command.Parameters.Add("@RFR_LAMPIRAN10", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN10;
                    command.Parameters.Add("@RFR_LAMPIRAN11", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN11;
                    command.Parameters.Add("@RFR_LAMPIRAN12", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN12;
                    command.Parameters.Add("@RFR_LAMPIRAN13", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN13;
                    command.Parameters.Add("@RFR_LAMPIRAN14", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN14;
                    command.Parameters.Add("@RFR_LAMPIRAN15", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN15;
                    command.Parameters.Add("@RFR_LAMPIRAN16", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN16;
                    command.Parameters.Add("@RFR_LAMPIRAN17", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN17;
                    command.Parameters.Add("@RFR_LAMPIRAN18", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN18;
                    command.Parameters.Add("@RFR_LAMPIRAN19", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN19;
                    command.Parameters.Add("@RFR_LAMPIRAN20", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN20;
                    command.Parameters.Add("@RFR_LAMPIRAN21", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN21;
                    command.Parameters.Add("@RFR_LAMPIRAN22", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN22;
                    command.Parameters.Add("@RFR_LAMPIRAN23", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN23;
                    command.Parameters.Add("@RFR_LAMPIRAN24", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN24;
                    command.Parameters.Add("@RFR_LAMPIRAN25", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN25;
                    command.Parameters.Add("@RFR_LAMPIRAN26", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN26;
                    command.Parameters.Add("@RFR_LAMPIRAN27", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN27;
                    command.Parameters.Add("@RFR_LAMPIRAN28", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN28;
                    command.Parameters.Add("@RFR_LAMPIRAN29", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN29;
                    command.Parameters.Add("@RFR_LAMPIRAN30", SqlDbType.VarChar).Value = trform4gdr.RFR_LAMPIRAN30;
                    command.Parameters.Add("@CAMPAIGN", SqlDbType.VarChar).Value = trform4gdr.CAMPAIGN;
                    command.Parameters.Add("@CAPTION", SqlDbType.VarChar).Value = trform4gdr.CAPTION;
                    command.Parameters.Add("@TARGET_LOKASI", SqlDbType.VarChar).Value = trform4gdr.TARGET_LOKASI;
                    command.Parameters.Add("@UMUR", SqlDbType.VarChar).Value = trform4gdr.UMUR;
                    command.Parameters.Add("@JENIS_KELAMIN", SqlDbType.VarChar).Value = trform4gdr.JENIS_KELAMIN;
                    command.Parameters.Add("@INTEREST_MINAT", SqlDbType.VarChar).Value = trform4gdr.INTEREST_MINAT;
                    command.Parameters.Add("@INFORMASI_TAMBAHAN", SqlDbType.VarChar).Value = trform4gdr.INFORMASI_TAMBAHAN;
                    command.Parameters.Add("@DIBUAT", SqlDbType.VarChar).Value = trform4gdr.DIBUAT;
                    command.Parameters.Add("@TGL_DIBUAT", SqlDbType.DateTime).Value = trform4gdr.TGL_DIBUAT;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform4gdr.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform4gdr.TGL_MENYETUJUI1;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform4gdr.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform4gdr.TGL_DITERIMA_1;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform4gdr.STATUS;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform4gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform4gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform4gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform4gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform4gdr.URUTAN_NEXT_USER;
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

        public void UpdateDibuat(TR_FORM4_GDR trform4gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM4_GDR SET STATUS = @STATUS, DIBUAT = @DIBUAT, TGL_DIBUAT = @TGL_DIBUAT, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform4gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform4gdr.STATUS;
                    command.Parameters.Add("@DIBUAT", SqlDbType.VarChar).Value = trform4gdr.DIBUAT;
                    command.Parameters.Add("@TGL_DIBUAT", SqlDbType.DateTime).Value = trform4gdr.TGL_DIBUAT;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform4gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform4gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform4gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform4gdr.URUTAN_NEXT_USER;
                    command.ExecuteScalar();

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

        public void UpdateMenyetujui1(TR_FORM4_GDR trform4gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM4_GDR SET STATUS = @STATUS, MENYETUJUI1 = @MENYETUJUI1, TGL_MENYETUJUI1 = @TGL_MENYETUJUI1, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform4gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform4gdr.STATUS;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform4gdr.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform4gdr.TGL_MENYETUJUI1;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform4gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform4gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform4gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform4gdr.URUTAN_NEXT_USER;
                    command.ExecuteScalar();

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

        public void UpdateDiterima1(TR_FORM4_GDR trform4gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM4_GDR SET STATUS = @STATUS, DITERIMA_1 = @DITERIMA_1, TGL_DITERIMA_1 = @TGL_DITERIMA_1, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform4gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform4gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform4gdr.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform4gdr.TGL_DITERIMA_1;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform4gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform4gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform4gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform4gdr.URUTAN_NEXT_USER;

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

        public void UpdateRevisiMenyetujui1(TR_FORM4_GDR trform4gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM4_GDR SET STATUS = @STATUS, MENYETUJUI1 = @MENYETUJUI1, TGL_MENYETUJUI1 = @TGL_MENYETUJUI1, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform4gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform4gdr.STATUS;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform4gdr.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform4gdr.TGL_MENYETUJUI1;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform4gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform4gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform4gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform4gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform4gdr.URUTAN_NEXT_USER;
                    command.ExecuteScalar();

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

        public void UpdateRevisiDiterima1(TR_FORM4_GDR trform4gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM4_GDR SET STATUS = @STATUS, DITERIMA_1 = @DITERIMA_1, TGL_DITERIMA_1 = @TGL_DITERIMA_1, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform4gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform4gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform4gdr.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform4gdr.TGL_DITERIMA_1;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform4gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform4gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform4gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform4gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform4gdr.URUTAN_NEXT_USER;

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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM TR_FORM4_GDR  WHERE NO_FORM = @NO_FORM"), CnString))
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM TR_FORM4_GDR WHERE {0} ", Where), CnString))
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
