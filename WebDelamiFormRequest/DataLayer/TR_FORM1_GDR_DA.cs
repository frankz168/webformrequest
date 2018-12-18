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
    public class TR_FORM1_GDR_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT NO_FORM, KODE_FORM, JENIS, TGL_REQUEST, ID_DEPT, KD_BRAND, DEPT_STORE_MALL, KOTA, ALOKASI_BUDGET, JADWAL_IMAGE, UTK_TOKO, JADWAL_ACARA, JADWAL_BUKA_TOKO, RFR_LAMPIRAN1, RFR_LAMPIRAN2, RFR_LAMPIRAN3, RFR_LAMPIRAN4, JADWAL_SELESAI_DESAIN, JADWAL_PRODUKSI_CETAK, JADWAL_KIRIM, JADWAL_PASANG, NAMA, JABATAN, ALAMAT_KIRIM, NO_TELP, EMAIL, DIBUAT, TGL_DIBUAT, MENYETUJUI1, TGL_MENYETUJUI1, MENYETUJUI2, TGL_MENYETUJUI2, DITERIMA_1, TGL_DITERIMA_1, DITERIMA_2, TGL_DITERIMA_2, DITERIMA_3, TGL_DITERIMA_3, STATUS, REVISI FROM TR_FORM1_GDR"), CnString))
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT NO_FORM, KODE_FORM, JENIS, TGL_REQUEST, ID_DEPT, KD_BRAND, DEPT_STORE_MALL, KOTA, ALOKASI_BUDGET, JADWAL_IMAGE, UTK_TOKO, JADWAL_ACARA, JADWAL_BUKA_TOKO, RFR_LAMPIRAN1, RFR_LAMPIRAN2, RFR_LAMPIRAN3, RFR_LAMPIRAN4, JADWAL_SELESAI_DESAIN, JADWAL_PRODUKSI_CETAK, JADWAL_KIRIM, JADWAL_PASANG, NAMA, JABATAN, ALAMAT_KIRIM, NO_TELP, EMAIL, DIBUAT, TGL_DIBUAT, MENYETUJUI1, TGL_MENYETUJUI1, MENYETUJUI2, TGL_MENYETUJUI2, DITERIMA_1, TGL_DITERIMA_1, DITERIMA_2, TGL_DITERIMA_2, DITERIMA_3, TGL_DITERIMA_3, STATUS, REVISI FROM TR_FORM1_GDR WHERE NO_FORM = @NO_FORM"), CnString))
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT NO_FORM, KODE_FORM, JENIS, TGL_REQUEST, ID_DEPT, KD_BRAND, DEPT_STORE_MALL, KOTA, ALOKASI_BUDGET, JADWAL_IMAGE, UTK_TOKO, JADWAL_ACARA, JADWAL_BUKA_TOKO, RFR_LAMPIRAN1, RFR_LAMPIRAN2, RFR_LAMPIRAN3, RFR_LAMPIRAN4, JADWAL_SELESAI_DESAIN, JADWAL_PRODUKSI_CETAK, JADWAL_KIRIM, JADWAL_PASANG, NAMA, JABATAN, ALAMAT_KIRIM, NO_TELP, EMAIL, DIBUAT, TGL_DIBUAT, MENYETUJUI1, TGL_MENYETUJUI1, MENYETUJUI2, TGL_MENYETUJUI2, DITERIMA_1, TGL_DITERIMA_1, DITERIMA_2, TGL_DITERIMA_2, DITERIMA_3, TGL_DITERIMA_3, STATUS, REVISI FROM TR_FORM1_GDR WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(TR_FORM1_GDR trform1gdr)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO TR_FORM1_GDR(NO_FORM, KODE_FORM, JENIS, TGL_REQUEST, ID_DEPT, KD_BRAND, DEPT_STORE_MALL, KOTA, ALOKASI_BUDGET, JADWAL_IMAGE, UTK_TOKO, JADWAL_ACARA, JADWAL_BUKA_TOKO, RFR_LAMPIRAN1, RFR_LAMPIRAN2, RFR_LAMPIRAN3, RFR_LAMPIRAN4, JADWAL_SELESAI_DESAIN, JADWAL_PRODUKSI_CETAK, JADWAL_KIRIM, JADWAL_PASANG, NAMA, JABATAN, ALAMAT_KIRIM, NO_TELP, EMAIL, DIBUAT, TGL_DIBUAT, MENYETUJUI1, TGL_MENYETUJUI1, MENYETUJUI2, TGL_MENYETUJUI2, DITERIMA_1, TGL_DITERIMA_1, DITERIMA_2, TGL_DITERIMA_2, DITERIMA_3, TGL_DITERIMA_3, STATUS, REVISI) VALUES (@NO_FORM, @KODE_FORM, @JENIS, @TGL_REQUEST, @ID_DEPT, @KD_BRAND, @DEPT_STORE_MALL, @KOTA, @ALOKASI_BUDGET, @JADWAL_IMAGE, @UTK_TOKO, @JADWAL_ACARA, @JADWAL_BUKA_TOKO, @RFR_LAMPIRAN1, @RFR_LAMPIRAN2, @RFR_LAMPIRAN3, @RFR_LAMPIRAN4, @JADWAL_SELESAI_DESAIN, @JADWAL_PRODUKSI_CETAK, @JADWAL_KIRIM, @JADWAL_PASANG, @NAMA, @JABATAN, @ALAMAT_KIRIM, @NO_TELP, @EMAIL, @DIBUAT, @TGL_DIBUAT, @MENYETUJUI1, @TGL_MENYETUJUI1, @MENYETUJUI2, @TGL_MENYETUJUI2, @DITERIMA_1, @TGL_DITERIMA_1, @DITERIMA_2, @TGL_DITERIMA_2, @DITERIMA_3, @TGL_DITERIMA_3, @STATUS, @REVISI)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdr.NO_FORM;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = trform1gdr.KODE_FORM;
                    command.Parameters.Add("@JENIS", SqlDbType.VarChar).Value = trform1gdr.JENIS;
                    command.Parameters.Add("@TGL_REQUEST", SqlDbType.DateTime).Value = trform1gdr.TGL_REQUEST;
                    command.Parameters.Add("@ID_DEPT", SqlDbType.Int).Value = trform1gdr.ID_DEPT;
                    command.Parameters.Add("@KD_BRAND", SqlDbType.VarChar).Value = trform1gdr.KD_BRAND;
                    command.Parameters.Add("@DEPT_STORE_MALL", SqlDbType.VarChar).Value = trform1gdr.DEPT_STORE_MALL;
                    command.Parameters.Add("@KOTA", SqlDbType.VarChar).Value = trform1gdr.KOTA;
                    command.Parameters.Add("@ALOKASI_BUDGET", SqlDbType.DateTime).Value = trform1gdr.ALOKASI_BUDGET;
                    command.Parameters.Add("@JADWAL_IMAGE", SqlDbType.DateTime).Value = trform1gdr.JADWAL_IMAGE;
                    command.Parameters.Add("@UTK_TOKO", SqlDbType.VarChar).Value = trform1gdr.UTK_TOKO;
                    command.Parameters.Add("@JADWAL_ACARA", SqlDbType.DateTime).Value = trform1gdr.JADWAL_ACARA;
                    command.Parameters.Add("@JADWAL_BUKA_TOKO", SqlDbType.DateTime).Value = trform1gdr.JADWAL_BUKA_TOKO;
                    command.Parameters.Add("@RFR_LAMPIRAN1", SqlDbType.VarChar).Value = trform1gdr.RFR_LAMPIRAN1;
                    command.Parameters.Add("@RFR_LAMPIRAN2", SqlDbType.VarChar).Value = trform1gdr.RFR_LAMPIRAN2;
                    command.Parameters.Add("@RFR_LAMPIRAN3", SqlDbType.VarChar).Value = trform1gdr.RFR_LAMPIRAN3;
                    command.Parameters.Add("@RFR_LAMPIRAN4", SqlDbType.VarChar).Value = trform1gdr.RFR_LAMPIRAN4;
                    command.Parameters.Add("@JADWAL_SELESAI_DESAIN", SqlDbType.DateTime).Value = trform1gdr.JADWAL_SELESAI_DESAIN;
                    command.Parameters.Add("@JADWAL_PRODUKSI_CETAK", SqlDbType.DateTime).Value = trform1gdr.JADWAL_PRODUKSI_CETAK;
                    command.Parameters.Add("@JADWAL_KIRIM", SqlDbType.DateTime).Value = trform1gdr.JADWAL_KIRIM;
                    command.Parameters.Add("@JADWAL_PASANG", SqlDbType.DateTime).Value = trform1gdr.JADWAL_PASANG;
                    command.Parameters.Add("@NAMA", SqlDbType.VarChar).Value = trform1gdr.NAMA;
                    command.Parameters.Add("@JABATAN", SqlDbType.VarChar).Value = trform1gdr.JABATAN;
                    command.Parameters.Add("@ALAMAT_KIRIM", SqlDbType.VarChar).Value = trform1gdr.ALAMAT_KIRIM;
                    command.Parameters.Add("@NO_TELP", SqlDbType.VarChar).Value = trform1gdr.NO_TELP;
                    command.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = trform1gdr.EMAIL;
                    command.Parameters.Add("@DIBUAT", SqlDbType.VarChar).Value = trform1gdr.DIBUAT;
                    command.Parameters.Add("@TGL_DIBUAT", SqlDbType.DateTime).Value = trform1gdr.TGL_DIBUAT;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform1gdr.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform1gdr.TGL_MENYETUJUI1;
                    command.Parameters.Add("@MENYETUJUI2", SqlDbType.VarChar).Value = trform1gdr.MENYETUJUI2;
                    command.Parameters.Add("@TGL_MENYETUJUI2", SqlDbType.DateTime).Value = trform1gdr.TGL_MENYETUJUI2;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform1gdr.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform1gdr.TGL_DITERIMA_1;
                    command.Parameters.Add("@DITERIMA_2", SqlDbType.VarChar).Value = trform1gdr.DITERIMA_2;
                    command.Parameters.Add("@TGL_DITERIMA_2", SqlDbType.DateTime).Value = trform1gdr.TGL_DITERIMA_2;
                    command.Parameters.Add("@DITERIMA_3", SqlDbType.VarChar).Value = trform1gdr.DITERIMA_3;
                    command.Parameters.Add("@TGL_DITERIMA_3", SqlDbType.DateTime).Value = trform1gdr.TGL_DITERIMA_3;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform1gdr.STATUS;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform1gdr.REVISI;
                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
            }
            finally
            {
                CnString.Close();
            }
        }

        public void Update(TR_FORM1_GDR trform1gdr)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM1_GDR SET KODE_FORM = @KODE_FORM, JENIS = @JENIS, TGL_REQUEST = @TGL_REQUEST, ID_DEPT = @ID_DEPT, KD_BRAND = @KD_BRAND, DEPT_STORE_MALL = @DEPT_STORE_MALL, KOTA = @KOTA, ALOKASI_BUDGET = @ALOKASI_BUDGET, JADWAL_IMAGE = @JADWAL_IMAGE, UTK_TOKO = @UTK_TOKO, JADWAL_ACARA = @JADWAL_ACARA, JADWAL_BUKA_TOKO = @JADWAL_BUKA_TOKO, RFR_LAMPIRAN1 = @RFR_LAMPIRAN1, RFR_LAMPIRAN2 = @RFR_LAMPIRAN2, RFR_LAMPIRAN3 = @RFR_LAMPIRAN3, RFR_LAMPIRAN4 = @RFR_LAMPIRAN4, JADWAL_SELESAI_DESAIN = @JADWAL_SELESAI_DESAIN, JADWAL_PRODUKSI_CETAK = @JADWAL_PRODUKSI_CETAK, JADWAL_KIRIM = @JADWAL_KIRIM, JADWAL_PASANG = @JADWAL_PASANG, NAMA = @NAMA, JABATAN = @JABATAN, ALAMAT_KIRIM = @ALAMAT_KIRIM, NO_TELP = @NO_TELP, EMAIL = @EMAIL, DIBUAT = @DIBUAT, TGL_DIBUAT = @TGL_DIBUAT, MENYETUJUI1 = @MENYETUJUI1, TGL_MENYETUJUI1 = @TGL_MENYETUJUI1, MENYETUJUI2 = @MENYETUJUI2, TGL_MENYETUJUI2 = @TGL_MENYETUJUI2, DITERIMA_1 = @DITERIMA_1, TGL_DITERIMA_1 = @TGL_DITERIMA_1, DITERIMA_2 = @DITERIMA_2, TGL_DITERIMA_2 = @TGL_DITERIMA_2, DITERIMA_3 = @DITERIMA_3, TGL_DITERIMA_3 = @TGL_DITERIMA_3, STATUS = @STATUS, REVISI = @REVISI WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdr.NO_FORM;
                    command.Parameters.Add("@KODE_FORM", SqlDbType.VarChar).Value = trform1gdr.KODE_FORM;
                    command.Parameters.Add("@JENIS", SqlDbType.VarChar).Value = trform1gdr.JENIS;
                    command.Parameters.Add("@TGL_REQUEST", SqlDbType.DateTime).Value = trform1gdr.TGL_REQUEST;
                    command.Parameters.Add("@ID_DEPT", SqlDbType.Int).Value = trform1gdr.ID_DEPT;
                    command.Parameters.Add("@KD_BRAND", SqlDbType.VarChar).Value = trform1gdr.KD_BRAND;
                    command.Parameters.Add("@DEPT_STORE_MALL", SqlDbType.VarChar).Value = trform1gdr.DEPT_STORE_MALL;
                    command.Parameters.Add("@KOTA", SqlDbType.VarChar).Value = trform1gdr.KOTA;
                    command.Parameters.Add("@ALOKASI_BUDGET", SqlDbType.DateTime).Value = trform1gdr.ALOKASI_BUDGET;
                    command.Parameters.Add("@JADWAL_IMAGE", SqlDbType.DateTime).Value = trform1gdr.JADWAL_IMAGE;
                    command.Parameters.Add("@UTK_TOKO", SqlDbType.VarChar).Value = trform1gdr.UTK_TOKO;
                    command.Parameters.Add("@JADWAL_ACARA", SqlDbType.DateTime).Value = trform1gdr.JADWAL_ACARA;
                    command.Parameters.Add("@JADWAL_BUKA_TOKO", SqlDbType.DateTime).Value = trform1gdr.JADWAL_BUKA_TOKO;
                    command.Parameters.Add("@RFR_LAMPIRAN1", SqlDbType.VarChar).Value = trform1gdr.RFR_LAMPIRAN1;
                    command.Parameters.Add("@RFR_LAMPIRAN2", SqlDbType.VarChar).Value = trform1gdr.RFR_LAMPIRAN2;
                    command.Parameters.Add("@RFR_LAMPIRAN3", SqlDbType.VarChar).Value = trform1gdr.RFR_LAMPIRAN3;
                    command.Parameters.Add("@RFR_LAMPIRAN4", SqlDbType.VarChar).Value = trform1gdr.RFR_LAMPIRAN4;
                    command.Parameters.Add("@JADWAL_SELESAI_DESAIN", SqlDbType.DateTime).Value = trform1gdr.JADWAL_SELESAI_DESAIN;
                    command.Parameters.Add("@JADWAL_PRODUKSI_CETAK", SqlDbType.DateTime).Value = trform1gdr.JADWAL_PRODUKSI_CETAK;
                    command.Parameters.Add("@JADWAL_KIRIM", SqlDbType.DateTime).Value = trform1gdr.JADWAL_KIRIM;
                    command.Parameters.Add("@JADWAL_PASANG", SqlDbType.DateTime).Value = trform1gdr.JADWAL_PASANG;
                    command.Parameters.Add("@NAMA", SqlDbType.VarChar).Value = trform1gdr.NAMA;
                    command.Parameters.Add("@JABATAN", SqlDbType.VarChar).Value = trform1gdr.JABATAN;
                    command.Parameters.Add("@ALAMAT_KIRIM", SqlDbType.VarChar).Value = trform1gdr.ALAMAT_KIRIM;
                    command.Parameters.Add("@NO_TELP", SqlDbType.VarChar).Value = trform1gdr.NO_TELP;
                    command.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = trform1gdr.EMAIL;
                    command.Parameters.Add("@DIBUAT", SqlDbType.VarChar).Value = trform1gdr.DIBUAT;
                    command.Parameters.Add("@TGL_DIBUAT", SqlDbType.DateTime).Value = trform1gdr.TGL_DIBUAT;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform1gdr.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform1gdr.TGL_MENYETUJUI1;
                    command.Parameters.Add("@MENYETUJUI2", SqlDbType.VarChar).Value = trform1gdr.MENYETUJUI2;
                    command.Parameters.Add("@TGL_MENYETUJUI2", SqlDbType.DateTime).Value = trform1gdr.TGL_MENYETUJUI2;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform1gdr.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform1gdr.TGL_DITERIMA_1;
                    command.Parameters.Add("@DITERIMA_2", SqlDbType.VarChar).Value = trform1gdr.DITERIMA_2;
                    command.Parameters.Add("@TGL_DITERIMA_2", SqlDbType.DateTime).Value = trform1gdr.TGL_DITERIMA_2;
                    command.Parameters.Add("@DITERIMA_3", SqlDbType.VarChar).Value = trform1gdr.DITERIMA_3;
                    command.Parameters.Add("@TGL_DITERIMA_3", SqlDbType.DateTime).Value = trform1gdr.TGL_DITERIMA_3;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform1gdr.STATUS;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform1gdr.REVISI;
                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
            }
            finally
            {
                CnString.Close();
            }
        }

        public void UpdateDibuat(TR_FORM1_GDR trform1gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM1_GDR SET STATUS = @STATUS, DIBUAT = @DIBUAT, TGL_DIBUAT = @TGL_DIBUAT WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform1gdr.STATUS;
                    command.Parameters.Add("@DIBUAT", SqlDbType.VarChar).Value = trform1gdr.DIBUAT;
                    command.Parameters.Add("@TGL_DIBUAT", SqlDbType.DateTime).Value = trform1gdr.TGL_DIBUAT;
                    command.ExecuteScalar();

                    command.ExecuteScalar();
                }

            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
            }
            finally
            {
                CnString.Close();
            }
        }

        public void UpdateMenyetujui1(TR_FORM1_GDR trform1gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM1_GDR SET STATUS = @STATUS, MENYETUJUI1 = @MENYETUJUI1, TGL_MENYETUJUI1 = @TGL_MENYETUJUI1 WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform1gdr.STATUS;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform1gdr.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform1gdr.TGL_MENYETUJUI1;
                    command.ExecuteScalar();
                   
                    command.ExecuteScalar();
                }

            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
            }
            finally
            {
                CnString.Close();
            }
        }

        public void UpdateMenyetujui2(TR_FORM1_GDR trform1gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM1_GDR SET STATUS = @STATUS, MENYETUJUI2 = @MENYETUJUI2, TGL_MENYETUJUI2 = @TGL_MENYETUJUI2 WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform1gdr.STATUS;
                    command.Parameters.Add("@MENYETUJUI2", SqlDbType.VarChar).Value = trform1gdr.MENYETUJUI2;
                    command.Parameters.Add("@TGL_MENYETUJUI2", SqlDbType.DateTime).Value = trform1gdr.TGL_MENYETUJUI2;
                    command.ExecuteScalar();

                    command.ExecuteScalar();
                }

            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
            }
            finally
            {
                CnString.Close();
            }
        }

        public void UpdateDiterima1(TR_FORM1_GDR trform1gdr)
        {
            string newId = "Berhasil";
            try
            {
                  string query = String.Format("UPDATE TR_FORM1_GDR SET STATUS = @STATUS, DITERIMA_1 = @DITERIMA_1, TGL_DITERIMA_1 = @TGL_DITERIMA_1 WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform1gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform1gdr.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform1gdr.TGL_DITERIMA_1;
                    command.ExecuteScalar();

                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
            }
            finally
            {
                CnString.Close();
            }
        }

        public void UpdateDiterima2(TR_FORM1_GDR trform1gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM1_GDR SET STATUS = @STATUS, DITERIMA_2 = @DITERIMA_2, TGL_DITERIMA_2 = @TGL_DITERIMA_2 WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform1gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_2", SqlDbType.VarChar).Value = trform1gdr.DITERIMA_2;
                    command.Parameters.Add("@TGL_DITERIMA_2", SqlDbType.DateTime).Value = trform1gdr.TGL_DITERIMA_2;
                    command.ExecuteScalar();

                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
            }
            finally
            {
                CnString.Close();
            }
        }

        public void UpdateDiterima3(TR_FORM1_GDR trform1gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM1_GDR SET STATUS = @STATUS, DITERIMA_3 = @DITERIMA_3, TGL_DITERIMA_3 = @TGL_DITERIMA_3 WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform1gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_3", SqlDbType.VarChar).Value = trform1gdr.DITERIMA_3;
                    command.Parameters.Add("@TGL_DITERIMA_3", SqlDbType.DateTime).Value = trform1gdr.TGL_DITERIMA_3;
                    command.ExecuteScalar();

                    command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
            }
            finally
            {
                CnString.Close();
            }
        }

        public void UpdateRevisiMenyetujui1(TR_FORM1_GDR trform1gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM1_GDR SET STATUS = @STATUS, MENYETUJUI1 = @MENYETUJUI1, TGL_MENYETUJUI1 = @TGL_MENYETUJUI1, REVISI = @REVISI WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform1gdr.STATUS;
                    command.Parameters.Add("@MENYETUJUI1", SqlDbType.VarChar).Value = trform1gdr.MENYETUJUI1;
                    command.Parameters.Add("@TGL_MENYETUJUI1", SqlDbType.DateTime).Value = trform1gdr.TGL_MENYETUJUI1;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform1gdr.REVISI;
                    command.ExecuteScalar();

                    command.ExecuteScalar();
                }

            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
            }
            finally
            {
                CnString.Close();
            }
        }

        public void UpdateRevisiMenyetujui2(TR_FORM1_GDR trform1gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM1_GDR SET STATUS = @STATUS, MENYETUJUI2 = @MENYETUJUI2, TGL_MENYETUJUI2 = @TGL_MENYETUJUI2, REVISI = @REVISI WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform1gdr.STATUS;
                    command.Parameters.Add("@MENYETUJUI2", SqlDbType.VarChar).Value = trform1gdr.MENYETUJUI2;
                    command.Parameters.Add("@TGL_MENYETUJUI2", SqlDbType.DateTime).Value = trform1gdr.TGL_MENYETUJUI2;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform1gdr.REVISI;
                    command.ExecuteScalar();

                    command.ExecuteScalar();
                }

            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
            }
            finally
            {
                CnString.Close();
            }
        }

        public void UpdateRevisiDiterima1(TR_FORM1_GDR trform1gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM1_GDR SET STATUS = @STATUS, DITERIMA_1 = @DITERIMA_1, TGL_DITERIMA_1 = @TGL_DITERIMA_1, REVISI = @REVISI WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform1gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_1", SqlDbType.VarChar).Value = trform1gdr.DITERIMA_1;
                    command.Parameters.Add("@TGL_DITERIMA_1", SqlDbType.DateTime).Value = trform1gdr.TGL_DITERIMA_1;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform1gdr.REVISI;
                    command.ExecuteScalar();

                    command.ExecuteScalar();
                }

            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
            }
            finally
            {
                CnString.Close();
            }
        }

        public void UpdateRevisiDiterima2(TR_FORM1_GDR trform1gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM1_GDR SET STATUS = @STATUS, DITERIMA_2 = @DITERIMA_2, TGL_DITERIMA_2 = @TGL_DITERIMA_2, REVISI = @REVISI WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform1gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_2", SqlDbType.VarChar).Value = trform1gdr.DITERIMA_2;
                    command.Parameters.Add("@TGL_DITERIMA_2", SqlDbType.DateTime).Value = trform1gdr.TGL_DITERIMA_2;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform1gdr.REVISI;
                    command.ExecuteScalar();

                    command.ExecuteScalar();
                }

            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
            }
            finally
            {
                CnString.Close();
            }
        }

        public void UpdateRevisiDiterima3(TR_FORM1_GDR trform1gdr)
        {
            string newId = "Berhasil";
            try
            {
                string query = String.Format("UPDATE TR_FORM1_GDR SET STATUS = @STATUS, DITERIMA_3 = @DITERIMA_3, TGL_DITERIMA_3 = @TGL_DITERIMA_3, REVISI = @REVISI WHERE NO_FORM = @NO_FORM ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdr.NO_FORM;
                    command.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = trform1gdr.STATUS;
                    command.Parameters.Add("@DITERIMA_3", SqlDbType.VarChar).Value = trform1gdr.DITERIMA_3;
                    command.Parameters.Add("@TGL_DITERIMA_3", SqlDbType.DateTime).Value = trform1gdr.TGL_DITERIMA_3;
                    command.Parameters.Add("@REVISI", SqlDbType.VarChar).Value = trform1gdr.REVISI;
                    command.ExecuteScalar();

                    command.ExecuteScalar();
                }

            }
            catch (Exception ex)
            {
                newId = "ERROR: " + ex.Message;
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM TR_FORM1_GDR  WHERE NO_FORM = @NO_FORM"), CnString))
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM TR_FORM1_GDR WHERE {0} ", Where), CnString))
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
            }
            finally
            {
                CnString.Close();
            }
        }
    }
}
