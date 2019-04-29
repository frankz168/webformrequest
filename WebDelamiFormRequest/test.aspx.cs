using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;

namespace WebDelamiFormRequest
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile != null)
            {
                try
                {
                 
                    string path = string.Concat(Server.MapPath("~/Files/" + FileUpload1.FileName));
                    FileUpload1.SaveAs(path);
                    // Connection String to Excel Workbook
                    string excelCS = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", path);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataSet dataSet = new DataSet();
                    //using (OleDbConnection con = new OleDbConnection(excelCS))
                    //{
                    //    OleDbCommand cmd = new OleDbCommand("select * from [Sheet1$]", con);
                    //    con.Open();

                    //    // Create DbDataReader to Data Worksheet
                    //    DbDataReader dr = cmd.ExecuteReader();
                    //    // SQL Server Connection String
                    //    string CS = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

                    //    // Bulk Copy to SQL Server 
                    //    SqlBulkCopy bulkInsert = new SqlBulkCopy(CS);
                    //    bulkInsert.DestinationTableName = "Persons";
                    //    bulkInsert.WriteToServer(dr);
                    //}

                    using (OleDbConnection con = new OleDbConnection(excelCS))
                    {
                        //OleDbConnection con = new OleDbConnection(connectionString);
                        con.Open();
                        string str = @"SELECT  [Id],[Name] FROM  [Sheet1$]";
                        OleDbCommand com = new OleDbCommand();
                        com = new OleDbCommand(str, con);
                        OleDbDataAdapter oledbda = new OleDbDataAdapter();
                        oledbda = new OleDbDataAdapter(com);
                        DataSet ds = new DataSet();
                        ds = new DataSet();
                        oledbda.Fill(ds, "[Sheet1$]");
                        con.Close();
                        System.Data.DataTable dt = new System.Data.DataTable();
                        dt = ds.Tables["[Sheet1$]"];
                    }
                }
                catch (Exception)
                {
                    
                }
            }
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //string message = "Selected Item: " + DropDownList1.SelectedItem.Text;
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('" + message + "');", true);
        }
    }
}