using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using WebDelamiFormRequest.DataLayer;
using WebDelamiFormRequest.Domain;
using System.Collections;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace WebDelamiFormRequest.Forms_Data_Process
{
    public partial class I_FormRequestGDR_Others : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dtToko = new DataTable();
        public string KODE_FORM = Common.KD_FORM_OTHERS;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _NO_FORM = Request.QueryString["NO_FORM"];

            if (Page.IsPostBack)
            {
                gvCustCt.DataBind();
                return;

            }
            else
            {
                TR_FORM_GDR_CUST_TEMP_DA TrFormGdrCustTemp = new DataLayer.TR_FORM_GDR_CUST_TEMP_DA();
                TrFormGdrCustTemp.DeleteFilter(string.Format("KODE_FORM = '{0}' AND NO_FORM = '{1}'", KODE_FORM, _NO_FORM));
                gvCustCt.DataBind();
            }


            if (Session["Username"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            LoadDataGridCustCt();
            LoadDetailGridMateri();

            if (_NO_FORM == null)
            {
                StartFirstTime();
            }
            else
            {
                HfNO_FORM.Value = _NO_FORM;
                HfID_DEPT.Value = Convert.ToString(Session["ID_DEPT"].ToString());
                HfUsername.Value = Session["Username"].ToString();
                LoadDataFormRequestGDR();
                CheckDdrJenisToko();
                ChangeButtonColor();
            }

            HfID_DEPT.Value = Convert.ToString(Session["ID_DEPT"].ToString());
            HfUsername.Value = Session["Username"].ToString();

            btn_uploadfilestore.Attributes["onchange"] = "UploadFile(this)";
            btn_uploadfilematerial.Attributes["onchange"] = "UploadFileMaterial(this)";
        }

        protected void Upload(object sender, EventArgs e)
        {
            if (Session["btn_uploadfilestore"] == null && btn_uploadfilestore.HasFile)
            {

                Session["btn_uploadfilestore"] = btn_uploadfilestore;
                link_filenameuploadstore.Text = btn_uploadfilestore.FileName;
                //btn_uploadfilestore = (FileUpload)Session["btn_uploadfilestore"];

            }

            // Next time submit and Session has values but FileUpload is Blank

            // Return the values from session to FileUpload
            else if (Session["btn_uploadfilestore"] != null && (!btn_uploadfilestore.HasFile))
            {

                btn_uploadfilestore = (FileUpload)Session["btn_uploadfilestore"];
                link_filenameuploadstore.Text = btn_uploadfilestore.FileName;
                //btn_uploadfilestore = (FileUpload)Session["btn_uploadfilestore"];

            }

            // Now there could be another sictution when Session has File but user want to change the file

            // In this case we have to change the file in session object

            else if (btn_uploadfilestore.HasFile)
            {

                Session["btn_uploadfilestore"] = btn_uploadfilestore;
                link_filenameuploadstore.Text = btn_uploadfilestore.FileName;
                //btn_uploadfilestore = (FileUpload)Session["btn_uploadfilestore"];

            }

            UploadStore();

        }

        protected void UploadMaterial(object sender, EventArgs e)
        {
            if (Session["btn_uploadfilematerial"] == null && btn_uploadfilematerial.HasFile)
            {

                Session["btn_uploadfilematerial"] = btn_uploadfilematerial;
                link_filenameuploadmaterial.Text = btn_uploadfilematerial.FileName;

            }

            // Next time submit and Session has values but FileUpload is Blank

            // Return the values from session to FileUpload
            else if (Session["btn_uploadfilematerial"] != null && (!btn_uploadfilematerial.HasFile))
            {

                btn_uploadfilematerial = (FileUpload)Session["btn_uploadfilematerial"];
                link_filenameuploadmaterial.Text = btn_uploadfilematerial.FileName;

            }

            // Now there could be another sictution when Session has File but user want to change the file

            // In this case we have to change the file in session object

            else if (btn_uploadfilematerial.HasFile)
            {

                Session["btn_uploadfilematerial"] = btn_uploadfilematerial;
                link_filenameuploadmaterial.Text = btn_uploadfilematerial.FileName;

            }

            UploadSiteAndContent();

        }

        protected void ddljenis_SelectedIndexChanged1(object sender, EventArgs e)
        {
            LoadDataFormMSCustToGridCustCt();
            CheckDdrJenisToko();
            link_filenameuploadstore.Text = "-";
        }

        protected void ddlphotographer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlphotographer.Text == EYesNo.Yes)
            //{
            //    ddldigitalimaging.Text = EYesNo.Yes;
            //}

            CalculateWorkingDaysRequiredDate();
            CalculateWorkingDaysDetailDate();
        }

        protected void ddlproduction_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateWorkingDaysRequiredDate();
            CalculateWorkingDaysDetailDate();
        }

        protected void ddldigitalimaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateWorkingDaysRequiredDate();
            CalculateWorkingDaysDetailDate();
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("L_FormRequestGDR_Others.aspx");
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "move_up()", true);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                SaveFormRequestGDR();
            }
            else
            {

            }

        }

        protected void btn_UpdateSubmit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "move_up()", true);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                UpdateSubmitFormRequestGDR();
                SendEmailAllType();
            }
            else
            {

            }


        }

        protected void btn_UpdateSubmitDesign_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "move_up()", true);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                UpdateSubmitFormRequestDesignGDR();
                SendEmailAllType();
            }
            else
            {

            }


        }

        protected void btn_Reject_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "move_up()", true);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                UpdateCancelAll();
            }
            else
            {

            }

        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Approved_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "move_up()", true);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                UpdateApproveAll();
            }
            else
            {

            }

        }

        protected void btn_ToRevise_Click(object sender, EventArgs e)
        {
            Pnl_Revisi.Visible = true;
            //btn_ToRevise.Visible = false;
            btn_Revise.Visible = true;
        }

        protected void btn_Revise_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "move_up()", true);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                UpdateReviseAll();
            }
            else
            {

            }

        }

        protected void btn_ToReviseDesign_Click(object sender, EventArgs e)
        {
            Pnl_RevisiDesign.Visible = true;
            //btn_ToReviseDesign.Visible = false;
            btn_ReviseDesign.Visible = true;
        }

        protected void btn_ReviseDesign_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "move_up()", true);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                UpdateReviseDesignAll();
            }
            else
            {

            }


        }

        protected void btn_ReviseContent_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "move_up()", true);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                if (ddlditerima2.Text != "Choose")
                {
                    UpdateSubmitFormRequestReviseContentGDR();

                }
                else
                {
                    DivMessage.InnerText = "Graphic Design Harus Diisi";
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }
            }
            else
            {

            }
        }

        protected void btn_Accepted_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "move_up()", true);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                UpdateAcceptedAll();
            }
            else
            {

            }


        }

        protected void btn_Done_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "move_up()", true);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //UpdateStatusDoneHeadDesign();
            }
            else
            {

            }


        }

        protected void btn_AddData_Click(object sender, EventArgs e)
        {
            AddToDetailMateri();
        }

        protected void btn_Close_Click(object sender, EventArgs e)
        {

        }

        protected void btn_CloseRevise_Click(object sender, EventArgs e)
        {

        }

        protected void btn_CloseReviseDesign_Click(object sender, EventArgs e)
        {

        }

        protected void btnFilenameStoreClear_Click(object sender, EventArgs e)
        {
            TR_FORM_GDR_CUST_TEMP_DA TrFormGdrCustTemp = new DataLayer.TR_FORM_GDR_CUST_TEMP_DA();

            string NO_FORM = text_noform.Text;
            link_filenameuploadstore.Text = "-";
            string Where = string.Format("KODE_FORM = '{0}' AND NO_FORM = '{1}'", KODE_FORM, NO_FORM);
            TrFormGdrCustTemp.DeleteFilter(Where);
            gvCustCt.DataBind();
            DivMessage.Visible = false;
        }

        protected void btnFilenamematerialClear_Click(object sender, EventArgs e)
        {
            LoadDetailGridMateri();
            string noform = text_noform.Text;
            link_filenameuploadmaterial.Text = "-";
            DivMessage.Visible = false;
        }

        protected void linkbtn_filename1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename1.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUpload/")
                    + linkbtn_filename1.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected void linkbtn_filename2_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename2.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUpload/")
                    + linkbtn_filename2.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected void linkbtn_filename3_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename3.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUpload/")
                    + linkbtn_filename3.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected void linkbtn_filename4_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename4.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUpload/")
                    + linkbtn_filename4.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        protected void linkbtn_filenamegd1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filenamegd1.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadGD/")
                    + linkbtn_filenamegd1.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected void linkbtn_filenamegd2_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filenamegd2.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadGD/")
                    + linkbtn_filenamegd2.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected void linkbtn_filenamegd3_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filenamegd3.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadGD/")
                    + linkbtn_filenamegd3.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected void linkbtn_filenamegd4_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filenamegd4.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadGD/")
                    + linkbtn_filenamegd4.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        protected void linkbtn_filenamepg1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filenamepg1.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadPG/")
                    + linkbtn_filenamepg1.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected void linkbtn_filenamepg2_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filenamepg2.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadPG/")
                    + linkbtn_filenamepg2.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected void linkbtn_filenamepg3_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filenamepg3.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadPG/")
                    + linkbtn_filenamepg3.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected void linkbtn_filenamepg4_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filenamepg4.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadPG/")
                    + linkbtn_filenamepg4.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected void linkbtn_filenamedi1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filenamedi1.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadDI/")
                    + linkbtn_filenamedi1.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected void linkbtn_filenamedi2_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filenamedi2.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadDI/")
                    + linkbtn_filenamedi2.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected void linkbtn_filenamedi3_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filenamedi3.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadDI/")
                    + linkbtn_filenamedi3.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected void linkbtn_filenamedi4_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filenamedi4.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadDI/")
                    + linkbtn_filenamedi4.Text);
                Response.End();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = "File Not Available";
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        protected void linkbtn_deletefilename1_Click(object sender, EventArgs e)
        {
            string filepath = linkbtn_filename1.Text;

            if (File.Exists(filepath))
            {

                File.Delete(filepath);

            }
        }

        protected void radio_yesjadwalpergantianimage_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_yesjadwalpergantianimage.Checked == true)
            {
                Pnl_JadwalPergantianImage.Visible = true;
                text_jadwalpergantianimage.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void radio_nojadwalpergantianimage_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_nojadwalpergantianimage.Checked == true)
            {
                Pnl_JadwalPergantianImage.Visible = false;
                text_jadwalpergantianimage.Text = new DateTime(1900, 01, 01).ToString();
            }
        }

        #region gridview
        protected void gvMainPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
        }

        protected void gvMainRowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[0].Text;
                foreach (Button button in e.Row.Cells[6].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                    }
                }
            }
        }

        protected void gvMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["MateriCetak"] as DataTable;
            dt.Rows[index].Delete();
            ViewState["MateriCetak"] = dt;
            BindGrid();
        }

        protected void gvMain_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMain.EditIndex = -1;
            BindGrid();

            //Bind data to the GridView control.
            gvMain.DataBind();
        }

        protected void gvMain_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            GridViewRow row = (GridViewRow)gvMain.Rows[e.RowIndex];
            string Id = ((TextBox)row.Cells[0].Controls[0]).Text;
            string jenis = ((TextBox)row.Cells[4].Controls[0]).Text;
            string ukuran = ((TextBox)row.Cells[5].Controls[0]).Text;
            string material = ((TextBox)row.Cells[6].Controls[0]).Text;
            string jumlahQTY = ((TextBox)row.Cells[7].Controls[0]).Text;
            string penjelasan = ((TextBox)row.Cells[8].Controls[0]).Text;
            string noform = Common.KD_FORM_OTHERS;
            DataTable dt = (DataTable)ViewState["MateriCetak"];

            dt.Rows[row.RowIndex]["JENIS_MATERIAL_CETAK"] = jenis;
            dt.Rows[row.RowIndex]["UKURAN"] = ukuran;
            dt.Rows[row.RowIndex]["MATERIAL"] = material;
            dt.Rows[row.RowIndex]["JUMLAH_QTY"] = jumlahQTY;
            dt.Rows[row.RowIndex]["PENJELASAN"] = penjelasan;
            ViewState["dt"] = dt;
            gvMain.EditIndex = -1;
            this.BindGrid();

        }

        protected void gvMain_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMain.EditIndex = e.NewEditIndex;
            //LoadDetailGridMateri();
            BindGrid();
        }


        protected void gvCustCt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustCt.PageIndex = e.NewPageIndex;
        }
        protected void gvCustCt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //header select all function
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ((CheckBox)e.Row.FindControl("chkHeader")).Attributes.Add("onclick",
                    "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("chkHeader")).ClientID + "')");
            }

        }

        #endregion

        #region All Functions

        //Action Upload Data Store And Content
        public void UploadStore()
        {
            string NO_FORM = text_noform.Text;
            TR_FORM_GDR_CUST_TEMP_DA TrFormGdrCustTemp = new DataLayer.TR_FORM_GDR_CUST_TEMP_DA();
            TrFormGdrCustTemp.DeleteFilter(string.Format("KODE_FORM = '{0}' AND NO_FORM = '{1}'", KODE_FORM, NO_FORM));

            DataLayer.MS_CUST_CT_DA MsCustCt = new DataLayer.MS_CUST_CT_DA();

            if (btn_uploadfilestore.PostedFile != null)
            {
                try
                {

                    string ext = System.IO.Path.GetExtension(this.btn_uploadfilestore.PostedFile.FileName).ToLower();

                    if (ext != ".jpg" || ext != ".png" || ext != ".gif" || ext != ".jpeg" || ext != ".pdf")
                    {
                        DivMessage.InnerText = "Format File Tidak Ada. Harap File Berformat(.xlsx)";
                        DivMessage.Attributes["class"] = "error";
                        //DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                        link_filenameuploadstore.Text = "-";
                        return;
                    }

                    string path = string.Concat(Server.MapPath("~/Files/" + btn_uploadfilestore.FileName));
                    btn_uploadfilestore.SaveAs(path);
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
                    //    string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                    //    // Bulk Copy to SQL Server 
                    //    SqlBulkCopy bulkInsert = new SqlBulkCopy(CS);
                    //    bulkInsert.DestinationTableName = "Persons";
                    //    bulkInsert.WriteToServer(dr);
                    //}

                    using (OleDbConnection con = new OleDbConnection(excelCS))
                    {
                        //OleDbConnection con = new OleDbConnection(ConnectionString);
                        con.Open();
                        string str = @"SELECT [Site] FROM  [Sheet1$]";
                        OleDbCommand com = new OleDbCommand();
                        com = new OleDbCommand(str, con);
                        OleDbDataAdapter oledbda = new OleDbDataAdapter();
                        oledbda = new OleDbDataAdapter(com);
                        DataSet Ds = new DataSet();
                        Ds = new DataSet();
                        oledbda.Fill(Ds, "[Sheet1$]");
                        con.Close();
                        System.Data.DataTable dt = new System.Data.DataTable();
                        dt = Ds.Tables["[Sheet1$]"];

                        string Site = "";
                        string kode_cust = "";
                        string kode_ct = "";
                        string nama_cust = "";
                        string nama_ct = "";

                        //if (Ds.Tables[0].Rows.Count > 0)
                        //{
                        //    Site = Convert.ToString(Ds.Tables[0].Rows[0]["Site"].ToString());
                        //}

                        int i = 0;
                        int index = 0;
                        foreach (DataRow Item in Ds.Tables[0].Rows)
                        {
                            index = i;
                            i++;

                            //double? Site = Item.Field<double?>("Site");
                            //if (Site.HasValue)
                            //{
                            //    Site = Item.Field<double?>("Site");
                            //}
                            //else
                            //{
                            //    Site = 0;
                            //}

                            if (!String.IsNullOrEmpty((Item.Field<String>("Site"))))
                            {
                                Site = Item.Field<String>("Site");
                            }
                            else
                            {
                                Site = "";
                            }


                            if (Site != "")
                            {
                                Ds = MsCustCt.GetDataFilter(string.Format("site = '{0}'", Site));

                                if (Ds.Tables[0].Rows.Count > 0)
                                {
                                    Site = Convert.ToString(Ds.Tables[0].Rows[0]["site"].ToString());
                                    kode_cust = Convert.ToString(Ds.Tables[0].Rows[0]["kode_cust"].ToString());
                                    kode_ct = Convert.ToString(Ds.Tables[0].Rows[0]["kode_ct"].ToString());
                                    nama_cust = Convert.ToString(Ds.Tables[0].Rows[0]["nama_cust"].ToString());
                                    nama_ct = Convert.ToString(Ds.Tables[0].Rows[0]["nama_ct"].ToString());
                                }

                                //int colsCount = dtToko.Columns.Count;
                                //dtToko.Rows.Add(index, KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct);
                                //gvCustCt.DataBind();


                                int ID = index;
                                string KODE_CUST = "";
                                string KODE_CT = "";
                                string SITE = "";
                                string NAMA_CUST = "";
                                string NAMA_CT = "";

                                KODE_FORM = Common.KD_FORM_OTHERS;
                                NO_FORM = text_noform.Text;
                                KODE_CUST = kode_cust;
                                KODE_CT = kode_ct;
                                SITE = Site;
                                NAMA_CUST = nama_cust;
                                NAMA_CT = nama_ct;

                                TR_FORM_GDR_CUST_TEMP trformgdrcusttemp = new TR_FORM_GDR_CUST_TEMP();
                                trformgdrcusttemp.KODE_FORM = KODE_FORM;
                                trformgdrcusttemp.NO_FORM = NO_FORM;
                                trformgdrcusttemp.kode_cust = KODE_CUST;
                                trformgdrcusttemp.kode_ct = KODE_CT;
                                trformgdrcusttemp.site = Convert.ToString(SITE);
                                trformgdrcusttemp.nama_cust = NAMA_CUST;
                                trformgdrcusttemp.nama_ct = NAMA_CT;
                                TrFormGdrCustTemp.Insert(trformgdrcusttemp);
                                gvCustCt.DataBind();
                            }


                        }

                        gvCustCt.Selection.SelectAll();
                        gvCustCt.DataBind();
                        DivMessage.Visible = false;
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                    DivMessage.InnerText = "Wrong Input The File Type";
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }
            }

        }

        public void UploadSiteAndContent()
        {
            LoadDetailGridMateri();
            string NO_FORM = text_noform.Text;
            DataLayer.MS_CUST_CT_DA MsCustCt = new DataLayer.MS_CUST_CT_DA();

            if (btn_uploadfilematerial.PostedFile != null)
            {
                try
                {

                    string ext = System.IO.Path.GetExtension(this.btn_uploadfilematerial.PostedFile.FileName).ToLower();

                    if (ext != ".jpg" || ext != ".png" || ext != ".gif" || ext != ".jpeg" || ext != ".pdf")
                    {
                        DivMessage.InnerText = "Format File Tidak Ada. Harap File Berformat(.xlsx)";
                        DivMessage.Attributes["class"] = "error";
                        //DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                        link_filenameuploadmaterial.Text = "-";
                        return;
                    }

                    string path = string.Concat(Server.MapPath("~/Files/" + btn_uploadfilematerial.FileName));
                    btn_uploadfilematerial.SaveAs(path);
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
                    //    string CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                    //    // Bulk Copy to SQL Server 
                    //    SqlBulkCopy bulkInsert = new SqlBulkCopy(CS);
                    //    bulkInsert.DestinationTableName = "Persons";
                    //    bulkInsert.WriteToServer(dr);
                    //}

                    using (OleDbConnection con = new OleDbConnection(excelCS))
                    {
                        //OleDbConnection con = new OleDbConnection(ConnectionString);
                        con.Open();
                        string str = @"SELECT [Site], [Content], [Size], [Material], [Qty], [Note] FROM  [Sheet1$]";
                        OleDbCommand com = new OleDbCommand();
                        com = new OleDbCommand(str, con);
                        OleDbDataAdapter oledbda = new OleDbDataAdapter();
                        oledbda = new OleDbDataAdapter(com);
                        DataSet Ds = new DataSet();
                        Ds = new DataSet();
                        oledbda.Fill(Ds, "[Sheet1$]");
                        con.Close();
                        System.Data.DataTable dt = new System.Data.DataTable();
                        dt = Ds.Tables["[Sheet1$]"];

                        string Site = "";
                        string nama_cust = "";
                        string jenis = "";
                        string ukuran = "";
                        string material = "";
                        string penjelasan = "";

                        //if (Ds.Tables[0].Rows.Count > 0)
                        //{
                        //    Site = Convert.ToString(Ds.Tables[0].Rows[0]["Site"].ToString());
                        //}

                        int i = 0;
                        int index = 0;
                        foreach (DataRow Item in Ds.Tables[0].Rows)
                        {
                            index = i;
                            i++;

                            if (!String.IsNullOrEmpty((Item.Field<String>("Site"))))
                            {
                                Site = Item.Field<String>("Site");
                            }
                            else
                            {
                                Site = "";
                                nama_cust = "";
                            }

                            //double? Site = Item.Field<double?>("Site");
                            //if (Site.HasValue)
                            //{
                            //    Site = Item.Field<double?>("Site");
                            //}
                            //else
                            //{
                            //    Site = 0;
                            //    nama_cust = "";
                            //}

                            if (!String.IsNullOrEmpty((Item.Field<String>("Content"))))
                            {
                                jenis = Item.Field<String>("Content");
                            }
                            else
                            {
                                jenis = "";
                            }

                            //double? Size = Item.Field<double?>("Size");
                            //if (Size.HasValue)
                            //{
                            //    Size = Item.Field<double?>("Size");
                            //}
                            //else
                            //{
                            //    Size = 0;
                            //}

                            if (!String.IsNullOrEmpty((Item.Field<String>("Size"))))
                            {
                                ukuran = Item.Field<String>("Size");
                            }
                            else
                            {
                                ukuran = "";
                            }

                            if (!String.IsNullOrEmpty((Item.Field<String>("Material"))))
                            {
                                material = Item.Field<String>("Material");
                            }
                            else
                            {
                                material = "";
                            }

                            //double? jumlahQTY = Item.Field<double?>("Qty");
                            //if (jumlahQTY.HasValue)
                            //{
                            //    jumlahQTY = Item.Field<double?>("Qty");
                            //}
                            //else
                            //{
                            //    jumlahQTY = 0;
                            //}
                            string jumlahQty = "";
                            if (!String.IsNullOrEmpty((Item.Field<String>("Qty"))))
                            {
                                jumlahQty = Item.Field<String>("Qty");
                            }
                            else
                            {
                                jumlahQty = "";
                            }


                            if (!String.IsNullOrEmpty((Item.Field<String>("Note"))))
                            {
                                penjelasan = Item.Field<String>("Note");
                            }
                            else
                            {
                                penjelasan = "";
                            }


                            Ds = MsCustCt.GetDataFilter(string.Format("site = '{0}'", Site));

                            if (Ds.Tables[0].Rows.Count > 0)
                            {
                                Site = Convert.ToString(Ds.Tables[0].Rows[0]["site"].ToString());
                                nama_cust = Convert.ToString(Ds.Tables[0].Rows[0]["nama_cust"].ToString());
                            }


                            //int colsCount = dtToko.Columns.Count;
                            //dtToko.Rows.Add(index, KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct);
                            //gvCustCt.DataBind();

                            int ID = index;
                            string NAMA_CUST = "";
                            string SITE = Site;

                            KODE_FORM = Common.KD_FORM_OTHERS;
                            NO_FORM = text_noform.Text;
                            NAMA_CUST = nama_cust;

                            DataTable dts = (DataTable)ViewState["MateriCetak"];
                            dts.Rows.Add("1", NO_FORM, SITE, NAMA_CUST, jenis, ukuran, material, jumlahQty, penjelasan);
                            ViewState["MateriCetak"] = dts;
                            this.BindGrid();

                        }
                        DivMessage.Visible = false;

                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                    DivMessage.InnerText = "Wrong Input The File Type";
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }
            }
        }


        // Action User
        public void StartFirstTime()
        {
            try
            {
                CreateNoFormGDR();
                string NO_FORM = text_noform.Text;
                TR_FORM_GDR_CUST_TEMP_DA TrFormGdrCustTemp = new DataLayer.TR_FORM_GDR_CUST_TEMP_DA();
                TrFormGdrCustTemp.DeleteFilter(string.Format("KODE_FORM = '{0}' AND NO_FORM = '{1}'", KODE_FORM, NO_FORM));
                Pnl_JadwalPergantianImage.Visible = true;
                Pnl_Acarabktoko.Visible = false;
                label_departmentvalue.Text = Session["DepartemenName"].ToString();
                text_brand.Text = Session["BrandName"].ToString();
                text_tanggalrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_tanggalrequired.Enabled = false;
                text_alokasibudget.Text = DateTime.Now.ToString("yyMM");
                text_jadwalpergantianimage.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_jadwalacarabktoko.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_jadwalselesaidisain.Text = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                text_jadwalkirim.Text = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                text_jadwalproduksi.Text = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                text_jadwalfoto.Text = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                text_jadwaldi.Text = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                text_jadwaladmcreative.Text = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                text_jadwalpergantianimage.Enabled = false;
                text_jadwalacarabktoko.Enabled = true;
                text_jadwalselesaidisain.Enabled = false;
                text_jadwalkirim.Enabled = false;
                text_jadwalproduksi.Enabled = false;
                text_jadwalfoto.Enabled = false;
                text_jadwaldi.Enabled = false;
                text_jadwaladmcreative.Enabled = false;
                text_dibuat.Text = Session["Username"].ToString();
                ddlditerima2.Text = "Choose";
                Pnl_Created.Visible = true;
                //Pnl_Production.Enabled = false;
                btn_UpdateSubmit.Visible = false;
                btn_UpdateSubmitDesign.Visible = false;
                CalculateWorkingDaysRequiredDate();
                CalculateWorkingDaysDetailDate();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void CheckDdrJenisToko()
        {
            try
            {
                if (ddljenis.SelectedValue == "OPENING_STORE" || ddljenis.SelectedValue == "BAZZAR" || ddljenis.SelectedValue == "OPENING_COUNTER")
                {
                    Pnl_Acarabktoko.Visible = true;
                    text_jadwalacarabktoko.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    Pnl_Acarabktoko.Visible = false;
                    text_jadwalacarabktoko.Text = new DateTime(1900, 01, 01).ToString();

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void LoadDataFormRequestGDR()
        {
            try
            {
                DataSet Ds = new DataSet();
                DataSet DsDept = new DataSet();
                DataSet DsBrand = new DataSet();
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();
                MS_JABATAN_DA MsJabatanDA = new MS_JABATAN_DA();
                MS_DEPT_DA MsDeptDA = new MS_DEPT_DA();
                BRAND_DA BrandDA = new BRAND_DA();
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();

                Ds = TrForm3Gdr.GetDataByKey(HfNO_FORM.Value);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    text_noform.Text = Convert.ToString(Ds.Tables[0].Rows[0]["NO_FORM"].ToString());
                    ddlpermintaandesign.Text = Convert.ToString(Ds.Tables[0].Rows[0]["PERMINTAAN_DESIGN"].ToString());
                    LoadDataDetailKategoriPermintaan();

                    ddlphotographer.Text = Convert.ToString(Ds.Tables[0].Rows[0]["PHOTOGRAPHER"].ToString());
                    string PHOTOGRAPHER = "";
                    //Check Photographer Yes / No
                    if (ddlphotographer.Text == EYesNo.Yes)
                    {
                        HfPhotoGrapher.Value = EYesNo.Yes;
                    }
                    else
                    {
                        HfPhotoGrapher.Value = EYesNo.No;
                    }
                    ddldigitalimaging.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DIGITAL_IMAGING"].ToString());
                    ddljenis.Text = Convert.ToString(Ds.Tables[0].Rows[0]["JENIS"].ToString());
                    text_tanggalrequest.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_REQUEST"].ToString()).ToString("yyyy-MM-dd");
                    if (text_tanggalrequest.Text == "1900-01-01")
                    {
                        text_tanggalrequest.Text = "";
                    }
                    text_tanggalrequired.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_REQUIRED"].ToString()).ToString("yyyy-MM-dd");
                    if (text_tanggalrequired.Text == "1900-01-01")
                    {
                        text_tanggalrequired.Text = "";
                    }
                    string KODE_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string KD_BRAND = Convert.ToString(Ds.Tables[0].Rows[0]["KD_BRAND"].ToString());
                    DsDept = MsDeptDA.GetDataFilter(string.Format("KODE_DEPT = '{0}'", KODE_DEPT));
                    if (DsDept.Tables[0].Rows.Count > 0)
                    {
                        label_departmentvalue.Text = Convert.ToString(DsDept.Tables[0].Rows[0]["DEPT"].ToString());
                    }


                    DsBrand = BrandDA.GetDataByKey(KD_BRAND);
                    if (DsBrand.Tables[0].Rows.Count > 0)
                    {
                        text_brand.Text = Convert.ToString(DsBrand.Tables[0].Rows[0]["BRAND"].ToString());
                    }
                    text_alokasibudget.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["ALOKASI_BUDGET"].ToString()).ToString("yyMM");
                    text_jadwalpergantianimage.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["JADWAL_IMAGE"].ToString()).ToString("yyyy-MM-dd");
                    if (text_jadwalpergantianimage.Text == "1900-01-01")
                    {
                        text_jadwalpergantianimage.Text = "";
                    }

                    text_jadwalacarabktoko.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["JADWAL_ACARABKTOKO"].ToString()).ToString("yyyy-MM-dd");
                    if (text_jadwalacarabktoko.Text == "1900-01-01")
                    {
                        text_jadwalacarabktoko.Text = "";
                    }
                    text_referensidesign.Text = Convert.ToString(Ds.Tables[0].Rows[0]["REFERENSI_DESIGN"].ToString());

                    //Lampiran file store and material
                    link_filenameuploadstore.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN_STORE"].ToString());
                    link_filenameuploadmaterial.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN_MATERIAL"].ToString());

                    //Lampiran File Ada 4
                    linkbtn_filename1.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN1"].ToString());
                    linkbtn_filename2.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN2"].ToString());
                    linkbtn_filename3.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN3"].ToString());
                    linkbtn_filename4.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN4"].ToString());


                    text_jadwalselesaidisain.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["JADWAL_SELESAI_DESAIN"].ToString()).ToString("yyyy-MM-dd");

                    if (text_jadwalselesaidisain.Text == "1900-01-01")
                    {
                        text_jadwalselesaidisain.Text = "";
                    }

                    text_jadwalproduksi.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["JADWAL_PRODUKSI_CETAK"].ToString()).ToString("yyyy-MM-dd");
                    if (text_jadwalproduksi.Text == "1900-01-01")
                    {
                        text_jadwalproduksi.Text = "";
                    }
                    text_jadwalkirim.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["JADWAL_KIRIM"].ToString()).ToString("yyyy-MM-dd");
                    if (text_jadwalkirim.Text == "1900-01-01")
                    {
                        text_jadwalkirim.Text = "";
                    }
                    text_jadwalfoto.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["JADWAL_FOTO"].ToString()).ToString("yyyy-MM-dd");
                    if (text_jadwalfoto.Text == "1900-01-01")
                    {
                        text_jadwalfoto.Text = "";
                    }

                    text_jadwaldi.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["JADWAL_DI"].ToString()).ToString("yyyy-MM-dd");
                    if (text_jadwaldi.Text == "1900-01-01")
                    {
                        text_jadwaldi.Text = "";
                    }

                    text_jadwaladmcreative.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["JADWAL_ADM_CREATIVE"].ToString()).ToString("yyyy-MM-dd");
                    if (text_jadwaladmcreative.Text == "1900-01-01")
                    {
                        text_jadwaladmcreative.Text = "";
                    }

                    text_dibuat.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DIBUAT"].ToString());
                    text_tanggaldibuat.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DIBUAT"].ToString()).ToString("yyyy-MM-dd");
                    text_menyetujui1.Text = Convert.ToString(Ds.Tables[0].Rows[0]["MENYETUJUI1"].ToString());
                    text_tanggalmenyetujui1.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_MENYETUJUI1"].ToString()).ToString("yyyy-MM-dd");
                    if (text_tanggalmenyetujui1.Text == "1900-01-01")
                    {
                        text_tanggalmenyetujui1.Text = "";
                    }
                    text_diterima1.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_1"].ToString());
                    text_tanggalditerima1.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DITERIMA_1"].ToString()).ToString("yyyy-MM-dd");
                    if (text_tanggalditerima1.Text == "1900-01-01")
                    {
                        text_tanggalditerima1.Text = "";
                    }
                    if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_2"].ToString())))
                    {
                        ddlditerima2.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_2"].ToString());
                    }
                    else
                    {
                        ddlditerima2.Text = "Choose";
                    }

                    text_tanggalditerima2.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DITERIMA_2"].ToString()).ToString("yyyy-MM-dd");
                    if (text_tanggalditerima2.Text == "1900-01-01")
                    {
                        text_tanggalditerima2.Text = "";
                    }
                    text_diterima3.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_3"].ToString());
                    text_tanggalditerima3.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DITERIMA_3"].ToString()).ToString("yyyy-MM-dd");
                    if (text_tanggalditerima3.Text == "1900-01-01")
                    {
                        text_tanggalditerima3.Text = "";
                    }
                    text_diterimalain1.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_LAIN_1"].ToString());
                    text_tanggalditerimalain1.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DITERIMA_LAIN_1"].ToString()).ToString("yyyy-MM-dd");
                    if (text_tanggalditerimalain1.Text == "1900-01-01")
                    {
                        text_tanggalditerimalain1.Text = "";
                    }
                    text_diterimalain2.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_LAIN_2"].ToString());
                    text_tanggalditerimalain2.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DITERIMA_LAIN_2"].ToString()).ToString("yyyy-MM-dd");
                    if (text_tanggalditerimalain2.Text == "1900-01-01")
                    {
                        text_tanggalditerimalain2.Text = "";
                    }
                    text_diterimalain3.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_LAIN_3"].ToString());
                    text_tanggalditerimalain3.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DITERIMA_LAIN_3"].ToString()).ToString("yyyy-MM-dd");
                    if (text_tanggalditerimalain3.Text == "1900-01-01")
                    {
                        text_tanggalditerimalain3.Text = "";
                    }
                    text_diterimalain4.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_LAIN_4"].ToString());
                    text_tanggalditerimalain4.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DITERIMA_LAIN_4"].ToString()).ToString("yyyy-MM-dd");
                    if (text_tanggalditerimalain4.Text == "1900-01-01")
                    {
                        text_tanggalditerimalain4.Text = "";
                    }
                    text_diterimalain5materi.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_LAIN_5_MATERI"].ToString());
                    text_tanggalditerimalain5materi.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DITERIMA_LAIN_5_MATERI"].ToString()).ToString("yyyy-MM-dd");
                    if (text_tanggalditerimalain5materi.Text == "1900-01-01")
                    {
                        text_tanggalditerimalain5materi.Text = "";
                    }

                    text_diterimalain5.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_LAIN_5"].ToString());
                    text_tanggalditerimalain5.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DITERIMA_LAIN_5"].ToString()).ToString("yyyy-MM-dd");
                    if (text_tanggalditerimalain5.Text == "1900-01-01")
                    {
                        text_tanggalditerimalain5.Text = "";
                    }
                    label_statusvalue.Text = Convert.ToString(Ds.Tables[0].Rows[0]["STATUS"].ToString());
                    text_revisiload.Text = Convert.ToString(Ds.Tables[0].Rows[0]["REVISI"].ToString());
                    text_statusver.Text = Convert.ToString(Ds.Tables[0].Rows[0]["STATUS_VER"].ToString());
                    string Status = Convert.ToString(Ds.Tables[0].Rows[0]["STATUS"].ToString());

                    linkbtn_filenamegd1.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN5_GD"].ToString());
                    linkbtn_filenamegd2.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN6_GD"].ToString());
                    linkbtn_filenamegd3.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN7_GD"].ToString());
                    linkbtn_filenamegd4.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN8_GD"].ToString());

                    linkbtn_filenamepg1.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN1_PG"].ToString());
                    linkbtn_filenamepg2.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN2_PG"].ToString());
                    linkbtn_filenamepg3.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN3_PG"].ToString());
                    linkbtn_filenamepg4.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN4_PG"].ToString());

                    linkbtn_filenamedi1.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN1_DI"].ToString());
                    linkbtn_filenamedi2.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN2_DI"].ToString());
                    linkbtn_filenamedi3.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN3_DI"].ToString());
                    linkbtn_filenamedi4.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN4_DI"].ToString());

                    ddlproduction.Text = Convert.ToString(Ds.Tables[0].Rows[0]["PRODUCTION"].ToString());
                    text_commentar.Text = Convert.ToString(Ds.Tables[0].Rows[0]["REVISI"].ToString());

                    LoadDataDetailCustCt();
                    LoadDataDetailMateri();


                    //Mengambil Posisi CM. Karena CM Ada terdapat 3 action sekaligus.
                    string KodeJabatan = "";

                    Ds = MsUserDA.GetDataFilter(string.Format("USERNAME = '{0}'", HfUsername.Value));
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        KodeJabatan = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                    }

                    string ACTIONGENERAL = "";

                    if ((HfPhotoGrapher.Value == EYesNo.Yes || ddldigitalimaging.Text == EYesNo.Yes) && KodeJabatan == "CM")
                    {
                        if (Status == EApprovalStatus.ApprovedPhoto || Status == EApprovalStatus.OnRevisePhoto)
                        {
                            ACTIONGENERAL = "Approved-PhotoGrapher-Photo";
                        }
                        else if (Status == EApprovalStatus.ApprovedDI || Status == EApprovalStatus.OnRevisePhotoDI)
                        {
                            ACTIONGENERAL = "Approved-PhotoGrapher-DI";
                        }
                        else
                        {
                            ACTIONGENERAL = "Approved";
                        }
                    }
                    else if (HfPhotoGrapher.Value == EYesNo.No && KodeJabatan == "CM")
                    {
                        ACTIONGENERAL = "Approved";
                    }
                    else if (KodeJabatan == "USR")
                    {
                        ACTIONGENERAL = "Created";
                    }
                    else
                    {
                        ACTIONGENERAL = "Approved";
                    }

                    HfActionGeneral.Value = ACTIONGENERAL;

                    //Mendapatkan Urutan User Berdasarkan Jabatan
                    Ds = MsUserDA.GetDataUrutanUserCustom(HfUsername.Value, KODE_FORM, ACTIONGENERAL);

                    int URUTAN = 0;
                    string ACTION = "";
                    string PAGE_NAME = "";
                    string SP = "";
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        URUTAN = Convert.ToInt32(Ds.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION = Convert.ToString(Ds.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME = Convert.ToString(Ds.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP = Convert.ToString(Ds.Tables[0].Rows[0]["SP"].ToString());

                        ViewOnlyState();

                        if (URUTAN == 1)
                        {
                            btn_UpdateSubmitDesign.Visible = false;
                            if (Status == EApprovalStatus.OnApprovedHD)
                            {
                                btn_Save.Visible = false;
                                btn_UpdateSubmit.Enabled = false;
                                btn_Reject.Enabled = false;
                                btn_Cancel.Enabled = false;
                            }
                            else if (Status == EApprovalStatus.OnRevise)
                            {
                                Pnl_Forms.Enabled = true;
                                Pnl_KategoriPermintaan.Enabled = true;
                                Pnl_FormOthers2.Enabled = true;
                                Pnl_RevisiLoad.Visible = true;
                                Pnl_RevisiLoad.Enabled = false;
                                btn_Save.Visible = false;
                                btn_UpdateSubmit.Enabled = true;
                                btn_Reject.Enabled = true;
                                btn_Cancel.Enabled = false;
                            }
                            else if (Status == EApprovalStatus.Cancel)
                            {
                                btn_Save.Visible = false;
                                btn_UpdateSubmit.Enabled = false;
                                btn_Reject.Enabled = false;
                                btn_Cancel.Enabled = false;
                            }
                            else
                            {
                                btn_Save.Visible = false;
                                btn_UpdateSubmit.Enabled = false;
                                btn_Reject.Enabled = false;
                                btn_Cancel.Enabled = false;
                            }
                        }
                        else if (URUTAN == 2)
                        {
                            text_menyetujui1.Text = HfUsername.Value;
                            ShowButtonUrutan2();

                            if (Status == EApprovalStatus.OnApprovedHD)
                            {
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = true;
                                btn_Revise.Enabled = true;
                                btn_Reject.Enabled = true;
                            }
                            else if (Status == EApprovalStatus.ApprovedHD)
                            {
                                btn_Approved.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = false;
                            }
                            else if (Status == EApprovalStatus.OnRevise)
                            {
                                Pnl_RevisiLoad.Visible = true;
                                Pnl_RevisiLoad.Enabled = false;
                                btn_Approved.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = false;
                            }
                            else
                            {
                                btn_Approved.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = false;
                            }


                        }

                        else if (URUTAN == 3)
                        {
                            text_diterimalain1.Text = HfUsername.Value;
                            ShowButtonUrutan3();
                            if (Status == EApprovalStatus.ApprovedHDPhoto)
                            {
                                //ddlditerima2.Enabled = true;
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = true;
                                btn_Revise.Enabled = true;
                                //btn_Reject.Enabled = true;
                            }

                            else if (Status == EApprovalStatus.OnRevisePhoto)
                            {
                                Pnl_RevisiLoad.Visible = true;
                                Pnl_RevisiLoad.Enabled = false;
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = false;
                            }

                            else
                            {
                                btn_Approved.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = false;
                            }
                        }
                        else if (URUTAN == 4)
                        {
                            text_diterimalain2.Text = HfUsername.Value;
                            ShowButtonUrutan4();
                            if (Status == EApprovalStatus.ApprovedPhoto)
                            {
                                //ddlditerima2.Enabled = true;
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = true;
                                btn_Revise.Enabled = true;
                                //btn_Reject.Enabled = true;
                            }

                            else if (Status == EApprovalStatus.OnRevisePhoto)
                            {
                                Pnl_RevisiLoad.Visible = true;
                                Pnl_RevisiLoad.Enabled = false;
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = false;
                            }

                            else
                            {
                                btn_Approved.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = false;
                            }
                        }
                        else if (URUTAN == 5)
                        {
                            text_diterimalain3.Text = HfUsername.Value;
                            ShowButtonUrutan5();
                            if (Status == EApprovalStatus.ApprovedCreativeManagerPG || Status == EApprovalStatus.ApprovedHDDI)
                            {
                                //ddlditerima2.Enabled = true;
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = true;
                                btn_Revise.Enabled = true;
                                //btn_Reject.Enabled = true;
                            }

                            else if (Status == EApprovalStatus.OnRevisePhotoDI)
                            {
                                Pnl_RevisiLoad.Visible = true;
                                Pnl_RevisiLoad.Enabled = false;
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = false;
                            }

                            else
                            {
                                btn_Approved.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = false;
                            }
                        }
                        else if (URUTAN == 6)
                        {
                            text_diterimalain4.Text = HfUsername.Value;
                            ShowButtonUrutan6();
                            if (Status == EApprovalStatus.ApprovedDI)
                            {
                                //ddlditerima2.Enabled = true;
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = true;
                                btn_Revise.Enabled = true;
                                //btn_Reject.Enabled = true;
                            }

                            else if (Status == EApprovalStatus.OnRevisePhotoDI)
                            {
                                Pnl_RevisiLoad.Visible = true;
                                Pnl_RevisiLoad.Enabled = false;
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = false;
                            }

                            else
                            {
                                btn_Approved.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = false;
                            }
                        }
                        else if (URUTAN == 7)
                        {
                            text_diterima1.Text = HfUsername.Value;
                            ShowButtonUrutan7();
                            if (Status == EApprovalStatus.ApprovedHD || Status == EApprovalStatus.ApprovedCreativeManagerDI)
                            {
                                ddlditerima2.Enabled = true;

                                btn_Accepted.Enabled = true;
                                btn_ToRevise.Enabled = true;
                                btn_Revise.Enabled = true;
                                btn_ReviseContent.Enabled = true;
                                btn_Reject.Enabled = true;
                            }

                            else if (Status == EApprovalStatus.OnRevise)
                            {
                                Pnl_RevisiLoad.Visible = true;
                                Pnl_RevisiLoad.Enabled = false;
                                btn_Accepted.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_ReviseContent.Enabled = false;
                                btn_Reject.Enabled = false;
                            }
                            else if (Status == EApprovalStatus.DoneProduction)
                            {
                                ddlditerima2.Enabled = true;

                                btn_Accepted.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_ReviseContent.Enabled = true;
                                btn_Reject.Enabled = false;
                            }
                            else if (Status == EApprovalStatus.Done)
                            {
                                btn_Accepted.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_ReviseContent.Enabled = true;
                                btn_Reject.Enabled = false;
                            }
                            //else if (Status == EApprovalStatus.ApprovedCreativeManager)
                            //{
                            //    btn_Done.Visible = true;
                            //    btn_Done.Enabled = true;
                            //    btn_Accepted.Enabled = false;
                            //    btn_ToRevise.Enabled = false;
                            //    btn_Revise.Enabled = false;
                            //    btn_Reject.Enabled = false;
                            //}

                            else
                            {
                                btn_Accepted.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_ReviseContent.Enabled = false;
                                btn_Reject.Enabled = false;
                            }

                        }
                        else if (URUTAN == 8)
                        {
                            ddlditerima2.Text = HfUsername.Value;
                            ShowButtonUrutan8();
                            if (Status == EApprovalStatus.AcceptedHeadDesign || Status == EApprovalStatus.OnReviseContent)
                            {
                                btn_Accepted.Enabled = true;
                                btn_ToRevise.Enabled = true;
                                btn_Revise.Enabled = true;
                                btn_Reject.Enabled = true;
                            }

                            else if (Status == EApprovalStatus.OnReviseDesign)
                            {
                                Pnl_RevisiLoad.Visible = true;
                                Pnl_RevisiLoad.Enabled = false;
                                btn_UpdateSubmitDesign.Visible = true;
                                btn_UpdateSubmitDesign.Enabled = true;
                                btn_Accepted.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = false;
                            }
                            else
                            {
                                btn_Accepted.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = false;
                            }
                        }
                        else if (URUTAN == 9)
                        {
                            text_diterima3.Text = HfUsername.Value;
                            ShowButtonUrutan9();
                            if (Status == EApprovalStatus.AcceptedGraphicDesign)
                            {
                                btn_Accepted.Enabled = true;
                                btn_ToReviseDesign.Enabled = true;
                                btn_ReviseDesign.Enabled = true;
                                btn_Reject.Enabled = true;
                            }

                            else if (Status == EApprovalStatus.OnReviseDesign)
                            {
                                Pnl_RevisiLoad.Visible = true;
                                Pnl_RevisiLoad.Enabled = false;
                                btn_Accepted.Enabled = false;
                                btn_ToReviseDesign.Enabled = false;
                                btn_ReviseDesign.Enabled = false;
                                btn_Reject.Enabled = false;
                            }
                            else
                            {
                                btn_Accepted.Enabled = false;
                                btn_ToReviseDesign.Enabled = false;
                                btn_ReviseDesign.Enabled = false;
                                btn_Reject.Enabled = false;
                            }
                        }
                        else if (URUTAN == 10)
                        {
                            //text_diterimalain5.Text = HfUsername.Value;
                            ShowButtonUrutan10();
                            if (Status == EApprovalStatus.DoneProduction)
                            {
                                text_diterimalain5materi.Text = HfUsername.Value;
                                btn_Accepted.Enabled = true;
                                btn_Accepted.Text = "Update Cetak Materi Produksi";
                            }
                            else if (Status == EApprovalStatus.DoneProductionCetakMateri)
                            {
                                text_diterimalain5.Text = HfUsername.Value;
                                btn_Accepted.Enabled = true;
                                btn_Accepted.Text = "Update-Status-Production-Distribusi";
                            }
                            else
                            {
                                btn_Accepted.Enabled = false;
                            }
                        }

                    }

                }

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void CreateNoFormGDR()
        {
            try
            {
                string where = string.Format("NO_FORM <> '' ORDER BY NO_FORM DESC");

                string NO_FORM = "";
                DataSet Ds = new DataSet();
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();

                DataSet DsForm = new DataSet();
                MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                string FORM_TYPE = "";
                DsForm = msformda.GetDataByKey(KODE_FORM);
                if (DsForm.Tables[0].Rows.Count > 0)
                {
                    FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                }

                Ds = TrForm3Gdr.GetDataFilter(where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    string noform = Ds.Tables[0].Rows[0]["NO_FORM"].ToString();
                    int noformangka = Convert.ToInt32(noform.Substring(noform.Length - 4));
                    decimal angkaakhir = Convert.ToDecimal(noformangka) + 1;
                    NO_FORM = "FORM-" + "GDR-" + FORM_TYPE + "-" + angkaakhir.ToString("0000");
                    text_noform.Text = NO_FORM;
                    label_statusvalue.Text = "New Data";
                }
                else
                {
                    NO_FORM = "FORM-GDR-" + FORM_TYPE + "-0001";
                    text_noform.Text = NO_FORM;
                    label_statusvalue.Text = "New Data";

                }

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        public void LoadStatusVer()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;

                Ds = TrForm3Gdr.GetDataByKey(NO_FORM);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    HfStatusVer.Value = Ds.Tables[0].Rows[0]["STATUS_VER"].ToString();
                }

                else
                {
                    HfStatusVer.Value = "";
                }

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SaveFormRequestGDR()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string KODE_FORM = Common.KD_FORM_OTHERS;
                string PERMINTAAN_DESIGN = ddlpermintaandesign.SelectedValue.ToString();
                string JENIS = ddljenis.Text;
                DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                string ID_DEPT = Convert.ToString(Session["ID_DEPT"].ToString());
                string KD_BRAND = Session["KD_BRAND"].ToString();
                DateTime? ALOKASI_BUDGET = DateTime.ParseExact(text_alokasibudget.Text, "yyMM", null);
                DateTime? JADWAL_IMAGE = string.IsNullOrEmpty(text_jadwalpergantianimage.Text) ? (DateTime?)null : DateTime.Parse(text_jadwalpergantianimage.Text);
                DateTime? JADWAL_ACARABKTOKO = string.IsNullOrEmpty(text_jadwalacarabktoko.Text) ? (DateTime?)null : DateTime.Parse(text_jadwalacarabktoko.Text);
                string REFERENSI_DESIGN = text_referensidesign.Text;
                string RFR_LAMPIRAN_STORE = link_filenameuploadstore.Text;
                string RFR_LAMPIRAN_MATERIAL = link_filenameuploadmaterial.Text;
                string RFR_LAMPIRAN1 = "";
                string RFR_LAMPIRAN2 = "";
                string RFR_LAMPIRAN3 = "";
                string RFR_LAMPIRAN4 = "";
                string JADWAL_SELESAI_DESAIN = "";
                if (text_jadwalselesaidisain.Text != "")
                {
                    JADWAL_SELESAI_DESAIN = DateTime.Parse(text_jadwalselesaidisain.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_SELESAI_DESAIN = "1900-01-01";
                }

                string JADWAL_PRODUKSI_CETAK = "";
                if (text_jadwalproduksi.Text != "")
                {
                    JADWAL_PRODUKSI_CETAK = DateTime.Parse(text_jadwalproduksi.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_PRODUKSI_CETAK = "1900-01-01";
                }


                string JADWAL_KIRIM = "";
                if (text_jadwalkirim.Text != "")
                {
                    JADWAL_KIRIM = DateTime.Parse(text_jadwalkirim.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_KIRIM = "1900-01-01";
                }

                string JADWAL_FOTO = "";
                if (text_jadwalfoto.Text != "")
                {
                    JADWAL_FOTO = DateTime.Parse(text_jadwalfoto.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_FOTO = "1900-01-01";
                }

                string JADWAL_DI = "";
                if (text_jadwaldi.Text != "")
                {
                    JADWAL_DI = DateTime.Parse(text_jadwaldi.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_DI = "1900-01-01";
                }

                string JADWAL_ADM_CREATIVE = "";
                if (text_jadwaladmcreative.Text != "")
                {
                    JADWAL_ADM_CREATIVE = DateTime.Parse(text_jadwaladmcreative.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_ADM_CREATIVE = "1900-01-01";
                }

                string DIBUAT = text_dibuat.Text;
                DateTime TGL_DIBUAT = DateTime.Now;
                DateTime startdate = new DateTime(1900, 01, 01);
                DateTime temp;

                if (checkbox_hording.Checked == true || checkbox_lighboxposter.Checked == true || checkbox_poster.Checked == true || checkbox_others1Others.Checked == true || checkbox_others2Others.Checked == true || checkbox_others3Others.Checked == true || checkbox_mediacetak.Checked == true || checkbox_digitaladvertising.Checked == true || checkbox_socialmedia.Checked == true || checkbox_other.Checked == true)
                {
                    if (DateTime.TryParse(text_tanggalrequired.Text, out temp))
                    {
                        DateTime TANGGAL_REQUIRED = Convert.ToDateTime(text_tanggalrequired.Text);
                        var todaysDate = DateTime.Today;
                        int result = DateTime.Compare(TANGGAL_REQUIRED, todaysDate);

                        if (result > 0)
                        {
                            TR_FORM_GDR_CUST_DA TrFormGdrCust = new DataLayer.TR_FORM_GDR_CUST_DA();

                            int totalgridjenis = 0;
                            //Check Store/Counter Or Dealers
                            if (JENIS == "OPENING_STORE")
                            {

                                for (var i = 0; i < gvCustCt.VisibleRowCount; ++i)
                                {
                                    if (gvCustCt.Selection.IsRowSelected(i))
                                    {
                                        totalgridjenis = 1;
                                    }
                                }

                                //int i = 0;
                                //int index = 0;
                                //foreach (GridViewRow Item in gvCustCt.Rows)
                                //{
                                //    index = i;
                                //    // do stuff
                                //    i++;

                                //    if ((Item.FindControl("chkRow") as CheckBox).Checked)
                                //    {

                                //    }

                                //}
                            }
                            else if (JENIS == "RENOVASI_STORE")
                            {

                                for (var i = 0; i < gvCustCt.VisibleRowCount; ++i)
                                {
                                    if (gvCustCt.Selection.IsRowSelected(i))
                                    {
                                        totalgridjenis = 1;
                                    }
                                }
                            }
                            else if (JENIS == "EXISTING_STORE")
                            {

                                for (var i = 0; i < gvCustCt.VisibleRowCount; ++i)
                                {
                                    if (gvCustCt.Selection.IsRowSelected(i))
                                    {
                                        totalgridjenis = 1;
                                    }
                                }
                            }
                            else if (JENIS == "BAZZAR")
                            {

                                for (var i = 0; i < gvCustCt.VisibleRowCount; ++i)
                                {
                                    if (gvCustCt.Selection.IsRowSelected(i))
                                    {
                                        totalgridjenis = 1;
                                    }
                                }
                            }
                            else if (JENIS == "OPENING_COUNTER")
                            {
                                for (var i = 0; i < gvCustCt.VisibleRowCount; ++i)
                                {
                                    if (gvCustCt.Selection.IsRowSelected(i))
                                    {
                                        totalgridjenis = 1;
                                    }
                                }
                            }
                            else if (JENIS == "RENOVASI_COUNTER")
                            {
                                for (var i = 0; i < gvCustCt.VisibleRowCount; ++i)
                                {
                                    if (gvCustCt.Selection.IsRowSelected(i))
                                    {
                                        totalgridjenis = 1;
                                    }
                                }
                            }
                            else if (JENIS == "EXISTING_COUNTER")
                            {
                                for (var i = 0; i < gvCustCt.VisibleRowCount; ++i)
                                {
                                    if (gvCustCt.Selection.IsRowSelected(i))
                                    {
                                        totalgridjenis = 1;
                                    }
                                }
                            }

                            else if (JENIS == "DEALERS")
                            {
                                totalgridjenis = 1;
                            }

                            else if (JENIS == "INTERNAL_DEPARTMENT")
                            {
                                totalgridjenis = 1;
                            }

                            if (totalgridjenis == 1)
                            {
                                //Check Material Gridview
                                TR_FORM1_GDR_MATERI_DA TrForm1GdrMateri = new DataLayer.TR_FORM1_GDR_MATERI_DA();

                                int i = 0;
                                int index = 0;
                                foreach (GridViewRow Item in gvMain.Rows)
                                {
                                    index = i;
                                    // do stuff
                                    i++;
                                }

                                if (i != 0)
                                {

                                    DateTime? TGL_REQUIRED = Convert.ToDateTime(text_tanggalrequired.Text);

                                    TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                                    TrForm3gdr.NO_FORM = NO_FORM;
                                    TrForm3gdr.KODE_FORM = KODE_FORM;
                                    TrForm3gdr.PERMINTAAN_DESIGN = PERMINTAAN_DESIGN;
                                    TrForm3gdr.JENIS = JENIS;
                                    TrForm3gdr.TGL_REQUEST = TGL_REQUEST;
                                    TrForm3gdr.TGL_REQUIRED = TGL_REQUIRED;
                                    TrForm3gdr.ID_DEPT = ID_DEPT;
                                    TrForm3gdr.KD_BRAND = KD_BRAND;
                                    TrForm3gdr.DEPT_STORE_MALL = "";
                                    TrForm3gdr.ALOKASI_BUDGET = ALOKASI_BUDGET;
                                    TrForm3gdr.JADWAL_IMAGE = JADWAL_IMAGE;
                                    TrForm3gdr.JADWAL_ACARABKTOKO = JADWAL_ACARABKTOKO;
                                    TrForm3gdr.REFERENSI_DESIGN = REFERENSI_DESIGN;

                                    TrForm3gdr.RFR_LAMPIRAN_STORE = RFR_LAMPIRAN_STORE;
                                    TrForm3gdr.RFR_LAMPIRAN_MATERIAL = RFR_LAMPIRAN_MATERIAL;

                                    if (btn_uploadfile1.HasFile)
                                    {
                                        int imgSize = btn_uploadfile1.PostedFile.ContentLength;
                                        string ext = System.IO.Path.GetExtension(this.btn_uploadfile1.PostedFile.FileName).ToLower();
                                        if (btn_uploadfile1.PostedFile != null && btn_uploadfile1.PostedFile.FileName != "")
                                        {


                                            if (btn_uploadfile1.PostedFile.ContentLength > 3000000)
                                            {
                                                DivMessage.InnerText = "File 1 is larger than 3MB.";
                                                DivMessage.Attributes["class"] = "error";
                                                //DivMessage.Attributes["class"] = "success";
                                                DivMessage.Visible = true;
                                                return;
                                            }
                                            //if (ext != ".jpg" || ext != ".png" || ext != ".gif" || ext != ".jpeg")
                                            //{
                                            //    DivMessage.InnerText = "Format File Tidak Ada. Harap File Berformat(.jpg, .png and .gif)";
                                            //    DivMessage.Attributes["class"] = "error";
                                            //    //DivMessage.Attributes["class"] = "success";
                                            //    DivMessage.Visible = true;
                                            //    return;
                                            //}

                                            else
                                            {
                                                RFR_LAMPIRAN1 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "1" + btn_uploadfile1.FileName;
                                                btn_uploadfile1.PostedFile
                              .SaveAs(Server.MapPath("~/Uploaded/FileUpload/") + RFR_LAMPIRAN1);
                                            }
                                        }
                                    }

                                    TrForm3gdr.RFR_LAMPIRAN1 = RFR_LAMPIRAN1;
                                    if (btn_uploadfile2.HasFile)
                                    {
                                        int imgSize = btn_uploadfile2.PostedFile.ContentLength;
                                        string ext = System.IO.Path.GetExtension(this.btn_uploadfile2.PostedFile.FileName).ToLower();
                                        if (btn_uploadfile2.PostedFile != null && btn_uploadfile2.PostedFile.FileName != "")
                                        {


                                            if (btn_uploadfile2.PostedFile.ContentLength > 3000000)
                                            {
                                                DivMessage.InnerText = "File 2 is larger than 3MB.";
                                                DivMessage.Attributes["class"] = "error";
                                                //DivMessage.Attributes["class"] = "success";
                                                DivMessage.Visible = true;
                                                return;
                                            }
                                            else
                                            {
                                                RFR_LAMPIRAN2 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "2" + btn_uploadfile2.FileName;
                                                btn_uploadfile2.PostedFile
                                                    .SaveAs(Server.MapPath("~/Uploaded/FileUpload/") + RFR_LAMPIRAN2);
                                            }

                                        }
                                    }
                                    TrForm3gdr.RFR_LAMPIRAN2 = RFR_LAMPIRAN2;
                                    if (btn_uploadfile3.HasFile)
                                    {
                                        int imgSize = btn_uploadfile3.PostedFile.ContentLength;
                                        string ext = System.IO.Path.GetExtension(this.btn_uploadfile3.PostedFile.FileName).ToLower();
                                        if (btn_uploadfile3.PostedFile != null && btn_uploadfile3.PostedFile.FileName != "")
                                        {


                                            if (btn_uploadfile3.PostedFile.ContentLength > 3000000)
                                            {
                                                DivMessage.InnerText = "File 3 is larger than 3MB.";
                                                DivMessage.Attributes["class"] = "error";
                                                //DivMessage.Attributes["class"] = "success";
                                                DivMessage.Visible = true;
                                                return;
                                            }
                                            else
                                            {
                                                RFR_LAMPIRAN3 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "3" + btn_uploadfile3.FileName;
                                                btn_uploadfile3.PostedFile
                                                    .SaveAs(Server.MapPath("~/Uploaded/FileUpload/") + RFR_LAMPIRAN3);
                                            }
                                        }
                                    }
                                    TrForm3gdr.RFR_LAMPIRAN3 = RFR_LAMPIRAN3;
                                    if (btn_uploadfile4.HasFile)
                                    {
                                        int imgSize = btn_uploadfile4.PostedFile.ContentLength;
                                        string ext = System.IO.Path.GetExtension(this.btn_uploadfile4.PostedFile.FileName).ToLower();
                                        if (btn_uploadfile4.PostedFile != null && btn_uploadfile4.PostedFile.FileName != "")
                                        {


                                            if (btn_uploadfile4.PostedFile.ContentLength > 3000000)
                                            {
                                                DivMessage.InnerText = "File 4 is larger than 3MB.";
                                                DivMessage.Attributes["class"] = "error";
                                                //DivMessage.Attributes["class"] = "success";
                                                DivMessage.Visible = true;
                                                return;
                                            }
                                            else
                                            {
                                                RFR_LAMPIRAN4 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "4" + btn_uploadfile4.FileName;
                                                btn_uploadfile4.PostedFile
                                                    .SaveAs(Server.MapPath("~/Uploaded/FileUpload/") + RFR_LAMPIRAN4);
                                            }
                                        }

                                    }
                                    TrForm3gdr.RFR_LAMPIRAN4 = RFR_LAMPIRAN4;
                                    TrForm3gdr.JADWAL_SELESAI_DESAIN = Convert.ToDateTime(JADWAL_SELESAI_DESAIN);
                                    TrForm3gdr.JADWAL_PRODUKSI_CETAK = Convert.ToDateTime(JADWAL_PRODUKSI_CETAK);
                                    TrForm3gdr.JADWAL_KIRIM = Convert.ToDateTime(JADWAL_KIRIM);
                                    TrForm3gdr.JADWAL_FOTO = Convert.ToDateTime(JADWAL_FOTO);
                                    TrForm3gdr.JADWAL_DI = Convert.ToDateTime(JADWAL_DI);
                                    TrForm3gdr.JADWAL_ADM_CREATIVE = Convert.ToDateTime(JADWAL_ADM_CREATIVE);
                                    TrForm3gdr.DIBUAT = DIBUAT;
                                    TrForm3gdr.TGL_DIBUAT = TGL_DIBUAT;
                                    TrForm3gdr.MENYETUJUI1 = "";
                                    TrForm3gdr.TGL_MENYETUJUI1 = startdate;
                                    TrForm3gdr.DITERIMA_1 = "";
                                    TrForm3gdr.TGL_DITERIMA_1 = startdate;
                                    TrForm3gdr.DITERIMA_2 = "";
                                    TrForm3gdr.TGL_DITERIMA_2 = startdate;
                                    TrForm3gdr.DITERIMA_3 = "";
                                    TrForm3gdr.TGL_DITERIMA_3 = startdate;
                                    TrForm3gdr.DITERIMA_LAIN_1 = "";
                                    TrForm3gdr.TGL_DITERIMA_LAIN_1 = startdate;
                                    TrForm3gdr.DITERIMA_LAIN_2 = "";
                                    TrForm3gdr.TGL_DITERIMA_LAIN_2 = startdate;
                                    TrForm3gdr.DITERIMA_LAIN_3 = "";
                                    TrForm3gdr.TGL_DITERIMA_LAIN_3 = startdate;
                                    TrForm3gdr.DITERIMA_LAIN_4 = "";
                                    TrForm3gdr.TGL_DITERIMA_LAIN_4 = startdate;
                                    TrForm3gdr.DITERIMA_LAIN_5_MATERI = "";
                                    TrForm3gdr.TGL_DITERIMA_LAIN_5_MATERI = startdate;
                                    TrForm3gdr.DITERIMA_LAIN_5 = "";
                                    TrForm3gdr.TGL_DITERIMA_LAIN_5 = startdate;
                                    TrForm3gdr.STATUS = EApprovalStatus.OnApprovedHD;
                                    TrForm3gdr.REVISI = "";

                                    Int64 ID = 0;
                                    string ID_DEPT_USER = "";
                                    string USERNEXT = "";
                                    string DEPT_USER_NEXT = "";
                                    string Email = "";
                                    string KD_JABATAN = "";

                                    int URUTAN = 0;
                                    string ACTION = "";
                                    string PAGE_NAME = "";
                                    string SP = "";

                                    int URUTAN_NEXT = 0;
                                    string ACTION_NEXT = "";
                                    string PAGE_NAME_NEXT = "";
                                    string SP_NEXT = "";


                                    MS_USER_DA MsUserDA = new MS_USER_DA();
                                    DataSet DsUser = new DataSet();

                                    string Where = string.Format("ID_DEPT LIKE '%{0}%' And KD_JABATAN = 'HDP'", HfID_DEPT.Value);
                                    DsUser = MsUserDA.GetDataFilter(Where);

                                    if (DsUser.Tables[0].Rows.Count > 0)
                                    {
                                        ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                                        ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                                        USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                                        DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                                        KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                                        //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                                        DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                                        if (DsUser.Tables[0].Rows.Count > 0)
                                        {
                                            URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                                            ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                                            PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                                            SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                                        }

                                        //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                                        DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                                        if (DsUser.Tables[0].Rows.Count > 0)
                                        {
                                            URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                                            ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                                            PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                                            SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                                        }
                                    }

                                    TrForm3gdr.USER_CURRENT = DIBUAT;
                                    TrForm3gdr.NEXT_USER = USERNEXT;
                                    TrForm3gdr.URUTAN_USER_CURRENT = URUTAN;
                                    TrForm3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;

                                    string PHOTOGRAPHER = "";
                                    //Check Photographer Yes / No
                                    if (ddlphotographer.Text == "Yes")
                                    {
                                        PHOTOGRAPHER = EYesNo.Yes;
                                    }
                                    else
                                    {
                                        PHOTOGRAPHER = EYesNo.No;
                                    }

                                    string DigitalImaging = "";
                                    //Check Digital Imaging Yes / No
                                    if (ddldigitalimaging.Text == "Yes")
                                    {
                                        DigitalImaging = EYesNo.Yes;
                                    }
                                    else
                                    {
                                        DigitalImaging = EYesNo.No;
                                    }
                                    string PRODUCTION = ddlproduction.Text;
                                    TrForm3gdr.PRODUCTION = PRODUCTION;
                                    TrForm3gdr.PHOTOGRAPHER = PHOTOGRAPHER;
                                    TrForm3gdr.DIGITAL_IMAGING = DigitalImaging;
                                    TrForm3Gdr.Insert(TrForm3gdr);

                                    SaveDetailKategoriPermintaan();
                                    SaveDetailCustCt();
                                    SaveDetailMateriCetak();
                                    SendEmailAllType();

                                    TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                                    DataSet DsFormActivity = new DataSet();

                                    TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                                    TrForm3gdractivity.USERNAME = HfUsername.Value;
                                    TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                                    TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                                    TrForm3gdractivity.NO_FORM = NO_FORM;
                                    TrForm3gdractivity.STATUS = EApprovalStatus.OnApprovedHD;
                                    TrForm3gdractivity.DESCRIPTION = "Insert New Data. Status To " + EApprovalStatus.ApprovedHD;
                                    TrForm3gdractivity.REVISION = "-";
                                    TrForm3gdractivity.URUTAN = 1;
                                    TrForm3gdractivity.SP = "*";
                                    TrForm3gdractivity.USER_CURRENT = DIBUAT;
                                    TrForm3gdractivity.NEXT_USER = USERNEXT;
                                    TrForm3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                                    TrForm3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                                    TrForm3GdrActivity.Insert(TrForm3gdractivity);

                                    DivMessage.InnerText = "Data Successfully Save";
                                    DivMessage.Style.Add("color", "black");
                                    DivMessage.Style.Add("background-color", "skyblue");
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;

                                    string HomePageUrl = "../Forms_Data_Process/L_FormRequestGDR_Others.aspx";
                                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                                }
                                else
                                {
                                    DivMessage.InnerText = "'Detail Of Content' must be filled.";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                }

                            }
                            else
                            {
                                DivMessage.InnerText = "Store / Counter must be listed for at least 1 data";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                            }
                        }
                        else
                        {
                            DivMessage.InnerText = "The Required Date May Not Be Smaller Than Today's Date";
                            DivMessage.Attributes["class"] = "error";
                            //DivMessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;
                        }
                    }
                    else
                    {
                        DivMessage.InnerText = "The Required Date Cannot Be Empty";
                        DivMessage.Attributes["class"] = "error";
                        //DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                    }
                }
                else
                {
                    DivMessage.InnerText = "Category Request Cannot Be Empty. Minimum 1 Checklist";
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }



            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateSubmitFormRequestGDR()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                TR_FORM_GDR_CUST_DA TrFormGdrCust = new DataLayer.TR_FORM_GDR_CUST_DA();
                DataSet DsCust = new DataSet();

                string NO_FORM = text_noform.Text;
                string KODE_FORM = Common.KD_FORM_OTHERS;
                string PERMINTAAN_DESIGN = ddlpermintaandesign.SelectedValue.ToString();
                string JENIS = ddljenis.Text;
                DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);
                //int ID_DEPT = Convert.ToInt16(Session["ID_DEPT"].ToString());
                //string KD_BRAND = Session["KD_BRAND"].ToString();
                DateTime? ALOKASI_BUDGET = DateTime.ParseExact(text_alokasibudget.Text, "yyMM", null);
                DateTime? JADWAL_IMAGE = string.IsNullOrEmpty(text_jadwalpergantianimage.Text) ? (DateTime?)null : DateTime.Parse(text_jadwalpergantianimage.Text);
                DateTime? JADWAL_ACARABKTOKO = string.IsNullOrEmpty(text_jadwalacarabktoko.Text) ? (DateTime?)null : DateTime.Parse(text_jadwalacarabktoko.Text);
                string REFERENSI_DESIGN = text_referensidesign.Text;
                string RFR_LAMPIRAN_STORE = link_filenameuploadstore.Text;
                string RFR_LAMPIRAN_MATERIAL = link_filenameuploadmaterial.Text;
                string RFR_LAMPIRAN1 = "";
                string RFR_LAMPIRAN2 = "";
                string RFR_LAMPIRAN3 = "";
                string RFR_LAMPIRAN4 = "";
                string JADWAL_SELESAI_DESAIN = "";
                if (text_jadwalselesaidisain.Text != "")
                {
                    JADWAL_SELESAI_DESAIN = DateTime.Parse(text_jadwalselesaidisain.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_SELESAI_DESAIN = "1900-01-01";
                }

                string JADWAL_PRODUKSI_CETAK = "";
                if (text_jadwalproduksi.Text != "")
                {
                    JADWAL_PRODUKSI_CETAK = DateTime.Parse(text_jadwalproduksi.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_PRODUKSI_CETAK = "1900-01-01";
                }


                string JADWAL_KIRIM = "";
                if (text_jadwalkirim.Text != "")
                {
                    JADWAL_KIRIM = DateTime.Parse(text_jadwalkirim.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_KIRIM = "1900-01-01";
                }

                string JADWAL_FOTO = "";
                if (text_jadwalfoto.Text != "")
                {
                    JADWAL_FOTO = DateTime.Parse(text_jadwalfoto.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_FOTO = "1900-01-01";
                }

                string JADWAL_DI = "";
                if (text_jadwaldi.Text != "")
                {
                    JADWAL_DI = DateTime.Parse(text_jadwaldi.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_DI = "1900-01-01";
                }

                string JADWAL_ADM_CREATIVE = "";
                if (text_jadwaladmcreative.Text != "")
                {
                    JADWAL_ADM_CREATIVE = DateTime.Parse(text_jadwaladmcreative.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_ADM_CREATIVE = "1900-01-01";
                }


                string DIBUAT = text_dibuat.Text;
                DateTime TGL_DIBUAT = DateTime.Now;
                DateTime startdate = new DateTime(1900, 01, 01);

                if (checkbox_hording.Checked == true || checkbox_lighboxposter.Checked == true || checkbox_poster.Checked == true || checkbox_others1Others.Checked == true || checkbox_others2Others.Checked == true || checkbox_others3Others.Checked == true || checkbox_mediacetak.Checked == true || checkbox_digitaladvertising.Checked == true || checkbox_socialmedia.Checked == true || checkbox_other.Checked == true)
                {
                    if (JENIS == "DEALERS" || JENIS == "INTERNAL_DEPARTMENT")
                    {
                        string Where = string.Format("KODE_FORM = '{0}' AND NO_FORM = '{1}'", KODE_FORM, NO_FORM);
                        TrFormGdrCust.DeleteFilter(Where);
                    }
                    else
                    {
                        SaveDetailCustCt();
                    }

                    TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                    TrForm3gdr.NO_FORM = NO_FORM;
                    TrForm3gdr.KODE_FORM = KODE_FORM;
                    TrForm3gdr.PERMINTAAN_DESIGN = PERMINTAAN_DESIGN;
                    TrForm3gdr.JENIS = JENIS;
                    TrForm3gdr.TGL_REQUEST = TGL_REQUEST;
                    TrForm3gdr.TGL_REQUIRED = TGL_REQUIRED;
                    //TrForm3gdr.ID_DEPT = ID_DEPT;
                    //TrForm3gdr.KD_BRAND = KD_BRAND;
                    TrForm3gdr.DEPT_STORE_MALL = "";
                    TrForm3gdr.ALOKASI_BUDGET = ALOKASI_BUDGET;
                    TrForm3gdr.JADWAL_IMAGE = JADWAL_IMAGE;
                    TrForm3gdr.JADWAL_ACARABKTOKO = JADWAL_ACARABKTOKO;
                    TrForm3gdr.REFERENSI_DESIGN = REFERENSI_DESIGN;

                    TrForm3gdr.RFR_LAMPIRAN_STORE = RFR_LAMPIRAN_STORE;
                    TrForm3gdr.RFR_LAMPIRAN_MATERIAL = RFR_LAMPIRAN_MATERIAL;

                    if (linkbtn_filename1.Text == "-")
                    {
                        if (btn_uploadfile1.HasFile)
                        {
                            int imgSize = btn_uploadfile1.PostedFile.ContentLength;
                            string ext = System.IO.Path.GetExtension(this.btn_uploadfile1.PostedFile.FileName).ToLower();
                            if (btn_uploadfile1.PostedFile != null && btn_uploadfile1.PostedFile.FileName != "")
                            {


                                if (btn_uploadfile1.PostedFile.ContentLength > 3000000)
                                {
                                    DivMessage.InnerText = "File 1 is larger than 3MB.";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                    return;
                                }
                                //if (ext != ".jpg" || ext != ".png" || ext != ".gif" || ext != ".jpeg")
                                //{
                                //    DivMessage.InnerText = "Format File Tidak Ada. Harap File Berformat(.jpg, .png and .gif)";
                                //    DivMessage.Attributes["class"] = "error";
                                //    //DivMessage.Attributes["class"] = "success";
                                //    DivMessage.Visible = true;
                                //    return;
                                //}

                                else
                                {
                                    RFR_LAMPIRAN1 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "1" + btn_uploadfile1.FileName;
                                    btn_uploadfile1.PostedFile
                  .SaveAs(Server.MapPath("~/Uploaded/FileUpload/") + RFR_LAMPIRAN1);
                                }
                            }
                        }
                    }
                    else
                    {
                        RFR_LAMPIRAN1 = linkbtn_filename1.Text;
                    }

                    TrForm3gdr.RFR_LAMPIRAN1 = RFR_LAMPIRAN1;

                    if (linkbtn_filename2.Text == "-")
                    {
                        if (btn_uploadfile2.HasFile)
                        {
                            int imgSize = btn_uploadfile2.PostedFile.ContentLength;
                            string ext = System.IO.Path.GetExtension(this.btn_uploadfile2.PostedFile.FileName).ToLower();
                            if (btn_uploadfile2.PostedFile != null && btn_uploadfile2.PostedFile.FileName != "")
                            {


                                if (btn_uploadfile2.PostedFile.ContentLength > 3000000)
                                {
                                    DivMessage.InnerText = "File 2 is larger than 3MB.";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                    return;
                                }
                                else
                                {
                                    RFR_LAMPIRAN2 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "2" + btn_uploadfile2.FileName;
                                    btn_uploadfile2.PostedFile
                                        .SaveAs(Server.MapPath("~/Uploaded/FileUpload/") + RFR_LAMPIRAN2);
                                }

                            }
                        }
                    }
                    else
                    {
                        RFR_LAMPIRAN2 = linkbtn_filename2.Text;
                    }

                    TrForm3gdr.RFR_LAMPIRAN2 = RFR_LAMPIRAN2;

                    if (linkbtn_filename3.Text == "-")
                    {
                        if (btn_uploadfile3.HasFile)
                        {
                            int imgSize = btn_uploadfile3.PostedFile.ContentLength;
                            string ext = System.IO.Path.GetExtension(this.btn_uploadfile3.PostedFile.FileName).ToLower();
                            if (btn_uploadfile3.PostedFile != null && btn_uploadfile3.PostedFile.FileName != "")
                            {


                                if (btn_uploadfile3.PostedFile.ContentLength > 3000000)
                                {
                                    DivMessage.InnerText = "File 3 is larger than 3MB.";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                    return;
                                }
                                else
                                {
                                    RFR_LAMPIRAN3 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "3" + btn_uploadfile3.FileName;
                                    btn_uploadfile3.PostedFile
                                        .SaveAs(Server.MapPath("~/Uploaded/FileUpload/") + RFR_LAMPIRAN3);
                                }
                            }
                        }
                    }
                    else
                    {
                        RFR_LAMPIRAN3 = linkbtn_filename3.Text;
                    }
                    TrForm3gdr.RFR_LAMPIRAN3 = RFR_LAMPIRAN3;

                    if (linkbtn_filename4.Text == "-")
                    {
                        if (btn_uploadfile4.HasFile)
                        {
                            int imgSize = btn_uploadfile4.PostedFile.ContentLength;
                            string ext = System.IO.Path.GetExtension(this.btn_uploadfile4.PostedFile.FileName).ToLower();
                            if (btn_uploadfile4.PostedFile != null && btn_uploadfile4.PostedFile.FileName != "")
                            {


                                if (btn_uploadfile4.PostedFile.ContentLength > 3000000)
                                {
                                    DivMessage.InnerText = "File 4 is larger than 3MB.";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                    return;
                                }
                                else
                                {
                                    RFR_LAMPIRAN4 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "4" + btn_uploadfile4.FileName;
                                    btn_uploadfile4.PostedFile
                                        .SaveAs(Server.MapPath("~/Uploaded/FileUpload/") + RFR_LAMPIRAN4);
                                }
                            }

                        }
                    }
                    else
                    {
                        RFR_LAMPIRAN4 = linkbtn_filename4.Text;
                    }
                    TrForm3gdr.RFR_LAMPIRAN4 = RFR_LAMPIRAN4;
                    TrForm3gdr.JADWAL_SELESAI_DESAIN = Convert.ToDateTime(JADWAL_SELESAI_DESAIN);
                    TrForm3gdr.JADWAL_PRODUKSI_CETAK = Convert.ToDateTime(JADWAL_PRODUKSI_CETAK);
                    TrForm3gdr.JADWAL_KIRIM = Convert.ToDateTime(JADWAL_KIRIM);
                    TrForm3gdr.JADWAL_FOTO = Convert.ToDateTime(JADWAL_FOTO);
                    TrForm3gdr.JADWAL_DI = Convert.ToDateTime(JADWAL_DI);
                    TrForm3gdr.JADWAL_ADM_CREATIVE = Convert.ToDateTime(JADWAL_ADM_CREATIVE);
                    TrForm3gdr.DIBUAT = DIBUAT;
                    TrForm3gdr.TGL_DIBUAT = TGL_DIBUAT;
                    TrForm3gdr.MENYETUJUI1 = "";
                    TrForm3gdr.TGL_MENYETUJUI1 = startdate;
                    TrForm3gdr.DITERIMA_1 = "";
                    TrForm3gdr.TGL_DITERIMA_1 = startdate;
                    TrForm3gdr.DITERIMA_2 = "";
                    TrForm3gdr.TGL_DITERIMA_2 = startdate;
                    TrForm3gdr.DITERIMA_3 = "";
                    TrForm3gdr.TGL_DITERIMA_3 = startdate;
                    TrForm3gdr.DITERIMA_LAIN_1 = "";
                    TrForm3gdr.TGL_DITERIMA_LAIN_1 = startdate;
                    TrForm3gdr.DITERIMA_LAIN_2 = "";
                    TrForm3gdr.TGL_DITERIMA_LAIN_2 = startdate;
                    TrForm3gdr.DITERIMA_LAIN_3 = "";
                    TrForm3gdr.TGL_DITERIMA_LAIN_3 = startdate;
                    TrForm3gdr.DITERIMA_LAIN_4 = "";
                    TrForm3gdr.TGL_DITERIMA_LAIN_4 = startdate;
                    TrForm3gdr.DITERIMA_LAIN_5 = "";
                    TrForm3gdr.TGL_DITERIMA_LAIN_5 = startdate;
                    TrForm3gdr.STATUS = EApprovalStatus.OnApprovedHD;
                    TrForm3gdr.REVISI = "";

                    Int64 ID = 0;
                    string ID_DEPT_USER = "";
                    string USERNEXT = "";
                    string DEPT_USER_NEXT = "";
                    string Email = "";
                    string KD_JABATAN = "";

                    int URUTAN = 0;
                    string ACTION = "";
                    string PAGE_NAME = "";
                    string SP = "";

                    int URUTAN_NEXT = 0;
                    string ACTION_NEXT = "";
                    string PAGE_NAME_NEXT = "";
                    string SP_NEXT = "";

                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    string WhereUser = string.Format("ID_DEPT LIKE '%{0}%' And KD_JABATAN = 'HDP'", HfID_DEPT.Value);
                    DsUser = MsUserDA.GetDataFilter(WhereUser);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                        ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                        USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                        DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                        DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                        DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }
                    }

                    TrForm3gdr.USER_CURRENT = HfUsername.Value;
                    TrForm3gdr.NEXT_USER = USERNEXT;
                    TrForm3gdr.URUTAN_USER_CURRENT = URUTAN;
                    TrForm3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                    string PHOTOGRAPHER = ddlphotographer.Text;
                    //Check Photographer Yes / No
                    if (PHOTOGRAPHER == EYesNo.Yes)
                    {
                        PHOTOGRAPHER = EYesNo.Yes;
                    }
                    else
                    {
                        PHOTOGRAPHER = EYesNo.No;
                    }
                    TrForm3gdr.PHOTOGRAPHER = PHOTOGRAPHER;
                    TrForm3Gdr.Update(TrForm3gdr);

                    UpdateDetailKategoriPermintaan();
                    UpdateDetailMateriCetak();
                    //SendEmailAllType();

                    TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                    TrForm3gdractivity.USERNAME = HfUsername.Value;
                    TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                    TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                    TrForm3gdractivity.NO_FORM = NO_FORM;
                    TrForm3gdractivity.STATUS = EApprovalStatus.OnApprovedHD;
                    TrForm3gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.OnRevise + " To " + EApprovalStatus.OnApprovedHD;
                    TrForm3gdractivity.REVISION = "-";
                    TrForm3gdractivity.URUTAN = 1;
                    TrForm3gdractivity.SP = "";
                    TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                    TrForm3gdractivity.NEXT_USER = USERNEXT;
                    TrForm3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    TrForm3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3GdrActivity.Insert(TrForm3gdractivity);


                    DivMessage.InnerText = "Data Successfully Updated And Submitted";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../MainMenu.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                }
                else
                {
                    DivMessage.InnerText = "Category Request Cannot Be Empty. Minimum 1 Checklist";
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateSubmitFormRequestDesignGDR()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string KODE_FORM = Common.KD_FORM_OTHERS;
                string PERMINTAAN_DESIGN = ddlpermintaandesign.SelectedValue.ToString();
                string JENIS = ddljenis.Text;
                DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);
                //int ID_DEPT = Convert.ToInt16(Session["ID_DEPT"].ToString());
                //string KD_BRAND = Session["KD_BRAND"].ToString();
                DateTime? ALOKASI_BUDGET = DateTime.ParseExact(text_alokasibudget.Text, "yyMM", null);
                DateTime? JADWAL_IMAGE = string.IsNullOrEmpty(text_jadwalpergantianimage.Text) ? (DateTime?)null : DateTime.Parse(text_jadwalpergantianimage.Text);
                DateTime? JADWAL_ACARABKTOKO = string.IsNullOrEmpty(text_jadwalacarabktoko.Text) ? (DateTime?)null : DateTime.Parse(text_jadwalacarabktoko.Text);
                string REFERENSI_DESIGN = text_referensidesign.Text;
                string RFR_LAMPIRAN1 = "";
                string RFR_LAMPIRAN2 = "";
                string RFR_LAMPIRAN3 = "";
                string RFR_LAMPIRAN4 = "";
                string JADWAL_SELESAI_DESAIN = "";
                if (text_jadwalselesaidisain.Text != "")
                {
                    JADWAL_SELESAI_DESAIN = DateTime.Parse(text_jadwalselesaidisain.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_SELESAI_DESAIN = "1900-01-01";
                }

                string JADWAL_PRODUKSI_CETAK = "";
                if (text_jadwalproduksi.Text != "")
                {
                    JADWAL_PRODUKSI_CETAK = DateTime.Parse(text_jadwalproduksi.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_PRODUKSI_CETAK = "1900-01-01";
                }


                string JADWAL_KIRIM = "";
                if (text_jadwalkirim.Text != "")
                {
                    JADWAL_KIRIM = DateTime.Parse(text_jadwalkirim.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_KIRIM = "1900-01-01";
                }

                string JADWAL_FOTO = "";
                if (text_jadwalfoto.Text != "")
                {
                    JADWAL_FOTO = DateTime.Parse(text_jadwalfoto.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_FOTO = "1900-01-01";
                }

                string JADWAL_DI = "";
                if (text_jadwaldi.Text != "")
                {
                    JADWAL_DI = DateTime.Parse(text_jadwaldi.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_DI = "1900-01-01";
                }

                string JADWAL_ADM_CREATIVE = "";
                if (text_jadwaladmcreative.Text != "")
                {
                    JADWAL_ADM_CREATIVE = DateTime.Parse(text_jadwaladmcreative.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_ADM_CREATIVE = "1900-01-01";
                }


                string DITERIMA_2 = ddlditerima2.Text;
                DateTime TGL_DITERIMA_2 = DateTime.Now;
                //string STATUS_VER = "D";

                TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                TrForm3gdr.NO_FORM = NO_FORM;
                TrForm3gdr.KODE_FORM = KODE_FORM;
                TrForm3gdr.PERMINTAAN_DESIGN = PERMINTAAN_DESIGN;
                TrForm3gdr.JENIS = JENIS;
                TrForm3gdr.TGL_REQUEST = TGL_REQUEST;
                TrForm3gdr.TGL_REQUIRED = TGL_REQUIRED;
                //TrForm3gdr.ID_DEPT = ID_DEPT;
                //TrForm3gdr.KD_BRAND = KD_BRAND;
                TrForm3gdr.DEPT_STORE_MALL = "";
                TrForm3gdr.ALOKASI_BUDGET = ALOKASI_BUDGET;
                TrForm3gdr.JADWAL_IMAGE = JADWAL_IMAGE;
                TrForm3gdr.JADWAL_ACARABKTOKO = JADWAL_ACARABKTOKO;
                TrForm3gdr.REFERENSI_DESIGN = REFERENSI_DESIGN;


                if (linkbtn_filename1.Text == "-" || linkbtn_filename1.Text == "")
                {
                    if (btn_uploadfile1.HasFile)
                    {
                        int imgSize = btn_uploadfile1.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfile1.PostedFile.FileName).ToLower();
                        if (btn_uploadfile1.PostedFile != null && btn_uploadfile1.PostedFile.FileName != "")
                        {


                            if (btn_uploadfile1.PostedFile.ContentLength > 3000000)
                            {
                                DivMessage.InnerText = "File 1 is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            //if (ext != ".jpg" || ext != ".png" || ext != ".gif" || ext != ".jpeg")
                            //{
                            //    DivMessage.InnerText = "Format File Tidak Ada. Harap File Berformat(.jpg, .png and .gif)";
                            //    DivMessage.Attributes["class"] = "error";
                            //    //DivMessage.Attributes["class"] = "success";
                            //    DivMessage.Visible = true;
                            //    return;
                            //}

                            else
                            {
                                RFR_LAMPIRAN1 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "1" + btn_uploadfile1.FileName;
                                btn_uploadfile1.PostedFile
              .SaveAs(Server.MapPath("~/Uploaded/FileUpload/") + RFR_LAMPIRAN1);
                            }
                        }
                    }
                }
                else
                {
                    RFR_LAMPIRAN1 = linkbtn_filename1.Text;
                }

                TrForm3gdr.RFR_LAMPIRAN1 = RFR_LAMPIRAN1;

                if (linkbtn_filename2.Text == "-" || linkbtn_filename2.Text == "")
                {
                    if (btn_uploadfile2.HasFile)
                    {
                        int imgSize = btn_uploadfile2.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfile2.PostedFile.FileName).ToLower();
                        if (btn_uploadfile2.PostedFile != null && btn_uploadfile2.PostedFile.FileName != "")
                        {


                            if (btn_uploadfile2.PostedFile.ContentLength > 3000000)
                            {
                                DivMessage.InnerText = "File 2 is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            else
                            {
                                RFR_LAMPIRAN2 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "2" + btn_uploadfile2.FileName;
                                btn_uploadfile2.PostedFile
                                    .SaveAs(Server.MapPath("~/Uploaded/FileUpload/") + RFR_LAMPIRAN2);
                            }

                        }
                    }
                }
                else
                {
                    RFR_LAMPIRAN2 = linkbtn_filename2.Text;
                }

                TrForm3gdr.RFR_LAMPIRAN2 = RFR_LAMPIRAN2;

                if (linkbtn_filename3.Text == "-" || linkbtn_filename3.Text == "")
                {
                    if (btn_uploadfile3.HasFile)
                    {
                        int imgSize = btn_uploadfile3.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfile3.PostedFile.FileName).ToLower();
                        if (btn_uploadfile3.PostedFile != null && btn_uploadfile3.PostedFile.FileName != "")
                        {


                            if (btn_uploadfile3.PostedFile.ContentLength > 3000000)
                            {
                                DivMessage.InnerText = "File 3 is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            else
                            {
                                RFR_LAMPIRAN3 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "3" + btn_uploadfile3.FileName;
                                btn_uploadfile3.PostedFile
                                    .SaveAs(Server.MapPath("~/Uploaded/FileUpload/") + RFR_LAMPIRAN3);
                            }
                        }
                    }
                }
                else
                {
                    RFR_LAMPIRAN3 = linkbtn_filename3.Text;
                }
                TrForm3gdr.RFR_LAMPIRAN3 = RFR_LAMPIRAN3;

                if (linkbtn_filename4.Text == "-" || linkbtn_filename4.Text == "")
                {
                    if (btn_uploadfile4.HasFile)
                    {
                        int imgSize = btn_uploadfile4.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfile4.PostedFile.FileName).ToLower();
                        if (btn_uploadfile4.PostedFile != null && btn_uploadfile4.PostedFile.FileName != "")
                        {


                            if (btn_uploadfile4.PostedFile.ContentLength > 3000000)
                            {
                                DivMessage.InnerText = "File 4 is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            else
                            {
                                RFR_LAMPIRAN4 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "4" + btn_uploadfile4.FileName;
                                btn_uploadfile4.PostedFile
                                    .SaveAs(Server.MapPath("~/Uploaded/FileUpload/") + RFR_LAMPIRAN4);
                            }
                        }

                    }
                }
                else
                {
                    RFR_LAMPIRAN4 = linkbtn_filename4.Text;
                }
                TrForm3gdr.RFR_LAMPIRAN4 = RFR_LAMPIRAN4;
                TrForm3gdr.JADWAL_SELESAI_DESAIN = Convert.ToDateTime(JADWAL_SELESAI_DESAIN);
                TrForm3gdr.JADWAL_PRODUKSI_CETAK = Convert.ToDateTime(JADWAL_PRODUKSI_CETAK);
                TrForm3gdr.JADWAL_KIRIM = Convert.ToDateTime(JADWAL_KIRIM);
                TrForm3gdr.JADWAL_FOTO = Convert.ToDateTime(JADWAL_FOTO);
                TrForm3gdr.DITERIMA_2 = DITERIMA_2;
                TrForm3gdr.TGL_DITERIMA_2 = TGL_DITERIMA_2;
                TrForm3gdr.STATUS = EApprovalStatus.AcceptedGraphicDesign;
                TrForm3gdr.REVISI = "";

                string RFR_LAMPIRAN5GD = "";
                string RFR_LAMPIRAN6GD = "";
                string RFR_LAMPIRAN7GD = "";
                string RFR_LAMPIRAN8GD = "";

                if (linkbtn_filenamegd1.Text == "-" || linkbtn_filenamegd1.Text == "")
                {
                    if (btn_uploadfilegd1.HasFile)
                    {
                        int imgSize = btn_uploadfilegd1.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfilegd1.PostedFile.FileName).ToLower();
                        if (btn_uploadfilegd1.PostedFile != null && btn_uploadfilegd1.PostedFile.FileName != "")
                        {


                            if (btn_uploadfilegd1.PostedFile.ContentLength > 3000000)
                            {
                                DivMessage.InnerText = "File 1 Gd is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            else
                            {
                                RFR_LAMPIRAN5GD = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfilegd1.FileName;
                                btn_uploadfilegd1.PostedFile
                                    .SaveAs(Server.MapPath("~/Uploaded/FileUploadGD/") + RFR_LAMPIRAN5GD);
                            }

                        }
                        else
                        {
                            RFR_LAMPIRAN5GD = linkbtn_filenamegd1.Text;
                        }
                    }
                }
                else
                {
                    RFR_LAMPIRAN5GD = linkbtn_filenamegd1.Text;
                }

                TrForm3gdr.RFR_LAMPIRAN5_GD = RFR_LAMPIRAN5GD;


                if (linkbtn_filenamegd2.Text == "-" || linkbtn_filenamegd2.Text == "")
                {

                    if (btn_uploadfilegd2.HasFile)
                    {
                        int imgSize = btn_uploadfilegd2.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfilegd2.PostedFile.FileName).ToLower();
                        if (btn_uploadfilegd2.PostedFile != null && btn_uploadfilegd2.PostedFile.FileName != "")
                        {


                            if (btn_uploadfilegd2.PostedFile.ContentLength > 3000000)
                            {
                                DivMessage.InnerText = "File 2 Gd is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            else
                            {
                                RFR_LAMPIRAN6GD = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfilegd2.FileName;
                                btn_uploadfilegd2.PostedFile
                                    .SaveAs(Server.MapPath("~/Uploaded/FileUploadGD/") + RFR_LAMPIRAN6GD);
                            }
                        }
                        else
                        {
                            RFR_LAMPIRAN6GD = linkbtn_filenamegd2.Text;
                        }
                    }
                }
                else
                {
                    RFR_LAMPIRAN6GD = linkbtn_filenamegd2.Text;
                }

                TrForm3gdr.RFR_LAMPIRAN6_GD = RFR_LAMPIRAN6GD;


                if (linkbtn_filenamegd3.Text == "-" || linkbtn_filenamegd3.Text == "")
                {

                    if (btn_uploadfilegd3.HasFile)
                    {
                        int imgSize = btn_uploadfilegd3.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfilegd3.PostedFile.FileName).ToLower();
                        if (btn_uploadfilegd3.PostedFile != null && btn_uploadfilegd3.PostedFile.FileName != "")
                        {


                            if (btn_uploadfilegd3.PostedFile.ContentLength > 3000000)
                            {
                                DivMessage.InnerText = "File 3 Gd is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            else
                            {
                                RFR_LAMPIRAN7GD = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfilegd3.FileName;
                                btn_uploadfilegd3.PostedFile
                                    .SaveAs(Server.MapPath("~/Uploaded/FileUploadGD/") + RFR_LAMPIRAN7GD);
                            }
                        }
                        else
                        {
                            RFR_LAMPIRAN7GD = linkbtn_filenamegd3.Text;
                        }
                    }
                }
                else
                {
                    RFR_LAMPIRAN7GD = linkbtn_filenamegd3.Text;
                }

                TrForm3gdr.RFR_LAMPIRAN7_GD = RFR_LAMPIRAN7GD;


                if (linkbtn_filenamegd4.Text == "-" || linkbtn_filenamegd4.Text == "")
                {

                    if (btn_uploadfilegd4.HasFile)
                    {
                        int imgSize = btn_uploadfilegd4.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfilegd4.PostedFile.FileName).ToLower();
                        if (btn_uploadfilegd4.PostedFile != null && btn_uploadfilegd4.PostedFile.FileName != "")
                        {


                            if (btn_uploadfilegd4.PostedFile.ContentLength > 3000000)
                            {
                                DivMessage.InnerText = "File 4 Gd is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            else
                            {

                                RFR_LAMPIRAN8GD = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfilegd4.FileName;
                                btn_uploadfilegd4.PostedFile
                                    .SaveAs(Server.MapPath("~/Uploaded/FileUploadGD/") + RFR_LAMPIRAN8GD);
                            }
                        }
                        else
                        {
                            RFR_LAMPIRAN8GD = linkbtn_filenamegd4.Text;
                        }
                    }
                }
                else
                {
                    RFR_LAMPIRAN8GD = linkbtn_filenamegd4.Text;
                }

                TrForm3gdr.RFR_LAMPIRAN8_GD = RFR_LAMPIRAN8GD;

                Int64 ID = 0;
                string ID_DEPT_USER = "";
                string USERNEXT = "";
                string DEPT_USER_NEXT = "";
                string Email = "";
                string KD_JABATAN = "";

                int URUTAN = 0;
                string ACTION = "";
                string PAGE_NAME = "";
                string SP = "";

                int URUTAN_NEXT = 0;
                string ACTION_NEXT = "";
                string PAGE_NAME_NEXT = "";
                string SP_NEXT = "";

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet DsUser = new DataSet();

                string Where = string.Format("ID_DEPT NOT LIKE '%13%' And KD_JABATAN = 'CM'");
                DsUser = MsUserDA.GetDataFilter(Where);

                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                    ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                    DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                    KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                    DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }

                    string ACTIONCM = "Approved";

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                    DsUser = MsUserDA.GetDataUrutanUserCustom(USERNEXT, KODE_FORM, ACTIONCM);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }

                }
                TrForm3gdr.USER_CURRENT = HfUsername.Value;
                TrForm3gdr.NEXT_USER = USERNEXT;
                TrForm3gdr.URUTAN_USER_CURRENT = URUTAN;
                TrForm3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm3Gdr.UpdateDesign(TrForm3gdr);

                UpdateDetailMateriCetak();
                //SendEmailAllType();

                TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                string STATUS = label_statusvalue.Text;
                if (STATUS == EApprovalStatus.OnReviseDesign)
                {


                    TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                    TrForm3gdractivity.USERNAME = HfUsername.Value;
                    TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                    TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                    TrForm3gdractivity.NO_FORM = NO_FORM;
                    TrForm3gdractivity.STATUS = EApprovalStatus.AcceptedGraphicDesign;
                    TrForm3gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.OnReviseDesign + " To " + EApprovalStatus.AcceptedGraphicDesign;
                    TrForm3gdractivity.REVISION = "-";
                    TrForm3gdractivity.URUTAN = 9;
                    TrForm3gdractivity.SP = "*";
                    TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                    TrForm3gdractivity.NEXT_USER = USERNEXT;
                    TrForm3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    TrForm3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3GdrActivity.Insert(TrForm3gdractivity);
                }

                DivMessage.InnerText = "Data Successfully Updated And Submitted Design";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateSubmitFormRequestReviseContentGDR()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                TR_FORM_GDR_CUST_DA TrFormGdrCust = new DataLayer.TR_FORM_GDR_CUST_DA();
                DataSet DsCust = new DataSet();

                string NO_FORM = text_noform.Text;
                string KODE_FORM = Common.KD_FORM_OTHERS;
                string JENIS = ddljenis.Text;
                string JADWAL_SELESAI_DESAIN = "";
                if (text_jadwalselesaidisain.Text != "")
                {
                    JADWAL_SELESAI_DESAIN = DateTime.Parse(text_jadwalselesaidisain.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_SELESAI_DESAIN = "1900-01-01";
                }

                string JADWAL_PRODUKSI_CETAK = "";
                if (text_jadwalproduksi.Text != "")
                {
                    JADWAL_PRODUKSI_CETAK = DateTime.Parse(text_jadwalproduksi.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_PRODUKSI_CETAK = "1900-01-01";
                }


                string JADWAL_KIRIM = "";
                if (text_jadwalkirim.Text != "")
                {
                    JADWAL_KIRIM = DateTime.Parse(text_jadwalkirim.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_KIRIM = "1900-01-01";
                }

                string JADWAL_FOTO = "";
                if (text_jadwalfoto.Text != "")
                {
                    JADWAL_FOTO = DateTime.Parse(text_jadwalfoto.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_FOTO = "1900-01-01";
                }

                string JADWAL_DI = "";
                if (text_jadwaldi.Text != "")
                {
                    JADWAL_DI = DateTime.Parse(text_jadwaldi.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_DI = "1900-01-01";
                }

                string JADWAL_ADM_CREATIVE = "";
                if (text_jadwaladmcreative.Text != "")
                {
                    JADWAL_ADM_CREATIVE = DateTime.Parse(text_jadwaladmcreative.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_ADM_CREATIVE = "1900-01-01";
                }


                //DateTime? JADWAL_DI = string.IsNullOrEmpty(text_jadwaldi.Text) ? (DateTime?)null : DateTime.Parse(text_jadwaldi.Text);
                string DITERIMA_1 = text_diterima1.Text;
                string DITERIMA_2 = ddlditerima2.SelectedValue.ToString();
                DateTime TGL_DITERIMA_1 = DateTime.Now;
                string REVISI = text_commentar.Text;

                //if (REVISI != "")
                //{
                if (JENIS == "DEALERS" || JENIS == "INTERNAL_DEPARTMENT")
                {
                    string Where = string.Format("KODE_FORM = '{0}' AND NO_FORM = '{1}'", KODE_FORM, NO_FORM);
                    TrFormGdrCust.DeleteFilter(Where);
                }
                else
                {
                    SaveDetailCustCt();
                }


                TR_FORM3_GDR trform3gdr = new TR_FORM3_GDR();
                trform3gdr.NO_FORM = NO_FORM;
                trform3gdr.KODE_FORM = KODE_FORM;
                trform3gdr.JADWAL_SELESAI_DESAIN = Convert.ToDateTime(JADWAL_SELESAI_DESAIN);
                trform3gdr.JADWAL_PRODUKSI_CETAK = Convert.ToDateTime(JADWAL_PRODUKSI_CETAK);
                trform3gdr.JADWAL_KIRIM = Convert.ToDateTime(JADWAL_KIRIM);
                trform3gdr.JADWAL_FOTO = Convert.ToDateTime(JADWAL_FOTO);
                trform3gdr.JADWAL_DI = Convert.ToDateTime(JADWAL_DI);
                trform3gdr.JADWAL_ADM_CREATIVE = Convert.ToDateTime(JADWAL_ADM_CREATIVE);
                trform3gdr.DITERIMA_1 = DITERIMA_1;
                trform3gdr.TGL_DITERIMA_1 = TGL_DITERIMA_1;
                trform3gdr.DITERIMA_2 = DITERIMA_2;
                trform3gdr.STATUS = EApprovalStatus.OnReviseContent;
                trform3gdr.REVISI = REVISI;

                Int64 ID = 0;
                string ID_DEPT_USER = "";
                string USERNEXT = "";
                string DEPT_USER_NEXT = "";
                string Email = "";
                string KD_JABATAN = "";

                int URUTAN = 0;
                string ACTION = "";
                string PAGE_NAME = "";
                string SP = "";

                int URUTAN_NEXT = 0;
                string ACTION_NEXT = "";
                string PAGE_NAME_NEXT = "";
                string SP_NEXT = "";

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet DsUser = new DataSet();

                string WhereUser = string.Format("USERNAME = '{0}' And KD_JABATAN = 'GD'", DITERIMA_2);
                DsUser = MsUserDA.GetDataFilter(WhereUser);

                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                    ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                    DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                    KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                    DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                    DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }
                }

                trform3gdr.USER_CURRENT = HfUsername.Value;
                trform3gdr.NEXT_USER = USERNEXT;
                trform3gdr.URUTAN_USER_CURRENT = URUTAN;
                trform3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm3Gdr.UpdateDiterima1RevisiContent(trform3gdr);

                UpdateDetailMateriCetak();
                SendEmailAllType();

                TR_FORM_GDR_ACTIVITY_DA TrForm2GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY trform2gdractivity = new TR_FORM_GDR_ACTIVITY();
                trform2gdractivity.USERNAME = HfUsername.Value;
                trform2gdractivity.ACTIVITY_TIME = DateTime.Now;
                trform2gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                trform2gdractivity.NO_FORM = NO_FORM;
                trform2gdractivity.STATUS = EApprovalStatus.OnReviseContent;
                trform2gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.OnReviseContent;
                trform2gdractivity.REVISION = REVISI;
                trform2gdractivity.URUTAN = 7;
                trform2gdractivity.SP = "";
                trform2gdractivity.USER_CURRENT = HfUsername.Value;
                trform2gdractivity.NEXT_USER = USERNEXT;
                trform2gdractivity.URUTAN_USER_CURRENT = URUTAN;
                trform2gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm2GdrActivity.Insert(trform2gdractivity);


                DivMessage.InnerText = "Data Successful Revise Content";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_VM.aspx");

                //}
                //else
                //{
                //    DivMessage.InnerText = "Komentar Cannot Be Empty";
                //    DivMessage.Attributes["class"] = "error";
                //    //DivMessage.Attributes["class"] = "success";
                //    DivMessage.Visible = true;
                //}


            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        /// <summary>
        /// Update Approve, Accepted, Cancel, Revise As All
        /// </summary>

        public void UpdateApproveAll()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mengambil Posisi CM. Karena CM Ada terdapat 3 action sekaligus.
                string KodeJabatan = "";

                DsUser = MsUserDA.GetDataFilter(string.Format("USERNAME = '{0}'", HfUsername.Value));
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    KodeJabatan = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                }

                string Status = label_statusvalue.Text;
                string ACTIONGENERAL = "";

                if (HfPhotoGrapher.Value == EYesNo.Yes || ddldigitalimaging.Text == EYesNo.Yes)
                {
                    if (KodeJabatan == "CM" && (Status == EApprovalStatus.ApprovedPhoto || Status == EApprovalStatus.OnRevisePhoto))
                    {
                        ACTIONGENERAL = "Approved-PhotoGrapher-Photo";
                    }
                    else if (KodeJabatan == "CM" && (Status == EApprovalStatus.ApprovedDI || Status == EApprovalStatus.OnRevisePhotoDI))
                    {
                        ACTIONGENERAL = "Approved-PhotoGrapher-DI";
                    }
                    else
                    {
                        ACTIONGENERAL = "Approved";
                    }
                }
                else if (HfPhotoGrapher.Value == EYesNo.No)
                {
                    ACTIONGENERAL = "Approved";
                }
                else
                {
                    ACTIONGENERAL = "Approved";
                }

                HfActionGeneral.Value = ACTIONGENERAL;

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUserCustom(HfUsername.Value, KODE_FORM, HfActionGeneral.Value);

                int URUTAN = 0;
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                    if (URUTAN == 2)
                    {
                        UpdateStatusApproved();
                        SendEmailAllType();

                    }
                    if (URUTAN == 3)
                    {
                        UpdateStatusApprovedPhotographer();
                        SendEmailAllType();

                    }
                    if (URUTAN == 4)
                    {
                        UpdateStatusApprovedCreativeManagerPhoto();
                        SendEmailAllType();

                    }
                    if (URUTAN == 5)
                    {
                        UpdateStatusApprovedDigitalImaging();
                        SendEmailAllType();

                    }
                    if (URUTAN == 6)
                    {
                        UpdateStatusApprovedCreativeManagerDigitalImaging();
                        SendEmailAllType();

                    }

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateAcceptedAll()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mengambil Posisi CM. Karena CM Ada terdapat 3 action sekaligus.
                string KodeJabatan = "";

                DsUser = MsUserDA.GetDataFilter(string.Format("USERNAME = '{0}'", HfUsername.Value));
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    KodeJabatan = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                }

                string Status = label_statusvalue.Text;
                string ACTIONGENERAL = "";

                if (HfPhotoGrapher.Value == EYesNo.Yes || ddldigitalimaging.Text == EYesNo.Yes)
                {
                    if (KodeJabatan == "CM" && (Status == EApprovalStatus.ApprovedPhoto || Status == EApprovalStatus.OnRevisePhoto))
                    {
                        ACTIONGENERAL = "Approved-PhotoGrapher-Photo";
                    }
                    else if (KodeJabatan == "CM" && (Status == EApprovalStatus.ApprovedDI || Status == EApprovalStatus.OnRevisePhotoDI))
                    {
                        ACTIONGENERAL = "Approved-PhotoGrapher-DI";
                    }
                    else
                    {
                        ACTIONGENERAL = "Approved";
                    }
                }
                else if (HfPhotoGrapher.Value == EYesNo.No)
                {
                    ACTIONGENERAL = "Approved";
                }
                else
                {
                    ACTIONGENERAL = "ALL";
                }

                HfActionGeneral.Value = ACTIONGENERAL;

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUserCustom(HfUsername.Value, KODE_FORM, HfActionGeneral.Value);

                int URUTAN = 0;
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                    if (URUTAN == 7)
                    {
                        if (ddlditerima2.Text != "Choose")
                        {
                            UpdateStatusAcceptedHeadDesign();
                        }
                        else
                        {
                            DivMessage.InnerText = "Graphic Design Harus Diisi";
                            DivMessage.Attributes["class"] = "error";
                            //DivMessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;
                        }

                    }
                    else if (URUTAN == 8)
                    {
                        UpdateStatusAcceptedGraphicDesign();
                        UpdateSubmitFormRequestDesignGDR();
                        SendEmailAllType();

                    }
                    else if (URUTAN == 9)
                    {
                        UpdateStatusApprovedCreativeManager();
                        SendEmailAllType();
                    }
                    else if (URUTAN == 10)
                    {
                        if (Status == EApprovalStatus.DoneProduction)
                        {
                            UpdateStatusApprovedProductionCetakMateri();
                        }
                        else
                        {
                            UpdateStatusApprovedProduction();
                        }
                        SendEmailAllType();
                    }

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateCancelAll()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mengambil Posisi CM. Karena CM Ada terdapat 3 action sekaligus.
                string KodeJabatan = "";

                DsUser = MsUserDA.GetDataFilter(string.Format("USERNAME = '{0}'", HfUsername.Value));
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    KodeJabatan = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                }

                string Status = label_statusvalue.Text;
                string ACTIONGENERAL = "";

                if (HfPhotoGrapher.Value == EYesNo.Yes || ddldigitalimaging.Text == EYesNo.Yes)
                {
                    if (KodeJabatan == "CM" && (Status == EApprovalStatus.ApprovedPhoto || Status == EApprovalStatus.OnRevisePhoto))
                    {
                        ACTIONGENERAL = "Approved-PhotoGrapher-Photo";
                    }
                    else if (KodeJabatan == "CM" && (Status == EApprovalStatus.ApprovedDI || Status == EApprovalStatus.OnRevisePhotoDI))
                    {
                        ACTIONGENERAL = "Approved-PhotoGrapher-DI";
                    }
                    else
                    {
                        ACTIONGENERAL = "Approved";
                    }
                }
                else if (HfPhotoGrapher.Value == EYesNo.No)
                {
                    ACTIONGENERAL = "Approved";
                }
                else
                {
                    ACTIONGENERAL = "ALL";
                }

                HfActionGeneral.Value = ACTIONGENERAL;

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUserCustom(HfUsername.Value, KODE_FORM, HfActionGeneral.Value);

                int URUTAN = 0;
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                    if (URUTAN == 1)
                    {
                        UpdateStatusCancel();
                        SendEmailAllType();

                    }
                    else if (URUTAN == 2)
                    {
                        UpdateStatusCancelHD();
                        SendEmailAllType();

                    }
                    else if (URUTAN == 3)
                    {
                        UpdateStatusCancelPhotoGrapher();
                        //UpdateStatusCancelHeadDesign();
                        SendEmailAllType();
                    }
                    else if (URUTAN == 4)
                    {

                    }
                    else if (URUTAN == 5)
                    {
                        UpdateStatusCancelDigitalImaging();
                        SendEmailAllType();
                    }
                    else if (URUTAN == 6)
                    {

                    }
                    else if (URUTAN == 7)
                    {
                        UpdateStatusCancelHeadDesign();
                        SendEmailAllType();
                    }
                    else if (URUTAN == 8)
                    {
                        UpdateStatusCancelGraphicDesign();
                        SendEmailAllType();
                    }
                    else if (URUTAN == 9)
                    {
                        UpdateStatusCancelCreativeManager();
                        SendEmailAllType();
                    }
                    else if (URUTAN == 10)
                    {

                    }


                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateReviseAll()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mengambil Posisi CM. Karena CM Ada terdapat 3 action sekaligus.
                string KodeJabatan = "";

                DsUser = MsUserDA.GetDataFilter(string.Format("USERNAME = '{0}'", HfUsername.Value));
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    KodeJabatan = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                }

                string Status = label_statusvalue.Text;
                string ACTIONGENERAL = "";

                if (HfPhotoGrapher.Value == EYesNo.Yes || ddldigitalimaging.Text == EYesNo.Yes)
                {
                    if (KodeJabatan == "CM" && (Status == EApprovalStatus.ApprovedPhoto || Status == EApprovalStatus.OnRevisePhoto))
                    {
                        ACTIONGENERAL = "Approved-PhotoGrapher-Photo";
                    }
                    else if (KodeJabatan == "CM" && (Status == EApprovalStatus.ApprovedDI || Status == EApprovalStatus.OnRevisePhotoDI))
                    {
                        ACTIONGENERAL = "Approved-PhotoGrapher-DI";
                    }
                    else
                    {
                        ACTIONGENERAL = "Approved";
                    }
                }
                else if (HfPhotoGrapher.Value == EYesNo.No)
                {
                    ACTIONGENERAL = "Approved";
                }
                else
                {
                    ACTIONGENERAL = "ALL";
                }

                HfActionGeneral.Value = ACTIONGENERAL;

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUserCustom(HfUsername.Value, KODE_FORM, HfActionGeneral.Value);

                int URUTAN = 0;
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                    if (URUTAN == 1)
                    {
                        //

                    }
                    else if (URUTAN == 2)
                    {
                        UpdateStatusRevisiHD();
                        SendEmailAllType();

                    }
                    else if (URUTAN == 3)
                    {
                        //

                    }
                    else if (URUTAN == 4)
                    {
                        UpdateStatusRevisiPhotographer();
                        SendEmailAllType();

                    }
                    else if (URUTAN == 5)
                    {
                        //

                    }
                    else if (URUTAN == 6)
                    {
                        UpdateStatusRevisiDigitalImaging();
                        SendEmailAllType();

                    }
                    else if (URUTAN == 7)
                    {
                        UpdateStatusRevisiHeadDesign();
                        SendEmailAllType();
                    }
                    else if (URUTAN == 8)
                    {
                        UpdateStatusRevisiGraphicDesign();
                        SendEmailAllType();
                    }
                    else if (URUTAN == 9)
                    {
                        //UpdateStatusRevisiCreativeManager();
                    }

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateReviseDesignAll()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mengambil Posisi CM. Karena CM Ada terdapat 3 action sekaligus.
                string KodeJabatan = "";

                DsUser = MsUserDA.GetDataFilter(string.Format("USERNAME = '{0}'", HfUsername.Value));
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    KodeJabatan = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                }

                string Status = label_statusvalue.Text;
                string ACTIONGENERAL = "";

                if (HfPhotoGrapher.Value == EYesNo.Yes || ddldigitalimaging.Text == EYesNo.Yes)
                {
                    if (KodeJabatan == "CM" && (Status == EApprovalStatus.ApprovedPhoto || Status == EApprovalStatus.OnRevisePhoto))
                    {
                        ACTIONGENERAL = "Approved-PhotoGrapher-Photo";
                    }
                    else if (KodeJabatan == "CM" && (Status == EApprovalStatus.ApprovedDI || Status == EApprovalStatus.OnRevisePhotoDI))
                    {
                        ACTIONGENERAL = "Approved-PhotoGrapher-DI";
                    }
                    else
                    {
                        ACTIONGENERAL = "Approved";
                    }
                }
                else if (HfPhotoGrapher.Value == EYesNo.No)
                {
                    ACTIONGENERAL = "Approved";
                }
                else
                {
                    ACTIONGENERAL = "ALL";
                }

                HfActionGeneral.Value = ACTIONGENERAL;

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUserCustom(HfUsername.Value, KODE_FORM, HfActionGeneral.Value);

                int URUTAN = 0;
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                    if (URUTAN == 9)
                    {
                        UpdateStatusRevisiDesign();
                        SendEmailAllType();
                    }

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //Action Head Department
        public void UpdateStatusApproved()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string MENYETUJUI1 = text_menyetujui1.Text;
                DateTime TGL_MENYETUJUI1 = DateTime.Now;
                string PHOTOGRAPHER = HfPhotoGrapher.Value;
                string DIGITAL_IMAGING = ddldigitalimaging.Text;
                string STATUS = "";
                string DESCRIPTION = "";

                Int64 ID = 0;
                string ID_DEPT_USER = "";
                string USERNEXT = "";
                string DEPT_USER_NEXT = "";
                string Email = "";
                string KD_JABATAN = "";

                int URUTAN = 0;
                string ACTION = "";
                string PAGE_NAME = "";
                string SP = "";

                int URUTAN_NEXT = 0;
                string ACTION_NEXT = "";
                string PAGE_NAME_NEXT = "";
                string SP_NEXT = "";


                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet DsUser = new DataSet();

                if (PHOTOGRAPHER == "Yes")
                {
                    STATUS = EApprovalStatus.ApprovedHDPhoto;
                    DESCRIPTION = "Update Status From " + EApprovalStatus.OnApprovedHD + " To " + EApprovalStatus.ApprovedHDPhoto;

                    string Where = string.Format("KD_JABATAN = 'PHOTO'");
                    DsUser = MsUserDA.GetDataFilter(Where);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                        ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                        USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                        DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                        DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                        DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }
                    }

                }
                else if (DIGITAL_IMAGING == "Yes" && PHOTOGRAPHER == "No")
                {
                    STATUS = EApprovalStatus.ApprovedHDDI;
                    DESCRIPTION = "Update Status From " + EApprovalStatus.OnApprovedHD + " To " + EApprovalStatus.ApprovedHDDI;

                    string Where = string.Format("KD_JABATAN = 'DI'");
                    DsUser = MsUserDA.GetDataFilter(Where);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                        ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                        USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                        DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                        DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                        DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }
                    }
                }
                else
                {
                    STATUS = EApprovalStatus.ApprovedHD;
                    DESCRIPTION = "Update Status From " + EApprovalStatus.OnApprovedHD + " To " + EApprovalStatus.ApprovedHD;

                    string Where = string.Format("KD_JABATAN = 'ADM-CREATIVE'");
                    DsUser = MsUserDA.GetDataFilter(Where);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                        ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                        USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                        DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                        DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                        DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }
                    }
                }


                TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                TrForm3gdr.NO_FORM = NO_FORM;
                TrForm3gdr.MENYETUJUI1 = MENYETUJUI1;
                TrForm3gdr.TGL_MENYETUJUI1 = TGL_MENYETUJUI1;
                TrForm3gdr.STATUS = STATUS;
                //TrForm3gdr.REVISI = "";
                TrForm3gdr.USER_CURRENT = HfUsername.Value;
                TrForm3gdr.NEXT_USER = USERNEXT;
                TrForm3gdr.URUTAN_USER_CURRENT = URUTAN;
                TrForm3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;

                TrForm3Gdr.UpdateMenyetujui1(TrForm3gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm3gdractivity.USERNAME = HfUsername.Value;
                TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                TrForm3gdractivity.NO_FORM = NO_FORM;
                TrForm3gdractivity.STATUS = STATUS;
                TrForm3gdractivity.DESCRIPTION = DESCRIPTION;
                TrForm3gdractivity.REVISION = "-";
                TrForm3gdractivity.URUTAN = 2;
                TrForm3gdractivity.SP = "";
                TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm3gdractivity.NEXT_USER = USERNEXT;
                TrForm3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                TrForm3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm3GdrActivity.Insert(TrForm3gdractivity);


                DivMessage.InnerText = "Data Successful Approved Head Department";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //Action Adm Design
        public void UpdateStatusAcceptedHeadDesign()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                TR_FORM_GDR_CUST_DA TrFormGdrCust = new DataLayer.TR_FORM_GDR_CUST_DA();
                DataSet DsCust = new DataSet();

                string NO_FORM = text_noform.Text;
                string JENIS = ddljenis.Text;

                string JADWAL_SELESAI_DESAIN = "";
                if (text_jadwalselesaidisain.Text != "")
                {
                    JADWAL_SELESAI_DESAIN = DateTime.Parse(text_jadwalselesaidisain.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_SELESAI_DESAIN = "1900-01-01";
                }

                string JADWAL_PRODUKSI_CETAK = "";
                if (text_jadwalproduksi.Text != "")
                {
                    JADWAL_PRODUKSI_CETAK = DateTime.Parse(text_jadwalproduksi.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_PRODUKSI_CETAK = "1900-01-01";
                }


                string JADWAL_KIRIM = "";
                if (text_jadwalkirim.Text != "")
                {
                    JADWAL_KIRIM = DateTime.Parse(text_jadwalkirim.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_KIRIM = "1900-01-01";
                }

                string JADWAL_FOTO = "";
                if (text_jadwalfoto.Text != "")
                {
                    JADWAL_FOTO = DateTime.Parse(text_jadwalfoto.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_FOTO = "1900-01-01";
                }

                string JADWAL_DI = "";
                if (text_jadwaldi.Text != "")
                {
                    JADWAL_DI = DateTime.Parse(text_jadwaldi.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_DI = "1900-01-01";
                }

                string JADWAL_ADM_CREATIVE = "";
                if (text_jadwaladmcreative.Text != "")
                {
                    JADWAL_ADM_CREATIVE = DateTime.Parse(text_jadwaladmcreative.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    JADWAL_ADM_CREATIVE = "1900-01-01";
                }


                string DITERIMA_1 = text_diterima1.Text;
                string DITERIMA_2 = ddlditerima2.SelectedValue.ToString();
                DateTime TGL_DITERIMA_1 = DateTime.Now;
                string PRODUCTION = ddlproduction.SelectedValue.ToString();
                string REVISI = text_commentar.Text;

                DateTime JadwalSelesaiDesain = Convert.ToDateTime(text_jadwalselesaidisain.Text);
                var todaysDate = DateTime.Today;
                int result = DateTime.Compare(JadwalSelesaiDesain, todaysDate);

                if (REVISI != "")
                {

                    if (result > 0)
                    {
                        if (JENIS == "DEALERS" || JENIS == "INTERNAL_DEPARTMENT")
                        {
                            string WhereCust = string.Format("KODE_FORM = '{0}' AND NO_FORM = '{1}'", KODE_FORM, NO_FORM);
                            TrFormGdrCust.DeleteFilter(WhereCust);
                        }
                        else
                        {
                            SaveDetailCustCt();
                        }


                        TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                        TrForm3gdr.NO_FORM = NO_FORM;
                        TrForm3gdr.JADWAL_SELESAI_DESAIN = Convert.ToDateTime(JADWAL_SELESAI_DESAIN);
                        TrForm3gdr.JADWAL_PRODUKSI_CETAK = Convert.ToDateTime(JADWAL_PRODUKSI_CETAK);
                        TrForm3gdr.JADWAL_KIRIM = Convert.ToDateTime(JADWAL_KIRIM);
                        TrForm3gdr.JADWAL_FOTO = Convert.ToDateTime(JADWAL_FOTO);
                        TrForm3gdr.JADWAL_DI = Convert.ToDateTime(JADWAL_DI);
                        TrForm3gdr.JADWAL_ADM_CREATIVE = Convert.ToDateTime(JADWAL_ADM_CREATIVE);
                        TrForm3gdr.DITERIMA_1 = DITERIMA_1;
                        TrForm3gdr.TGL_DITERIMA_1 = TGL_DITERIMA_1;
                        TrForm3gdr.DITERIMA_2 = DITERIMA_2;
                        TrForm3gdr.STATUS = EApprovalStatus.AcceptedHeadDesign;
                        TrForm3gdr.REVISI = REVISI;

                        Int64 ID = 0;
                        string ID_DEPT_USER = "";
                        string USERNEXT = "";
                        string DEPT_USER_NEXT = "";
                        string Email = "";
                        string KD_JABATAN = "";

                        int URUTAN = 0;
                        string ACTION = "";
                        string PAGE_NAME = "";
                        string SP = "";

                        int URUTAN_NEXT = 0;
                        string ACTION_NEXT = "";
                        string PAGE_NAME_NEXT = "";
                        string SP_NEXT = "";


                        MS_USER_DA MsUserDA = new MS_USER_DA();
                        DataSet DsUser = new DataSet();

                        string Where = string.Format("USERNAME = '{0}' And KD_JABATAN = 'GD'", DITERIMA_2);
                        DsUser = MsUserDA.GetDataFilter(Where);

                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                            ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                            USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                            DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                            Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                            KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                            //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                            DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                            if (DsUser.Tables[0].Rows.Count > 0)
                            {
                                URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                                ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                                PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                                SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                            }

                            //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                            DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                            if (DsUser.Tables[0].Rows.Count > 0)
                            {
                                URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                                ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                                PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                                SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                            }
                        }
                        TrForm3gdr.USER_CURRENT = HfUsername.Value;
                        TrForm3gdr.NEXT_USER = USERNEXT;
                        TrForm3gdr.URUTAN_USER_CURRENT = URUTAN;
                        TrForm3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                        TrForm3gdr.PRODUCTION = PRODUCTION;
                        TrForm3Gdr.UpdateDiterima1(TrForm3gdr);

                        UpdateDetailMateriCetak();

                        TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                        DataSet DsFormActivity = new DataSet();

                        TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                        TrForm3gdractivity.USERNAME = HfUsername.Value;
                        TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                        TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                        TrForm3gdractivity.NO_FORM = NO_FORM;
                        TrForm3gdractivity.STATUS = EApprovalStatus.AcceptedHeadDesign;
                        TrForm3gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.ApprovedHD + " To " + EApprovalStatus.AcceptedHeadDesign;
                        TrForm3gdractivity.REVISION = REVISI;
                        TrForm3gdractivity.URUTAN = 7;
                        TrForm3gdractivity.SP = "";
                        TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                        TrForm3gdractivity.NEXT_USER = USERNEXT;
                        TrForm3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                        TrForm3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                        TrForm3GdrActivity.Insert(TrForm3gdractivity);

                        DivMessage.InnerText = "Data Successful Accepted Admin Creative";
                        DivMessage.Style.Add("color", "black");
                        DivMessage.Style.Add("background-color", "skyblue");
                        DivMessage.Attributes["class"] = "error";
                        //DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;

                        string HomePageUrl = "../MainMenu.aspx";
                        Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                        SendEmailAllType();
                        //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                    }
                    else
                    {
                        DivMessage.InnerText = "Tanggal Jadwal Desain Tidak Boleh Lebih Kecil Dari Tanggal Hari Ini";
                        DivMessage.Attributes["class"] = "error";
                        //DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                    }
                }
                else
                {
                    DivMessage.InnerText = "Komentar Cannot Be Empty";
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //Action Graphic Design
        public void UpdateStatusAcceptedGraphicDesign()
        {
            try
            {

                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_2 = ddlditerima2.Text;
                DateTime TGL_DITERIMA_2 = DateTime.Now;

                TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                TrForm3gdr.NO_FORM = NO_FORM;
                TrForm3gdr.DITERIMA_2 = DITERIMA_2;
                TrForm3gdr.TGL_DITERIMA_2 = TGL_DITERIMA_2;
                TrForm3gdr.STATUS = EApprovalStatus.AcceptedGraphicDesign;
                //TrForm3gdr.REVISI = "";
                Int64 ID = 0;
                string ID_DEPT_USER = "";
                string USERNEXT = "";
                string DEPT_USER_NEXT = "";
                string Email = "";
                string KD_JABATAN = "";

                int URUTAN = 0;
                string ACTION = "";
                string PAGE_NAME = "";
                string SP = "";

                int URUTAN_NEXT = 0;
                string ACTION_NEXT = "";
                string PAGE_NAME_NEXT = "";
                string SP_NEXT = "";


                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet DsUser = new DataSet();

                string Where = string.Format("ID_DEPT NOT LIKE '%13%' And KD_JABATAN = 'CM'");
                DsUser = MsUserDA.GetDataFilter(Where);

                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                    ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                    DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                    KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                    DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }

                    string ACTIONCM = "Approved";

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                    DsUser = MsUserDA.GetDataUrutanUserCustom(USERNEXT, KODE_FORM, ACTIONCM);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }
                }
                TrForm3gdr.USER_CURRENT = HfUsername.Value;
                TrForm3gdr.NEXT_USER = USERNEXT;
                TrForm3gdr.URUTAN_USER_CURRENT = URUTAN;
                TrForm3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm3Gdr.UpdateDiterima2(TrForm3gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm3gdractivity.USERNAME = HfUsername.Value;
                TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                TrForm3gdractivity.NO_FORM = NO_FORM;
                TrForm3gdractivity.STATUS = EApprovalStatus.AcceptedGraphicDesign;
                TrForm3gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.AcceptedHeadDesign + " To " + EApprovalStatus.AcceptedGraphicDesign;
                TrForm3gdractivity.REVISION = "-";
                TrForm3gdractivity.URUTAN = 8;
                TrForm3gdractivity.SP = "*";
                TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm3gdractivity.NEXT_USER = USERNEXT;
                TrForm3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                TrForm3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm3GdrActivity.Insert(TrForm3gdractivity);

                DivMessage.InnerText = "Data Successful Accepted Graphic Design";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //Action Creative Manager
        public void UpdateStatusApprovedCreativeManager()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_3 = text_diterima3.Text;
                DateTime TGL_DITERIMA_3 = DateTime.Now;
                string STATUS_VER = "D";
                string PRODUCTION = ddlproduction.SelectedValue.ToString();
                string STATUS = "";
                string DESCRIPTION = "";

                if (PRODUCTION == EYesNo.Yes)
                {
                    STATUS = EApprovalStatus.DoneProduction;
                    DESCRIPTION = "Update Status From " + EApprovalStatus.AcceptedGraphicDesign + " To " + EApprovalStatus.DoneProduction;

                    TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                    TrForm3gdr.NO_FORM = NO_FORM;
                    TrForm3gdr.DITERIMA_3 = DITERIMA_3;
                    TrForm3gdr.TGL_DITERIMA_3 = TGL_DITERIMA_3;
                    TrForm3gdr.STATUS = STATUS;
                    //TrForm3gdr.REVISI = "";

                    LoadStatusVer();
                    TrForm3gdr.STATUS_VER = HfStatusVer.Value + STATUS_VER;
                    Int64 ID = 0;
                    string ID_DEPT_USER = "";
                    string USERNEXT = "";
                    string DEPT_USER_NEXT = "";
                    string Email = "";
                    string KD_JABATAN = "";

                    int URUTAN = 0;
                    string ACTION = "";
                    string PAGE_NAME = "";
                    string SP = "";

                    int URUTAN_NEXT = 0;
                    string ACTION_NEXT = "";
                    string PAGE_NAME_NEXT = "";
                    string SP_NEXT = "";

                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    string Where = string.Format("KD_JABATAN = 'PDC'");
                    DsUser = MsUserDA.GetDataFilter(Where);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                        ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                        USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                        DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                        string ACTIONCM = "Approved";
                        //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                        DsUser = MsUserDA.GetDataUrutanUserCustom(HfUsername.Value, KODE_FORM, ACTIONCM);
                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                        DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }
                    }
                    TrForm3gdr.USER_CURRENT = HfUsername.Value;
                    TrForm3gdr.NEXT_USER = USERNEXT;
                    TrForm3gdr.URUTAN_USER_CURRENT = URUTAN;
                    TrForm3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;

                    TrForm3Gdr.UpdateDiterima3(TrForm3gdr);

                    TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                    TrForm3gdractivity.USERNAME = HfUsername.Value;
                    TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                    TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                    TrForm3gdractivity.NO_FORM = NO_FORM;
                    TrForm3gdractivity.STATUS = STATUS;
                    TrForm3gdractivity.DESCRIPTION = DESCRIPTION;
                    TrForm3gdractivity.REVISION = "-";
                    TrForm3gdractivity.URUTAN = 9;
                    TrForm3gdractivity.SP = "";
                    TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                    TrForm3gdractivity.NEXT_USER = USERNEXT;
                    TrForm3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    TrForm3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3GdrActivity.Insert(TrForm3gdractivity);

                }
                else
                {
                    STATUS = EApprovalStatus.Done;
                    DESCRIPTION = "Update Status From " + EApprovalStatus.AcceptedGraphicDesign + " To " + EApprovalStatus.Done;

                    TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                    TrForm3gdr.NO_FORM = NO_FORM;
                    TrForm3gdr.DITERIMA_3 = DITERIMA_3;
                    TrForm3gdr.TGL_DITERIMA_3 = TGL_DITERIMA_3;
                    TrForm3gdr.STATUS = STATUS;
                    //TrForm3gdr.REVISI = "";

                    LoadStatusVer();
                    TrForm3gdr.STATUS_VER = HfStatusVer.Value + STATUS_VER;
                    Int64 ID = 0;
                    string ID_DEPT_USER = "";
                    string USERNEXT = "";
                    string DEPT_USER_NEXT = "";
                    string Email = "";
                    string KD_JABATAN = "";

                    int URUTAN = 0;
                    string ACTION = "";
                    string PAGE_NAME = "";
                    string SP = "";

                    int URUTAN_NEXT = 0;
                    string ACTION_NEXT = "";
                    string PAGE_NAME_NEXT = "";
                    string SP_NEXT = "";

                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    string Where = string.Format("ID_DEPT NOT LIKE '%13%' And KD_JABATAN = 'CM'");
                    DsUser = MsUserDA.GetDataFilter(Where);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                        ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                        USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                        DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                        DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                        DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }
                    }
                    TrForm3gdr.USER_CURRENT = HfUsername.Value;
                    TrForm3gdr.NEXT_USER = USERNEXT;
                    TrForm3gdr.URUTAN_USER_CURRENT = URUTAN;
                    TrForm3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;

                    TrForm3Gdr.UpdateDiterima3(TrForm3gdr);

                    TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                    TrForm3gdractivity.USERNAME = HfUsername.Value;
                    TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                    TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                    TrForm3gdractivity.NO_FORM = NO_FORM;
                    TrForm3gdractivity.STATUS = STATUS;
                    TrForm3gdractivity.DESCRIPTION = DESCRIPTION;
                    TrForm3gdractivity.REVISION = "-";
                    TrForm3gdractivity.URUTAN = 9;
                    TrForm3gdractivity.SP = "";
                    TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                    TrForm3gdractivity.NEXT_USER = USERNEXT;
                    TrForm3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    TrForm3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3GdrActivity.Insert(TrForm3gdractivity);

                }




                DivMessage.InnerText = "Data Berhasil Di Approve Creative Manager";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //PhotoGrapher

        //Action PhotoGrapher

        public void UpdateStatusApprovedPhotographer()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_LAIN_1 = text_diterimalain1.Text;
                DateTime TGL_DITERIMA_LAIN_1 = DateTime.Now;
                string RFR_LAMPIRAN1_PG = "";
                string RFR_LAMPIRAN2_PG = "";
                string RFR_LAMPIRAN3_PG = "";
                string RFR_LAMPIRAN4_PG = "";


                TR_FORM3_GDR trform3gdr = new TR_FORM3_GDR();
                trform3gdr.NO_FORM = NO_FORM;
                trform3gdr.DITERIMA_LAIN_1 = DITERIMA_LAIN_1;
                trform3gdr.TGL_DITERIMA_LAIN_1 = TGL_DITERIMA_LAIN_1;
                trform3gdr.STATUS = EApprovalStatus.ApprovedPhoto;

                //Upload File Photographer
                if (linkbtn_filenamepg1.Text == "-" || linkbtn_filenamepg1.Text == "")
                {
                    if (btn_uploadfilepg1.HasFile)
                    {
                        int imgSize = btn_uploadfilepg1.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfilepg1.PostedFile.FileName).ToLower();
                        if (btn_uploadfilepg1.PostedFile != null && btn_uploadfilepg1.PostedFile.FileName != "")
                        {


                            if (btn_uploadfilepg1.PostedFile.ContentLength > 3000000)
                            {
                                DivMessage.InnerText = "File 1 is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            //if (ext != ".jpg" || ext != ".png" || ext != ".gif" || ext != ".jpeg")
                            //{
                            //    DivMessage.InnerText = "Format File Tidak Ada. Harap File Berformat(.jpg, .png and .gif)";
                            //    DivMessage.Attributes["class"] = "error";
                            //    //DivMessage.Attributes["class"] = "success";
                            //    DivMessage.Visible = true;
                            //    return;
                            //}

                            else
                            {
                                RFR_LAMPIRAN1_PG = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfile1.FileName;
                                btn_uploadfilepg1.PostedFile
              .SaveAs(Server.MapPath("~/Uploaded/FileUploadPG/") + RFR_LAMPIRAN1_PG);
                            }
                        }
                    }
                }
                else
                {
                    RFR_LAMPIRAN1_PG = linkbtn_filenamepg1.Text;
                }

                trform3gdr.RFR_LAMPIRAN1_PG = RFR_LAMPIRAN1_PG;

                if (linkbtn_filenamepg2.Text == "-" || linkbtn_filenamepg2.Text == "")
                {
                    if (btn_uploadfilepg2.HasFile)
                    {
                        int imgSize = btn_uploadfilepg2.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfilepg2.PostedFile.FileName).ToLower();
                        if (btn_uploadfilepg2.PostedFile != null && btn_uploadfilepg2.PostedFile.FileName != "")
                        {


                            if (btn_uploadfilepg2.PostedFile.ContentLength > 3000000)
                            {
                                DivMessage.InnerText = "File 2 is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            else
                            {
                                RFR_LAMPIRAN2_PG = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfile2.FileName;
                                btn_uploadfilepg2.PostedFile
                                    .SaveAs(Server.MapPath("~/Uploaded/FileUploadPG/") + RFR_LAMPIRAN2_PG);
                            }

                        }
                    }
                }
                else
                {
                    RFR_LAMPIRAN2_PG = linkbtn_filenamepg2.Text;
                }
                trform3gdr.RFR_LAMPIRAN2_PG = RFR_LAMPIRAN2_PG;


                if (linkbtn_filenamepg3.Text == "-" || linkbtn_filenamepg3.Text == "")
                {
                    if (btn_uploadfilepg3.HasFile)
                    {
                        int imgSize = btn_uploadfilepg3.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfilepg3.PostedFile.FileName).ToLower();
                        if (btn_uploadfilepg3.PostedFile != null && btn_uploadfilepg3.PostedFile.FileName != "")
                        {


                            if (btn_uploadfilepg3.PostedFile.ContentLength > 3000000)
                            {
                                DivMessage.InnerText = "File 3 is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            else
                            {
                                RFR_LAMPIRAN3_PG = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfile3.FileName;
                                btn_uploadfilepg3.PostedFile
                                    .SaveAs(Server.MapPath("~/Uploaded/FileUploadPG/") + RFR_LAMPIRAN3_PG);
                            }
                        }
                    }
                }
                else
                {
                    RFR_LAMPIRAN3_PG = linkbtn_filenamepg3.Text;
                }
                trform3gdr.RFR_LAMPIRAN3_PG = RFR_LAMPIRAN3_PG;


                if (linkbtn_filenamepg4.Text == "-" || linkbtn_filenamepg4.Text == "")
                {
                    if (btn_uploadfilepg4.HasFile)
                    {
                        int imgSize = btn_uploadfilepg4.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfilepg4.PostedFile.FileName).ToLower();
                        if (btn_uploadfilepg4.PostedFile != null && btn_uploadfilepg4.PostedFile.FileName != "")
                        {


                            if (btn_uploadfilepg4.PostedFile.ContentLength > 3000000)
                            {
                                DivMessage.InnerText = "File 4 is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            else
                            {
                                RFR_LAMPIRAN4_PG = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfile4.FileName;
                                btn_uploadfilepg4.PostedFile
                                    .SaveAs(Server.MapPath("~/Uploaded/FileUploadPG/") + RFR_LAMPIRAN4_PG);
                            }
                        }

                    }
                }
                else
                {
                    RFR_LAMPIRAN4_PG = linkbtn_filenamepg4.Text;
                }
                trform3gdr.RFR_LAMPIRAN4_PG = RFR_LAMPIRAN4_PG;


                //trform3gdr.REVISI = "";
                Int64 ID = 0;
                string ID_DEPT_USER = "";
                string USERNEXT = "";
                string DEPT_USER_NEXT = "";
                string Email = "";
                string KD_JABATAN = "";

                int URUTAN = 0;
                string ACTION = "";
                string PAGE_NAME = "";
                string SP = "";

                int URUTAN_NEXT = 0;
                string ACTION_NEXT = "";
                string PAGE_NAME_NEXT = "";
                string SP_NEXT = "";


                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet DsUser = new DataSet();

                string Where = string.Format("ID_DEPT NOT LIKE '%13%' And KD_JABATAN = 'CM'");
                DsUser = MsUserDA.GetDataFilter(Where);

                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                    ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                    DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                    KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                    DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }

                    string Status = label_statusvalue.Text;
                    string ACTIONCM = "";
                    if (HfPhotoGrapher.Value == EYesNo.Yes)
                    {
                        if (Status == EApprovalStatus.ApprovedHDPhoto || Status == EApprovalStatus.OnRevisePhoto)
                        {
                            ACTIONCM = "Approved-PhotoGrapher-Photo";
                        }
                        else
                        {
                            ACTIONCM = "Approved";
                        }
                    }

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                    DsUser = MsUserDA.GetDataUrutanUserCustom(USERNEXT, KODE_FORM, ACTIONCM);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }
                }
                trform3gdr.USER_CURRENT = HfUsername.Value;
                trform3gdr.NEXT_USER = USERNEXT;
                trform3gdr.URUTAN_USER_CURRENT = URUTAN;
                trform3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm3Gdr.UpdateDiterimaLain1(trform3gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY trform3gdractivity = new TR_FORM_GDR_ACTIVITY();
                trform3gdractivity.USERNAME = HfUsername.Value;
                trform3gdractivity.ACTIVITY_TIME = DateTime.Now;
                trform3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                trform3gdractivity.NO_FORM = NO_FORM;
                trform3gdractivity.STATUS = EApprovalStatus.ApprovedPhoto;
                trform3gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.ApprovedHDPhoto + " To " + EApprovalStatus.ApprovedPhoto;
                trform3gdractivity.REVISION = "-";
                trform3gdractivity.URUTAN = 3;
                trform3gdractivity.SP = "";
                trform3gdractivity.USER_CURRENT = HfUsername.Value;
                trform3gdractivity.NEXT_USER = USERNEXT;
                trform3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                trform3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm3GdrActivity.Insert(trform3gdractivity);

                DivMessage.InnerText = "Data Successful Approved Photographer";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_VM.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        //Action Creative Director PhotoGrapher
        public void UpdateStatusApprovedCreativeManagerPhoto()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string PHOTOGRAPHER = ddlphotographer.SelectedValue.ToString();
                string DIGITAL_IMAGING = ddldigitalimaging.SelectedValue.ToString();
                string DITERIMA_LAIN_2 = text_diterimalain2.Text;
                DateTime TGL_DITERIMA_LAIN_2 = DateTime.Now;

                if (PHOTOGRAPHER == EYesNo.Yes && DIGITAL_IMAGING == EYesNo.Yes)
                {
                    TR_FORM3_GDR trform3gdr = new TR_FORM3_GDR();
                    trform3gdr.NO_FORM = NO_FORM;
                    trform3gdr.DITERIMA_LAIN_2 = DITERIMA_LAIN_2;
                    trform3gdr.TGL_DITERIMA_LAIN_2 = TGL_DITERIMA_LAIN_2;
                    trform3gdr.STATUS = EApprovalStatus.ApprovedCreativeManagerPG;
                    //trform3gdr.REVISI = "";
                    Int64 ID = 0;
                    string ID_DEPT_USER = "";
                    string USERNEXT = "";
                    string DEPT_USER_NEXT = "";
                    string Email = "";
                    string KD_JABATAN = "";

                    int URUTAN = 0;
                    string ACTION = "";
                    string PAGE_NAME = "";
                    string SP = "";

                    int URUTAN_NEXT = 0;
                    string ACTION_NEXT = "";
                    string PAGE_NAME_NEXT = "";
                    string SP_NEXT = "";

                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    string Where = string.Format("KD_JABATAN = 'DI'");
                    DsUser = MsUserDA.GetDataFilter(Where);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                        ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                        USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                        DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                        DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                        DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }
                    }
                    trform3gdr.USER_CURRENT = HfUsername.Value;
                    trform3gdr.NEXT_USER = USERNEXT;
                    trform3gdr.URUTAN_USER_CURRENT = URUTAN;
                    trform3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3Gdr.UpdateDiterimaLain2(trform3gdr);

                    TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY trform3gdractivity = new TR_FORM_GDR_ACTIVITY();
                    trform3gdractivity.USERNAME = HfUsername.Value;
                    trform3gdractivity.ACTIVITY_TIME = DateTime.Now;
                    trform3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                    trform3gdractivity.NO_FORM = NO_FORM;
                    trform3gdractivity.STATUS = EApprovalStatus.ApprovedCreativeManagerPG;
                    trform3gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.ApprovedPhoto + " To " + EApprovalStatus.ApprovedCreativeManagerPG;
                    trform3gdractivity.REVISION = "-";
                    trform3gdractivity.URUTAN = 4;
                    trform3gdractivity.SP = "";
                    trform3gdractivity.USER_CURRENT = HfUsername.Value;
                    trform3gdractivity.NEXT_USER = USERNEXT;
                    trform3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    trform3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3GdrActivity.Insert(trform3gdractivity);

                    DivMessage.InnerText = "Data Successful Approved Creative Director-Foto";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../MainMenu.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_VM.aspx");
                }
                else if (PHOTOGRAPHER == EYesNo.Yes && DIGITAL_IMAGING == EYesNo.No)
                {
                    TR_FORM3_GDR trform3gdr = new TR_FORM3_GDR();
                    trform3gdr.NO_FORM = NO_FORM;
                    trform3gdr.DITERIMA_LAIN_2 = DITERIMA_LAIN_2;
                    trform3gdr.TGL_DITERIMA_LAIN_2 = TGL_DITERIMA_LAIN_2;
                    trform3gdr.STATUS = EApprovalStatus.Done;
                    //trform3gdr.REVISI = "";
                    Int64 ID = 0;
                    string ID_DEPT_USER = "";
                    string USERNEXT = "";
                    string DEPT_USER_NEXT = "";
                    string Email = "";
                    string KD_JABATAN = "";

                    int URUTAN = 0;
                    string ACTION = "";
                    string PAGE_NAME = "";
                    string SP = "";

                    int URUTAN_NEXT = 0;
                    string ACTION_NEXT = "";
                    string PAGE_NAME_NEXT = "";
                    string SP_NEXT = "";

                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    string Where = string.Format("ID_DEPT NOT LIKE '%13%' And KD_JABATAN = 'CM'");
                    DsUser = MsUserDA.GetDataFilter(Where);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                        ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                        USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                        DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                        string ACTIONCM = "Approved-PhotoGrapher-Photo";
                        //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                        DsUser = MsUserDA.GetDataUrutanUserCustom(HfUsername.Value, KODE_FORM, ACTIONCM);
                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                        DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }
                    }
                    trform3gdr.USER_CURRENT = HfUsername.Value;
                    trform3gdr.NEXT_USER = USERNEXT;
                    trform3gdr.URUTAN_USER_CURRENT = URUTAN;
                    trform3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3Gdr.UpdateDiterimaLain2(trform3gdr);

                    TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY trform3gdractivity = new TR_FORM_GDR_ACTIVITY();
                    trform3gdractivity.USERNAME = HfUsername.Value;
                    trform3gdractivity.ACTIVITY_TIME = DateTime.Now;
                    trform3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                    trform3gdractivity.NO_FORM = NO_FORM;
                    trform3gdractivity.STATUS = EApprovalStatus.Done;
                    trform3gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.ApprovedPhoto + " To " + EApprovalStatus.Done;
                    trform3gdractivity.REVISION = "-";
                    trform3gdractivity.URUTAN = 4;
                    trform3gdractivity.SP = "";
                    trform3gdractivity.USER_CURRENT = HfUsername.Value;
                    trform3gdractivity.NEXT_USER = USERNEXT;
                    trform3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    trform3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3GdrActivity.Insert(trform3gdractivity);

                    DivMessage.InnerText = "Data Successful Approved Creative Director-Foto (Done)";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../MainMenu.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_VM.aspx");
                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        //Digital Imaging

        //Action Digital Imaging
        public void UpdateStatusApprovedDigitalImaging()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_LAIN_3 = text_diterimalain3.Text;
                DateTime TGL_DITERIMA_LAIN_3 = DateTime.Now;
                string RFR_LAMPIRAN1_DI = "";
                string RFR_LAMPIRAN2_DI = "";
                string RFR_LAMPIRAN3_DI = "";
                string RFR_LAMPIRAN4_DI = "";


                TR_FORM3_GDR trform3gdr = new TR_FORM3_GDR();
                trform3gdr.NO_FORM = NO_FORM;
                trform3gdr.DITERIMA_LAIN_3 = DITERIMA_LAIN_3;
                trform3gdr.TGL_DITERIMA_LAIN_3 = TGL_DITERIMA_LAIN_3;
                trform3gdr.STATUS = EApprovalStatus.ApprovedDI;

                //Upload File Digital Imaging
                if (linkbtn_filenamedi1.Text == "-" || linkbtn_filenamedi1.Text == "")
                {
                    if (btn_uploadfiledi1.HasFile)
                    {
                        int imgSize = btn_uploadfiledi1.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfiledi1.PostedFile.FileName).ToLower();
                        if (btn_uploadfiledi1.PostedFile != null && btn_uploadfiledi1.PostedFile.FileName != "")
                        {


                            if (btn_uploadfiledi1.PostedFile.ContentLength > 3000000)
                            {
                                DivMessage.InnerText = "File 1 is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            //if (ext != ".jpg" || ext != ".png" || ext != ".gif" || ext != ".jpeg")
                            //{
                            //    DivMessage.InnerText = "Format File Tidak Ada. Harap File Berformat(.jpg, .png and .gif)";
                            //    DivMessage.Attributes["class"] = "error";
                            //    //DivMessage.Attributes["class"] = "success";
                            //    DivMessage.Visible = true;
                            //    return;
                            //}

                            else
                            {
                                RFR_LAMPIRAN1_DI = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfiledi1.FileName;
                                btn_uploadfiledi1.PostedFile
              .SaveAs(Server.MapPath("~/Uploaded/FileUploadDI/") + RFR_LAMPIRAN1_DI);
                            }
                        }
                    }
                }
                else
                {
                    RFR_LAMPIRAN1_DI = linkbtn_filenamedi1.Text;
                }

                trform3gdr.RFR_LAMPIRAN1_DI = RFR_LAMPIRAN1_DI;

                if (linkbtn_filenamedi2.Text == "-" || linkbtn_filenamedi2.Text == "")
                {
                    if (btn_uploadfiledi2.HasFile)
                    {
                        int imgSize = btn_uploadfiledi2.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfiledi2.PostedFile.FileName).ToLower();
                        if (btn_uploadfiledi2.PostedFile != null && btn_uploadfiledi2.PostedFile.FileName != "")
                        {


                            if (btn_uploadfiledi2.PostedFile.ContentLength > 3000000)
                            {
                                DivMessage.InnerText = "File 2 is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            else
                            {
                                RFR_LAMPIRAN2_DI = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfiledi2.FileName;
                                btn_uploadfiledi2.PostedFile
                                    .SaveAs(Server.MapPath("~/Uploaded/FileUploadDI/") + RFR_LAMPIRAN2_DI);
                            }

                        }
                    }
                }
                else
                {
                    RFR_LAMPIRAN2_DI = linkbtn_filenamedi2.Text;
                }

                trform3gdr.RFR_LAMPIRAN2_DI = RFR_LAMPIRAN2_DI;

                if (linkbtn_filenamedi3.Text == "-" || linkbtn_filenamedi3.Text == "")
                {
                    if (btn_uploadfiledi3.HasFile)
                    {
                        int imgSize = btn_uploadfiledi3.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfiledi3.PostedFile.FileName).ToLower();
                        if (btn_uploadfiledi3.PostedFile != null && btn_uploadfiledi3.PostedFile.FileName != "")
                        {


                            if (btn_uploadfiledi3.PostedFile.ContentLength > 3000000)
                            {
                                DivMessage.InnerText = "File 3 is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            else
                            {
                                RFR_LAMPIRAN3_DI = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfiledi3.FileName;
                                btn_uploadfiledi3.PostedFile
                                    .SaveAs(Server.MapPath("~/Uploaded/FileUploadDI/") + RFR_LAMPIRAN3_DI);
                            }
                        }
                    }
                }

                else
                {
                    RFR_LAMPIRAN3_DI = linkbtn_filenamedi3.Text;
                }

                trform3gdr.RFR_LAMPIRAN3_DI = RFR_LAMPIRAN3_DI;

                if (linkbtn_filenamedi4.Text == "-" || linkbtn_filenamedi4.Text == "")
                {
                    if (btn_uploadfiledi4.HasFile)
                    {
                        int imgSize = btn_uploadfiledi4.PostedFile.ContentLength;
                        string ext = System.IO.Path.GetExtension(this.btn_uploadfiledi4.PostedFile.FileName).ToLower();
                        if (btn_uploadfiledi4.PostedFile != null && btn_uploadfiledi4.PostedFile.FileName != "")
                        {


                            if (btn_uploadfiledi4.PostedFile.ContentLength > 4000000)
                            {
                                DivMessage.InnerText = "File 4 is larger than 3MB.";
                                DivMessage.Attributes["class"] = "error";
                                //DivMessage.Attributes["class"] = "success";
                                DivMessage.Visible = true;
                                return;
                            }
                            else
                            {
                                RFR_LAMPIRAN4_DI = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfiledi4.FileName;
                                btn_uploadfiledi4.PostedFile
                                    .SaveAs(Server.MapPath("~/Uploaded/FileUploadDI/") + RFR_LAMPIRAN4_DI);
                            }
                        }

                    }
                }
                else
                {
                    RFR_LAMPIRAN4_DI = linkbtn_filenamedi4.Text;
                }
                trform3gdr.RFR_LAMPIRAN4_DI = RFR_LAMPIRAN4_DI;


                //trform3gdr.REVISI = "";
                Int64 ID = 0;
                string ID_DEPT_USER = "";
                string USERNEXT = "";
                string DEPT_USER_NEXT = "";
                string Email = "";
                string KD_JABATAN = "";

                int URUTAN = 0;
                string ACTION = "";
                string PAGE_NAME = "";
                string SP = "";

                int URUTAN_NEXT = 0;
                string ACTION_NEXT = "";
                string PAGE_NAME_NEXT = "";
                string SP_NEXT = "";


                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet DsUser = new DataSet();

                string Where = string.Format("ID_DEPT NOT LIKE '%13%' And KD_JABATAN = 'CM'");
                DsUser = MsUserDA.GetDataFilter(Where);

                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                    ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                    DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                    KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                    DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }

                    string Status = label_statusvalue.Text;
                    string ACTIONCM = "";
                    if (HfPhotoGrapher.Value == EYesNo.Yes)
                    {
                        if (Status == EApprovalStatus.ApprovedCreativeManagerPG || Status == EApprovalStatus.OnRevisePhotoDI)
                        {
                            ACTIONCM = "Approved-PhotoGrapher-DI";
                        }
                        else
                        {
                            ACTIONCM = "Approved";
                        }
                    }

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                    DsUser = MsUserDA.GetDataUrutanUserCustom(USERNEXT, KODE_FORM, ACTIONCM);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }
                }
                trform3gdr.USER_CURRENT = HfUsername.Value;
                trform3gdr.NEXT_USER = USERNEXT;
                trform3gdr.URUTAN_USER_CURRENT = URUTAN;
                trform3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm3Gdr.UpdateDiterimaLain3(trform3gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY trform3gdractivity = new TR_FORM_GDR_ACTIVITY();
                trform3gdractivity.USERNAME = HfUsername.Value;
                trform3gdractivity.ACTIVITY_TIME = DateTime.Now;
                trform3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                trform3gdractivity.NO_FORM = NO_FORM;
                trform3gdractivity.STATUS = EApprovalStatus.ApprovedDI;
                trform3gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.ApprovedCreativeManagerPG + " To " + EApprovalStatus.ApprovedDI;
                trform3gdractivity.REVISION = "-";
                trform3gdractivity.URUTAN = 5;
                trform3gdractivity.SP = "";
                trform3gdractivity.USER_CURRENT = HfUsername.Value;
                trform3gdractivity.NEXT_USER = USERNEXT;
                trform3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                trform3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm3GdrActivity.Insert(trform3gdractivity);

                DivMessage.InnerText = "Data Successful Approved Digital Imaging";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_VM.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        //Action Creative Director DI
        public void UpdateStatusApprovedCreativeManagerDigitalImaging()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_LAIN_4 = text_diterimalain4.Text;
                DateTime TGL_DITERIMA_LAIN_4 = DateTime.Now;

                TR_FORM3_GDR trform3gdr = new TR_FORM3_GDR();
                trform3gdr.NO_FORM = NO_FORM;
                trform3gdr.DITERIMA_LAIN_4 = DITERIMA_LAIN_4;
                trform3gdr.TGL_DITERIMA_LAIN_4 = TGL_DITERIMA_LAIN_4;
                trform3gdr.STATUS = EApprovalStatus.ApprovedCreativeManagerDI;
                //trform3gdr.REVISI = "";
                Int64 ID = 0;
                string ID_DEPT_USER = "";
                string USERNEXT = "";
                string DEPT_USER_NEXT = "";
                string Email = "";
                string KD_JABATAN = "";

                int URUTAN = 0;
                string ACTION = "";
                string PAGE_NAME = "";
                string SP = "";

                int URUTAN_NEXT = 0;
                string ACTION_NEXT = "";
                string PAGE_NAME_NEXT = "";
                string SP_NEXT = "";


                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet DsUser = new DataSet();

                string Where = string.Format("KD_JABATAN = 'ADM-CREATIVE'");
                DsUser = MsUserDA.GetDataFilter(Where);

                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                    ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                    DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                    KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                    DsUser = MsUserDA.GetDataUrutanUserCustom(HfUsername.Value, KODE_FORM, HfActionGeneral.Value);
                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                    DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }
                }
                trform3gdr.USER_CURRENT = HfUsername.Value;
                trform3gdr.NEXT_USER = USERNEXT;
                trform3gdr.URUTAN_USER_CURRENT = URUTAN;
                trform3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm3Gdr.UpdateDiterimaLain4(trform3gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY trform3gdractivity = new TR_FORM_GDR_ACTIVITY();
                trform3gdractivity.USERNAME = HfUsername.Value;
                trform3gdractivity.ACTIVITY_TIME = DateTime.Now;
                trform3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                trform3gdractivity.NO_FORM = NO_FORM;
                trform3gdractivity.STATUS = EApprovalStatus.ApprovedCreativeManagerDI;
                trform3gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.ApprovedDI + " To " + EApprovalStatus.ApprovedCreativeManagerDI;
                trform3gdractivity.REVISION = "-";
                trform3gdractivity.URUTAN = 6;
                trform3gdractivity.SP = "";
                trform3gdractivity.USER_CURRENT = HfUsername.Value;
                trform3gdractivity.NEXT_USER = USERNEXT;
                trform3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                trform3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm3GdrActivity.Insert(trform3gdractivity);

                DivMessage.InnerText = "Data Succesful Approved Creative Director-DI";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_VM.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //Production

        //Action Production
        public void UpdateStatusApprovedProductionCetakMateri()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_LAIN_5_MATERI = text_diterimalain5materi.Text;
                DateTime TGL_DITERIMA_LAIN_5_MATERI = DateTime.Now;

                TR_FORM3_GDR trform3gdr = new TR_FORM3_GDR();
                trform3gdr.NO_FORM = NO_FORM;
                trform3gdr.DITERIMA_LAIN_5_MATERI = DITERIMA_LAIN_5_MATERI;
                trform3gdr.TGL_DITERIMA_LAIN_5_MATERI = TGL_DITERIMA_LAIN_5_MATERI;
                trform3gdr.STATUS = EApprovalStatus.DoneProductionCetakMateri;
                //trform3gdr.REVISI = "";
                Int64 ID = 0;
                string ID_DEPT_USER = "";
                string USERNEXT = "";
                string DEPT_USER_NEXT = "";
                string Email = "";
                string KD_JABATAN = "";

                int URUTAN = 0;
                string ACTION = "";
                string PAGE_NAME = "";
                string SP = "";

                int URUTAN_NEXT = 0;
                string ACTION_NEXT = "";
                string PAGE_NAME_NEXT = "";
                string SP_NEXT = "";


                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet DsUser = new DataSet();

                string Where = string.Format("KD_JABATAN = 'PDC'");
                DsUser = MsUserDA.GetDataFilter(Where);

                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                    ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                    DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                    KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                    DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                    DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }
                }
                trform3gdr.USER_CURRENT = HfUsername.Value;
                trform3gdr.NEXT_USER = USERNEXT;
                trform3gdr.URUTAN_USER_CURRENT = URUTAN;
                trform3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm3Gdr.UpdateDiterimaLain5Materi(trform3gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY trform3gdractivity = new TR_FORM_GDR_ACTIVITY();
                trform3gdractivity.USERNAME = HfUsername.Value;
                trform3gdractivity.ACTIVITY_TIME = DateTime.Now;
                trform3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                trform3gdractivity.NO_FORM = NO_FORM;
                trform3gdractivity.STATUS = EApprovalStatus.DoneProductionCetakMateri;
                trform3gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.DoneProduction + " To " + EApprovalStatus.DoneProductionCetakMateri;
                trform3gdractivity.REVISION = "-";
                trform3gdractivity.URUTAN = 10;
                trform3gdractivity.SP = "";
                trform3gdractivity.USER_CURRENT = HfUsername.Value;
                trform3gdractivity.NEXT_USER = USERNEXT;
                trform3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                trform3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm3GdrActivity.Insert(trform3gdractivity);

                DivMessage.InnerText = "Data Successfully Produced in Material Printing";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_VM.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusApprovedProduction()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_LAIN_5 = text_diterimalain5.Text;
                DateTime TGL_DITERIMA_LAIN_5 = DateTime.Now;

                TR_FORM3_GDR trform3gdr = new TR_FORM3_GDR();
                trform3gdr.NO_FORM = NO_FORM;
                trform3gdr.DITERIMA_LAIN_5 = DITERIMA_LAIN_5;
                trform3gdr.TGL_DITERIMA_LAIN_5 = TGL_DITERIMA_LAIN_5;
                trform3gdr.STATUS = EApprovalStatus.DeliveredDistribution;
                //trform3gdr.REVISI = "";
                Int64 ID = 0;
                string ID_DEPT_USER = "";
                string USERNEXT = "";
                string DEPT_USER_NEXT = "";
                string Email = "";
                string KD_JABATAN = "";

                int URUTAN = 0;
                string ACTION = "";
                string PAGE_NAME = "";
                string SP = "";

                int URUTAN_NEXT = 0;
                string ACTION_NEXT = "";
                string PAGE_NAME_NEXT = "";
                string SP_NEXT = "";


                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet DsUser = new DataSet();

                string Where = string.Format("KD_JABATAN = 'PDC'");
                DsUser = MsUserDA.GetDataFilter(Where);

                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                    ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                    DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                    KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                    DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }

                    //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                    DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                        ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                        PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                        SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                    }
                }
                trform3gdr.USER_CURRENT = HfUsername.Value;
                trform3gdr.NEXT_USER = USERNEXT;
                trform3gdr.URUTAN_USER_CURRENT = URUTAN;
                trform3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm3Gdr.UpdateDiterimaLain5(trform3gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY trform3gdractivity = new TR_FORM_GDR_ACTIVITY();
                trform3gdractivity.USERNAME = HfUsername.Value;
                trform3gdractivity.ACTIVITY_TIME = DateTime.Now;
                trform3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                trform3gdractivity.NO_FORM = NO_FORM;
                trform3gdractivity.STATUS = EApprovalStatus.DeliveredDistribution;
                trform3gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.DoneProductionCetakMateri + " To " + EApprovalStatus.DeliveredDistribution;
                trform3gdractivity.REVISION = "-";
                trform3gdractivity.URUTAN = 10;
                trform3gdractivity.SP = "";
                trform3gdractivity.USER_CURRENT = HfUsername.Value;
                trform3gdractivity.NEXT_USER = USERNEXT;
                trform3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                trform3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm3GdrActivity.Insert(trform3gdractivity);

                DivMessage.InnerText = "Data Successful Approved Production";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_VM.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }


        /// <summary>
        /// Update Status Cancel Function
        /// </summary>

        public void UpdateStatusCancel()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DIBUAT = text_dibuat.Text;
                DateTime TGL_DIBUAT = DateTime.Now;

                TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                TrForm3gdr.NO_FORM = NO_FORM;
                TrForm3gdr.DIBUAT = DIBUAT;
                TrForm3gdr.TGL_DIBUAT = TGL_DIBUAT;
                TrForm3gdr.STATUS = EApprovalStatus.Cancel;
                //TrForm3gdr.REVISI = "";

                TrForm3gdr.USER_CURRENT = HfUsername.Value;
                TrForm3gdr.NEXT_USER = "-";
                TrForm3gdr.URUTAN_USER_CURRENT = 1;
                TrForm3gdr.URUTAN_NEXT_USER = 0;
                TrForm3Gdr.UpdateDibuat(TrForm3gdr);


                TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm3gdractivity.USERNAME = HfUsername.Value;
                TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                TrForm3gdractivity.NO_FORM = NO_FORM;
                TrForm3gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm3gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm3gdractivity.REVISION = "-";
                TrForm3gdractivity.URUTAN = 1;
                TrForm3gdractivity.SP = "";
                TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm3gdractivity.NEXT_USER = "-";
                TrForm3gdractivity.URUTAN_USER_CURRENT = 1;
                TrForm3gdractivity.URUTAN_NEXT_USER = 0;
                TrForm3GdrActivity.Insert(TrForm3gdractivity);


                DivMessage.InnerText = "Data Successful Cancel";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusCancelHD()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string MENYETUJUI1 = text_menyetujui1.Text;
                DateTime TGL_MENYETUJUI1 = DateTime.Now;

                TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                TrForm3gdr.NO_FORM = NO_FORM;
                TrForm3gdr.MENYETUJUI1 = MENYETUJUI1;
                TrForm3gdr.TGL_MENYETUJUI1 = TGL_MENYETUJUI1;
                TrForm3gdr.STATUS = EApprovalStatus.Cancel;
                //TrForm3gdr.REVISI = "";
                TrForm3gdr.USER_CURRENT = HfUsername.Value;
                TrForm3gdr.NEXT_USER = "-";
                TrForm3gdr.URUTAN_USER_CURRENT = 1;
                TrForm3gdr.URUTAN_NEXT_USER = 0;
                TrForm3Gdr.UpdateMenyetujui1(TrForm3gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm3gdractivity.USERNAME = HfUsername.Value;
                TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                TrForm3gdractivity.NO_FORM = NO_FORM;
                TrForm3gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm3gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm3gdractivity.REVISION = "-";
                TrForm3gdractivity.URUTAN = 2;
                TrForm3gdractivity.SP = "";
                TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm3gdractivity.NEXT_USER = "-";
                TrForm3gdractivity.URUTAN_USER_CURRENT = 1;
                TrForm3gdractivity.URUTAN_NEXT_USER = 0;
                TrForm3GdrActivity.Insert(TrForm3gdractivity);

                DivMessage.InnerText = "Data Successful Cancel";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusCancelHeadDesign()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_1 = text_diterima1.Text;
                DateTime TGL_DITERIMA_1 = DateTime.Now;

                TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                TrForm3gdr.NO_FORM = NO_FORM;
                TrForm3gdr.DITERIMA_1 = DITERIMA_1;
                TrForm3gdr.TGL_DITERIMA_1 = TGL_DITERIMA_1;
                TrForm3gdr.STATUS = EApprovalStatus.Cancel;
                //TrForm3gdr.REVISI = "";
                TrForm3gdr.REVISI = text_commentar.Text;
                TrForm3gdr.USER_CURRENT = HfUsername.Value;
                TrForm3gdr.NEXT_USER = "-";
                TrForm3gdr.URUTAN_USER_CURRENT = 1;
                TrForm3gdr.URUTAN_NEXT_USER = 0;
                TrForm3Gdr.UpdateDiterima1NonJadwal(TrForm3gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm3gdractivity.USERNAME = HfUsername.Value;
                TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                TrForm3gdractivity.NO_FORM = NO_FORM;
                TrForm3gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm3gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm3gdractivity.REVISION = text_commentar.Text;
                TrForm3gdractivity.URUTAN = 7;
                TrForm3gdractivity.SP = "";
                TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm3gdractivity.NEXT_USER = "-";
                TrForm3gdractivity.URUTAN_USER_CURRENT = 1;
                TrForm3gdractivity.URUTAN_NEXT_USER = 0;
                TrForm3GdrActivity.Insert(TrForm3gdractivity);


                DivMessage.InnerText = "Data Successful Cancel";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusCancelGraphicDesign()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_2 = ddlditerima2.Text;
                DateTime TGL_DITERIMA_2 = DateTime.Now;

                TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                TrForm3gdr.NO_FORM = NO_FORM;
                TrForm3gdr.DITERIMA_2 = DITERIMA_2;
                TrForm3gdr.TGL_DITERIMA_2 = TGL_DITERIMA_2;
                TrForm3gdr.STATUS = EApprovalStatus.Cancel;
                //TrForm3gdr.REVISI = "";
                TrForm3gdr.USER_CURRENT = HfUsername.Value;
                TrForm3gdr.NEXT_USER = "-";
                TrForm3gdr.URUTAN_USER_CURRENT = 1;
                TrForm3gdr.URUTAN_NEXT_USER = 0;
                TrForm3Gdr.UpdateDiterima2(TrForm3gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm3gdractivity.USERNAME = HfUsername.Value;
                TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                TrForm3gdractivity.NO_FORM = NO_FORM;
                TrForm3gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm3gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm3gdractivity.REVISION = "-";
                TrForm3gdractivity.URUTAN = 8;
                TrForm3gdractivity.SP = "";
                TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm3gdractivity.NEXT_USER = "-";
                TrForm3gdractivity.URUTAN_USER_CURRENT = 1;
                TrForm3gdractivity.URUTAN_NEXT_USER = 0;
                TrForm3GdrActivity.Insert(TrForm3gdractivity);


                DivMessage.InnerText = "Data Successful Cancel";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusCancelCreativeManager()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_3 = text_diterima3.Text;
                DateTime TGL_DITERIMA_3 = DateTime.Now;

                TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                TrForm3gdr.NO_FORM = NO_FORM;
                TrForm3gdr.DITERIMA_3 = DITERIMA_3;
                TrForm3gdr.TGL_DITERIMA_3 = TGL_DITERIMA_3;
                TrForm3gdr.STATUS = EApprovalStatus.Cancel;
                //TrForm3gdr.REVISI = "";
                TrForm3gdr.USER_CURRENT = HfUsername.Value;
                TrForm3gdr.NEXT_USER = "-";
                TrForm3gdr.URUTAN_USER_CURRENT = 1;
                TrForm3gdr.URUTAN_NEXT_USER = 0;
                TrForm3Gdr.UpdateDiterima3(TrForm3gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm3gdractivity.USERNAME = HfUsername.Value;
                TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                TrForm3gdractivity.NO_FORM = NO_FORM;
                TrForm3gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm3gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm3gdractivity.REVISION = "-";
                TrForm3gdractivity.URUTAN = 9;
                TrForm3gdractivity.SP = "";
                TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm3gdractivity.NEXT_USER = "-";
                TrForm3gdractivity.URUTAN_USER_CURRENT = 1;
                TrForm3gdractivity.URUTAN_NEXT_USER = 0;
                TrForm3GdrActivity.Insert(TrForm3gdractivity);


                DivMessage.InnerText = "Data Successful Cancel";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusCancelPhotoGrapher()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string KOMENTAR = text_commentar.Text;
                string DITERIMA_LAIN_1 = text_diterimalain1.Text;
                DateTime TGL_DITERIMA_LAIN_1 = DateTime.Now;

                TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                TrForm3gdr.NO_FORM = NO_FORM;
                TrForm3gdr.DITERIMA_LAIN_1 = DITERIMA_LAIN_1;
                TrForm3gdr.TGL_DITERIMA_LAIN_1 = TGL_DITERIMA_LAIN_1;
                TrForm3gdr.STATUS = EApprovalStatus.Cancel;
                TrForm3gdr.REVISI = KOMENTAR;
                TrForm3gdr.USER_CURRENT = HfUsername.Value;
                TrForm3gdr.NEXT_USER = "-";
                TrForm3gdr.URUTAN_USER_CURRENT = 3;
                TrForm3gdr.URUTAN_NEXT_USER = 0;
                TrForm3Gdr.UpdateCancelDiterimaLain1(TrForm3gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm3gdractivity.USERNAME = HfUsername.Value;
                TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                TrForm3gdractivity.NO_FORM = NO_FORM;
                TrForm3gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm3gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm3gdractivity.REVISION = KOMENTAR;
                TrForm3gdractivity.URUTAN = 3;
                TrForm3gdractivity.SP = "";
                TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm3gdractivity.NEXT_USER = "-";
                TrForm3gdractivity.URUTAN_USER_CURRENT = 3;
                TrForm3gdractivity.URUTAN_NEXT_USER = 0;
                TrForm3GdrActivity.Insert(TrForm3gdractivity);

                DivMessage.InnerText = "Data Successful Cancel";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        public void UpdateStatusCancelDigitalImaging()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string KOMENTAR = text_commentar.Text;
                string DITERIMA_LAIN_3 = text_diterimalain3.Text;
                DateTime TGL_DITERIMA_LAIN_3 = DateTime.Now;

                TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                TrForm3gdr.NO_FORM = NO_FORM;
                TrForm3gdr.DITERIMA_LAIN_3 = DITERIMA_LAIN_3;
                TrForm3gdr.TGL_DITERIMA_LAIN_3 = TGL_DITERIMA_LAIN_3;
                TrForm3gdr.STATUS = EApprovalStatus.Cancel;
                TrForm3gdr.REVISI = KOMENTAR;
                TrForm3gdr.USER_CURRENT = HfUsername.Value;
                TrForm3gdr.NEXT_USER = "-";
                TrForm3gdr.URUTAN_USER_CURRENT = 5;
                TrForm3gdr.URUTAN_NEXT_USER = 0;
                TrForm3Gdr.UpdateCancelDiterimaLain3(TrForm3gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm3gdractivity.USERNAME = HfUsername.Value;
                TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                TrForm3gdractivity.NO_FORM = NO_FORM;
                TrForm3gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm3gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm3gdractivity.REVISION = KOMENTAR;
                TrForm3gdractivity.URUTAN = 5;
                TrForm3gdractivity.SP = "";
                TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm3gdractivity.NEXT_USER = "-";
                TrForm3gdractivity.URUTAN_USER_CURRENT = 5;
                TrForm3gdractivity.URUTAN_NEXT_USER = 0;
                TrForm3GdrActivity.Insert(TrForm3gdractivity);

                DivMessage.InnerText = "Data Successful Cancel";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        /// <summary>
        /// Update Status Revisi Plus Revisinya
        /// </summary>
        /// 

        public void UpdateStatusRevisiHD()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string MENYETUJUI1 = text_menyetujui1.Text;
                string REVISI = text_revisi.Text;
                DateTime TGL_MENYETUJUI1 = DateTime.Now;

                if (REVISI != "")
                {
                    TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                    TrForm3gdr.NO_FORM = NO_FORM;
                    TrForm3gdr.MENYETUJUI1 = MENYETUJUI1;
                    TrForm3gdr.TGL_MENYETUJUI1 = TGL_MENYETUJUI1;
                    TrForm3gdr.STATUS = EApprovalStatus.OnRevise;
                    TrForm3gdr.REVISI = REVISI;

                    string DIBUAT = text_dibuat.Text;
                    Int64 ID = 0;
                    string ID_DEPT_USER = "";
                    string USERNEXT = "";
                    string DEPT_USER_NEXT = "";
                    string Email = "";
                    string KD_JABATAN = "";

                    int URUTAN = 0;
                    string ACTION = "";
                    string PAGE_NAME = "";
                    string SP = "";

                    int URUTAN_NEXT = 0;
                    string ACTION_NEXT = "";
                    string PAGE_NAME_NEXT = "";
                    string SP_NEXT = "";


                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    string Where = string.Format("USERNAME = '{0}' AND KD_JABATAN = 'USR'", DIBUAT);
                    DsUser = MsUserDA.GetDataFilter(Where);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                        ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                        USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                        DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                        DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                        DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }
                    }

                    TrForm3gdr.USER_CURRENT = HfUsername.Value;
                    TrForm3gdr.NEXT_USER = USERNEXT;
                    TrForm3gdr.URUTAN_USER_CURRENT = URUTAN;
                    TrForm3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3Gdr.UpdateRevisiMenyetujui1(TrForm3gdr);

                    TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                    TrForm3gdractivity.USERNAME = HfUsername.Value;
                    TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                    TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                    TrForm3gdractivity.NO_FORM = NO_FORM;
                    TrForm3gdractivity.STATUS = EApprovalStatus.OnRevise;
                    TrForm3gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.OnRevise;
                    TrForm3gdractivity.REVISION = REVISI;
                    TrForm3gdractivity.URUTAN = 2;
                    TrForm3gdractivity.SP = "";
                    TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                    TrForm3gdractivity.NEXT_USER = USERNEXT;
                    TrForm3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    TrForm3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3GdrActivity.Insert(TrForm3gdractivity);

                    DivMessage.InnerText = "Data Successful Revise";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../MainMenu.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                }
                else
                {
                    DivMessage.InnerText = "Revise Cannot Be Empty";
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusRevisiHeadDesign()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_1 = text_diterima1.Text;
                string REVISI = text_revisi.Text;
                DateTime TGL_DITERIMA_1 = DateTime.Now;

                if (REVISI != "")
                {
                    TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                    TrForm3gdr.NO_FORM = NO_FORM;
                    TrForm3gdr.DITERIMA_1 = DITERIMA_1;
                    TrForm3gdr.TGL_DITERIMA_1 = TGL_DITERIMA_1;
                    TrForm3gdr.STATUS = EApprovalStatus.OnRevise;
                    TrForm3gdr.REVISI = REVISI;

                    string DIBUAT = text_dibuat.Text;
                    Int64 ID = 0;
                    string ID_DEPT_USER = "";
                    string USERNEXT = "";
                    string DEPT_USER_NEXT = "";
                    string Email = "";
                    string KD_JABATAN = "";

                    int URUTAN = 0;
                    string ACTION = "";
                    string PAGE_NAME = "";
                    string SP = "";

                    int URUTAN_NEXT = 0;
                    string ACTION_NEXT = "";
                    string PAGE_NAME_NEXT = "";
                    string SP_NEXT = "";


                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    string Where = string.Format("USERNAME = '{0}' AND KD_JABATAN = 'USR'", DIBUAT);
                    DsUser = MsUserDA.GetDataFilter(Where);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                        ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                        USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                        DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                        DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                        DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }
                    }
                    TrForm3gdr.USER_CURRENT = HfUsername.Value;
                    TrForm3gdr.NEXT_USER = USERNEXT;
                    TrForm3gdr.URUTAN_USER_CURRENT = URUTAN;
                    TrForm3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3Gdr.UpdateRevisiDiterima1(TrForm3gdr);

                    TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                    TrForm3gdractivity.USERNAME = HfUsername.Value;
                    TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                    TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                    TrForm3gdractivity.NO_FORM = NO_FORM;
                    TrForm3gdractivity.STATUS = EApprovalStatus.OnRevise;
                    TrForm3gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.OnRevise;
                    TrForm3gdractivity.REVISION = REVISI;
                    TrForm3gdractivity.URUTAN = 7;
                    TrForm3gdractivity.SP = "";
                    TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                    TrForm3gdractivity.NEXT_USER = USERNEXT;
                    TrForm3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    TrForm3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3GdrActivity.Insert(TrForm3gdractivity);


                    DivMessage.InnerText = "Data Successful Revise";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../MainMenu.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                }
                else
                {
                    DivMessage.InnerText = "Revise Cannot Be Empty";
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusRevisiGraphicDesign()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_2 = ddlditerima2.Text;
                string REVISI = text_revisi.Text;
                DateTime TGL_DITERIMA_2 = DateTime.Now;

                if (REVISI != "")
                {
                    TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                    TrForm3gdr.NO_FORM = NO_FORM;
                    TrForm3gdr.DITERIMA_2 = DITERIMA_2;
                    TrForm3gdr.TGL_DITERIMA_2 = TGL_DITERIMA_2;
                    TrForm3gdr.STATUS = EApprovalStatus.OnRevise;
                    TrForm3gdr.REVISI = REVISI;
                    string DIBUAT = text_dibuat.Text;
                    Int64 ID = 0;
                    string ID_DEPT_USER = "";
                    string USERNEXT = "";
                    string DEPT_USER_NEXT = "";
                    string Email = "";
                    string KD_JABATAN = "";

                    int URUTAN = 0;
                    string ACTION = "";
                    string PAGE_NAME = "";
                    string SP = "";

                    int URUTAN_NEXT = 0;
                    string ACTION_NEXT = "";
                    string PAGE_NAME_NEXT = "";
                    string SP_NEXT = "";


                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    string Where = string.Format("USERNAME = '{0}' AND KD_JABATAN = 'USR'", DIBUAT);
                    DsUser = MsUserDA.GetDataFilter(Where);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                        ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                        USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                        DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                        DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                        DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }
                    }
                    TrForm3gdr.USER_CURRENT = HfUsername.Value;
                    TrForm3gdr.NEXT_USER = USERNEXT;
                    TrForm3gdr.URUTAN_USER_CURRENT = URUTAN;
                    TrForm3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3Gdr.UpdateRevisiDiterima2(TrForm3gdr);

                    TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                    TrForm3gdractivity.USERNAME = HfUsername.Value;
                    TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                    TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                    TrForm3gdractivity.NO_FORM = NO_FORM;
                    TrForm3gdractivity.STATUS = EApprovalStatus.OnRevise;
                    TrForm3gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.OnRevise;
                    TrForm3gdractivity.REVISION = REVISI;
                    TrForm3gdractivity.URUTAN = 8;
                    TrForm3gdractivity.SP = "";
                    TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                    TrForm3gdractivity.NEXT_USER = USERNEXT;
                    TrForm3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    TrForm3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3GdrActivity.Insert(TrForm3gdractivity);


                    DivMessage.InnerText = "Data Successful Revise";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../MainMenu.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                }
                else
                {
                    DivMessage.InnerText = "Revise Cannot Be Empty";
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //public void UpdateStatusRevisiCreativeManager()
        //{
        //    try
        //    {
        //        TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
        //        DataSet Ds = new DataSet();

        //        string NO_FORM = text_noform.Text;
        //        string DITERIMA_3 = text_diterima3.Text;
        //        string REVISI = text_revisi.Text;
        //        DateTime TGL_DITERIMA_3 = DateTime.Now;

        //        TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
        //        TrForm3gdr.NO_FORM = NO_FORM;
        //        TrForm3gdr.DITERIMA_3 = DITERIMA_3;
        //        TrForm3gdr.TGL_DITERIMA_3 = TGL_DITERIMA_3;
        //        TrForm3gdr.STATUS = EApprovalStatus.OnRevise;
        //        TrForm3gdr.REVISI = REVISI;
        //        TrForm3Gdr.UpdateRevisiDiterima3(TrForm3gdr);

        //        DivMessage.InnerText = "Data Successful Revise";
        //        DivMessage.Style.Add("color", "black");
        //        DivMessage.Style.Add("background-color", "skyblue");
        //        DivMessage.Attributes["class"] = "error";
        //        //DivMessage.Attributes["class"] = "success";
        //        DivMessage.Visible = true;

        //        string HomePageUrl = "../MainMenu.aspx";
        //        Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
        //        //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
        //    }
        //    catch (Exception Ex)
        //    {
        //        DivMessage.InnerText = Ex.Message;
        //        DivMessage.Attributes["class"] = "error";
        //        //DivMessage.Attributes["class"] = "success";
        //        DivMessage.Visible = true;
        //    }
        //}

        public void UpdateStatusRevisiDesign()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_2 = ddlditerima2.Text;
                string DITERIMA_3 = text_diterima3.Text;
                string REVISIDESIGN = text_revisidesign.Text;
                DateTime TGL_DITERIMA_3 = DateTime.Now;
                string STATUS_VER = "R";

                if (REVISIDESIGN != "")
                {
                    TR_FORM3_GDR TrForm3gdr = new TR_FORM3_GDR();
                    TrForm3gdr.NO_FORM = NO_FORM;
                    TrForm3gdr.DITERIMA_3 = DITERIMA_3;
                    TrForm3gdr.TGL_DITERIMA_3 = TGL_DITERIMA_3;
                    TrForm3gdr.STATUS = EApprovalStatus.OnReviseDesign;
                    TrForm3gdr.REVISI = REVISIDESIGN;
                    LoadStatusVer();
                    TrForm3gdr.STATUS_VER = HfStatusVer.Value + STATUS_VER;

                    Int64 ID = 0;
                    string ID_DEPT_USER = "";
                    string USERNEXT = "";
                    string DEPT_USER_NEXT = "";
                    string Email = "";
                    string KD_JABATAN = "";

                    int URUTAN = 0;
                    string ACTION = "";
                    string PAGE_NAME = "";
                    string SP = "";

                    int URUTAN_NEXT = 0;
                    string ACTION_NEXT = "";
                    string PAGE_NAME_NEXT = "";
                    string SP_NEXT = "";


                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    string Where = string.Format("USERNAME = '{0}' And KD_JABATAN = 'GD'", DITERIMA_2);
                    DsUser = MsUserDA.GetDataFilter(Where);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                        ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                        USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                        DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                        DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                        DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }
                    }
                    TrForm3gdr.USER_CURRENT = HfUsername.Value;
                    TrForm3gdr.NEXT_USER = USERNEXT;
                    TrForm3gdr.URUTAN_USER_CURRENT = URUTAN;
                    TrForm3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3Gdr.UpdateRevisiDiterima3(TrForm3gdr);

                    TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY TrForm3gdractivity = new TR_FORM_GDR_ACTIVITY();
                    TrForm3gdractivity.USERNAME = HfUsername.Value;
                    TrForm3gdractivity.ACTIVITY_TIME = DateTime.Now;
                    TrForm3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                    TrForm3gdractivity.NO_FORM = NO_FORM;
                    TrForm3gdractivity.STATUS = EApprovalStatus.OnReviseDesign;
                    TrForm3gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.OnReviseDesign;
                    TrForm3gdractivity.REVISION = REVISIDESIGN;
                    TrForm3gdractivity.URUTAN = 5;
                    TrForm3gdractivity.SP = "";
                    TrForm3gdractivity.USER_CURRENT = HfUsername.Value;
                    TrForm3gdractivity.NEXT_USER = USERNEXT;
                    TrForm3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    TrForm3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3GdrActivity.Insert(TrForm3gdractivity);


                    DivMessage.InnerText = "Data Successful Revise Design";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../MainMenu.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                }
                else
                {
                    DivMessage.InnerText = "Revise Cannot Be Empty";
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //Revisi Fotographer
        public void UpdateStatusRevisiPhotographer()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_LAIN_1 = text_diterimalain1.Text;
                string REVISI = text_revisi.Text;
                DateTime TGL_DITERIMA_LAIN_1 = DateTime.Now;

                if (REVISI != "")
                {
                    TR_FORM3_GDR trform3gdr = new TR_FORM3_GDR();
                    trform3gdr.NO_FORM = NO_FORM;
                    trform3gdr.DITERIMA_LAIN_1 = DITERIMA_LAIN_1;
                    trform3gdr.TGL_DITERIMA_LAIN_1 = TGL_DITERIMA_LAIN_1;
                    trform3gdr.STATUS = EApprovalStatus.OnRevisePhoto;
                    trform3gdr.REVISI = REVISI;

                    string DIBUAT = text_dibuat.Text;
                    Int64 ID = 0;
                    string ID_DEPT_USER = "";
                    string USERNEXT = "";
                    string DEPT_USER_NEXT = "";
                    string Email = "";
                    string KD_JABATAN = "";

                    int URUTAN = 0;
                    string ACTION = "";
                    string PAGE_NAME = "";
                    string SP = "";

                    int URUTAN_NEXT = 0;
                    string ACTION_NEXT = "";
                    string PAGE_NAME_NEXT = "";
                    string SP_NEXT = "";


                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    string Where = string.Format("USERNAME = '{0}' AND KD_JABATAN = 'PHOTO'", DITERIMA_LAIN_1);
                    DsUser = MsUserDA.GetDataFilter(Where);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                        ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                        USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                        DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                        DsUser = MsUserDA.GetDataUrutanUserCustom(HfUsername.Value, KODE_FORM, HfActionGeneral.Value);
                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                        DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }
                    }
                    trform3gdr.USER_CURRENT = HfUsername.Value;
                    trform3gdr.NEXT_USER = USERNEXT;
                    trform3gdr.URUTAN_USER_CURRENT = URUTAN;
                    trform3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;

                    TrForm3Gdr.UpdateRevisiDiterimaLain1(trform3gdr);

                    TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY trform3gdractivity = new TR_FORM_GDR_ACTIVITY();
                    trform3gdractivity.USERNAME = HfUsername.Value;
                    trform3gdractivity.ACTIVITY_TIME = DateTime.Now;
                    trform3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                    trform3gdractivity.NO_FORM = NO_FORM;
                    trform3gdractivity.STATUS = EApprovalStatus.OnRevisePhoto;
                    trform3gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.OnRevisePhoto;
                    trform3gdractivity.REVISION = REVISI;
                    trform3gdractivity.URUTAN = 6;
                    trform3gdractivity.SP = "";
                    trform3gdractivity.USER_CURRENT = HfUsername.Value;
                    trform3gdractivity.NEXT_USER = USERNEXT;
                    trform3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    trform3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3GdrActivity.Insert(trform3gdractivity);



                    DivMessage.InnerText = "Data Successful Revise";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../MainMenu.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_VM.aspx");
                }
                else
                {
                    DivMessage.InnerText = "Revise Cannot Be Empty";
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //Revisi Digital Imaging
        public void UpdateStatusRevisiDigitalImaging()
        {
            try
            {
                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_LAIN_3 = text_diterimalain3.Text;
                string REVISI = text_revisi.Text;
                DateTime TGL_DITERIMA_LAIN_3 = DateTime.Now;

                if (REVISI != "")
                {
                    TR_FORM3_GDR trform3gdr = new TR_FORM3_GDR();
                    trform3gdr.NO_FORM = NO_FORM;
                    trform3gdr.DITERIMA_LAIN_3 = DITERIMA_LAIN_3;
                    trform3gdr.TGL_DITERIMA_LAIN_3 = TGL_DITERIMA_LAIN_3;
                    trform3gdr.STATUS = EApprovalStatus.OnRevisePhotoDI;
                    trform3gdr.REVISI = REVISI;

                    string DIBUAT = text_dibuat.Text;
                    Int64 ID = 0;
                    string ID_DEPT_USER = "";
                    string USERNEXT = "";
                    string DEPT_USER_NEXT = "";
                    string Email = "";
                    string KD_JABATAN = "";

                    int URUTAN = 0;
                    string ACTION = "";
                    string PAGE_NAME = "";
                    string SP = "";

                    int URUTAN_NEXT = 0;
                    string ACTION_NEXT = "";
                    string PAGE_NAME_NEXT = "";
                    string SP_NEXT = "";


                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    string Where = string.Format("USERNAME = '{0}' AND KD_JABATAN = 'DI'", DITERIMA_LAIN_3);
                    DsUser = MsUserDA.GetDataFilter(Where);

                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        ID = Convert.ToInt64(DsUser.Tables[0].Rows[0]["ID"].ToString());
                        ID_DEPT_USER = Convert.ToString(DsUser.Tables[0].Rows[0]["ID_DEPT"].ToString());
                        USERNEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
                        DEPT_USER_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["DEPT"].ToString());
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Login
                        DsUser = MsUserDA.GetDataUrutanUserCustom(HfUsername.Value, KODE_FORM, HfActionGeneral.Value);
                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }

                        //Mendapatkan Urutan User Berdasarkan Jabatan User Next
                        DsUser = MsUserDA.GetDataUrutanUser(USERNEXT, KODE_FORM);

                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            URUTAN_NEXT = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                            ACTION_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["ACTION"].ToString());
                            PAGE_NAME_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                            SP_NEXT = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                        }
                    }
                    trform3gdr.USER_CURRENT = HfUsername.Value;
                    trform3gdr.NEXT_USER = USERNEXT;
                    trform3gdr.URUTAN_USER_CURRENT = URUTAN;
                    trform3gdr.URUTAN_NEXT_USER = URUTAN_NEXT;

                    TrForm3Gdr.UpdateRevisiDiterimaLain3(trform3gdr);

                    TR_FORM_GDR_ACTIVITY_DA TrForm3GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY trform3gdractivity = new TR_FORM_GDR_ACTIVITY();
                    trform3gdractivity.USERNAME = HfUsername.Value;
                    trform3gdractivity.ACTIVITY_TIME = DateTime.Now;
                    trform3gdractivity.KODE_FORM = Common.KD_FORM_OTHERS;
                    trform3gdractivity.NO_FORM = NO_FORM;
                    trform3gdractivity.STATUS = EApprovalStatus.OnRevisePhotoDI;
                    trform3gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.OnRevisePhotoDI;
                    trform3gdractivity.REVISION = REVISI;
                    trform3gdractivity.URUTAN = 8;
                    trform3gdractivity.SP = "";
                    trform3gdractivity.USER_CURRENT = HfUsername.Value;
                    trform3gdractivity.NEXT_USER = USERNEXT;
                    trform3gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    trform3gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm3GdrActivity.Insert(trform3gdractivity);



                    DivMessage.InnerText = "Data Successful Revise";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../MainMenu.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_VM.aspx");
                }
                else
                {
                    DivMessage.InnerText = "Revise Cannot Be Empty";
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
                }

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        /// <summary>
        /// Show Button Sesuai Urutan
        /// </summary>

        public void ShowButtonUrutan2()
        {
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            btn_UpdateSubmitDesign.Visible = false;

            btn_ToRevise.Visible = true;
            //btn_Revise.Visible = true;
            btn_Approved.Visible = true;
            btn_Reject.Visible = true;
        }

        public void ShowButtonUrutan3()
        {
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            btn_UpdateSubmitDesign.Visible = false;

            btn_Approved.Visible = true;
            btn_Approved.Text = "Posting Photo";
            Pnl_UploadFilePG.Visible = true;
            Pnl_UploadFilePG.Enabled = true;
            Pnl_Commentar.Visible = true;
            btn_ToRevise.Visible = true;
            btn_Reject.Visible = true;
        }

        public void ShowButtonUrutan4()
        {
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            btn_UpdateSubmitDesign.Visible = false;
            btn_Reject.Visible = false;

            btn_Approved.Visible = true;
            btn_Approved.Text = "Approved-Photo";
            btn_ToRevise.Visible = true;
            Pnl_UploadFilePG.Visible = true;
            Pnl_UploadFilePG.Enabled = true;
        }

        public void ShowButtonUrutan5()
        {
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            btn_UpdateSubmitDesign.Visible = false;

            btn_Approved.Visible = true;
            btn_Approved.Text = "Posting-Retouch-Foto";
            Pnl_UploadFileDI.Visible = true;
            Pnl_UploadFileDI.Enabled = true;
            Pnl_Commentar.Visible = true;
            btn_ToRevise.Visible = true;
            btn_Reject.Visible = true;
        }

        public void ShowButtonUrutan6()
        {
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            btn_UpdateSubmitDesign.Visible = false;
            btn_Reject.Visible = false;

            btn_Approved.Visible = true;
            btn_Approved.Text = "Approved-Photo-DI";
            btn_ToRevise.Visible = true;
            Pnl_UploadFileDI.Visible = true;
            Pnl_UploadFileDI.Enabled = true;
        }

        public void ShowButtonUrutan7()
        {
            btn_Save.Visible = false;
            btn_Approved.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            btn_UpdateSubmitDesign.Visible = false;

            Pnl_JadwalPekerjaan.Enabled = true;
            text_dibuat.Enabled = false;
            text_menyetujui1.Enabled = false;
            text_diterima1.Enabled = false;

            text_jadwalselesaidisain.Enabled = true;
            text_jadwalproduksi.Enabled = true;
            text_jadwalkirim.Enabled = true;
            text_jadwalfoto.Enabled = true;
            text_jadwaldi.Enabled = true;

            Pnl_FormOthers1.Enabled = true;
            Pnl_FormOthers2.Enabled = true;
            text_alokasibudget.Enabled = false;
            radio_yesjadwalpergantianimage.Enabled = false;
            radio_nojadwalpergantianimage.Enabled = false;
            text_jadwalpergantianimage.Enabled = false;

            Pnl_Production.Enabled = false;
            Pnl_Commentar.Visible = true;
            btn_Accepted.Visible = true;
            btn_ToRevise.Visible = true;
            //btn_Revise.Visible = true;
            if (label_statusvalue.Text == "Done" || label_statusvalue.Text == "In-Production")
            {
                btn_ReviseContent.Visible = true;
                btn_ReviseContent.Enabled = true;
            }
            else
            {
                btn_ReviseContent.Visible = false;
            }
            btn_Reject.Visible = true;
        }

        public void ShowButtonUrutan8()
        {
            btn_Save.Visible = false;
            btn_Approved.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            btn_UpdateSubmitDesign.Visible = false;

            btn_Accepted.Visible = true;
            btn_Accepted.Text = "Submit";
            //btn_ToRevise.Visible = true;
            ////btn_Revise.Visible = true;
            //btn_Reject.Visible = true;
            Pnl_UploadFileGD.Visible = true;
            Pnl_UploadFileGD.Enabled = true;
        }

        public void ShowButtonUrutan9()
        {
            btn_Save.Visible = false;
            btn_Approved.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            btn_UpdateSubmitDesign.Visible = false;

            btn_Accepted.Visible = true;
            btn_ToReviseDesign.Visible = true;
            //btn_Revise.Visible = true;
            btn_Reject.Visible = true;
            Pnl_UploadFileGD.Visible = true;
            Pnl_StatusVer.Visible = true;
        }

        public void ShowButtonUrutan10()
        {
            btn_Save.Visible = false;
            btn_Approved.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            btn_UpdateSubmitDesign.Visible = false;
            btn_Reject.Visible = false;
            btn_ToRevise.Visible = false;

            btn_Accepted.Visible = true;
            //btn_Accepted.Text = "Update-Status-Production-Distribusi";
            Pnl_UploadFileGD.Visible = true;
            Pnl_StatusVer.Visible = true;
        }

        /// <summary>
        /// Add Data Detail TR_FORM3_GDR_KATEGORI
        /// </summary>
        /// 

        public void LoadDataDetailKategoriPermintaan()
        {
            try
            {
                TR_FORM3_GDR_KATEGORI_DA TrForm3GdrKategori = new DataLayer.TR_FORM3_GDR_KATEGORI_DA();
                DataSet Ds = new DataSet();

                string KODE_KATEGORI = "";
                string DETAIL = "";
                string ISPILIH = "";
                string Where = string.Format("NO_FORM = '{0}'", HfNO_FORM.Value);
                Ds = TrForm3GdrKategori.GetDataFilter(Where);

                int i = 0;
                int index = 0;
                foreach (DataRow Item in Ds.Tables[0].Rows)
                {
                    index = i;
                    i++;

                    if (!String.IsNullOrEmpty((Item.Field<String>("KODE_KATEGORI"))))
                    {
                        KODE_KATEGORI = Item.Field<String>("KODE_KATEGORI");
                    }
                    else
                    {
                        KODE_KATEGORI = "";
                    }


                    if (!String.IsNullOrEmpty((Item.Field<String>("DETAIL"))))
                    {
                        DETAIL = Item.Field<String>("DETAIL");
                    }
                    else
                    {
                        DETAIL = "";
                    }

                    //Load Group Kategori
                    if (DETAIL == "Poster") checkbox_poster.Checked = true;
                    if (DETAIL == "LighboxPoster") checkbox_lighboxposter.Checked = true;
                    if (DETAIL == "Hording") checkbox_hording.Checked = true;
                    if (KODE_KATEGORI == "KP-OTHERS-01")
                    {
                        text_others1Others.Text = DETAIL;
                        checkbox_others1Others.Checked = true;
                    }
                    if (KODE_KATEGORI == "KP-OTHERS-02")
                    {
                        text_others2Others.Text = DETAIL;
                        checkbox_others2Others.Checked = true;
                    }
                    if (KODE_KATEGORI == "KP-OTHERS-03")
                    {
                        text_others3Others.Text = DETAIL;
                        checkbox_others3Others.Checked = true;
                    }

                    if (DETAIL == "SocialMedia") checkbox_socialmedia.Checked = true;
                    if (DETAIL == "MediaCetak") checkbox_mediacetak.Checked = true;
                    if (DETAIL == "DigitalAdvertising") checkbox_digitaladvertising.Checked = true;
                    if (DETAIL == "Other") checkbox_other.Checked = true;

                }

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SaveDetailKategoriPermintaan()
        {
            try
            {
                GroupSaveKategoriPermintaan();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateDetailKategoriPermintaan()
        {
            try
            {
                GroupUpdateKategoriPermintaan();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        public void GroupSaveKategoriPermintaan()
        {
            try
            {
                TR_FORM3_GDR_KATEGORI_DA TrForm3GdrKategori = new DataLayer.TR_FORM3_GDR_KATEGORI_DA();
                DataSet Ds = new DataSet();

                TR_FORM3_GDR_KATEGORI trform3gdrkategori = new TR_FORM3_GDR_KATEGORI();

                //Group Save Kategori
                string noform = text_noform.Text;
                string kodekategori = "KP";
                string kodekategoriothers1 = "KP-OTHERS-01";
                string kodekategoriothers2 = "KP-OTHERS-02";
                string kodekategoriothers3 = "KP-OTHERS-03";
                string internaldepartment = "InternalDepartment";
                string poster = "Poster";
                string lighboxposter = "LighboxPoster";
                string hording = "Hording";
                string other1other = text_others1Others.Text;
                string other2other = text_others2Others.Text;
                string other3other = text_others3Others.Text;
                string socialmedia = "SocialMedia";
                string mediacetak = "MediaCetak";
                string digitaladvertising = "DigitalAdvertising";
                string other = "Other";

                if (checkbox_poster.Checked == true)
                {
                    trform3gdrkategori.NO_FORM = noform;
                    trform3gdrkategori.KODE_KATEGORI = kodekategori;
                    trform3gdrkategori.DETAIL = poster;
                    trform3gdrkategori.ISPILIH = "Yes";
                    TrForm3GdrKategori.Insert(trform3gdrkategori);

                }

                if (checkbox_lighboxposter.Checked == true)
                {
                    trform3gdrkategori.NO_FORM = noform;
                    trform3gdrkategori.KODE_KATEGORI = kodekategori;
                    trform3gdrkategori.DETAIL = lighboxposter;
                    trform3gdrkategori.ISPILIH = "Yes";
                    TrForm3GdrKategori.Insert(trform3gdrkategori);

                }

                if (checkbox_hording.Checked == true)
                {
                    trform3gdrkategori.NO_FORM = noform;
                    trform3gdrkategori.KODE_KATEGORI = kodekategori;
                    trform3gdrkategori.DETAIL = hording;
                    trform3gdrkategori.ISPILIH = "Yes";
                    TrForm3GdrKategori.Insert(trform3gdrkategori);

                }

                if (checkbox_others1Others.Checked == true)
                {
                    trform3gdrkategori.NO_FORM = noform;
                    trform3gdrkategori.KODE_KATEGORI = kodekategoriothers1;
                    trform3gdrkategori.DETAIL = other1other;
                    trform3gdrkategori.ISPILIH = "Yes";
                    TrForm3GdrKategori.Insert(trform3gdrkategori);

                }

                if (checkbox_others2Others.Checked == true)
                {
                    trform3gdrkategori.NO_FORM = noform;
                    trform3gdrkategori.KODE_KATEGORI = kodekategoriothers2;
                    trform3gdrkategori.DETAIL = other2other;
                    trform3gdrkategori.ISPILIH = "Yes";
                    TrForm3GdrKategori.Insert(trform3gdrkategori);

                }

                if (checkbox_others3Others.Checked == true)
                {
                    trform3gdrkategori.NO_FORM = noform;
                    trform3gdrkategori.KODE_KATEGORI = kodekategoriothers3;
                    trform3gdrkategori.DETAIL = other3other;
                    trform3gdrkategori.ISPILIH = "Yes";
                    TrForm3GdrKategori.Insert(trform3gdrkategori);

                }


                if (checkbox_socialmedia.Checked == true)
                {
                    trform3gdrkategori.NO_FORM = noform;
                    trform3gdrkategori.KODE_KATEGORI = kodekategori;
                    trform3gdrkategori.DETAIL = socialmedia;
                    trform3gdrkategori.ISPILIH = "Yes";
                    TrForm3GdrKategori.Insert(trform3gdrkategori);

                }

                if (checkbox_mediacetak.Checked == true)
                {
                    trform3gdrkategori.NO_FORM = noform;
                    trform3gdrkategori.KODE_KATEGORI = kodekategori;
                    trform3gdrkategori.DETAIL = mediacetak;
                    trform3gdrkategori.ISPILIH = "Yes";
                    TrForm3GdrKategori.Insert(trform3gdrkategori);

                }

                if (checkbox_digitaladvertising.Checked == true)
                {
                    trform3gdrkategori.NO_FORM = noform;
                    trform3gdrkategori.KODE_KATEGORI = kodekategori;
                    trform3gdrkategori.DETAIL = digitaladvertising;
                    trform3gdrkategori.ISPILIH = "Yes";
                    TrForm3GdrKategori.Insert(trform3gdrkategori);

                }

                if (checkbox_other.Checked == true)
                {
                    trform3gdrkategori.NO_FORM = noform;
                    trform3gdrkategori.KODE_KATEGORI = kodekategori;
                    trform3gdrkategori.DETAIL = other;
                    trform3gdrkategori.ISPILIH = "Yes";
                    TrForm3GdrKategori.Insert(trform3gdrkategori);

                }



            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        public void GroupUpdateKategoriPermintaan()
        {
            try
            {

                TR_FORM3_GDR_KATEGORI_DA TrForm3GdrKategori = new DataLayer.TR_FORM3_GDR_KATEGORI_DA();
                DataSet Ds = new DataSet();

                TR_FORM3_GDR_KATEGORI trform3gdrkategori = new TR_FORM3_GDR_KATEGORI();


                string noform = text_noform.Text;
                string kodekategori = "KP";

                string Where = string.Format("NO_FORM = '{0}' AND KODE_KATEGORI = '{1}'", noform, kodekategori);
                TrForm3GdrKategori.DeleteFilter(Where);

                GroupSaveKategoriPermintaan();

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        /// <summary>
        /// Add Data Cust Di TR_FORM1_GDR_CUST
        /// </summary>

        public void LoadDataGridCustCt()
        {
            try
            {
                //dtToko.Columns.AddRange(new DataColumn[8] {
                //    new DataColumn("ID", typeof(string)),
                //    new DataColumn("KODE_FORM", typeof(string)),
                //    new DataColumn("NO_FORM", typeof(string)),
                //    new DataColumn("kode_cust", typeof(string)),
                //    new DataColumn("kode_ct", typeof(string)),
                //    new DataColumn("site", typeof(string)),
                //    new DataColumn("nama_cust", typeof(string)),
                //    new DataColumn("nama_ct", typeof(string)),
                //});
                //ViewState["Toko"] = dtToko;
                //this.BindGridCustCt();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        protected void BindGridCustCt()
        {
            //gvCustCt.DataSource = (DataTable)ViewState["Toko"];
            gvCustCt.DataBind();
        }

        public void LoadDataDetailCustCt()
        {
            try
            {

                DataLayer.MS_CUST_CT_DA MsCustCt = new DataLayer.MS_CUST_CT_DA();
                DataSet DsCustAzure = new DataSet();

                TR_FORM_GDR_CUST_DA TrFormGdrCust = new DataLayer.TR_FORM_GDR_CUST_DA();
                TR_FORM_GDR_CUST_TEMP_DA TrFormGdrCustTemp = new DataLayer.TR_FORM_GDR_CUST_TEMP_DA();
                DataSet Ds = new DataSet();

                string KODE_FORM = Common.KD_FORM_OTHERS;

                string NO_FORM = text_noform.Text;
                string KODE_CUST = "";
                string KODE_CUSTALL = "";
                string KODE_CTALL = "";
                string KODE_CT = "";
                string SITE = "";
                string NAMA_CUST = "";
                string NAMA_CT = "";


                string Where = string.Format("KODE_FORM = 'FRM-0003' AND NO_FORM = '{0}'", NO_FORM);

                Ds = TrFormGdrCust.GetDataFilter(Where);

                int i = 0;
                int index = 0;
                foreach (DataRow Item in Ds.Tables[0].Rows)
                {
                    index = i;
                    i++;


                    if (!String.IsNullOrEmpty((Item.Field<String>("kode_cust"))))
                    {
                        KODE_CUST = Item.Field<String>("kode_cust");
                    }
                    else
                    {
                        KODE_CUST = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("kode_ct"))))
                    {
                        KODE_CT = Item.Field<String>("kode_ct");
                    }
                    else
                    {
                        KODE_CT = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("site"))))
                    {
                        SITE = Item.Field<String>("site");
                    }
                    else
                    {
                        SITE = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("nama_cust"))))
                    {
                        NAMA_CUST = Item.Field<String>("nama_cust");
                    }
                    else
                    {
                        NAMA_CUST = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("nama_ct"))))
                    {
                        NAMA_CT = Item.Field<String>("nama_ct");
                    }
                    else
                    {
                        NAMA_CT = "";
                    }


                    TR_FORM_GDR_CUST_TEMP trformgdrcusttemp = new TR_FORM_GDR_CUST_TEMP();
                    trformgdrcusttemp.KODE_FORM = KODE_FORM;
                    trformgdrcusttemp.NO_FORM = NO_FORM;
                    trformgdrcusttemp.kode_cust = KODE_CUST;
                    trformgdrcusttemp.kode_ct = KODE_CT;
                    trformgdrcusttemp.site = SITE;
                    trformgdrcusttemp.nama_cust = NAMA_CUST;
                    trformgdrcusttemp.nama_ct = NAMA_CT;
                    TrFormGdrCustTemp.Insert(trformgdrcusttemp);
                    gvCustCt.DataBind();
                    //int colsCount = dtToko.Columns.Count;
                    //dtToko.Rows.Add(index, KODE_FORM, NO_FORM, KODE_CUST, KODE_CT, SITE, NAMA_CUST, NAMA_CT);
                    //gvCustCt.DataBind();

                    //foreach (GridViewRow Row in gvCustCt.Rows)
                    //{
                    //    CheckBox chkRow = Row.FindControl("chkRow") as CheckBox;
                    //    chkRow.Checked = true;
                    //}

                    //KODE_CUSTALL += Item.Field<String>("kode_cust") + ",";
                    //KODE_CTALL += Item.Field<String>("kode_ct") + ",";

                    //string jenis = ddljenis.SelectedValue.ToString();
                    //string kode_cust = "";
                    //string kode_ct = "";
                    //string site = "";
                    //string nama_cust = "";
                    //string nama_ct = "";

                    //if (jenis == "STORE")
                    //{
                    //    string WhereSTORE = string.Format("KODE_CUST LIKE '%SHR%'");
                    //    DsCustAzure = MsCustCt.GetDataFilter(WhereSTORE);
                    //    int icust = 0;
                    //    int indexcust = 0;
                    //    foreach (DataRow ItemCust in DsCustAzure.Tables[0].Rows)
                    //    {
                    //        indexcust = i;
                    //        icust++;

                    //        if (!String.IsNullOrEmpty((ItemCust.Field<String>("kode_cust"))))
                    //        {
                    //            kode_cust = ItemCust.Field<String>("kode_cust");
                    //        }
                    //        else
                    //        {
                    //            kode_cust = "";
                    //        }

                    //        if (!String.IsNullOrEmpty((ItemCust.Field<String>("kode_ct"))))
                    //        {
                    //            kode_ct = ItemCust.Field<String>("kode_ct");
                    //        }
                    //        else
                    //        {
                    //            kode_ct = "";
                    //        }

                    //        if (!String.IsNullOrEmpty((ItemCust.Field<String>("site"))))
                    //        {
                    //            site = ItemCust.Field<String>("site");
                    //        }
                    //        else
                    //        {
                    //            site = "";
                    //        }

                    //        if (!String.IsNullOrEmpty((ItemCust.Field<String>("nama_cust"))))
                    //        {
                    //            nama_cust = ItemCust.Field<String>("nama_cust");
                    //        }
                    //        else
                    //        {
                    //            nama_cust = "";
                    //        }

                    //        if (!String.IsNullOrEmpty((ItemCust.Field<String>("nama_ct"))))
                    //        {
                    //            nama_ct = ItemCust.Field<String>("nama_ct");
                    //        }
                    //        else
                    //        {
                    //            nama_ct = "";
                    //        }

                    //        int colsCustCount = dtToko.Columns.Count;
                    //        dtToko.Rows.Add(index, KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct);
                    //        gvCustCt.DataBind();

                    //    }
                    //}
                    //else if (jenis == "COUNTER")
                    //{
                    //    string WhereCounter = string.Format("KODE_CUST NOT LIKE '%SHR%'");
                    //    DsCustAzure = MsCustCt.GetDataFilter(WhereCounter);
                    //    int icust = 0;
                    //    int indexcust = 0;
                    //    foreach (DataRow ItemCust in DsCustAzure.Tables[0].Rows)
                    //    {
                    //        indexcust = i;
                    //        icust++;

                    //        if (!String.IsNullOrEmpty((ItemCust.Field<String>("kode_cust"))))
                    //        {
                    //            kode_cust = ItemCust.Field<String>("kode_cust");
                    //        }
                    //        else
                    //        {
                    //            kode_cust = "";
                    //        }

                    //        if (!String.IsNullOrEmpty((ItemCust.Field<String>("kode_ct"))))
                    //        {
                    //            kode_ct = ItemCust.Field<String>("kode_ct");
                    //        }
                    //        else
                    //        {
                    //            kode_ct = "";
                    //        }

                    //        if (!String.IsNullOrEmpty((ItemCust.Field<String>("site"))))
                    //        {
                    //            site = ItemCust.Field<String>("site");
                    //        }
                    //        else
                    //        {
                    //            site = "";
                    //        }

                    //        if (!String.IsNullOrEmpty((ItemCust.Field<String>("nama_cust"))))
                    //        {
                    //            nama_cust = ItemCust.Field<String>("nama_cust");
                    //        }
                    //        else
                    //        {
                    //            nama_cust = "";
                    //        }

                    //        if (!String.IsNullOrEmpty((ItemCust.Field<String>("nama_ct"))))
                    //        {
                    //            nama_ct = ItemCust.Field<String>("nama_ct");
                    //        }
                    //        else
                    //        {
                    //            nama_ct = "";
                    //        }

                    //        int colsCustCount = dtToko.Columns.Count;
                    //        dtToko.Rows.Add(index, KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct);
                    //        gvCustCt.DataBind();

                    //    }
                    //}
                    //else
                    //{

                    //}

                }

                gvCustCt.Selection.SelectAll();
                gvCustCt.DataBind();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void LoadDataFormMSCustToGridCustCt()
        {
            try
            {

                string KODE_FORM = Common.KD_FORM_OTHERS;
                string NO_FORM = text_noform.Text;

                LoadDataGridCustCt();
                TR_FORM_GDR_CUST_TEMP_DA TrFormGdrCustTemp = new DataLayer.TR_FORM_GDR_CUST_TEMP_DA();
                TrFormGdrCustTemp.DeleteFilter(string.Format("KODE_FORM = '{0}' AND NO_FORM = '{1}'", KODE_FORM, NO_FORM));

                DataSet Ds = new DataSet();
                DataLayer.MS_CUST_CT_DA MsCustCt = new DataLayer.MS_CUST_CT_DA();

                string jenis = ddljenis.SelectedValue.ToString();
                string kode_cust = "";
                string kode_ct = "";
                string site = "";
                string nama_cust = "";
                string nama_ct = "";

                if (jenis == "OPENING_STORE")
                {
                    string WhereSTORE = string.Format("(site LIKE '2%' OR site LIKE '3%' OR site LIKE '9%' AND (site NOT LIKE '%998%' AND site NOT LIKE '%999%'))");
                    Ds = MsCustCt.GetDataFilter(WhereSTORE);
                    int i = 0;
                    int index = 0;
                    foreach (DataRow Item in Ds.Tables[0].Rows)
                    {
                        index = i;
                        i++;

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_cust"))))
                        {
                            kode_cust = Item.Field<String>("kode_cust");
                        }
                        else
                        {
                            kode_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_ct"))))
                        {
                            kode_ct = Item.Field<String>("kode_ct");
                        }
                        else
                        {
                            kode_ct = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("site"))))
                        {
                            site = Item.Field<String>("site");
                        }
                        else
                        {
                            site = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_cust"))))
                        {
                            nama_cust = Item.Field<String>("nama_cust");
                        }
                        else
                        {
                            nama_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_ct"))))
                        {
                            nama_ct = Item.Field<String>("nama_ct");
                        }
                        else
                        {
                            nama_ct = "";
                        }


                        //int colsCount = dtToko.Columns.Count;
                        //dtToko.Rows.Add(index, KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct);
                        //gvCustCt.DataBind();

                        int ID = index;
                        string KODE_CUST = "";
                        string KODE_CT = "";
                        string SITE = "";
                        string NAMA_CUST = "";
                        string NAMA_CT = "";

                        KODE_FORM = Common.KD_FORM_OTHERS;
                        NO_FORM = text_noform.Text;
                        KODE_CUST = kode_cust;
                        KODE_CT = kode_ct;
                        SITE = site;
                        NAMA_CUST = nama_cust;
                        NAMA_CT = nama_ct;

                        TR_FORM_GDR_CUST_TEMP trformgdrcusttemp = new TR_FORM_GDR_CUST_TEMP();
                        trformgdrcusttemp.KODE_FORM = KODE_FORM;
                        trformgdrcusttemp.NO_FORM = NO_FORM;
                        trformgdrcusttemp.kode_cust = KODE_CUST;
                        trformgdrcusttemp.kode_ct = KODE_CT;
                        trformgdrcusttemp.site = SITE;
                        trformgdrcusttemp.nama_cust = NAMA_CUST;
                        trformgdrcusttemp.nama_ct = NAMA_CT;
                        TrFormGdrCustTemp.Insert(trformgdrcusttemp);

                        gvCustCt.DataBind();

                    }
                }
                else if (jenis == "RENOVASI_STORE")
                {
                    string WhereSTORE = string.Format("(site LIKE '2%' OR site LIKE '3%')");
                    Ds = MsCustCt.GetDataFilter(WhereSTORE);
                    int i = 0;
                    int index = 0;
                    foreach (DataRow Item in Ds.Tables[0].Rows)
                    {
                        index = i;
                        i++;

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_cust"))))
                        {
                            kode_cust = Item.Field<String>("kode_cust");
                        }
                        else
                        {
                            kode_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_ct"))))
                        {
                            kode_ct = Item.Field<String>("kode_ct");
                        }
                        else
                        {
                            kode_ct = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("site"))))
                        {
                            site = Item.Field<String>("site");
                        }
                        else
                        {
                            site = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_cust"))))
                        {
                            nama_cust = Item.Field<String>("nama_cust");
                        }
                        else
                        {
                            nama_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_ct"))))
                        {
                            nama_ct = Item.Field<String>("nama_ct");
                        }
                        else
                        {
                            nama_ct = "";
                        }


                        //int colsCount = dtToko.Columns.Count;
                        //dtToko.Rows.Add(index, KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct);
                        //gvCustCt.DataBind();

                        int ID = index;
                        string KODE_CUST = "";
                        string KODE_CT = "";
                        string SITE = "";
                        string NAMA_CUST = "";
                        string NAMA_CT = "";

                        KODE_FORM = Common.KD_FORM_OTHERS;
                        NO_FORM = text_noform.Text;
                        KODE_CUST = kode_cust;
                        KODE_CT = kode_ct;
                        SITE = site;
                        NAMA_CUST = nama_cust;
                        NAMA_CT = nama_ct;

                        TR_FORM_GDR_CUST_TEMP trformgdrcusttemp = new TR_FORM_GDR_CUST_TEMP();
                        trformgdrcusttemp.KODE_FORM = KODE_FORM;
                        trformgdrcusttemp.NO_FORM = NO_FORM;
                        trformgdrcusttemp.kode_cust = KODE_CUST;
                        trformgdrcusttemp.kode_ct = KODE_CT;
                        trformgdrcusttemp.site = SITE;
                        trformgdrcusttemp.nama_cust = NAMA_CUST;
                        trformgdrcusttemp.nama_ct = NAMA_CT;
                        TrFormGdrCustTemp.Insert(trformgdrcusttemp);

                        gvCustCt.DataBind();

                    }
                }
                else if (jenis == "EXISTING_STORE")
                {
                    string WhereSTORE = string.Format("(site LIKE '2%' OR site LIKE '3%')");
                    Ds = MsCustCt.GetDataFilter(WhereSTORE);
                    int i = 0;
                    int index = 0;
                    foreach (DataRow Item in Ds.Tables[0].Rows)
                    {
                        index = i;
                        i++;

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_cust"))))
                        {
                            kode_cust = Item.Field<String>("kode_cust");
                        }
                        else
                        {
                            kode_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_ct"))))
                        {
                            kode_ct = Item.Field<String>("kode_ct");
                        }
                        else
                        {
                            kode_ct = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("site"))))
                        {
                            site = Item.Field<String>("site");
                        }
                        else
                        {
                            site = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_cust"))))
                        {
                            nama_cust = Item.Field<String>("nama_cust");
                        }
                        else
                        {
                            nama_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_ct"))))
                        {
                            nama_ct = Item.Field<String>("nama_ct");
                        }
                        else
                        {
                            nama_ct = "";
                        }


                        //int colsCount = dtToko.Columns.Count;
                        //dtToko.Rows.Add(index, KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct);
                        //gvCustCt.DataBind();

                        int ID = index;
                        string KODE_CUST = "";
                        string KODE_CT = "";
                        string SITE = "";
                        string NAMA_CUST = "";
                        string NAMA_CT = "";

                        KODE_FORM = Common.KD_FORM_OTHERS;
                        NO_FORM = text_noform.Text;
                        KODE_CUST = kode_cust;
                        KODE_CT = kode_ct;
                        SITE = site;
                        NAMA_CUST = nama_cust;
                        NAMA_CT = nama_ct;

                        TR_FORM_GDR_CUST_TEMP trformgdrcusttemp = new TR_FORM_GDR_CUST_TEMP();
                        trformgdrcusttemp.KODE_FORM = KODE_FORM;
                        trformgdrcusttemp.NO_FORM = NO_FORM;
                        trformgdrcusttemp.kode_cust = KODE_CUST;
                        trformgdrcusttemp.kode_ct = KODE_CT;
                        trformgdrcusttemp.site = SITE;
                        trformgdrcusttemp.nama_cust = NAMA_CUST;
                        trformgdrcusttemp.nama_ct = NAMA_CT;
                        TrFormGdrCustTemp.Insert(trformgdrcusttemp);

                        gvCustCt.DataBind();

                    }
                }
                else if (jenis == "BAZZAR")
                {
                    string WhereCounter = string.Format("(site LIKE '8%')");
                    Ds = MsCustCt.GetDataFilter(WhereCounter);

                    int i = 0;
                    int index = 0;
                    foreach (DataRow Item in Ds.Tables[0].Rows)
                    {
                        index = i;
                        i++;

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_cust"))))
                        {
                            kode_cust = Item.Field<String>("kode_cust");
                        }
                        else
                        {
                            kode_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_ct"))))
                        {
                            kode_ct = Item.Field<String>("kode_ct");
                        }
                        else
                        {
                            kode_ct = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("site"))))
                        {
                            site = Item.Field<String>("site");
                        }
                        else
                        {
                            site = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_cust"))))
                        {
                            nama_cust = Item.Field<String>("nama_cust");
                        }
                        else
                        {
                            nama_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_ct"))))
                        {
                            nama_ct = Item.Field<String>("nama_ct");
                        }
                        else
                        {
                            nama_ct = "";
                        }

                        //int colsCount = dtToko.Columns.Count;
                        //dtToko.Rows.Add(index, KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct);
                        //gvCustCt.DataBind();
                        int ID = index;
                        string KODE_CUST = "";
                        string KODE_CT = "";
                        string SITE = "";
                        string NAMA_CUST = "";
                        string NAMA_CT = "";

                        KODE_FORM = Common.KD_FORM_OTHERS;
                        NO_FORM = text_noform.Text;
                        KODE_CUST = kode_cust;
                        KODE_CT = kode_ct;
                        SITE = site;
                        NAMA_CUST = nama_cust;
                        NAMA_CT = nama_ct;

                        TR_FORM_GDR_CUST_TEMP trformgdrcusttemp = new TR_FORM_GDR_CUST_TEMP();
                        trformgdrcusttemp.KODE_FORM = KODE_FORM;
                        trformgdrcusttemp.NO_FORM = NO_FORM;
                        trformgdrcusttemp.kode_cust = KODE_CUST;
                        trformgdrcusttemp.kode_ct = KODE_CT;
                        trformgdrcusttemp.site = SITE;
                        trformgdrcusttemp.nama_cust = NAMA_CUST;
                        trformgdrcusttemp.nama_ct = NAMA_CT;
                        TrFormGdrCustTemp.Insert(trformgdrcusttemp);

                        gvCustCt.DataBind();


                    }
                }
                else if (jenis == "OPENING_COUNTER")
                {
                    string WhereCounter = string.Format("(site LIKE '5%' OR site LIKE '6%')");
                    Ds = MsCustCt.GetDataFilter(WhereCounter);

                    int i = 0;
                    int index = 0;
                    foreach (DataRow Item in Ds.Tables[0].Rows)
                    {
                        index = i;
                        i++;

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_cust"))))
                        {
                            kode_cust = Item.Field<String>("kode_cust");
                        }
                        else
                        {
                            kode_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_ct"))))
                        {
                            kode_ct = Item.Field<String>("kode_ct");
                        }
                        else
                        {
                            kode_ct = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("site"))))
                        {
                            site = Item.Field<String>("site");
                        }
                        else
                        {
                            site = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_cust"))))
                        {
                            nama_cust = Item.Field<String>("nama_cust");
                        }
                        else
                        {
                            nama_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_ct"))))
                        {
                            nama_ct = Item.Field<String>("nama_ct");
                        }
                        else
                        {
                            nama_ct = "";
                        }

                        //int colsCount = dtToko.Columns.Count;
                        //dtToko.Rows.Add(index, KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct);
                        //gvCustCt.DataBind();
                        int ID = index;
                        string KODE_CUST = "";
                        string KODE_CT = "";
                        string SITE = "";
                        string NAMA_CUST = "";
                        string NAMA_CT = "";

                        KODE_FORM = Common.KD_FORM_OTHERS;
                        NO_FORM = text_noform.Text;
                        KODE_CUST = kode_cust;
                        KODE_CT = kode_ct;
                        SITE = site;
                        NAMA_CUST = nama_cust;
                        NAMA_CT = nama_ct;

                        TR_FORM_GDR_CUST_TEMP trformgdrcusttemp = new TR_FORM_GDR_CUST_TEMP();
                        trformgdrcusttemp.KODE_FORM = KODE_FORM;
                        trformgdrcusttemp.NO_FORM = NO_FORM;
                        trformgdrcusttemp.kode_cust = KODE_CUST;
                        trformgdrcusttemp.kode_ct = KODE_CT;
                        trformgdrcusttemp.site = SITE;
                        trformgdrcusttemp.nama_cust = NAMA_CUST;
                        trformgdrcusttemp.nama_ct = NAMA_CT;
                        TrFormGdrCustTemp.Insert(trformgdrcusttemp);

                        gvCustCt.DataBind();


                    }
                }
                else if (jenis == "RENOVASI_COUNTER")
                {
                    string WhereCounter = string.Format("(site LIKE '5%' OR site LIKE '6%')");
                    Ds = MsCustCt.GetDataFilter(WhereCounter);

                    int i = 0;
                    int index = 0;
                    foreach (DataRow Item in Ds.Tables[0].Rows)
                    {
                        index = i;
                        i++;

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_cust"))))
                        {
                            kode_cust = Item.Field<String>("kode_cust");
                        }
                        else
                        {
                            kode_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_ct"))))
                        {
                            kode_ct = Item.Field<String>("kode_ct");
                        }
                        else
                        {
                            kode_ct = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("site"))))
                        {
                            site = Item.Field<String>("site");
                        }
                        else
                        {
                            site = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_cust"))))
                        {
                            nama_cust = Item.Field<String>("nama_cust");
                        }
                        else
                        {
                            nama_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_ct"))))
                        {
                            nama_ct = Item.Field<String>("nama_ct");
                        }
                        else
                        {
                            nama_ct = "";
                        }

                        //int colsCount = dtToko.Columns.Count;
                        //dtToko.Rows.Add(index, KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct);
                        //gvCustCt.DataBind();
                        int ID = index;
                        string KODE_CUST = "";
                        string KODE_CT = "";
                        string SITE = "";
                        string NAMA_CUST = "";
                        string NAMA_CT = "";

                        KODE_FORM = Common.KD_FORM_OTHERS;
                        NO_FORM = text_noform.Text;
                        KODE_CUST = kode_cust;
                        KODE_CT = kode_ct;
                        SITE = site;
                        NAMA_CUST = nama_cust;
                        NAMA_CT = nama_ct;

                        TR_FORM_GDR_CUST_TEMP trformgdrcusttemp = new TR_FORM_GDR_CUST_TEMP();
                        trformgdrcusttemp.KODE_FORM = KODE_FORM;
                        trformgdrcusttemp.NO_FORM = NO_FORM;
                        trformgdrcusttemp.kode_cust = KODE_CUST;
                        trformgdrcusttemp.kode_ct = KODE_CT;
                        trformgdrcusttemp.site = SITE;
                        trformgdrcusttemp.nama_cust = NAMA_CUST;
                        trformgdrcusttemp.nama_ct = NAMA_CT;
                        TrFormGdrCustTemp.Insert(trformgdrcusttemp);

                        gvCustCt.DataBind();


                    }
                }
                else if (jenis == "EXISTING_COUNTER")
                {
                    string WhereCounter = string.Format("(site LIKE '5%' OR site LIKE '6%')");
                    Ds = MsCustCt.GetDataFilter(WhereCounter);

                    int i = 0;
                    int index = 0;
                    foreach (DataRow Item in Ds.Tables[0].Rows)
                    {
                        index = i;
                        i++;

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_cust"))))
                        {
                            kode_cust = Item.Field<String>("kode_cust");
                        }
                        else
                        {
                            kode_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_ct"))))
                        {
                            kode_ct = Item.Field<String>("kode_ct");
                        }
                        else
                        {
                            kode_ct = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("site"))))
                        {
                            site = Item.Field<String>("site");
                        }
                        else
                        {
                            site = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_cust"))))
                        {
                            nama_cust = Item.Field<String>("nama_cust");
                        }
                        else
                        {
                            nama_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_ct"))))
                        {
                            nama_ct = Item.Field<String>("nama_ct");
                        }
                        else
                        {
                            nama_ct = "";
                        }

                        //int colsCount = dtToko.Columns.Count;
                        //dtToko.Rows.Add(index, KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct);
                        //gvCustCt.DataBind();
                        int ID = index;
                        string KODE_CUST = "";
                        string KODE_CT = "";
                        string SITE = "";
                        string NAMA_CUST = "";
                        string NAMA_CT = "";

                        KODE_FORM = Common.KD_FORM_OTHERS;
                        NO_FORM = text_noform.Text;
                        KODE_CUST = kode_cust;
                        KODE_CT = kode_ct;
                        SITE = site;
                        NAMA_CUST = nama_cust;
                        NAMA_CT = nama_ct;

                        TR_FORM_GDR_CUST_TEMP trformgdrcusttemp = new TR_FORM_GDR_CUST_TEMP();
                        trformgdrcusttemp.KODE_FORM = KODE_FORM;
                        trformgdrcusttemp.NO_FORM = NO_FORM;
                        trformgdrcusttemp.kode_cust = KODE_CUST;
                        trformgdrcusttemp.kode_ct = KODE_CT;
                        trformgdrcusttemp.site = SITE;
                        trformgdrcusttemp.nama_cust = NAMA_CUST;
                        trformgdrcusttemp.nama_ct = NAMA_CT;
                        TrFormGdrCustTemp.Insert(trformgdrcusttemp);

                        gvCustCt.DataBind();


                    }
                }
                else if (jenis == "RFS")
                {
                    string WhereCounter = string.Format("site LIKE '9%' AND (site NOT LIKE '%998%' AND site NOT LIKE '%999%')");
                    Ds = MsCustCt.GetDataFilter(WhereCounter);

                    int i = 0;
                    int index = 0;
                    foreach (DataRow Item in Ds.Tables[0].Rows)
                    {
                        index = i;
                        i++;

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_cust"))))
                        {
                            kode_cust = Item.Field<String>("kode_cust");
                        }
                        else
                        {
                            kode_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("kode_ct"))))
                        {
                            kode_ct = Item.Field<String>("kode_ct");
                        }
                        else
                        {
                            kode_ct = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("site"))))
                        {
                            site = Item.Field<String>("site");
                        }
                        else
                        {
                            site = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_cust"))))
                        {
                            nama_cust = Item.Field<String>("nama_cust");
                        }
                        else
                        {
                            nama_cust = "";
                        }

                        if (!String.IsNullOrEmpty((Item.Field<String>("nama_ct"))))
                        {
                            nama_ct = Item.Field<String>("nama_ct");
                        }
                        else
                        {
                            nama_ct = "";
                        }

                        //int colsCount = dtToko.Columns.Count;
                        //dtToko.Rows.Add(index, KODE_FORM, NO_FORM, kode_cust, kode_ct, site, nama_cust, nama_ct);
                        //gvCustCt.DataBind();
                        int ID = index;
                        string KODE_CUST = "";
                        string KODE_CT = "";
                        string SITE = "";
                        string NAMA_CUST = "";
                        string NAMA_CT = "";

                        KODE_FORM = Common.KD_FORM_OTHERS;
                        NO_FORM = text_noform.Text;
                        KODE_CUST = kode_cust;
                        KODE_CT = kode_ct;
                        SITE = site;
                        NAMA_CUST = nama_cust;
                        NAMA_CT = nama_ct;

                        TR_FORM_GDR_CUST_TEMP trformgdrcusttemp = new TR_FORM_GDR_CUST_TEMP();
                        trformgdrcusttemp.KODE_FORM = KODE_FORM;
                        trformgdrcusttemp.NO_FORM = NO_FORM;
                        trformgdrcusttemp.kode_cust = KODE_CUST;
                        trformgdrcusttemp.kode_ct = KODE_CT;
                        trformgdrcusttemp.site = SITE;
                        trformgdrcusttemp.nama_cust = NAMA_CUST;
                        trformgdrcusttemp.nama_ct = NAMA_CT;
                        TrFormGdrCustTemp.Insert(trformgdrcusttemp);

                        gvCustCt.DataBind();


                    }
                }
                else
                {
                    gvCustCt.DataBind();
                }


            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SaveDetailCustCt()
        {
            try
            {
                TR_FORM_GDR_CUST_DA TrFormGdrCust = new DataLayer.TR_FORM_GDR_CUST_DA();
                DataSet Ds = new DataSet();

                //int i = 0;
                //int index = 0;
                string KODE_FORM = "";
                string NO_FORM = "";

                KODE_FORM = Common.KD_FORM_OTHERS;
                NO_FORM = text_noform.Text;
                TrFormGdrCust.DeleteFilter(string.Format("KODE_FORM = '{0}' AND NO_FORM = '{1}'", KODE_FORM, NO_FORM));


                List<object> SelectedCust = gvCustCt.GetSelectedFieldValues(new string[] { "ID", "kode_cust", "kode_ct", "site", "nama_cust", "nama_ct" });
                foreach (object cust in SelectedCust)
                {
                    IList items = cust as IList;
                    if (items == null) return;

                    string KODE_CUST = "";
                    string KODE_CT = "";
                    string SITE = "";
                    string NAMA_CUST = "";
                    string NAMA_CT = "";

                    int ID = Convert.ToInt32(items[0]);
                    KODE_CUST = Convert.ToString(items[1]);
                    KODE_CT = Convert.ToString(items[2]);
                    SITE = Convert.ToString(items[3]);
                    NAMA_CUST = Convert.ToString(items[4]);
                    NAMA_CT = Convert.ToString(items[5]);

                    TR_FORM_GDR_CUST trformgdrcust = new TR_FORM_GDR_CUST();
                    trformgdrcust.KODE_FORM = KODE_FORM;
                    trformgdrcust.NO_FORM = NO_FORM;
                    trformgdrcust.kode_cust = KODE_CUST;
                    trformgdrcust.kode_ct = KODE_CT;
                    trformgdrcust.site = SITE;
                    trformgdrcust.nama_cust = NAMA_CUST;
                    trformgdrcust.nama_ct = NAMA_CT;
                    TrFormGdrCust.Insert(trformgdrcust);

                }

                TR_FORM_GDR_CUST_TEMP_DA TrFormGdrCustTemp = new DataLayer.TR_FORM_GDR_CUST_TEMP_DA();
                TrFormGdrCustTemp.DeleteFilter(string.Format("KODE_FORM = '{0}' AND NO_FORM = '{1}'", KODE_FORM, NO_FORM));

                //List<object> keys = gvCustCt.GetSelectedFieldValues(new string[] { gvCustCt.KeyFieldName });
                //List<object> kode_cust = gvCustCt.GetSelectedFieldValues("kode_cust");
                ////int a = ASPxGridView1.VisibleRowCount;
                //for (int i = 0; i < keys.Count; i++)
                //{
                //   string dataInCell = Convert.ToString(.GetRowCellValue(i, "FieldName"));
                //    //ASPxGridView1.UpdateEdit(ASPxGridView1.FindVisibleIndexByKeyValue(keys[i]));

                //}

                //foreach (GridViewRow Item in gvCustCt.Rows)
                //{
                //    index = i;
                //    // do stuff
                //    i++;

                //    if ((Item.FindControl("chkRow") as CheckBox).Checked)
                //    {
                //        int ID = index;
                //        string KODE_FORM = "";
                //        string NO_FORM = "";
                //        string KODE_CUST = "";
                //        string KODE_CT = "";
                //        string SITE = "";
                //        string NAMA_CUST = "";
                //        string NAMA_CT = "";

                //        KODE_FORM = Common.KD_FORM_OTHERS;
                //        NO_FORM = text_noform.Text;
                //        KODE_CUST = Item.Cells[4].Text;
                //        KODE_CT = Item.Cells[5].Text;
                //        SITE = Item.Cells[6].Text;
                //        NAMA_CUST = Item.Cells[7].Text;
                //        NAMA_CT = Item.Cells[8].Text;

                //        TR_FORM_GDR_CUST trformgdrcust = new TR_FORM_GDR_CUST();
                //        trformgdrcust.KODE_FORM = KODE_FORM;
                //        trformgdrcust.NO_FORM = NO_FORM;
                //        trformgdrcust.kode_cust = KODE_CUST;
                //        trformgdrcust.kode_ct = KODE_CT;
                //        trformgdrcust.site = SITE;
                //        trformgdrcust.nama_cust = NAMA_CUST;
                //        trformgdrcust.nama_ct = NAMA_CT;
                //        TrFormGdrCust.Insert(trformgdrcust);

                //    }

                //}
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        /// <summary>
        /// Add Data Pop Up Detail TR_FORM3_GDR_MATERI
        /// </summary>

        public void LoadDetailGridMateri()
        {
            try
            {
                dt.Columns.AddRange(new DataColumn[9] {
                    new DataColumn("ID_MATERI", typeof(string)),
                    new DataColumn("NO_FORM", typeof(string)),
                    new DataColumn("site", typeof(string)),
                    new DataColumn("nama_cust", typeof(string)),
                    new DataColumn("JENIS_MATERIAL_CETAK", typeof(string)),
                    new DataColumn("UKURAN", typeof(string)),
                    new DataColumn("MATERIAL", typeof(string)),
                    new DataColumn("JUMLAH_QTY", typeof(string)),
                    new DataColumn("PENJELASAN", typeof(string)),
                });
                ViewState["MateriCetak"] = dt;
                this.BindGrid();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }


        }

        protected void BindGrid()
        {
            gvMain.DataSource = (DataTable)ViewState["MateriCetak"];
            gvMain.DataBind();
        }

        public void LoadDataDetailMateri()
        {
            try
            {
                TR_FORM3_GDR_MATERI_DA TrForm3GdrMateri = new DataLayer.TR_FORM3_GDR_MATERI_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string site = "";
                string nama_cust = "";
                string JENIS_MATERIAL_CETAK = "";
                string UKURAN = "";
                string MATERIAL = "";
                //string JUMLAH_QTY = "";
                string PENJELASAN = "";

                string Where = string.Format("NO_FORM = '{0}'", NO_FORM);

                Ds = TrForm3GdrMateri.GetDataFilter(Where);

                int i = 0;
                int index = 0;
                foreach (DataRow Item in Ds.Tables[0].Rows)
                {
                    index = i;
                    i++;


                    if (!String.IsNullOrEmpty((Item.Field<String>("site"))))
                    {
                        site = Item.Field<String>("site");
                    }
                    else
                    {
                        site = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("nama_cust"))))
                    {
                        nama_cust = Item.Field<String>("nama_cust");
                    }
                    else
                    {
                        nama_cust = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("JENIS_MATERIAL_CETAK"))))
                    {
                        JENIS_MATERIAL_CETAK = Item.Field<String>("JENIS_MATERIAL_CETAK");
                    }
                    else
                    {
                        JENIS_MATERIAL_CETAK = "";
                    }


                    if (!String.IsNullOrEmpty((Item.Field<String>("UKURAN"))))
                    {
                        UKURAN = Item.Field<String>("UKURAN");
                    }
                    else
                    {
                        UKURAN = "";
                    }


                    if (!String.IsNullOrEmpty((Item.Field<String>("MATERIAL"))))
                    {
                        MATERIAL = Item.Field<String>("MATERIAL");
                    }
                    else
                    {
                        MATERIAL = "";
                    }

                    decimal? JUMLAH_QTY = Item.Field<decimal?>("JUMLAH_QTY");
                    if (JUMLAH_QTY.HasValue)
                    {
                        JUMLAH_QTY = Item.Field<decimal?>("JUMLAH_QTY");
                    }
                    else
                    {
                        JUMLAH_QTY = 0;
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("PENJELASAN"))))
                    {
                        PENJELASAN = Item.Field<String>("PENJELASAN");
                    }
                    else
                    {
                        PENJELASAN = "";
                    }

                    dt.Rows.Add(index, NO_FORM, site, nama_cust, JENIS_MATERIAL_CETAK, UKURAN, MATERIAL, JUMLAH_QTY, PENJELASAN);
                    gvMain.DataBind();
                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void AddToDetailMateri()
        {
            try
            {
                string noform = text_noform.Text;
                string site = "-";
                string nama_cust = "-";
                string jenis = text_jenismatericetak.Text;
                string ukuran = text_ukuran.Text;
                string material = text_material.Text;
                string jumlahQTY = text_jumlahQTY.Text;
                string penjelasan = text_penjelasan.Text;

                if (jenis == "") jenis = "";
                if (ukuran == "") ukuran = "";
                if (material == "") material = "";
                if (jumlahQTY == "") jumlahQTY = "0";
                if (penjelasan == "") penjelasan = "";

                if (jenis != "" && ukuran != "" && material != "" & jumlahQTY != "0")
                {
                    DataTable dt = (DataTable)ViewState["MateriCetak"];
                    dt.Rows.Add("1", noform, site, nama_cust, jenis, ukuran, material, Convert.ToDecimal(jumlahQTY), penjelasan);
                    ViewState["MateriCetak"] = dt;
                    this.BindGrid();

                    text_jenismatericetak.Text = string.Empty;
                    text_ukuran.Text = string.Empty;
                    text_material.Text = string.Empty;
                    text_jumlahQTY.Text = string.Empty;
                    text_penjelasan.Text = string.Empty;

                    DivMessageMaterial.Visible = false;
                }
                else
                {
                    ModalJenisMateri.Show();
                    DivMessageMaterial.InnerText = "Type / Size / Material / Amount of Qty Can Not Be Empty";
                    DivMessageMaterial.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessageMaterial.Visible = true;
                }

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SaveDetailMateriCetak()
        {
            try
            {
                TR_FORM3_GDR_MATERI_DA TrForm3GdrMateri = new DataLayer.TR_FORM3_GDR_MATERI_DA();
                DataSet Ds = new DataSet();

                int i = 0;
                int index = 0;
                foreach (GridViewRow Item in gvMain.Rows)
                {
                    index = i;
                    // do stuff
                    i++;

                    int ID_MATERI = index;
                    string NO_FORM = "";
                    string site = "";
                    string nama_cust = "";
                    string JENIS_MATERIAL_CETAK = "";
                    string UKURAN = "";
                    string MATERIAL = "";
                    string JUMLAH_QTY = "";
                    string PENJELASAN = "";
                    string retex = "&nbsp;";
                    NO_FORM = HttpUtility.HtmlDecode(text_noform.Text.Replace(retex, "").Trim());
                    site = HttpUtility.HtmlDecode(Item.Cells[2].Text.Replace(retex, "").Trim());
                    nama_cust = HttpUtility.HtmlDecode(Item.Cells[3].Text.Replace(retex, "").Trim());
                    JENIS_MATERIAL_CETAK = HttpUtility.HtmlDecode(Item.Cells[4].Text.Replace(retex, "").Trim());
                    UKURAN = HttpUtility.HtmlDecode(Item.Cells[5].Text.Replace(retex, "").Trim());
                    MATERIAL = HttpUtility.HtmlDecode(Item.Cells[6].Text.Replace(retex, "").Trim());
                    if (HttpUtility.HtmlDecode(Item.Cells[7].Text.Replace(retex, "").Trim()) == "")
                    {
                        JUMLAH_QTY = "0";
                    }
                    else
                    {
                        JUMLAH_QTY = HttpUtility.HtmlDecode(Item.Cells[7].Text.Replace(retex, "").Trim());
                    }

                    PENJELASAN = HttpUtility.HtmlDecode(Item.Cells[8].Text.Replace(retex, "").Trim());

                    TR_FORM3_GDR_MATERI TrForm3gdrmateri = new TR_FORM3_GDR_MATERI();
                    //TrForm3gdrmateri.ID_MATERI = ID_MATERI;
                    TrForm3gdrmateri.NO_FORM = NO_FORM;
                    TrForm3gdrmateri.site = site;
                    TrForm3gdrmateri.nama_cust = nama_cust;
                    TrForm3gdrmateri.JENIS_MATERIAL_CETAK = JENIS_MATERIAL_CETAK;
                    TrForm3gdrmateri.UKURAN = UKURAN;
                    TrForm3gdrmateri.MATERIAL = MATERIAL;
                    TrForm3gdrmateri.JUMLAH_QTY = Convert.ToDecimal(JUMLAH_QTY);
                    TrForm3gdrmateri.PENJELASAN = PENJELASAN;
                    TrForm3GdrMateri.Insert(TrForm3gdrmateri);

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateDetailMateriCetak()
        {
            try
            {
                TR_FORM3_GDR_MATERI_DA TrForm3GdrMateri = new DataLayer.TR_FORM3_GDR_MATERI_DA();
                DataSet Ds = new DataSet();

                string noform = text_noform.Text;
                string Where = string.Format("NO_FORM = '{0}'", noform);
                TrForm3GdrMateri.DeleteFilter(Where);
                SaveDetailMateriCetak();

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        /// <summary>
        /// Add Data Pop Up Detail TR_FORM3_GDR_MATERI
        /// </summary>
        /// 
        public void ChangeButtonColor()
        {

            //Button Done Change Color
            if (btn_Done.Enabled)
            {


            }
            else
            {
                btn_Done.BackColor = Color.Gray;

            }

            //Button Approved Change Color
            if (btn_Approved.Enabled)
            {


            }
            else
            {
                btn_Approved.BackColor = Color.Gray;
                //PanelMain.Enabled = false;
            }

            //Button To Revise Change Color
            if (btn_ToRevise.Enabled)
            {


            }
            else
            {
                btn_ToRevise.BackColor = Color.Gray;
            }

            //Button To Revise Design Change Color
            if (btn_ToReviseDesign.Enabled)
            {


            }
            else
            {
                btn_ToReviseDesign.BackColor = Color.Gray;
            }

            //Button Revise Content Change Color
            if (btn_ReviseContent.Enabled)
            {


            }
            else
            {
                btn_ReviseContent.BackColor = Color.Gray;
            }


            //Button Revise Change Color
            if (btn_Revise.Enabled)
            {


            }
            else
            {
                btn_Revise.BackColor = Color.Gray;
            }

            //Button Revise Change Color
            if (btn_ReviseDesign.Enabled)
            {


            }
            else
            {
                btn_ReviseDesign.BackColor = Color.Gray;
            }

            //Button Accepted Change Color
            if (btn_Accepted.Enabled)
            {


            }
            else
            {
                btn_Accepted.BackColor = Color.Gray;
            }

            //Button Save Change Color
            if (btn_Save.Enabled)
            {


            }
            else
            {
                btn_Save.BackColor = Color.Gray;
            }

            //Button Update Posting Example
            if (btn_UpdateSubmit.Enabled)
            {


            }
            else
            {
                btn_UpdateSubmit.BackColor = Color.Gray;
            }

            //Button Cancel Change Color
            if (btn_Cancel.Enabled)
            {


            }
            else
            {
                btn_Cancel.BackColor = Color.Gray;
            }

            //Button Cancel Posting Change Color
            if (btn_Reject.Enabled)
            {


            }
            else
            {
                btn_Reject.BackColor = Color.Gray;
            }

            //Button Delete Change Color
            if (btn_Delete.Enabled)
            {


            }
            else
            {
                btn_Delete.BackColor = Color.Gray;
            }
        }

        /// <summary>
        /// Sending Email. Type = Submit, Approve, Approve BM, Accept.
        /// </summary>

        public void SendEmailAllType()
        {
            try
            {
                DataSet DsForm = new DataSet();
                TR_FORM3_GDR_DA TrForm3DA = new TR_FORM3_GDR_DA();

                //Mendapatkan Status Form Berdasarkan No Form
                string NO_FORM = text_noform.Text;
                string Status = "";
                DsForm = TrForm3DA.GetDataByKey(NO_FORM);

                if (DsForm.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToString(DsForm.Tables[0].Rows[0]["STATUS"].ToString());

                    switch (Status)
                    {
                        case "Submit":
                            SendEmailOnApprovedToHD();
                            //SendEmailApprovedHDToHeadDesign();
                            break;
                        case "Approved-Head-Department":
                            SendEmailApprovedHDToHeadDesign();
                            break;
                        case "Approved-Head-Department-Photo":
                            SendEmailApprovedHDToPhoto();
                            break;
                        case "Approved-Head-Department-DI":
                            SendEmailApprovedHDToDI();
                            break;
                        case "Approved-Photo":
                            SendEmailApprovedPhotoToCreativeDirectorPhoto();
                            break;
                        case "Approved-Creative-Director-Photo":
                            SendEmailApprovedCreativeDirectorPhotoToDI();
                            break;
                        case "Approved-DI":
                            SendEmailApprovedDIToCreativeDirectorDI();
                            break;
                        case "Approved-Creative-Director-DI":
                            SendEmailApprovedCreativeDirectorDIToHeadDesign();
                            break;
                        case "Approved-Admin-Creative":
                            SendEmailAcceptedHeadDesignToGraphicDesign();
                            break;
                        case "Posting-Design":
                            SendEmailAcceptedGraphicDesignToCreativeManager();
                            break;
                        case "Approved-Creative-Director":
                            SendEmailAcceptedCreativeManagerToDone();
                            break;
                        case "Done":
                            SendEmailAcceptedCreativeManagerToDone();
                            SendEmailUserToDone();
                            break;
                        case "In-Production":
                            SendEmailAcceptedCreativeManagerToProduction();
                            break;
                        case "Delivered-Distribution":
                            SendEmailAcceptedCreativeManagerToDone();
                            SendEmailUserToDone();
                            break;
                        case "On-Revise":
                            SendEmailReviseToUser();
                            break;
                        case "On-Revise-Photo":
                            SendEmailReviseCreativeDirectorPhotoToPhoto();
                            break;
                        case "On-Revise-Photo-DI":
                            SendEmailReviseCreativeDirectorDIToDI();
                            break;
                        case "On-Revise-Design":
                            SendEmailReviseDesignToGraphicDesign();
                            break;
                        case "On-Revise-Content":
                            SendEmailReviseContentToGraphicDesign();
                            break;
                        case "Cancel":
                            SendEmailCancelToUser();
                            break;
                        default:
                            Console.WriteLine("Mengirim Email");
                            break;
                    }

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailOnApprovedToHD()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("ID_DEPT LIKE '%{0}%' And KD_JABATAN = 'HDP'", HfID_DEPT.Value);
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Head Department ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Approve-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailApprovedHDToHeadDesign()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("KD_JABATAN = 'ADM-CREATIVE'");
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Admin Creative ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Approve-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailApprovedHDToPhoto()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("KD_JABATAN = 'PHOTO'");
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Photographer ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Approve-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailApprovedHDToDI()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("KD_JABATAN = 'DI'");
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Digital Imaging,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Approve-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailAcceptedHeadDesignToGraphicDesign()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string diterima2 = ddlditerima2.SelectedValue.ToString();
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("USERNAME = '{0}' And KD_JABATAN = 'GD'", diterima2);
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Graphic Design ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Approve-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailAcceptedGraphicDesignToCreativeManager()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("ID_DEPT NOT LIKE '%13%' And KD_JABATAN = 'CM'");
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Creative Director,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan Design berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Approve-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailAcceptedCreativeManagerToDone()
        {
            try
            {
                int Id_Dept = 0;
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("ID_DEPT NOT LIKE '%13%' And KD_JABATAN = 'CM'");
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Creative Director ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Berhasil diselesaikan <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Approve-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailAcceptedCreativeManagerToProduction()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("KD_JABATAN = 'PDC'");
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Production,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Approve-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //Send Email Fotografer
        public void SendEmailApprovedPhotoToCreativeDirectorPhoto()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("ID_DEPT NOT LIKE '%13%' And KD_JABATAN = 'CM'");
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Creative Director,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan Design berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Approve-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailApprovedCreativeDirectorPhotoToDI()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("KD_JABATAN = 'DI'", HfID_DEPT.Value);
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Admin Creative ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Approve-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailApprovedDIToCreativeDirectorDI()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("ID_DEPT NOT LIKE '%13%' And KD_JABATAN = 'CM'");
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Creative Director,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan Design berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Approve-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailApprovedCreativeDirectorDIToHeadDesign()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("KD_JABATAN = 'ADM-CREATIVE'");
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Admin Creative ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Approve-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailUserToDone()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string NO_FORM = text_noform.Text;
                string DIBUAT = text_dibuat.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("NO_FORM = '{0}' And DIBUAT = '{1}'", NO_FORM, DIBUAT);
                Ds = TrForm3Gdr.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["DIBUAT"].ToString());

                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    string WhereUser = string.Format("USERNAME = '{0}'", USERNAME);
                    DsUser = MsUserDA.GetDataFilter(WhereUser);
                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                    }

                    //Keterangan Isi Form
                    //string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    //string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Requester,<br />" + USERNAME + "<br /><br />Dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Berhasil diselesaikan <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Ticket Done-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailReviseToUser()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string NO_FORM = text_noform.Text;
                string DIBUAT = text_dibuat.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("NO_FORM = '{0}' And DIBUAT = '{1}'", NO_FORM, DIBUAT);
                Ds = TrForm3Gdr.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["DIBUAT"].ToString());
                    string MENYETUJUI_1 = "";
                    string DITERIMA_1 = "";
                    string DITERIMA_2 = "";
                    string DITERIMA_3 = "";
                    if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["MENYETUJUI1"].ToString())))
                    {
                        MENYETUJUI_1 = Convert.ToString(Ds.Tables[0].Rows[0]["MENYETUJUI1"].ToString());
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_1"].ToString())))
                    {
                        DITERIMA_1 = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_1"].ToString());
                    }
                    if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_2"].ToString())))
                    {
                        DITERIMA_2 = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_2"].ToString());
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_3"].ToString())))
                    {
                        DITERIMA_3 = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_3"].ToString());
                    }

                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();
                    DataSet DsHeadDepartment = new DataSet();
                    DataSet DsSectionHeadDesign = new DataSet();
                    DataSet DsGraphicDesign = new DataSet();
                    DataSet DsCreativeManager = new DataSet();

                    string URUTAN = "";

                    //Keterangan Isi Form
                    //string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    //string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);
                    string REVISI = text_revisi.Text;


                    //Mendapatkan Urutan User Berdasarkan Jabatan
                    DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Requester,<br />" + USERNAME + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Revisi oleh: " + HfUsername.Value + "<br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + USERNAME))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);

                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + USERNAME))));
                        MailHelper.SendMail(Email, "Email Revise-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                    }

                    //Mendapatkan Urutan Head Department Berdasarkan Jabatan
                    DsHeadDepartment = MsUserDA.GetDataUrutanUser(MENYETUJUI_1, KODE_FORM);
                    if (DsHeadDepartment.Tables[0].Rows.Count > 0)
                    {
                        Email = DsHeadDepartment.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsHeadDepartment.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsHeadDepartment.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Head Department,<br />" + MENYETUJUI_1 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Revisi oleh: " + HfUsername.Value + "<br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + MENYETUJUI_1))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);



                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + MENYETUJUI_1))));
                        MailHelper.SendMail(Email, "Email Revise-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                    }

                    //Mendapatkan Urutan Section Head Berdasarkan Jabatan
                    DsSectionHeadDesign = MsUserDA.GetDataUrutanUser(DITERIMA_1, KODE_FORM);
                    if (DsSectionHeadDesign.Tables[0].Rows.Count > 0)
                    {
                        Email = DsSectionHeadDesign.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsSectionHeadDesign.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsSectionHeadDesign.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Section Head,<br />" + DITERIMA_1 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Revisi oleh: " + HfUsername.Value + " <br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + DITERIMA_1))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);



                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + DITERIMA_1))));
                        MailHelper.SendMail(Email, "Email Revise-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                    }

                    //Mendapatkan Urutan Graphic Design Berdasarkan Jabatan
                    DsGraphicDesign = MsUserDA.GetDataUrutanUser(DITERIMA_2, KODE_FORM);
                    if (DsGraphicDesign.Tables[0].Rows.Count > 0)
                    {
                        Email = DsGraphicDesign.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsGraphicDesign.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsGraphicDesign.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Graphic Design,<br />" + DITERIMA_2 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Revisi oleh: " + HfUsername.Value + " <br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + DITERIMA_2))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);



                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + DITERIMA_2))));
                        MailHelper.SendMail(Email, "Email Revise-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                    }

                    //Mendapatkan Urutan Creative Manager Berdasarkan Jabatan
                    DsCreativeManager = MsUserDA.GetDataUrutanUser(DITERIMA_3, KODE_FORM);
                    if (DsCreativeManager.Tables[0].Rows.Count > 0)
                    {
                        Email = DsCreativeManager.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsCreativeManager.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsCreativeManager.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Creative Director,<br />" + DITERIMA_3 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Revisi oleh: " + HfUsername.Value + " <br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + DITERIMA_3))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);



                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + DITERIMA_3))));
                        MailHelper.SendMail(Email, "Email Revise-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                    }
                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailReviseContentToUser()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string NO_FORM = text_noform.Text;
                string DIBUAT = text_dibuat.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("NO_FORM = '{0}' And DIBUAT = '{1}'", NO_FORM, DIBUAT);
                Ds = TrForm3Gdr.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["DIBUAT"].ToString());

                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    string WhereUser = string.Format("USERNAME = '{0}'", USERNAME);
                    DsUser = MsUserDA.GetDataFilter(WhereUser);
                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                    }

                    //Keterangan Isi Form
                    //string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    //string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Requester,<br />" + USERNAME + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Berhasil Di Revise Content <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Revise Content-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailReviseDesignToGraphicDesign()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string DITERIMA_2 = ddlditerima2.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("USERNAME = '{0}' And KD_JABATAN = 'GD'", DITERIMA_2);
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);
                    string REVISI = text_revisi.Text;

                    string EmailTemplateEng = "Dear Graphic Design,<br />" + USERNAME + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Revisi oleh: " + HfUsername.Value + " <br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";


                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Revise Design-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailReviseContentToGraphicDesign()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string DITERIMA_2 = ddlditerima2.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("USERNAME = '{0}' And KD_JABATAN = 'GD'", DITERIMA_2);
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());


                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);
                    string REVISI = text_revisi.Text;

                    string EmailTemplateEng = "Dear Graphic Design,<br />" + USERNAME + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Revisi oleh: " + HfUsername.Value + " <br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);

                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Revise Design-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                    SendEmailReviseContentToUser();

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //Revise Fotografer
        public void SendEmailReviseCreativeDirectorPhotoToPhoto()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string DITERIMA_LAIN_1 = text_diterimalain1.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("USERNAME = '{0}' And KD_JABATAN = 'PHOTO'", DITERIMA_LAIN_1);
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());


                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);
                    string REVISI = text_revisi.Text;

                    string EmailTemplateEng = "Dear Fotografer,<br />" + USERNAME + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Revisi oleh: " + HfUsername.Value + " <br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Revise Design-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailReviseCreativeDirectorDIToDI()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string DITERIMA_LAIN_3 = text_diterimalain3.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("USERNAME = '{0}' And KD_JABATAN = 'DI'", DITERIMA_LAIN_3);
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());


                    //Keterangan Isi Form
                    string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);
                    string REVISI = text_revisi.Text;

                    string EmailTemplateEng = "Dear Fotografer,<br />" + USERNAME + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Revisi oleh: " + HfUsername.Value + " <br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Revise Design-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

                }
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SendEmailCancelToUser()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string NO_FORM = text_noform.Text;
                string DIBUAT = text_dibuat.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                TR_FORM3_GDR_DA TrForm3Gdr = new DataLayer.TR_FORM3_GDR_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("NO_FORM = '{0}' And DIBUAT = '{1}'", NO_FORM, DIBUAT);
                Ds = TrForm3Gdr.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["DIBUAT"].ToString());
                    string MENYETUJUI_1 = "";
                    string DITERIMA_1 = "";
                    string DITERIMA_2 = "";
                    string DITERIMA_3 = "";
                    if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["MENYETUJUI1"].ToString())))
                    {
                        MENYETUJUI_1 = Convert.ToString(Ds.Tables[0].Rows[0]["MENYETUJUI1"].ToString());
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_1"].ToString())))
                    {
                        DITERIMA_1 = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_1"].ToString());
                    }
                    if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_2"].ToString())))
                    {
                        DITERIMA_2 = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_2"].ToString());
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_3"].ToString())))
                    {
                        DITERIMA_3 = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_3"].ToString());
                    }

                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();
                    DataSet DsHeadDepartment = new DataSet();
                    DataSet DsSectionHeadDesign = new DataSet();
                    DataSet DsGraphicDesign = new DataSet();
                    DataSet DsCreativeManager = new DataSet();

                    string URUTAN = "";

                    //Keterangan Isi Form
                    //string NO_FORM = text_noform.Text;
                    DataSet DsForm = new DataSet();
                    MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                    string FORM_TYPE = "";
                    DsForm = msformda.GetDataByKey(KODE_FORM);
                    if (DsForm.Tables[0].Rows.Count > 0)
                    {
                        FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                    }
                    string BRAND = text_brand.Text;
                    //string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    //Mendapatkan Urutan User Berdasarkan Jabatan
                    DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Requester,<br />" + USERNAME + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Cancel oleh: " + HfUsername.Value + "<br/>Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + USERNAME))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);

                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + USERNAME))));
                        MailHelper.SendMail(Email, "Email Cancel-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                    }

                    //Mendapatkan Urutan Head Department Berdasarkan Jabatan
                    DsHeadDepartment = MsUserDA.GetDataUrutanUser(MENYETUJUI_1, KODE_FORM);
                    if (DsHeadDepartment.Tables[0].Rows.Count > 0)
                    {
                        Email = DsHeadDepartment.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsHeadDepartment.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsHeadDepartment.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Head Department,<br />" + MENYETUJUI_1 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Cancel oleh: " + HfUsername.Value + "<br/>Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + MENYETUJUI_1))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);



                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + MENYETUJUI_1))));
                        MailHelper.SendMail(Email, "Email Cancel-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                    }

                    //Mendapatkan Urutan Section Head Berdasarkan Jabatan
                    DsSectionHeadDesign = MsUserDA.GetDataUrutanUser(DITERIMA_1, KODE_FORM);
                    if (DsSectionHeadDesign.Tables[0].Rows.Count > 0)
                    {
                        Email = DsSectionHeadDesign.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsSectionHeadDesign.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsSectionHeadDesign.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Section Head,<br />" + DITERIMA_1 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Cancel oleh: " + HfUsername.Value + "<br/>Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + DITERIMA_1))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);



                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + DITERIMA_1))));
                        MailHelper.SendMail(Email, "Email Cancel-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                    }

                    //Mendapatkan Urutan Graphic Design Berdasarkan Jabatan
                    DsGraphicDesign = MsUserDA.GetDataUrutanUser(DITERIMA_2, KODE_FORM);
                    if (DsGraphicDesign.Tables[0].Rows.Count > 0)
                    {
                        Email = DsGraphicDesign.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsGraphicDesign.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsGraphicDesign.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Graphic Design,<br />" + DITERIMA_2 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Cancel oleh: " + HfUsername.Value + "<br/>Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + DITERIMA_2))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);



                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + DITERIMA_2))));
                        MailHelper.SendMail(Email, "Email Cancel-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                    }

                    //Mendapatkan Urutan Creative Manager Berdasarkan Jabatan
                    DsCreativeManager = MsUserDA.GetDataUrutanUser(DITERIMA_3, KODE_FORM);
                    if (DsCreativeManager.Tables[0].Rows.Count > 0)
                    {
                        Email = DsCreativeManager.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsCreativeManager.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsCreativeManager.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Creative Director,<br />" + DITERIMA_3 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Cancel oleh: " + HfUsername.Value + "<br/>Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + DITERIMA_3))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);



                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + DITERIMA_3))));
                        MailHelper.SendMail(Email, "Email Cancel-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                    }
                }
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

        /// <summary>
        /// viewStateOnly, Except Button
        /// </summary>
        /// 

        public static DateTime AddBusinessDays(DateTime date, int days)
        {
            if (days < 0)
            {
                throw new ArgumentException("days cannot be negative", "days");
            }

            if (days == 0) return date;

            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                date = date.AddDays(2);
                days -= 1;
            }
            else if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
                days -= 1;
            }

            date = date.AddDays(days / 5 * 7);
            int extraDays = days % 5;

            if ((int)date.DayOfWeek + extraDays > 5)
            {
                extraDays += 2;
            }

            return date.AddDays(extraDays);

        }
        public void CalculateWorkingDaysRequiredDate()
        {
            try
            {
                //Check Required Date With Production
                string PRODUCTION = ddlproduction.Text;
                string PHOTOGRAPER = ddlphotographer.Text;
                string DIGITAL_IMAGING = ddldigitalimaging.Text;
                DateTime TGL_REQUEST = Convert.ToDateTime(text_tanggalrequest.Text);
                string TGL_REQUIRED = "";
                int Days = 0;

                if (PRODUCTION == EYesNo.Yes && PHOTOGRAPER == EYesNo.Yes && DIGITAL_IMAGING == EYesNo.Yes)
                {
                    Days = 22;
                    //string s = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                    DateTime dt = AddBusinessDays(TGL_REQUEST, Days);
                    TGL_REQUIRED = DateTime.Parse(Convert.ToString(dt)).ToString("yyyy-MM-dd");
                    text_tanggalrequired.Text = TGL_REQUIRED;
                }

                if (PRODUCTION == EYesNo.Yes && PHOTOGRAPER == EYesNo.No && DIGITAL_IMAGING == EYesNo.Yes)
                {
                    Days = 17;
                    //string s = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                    DateTime dt = AddBusinessDays(TGL_REQUEST, Days);
                    TGL_REQUIRED = DateTime.Parse(Convert.ToString(dt)).ToString("yyyy-MM-dd");
                    text_tanggalrequired.Text = TGL_REQUIRED;
                }

                if (PRODUCTION == EYesNo.Yes && PHOTOGRAPER == EYesNo.No && DIGITAL_IMAGING == EYesNo.No)
                {
                    Days = 15;
                    //string s = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                    DateTime dt = AddBusinessDays(TGL_REQUEST, Days);
                    TGL_REQUIRED = DateTime.Parse(Convert.ToString(dt)).ToString("yyyy-MM-dd");
                    text_tanggalrequired.Text = TGL_REQUIRED;
                }



                //Check Required Date No Production

                if (PRODUCTION == EYesNo.No && PHOTOGRAPER == EYesNo.Yes && DIGITAL_IMAGING == EYesNo.Yes)
                {
                    Days = 13;
                    //string s = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                    DateTime dt = AddBusinessDays(TGL_REQUEST, Days);
                    TGL_REQUIRED = DateTime.Parse(Convert.ToString(dt)).ToString("yyyy-MM-dd");
                    text_tanggalrequired.Text = TGL_REQUIRED;
                }

                if (PRODUCTION == EYesNo.No && PHOTOGRAPER == EYesNo.No && DIGITAL_IMAGING == EYesNo.Yes)
                {
                    Days = 8;
                    //string s = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                    DateTime dt = AddBusinessDays(TGL_REQUEST, Days);
                    TGL_REQUIRED = DateTime.Parse(Convert.ToString(dt)).ToString("yyyy-MM-dd");
                    text_tanggalrequired.Text = TGL_REQUIRED;
                }

                if (PRODUCTION == EYesNo.No && PHOTOGRAPER == EYesNo.No && DIGITAL_IMAGING == EYesNo.No)
                {
                    Days = 6;
                    //string s = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                    DateTime dt = AddBusinessDays(TGL_REQUEST, Days);
                    TGL_REQUIRED = DateTime.Parse(Convert.ToString(dt)).ToString("yyyy-MM-dd");
                    text_tanggalrequired.Text = TGL_REQUIRED;
                }


            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        public void CalculateWorkingDaysDetailDate()
        {
            //Check Required Detail Date With Production
            string PRODUCTION = ddlproduction.Text;
            string PHOTOGRAPER = ddlphotographer.Text;
            string DIGITAL_IMAGING = ddldigitalimaging.Text;
            DateTime TGL_REQUEST = Convert.ToDateTime(text_tanggalrequest.Text);
            string TGL_REQUIRED = "";
            string TGL_PHOTOGRAPHER = "";
            string TGL_DI = "";
            string TGL_ADM_CREATIVE = "";
            string TGL_FINISH_DESIGN = "";
            string TGL_PRODUCTION = "";
            string TGL_SEND = "";
            int DaysPhotoGrapher = 0;
            int DaysDI = 0;
            int DaysAdmCreative = 0;
            int DaysFinishDesign = 0;
            int DaysProduction = 0;
            int DaysSend = 0;

            if (PRODUCTION == EYesNo.Yes && PHOTOGRAPER == EYesNo.Yes && DIGITAL_IMAGING == EYesNo.No)
            {
                DaysPhotoGrapher = 5;
                DaysDI = 0;
                DaysAdmCreative = DaysPhotoGrapher + 1;
                DaysFinishDesign = DaysAdmCreative + 5;
                DaysProduction = DaysFinishDesign + 4;
                DaysSend = DaysProduction + 3;
                //string s = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                DateTime dtphotographer = AddBusinessDays(TGL_REQUEST, DaysPhotoGrapher);
                //DateTime dtdi = AddBusinessDays(TGL_REQUEST, DaysDI);
                DateTime dtadmcreative = AddBusinessDays(TGL_REQUEST, DaysAdmCreative);
                DateTime dtfinishdesign = AddBusinessDays(TGL_REQUEST, DaysFinishDesign);
                DateTime dtproduction = AddBusinessDays(TGL_REQUEST, DaysProduction);
                DateTime dtsend = AddBusinessDays(TGL_REQUEST, DaysSend);
                TGL_PHOTOGRAPHER = DateTime.Parse(Convert.ToString(dtphotographer)).ToString("yyyy-MM-dd");
                TGL_DI = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                TGL_ADM_CREATIVE = DateTime.Parse(Convert.ToString(dtadmcreative)).ToString("yyyy-MM-dd");
                TGL_FINISH_DESIGN = DateTime.Parse(Convert.ToString(dtfinishdesign)).ToString("yyyy-MM-dd");
                TGL_PRODUCTION = DateTime.Parse(Convert.ToString(dtproduction)).ToString("yyyy-MM-dd");
                TGL_SEND = DateTime.Parse(Convert.ToString(dtsend)).ToString("yyyy-MM-dd");
                text_jadwalfoto.Text = TGL_PHOTOGRAPHER;
                text_jadwaldi.Text = TGL_DI;
                text_jadwaladmcreative.Text = TGL_ADM_CREATIVE;
                text_jadwalselesaidisain.Text = TGL_FINISH_DESIGN;
                text_jadwalproduksi.Text = TGL_PRODUCTION;
                text_jadwalkirim.Text = TGL_SEND;
            }

            if (PRODUCTION == EYesNo.Yes && PHOTOGRAPER == EYesNo.Yes && DIGITAL_IMAGING == EYesNo.Yes)
            {
                DaysPhotoGrapher = 5;
                DaysDI = DaysPhotoGrapher + 2;
                DaysAdmCreative = DaysDI + 1;
                DaysFinishDesign = DaysAdmCreative + 5;
                DaysProduction = DaysFinishDesign + 4;
                DaysSend = DaysProduction + 3;
                //string s = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                DateTime dtphotographer = AddBusinessDays(TGL_REQUEST, DaysPhotoGrapher);
                DateTime dtdi = AddBusinessDays(TGL_REQUEST, DaysDI);
                DateTime dtadmcreative = AddBusinessDays(TGL_REQUEST, DaysAdmCreative);
                DateTime dtfinishdesign = AddBusinessDays(TGL_REQUEST, DaysFinishDesign);
                DateTime dtproduction = AddBusinessDays(TGL_REQUEST, DaysProduction);
                DateTime dtsend = AddBusinessDays(TGL_REQUEST, DaysSend);
                TGL_PHOTOGRAPHER = DateTime.Parse(Convert.ToString(dtphotographer)).ToString("yyyy-MM-dd");
                TGL_DI = DateTime.Parse(Convert.ToString(dtdi)).ToString("yyyy-MM-dd");
                TGL_ADM_CREATIVE = DateTime.Parse(Convert.ToString(dtadmcreative)).ToString("yyyy-MM-dd");
                TGL_FINISH_DESIGN = DateTime.Parse(Convert.ToString(dtfinishdesign)).ToString("yyyy-MM-dd");
                TGL_PRODUCTION = DateTime.Parse(Convert.ToString(dtproduction)).ToString("yyyy-MM-dd");
                TGL_SEND = DateTime.Parse(Convert.ToString(dtsend)).ToString("yyyy-MM-dd");
                text_jadwalfoto.Text = TGL_PHOTOGRAPHER;
                text_jadwaldi.Text = TGL_DI;
                text_jadwaladmcreative.Text = TGL_ADM_CREATIVE;
                text_jadwalselesaidisain.Text = TGL_FINISH_DESIGN;
                text_jadwalproduksi.Text = TGL_PRODUCTION;
                text_jadwalkirim.Text = TGL_SEND;
            }

            if (PRODUCTION == EYesNo.Yes && PHOTOGRAPER == EYesNo.No && DIGITAL_IMAGING == EYesNo.Yes)
            {
                DaysPhotoGrapher = 0;
                DaysDI = DaysPhotoGrapher + 2;
                DaysAdmCreative = DaysDI + 1;
                DaysFinishDesign = DaysAdmCreative + 5;
                DaysProduction = DaysFinishDesign + 4;
                DaysSend = DaysProduction + 3;
                //string s = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                DateTime dtphotographer = AddBusinessDays(TGL_REQUEST, DaysPhotoGrapher);
                DateTime dtdi = AddBusinessDays(TGL_REQUEST, DaysDI);
                DateTime dtadmcreative = AddBusinessDays(TGL_REQUEST, DaysAdmCreative);
                DateTime dtfinishdesign = AddBusinessDays(TGL_REQUEST, DaysFinishDesign);
                DateTime dtproduction = AddBusinessDays(TGL_REQUEST, DaysProduction);
                DateTime dtsend = AddBusinessDays(TGL_REQUEST, DaysSend);
                TGL_PHOTOGRAPHER = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                TGL_DI = DateTime.Parse(Convert.ToString(dtdi)).ToString("yyyy-MM-dd");
                TGL_ADM_CREATIVE = DateTime.Parse(Convert.ToString(dtadmcreative)).ToString("yyyy-MM-dd");
                TGL_FINISH_DESIGN = DateTime.Parse(Convert.ToString(dtfinishdesign)).ToString("yyyy-MM-dd");
                TGL_PRODUCTION = DateTime.Parse(Convert.ToString(dtproduction)).ToString("yyyy-MM-dd");
                TGL_SEND = DateTime.Parse(Convert.ToString(dtsend)).ToString("yyyy-MM-dd");
                text_jadwalfoto.Text = TGL_PHOTOGRAPHER;
                text_jadwaldi.Text = TGL_DI;
                text_jadwaladmcreative.Text = TGL_ADM_CREATIVE;
                text_jadwalselesaidisain.Text = TGL_FINISH_DESIGN;
                text_jadwalproduksi.Text = TGL_PRODUCTION;
                text_jadwalkirim.Text = TGL_SEND;
            }

            if (PRODUCTION == EYesNo.Yes && PHOTOGRAPER == EYesNo.No && DIGITAL_IMAGING == EYesNo.No)
            {
                DaysPhotoGrapher = 0;
                DaysDI = 0;
                DaysAdmCreative = DaysDI + 1;
                DaysFinishDesign = DaysAdmCreative + 5;
                DaysProduction = DaysFinishDesign + 4;
                DaysSend = DaysProduction + 3;
                //string s = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                DateTime dtphotographer = AddBusinessDays(TGL_REQUEST, DaysPhotoGrapher);
                DateTime dtdi = AddBusinessDays(TGL_REQUEST, DaysDI);
                DateTime dtadmcreative = AddBusinessDays(TGL_REQUEST, DaysAdmCreative);
                DateTime dtfinishdesign = AddBusinessDays(TGL_REQUEST, DaysFinishDesign);
                DateTime dtproduction = AddBusinessDays(TGL_REQUEST, DaysProduction);
                DateTime dtsend = AddBusinessDays(TGL_REQUEST, DaysSend);
                TGL_PHOTOGRAPHER = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                TGL_DI = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                TGL_ADM_CREATIVE = DateTime.Parse(Convert.ToString(dtadmcreative)).ToString("yyyy-MM-dd");
                TGL_FINISH_DESIGN = DateTime.Parse(Convert.ToString(dtfinishdesign)).ToString("yyyy-MM-dd");
                TGL_PRODUCTION = DateTime.Parse(Convert.ToString(dtproduction)).ToString("yyyy-MM-dd");
                TGL_SEND = DateTime.Parse(Convert.ToString(dtsend)).ToString("yyyy-MM-dd");
                text_jadwalfoto.Text = TGL_PHOTOGRAPHER;
                text_jadwaldi.Text = TGL_DI;
                text_jadwalselesaidisain.Text = TGL_FINISH_DESIGN;
                text_jadwalproduksi.Text = TGL_PRODUCTION;
                text_jadwalkirim.Text = TGL_SEND;
            }

            //Check Required Date No Production
            if (PRODUCTION == EYesNo.No && PHOTOGRAPER == EYesNo.Yes && DIGITAL_IMAGING == EYesNo.No)
            {
                DaysPhotoGrapher = 5;
                DaysDI = 0;
                DaysAdmCreative = DaysPhotoGrapher + 1;
                DaysFinishDesign = DaysAdmCreative + 5;
                //DaysProduction = DaysFinishDesign + 4;
                //DaysSend = DaysProduction + 3;
                //string s = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                DateTime dtphotographer = AddBusinessDays(TGL_REQUEST, DaysPhotoGrapher);
                //DateTime dtdi = AddBusinessDays(TGL_REQUEST, DaysDI);
                DateTime dtadmcreative = AddBusinessDays(TGL_REQUEST, DaysAdmCreative);
                DateTime dtfinishdesign = AddBusinessDays(TGL_REQUEST, DaysFinishDesign);
                //DateTime dtproduction = AddBusinessDays(TGL_REQUEST, DaysProduction);
                //DateTime dtsend = AddBusinessDays(TGL_REQUEST, DaysSend);
                TGL_PHOTOGRAPHER = DateTime.Parse(Convert.ToString(dtphotographer)).ToString("yyyy-MM-dd");
                TGL_DI = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                TGL_ADM_CREATIVE = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                TGL_FINISH_DESIGN = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                TGL_PRODUCTION = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                TGL_SEND = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                text_jadwalfoto.Text = TGL_PHOTOGRAPHER;
                text_jadwaldi.Text = TGL_DI;
                text_jadwaladmcreative.Text = TGL_ADM_CREATIVE;
                text_jadwalselesaidisain.Text = TGL_FINISH_DESIGN;
                text_jadwalproduksi.Text = TGL_PRODUCTION;
                text_jadwalkirim.Text = TGL_SEND;
            }

            if (PRODUCTION == EYesNo.No && PHOTOGRAPER == EYesNo.Yes && DIGITAL_IMAGING == EYesNo.Yes)
            {
                DaysPhotoGrapher = 5;
                DaysDI = DaysPhotoGrapher + 2;
                DaysAdmCreative = DaysDI + 1;
                DaysFinishDesign = DaysAdmCreative + 5;
                //DaysProduction = DaysFinishDesign + 4;
                //DaysSend = DaysProduction + 3;
                //string s = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                DateTime dtphotographer = AddBusinessDays(TGL_REQUEST, DaysPhotoGrapher);
                DateTime dtdi = AddBusinessDays(TGL_REQUEST, DaysDI);
                DateTime dtadmcreative = AddBusinessDays(TGL_REQUEST, DaysAdmCreative);
                DateTime dtfinishdesign = AddBusinessDays(TGL_REQUEST, DaysFinishDesign);
                //DateTime dtproduction = AddBusinessDays(TGL_REQUEST, DaysProduction);
                //DateTime dtsend = AddBusinessDays(TGL_REQUEST, DaysSend);
                TGL_PHOTOGRAPHER = DateTime.Parse(Convert.ToString(dtphotographer)).ToString("yyyy-MM-dd");
                TGL_DI = DateTime.Parse(Convert.ToString(dtdi)).ToString("yyyy-MM-dd");
                TGL_ADM_CREATIVE = DateTime.Parse(Convert.ToString(dtadmcreative)).ToString("yyyy-MM-dd");
                TGL_FINISH_DESIGN = DateTime.Parse(Convert.ToString(dtfinishdesign)).ToString("yyyy-MM-dd");
                TGL_PRODUCTION = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                TGL_SEND = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                text_jadwalfoto.Text = TGL_PHOTOGRAPHER;
                text_jadwaldi.Text = TGL_DI;
                text_jadwaladmcreative.Text = TGL_ADM_CREATIVE;
                text_jadwalselesaidisain.Text = TGL_FINISH_DESIGN;
                text_jadwalproduksi.Text = TGL_PRODUCTION;
                text_jadwalkirim.Text = TGL_SEND;
            }

            if (PRODUCTION == EYesNo.No && PHOTOGRAPER == EYesNo.No && DIGITAL_IMAGING == EYesNo.Yes)
            {
                DaysPhotoGrapher = 0;
                DaysDI = DaysPhotoGrapher + 2;
                DaysAdmCreative = DaysDI + 1;
                DaysFinishDesign = DaysAdmCreative + 5;
                //DaysProduction = DaysFinishDesign + 4;
                //DaysSend = DaysProduction + 3;
                //string s = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                DateTime dtphotographer = AddBusinessDays(TGL_REQUEST, DaysPhotoGrapher);
                DateTime dtdi = AddBusinessDays(TGL_REQUEST, DaysDI);
                DateTime dtadmcreative = AddBusinessDays(TGL_REQUEST, DaysAdmCreative);
                DateTime dtfinishdesign = AddBusinessDays(TGL_REQUEST, DaysFinishDesign);
                //DateTime dtproduction = AddBusinessDays(TGL_REQUEST, DaysProduction);
                //DateTime dtsend = AddBusinessDays(TGL_REQUEST, DaysSend);
                TGL_PHOTOGRAPHER = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                TGL_DI = DateTime.Parse(Convert.ToString(dtdi)).ToString("yyyy-MM-dd");
                TGL_ADM_CREATIVE = DateTime.Parse(Convert.ToString(dtadmcreative)).ToString("yyyy-MM-dd");
                TGL_FINISH_DESIGN = DateTime.Parse(Convert.ToString(dtfinishdesign)).ToString("yyyy-MM-dd");
                TGL_PRODUCTION = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                TGL_SEND = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                text_jadwalfoto.Text = TGL_PHOTOGRAPHER;
                text_jadwaldi.Text = TGL_DI;
                text_jadwaladmcreative.Text = TGL_ADM_CREATIVE;
                text_jadwalselesaidisain.Text = TGL_FINISH_DESIGN;
                text_jadwalproduksi.Text = TGL_PRODUCTION;
                text_jadwalkirim.Text = TGL_SEND;
            }

            if (PRODUCTION == EYesNo.No && PHOTOGRAPER == EYesNo.No && DIGITAL_IMAGING == EYesNo.No)
            {
                DaysPhotoGrapher = 0;
                DaysDI = 0;
                DaysAdmCreative = DaysDI + 1;
                DaysFinishDesign = DaysAdmCreative + 5;
                //DaysProduction = DaysFinishDesign + 4;
                //DaysSend = DaysProduction + 3;
                //string s = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                DateTime dtphotographer = AddBusinessDays(TGL_REQUEST, DaysPhotoGrapher);
                DateTime dtdi = AddBusinessDays(TGL_REQUEST, DaysDI);
                DateTime dtadmcreative = AddBusinessDays(TGL_REQUEST, DaysAdmCreative);
                DateTime dtfinishdesign = AddBusinessDays(TGL_REQUEST, DaysFinishDesign);
                //DateTime dtproduction = AddBusinessDays(TGL_REQUEST, DaysProduction);
                //DateTime dtsend = AddBusinessDays(TGL_REQUEST, DaysSend);
                TGL_PHOTOGRAPHER = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                TGL_DI = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                TGL_ADM_CREATIVE = DateTime.Parse(Convert.ToString(dtadmcreative)).ToString("yyyy-MM-dd");
                TGL_FINISH_DESIGN = DateTime.Parse(Convert.ToString(dtfinishdesign)).ToString("yyyy-MM-dd");
                TGL_PRODUCTION = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                TGL_SEND = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
                text_jadwalfoto.Text = TGL_PHOTOGRAPHER;
                text_jadwaldi.Text = TGL_DI;
                text_jadwaladmcreative.Text = TGL_ADM_CREATIVE;
                text_jadwalselesaidisain.Text = TGL_FINISH_DESIGN;
                text_jadwalproduksi.Text = TGL_PRODUCTION;
                text_jadwalkirim.Text = TGL_SEND;
            }

        }

        public void ViewOnlyState()
        {
            Pnl_Forms.Enabled = false;
            Pnl_KategoriPermintaan.Enabled = false;
            Pnl_FotograferSelect.Enabled = false;
            Pnl_DigitalImagingSelect.Enabled = false;
            Pnl_FormOthers1.Enabled = false;
            Pnl_FormOthers2.Enabled = false;
            Pnl_JadwalPekerjaan.Enabled = false;
            Pnl_Production.Enabled = false;

        }


    }
}