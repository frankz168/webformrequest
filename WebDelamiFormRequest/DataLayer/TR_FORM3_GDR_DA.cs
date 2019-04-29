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
    public class TR_FORM3_GDR_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM3_GDR"), CnString))
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM3_GDR WHERE NO_FORM = @NO_FORM"), CnString))
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT * FROM TR_FORM3_GDR WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO TR_FORM3_GDR(NO_FORM, KODE_FORM, PERMINTAAN_DESIGN, JENIS, TGL_REQUEST, TGL_REQUIRED, ID_DEPT, KD_BRAND, DEPT_STORE_MALL, ALOKASI_BUDGET, JADWAL_IMAGE, JADWAL_ACARABKTOKO, REFERENSI_DESIGN, RFR_LAMPIRAN_STORE, RFR_LAMPIRAN_MATERIAL, RFR_LAMPIRAN1, RFR_LAMPIRAN2, RFR_LAMPIRAN3, RFR_LAMPIRAN4, JADWAL_SELESAI_DESAIN, JADWAL_PRODUKSI_CETAK, JADWAL_KIRIM, JADWAL_FOTO, JADWAL_DI, JADWAL_ADM_CREATIVE, DIBUAT, TGL_DIBUAT, MENYETUJUI1, TGL_MENYETUJUI1, DITERIMA_1, TGL_DITERIMA_1, DITERIMA_2, TGL_DITERIMA_2, DITERIMA_3, TGL_DITERIMA_3, DITERIMA_LAIN_1, TGL_DITERIMA_LAIN_1, DITERIMA_LAIN_2, TGL_DITERIMA_LAIN_2, DITERIMA_LAIN_3, TGL_DITERIMA_LAIN_3, DITERIMA_LAIN_4, TGL_DITERIMA_LAIN_4, DITERIMA_LAIN_5_MATERI, TGL_DITERIMA_LAIN_5_MATERI, DITERIMA_LAIN_5, TGL_DITERIMA_LAIN_5, STATUS, REVISI, USER_CURRENT, NEXT_USER, URUTAN_USER_CURRENT, URUTAN_NEXT_USER, PHOTOGRAPHER, DIGITAL_IMAGING, PRODUCTION) VALUES (@NO_FORM, @KODE_FORM, @PERMINTAAN_DESIGN, @JENIS, @TGL_REQUEST, @TGL_REQUIRED, @ID_DEPT, @KD_BRAND, @DEPT_STORE_MALL, @ALOKASI_BUDGET, @JADWAL_IMAGE, @JADWAL_ACARABKTOKO, @REFERENSI_DESIGN, @RFR_LAMPIRAN_STORE, @RFR_LAMPIRAN_MATERIAL, @RFR_LAMPIRAN1, @RFR_LAMPIRAN2, @RFR_LAMPIRAN3, @RFR_LAMPIRAN4, @JADWAL_SELESAI_DESAIN, @JADWAL_PRODUKSI_CETAK, @JADWAL_KIRIM, @JADWAL_FOTO, @JADWAL_DI, @JADWAL_ADM_CREATIVE, @DIBUAT, @TGL_DIBUAT, @MENYETUJUI1, @TGL_MENYETUJUI1, @DITERIMA_1, @TGL_DITERIMA_1, @DITERIMA_2, @TGL_DITERIMA_2, @DITERIMA_3, @TGL_DITERIMA_3, @DITERIMA_LAIN_1, @TGL_DITERIMA_LAIN_1, @DITERIMA_LAIN_2, @TGL_DITERIMA_LAIN_2, @DITERIMA_LAIN_3, @TGL_DITERIMA_LAIN_3, @DITERIMA_LAIN_4, @TGL_DITERIMA_LAIN_4, @DITERIMA_LAIN_5_MATERI, @TGL_DITERIMA_LAIN_5_MATERI, @DITERIMA_LAIN_5, @TGL_DITERIMA_LAIN_5, @STATUS, @REVISI, @USER_CURRENT, @NEXT_USER, @URUTAN_USER_CURRENT, @URUTAN_NEXT_USER, @PHOTOGRAPHER, @DIGITAL_IMAGING, @PRODUCTION)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = trform3gdr.KODE_FORM;
                    command.Parameters.Add("@PERMINTAAN_DESIGN", SqlDbType.VarChar).Value = trform3gdr.PERMINTAAN_DESIGN;
                    command.Parameters.Add("@JENIS", SqlDbType.VarChar).Value = trform3gdr.JENIS;
                    command.Parameters.Add("@TGL_REQUEST", SqlDbType.DateTime).Value = trform3gdr.TGL_REQUEST;
                    command.Parameters.Add("@TGL_REQUIRED", SqlDbType.DateTime).Value = trform3gdr.TGL_REQUIRED;
                    command.Parameters.Add("@ID_DEPT", SqlDbType.VarChar).Value = trform3gdr.ID_DEPT;
                    command.Parameters.Add("@KD_BRAND", SqlDbType.VarChar).Value = trform3gdr.KD_BRAND;
                    command.Parameters.Add("@DEPT_STORE_MALL", SqlDbType.VarChar).Value = trform3gdr.DEPT_STORE_MALL;
                    command.Parameters.Add("@ALOKASI_BUDGET", SqlDbType.DateTime).Value = trform3gdr.ALOKASI_BUDGET;
                    command.Parameters.Add("@JADWAL_IMAGE", SqlDbType.DateTime).Value = trform3gdr.JADWAL_IMAGE;
                    command.Parameters.Add("@JADWAL_ACARABKTOKO", SqlDbType.DateTime).Value = trform3gdr.JADWAL_ACARABKTOKO;
                    command.Parameters.Add("@REFERENSI_DESIGN", SqlDbType.VarChar).Value = trform3gdr.REFERENSI_DESIGN;
                    command.Parameters.Add("@RFR_LAMPIRAN_STORE", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN_STORE;
                    command.Parameters.Add("@RFR_LAMPIRAN_MATERIAL", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN_MATERIAL;
                    command.Parameters.Add("@RFR_LAMPIRAN1", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN1;
                    command.Parameters.Add("@RFR_LAMPIRAN2", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN2;
                    command.Parameters.Add("@RFR_LAMPIRAN3", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN3;
                    command.Parameters.Add("@RFR_LAMPIRAN4", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN4;
                    command.Parameters.Add("@JADWAL_SELESAI_DESAIN", SqlDbType.DateTime).Value = trform3gdr.JADWAL_SELESAI_DESAIN;
                    command.Parameters.Add("@JADWAL_PRODUKSI_CETAK", SqlDbType.DateTime).Value = trform3gdr.JADWAL_PRODUKSI_CETAK;
                    command.Parameters.Add("@JADWAL_KIRIM", SqlDbType.DateTime).Value = trform3gdr.JADWAL_KIRIM;
                    command.Parameters.Add("@JADWAL_FOTO", SqlDbType.DateTime).Value = trform3gdr.JADWAL_FOTO;
                    command.Parameters.Add("@JADWAL_DI", SqlDbType.DateTime).Value = trform3gdr.JADWAL_DI;
                    command.Parameters.Add("@JADWAL_ADM_CREATIVE", SqlDbType.DateTime).Value = trform3gdr.JADWAL_ADM_CREATIVE;
                    command.Parameters.Add("@DIBUAT", SqlDbType.VarChar).Value = trform3gdr.DIBUAT;
                    command.Parameters.Add("@TGL_DIBUAT", SqlDbType.DateTime).Value = trform3gdr.TGL_DIBUAT;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform3gdr.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform3gdr.TGL_MENYETUJUI1;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_1;
                    command.Parameters.Add("@DITERIMA_2", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_2;
                    command.Parameters.Add("@TGL_DITERIMA_2", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_2;
                    command.Parameters.Add("@DITERIMA_3", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_3;
                    command.Parameters.Add("@TGL_DITERIMA_3", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_3;
                    command.Parameters.Add("@DITERIMA_LAIN_1", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_1;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_1", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_1;
                    command.Parameters.Add("@DITERIMA_LAIN_2", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_2;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_2", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_2;
                    command.Parameters.Add("@DITERIMA_LAIN_3", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_3;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_3", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_3;
                    command.Parameters.Add("@DITERIMA_LAIN_4", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_4;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_4", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_4;
                    command.Parameters.Add("@DITERIMA_LAIN_5_MATERI", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_5_MATERI;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_5_MATERI", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_5_MATERI;
                    command.Parameters.Add("@DITERIMA_LAIN_5", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_5;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_5", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_5;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform3gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
                    command.Parameters.Add("@PHOTOGRAPHER", SqlDbType.VarChar).Value = trform3gdr.PHOTOGRAPHER;
                    command.Parameters.Add("@DIGITAL_IMAGING", SqlDbType.VarChar).Value = trform3gdr.DIGITAL_IMAGING;
                    command.Parameters.Add("@PRODUCTION", SqlDbType.VarChar).Value = trform3gdr.PRODUCTION;
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

        public void Update(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM3_GDR SET KODE_FORM = @KODE_FORM, JENIS = @JENIS, TGL_REQUEST = @TGL_REQUEST, TGL_REQUIRED = @TGL_REQUIRED, DEPT_STORE_MALL = @DEPT_STORE_MALL, ALOKASI_BUDGET = @ALOKASI_BUDGET, JADWAL_IMAGE = @JADWAL_IMAGE, JADWAL_ACARABKTOKO = @JADWAL_ACARABKTOKO, REFERENSI_DESIGN = @REFERENSI_DESIGN, RFR_LAMPIRAN_STORE = @RFR_LAMPIRAN_STORE, RFR_LAMPIRAN_MATERIAL = @RFR_LAMPIRAN_MATERIAL, RFR_LAMPIRAN1 = @RFR_LAMPIRAN1, RFR_LAMPIRAN2 = @RFR_LAMPIRAN2, RFR_LAMPIRAN3 = @RFR_LAMPIRAN3, RFR_LAMPIRAN4 = @RFR_LAMPIRAN4, JADWAL_SELESAI_DESAIN = @JADWAL_SELESAI_DESAIN, JADWAL_PRODUKSI_CETAK = @JADWAL_PRODUKSI_CETAK, JADWAL_KIRIM = @JADWAL_KIRIM, JADWAL_FOTO = @JADWAL_FOTO, JADWAL_DI = @JADWAL_DI, JADWAL_ADM_CREATIVE = @JADWAL_ADM_CREATIVE, DIBUAT = @DIBUAT, TGL_DIBUAT = @TGL_DIBUAT, MENYETUJUI1 = @MENYETUJUI1, TGL_MENYETUJUI1 = @TGL_MENYETUJUI1, DITERIMA_1 = @DITERIMA_1, TGL_DITERIMA_1 = @TGL_DITERIMA_1, DITERIMA_2 = @DITERIMA_2, TGL_DITERIMA_2 = @TGL_DITERIMA_2, DITERIMA_3 = @DITERIMA_3, TGL_DITERIMA_3 = @TGL_DITERIMA_3, STATUS = @STATUS, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = trform3gdr.KODE_FORM;
                    command.Parameters.Add("@JENIS", SqlDbType.VarChar).Value = trform3gdr.JENIS;
                    command.Parameters.Add("@TGL_REQUEST", SqlDbType.DateTime).Value = trform3gdr.TGL_REQUEST;
                    command.Parameters.Add("@TGL_REQUIRED", SqlDbType.DateTime).Value = trform3gdr.TGL_REQUIRED;
                    //command.Parameters.Add("@ID_DEPT", SqlDbType.Int).Value = trform3gdr.ID_DEPT;
                    //command.Parameters.Add("@KD_BRAND", SqlDbType.VarChar).Value = trform3gdr.KD_BRAND;
                    command.Parameters.Add("@DEPT_STORE_MALL", SqlDbType.VarChar).Value = trform3gdr.DEPT_STORE_MALL;
                    command.Parameters.Add("@ALOKASI_BUDGET", SqlDbType.DateTime).Value = trform3gdr.ALOKASI_BUDGET;
                    command.Parameters.Add("@JADWAL_IMAGE", SqlDbType.DateTime).Value = trform3gdr.JADWAL_IMAGE;
                    command.Parameters.Add("@JADWAL_ACARABKTOKO", SqlDbType.DateTime).Value = trform3gdr.JADWAL_ACARABKTOKO;
                    command.Parameters.Add("@REFERENSI_DESIGN", SqlDbType.VarChar).Value = trform3gdr.REFERENSI_DESIGN;
                    command.Parameters.Add("@RFR_LAMPIRAN_STORE", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN_STORE;
                    command.Parameters.Add("@RFR_LAMPIRAN_MATERIAL", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN_MATERIAL;
                    command.Parameters.Add("@RFR_LAMPIRAN1", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN1;
                    command.Parameters.Add("@RFR_LAMPIRAN2", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN2;
                    command.Parameters.Add("@RFR_LAMPIRAN3", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN3;
                    command.Parameters.Add("@RFR_LAMPIRAN4", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN4;
                    command.Parameters.Add("@JADWAL_SELESAI_DESAIN", SqlDbType.DateTime).Value = trform3gdr.JADWAL_SELESAI_DESAIN;
                    command.Parameters.Add("@JADWAL_PRODUKSI_CETAK", SqlDbType.DateTime).Value = trform3gdr.JADWAL_PRODUKSI_CETAK;
                    command.Parameters.Add("@JADWAL_KIRIM", SqlDbType.DateTime).Value = trform3gdr.JADWAL_KIRIM;
                    command.Parameters.Add("@JADWAL_FOTO", SqlDbType.DateTime).Value = trform3gdr.JADWAL_FOTO;
                    command.Parameters.Add("@JADWAL_DI", SqlDbType.DateTime).Value = trform3gdr.JADWAL_DI;
                    command.Parameters.Add("@JADWAL_ADM_CREATIVE", SqlDbType.DateTime).Value = trform3gdr.JADWAL_ADM_CREATIVE;
                    command.Parameters.Add("@DIBUAT", SqlDbType.VarChar).Value = trform3gdr.DIBUAT;
                    command.Parameters.Add("@TGL_DIBUAT", SqlDbType.DateTime).Value = trform3gdr.TGL_DIBUAT;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform3gdr.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform3gdr.TGL_MENYETUJUI1;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_1;
                    command.Parameters.Add("@DITERIMA_2", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_2;
                    command.Parameters.Add("@TGL_DITERIMA_2", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_2;
                    command.Parameters.Add("@DITERIMA_3", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_3;
                    command.Parameters.Add("@TGL_DITERIMA_3", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_3;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform3gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateDesign(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM3_GDR SET KODE_FORM = @KODE_FORM, JENIS = @JENIS, TGL_REQUEST = @TGL_REQUEST, TGL_REQUIRED = @TGL_REQUIRED, DEPT_STORE_MALL = @DEPT_STORE_MALL, ALOKASI_BUDGET = @ALOKASI_BUDGET, JADWAL_IMAGE = @JADWAL_IMAGE, JADWAL_ACARABKTOKO = @JADWAL_ACARABKTOKO, REFERENSI_DESIGN = @REFERENSI_DESIGN, RFR_LAMPIRAN1 = @RFR_LAMPIRAN1, RFR_LAMPIRAN2 = @RFR_LAMPIRAN2, RFR_LAMPIRAN3 = @RFR_LAMPIRAN3, RFR_LAMPIRAN4 = @RFR_LAMPIRAN4, JADWAL_SELESAI_DESAIN = @JADWAL_SELESAI_DESAIN, JADWAL_PRODUKSI_CETAK = @JADWAL_PRODUKSI_CETAK, JADWAL_KIRIM = @JADWAL_KIRIM, JADWAL_FOTO = @JADWAL_FOTO, DITERIMA_2 = @DITERIMA_2, TGL_DITERIMA_2 = @TGL_DITERIMA_2, STATUS = @STATUS, REVISI = @REVISI, RFR_LAMPIRAN5_GD = @RFR_LAMPIRAN5_GD, RFR_LAMPIRAN6_GD = @RFR_LAMPIRAN6_GD, RFR_LAMPIRAN7_GD = @RFR_LAMPIRAN7_GD, RFR_LAMPIRAN8_GD = @RFR_LAMPIRAN8_GD, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = trform3gdr.KODE_FORM;
                    command.Parameters.Add("@JENIS", SqlDbType.VarChar).Value = trform3gdr.JENIS;
                    command.Parameters.Add("@TGL_REQUEST", SqlDbType.DateTime).Value = trform3gdr.TGL_REQUEST;
                    command.Parameters.Add("@TGL_REQUIRED", SqlDbType.DateTime).Value = trform3gdr.TGL_REQUIRED;
                    //command.Parameters.Add("@ID_DEPT", SqlDbType.Int).Value = trform3gdr.ID_DEPT;
                    //command.Parameters.Add("@KD_BRAND", SqlDbType.VarChar).Value = trform3gdr.KD_BRAND;
                    command.Parameters.Add("@DEPT_STORE_MALL", SqlDbType.VarChar).Value = trform3gdr.DEPT_STORE_MALL;
                    command.Parameters.Add("@ALOKASI_BUDGET", SqlDbType.DateTime).Value = trform3gdr.ALOKASI_BUDGET;
                    command.Parameters.Add("@JADWAL_IMAGE", SqlDbType.DateTime).Value = trform3gdr.JADWAL_IMAGE;
                    command.Parameters.Add("@JADWAL_ACARABKTOKO", SqlDbType.DateTime).Value = trform3gdr.JADWAL_ACARABKTOKO;
                    command.Parameters.Add("@REFERENSI_DESIGN", SqlDbType.VarChar).Value = trform3gdr.REFERENSI_DESIGN;
                    command.Parameters.Add("@RFR_LAMPIRAN1", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN1;
                    command.Parameters.Add("@RFR_LAMPIRAN2", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN2;
                    command.Parameters.Add("@RFR_LAMPIRAN3", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN3;
                    command.Parameters.Add("@RFR_LAMPIRAN4", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN4;
                    command.Parameters.Add("@JADWAL_SELESAI_DESAIN", SqlDbType.DateTime).Value = trform3gdr.JADWAL_SELESAI_DESAIN;
                    command.Parameters.Add("@JADWAL_PRODUKSI_CETAK", SqlDbType.DateTime).Value = trform3gdr.JADWAL_PRODUKSI_CETAK;
                    command.Parameters.Add("@JADWAL_KIRIM", SqlDbType.DateTime).Value = trform3gdr.JADWAL_KIRIM;
                    command.Parameters.Add("@JADWAL_FOTO", SqlDbType.DateTime).Value = trform3gdr.JADWAL_FOTO;
                    command.Parameters.Add("@DITERIMA_2", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_2;
                    command.Parameters.Add("@TGL_DITERIMA_2", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_2;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform3gdr.REVISI;
                    command.Parameters.Add("@RFR_LAMPIRAN5_GD", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN5_GD;
                    command.Parameters.Add("@RFR_LAMPIRAN6_GD", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN6_GD;
                    command.Parameters.Add("@RFR_LAMPIRAN7_GD", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN7_GD;
                    command.Parameters.Add("@RFR_LAMPIRAN8_GD", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN8_GD;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateDibuat(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DIBUAT = @DIBUAT, TGL_DIBUAT = @TGL_DIBUAT, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DIBUAT", SqlDbType.VarChar).Value = trform3gdr.DIBUAT;
                    command.Parameters.Add("@TGL_DIBUAT", SqlDbType.DateTime).Value = trform3gdr.TGL_DIBUAT;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;

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

        public void UpdateMenyetujui1(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, MENYETUJUI1 = @MENYETUJUI1, TGL_MENYETUJUI1 = @TGL_MENYETUJUI1, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform3gdr.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform3gdr.TGL_MENYETUJUI1;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateDiterima1(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET JADWAL_SELESAI_DESAIN = @JADWAL_SELESAI_DESAIN, JADWAL_PRODUKSI_CETAK = @JADWAL_PRODUKSI_CETAK, JADWAL_KIRIM = @JADWAL_KIRIM, JADWAL_FOTO = @JADWAL_FOTO, JADWAL_DI = @JADWAL_DI, JADWAL_ADM_CREATIVE = @JADWAL_ADM_CREATIVE, STATUS = @STATUS, DITERIMA_1 = @DITERIMA_1, DITERIMA_2 = @DITERIMA_2, TGL_DITERIMA_1 = @TGL_DITERIMA_1, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER, PRODUCTION = @PRODUCTION WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@JADWAL_SELESAI_DESAIN", SqlDbType.DateTime).Value = trform3gdr.JADWAL_SELESAI_DESAIN;
                    command.Parameters.Add("@JADWAL_PRODUKSI_CETAK", SqlDbType.DateTime).Value = trform3gdr.JADWAL_PRODUKSI_CETAK;
                    command.Parameters.Add("@JADWAL_KIRIM", SqlDbType.DateTime).Value = trform3gdr.JADWAL_KIRIM;
                    command.Parameters.Add("@JADWAL_FOTO", SqlDbType.DateTime).Value = trform3gdr.JADWAL_FOTO;
                    command.Parameters.Add("@JADWAL_DI", SqlDbType.DateTime).Value = trform3gdr.JADWAL_DI;
                    command.Parameters.Add("@JADWAL_ADM_CREATIVE", SqlDbType.DateTime).Value = trform3gdr.JADWAL_ADM_CREATIVE;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_1;
                    command.Parameters.Add("@DITERIMA_2", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_2;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform3gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
                    command.Parameters.Add("@PRODUCTION", SqlDbType.VarChar).Value = trform3gdr.PRODUCTION;

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

        public void UpdateDiterima1NonJadwal(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_1 = @DITERIMA_1, TGL_DITERIMA_1 = @TGL_DITERIMA_1, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_1;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform3gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateDiterima1RevisiContent(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET JADWAL_SELESAI_DESAIN = @JADWAL_SELESAI_DESAIN, JADWAL_PRODUKSI_CETAK = @JADWAL_PRODUKSI_CETAK, JADWAL_KIRIM = @JADWAL_KIRIM, JADWAL_FOTO = @JADWAL_FOTO, JADWAL_DI = @JADWAL_DI, JADWAL_ADM_CREATIVE = @JADWAL_ADM_CREATIVE, STATUS = @STATUS, DITERIMA_1 = @DITERIMA_1, DITERIMA_2 = @DITERIMA_2, TGL_DITERIMA_1 = @TGL_DITERIMA_1, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@JADWAL_SELESAI_DESAIN", SqlDbType.DateTime).Value = trform3gdr.JADWAL_SELESAI_DESAIN;
                    command.Parameters.Add("@JADWAL_PRODUKSI_CETAK", SqlDbType.DateTime).Value = trform3gdr.JADWAL_PRODUKSI_CETAK;
                    command.Parameters.Add("@JADWAL_KIRIM", SqlDbType.DateTime).Value = trform3gdr.JADWAL_KIRIM;
                    command.Parameters.Add("@JADWAL_FOTO", SqlDbType.DateTime).Value = trform3gdr.JADWAL_FOTO;
                    command.Parameters.Add("@JADWAL_DI", SqlDbType.DateTime).Value = trform3gdr.JADWAL_DI;
                    command.Parameters.Add("@JADWAL_ADM_CREATIVE", SqlDbType.DateTime).Value = trform3gdr.JADWAL_ADM_CREATIVE;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_1;
                    command.Parameters.Add("@DITERIMA_2", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_2;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform3gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;

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

        public void UpdateDiterima2(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_2 = @DITERIMA_2, TGL_DITERIMA_2 = @TGL_DITERIMA_2, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_2", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_2;
                    command.Parameters.Add("@TGL_DITERIMA_2", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_2;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateDiterima3(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_3 = @DITERIMA_3, TGL_DITERIMA_3 = @TGL_DITERIMA_3, STATUS_VER = @STATUS_VER, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_3", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_3;
                    command.Parameters.Add("@TGL_DITERIMA_3", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_3;
                    command.Parameters.Add("@STATUS_VER", SqlDbType.VarChar).Value = trform3gdr.STATUS_VER;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateRevisiMenyetujui1(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, MENYETUJUI1 = @MENYETUJUI1, TGL_MENYETUJUI1 = @TGL_MENYETUJUI1, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform3gdr.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform3gdr.TGL_MENYETUJUI1;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform3gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateRevisiDiterima1(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_1 = @DITERIMA_1, TGL_DITERIMA_1 = @TGL_DITERIMA_1, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_1;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform3gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateRevisiDiterima2(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_2 = @DITERIMA_2, TGL_DITERIMA_2 = @TGL_DITERIMA_2, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_2", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_2;
                    command.Parameters.Add("@TGL_DITERIMA_2", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_2;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform3gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateRevisiDiterima3(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_3 = @DITERIMA_3, TGL_DITERIMA_3 = @TGL_DITERIMA_3, REVISI = @REVISI, STATUS_VER = @STATUS_VER, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_3", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_3;
                    command.Parameters.Add("@TGL_DITERIMA_3", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_3;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform3gdr.REVISI;
                    command.Parameters.Add("@STATUS_VER", SqlDbType.VarChar).Value = trform3gdr.STATUS_VER;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        //Update User Photographer
        public void UpdateDiterimaLain1(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_LAIN_1 = @DITERIMA_LAIN_1, TGL_DITERIMA_LAIN_1 = @TGL_DITERIMA_LAIN_1, RFR_LAMPIRAN1_PG = @RFR_LAMPIRAN1_PG, RFR_LAMPIRAN2_PG = @RFR_LAMPIRAN2_PG, RFR_LAMPIRAN3_PG = @RFR_LAMPIRAN3_PG, RFR_LAMPIRAN4_PG = @RFR_LAMPIRAN4_PG, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_1", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_1;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_1", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_1;
                    command.Parameters.Add("@RFR_LAMPIRAN1_PG", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN1_PG;
                    command.Parameters.Add("@RFR_LAMPIRAN2_PG", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN2_PG;
                    command.Parameters.Add("@RFR_LAMPIRAN3_PG", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN3_PG;
                    command.Parameters.Add("@RFR_LAMPIRAN4_PG", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN4_PG;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateDiterimaLain2(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_LAIN_2 = @DITERIMA_LAIN_2, TGL_DITERIMA_LAIN_2 = @TGL_DITERIMA_LAIN_2, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_2", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_2;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_2", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_2;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateRevisiDiterimaLain1(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_LAIN_1 = @DITERIMA_LAIN_1, TGL_DITERIMA_LAIN_1 = @TGL_DITERIMA_LAIN_1, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_1", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_1;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_1", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_1;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform3gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateRevisiDiterimaLain2(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_LAIN_2 = @DITERIMA_LAIN_2, TGL_DITERIMA_LAIN_2 = @TGL_DITERIMA_LAIN_2, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_2", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_2;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_2", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_2;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform3gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateCancelDiterimaLain1(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_LAIN_1 = @DITERIMA_LAIN_1, TGL_DITERIMA_LAIN_1 = @TGL_DITERIMA_LAIN_1, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_1", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_1;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_1", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_1;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        //Update Digital Imaging

        public void UpdateDiterimaLain3(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_LAIN_3 = @DITERIMA_LAIN_3, TGL_DITERIMA_LAIN_3 = @TGL_DITERIMA_LAIN_3, RFR_LAMPIRAN1_DI = @RFR_LAMPIRAN1_DI, RFR_LAMPIRAN2_DI = @RFR_LAMPIRAN2_DI, RFR_LAMPIRAN3_DI = @RFR_LAMPIRAN3_DI, RFR_LAMPIRAN4_DI = @RFR_LAMPIRAN4_DI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_3", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_3;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_3", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_3;
                    command.Parameters.Add("@RFR_LAMPIRAN1_DI", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN1_DI;
                    command.Parameters.Add("@RFR_LAMPIRAN2_DI", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN2_DI;
                    command.Parameters.Add("@RFR_LAMPIRAN3_DI", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN3_DI;
                    command.Parameters.Add("@RFR_LAMPIRAN4_DI", SqlDbType.VarChar).Value = trform3gdr.RFR_LAMPIRAN4_DI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateDiterimaLain4(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_LAIN_4 = @DITERIMA_LAIN_4, TGL_DITERIMA_LAIN_4 = @TGL_DITERIMA_LAIN_4, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_4", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_4;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_4", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_4;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateRevisiDiterimaLain3(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_LAIN_3 = @DITERIMA_LAIN_3, TGL_DITERIMA_LAIN_3 = @TGL_DITERIMA_LAIN_3, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_3", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_3;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_3", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_3;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform3gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateRevisiDiterimaLain4(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_LAIN_4 = @DITERIMA_LAIN_4, TGL_DITERIMA_LAIN_4 = @TGL_DITERIMA_LAIN_4, REVISI = @REVISI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_4", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_4;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_4", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_4;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform3gdr.REVISI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateCancelDiterimaLain3(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_LAIN_3 = @DITERIMA_LAIN_3, TGL_DITERIMA_LAIN_3 = @TGL_DITERIMA_LAIN_3, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_3", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_3;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_3", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_3;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        //Update Production
        public void UpdateDiterimaLain5Materi(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_LAIN_5_MATERI = @DITERIMA_LAIN_5_MATERI, TGL_DITERIMA_LAIN_5_MATERI = @TGL_DITERIMA_LAIN_5_MATERI, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_5_MATERI", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_5_MATERI;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_5_MATERI", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_5_MATERI;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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

        public void UpdateDiterimaLain5(TR_FORM3_GDR trform3gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM3_GDR SET STATUS = @STATUS, DITERIMA_LAIN_5 = @DITERIMA_LAIN_5, TGL_DITERIMA_LAIN_5 = @TGL_DITERIMA_LAIN_5, USER_CURRENT = @USER_CURRENT, NEXT_USER = @NEXT_USER, URUTAN_USER_CURRENT = @URUTAN_USER_CURRENT, URUTAN_NEXT_USER = @URUTAN_NEXT_USER WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform3gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform3gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_LAIN_5", SqlDbType.VarChar).Value = trform3gdr.DITERIMA_LAIN_5;
                    command.Parameters.Add("@TGL_DITERIMA_LAIN_5", SqlDbType.DateTime).Value = trform3gdr.TGL_DITERIMA_LAIN_5;
                    command.Parameters.Add("@USER_CURRENT", SqlDbType.VarChar).Value = trform3gdr.USER_CURRENT;
                    command.Parameters.Add("@NEXT_USER", SqlDbType.VarChar).Value = trform3gdr.NEXT_USER;
                    command.Parameters.Add("@URUTAN_USER_CURRENT", SqlDbType.Int).Value = trform3gdr.URUTAN_USER_CURRENT;
                    command.Parameters.Add("@URUTAN_NEXT_USER", SqlDbType.Int).Value = trform3gdr.URUTAN_NEXT_USER;
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM TR_FORM3_GDR  WHERE NO_FORM = @NO_FORM"), CnString))
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM TR_FORM3_GDR WHERE {0} ", Where), CnString))
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
