﻿using System;
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
    public class TR_FORM1_GDR_MATERI_DA
    {
        private static string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection CnString = new SqlConnection(conn);

        public virtual DataSet GetDataAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID_MATERI, NO_FORM, JENIS_MATERIAL_CETAK, UKURAN, JUMLAH_A4, JUMLAH_A5, JUMLAH_QTY, PENJELASAN FROM TR_FORM1_GDR_MATERI"), CnString))
            {
                command.CommandType = CommandType.Text;
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public virtual DataSet GetDataByKey(String ID_MATERI)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID_MATERI, NO_FORM, JENIS_MATERIAL_CETAK, UKURAN, JUMLAH_A4, JUMLAH_A5, JUMLAH_QTY, PENJELASAN FROM TR_FORM1_GDR_MATERI WHERE ID_MATERI = @ID_MATERI"), CnString))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@ID_MATERI", ID_MATERI);
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
            using (SqlCommand command = new SqlCommand(string.Format("SELECT ID_MATERI, NO_FORM, JENIS_MATERIAL_CETAK, UKURAN, JUMLAH_A4, JUMLAH_A5, JUMLAH_QTY, PENJELASAN FROM TR_FORM1_GDR_MATERI WHERE {0} ", Where), CnString))
            {
                CnString.Open();

                adapter.SelectCommand = command;
                adapter.Fill(dataSet, "SearchData");
                CnString.Close();
            }
            return dataSet;
        }

        public void Insert(TR_FORM1_GDR_MATERI trform1gdrmateri)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("INSERT INTO TR_FORM1_GDR_MATERI(ID_MATERI, NO_FORM, JENIS_MATERIAL_CETAK, UKURAN, JUMLAH_A4, JUMLAH_A5, JUMLAH_QTY, PENJELASAN) VALUES (@ID_MATERI, @NO_FORM, @JENIS_MATERIAL_CETAK, @UKURAN, @JUMLAH_A4, @JUMLAH_A5, @JUMLAH_QTY, @PENJELASAN)");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID_MATERI", SqlDbType.Int).Value = trform1gdrmateri.ID_MATERI;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdrmateri.NO_FORM;
                    command.Parameters.Add("@JENIS_MATERIAL_CETAK", SqlDbType.VarChar).Value = trform1gdrmateri.JENIS_MATERIAL_CETAK;
                    command.Parameters.Add("@UKURAN", SqlDbType.VarChar).Value = trform1gdrmateri.UKURAN;
                    command.Parameters.Add("@JUMLAH_A4", SqlDbType.Decimal).Value = trform1gdrmateri.JUMLAH_A4;
                    command.Parameters.Add("@JUMLAH_A5", SqlDbType.Decimal).Value = trform1gdrmateri.JUMLAH_A5;
                    command.Parameters.Add("@JUMLAH_QTY", SqlDbType.Decimal).Value = trform1gdrmateri.JUMLAH_QTY;
                    command.Parameters.Add("@PENJELASAN", SqlDbType.VarChar).Value = trform1gdrmateri.PENJELASAN;
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

        public void Update(TR_FORM1_GDR_MATERI trform1gdrmateri)
        {
            string newId = "Berhasil";
            try
            {

                string query = String.Format("UPDATE TR_FORM1_GDR_MATERI SET NO_FORM = @NO_FORM, JENIS_MATERIAL_CETAK = @JENIS_MATERIAL_CETAK, UKURAN = @UKURAN, JUMLAH_A4 = @JUMLAH_A4, JUMLAH_A5 = @JUMLAH_A5, JUMLAH_QTY = @JUMLAH_QTY, PENJELASAN = @PENJELASAN WHERE ID_MATERI = @ID_MATERI ");
                CnString.Open();

                using (SqlCommand command = new SqlCommand(query, CnString))
                {
                    command.Parameters.Add("@ID_MATERI", SqlDbType.Int).Value = trform1gdrmateri.ID_MATERI;
                    command.Parameters.Add("@NO_FORM", SqlDbType.VarChar).Value = trform1gdrmateri.NO_FORM;
                    command.Parameters.Add("@JENIS_MATERIAL_CETAK", SqlDbType.VarChar).Value = trform1gdrmateri.JENIS_MATERIAL_CETAK;
                    command.Parameters.Add("@UKURAN", SqlDbType.VarChar).Value = trform1gdrmateri.UKURAN;
                    command.Parameters.Add("@JUMLAH_A4", SqlDbType.Decimal).Value = trform1gdrmateri.JUMLAH_A4;
                    command.Parameters.Add("@JUMLAH_A5", SqlDbType.Decimal).Value = trform1gdrmateri.JUMLAH_A5;
                    command.Parameters.Add("@JUMLAH_QTY", SqlDbType.Decimal).Value = trform1gdrmateri.JUMLAH_QTY;
                    command.Parameters.Add("@PENJELASAN", SqlDbType.VarChar).Value = trform1gdrmateri.PENJELASAN;
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

        public void DeleteByKey(String ID_MATERI)
        {
            string newId = "Berhasil";
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();
                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM TR_FORM1_GDR_MATERI  WHERE ID_MATERI = @ID_MATERI"), CnString))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@ID_MATERI", ID_MATERI);
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
                using (SqlCommand command = new SqlCommand(string.Format("DELETE  FROM TR_FORM1_GDR_MATERI WHERE {0} ", Where), CnString))
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
