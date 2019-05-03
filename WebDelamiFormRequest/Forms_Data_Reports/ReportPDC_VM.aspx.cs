using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDelamiFormRequest.DataLayer;

namespace WebDelamiFormRequest.Forms_Data_Reports
{
    public partial class ReportPDC_VM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            if (Session["Username"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            HfUsername.Value = Session["Username"].ToString();
            CheckUrutan();
            //gvMain.DataSourceID = "CT_GridFormAll";
            gvMain.DataBind();
        }


        #region All Function

        public void CheckUrutan()
        {

            try
            {
                LoadGridFormAll();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        public void LoadGridFormAll()
        {
            try
            {

                gvMain.DataSourceID = "CT_GridFormPDC_VM";
                gvMain.DataBind();

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        #endregion

        #region gridview

        //Devexpress
        protected void gvMain_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                string commandName = e.CommandArgs.CommandName;
                if (commandName.ToLower() != "page")
                {
                    //GridViewRow grv = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    //int rowIndex = grv.RowIndex;
                    object id = e.KeyValue;
                    string KodeForm = gvMain.GetRowValuesByKeyValue(id, "KODE_FORM").ToString();
                    string NoForm = gvMain.GetRowValuesByKeyValue(id, "NO_FORM").ToString();
                    //string NoForm = gvMain.DataKeys[rowIndex]["NO_FORM"].ToString();

                    DataSet DsUser = new DataSet();
                    MS_USER_DA MsUserDA = new MS_USER_DA();

                    //Mendapatkan Urutan User Berdasarkan Jabatan
                    DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KodeForm);
                    string PAGE_NAME = "";
                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        HfPagesName.Value = PAGE_NAME;
                    }

                    if (commandName == "ClickRow")
                    {
                        HfNO_FORM.Value = NoForm;
                        var url = "../Forms_Data_Process/" + HfPagesName.Value + "?NO_FORM=" + HfNO_FORM.Value;
                        string redirect = "<script>window.open('" + url + "');</script>";
                        Response.Write(redirect);

                    }

                    if (commandName == "ActivityRow")
                    {
                        HfNO_FORM.Value = NoForm;
                        var url = "../Forms_Data_Reports/ReportActivityForm.aspx?NO_FORM=" + HfNO_FORM.Value;
                        string redirect = "<script>window.open('" + url + "');</script>";
                        Response.Write(redirect);
                    }
                }
            }
            catch (Exception ex)
            {
                DivMessage.InnerText = ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }


        //protected void gvMainRowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName.ToLower() != "page")
        //        {
        //            GridViewRow grv = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
        //            int rowIndex = grv.RowIndex;
        //            string NoForm = gvMain.DataKeys[rowIndex]["NO_FORM"].ToString();

        //            if (e.CommandName == "ClickRow")
        //            {
        //                HfNO_FORM.Value = NoForm;


        //                Response.Redirect("~/Forms_Data/" + HfPagesName.Value + "?NO_FORM=" + HfNO_FORM.Value);
        //            }

        //            if (e.CommandName == "ActivityRow")
        //            {
        //                HfNO_FORM.Value = NoForm;


        //                Response.Redirect("~/Reports/ReportActivityForm.aspx?NO_FORM=" + HfNO_FORM.Value);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DivMessage.InnerText = ex.Message;
        //        DivMessage.Attributes["class"] = "error";
        //        //DivMessage.Attributes["class"] = "success";
        //        DivMessage.Visible = true;
        //    }

        //}

        //protected void gvMainPageChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvMain.PageIndex = e.NewPageIndex;
        //}

        #endregion

    }
}