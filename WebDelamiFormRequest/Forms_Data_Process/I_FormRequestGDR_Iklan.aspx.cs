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

namespace WebDelamiFormRequest.Forms_Data_Process
{
    public partial class I_FormRequestGDR_Iklan : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dtToko = new DataTable();
        public string KODE_FORM = "FRM-0004";

        protected void Page_Load(object sender, EventArgs e)
        {
            string _NO_FORM = Request.QueryString["NO_FORM"];

            if (Page.IsPostBack)
                return;

            if (Session["Username"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (_NO_FORM == null)
            {
                StartFirstTime();
            }
            else
            {
                HfNO_FORM.Value = _NO_FORM;
                HfUsername.Value = Session["Username"].ToString();
                LoadDataFormRequestGDR();
                ChangeButtonColor();
            }

            HfID_DEPT.Value = Convert.ToString(Session["ID_DEPT"].ToString());
            HfUsername.Value = Session["Username"].ToString();

            checkbox_checkall.Attributes.Add("onchange", "Selectall");

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

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("L_FormRequestGDR_Iklan.aspx");
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

        protected void btn_CloseRevise_Click(object sender, EventArgs e)
        {

        }

        protected void linkbtn_filename1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename1.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
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

        protected void linkbtn_filename5_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename5.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename5.Text);
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

        protected void linkbtn_filename6_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename6.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename6.Text);
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

        protected void linkbtn_filename7_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename7.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename7.Text);
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

        protected void linkbtn_filename8_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename8.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename8.Text);
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

        protected void linkbtn_filename9_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename9.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename9.Text);
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

        protected void linkbtn_filename10_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename10.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename10.Text);
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

        protected void linkbtn_filename11_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename11.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename11.Text);
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

        protected void linkbtn_filename12_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename12.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename12.Text);
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

        protected void linkbtn_filename13_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename13.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename13.Text);
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

        protected void linkbtn_filename14_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename14.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename14.Text);
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

        protected void linkbtn_filename15_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename15.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename15.Text);
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

        protected void linkbtn_filename16_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename16.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename16.Text);
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

        protected void linkbtn_filename17_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename17.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename17.Text);
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

        protected void linkbtn_filename18_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename18.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename18.Text);
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

        protected void linkbtn_filename19_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename19.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename19.Text);
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

        protected void linkbtn_filename20_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename20.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename20.Text);
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

        protected void linkbtn_filename21_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename21.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename21.Text);
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

        protected void linkbtn_filename22_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename22.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename22.Text);
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

        protected void linkbtn_filename23_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename23.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename23.Text);
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

        protected void linkbtn_filename24_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename24.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename24.Text);
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

        protected void linkbtn_filename25_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename25.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename25.Text);
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

        protected void linkbtn_filename26_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename26.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename26.Text);
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

        protected void linkbtn_filename27_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename27.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename27.Text);
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

        protected void linkbtn_filename28_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename28.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename28.Text);
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

        protected void linkbtn_filename29_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename29.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename29.Text);
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

        protected void linkbtn_filename30_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename30.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadAds/")
                    + linkbtn_filename30.Text);
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


        #region All Functions

        // Action User
        public void StartFirstTime()
        {
            try
            {
                CreateNoFormGDR();
                label_departmentvalue.Text = Session["DepartemenName"].ToString();
                text_tanggalrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_periodecampaignfrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_periodecampaignto.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_dibuat.Text = Session["Username"].ToString();
                Pnl_Created.Visible = true;
                btn_UpdateSubmit.Visible = false;
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
                TR_FORM4_GDR_DA TrForm4Gdr = new DataLayer.TR_FORM4_GDR_DA();

                Ds = TrForm4Gdr.GetDataByKey(HfNO_FORM.Value);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    text_noform.Text = Convert.ToString(Ds.Tables[0].Rows[0]["NO_FORM"].ToString());
                    text_tanggalrequest.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_REQUEST"].ToString()).ToString("yyyy-MM-dd");
                    if (text_tanggalrequest.Text == "1900-01-01")
                    {
                        text_tanggalrequest.Text = "";
                    }

                    string KODE_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    DsDept = MsDeptDA.GetDataFilter(string.Format("KODE_DEPT = '{0}'", KODE_DEPT));
                    if (DsDept.Tables[0].Rows.Count > 0)
                    {
                        label_departmentvalue.Text = Convert.ToString(DsDept.Tables[0].Rows[0]["DEPT"].ToString());
                    }

                    //text_brand.Text = Convert.ToString(Ds.Tables[0].Rows[0]["BRAND"].ToString());
                    text_periodecampaignfrom.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["PERIODE_CAMPAIGN_FROM"].ToString()).ToString("yyyy-MM-dd");
                    if (text_periodecampaignfrom.Text == "1900-01-01")
                    {
                        text_periodecampaignfrom.Text = "";
                    }

                    text_periodecampaignto.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["PERIODE_CAMPAIGN_TO"].ToString()).ToString("yyyy-MM-dd");
                    if (text_periodecampaignto.Text == "1900-01-01")
                    {
                        text_periodecampaignto.Text = "";
                    }

                    text_budget.Text = Convert.ToString(Convert.ToDecimal(Ds.Tables[0].Rows[0]["BUDGET"]).ToString("#,#0.##"));

                    LoadDataDetailRepairBrand();
                    LoadDataDetailIklan();

                    //Lampiran File Ada 30
                    linkbtn_filename1.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN1"].ToString());
                    linkbtn_filename2.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN2"].ToString());
                    linkbtn_filename3.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN3"].ToString());
                    linkbtn_filename4.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN4"].ToString());

                    linkbtn_filename5.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN5"].ToString());
                    linkbtn_filename6.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN6"].ToString());
                    linkbtn_filename7.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN7"].ToString());
                    linkbtn_filename8.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN8"].ToString());

                    linkbtn_filename9.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN9"].ToString());
                    linkbtn_filename10.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN10"].ToString());
                    linkbtn_filename11.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN11"].ToString());
                    linkbtn_filename12.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN12"].ToString());

                    linkbtn_filename13.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN13"].ToString());
                    linkbtn_filename14.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN14"].ToString());
                    linkbtn_filename15.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN15"].ToString());
                    linkbtn_filename16.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN16"].ToString());

                    linkbtn_filename17.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN17"].ToString());
                    linkbtn_filename18.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN18"].ToString());
                    linkbtn_filename19.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN19"].ToString());
                    linkbtn_filename20.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN20"].ToString());

                    linkbtn_filename21.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN21"].ToString());
                    linkbtn_filename22.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN22"].ToString());
                    linkbtn_filename23.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN23"].ToString());
                    linkbtn_filename24.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN24"].ToString());
                    linkbtn_filename25.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN25"].ToString());
                    linkbtn_filename26.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN26"].ToString());
                    linkbtn_filename27.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN27"].ToString());
                    linkbtn_filename28.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN28"].ToString());

                    linkbtn_filename29.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN29"].ToString());
                    linkbtn_filename30.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN30"].ToString());
              

                    text_campaign.Text = Convert.ToString(Ds.Tables[0].Rows[0]["CAMPAIGN"].ToString());
                    text_caption.Text = Convert.ToString(Ds.Tables[0].Rows[0]["CAPTION"].ToString());
                    text_targetlokasi.Text = Convert.ToString(Ds.Tables[0].Rows[0]["TARGET_LOKASI"].ToString());
                    text_umur.Text = Convert.ToString(Ds.Tables[0].Rows[0]["UMUR"].ToString());
                    ddljeniskelamin.Text = Convert.ToString(Ds.Tables[0].Rows[0]["JENIS_KELAMIN"].ToString());
                    text_interestminat.Text = Convert.ToString(Ds.Tables[0].Rows[0]["INTEREST_MINAT"].ToString());
                    text_informasitambahan.Text = Convert.ToString(Ds.Tables[0].Rows[0]["INFORMASI_TAMBAHAN"].ToString());

                    text_dibuat.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DIBUAT"].ToString());
                    text_tanggaldibuat.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DIBUAT"].ToString()).ToString("yyyy-MM-dd");
                    if (text_tanggaldibuat.Text == "1900-01-01")
                    {
                        text_tanggaldibuat.Text = "";
                    }
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

                    label_statusvalue.Text = Convert.ToString(Ds.Tables[0].Rows[0]["STATUS"].ToString());
                    text_revisiload.Text = Convert.ToString(Ds.Tables[0].Rows[0]["REVISI"].ToString());
                    string Status = Convert.ToString(Ds.Tables[0].Rows[0]["STATUS"].ToString());

                    //Mendapatkan Urutan User Berdasarkan Jabatan
                    Ds = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);

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

                            if (Status == EApprovalStatus.OnApprovedHD)
                            {
                                btn_Save.Visible = false;
                                btn_UpdateSubmit.Enabled = false;
                                btn_Reject.Enabled = false;
                                btn_Cancel.Enabled = true;
                            }
                            else if (Status == EApprovalStatus.OnRevise)
                            {
                                Pnl_Forms.Enabled = true;
                                Pnl_Others1.Enabled = true;
                                Pnl_RevisiLoad.Visible = true;
                                Pnl_RevisiLoad.Enabled = false;
                                btn_Save.Visible = false;
                                btn_UpdateSubmit.Enabled = true;
                                btn_Reject.Visible = true;
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
                            else if (Status == EApprovalStatus.ApprovedGMMarkom)
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
                            text_diterima1.Text = HfUsername.Value;
                            ShowButtonUrutan3();
                            if (Status == EApprovalStatus.ApprovedGMMarkom)
                            {
                                btn_Accepted.Enabled = true;
                                btn_ToRevise.Enabled = true;
                                btn_Revise.Enabled = true;
                                btn_Reject.Enabled = true;
                            }

                            else if (Status == EApprovalStatus.OnRevise)
                            {
                                Pnl_RevisiLoad.Visible = true;
                                Pnl_RevisiLoad.Enabled = false;
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
                TR_FORM4_GDR_DA trform4gdr = new DataLayer.TR_FORM4_GDR_DA();

                DataSet DsForm = new DataSet();
                MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                string FORM_TYPE = "";
                DsForm = msformda.GetDataByKey(KODE_FORM);
                if (DsForm.Tables[0].Rows.Count > 0)
                {
                    FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                }

                Ds = trform4gdr.GetDataFilter(where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    string noform = Ds.Tables[0].Rows[0]["NO_FORM"].ToString();
                    int noformangka = Convert.ToInt32(noform.Substring(noform.Length - 4));
                    decimal angkaakhir = Convert.ToDecimal(noformangka) + 1;
                    NO_FORM = "FORM-" + FORM_TYPE + "-" + angkaakhir.ToString("0000");
                    text_noform.Text = NO_FORM;
                    label_statusvalue.Text = "New Data";
                }
                else
                {
                    NO_FORM = "FORM-" + FORM_TYPE + "-0001";
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

        public void SaveFormRequestGDR()
        {
            try
            {
                TR_FORM4_GDR_DA trform4gdrda = new DataLayer.TR_FORM4_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string KODE_FORM = "FRM-0004";
                string ID_DEPT = Convert.ToString(Session["ID_DEPT"].ToString());
                DateTime? TGL_REQUEST = Convert.ToDateTime(text_tanggalrequest.Text);
                string BRAND = "";
                DateTime? PERIODE_CAMPAIGN_FROM = Convert.ToDateTime(text_periodecampaignfrom.Text);
                DateTime? PERIODE_CAMPAIGN_TO = Convert.ToDateTime(text_periodecampaignto.Text);
                string BUDGET = Convert.ToString(text_budget.Text);
                if (BUDGET != "")
                {
                    BUDGET = Convert.ToDecimal(text_budget.Text).ToString();
                }
                else
                {
                    BUDGET = "0";
                }
                string RFR_LAMPIRAN1 = "";
                string RFR_LAMPIRAN2 = "";
                string RFR_LAMPIRAN3 = "";
                string RFR_LAMPIRAN4 = "";
                string RFR_LAMPIRAN5 = "";
                string RFR_LAMPIRAN6 = "";
                string RFR_LAMPIRAN7 = "";
                string RFR_LAMPIRAN8 = "";
                string RFR_LAMPIRAN9 = "";
                string RFR_LAMPIRAN10 = "";
                string RFR_LAMPIRAN11 = "";
                string RFR_LAMPIRAN12 = "";
                string RFR_LAMPIRAN13 = "";
                string RFR_LAMPIRAN14 = "";
                string RFR_LAMPIRAN15 = "";
                string RFR_LAMPIRAN16 = "";
                string RFR_LAMPIRAN17 = "";
                string RFR_LAMPIRAN18 = "";
                string RFR_LAMPIRAN19 = "";
                string RFR_LAMPIRAN20 = "";
                string RFR_LAMPIRAN21 = "";
                string RFR_LAMPIRAN22 = "";
                string RFR_LAMPIRAN23 = "";
                string RFR_LAMPIRAN24 = "";
                string RFR_LAMPIRAN25 = "";
                string RFR_LAMPIRAN26 = "";
                string RFR_LAMPIRAN27 = "";
                string RFR_LAMPIRAN28 = "";
                string RFR_LAMPIRAN29 = "";
                string RFR_LAMPIRAN30 = "";
                string CAMPAIGN = text_campaign.Text;
                string CAPTION = text_caption.Text;
                string TARGET_LOKASI = text_targetlokasi.Text;
                string UMUR = text_umur.Text;
                string JENIS_KELAMIN = ddljeniskelamin.Text;
                string INTEREST_MINAT = text_interestminat.Text;
                string INFORMASI_TAMBAHAN = text_informasitambahan.Text;
                string DIBUAT = text_dibuat.Text;
                DateTime TGL_DIBUAT = DateTime.Now;
                DateTime startdate = new DateTime(1900, 01, 01);
                DateTime temp;

                if (checkbox_colorbox.Checked == true || checkbox_adidas.Checked == true || checkbox_lecoq.Checked == true || checkbox_jockey.Checked == true || checkbox_tirajeans.Checked == true || checkbox_executive.Checked == true || checkbox_wood.Checked == true || checkbox_wrangler.Checked == true || checkbox_lee.Checked == true || checkbox_etcetera.Checked == true || checkbox_checkall.Checked == true)
                { 
                    if (BUDGET != "0")
                    {
                        //Check List Jenis iklan Empty Or No
                        if (radio_facebookinstagramadsjenis1.Checked == true || checkbox_sponsorjenis1.Checked == true || checkbox_trafficjenis1.Checked == true || checkbox_videojenis1.Checked == true || checkbox_conversionjenis1.Checked == true || checkbox_brandawarenesjenis1.Checked == true || checkbox_boostpostjenis1.Checked == true || checkbox_engagementjenis1.Checked == true || checkbox_leadjenis1.Checked == true || checkbox_reachjenis1.Checked == true || checkbox_otherjenis1.Checked == true || radio_googlejenis2.Checked == true || checkbox_youtubeadsjenis2.Checked == true || checkbox_trafficjenis1.Checked == true || checkbox_productbrandsjenis2.Checked == true || checkbox_googledisplaynetworkjenis2.Checked == true || checkbox_googleadwordsjenis2.Checked == true)
                        {

                            TR_FORM4_GDR trform4gdr = new TR_FORM4_GDR();
                            trform4gdr.NO_FORM = NO_FORM;
                            trform4gdr.KODE_FORM = KODE_FORM;
                            trform4gdr.TGL_REQUEST = TGL_REQUEST;
                            trform4gdr.ID_DEPT = ID_DEPT;
                            //trform4gdr.BRAND = BRAND;
                            trform4gdr.PERIODE_CAMPAIGN_FROM = PERIODE_CAMPAIGN_FROM;
                            trform4gdr.PERIODE_CAMPAIGN_TO = PERIODE_CAMPAIGN_TO;
                            trform4gdr.BUDGET = Convert.ToDecimal(BUDGET);

                            if (btn_uploadfile1.HasFile)
                            {
                                int imgSize = btn_uploadfile1.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile1.PostedFile.FileName).ToLower();
                                if (btn_uploadfile1.PostedFile != null && btn_uploadfile1.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile1.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 1 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN1 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "1" + "-" + btn_uploadfile1.FileName;
                                        btn_uploadfile1.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN1);
                                    }
                                }
                            }

                            trform4gdr.RFR_LAMPIRAN1 = RFR_LAMPIRAN1;
                            if (btn_uploadfile2.HasFile)
                            {
                                int imgSize = btn_uploadfile2.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile2.PostedFile.FileName).ToLower();
                                if (btn_uploadfile2.PostedFile != null && btn_uploadfile2.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile2.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 2 Lebih Besar Dari 3 MB.";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                    else
                                    {
                                        RFR_LAMPIRAN2 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "2" + "-" + btn_uploadfile2.FileName;
                                        btn_uploadfile2.PostedFile
                                            .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN2);
                                    }

                                }
                            }
                            trform4gdr.RFR_LAMPIRAN2 = RFR_LAMPIRAN2;
                            if (btn_uploadfile3.HasFile)
                            {
                                int imgSize = btn_uploadfile3.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile3.PostedFile.FileName).ToLower();
                                if (btn_uploadfile3.PostedFile != null && btn_uploadfile3.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile3.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 3 Lebih Besar Dari 3 MB.";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                    else
                                    {
                                        RFR_LAMPIRAN3 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "3" + "-" + btn_uploadfile3.FileName;
                                        btn_uploadfile3.PostedFile
                                            .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN3);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN3 = RFR_LAMPIRAN3;
                            if (btn_uploadfile4.HasFile)
                            {
                                int imgSize = btn_uploadfile4.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile4.PostedFile.FileName).ToLower();
                                if (btn_uploadfile4.PostedFile != null && btn_uploadfile4.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile4.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 4 Lebih Besar Dari 3 MB.";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                    else
                                    {
                                        RFR_LAMPIRAN4 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "4" + "-" + btn_uploadfile4.FileName;
                                        btn_uploadfile4.PostedFile
                                            .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN4);
                                    }
                                }

                            }
                            trform4gdr.RFR_LAMPIRAN4 = RFR_LAMPIRAN4;

                            if (btn_uploadfile5.HasFile)
                            {
                                int imgSize = btn_uploadfile5.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile5.PostedFile.FileName).ToLower();
                                if (btn_uploadfile5.PostedFile != null && btn_uploadfile5.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile5.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 5 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN5 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "5" + "-" + btn_uploadfile5.FileName;
                                        btn_uploadfile5.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN5);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN5 = RFR_LAMPIRAN5;


                            if (btn_uploadfile6.HasFile)
                            {
                                int imgSize = btn_uploadfile6.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile6.PostedFile.FileName).ToLower();
                                if (btn_uploadfile6.PostedFile != null && btn_uploadfile6.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile6.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 6 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN6 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "6" + "-" + btn_uploadfile6.FileName;
                                        btn_uploadfile6.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN6);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN6 = RFR_LAMPIRAN6;


                            if (btn_uploadfile7.HasFile)
                            {
                                int imgSize = btn_uploadfile7.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile7.PostedFile.FileName).ToLower();
                                if (btn_uploadfile7.PostedFile != null && btn_uploadfile7.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile7.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 7 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN7 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "7" + "-" + btn_uploadfile7.FileName;
                                        btn_uploadfile7.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN7);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN7 = RFR_LAMPIRAN7;


                            if (btn_uploadfile8.HasFile)
                            {
                                int imgSize = btn_uploadfile8.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile8.PostedFile.FileName).ToLower();
                                if (btn_uploadfile8.PostedFile != null && btn_uploadfile8.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile8.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 8 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN8 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "8" + "-" + btn_uploadfile8.FileName;
                                        btn_uploadfile8.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN8);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN8 = RFR_LAMPIRAN8;


                            if (btn_uploadfile9.HasFile)
                            {
                                int imgSize = btn_uploadfile9.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile9.PostedFile.FileName).ToLower();
                                if (btn_uploadfile9.PostedFile != null && btn_uploadfile9.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile9.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 9 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN9 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "9" + "-" + btn_uploadfile9.FileName;
                                        btn_uploadfile9.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN9);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN9 = RFR_LAMPIRAN9;


                            if (btn_uploadfile10.HasFile)
                            {
                                int imgSize = btn_uploadfile10.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile10.PostedFile.FileName).ToLower();
                                if (btn_uploadfile10.PostedFile != null && btn_uploadfile10.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile10.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 10 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN10 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "10" + "-" + btn_uploadfile10.FileName;
                                        btn_uploadfile10.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN10);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN10 = RFR_LAMPIRAN10;


                            if (btn_uploadfile11.HasFile)
                            {
                                int imgSize = btn_uploadfile11.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile11.PostedFile.FileName).ToLower();
                                if (btn_uploadfile11.PostedFile != null && btn_uploadfile11.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile11.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 11 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN11 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "11" + "-" + btn_uploadfile11.FileName;
                                        btn_uploadfile11.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN11);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN11 = RFR_LAMPIRAN11;


                            if (btn_uploadfile12.HasFile)
                            {
                                int imgSize = btn_uploadfile12.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile12.PostedFile.FileName).ToLower();
                                if (btn_uploadfile12.PostedFile != null && btn_uploadfile12.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile12.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 12 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN12 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "12" + "-" + btn_uploadfile12.FileName;
                                        btn_uploadfile12.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN12);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN12 = RFR_LAMPIRAN12;


                            if (btn_uploadfile13.HasFile)
                            {
                                int imgSize = btn_uploadfile13.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile13.PostedFile.FileName).ToLower();
                                if (btn_uploadfile13.PostedFile != null && btn_uploadfile13.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile13.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 13 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN13 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "13" + "-" + btn_uploadfile13.FileName;
                                        btn_uploadfile13.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN13);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN13 = RFR_LAMPIRAN13;


                            if (btn_uploadfile14.HasFile)
                            {
                                int imgSize = btn_uploadfile14.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile14.PostedFile.FileName).ToLower();
                                if (btn_uploadfile14.PostedFile != null && btn_uploadfile14.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile14.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 14 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN14 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "14" + "-" + btn_uploadfile14.FileName;
                                        btn_uploadfile14.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN14);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN14 = RFR_LAMPIRAN14;



                            if (btn_uploadfile15.HasFile)
                            {
                                int imgSize = btn_uploadfile15.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile15.PostedFile.FileName).ToLower();
                                if (btn_uploadfile15.PostedFile != null && btn_uploadfile15.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile15.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 15 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN15 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "15" + "-" + btn_uploadfile15.FileName;
                                        btn_uploadfile15.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN15);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN15 = RFR_LAMPIRAN15;


                            if (btn_uploadfile16.HasFile)
                            {
                                int imgSize = btn_uploadfile16.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile16.PostedFile.FileName).ToLower();
                                if (btn_uploadfile16.PostedFile != null && btn_uploadfile16.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile16.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 16 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN16 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "16" + "-" + btn_uploadfile16.FileName;
                                        btn_uploadfile16.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN16);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN16 = RFR_LAMPIRAN16;


                            if (btn_uploadfile17.HasFile)
                            {
                                int imgSize = btn_uploadfile17.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile17.PostedFile.FileName).ToLower();
                                if (btn_uploadfile17.PostedFile != null && btn_uploadfile17.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile17.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 17 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN17 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "17" + "-" + btn_uploadfile17.FileName;
                                        btn_uploadfile17.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN17);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN17 = RFR_LAMPIRAN17;


                            if (btn_uploadfile18.HasFile)
                            {
                                int imgSize = btn_uploadfile18.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile18.PostedFile.FileName).ToLower();
                                if (btn_uploadfile18.PostedFile != null && btn_uploadfile18.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile18.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 18 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN18 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "18" + "-" + btn_uploadfile18.FileName;
                                        btn_uploadfile18.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN18);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN18 = RFR_LAMPIRAN18;


                            if (btn_uploadfile19.HasFile)
                            {
                                int imgSize = btn_uploadfile19.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile19.PostedFile.FileName).ToLower();
                                if (btn_uploadfile19.PostedFile != null && btn_uploadfile19.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile19.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 19 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN19 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "19" + "-" + btn_uploadfile19.FileName;
                                        btn_uploadfile19.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN19);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN19 = RFR_LAMPIRAN19;


                            if (btn_uploadfile20.HasFile)
                            {
                                int imgSize = btn_uploadfile20.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile20.PostedFile.FileName).ToLower();
                                if (btn_uploadfile20.PostedFile != null && btn_uploadfile20.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile20.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 20 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN20 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "20" + "-" + btn_uploadfile20.FileName;
                                        btn_uploadfile20.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN20);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN20 = RFR_LAMPIRAN20;


                            if (btn_uploadfile21.HasFile)
                            {
                                int imgSize = btn_uploadfile21.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile21.PostedFile.FileName).ToLower();
                                if (btn_uploadfile21.PostedFile != null && btn_uploadfile21.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile21.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 21 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN21 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "21" + "-" + btn_uploadfile21.FileName;
                                        btn_uploadfile21.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN21);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN21 = RFR_LAMPIRAN21;


                            if (btn_uploadfile22.HasFile)
                            {
                                int imgSize = btn_uploadfile22.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile22.PostedFile.FileName).ToLower();
                                if (btn_uploadfile22.PostedFile != null && btn_uploadfile22.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile22.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 22 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN22 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "22" + "-" + btn_uploadfile22.FileName;
                                        btn_uploadfile22.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN22);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN22 = RFR_LAMPIRAN22;


                            if (btn_uploadfile23.HasFile)
                            {
                                int imgSize = btn_uploadfile23.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile23.PostedFile.FileName).ToLower();
                                if (btn_uploadfile23.PostedFile != null && btn_uploadfile23.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile23.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 23 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN23 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "23" + "-" + btn_uploadfile23.FileName;
                                        btn_uploadfile23.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN23);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN23 = RFR_LAMPIRAN23;


                            if (btn_uploadfile24.HasFile)
                            {
                                int imgSize = btn_uploadfile24.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile24.PostedFile.FileName).ToLower();
                                if (btn_uploadfile24.PostedFile != null && btn_uploadfile24.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile24.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 24 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN24 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "24" + "-" + btn_uploadfile24.FileName;
                                        btn_uploadfile24.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN24);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN24 = RFR_LAMPIRAN24;


                            if (btn_uploadfile25.HasFile)
                            {
                                int imgSize = btn_uploadfile25.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile25.PostedFile.FileName).ToLower();
                                if (btn_uploadfile25.PostedFile != null && btn_uploadfile25.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile25.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 25 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN25 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "25" + "-" + btn_uploadfile25.FileName;
                                        btn_uploadfile25.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN25);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN25 = RFR_LAMPIRAN25;


                            if (btn_uploadfile26.HasFile)
                            {
                                int imgSize = btn_uploadfile26.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile26.PostedFile.FileName).ToLower();
                                if (btn_uploadfile26.PostedFile != null && btn_uploadfile26.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile26.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 26 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN26 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "26" + "-" + btn_uploadfile26.FileName;
                                        btn_uploadfile26.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN26);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN26 = RFR_LAMPIRAN26;


                            if (btn_uploadfile27.HasFile)
                            {
                                int imgSize = btn_uploadfile27.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile27.PostedFile.FileName).ToLower();
                                if (btn_uploadfile27.PostedFile != null && btn_uploadfile27.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile27.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 27 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN27 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "27" + "-" + btn_uploadfile27.FileName;
                                        btn_uploadfile27.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN27);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN27 = RFR_LAMPIRAN27;


                            if (btn_uploadfile28.HasFile)
                            {
                                int imgSize = btn_uploadfile28.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile28.PostedFile.FileName).ToLower();
                                if (btn_uploadfile28.PostedFile != null && btn_uploadfile28.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile28.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 28 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN28 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "28" + "-" + btn_uploadfile28.FileName;
                                        btn_uploadfile28.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN28);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN28 = RFR_LAMPIRAN28;


                            if (btn_uploadfile29.HasFile)
                            {
                                int imgSize = btn_uploadfile29.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile29.PostedFile.FileName).ToLower();
                                if (btn_uploadfile29.PostedFile != null && btn_uploadfile29.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile29.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 29 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN29 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "29" + "-" + btn_uploadfile29.FileName;
                                        btn_uploadfile29.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN29);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN29 = RFR_LAMPIRAN29;


                            if (btn_uploadfile30.HasFile)
                            {
                                int imgSize = btn_uploadfile30.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile30.PostedFile.FileName).ToLower();
                                if (btn_uploadfile30.PostedFile != null && btn_uploadfile30.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile30.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 30 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN30 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "30" + "-" + btn_uploadfile30.FileName;
                                        btn_uploadfile30.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN30);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN30 = RFR_LAMPIRAN30;

                            trform4gdr.CAMPAIGN = CAMPAIGN;
                            trform4gdr.CAPTION = CAPTION;
                            trform4gdr.TARGET_LOKASI = TARGET_LOKASI;
                            trform4gdr.UMUR = UMUR;
                            trform4gdr.JENIS_KELAMIN = JENIS_KELAMIN;
                            trform4gdr.INTEREST_MINAT = INTEREST_MINAT;
                            trform4gdr.INFORMASI_TAMBAHAN = INFORMASI_TAMBAHAN;
                            trform4gdr.DIBUAT = DIBUAT;
                            trform4gdr.TGL_DIBUAT = TGL_DIBUAT;
                            trform4gdr.MENYETUJUI1 = "";
                            trform4gdr.TGL_MENYETUJUI1 = startdate;
                            trform4gdr.DITERIMA_1 = "";
                            trform4gdr.TGL_DITERIMA_1 = startdate;
                            trform4gdr.STATUS = EApprovalStatus.OnApprovedHD;
                            trform4gdr.REVISI = "";

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

                            trform4gdr.USER_CURRENT = DIBUAT;
                            trform4gdr.NEXT_USER = USERNEXT;
                            trform4gdr.URUTAN_USER_CURRENT = URUTAN;
                            trform4gdr.URUTAN_NEXT_USER = URUTAN_NEXT;

                            trform4gdrda.Insert(trform4gdr);

                            SaveDetailRepairBrand();
                            SaveDetailIklan();
                            SendEmailAllType();

                            TR_FORM_GDR_ACTIVITY_DA trformgdrActivityDa = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                            DataSet DsFormActivity = new DataSet();

                            TR_FORM_GDR_ACTIVITY trformgdractivity = new TR_FORM_GDR_ACTIVITY();
                            trformgdractivity.USERNAME = HfUsername.Value;
                            trformgdractivity.ACTIVITY_TIME = DateTime.Now;
                            trformgdractivity.KODE_FORM = "FRM-0004";
                            trformgdractivity.NO_FORM = NO_FORM;
                            trformgdractivity.STATUS = EApprovalStatus.OnApprovedHD;
                            trformgdractivity.DESCRIPTION = "Insert New Data. Status To " + EApprovalStatus.ApprovedGMMarkom;
                            trformgdractivity.REVISION = "-";
                            trformgdractivity.URUTAN = 1;
                            trformgdractivity.SP = "*";
                            trformgdractivity.USER_CURRENT = DIBUAT;
                            trformgdractivity.NEXT_USER = USERNEXT;
                            trformgdractivity.URUTAN_USER_CURRENT = URUTAN;
                            trformgdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                            trformgdrActivityDa.Insert(trformgdractivity);


                            DivMessage.InnerText = "Data Successfully Save";
                            DivMessage.Style.Add("color", "black");
                            DivMessage.Style.Add("background-color", "skyblue");
                            DivMessage.Attributes["class"] = "error";
                            //DivMessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;

                            string HomePageUrl = "../Forms_Data_Process/L_FormRequestGDR_Iklan.aspx";
                            Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                            //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                        }

                        else
                        {
                            DivMessage.InnerText = "'Type Of Request' must be filled ";
                            DivMessage.Attributes["class"] = "error";
                            //DivMessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;
                        }
                    }
                    else
                    {
                        DivMessage.InnerText = "Budget Cannot Be Empty";
                        DivMessage.Attributes["class"] = "error";
                        //DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                    }

                }
                else
                {
                    DivMessage.InnerText = "Brand Cannot Be Empty";
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
                TR_FORM4_GDR_DA trform4gdrda = new DataLayer.TR_FORM4_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string KODE_FORM = "FRM-0004";
                //string ID_DEPT = Convert.ToString(Session["ID_DEPT"].ToString());
                DateTime? TGL_REQUEST = Convert.ToDateTime(text_tanggalrequest.Text);
                string BRAND = "";
                DateTime? PERIODE_CAMPAIGN_FROM = Convert.ToDateTime(text_periodecampaignfrom.Text);
                DateTime? PERIODE_CAMPAIGN_TO = Convert.ToDateTime(text_periodecampaignto.Text);
                string BUDGET = Convert.ToString(text_budget.Text);
                if (BUDGET != "")
                {
                    BUDGET = Convert.ToDecimal(text_budget.Text).ToString();
                }
                else
                {
                    BUDGET = "0";
                }
                string RFR_LAMPIRAN1 = "";
                string RFR_LAMPIRAN2 = "";
                string RFR_LAMPIRAN3 = "";
                string RFR_LAMPIRAN4 = "";
                string RFR_LAMPIRAN5 = "";
                string RFR_LAMPIRAN6 = "";
                string RFR_LAMPIRAN7 = "";
                string RFR_LAMPIRAN8 = "";
                string RFR_LAMPIRAN9 = "";
                string RFR_LAMPIRAN10 = "";
                string RFR_LAMPIRAN11 = "";
                string RFR_LAMPIRAN12 = "";
                string RFR_LAMPIRAN13 = "";
                string RFR_LAMPIRAN14 = "";
                string RFR_LAMPIRAN15 = "";
                string RFR_LAMPIRAN16 = "";
                string RFR_LAMPIRAN17 = "";
                string RFR_LAMPIRAN18 = "";
                string RFR_LAMPIRAN19 = "";
                string RFR_LAMPIRAN20 = "";
                string RFR_LAMPIRAN21 = "";
                string RFR_LAMPIRAN22 = "";
                string RFR_LAMPIRAN23 = "";
                string RFR_LAMPIRAN24 = "";
                string RFR_LAMPIRAN25 = "";
                string RFR_LAMPIRAN26 = "";
                string RFR_LAMPIRAN27 = "";
                string RFR_LAMPIRAN28 = "";
                string RFR_LAMPIRAN29 = "";
                string RFR_LAMPIRAN30 = "";
                string CAMPAIGN = text_campaign.Text;
                string CAPTION = text_caption.Text;
                string TARGET_LOKASI = text_targetlokasi.Text;
                string UMUR = text_umur.Text;
                string JENIS_KELAMIN = ddljeniskelamin.Text;
                string INTEREST_MINAT = text_interestminat.Text;
                string INFORMASI_TAMBAHAN = text_informasitambahan.Text;
                string DIBUAT = text_dibuat.Text;
                DateTime TGL_DIBUAT = DateTime.Now;
                DateTime startdate = new DateTime(1900, 01, 01);
                DateTime temp;

                if (checkbox_colorbox.Checked == true || checkbox_adidas.Checked == true || checkbox_lecoq.Checked == true || checkbox_jockey.Checked == true || checkbox_tirajeans.Checked == true || checkbox_executive.Checked == true || checkbox_wood.Checked == true || checkbox_wrangler.Checked == true || checkbox_lee.Checked == true || checkbox_etcetera.Checked == true || checkbox_checkall.Checked == true)
                {
                    if (BUDGET != "0")
                    {
                        //Check List Jenis iklan Empty Or No
                        if (radio_facebookinstagramadsjenis1.Checked == true || checkbox_sponsorjenis1.Checked == true || checkbox_trafficjenis1.Checked == true || checkbox_videojenis1.Checked == true || checkbox_conversionjenis1.Checked == true || checkbox_brandawarenesjenis1.Checked == true || checkbox_boostpostjenis1.Checked == true || checkbox_engagementjenis1.Checked == true || checkbox_leadjenis1.Checked == true || checkbox_reachjenis1.Checked == true || checkbox_otherjenis1.Checked == true || radio_googlejenis2.Checked == true || checkbox_youtubeadsjenis2.Checked == true || checkbox_trafficjenis1.Checked == true || checkbox_productbrandsjenis2.Checked == true || checkbox_googledisplaynetworkjenis2.Checked == true || checkbox_googleadwordsjenis2.Checked == true)
                        {

                            TR_FORM4_GDR trform4gdr = new TR_FORM4_GDR();
                            trform4gdr.NO_FORM = NO_FORM;
                            trform4gdr.KODE_FORM = KODE_FORM;
                            trform4gdr.TGL_REQUEST = TGL_REQUEST;
                            //trform4gdr.ID_DEPT = ID_DEPT;
                            //trform4gdr.BRAND = BRAND;
                            trform4gdr.PERIODE_CAMPAIGN_FROM = PERIODE_CAMPAIGN_FROM;
                            trform4gdr.PERIODE_CAMPAIGN_TO = PERIODE_CAMPAIGN_TO;
                            trform4gdr.BUDGET = Convert.ToDecimal(BUDGET);

                            if (btn_uploadfile1.HasFile)
                            {
                                int imgSize = btn_uploadfile1.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile1.PostedFile.FileName).ToLower();
                                if (btn_uploadfile1.PostedFile != null && btn_uploadfile1.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile1.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 1 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN1 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "1" + "-" + btn_uploadfile1.FileName;
                                        btn_uploadfile1.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN1);
                                    }
                                }
                            }

                            trform4gdr.RFR_LAMPIRAN1 = RFR_LAMPIRAN1;
                            if (btn_uploadfile2.HasFile)
                            {
                                int imgSize = btn_uploadfile2.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile2.PostedFile.FileName).ToLower();
                                if (btn_uploadfile2.PostedFile != null && btn_uploadfile2.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile2.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 2 Lebih Besar Dari 3 MB.";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                    else
                                    {
                                        RFR_LAMPIRAN2 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "2" + "-" + btn_uploadfile2.FileName;
                                        btn_uploadfile2.PostedFile
                                            .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN2);
                                    }

                                }
                            }
                            trform4gdr.RFR_LAMPIRAN2 = RFR_LAMPIRAN2;
                            if (btn_uploadfile3.HasFile)
                            {
                                int imgSize = btn_uploadfile3.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile3.PostedFile.FileName).ToLower();
                                if (btn_uploadfile3.PostedFile != null && btn_uploadfile3.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile3.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 3 Lebih Besar Dari 3 MB.";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                    else
                                    {
                                        RFR_LAMPIRAN3 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "3" + "-" + btn_uploadfile3.FileName;
                                        btn_uploadfile3.PostedFile
                                            .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN3);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN3 = RFR_LAMPIRAN3;
                            if (btn_uploadfile4.HasFile)
                            {
                                int imgSize = btn_uploadfile4.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile4.PostedFile.FileName).ToLower();
                                if (btn_uploadfile4.PostedFile != null && btn_uploadfile4.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile4.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 4 Lebih Besar Dari 3 MB.";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                    else
                                    {
                                        RFR_LAMPIRAN4 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "4" + "-" + btn_uploadfile4.FileName;
                                        btn_uploadfile4.PostedFile
                                            .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN4);
                                    }
                                }

                            }
                            trform4gdr.RFR_LAMPIRAN4 = RFR_LAMPIRAN4;

                            if (btn_uploadfile5.HasFile)
                            {
                                int imgSize = btn_uploadfile5.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile5.PostedFile.FileName).ToLower();
                                if (btn_uploadfile5.PostedFile != null && btn_uploadfile5.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile5.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 5 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN5 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "5" + "-" + btn_uploadfile5.FileName;
                                        btn_uploadfile5.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN5);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN5 = RFR_LAMPIRAN5;


                            if (btn_uploadfile6.HasFile)
                            {
                                int imgSize = btn_uploadfile6.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile6.PostedFile.FileName).ToLower();
                                if (btn_uploadfile6.PostedFile != null && btn_uploadfile6.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile6.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 6 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN6 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "6" + "-" + btn_uploadfile6.FileName;
                                        btn_uploadfile6.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN6);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN6 = RFR_LAMPIRAN6;


                            if (btn_uploadfile7.HasFile)
                            {
                                int imgSize = btn_uploadfile7.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile7.PostedFile.FileName).ToLower();
                                if (btn_uploadfile7.PostedFile != null && btn_uploadfile7.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile7.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 7 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN7 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "7" + "-" + btn_uploadfile7.FileName;
                                        btn_uploadfile7.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN7);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN7 = RFR_LAMPIRAN7;


                            if (btn_uploadfile8.HasFile)
                            {
                                int imgSize = btn_uploadfile8.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile8.PostedFile.FileName).ToLower();
                                if (btn_uploadfile8.PostedFile != null && btn_uploadfile8.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile8.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 8 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN8 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "8" + "-" + btn_uploadfile8.FileName;
                                        btn_uploadfile8.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN8);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN8 = RFR_LAMPIRAN8;


                            if (btn_uploadfile9.HasFile)
                            {
                                int imgSize = btn_uploadfile9.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile9.PostedFile.FileName).ToLower();
                                if (btn_uploadfile9.PostedFile != null && btn_uploadfile9.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile9.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 9 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN9 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "9" + "-" + btn_uploadfile9.FileName;
                                        btn_uploadfile9.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN9);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN9 = RFR_LAMPIRAN9;


                            if (btn_uploadfile10.HasFile)
                            {
                                int imgSize = btn_uploadfile10.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile10.PostedFile.FileName).ToLower();
                                if (btn_uploadfile10.PostedFile != null && btn_uploadfile10.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile10.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 10 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN10 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "10" + "-" + btn_uploadfile10.FileName;
                                        btn_uploadfile10.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN10);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN10 = RFR_LAMPIRAN10;


                            if (btn_uploadfile11.HasFile)
                            {
                                int imgSize = btn_uploadfile11.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile11.PostedFile.FileName).ToLower();
                                if (btn_uploadfile11.PostedFile != null && btn_uploadfile11.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile11.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 11 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN11 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "11" + "-" + btn_uploadfile11.FileName;
                                        btn_uploadfile11.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN11);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN11 = RFR_LAMPIRAN11;


                            if (btn_uploadfile12.HasFile)
                            {
                                int imgSize = btn_uploadfile12.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile12.PostedFile.FileName).ToLower();
                                if (btn_uploadfile12.PostedFile != null && btn_uploadfile12.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile12.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 12 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN12 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "12" + "-" + btn_uploadfile12.FileName;
                                        btn_uploadfile12.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN12);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN12 = RFR_LAMPIRAN12;


                            if (btn_uploadfile13.HasFile)
                            {
                                int imgSize = btn_uploadfile13.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile13.PostedFile.FileName).ToLower();
                                if (btn_uploadfile13.PostedFile != null && btn_uploadfile13.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile13.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 13 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN13 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "13" + "-" + btn_uploadfile13.FileName;
                                        btn_uploadfile13.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN13);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN13 = RFR_LAMPIRAN13;


                            if (btn_uploadfile14.HasFile)
                            {
                                int imgSize = btn_uploadfile14.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile14.PostedFile.FileName).ToLower();
                                if (btn_uploadfile14.PostedFile != null && btn_uploadfile14.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile14.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 14 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN14 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "14" + "-" + btn_uploadfile14.FileName;
                                        btn_uploadfile14.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN14);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN14 = RFR_LAMPIRAN14;



                            if (btn_uploadfile15.HasFile)
                            {
                                int imgSize = btn_uploadfile15.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile15.PostedFile.FileName).ToLower();
                                if (btn_uploadfile15.PostedFile != null && btn_uploadfile15.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile15.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 15 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN15 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "15" + "-" + btn_uploadfile15.FileName;
                                        btn_uploadfile15.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN15);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN15 = RFR_LAMPIRAN15;


                            if (btn_uploadfile16.HasFile)
                            {
                                int imgSize = btn_uploadfile16.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile16.PostedFile.FileName).ToLower();
                                if (btn_uploadfile16.PostedFile != null && btn_uploadfile16.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile16.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 16 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN16 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "16" + "-" + btn_uploadfile16.FileName;
                                        btn_uploadfile16.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN16);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN16 = RFR_LAMPIRAN16;


                            if (btn_uploadfile17.HasFile)
                            {
                                int imgSize = btn_uploadfile17.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile17.PostedFile.FileName).ToLower();
                                if (btn_uploadfile17.PostedFile != null && btn_uploadfile17.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile17.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 17 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN17 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "17" + "-" + btn_uploadfile17.FileName;
                                        btn_uploadfile17.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN17);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN17 = RFR_LAMPIRAN17;


                            if (btn_uploadfile18.HasFile)
                            {
                                int imgSize = btn_uploadfile18.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile18.PostedFile.FileName).ToLower();
                                if (btn_uploadfile18.PostedFile != null && btn_uploadfile18.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile18.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 18 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN18 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "18" + "-" + btn_uploadfile18.FileName;
                                        btn_uploadfile18.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN18);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN18 = RFR_LAMPIRAN18;


                            if (btn_uploadfile19.HasFile)
                            {
                                int imgSize = btn_uploadfile19.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile19.PostedFile.FileName).ToLower();
                                if (btn_uploadfile19.PostedFile != null && btn_uploadfile19.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile19.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 19 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN19 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "19" + "-" + btn_uploadfile19.FileName;
                                        btn_uploadfile19.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN19);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN19 = RFR_LAMPIRAN19;


                            if (btn_uploadfile20.HasFile)
                            {
                                int imgSize = btn_uploadfile20.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile20.PostedFile.FileName).ToLower();
                                if (btn_uploadfile20.PostedFile != null && btn_uploadfile20.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile20.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 20 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN20 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "20" + "-" + btn_uploadfile20.FileName;
                                        btn_uploadfile20.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN20);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN20 = RFR_LAMPIRAN20;


                            if (btn_uploadfile21.HasFile)
                            {
                                int imgSize = btn_uploadfile21.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile21.PostedFile.FileName).ToLower();
                                if (btn_uploadfile21.PostedFile != null && btn_uploadfile21.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile21.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 21 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN21 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "21" + "-" + btn_uploadfile21.FileName;
                                        btn_uploadfile21.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN21);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN21 = RFR_LAMPIRAN21;


                            if (btn_uploadfile22.HasFile)
                            {
                                int imgSize = btn_uploadfile22.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile22.PostedFile.FileName).ToLower();
                                if (btn_uploadfile22.PostedFile != null && btn_uploadfile22.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile22.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 22 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN22 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "22" + "-" + btn_uploadfile22.FileName;
                                        btn_uploadfile22.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN22);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN22 = RFR_LAMPIRAN22;


                            if (btn_uploadfile23.HasFile)
                            {
                                int imgSize = btn_uploadfile23.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile23.PostedFile.FileName).ToLower();
                                if (btn_uploadfile23.PostedFile != null && btn_uploadfile23.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile23.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 23 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN23 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "23" + "-" + btn_uploadfile23.FileName;
                                        btn_uploadfile23.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN23);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN23 = RFR_LAMPIRAN23;


                            if (btn_uploadfile24.HasFile)
                            {
                                int imgSize = btn_uploadfile24.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile24.PostedFile.FileName).ToLower();
                                if (btn_uploadfile24.PostedFile != null && btn_uploadfile24.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile24.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 24 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN24 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "24" + "-" + btn_uploadfile24.FileName;
                                        btn_uploadfile24.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN24);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN24 = RFR_LAMPIRAN24;


                            if (btn_uploadfile25.HasFile)
                            {
                                int imgSize = btn_uploadfile25.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile25.PostedFile.FileName).ToLower();
                                if (btn_uploadfile25.PostedFile != null && btn_uploadfile25.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile25.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 25 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN25 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "25" + "-" + btn_uploadfile25.FileName;
                                        btn_uploadfile25.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN25);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN25 = RFR_LAMPIRAN25;


                            if (btn_uploadfile26.HasFile)
                            {
                                int imgSize = btn_uploadfile26.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile26.PostedFile.FileName).ToLower();
                                if (btn_uploadfile26.PostedFile != null && btn_uploadfile26.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile26.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 26 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN26 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "26" + "-" + btn_uploadfile26.FileName;
                                        btn_uploadfile26.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN26);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN26 = RFR_LAMPIRAN26;


                            if (btn_uploadfile27.HasFile)
                            {
                                int imgSize = btn_uploadfile27.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile27.PostedFile.FileName).ToLower();
                                if (btn_uploadfile27.PostedFile != null && btn_uploadfile27.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile27.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 27 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN27 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "27" + "-" + btn_uploadfile27.FileName;
                                        btn_uploadfile27.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN27);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN27 = RFR_LAMPIRAN27;


                            if (btn_uploadfile28.HasFile)
                            {
                                int imgSize = btn_uploadfile28.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile28.PostedFile.FileName).ToLower();
                                if (btn_uploadfile28.PostedFile != null && btn_uploadfile28.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile28.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 28 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN28 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "28" + "-" + btn_uploadfile28.FileName;
                                        btn_uploadfile28.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN28);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN28 = RFR_LAMPIRAN28;


                            if (btn_uploadfile29.HasFile)
                            {
                                int imgSize = btn_uploadfile29.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile29.PostedFile.FileName).ToLower();
                                if (btn_uploadfile29.PostedFile != null && btn_uploadfile29.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile29.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 29 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN29 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "29" + "-" + btn_uploadfile29.FileName;
                                        btn_uploadfile29.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN29);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN29 = RFR_LAMPIRAN29;


                            if (btn_uploadfile30.HasFile)
                            {
                                int imgSize = btn_uploadfile30.PostedFile.ContentLength;
                                string ext = System.IO.Path.GetExtension(this.btn_uploadfile30.PostedFile.FileName).ToLower();
                                if (btn_uploadfile30.PostedFile != null && btn_uploadfile30.PostedFile.FileName != "")
                                {


                                    if (btn_uploadfile30.PostedFile.ContentLength > 3000000)
                                    {
                                        DivMessage.InnerText = "File 30 Lebih Besar Dari 3 MB.";
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
                                        RFR_LAMPIRAN30 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "30" + "-" + btn_uploadfile30.FileName;
                                        btn_uploadfile30.PostedFile
                      .SaveAs(Server.MapPath("~/Uploaded/FileUploadAds/") + RFR_LAMPIRAN30);
                                    }
                                }
                            }
                            trform4gdr.RFR_LAMPIRAN30 = RFR_LAMPIRAN30;
                            trform4gdr.CAMPAIGN = CAMPAIGN;
                            trform4gdr.CAPTION = CAPTION;
                            trform4gdr.TARGET_LOKASI = TARGET_LOKASI;
                            trform4gdr.UMUR = UMUR;
                            trform4gdr.JENIS_KELAMIN = JENIS_KELAMIN;
                            trform4gdr.INTEREST_MINAT = INTEREST_MINAT;
                            trform4gdr.INFORMASI_TAMBAHAN = INFORMASI_TAMBAHAN;
                            trform4gdr.DIBUAT = DIBUAT;
                            trform4gdr.TGL_DIBUAT = TGL_DIBUAT;
                            trform4gdr.MENYETUJUI1 = "";
                            trform4gdr.TGL_MENYETUJUI1 = startdate;
                            trform4gdr.DITERIMA_1 = "";
                            trform4gdr.TGL_DITERIMA_1 = startdate;
                            trform4gdr.STATUS = EApprovalStatus.OnApprovedHD;
                            trform4gdr.REVISI = "";
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

                            trform4gdr.USER_CURRENT = DIBUAT;
                            trform4gdr.NEXT_USER = USERNEXT;
                            trform4gdr.URUTAN_USER_CURRENT = URUTAN;
                            trform4gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                            trform4gdrda.Update(trform4gdr);

                            UpdateDetailRepairBrand();
                            UpdateDetailIklan();
                            SendEmailAllType();

                            TR_FORM_GDR_ACTIVITY_DA trformgdrActivityDa = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                            DataSet DsFormActivity = new DataSet();

                            TR_FORM_GDR_ACTIVITY trformgdractivity = new TR_FORM_GDR_ACTIVITY();
                            trformgdractivity.USERNAME = HfUsername.Value;
                            trformgdractivity.ACTIVITY_TIME = DateTime.Now;
                            trformgdractivity.KODE_FORM = "FRM-0004";
                            trformgdractivity.NO_FORM = NO_FORM;
                            trformgdractivity.STATUS = EApprovalStatus.OnApprovedHD;
                            trformgdractivity.DESCRIPTION = "Insert New Data. Status To " + EApprovalStatus.ApprovedGMMarkom;
                            trformgdractivity.REVISION = "-";
                            trformgdractivity.URUTAN = 1;
                            trformgdractivity.SP = "*";
                            trformgdractivity.USER_CURRENT = DIBUAT;
                            trformgdractivity.NEXT_USER = USERNEXT;
                            trformgdractivity.URUTAN_USER_CURRENT = URUTAN;
                            trformgdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                            trformgdrActivityDa.Insert(trformgdractivity);

                            DivMessage.InnerText = "Data Successfully Save";
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
                            DivMessage.InnerText = "Jenis Iklan Minimal DiCheckList 1 Data";
                            DivMessage.Attributes["class"] = "error";
                            //DivMessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;
                        }
                    }
                    else
                    {
                        DivMessage.InnerText = "Budget Cannot Be Empty";
                        DivMessage.Attributes["class"] = "error";
                        //DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                    }

                }
                else
                {
                    DivMessage.InnerText = "Brand Cannot Be Empty";
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

        public void UpdateApproveAll()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);

                int URUTAN = 0;
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                    if (URUTAN == 2)
                    {
                        UpdateStatusApproved();
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

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);

                int URUTAN = 0;
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                    if (URUTAN == 3)
                    {
                        UpdateStatusApprovedDigitalMarketing();
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

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);

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
                        UpdateStatusCancelDigitalMarketing();
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

        public void UpdateReviseAll()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);

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
                        UpdateStatusRevisiDigitalMarketing();
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
                TR_FORM4_GDR_DA TrForm4Gdr = new DataLayer.TR_FORM4_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string MENYETUJUI1 = text_menyetujui1.Text;
                DateTime TGL_MENYETUJUI1 = DateTime.Now;

                TR_FORM4_GDR TrForm4gdr = new TR_FORM4_GDR();
                TrForm4gdr.NO_FORM = NO_FORM;
                TrForm4gdr.MENYETUJUI1 = MENYETUJUI1;
                TrForm4gdr.TGL_MENYETUJUI1 = TGL_MENYETUJUI1;
                TrForm4gdr.STATUS = EApprovalStatus.ApprovedGMMarkom;
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

                string Where = string.Format("KD_JABATAN = 'DM'");
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
                TrForm4gdr.USER_CURRENT = HfUsername.Value;
                TrForm4gdr.NEXT_USER = USERNEXT;
                TrForm4gdr.URUTAN_USER_CURRENT = URUTAN;
                TrForm4gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm4Gdr.UpdateMenyetujui1(TrForm4gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm4GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm4gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm4gdractivity.USERNAME = HfUsername.Value;
                TrForm4gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm4gdractivity.KODE_FORM = "FRM-0004";
                TrForm4gdractivity.NO_FORM = NO_FORM;
                TrForm4gdractivity.STATUS = EApprovalStatus.ApprovedGMMarkom;
                TrForm4gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.OnApprovedHD + " To " + EApprovalStatus.ApprovedGMMarkom;
                TrForm4gdractivity.REVISION = "-";
                TrForm4gdractivity.URUTAN = 2;
                TrForm4gdractivity.SP = "";
                TrForm4gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm4gdractivity.NEXT_USER = USERNEXT;
                TrForm4gdractivity.URUTAN_USER_CURRENT = URUTAN;
                TrForm4gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm4GdrActivity.Insert(TrForm4gdractivity);


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

        //Action Digital Marketing
        public void UpdateStatusApprovedDigitalMarketing()
        {
            try
            {
                TR_FORM4_GDR_DA TrForm4Gdr = new DataLayer.TR_FORM4_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_1 = text_diterima1.Text;
                DateTime TGL_DITERIMA_1 = DateTime.Now;

                TR_FORM4_GDR TrForm4gdr = new TR_FORM4_GDR();
                TrForm4gdr.NO_FORM = NO_FORM;
                TrForm4gdr.DITERIMA_1 = DITERIMA_1;
                TrForm4gdr.TGL_DITERIMA_1 = TGL_DITERIMA_1;
                TrForm4gdr.STATUS = EApprovalStatus.Done;
                //TrForm4gdr.REVISI = "";

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

                string Where = string.Format("ID_DEPT LIKE '%34%' And KD_JABATAN = 'DM'");
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

                TrForm4gdr.USER_CURRENT = HfUsername.Value;
                TrForm4gdr.NEXT_USER = USERNEXT;
                TrForm4gdr.URUTAN_USER_CURRENT = URUTAN;
                TrForm4gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm4Gdr.UpdateDiterima1(TrForm4gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm4GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm4gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm4gdractivity.USERNAME = HfUsername.Value;
                TrForm4gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm4gdractivity.KODE_FORM = "FRM-0004";
                TrForm4gdractivity.NO_FORM = NO_FORM;
                TrForm4gdractivity.STATUS = EApprovalStatus.Done;
                TrForm4gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.ApprovedGMMarkom + " To " + EApprovalStatus.ApprovedDM + "And Status Ticket To Be " + EApprovalStatus.Done;
                TrForm4gdractivity.REVISION = "-";
                TrForm4gdractivity.URUTAN = 3;
                TrForm4gdractivity.SP = "";
                TrForm4gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm4gdractivity.NEXT_USER = USERNEXT;
                TrForm4gdractivity.URUTAN_USER_CURRENT = URUTAN;
                TrForm4gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm4GdrActivity.Insert(TrForm4gdractivity);

                DivMessage.InnerText = "Data Successful Approved Digital Marketing And Done";
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

        public void UpdateStatusCancel()
        {
            try
            {
                TR_FORM4_GDR_DA TrForm4Gdr = new DataLayer.TR_FORM4_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DIBUAT = text_dibuat.Text;
                DateTime TGL_DIBUAT = DateTime.Now;

                TR_FORM4_GDR TrForm4gdr = new TR_FORM4_GDR();
                TrForm4gdr.NO_FORM = NO_FORM;
                TrForm4gdr.DIBUAT = DIBUAT;
                TrForm4gdr.TGL_DIBUAT = TGL_DIBUAT;
                TrForm4gdr.STATUS = EApprovalStatus.Cancel;
                //TrForm3gdr.REVISI = "";
                TrForm4gdr.USER_CURRENT = HfUsername.Value;
                TrForm4gdr.NEXT_USER = "-";
                TrForm4gdr.URUTAN_USER_CURRENT = 1;
                TrForm4gdr.URUTAN_NEXT_USER = 0;
                TrForm4Gdr.UpdateDibuat(TrForm4gdr);


                TR_FORM_GDR_ACTIVITY_DA TrForm4GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm4gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm4gdractivity.USERNAME = HfUsername.Value;
                TrForm4gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm4gdractivity.KODE_FORM = "FRM-0004";
                TrForm4gdractivity.NO_FORM = NO_FORM;
                TrForm4gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm4gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm4gdractivity.REVISION = "-";
                TrForm4gdractivity.URUTAN = 1;
                TrForm4gdractivity.SP = "";
                TrForm4gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm4gdractivity.NEXT_USER = "-";
                TrForm4gdractivity.URUTAN_USER_CURRENT = 1;
                TrForm4gdractivity.URUTAN_NEXT_USER = 0;
                TrForm4GdrActivity.Insert(TrForm4gdractivity);


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
                TR_FORM4_GDR_DA TrForm4Gdr = new DataLayer.TR_FORM4_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string MENYETUJUI1 = text_menyetujui1.Text;
                DateTime TGL_MENYETUJUI1 = DateTime.Now;

                TR_FORM4_GDR TrForm4gdr = new TR_FORM4_GDR();
                TrForm4gdr.NO_FORM = NO_FORM;
                TrForm4gdr.MENYETUJUI1 = MENYETUJUI1;
                TrForm4gdr.TGL_MENYETUJUI1 = TGL_MENYETUJUI1;
                TrForm4gdr.STATUS = EApprovalStatus.Cancel;
                //TrForm3gdr.REVISI = "";
                TrForm4gdr.USER_CURRENT = HfUsername.Value;
                TrForm4gdr.NEXT_USER = "-";
                TrForm4gdr.URUTAN_USER_CURRENT = 1;
                TrForm4gdr.URUTAN_NEXT_USER = 0;
                TrForm4Gdr.UpdateMenyetujui1(TrForm4gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm4GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm4gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm4gdractivity.USERNAME = HfUsername.Value;
                TrForm4gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm4gdractivity.KODE_FORM = "FRM-0004";
                TrForm4gdractivity.NO_FORM = NO_FORM;
                TrForm4gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm4gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm4gdractivity.REVISION = "-";
                TrForm4gdractivity.URUTAN = 2;
                TrForm4gdractivity.SP = "";
                TrForm4gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm4gdractivity.NEXT_USER = "-";
                TrForm4gdractivity.URUTAN_USER_CURRENT = 1;
                TrForm4gdractivity.URUTAN_NEXT_USER = 0;
                TrForm4GdrActivity.Insert(TrForm4gdractivity);

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

        public void UpdateStatusCancelDigitalMarketing()
        {
            try
            {
                TR_FORM4_GDR_DA TrForm4Gdr = new DataLayer.TR_FORM4_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_1 = text_diterima1.Text;
                DateTime TGL_DITERIMA_1 = DateTime.Now;

                TR_FORM4_GDR TrForm4gdr = new TR_FORM4_GDR();
                TrForm4gdr.NO_FORM = NO_FORM;
                TrForm4gdr.DITERIMA_1 = DITERIMA_1;
                TrForm4gdr.TGL_DITERIMA_1 = TGL_DITERIMA_1;
                TrForm4gdr.STATUS = EApprovalStatus.Cancel;
                //TrForm3gdr.REVISI = "";
                TrForm4gdr.USER_CURRENT = HfUsername.Value;
                TrForm4gdr.NEXT_USER = "-";
                TrForm4gdr.URUTAN_USER_CURRENT = 1;
                TrForm4gdr.URUTAN_NEXT_USER = 0;
                TrForm4Gdr.UpdateDiterima1(TrForm4gdr);

                TR_FORM_GDR_ACTIVITY_DA TrForm4GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm4gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm4gdractivity.USERNAME = HfUsername.Value;
                TrForm4gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm4gdractivity.KODE_FORM = "FRM-0004";
                TrForm4gdractivity.NO_FORM = NO_FORM;
                TrForm4gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm4gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm4gdractivity.REVISION = "-";
                TrForm4gdractivity.URUTAN = 3;
                TrForm4gdractivity.SP = "";
                TrForm4gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm4gdractivity.NEXT_USER = "-";
                TrForm4gdractivity.URUTAN_USER_CURRENT = 1;
                TrForm4gdractivity.URUTAN_NEXT_USER = 0;
                TrForm4GdrActivity.Insert(TrForm4gdractivity);


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
                TR_FORM4_GDR_DA TrForm4Gdr = new DataLayer.TR_FORM4_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DIBUAT = text_dibuat.Text;
                string MENYETUJUI1 = text_menyetujui1.Text;
                string REVISI = text_revisi.Text;
                DateTime TGL_MENYETUJUI1 = DateTime.Now;

                if (REVISI != "")
                {
                    TR_FORM4_GDR TrForm4gdr = new TR_FORM4_GDR();
                    TrForm4gdr.NO_FORM = NO_FORM;
                    TrForm4gdr.MENYETUJUI1 = MENYETUJUI1;
                    TrForm4gdr.TGL_MENYETUJUI1 = TGL_MENYETUJUI1;
                    TrForm4gdr.STATUS = EApprovalStatus.OnRevise;
                    TrForm4gdr.REVISI = REVISI;

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

                    TrForm4gdr.USER_CURRENT = HfUsername.Value;
                    TrForm4gdr.NEXT_USER = USERNEXT;
                    TrForm4gdr.URUTAN_USER_CURRENT = URUTAN;
                    TrForm4gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm4Gdr.UpdateRevisiMenyetujui1(TrForm4gdr);

                    TR_FORM_GDR_ACTIVITY_DA TrForm4GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY TrForm4gdractivity = new TR_FORM_GDR_ACTIVITY();
                    TrForm4gdractivity.USERNAME = HfUsername.Value;
                    TrForm4gdractivity.ACTIVITY_TIME = DateTime.Now;
                    TrForm4gdractivity.KODE_FORM = "FRM-0004";
                    TrForm4gdractivity.NO_FORM = NO_FORM;
                    TrForm4gdractivity.STATUS = EApprovalStatus.OnRevise;
                    TrForm4gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.OnRevise;
                    TrForm4gdractivity.REVISION = REVISI;
                    TrForm4gdractivity.URUTAN = 2;
                    TrForm4gdractivity.SP = "";
                    TrForm4gdractivity.USER_CURRENT = HfUsername.Value;
                    TrForm4gdractivity.NEXT_USER = USERNEXT;
                    TrForm4gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    TrForm4gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm4GdrActivity.Insert(TrForm4gdractivity);

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

        public void UpdateStatusRevisiDigitalMarketing()
        {
            try
            {
                TR_FORM4_GDR_DA TrForm4Gdr = new DataLayer.TR_FORM4_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DIBUAT = text_dibuat.Text;
                string DITERIMA_1 = text_diterima1.Text;
                string REVISI = text_revisi.Text;
                DateTime TGL_DITERIMA_1 = DateTime.Now;

                if (REVISI != "")
                {
                    TR_FORM4_GDR TrForm4gdr = new TR_FORM4_GDR();
                    TrForm4gdr.NO_FORM = NO_FORM;
                    TrForm4gdr.DITERIMA_1 = DITERIMA_1;
                    TrForm4gdr.TGL_DITERIMA_1 = TGL_DITERIMA_1;
                    TrForm4gdr.STATUS = EApprovalStatus.OnRevise;
                    TrForm4gdr.REVISI = REVISI;

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

                    TrForm4gdr.USER_CURRENT = HfUsername.Value;
                    TrForm4gdr.NEXT_USER = USERNEXT;
                    TrForm4gdr.URUTAN_USER_CURRENT = URUTAN;
                    TrForm4gdr.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm4Gdr.UpdateRevisiDiterima1(TrForm4gdr);

                    TR_FORM_GDR_ACTIVITY_DA TrForm4GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY TrForm4gdractivity = new TR_FORM_GDR_ACTIVITY();
                    TrForm4gdractivity.USERNAME = HfUsername.Value;
                    TrForm4gdractivity.ACTIVITY_TIME = DateTime.Now;
                    TrForm4gdractivity.KODE_FORM = "FRM-0004";
                    TrForm4gdractivity.NO_FORM = NO_FORM;
                    TrForm4gdractivity.STATUS = EApprovalStatus.OnRevise;
                    TrForm4gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.OnRevise;
                    TrForm4gdractivity.REVISION = REVISI;
                    TrForm4gdractivity.URUTAN = 3;
                    TrForm4gdractivity.SP = "";
                    TrForm4gdractivity.USER_CURRENT = HfUsername.Value;
                    TrForm4gdractivity.NEXT_USER = USERNEXT;
                    TrForm4gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    TrForm4gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm4GdrActivity.Insert(TrForm4gdractivity);


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

        /// <summary>
        /// Show Button Sesuai Urutan
        /// </summary>

        public void ShowButtonUrutan2()
        {
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;

            btn_ToRevise.Visible = true;
            //btn_Revise.Visible = true;
            btn_Approved.Visible = true;
            btn_Reject.Visible = true;

        }

        public void ShowButtonUrutan3()
        {
            btn_Save.Visible = false;
            btn_Approved.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            text_dibuat.Enabled = false;
            text_menyetujui1.Enabled = false;
            text_diterima1.Enabled = false;


            btn_Accepted.Visible = true;
            btn_Accepted.Text = "Accepted";
            btn_ToRevise.Visible = true;
            //btn_Revise.Visible = true;
            //btn_Reject.Visible = true;
        }


        /// <summary>
        /// Add Data Pop Up Detail TR_FORM1_GDR_BUDGET
        /// </summary>

        public void LoadDataDetailIklan()
        {
            try
            {
                TR_FORM4_GDR_IKLAN_DA TrForm4GdrIklan = new DataLayer.TR_FORM4_GDR_IKLAN_DA();
                DataSet Ds = new DataSet();

                string KODE_IKLAN = "";
                string NAMA_IKLAN = "";
                string DETAIL = "";
                string KET = "";
                string ISPILIH = "";
                string Where = string.Format("NO_FORM = '{0}'", HfNO_FORM.Value);
                Ds = TrForm4GdrIklan.GetDataFilter(Where);

                int i = 0;
                int index = 0;
                foreach (DataRow Item in Ds.Tables[0].Rows)
                {
                    index = i;
                    i++;

                    if (!String.IsNullOrEmpty((Item.Field<String>("KODE_IKLAN"))))
                    {
                        KODE_IKLAN = Item.Field<String>("KODE_IKLAN");
                    }
                    else
                    {
                        KODE_IKLAN = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("NAMA_IKLAN"))))
                    {
                        NAMA_IKLAN = Item.Field<String>("NAMA_IKLAN");
                    }
                    else
                    {
                        NAMA_IKLAN = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("DETAIL"))))
                    {
                        DETAIL = Item.Field<String>("DETAIL");
                    }
                    else
                    {
                        DETAIL = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("KET"))))
                    {
                        KET = Item.Field<String>("KET");
                    }
                    else
                    {
                        KET = "";
                    }

                    //Load Group Facebook / Instagram Ads
                    if (KODE_IKLAN == "FBIG") radio_facebookinstagramadsjenis1.Checked = true;
                    if (DETAIL == "SponsoredJenisIklan1") checkbox_sponsorjenis1.Checked = true;
                    if (DETAIL == "BoostPostJenisIklan1") checkbox_boostpostjenis1.Checked = true;
                    if (DETAIL == "TraficJenis1") checkbox_trafficjenis1.Checked = true;
                    if (DETAIL == "VideoJenis1") checkbox_videojenis1.Checked = true;
                    if (DETAIL == "ConversionJenis1") checkbox_conversionjenis1.Checked = true;
                    if (DETAIL == "BrandAwarenesJenis1") checkbox_brandawarenesjenis1.Checked = true;

                    if (DETAIL == "EngagementJenis1") checkbox_engagementjenis1.Checked = true;
                    if (DETAIL == "LeadJenis1") checkbox_leadjenis1.Checked = true;
                    if (DETAIL == "ReachJenis1") checkbox_reachjenis1.Checked = true;
                    if (DETAIL == "DPAJenis1") checkbox_dpajenis1.Checked = true;
                    if (DETAIL == "OtherJenis1") checkbox_otherjenis1.Checked = true;

                    //Load Group Google
                    if (KODE_IKLAN == "Google") radio_googlejenis2.Checked = true;
                    if (DETAIL == "YoutubeAdsJenis2") checkbox_youtubeadsjenis2.Checked = true;
                    if (DETAIL == "GoogleDisplayNetworkJenis2") checkbox_googleadwordsjenis2.Checked = true;
                    if (DETAIL == "GoogleAdWordsJenis2") checkbox_googleadwordsjenis2.Checked = true;


                    if (DETAIL == "BrandAwarenesJenis2") checkbox_brandawarenesjenis2.Checked = true;
                    if (DETAIL == "Product&BrandsConsiderationJenis2") checkbox_productbrandsjenis2.Checked = true;



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

        public void SaveDetailIklan()
        {
            try
            {
                GroupSaveIklanFacebookInstagramAds();
                GroupSaveIklanGoogleAds();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateDetailIklan()
        {
            try
            {
                GroupUpdateIklanFacebookInstagramAds();
                GroupUpdateIklanGoogleAds();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        //Save Detail Iklan Per Function
        public void GroupSaveIklanFacebookInstagramAds()
        {
            try
            {
                TR_FORM4_GDR_IKLAN_DA TrForm4GdrIklan = new DataLayer.TR_FORM4_GDR_IKLAN_DA();
                DataSet Ds = new DataSet();

                TR_FORM4_GDR_IKLAN trform4gdriklan = new TR_FORM4_GDR_IKLAN();

                //Group Iklan Facebook / Instagram Ads
                string noform = text_noform.Text;
                string kodeiklan = "FBIG";
                string namaiklan = "Facebook/Instagram Ads";
                string sponsored = "";
                string boostpost = "";
                string trafic = "";
                string video = "";
                string conversion = "";
                string brandawarenes = "";
                string engagement = "";
                string lead = "";
                string reach = "";
                string DPA = "";
                string other = "";
                string KET = "";

                if (radio_facebookinstagramadsjenis1.Checked == true)
                {
                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = kodeiklan;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);
                }

                if (checkbox_sponsorjenis1.Checked == true)
                {
                    sponsored = "SponsoredJenisIklan1";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = sponsored;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

                }

                if (checkbox_boostpostjenis1.Checked == true)
                {
                    boostpost = "BoostPostJenisIklan1";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = boostpost;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

                }

                if (checkbox_trafficjenis1.Checked == true)
                {
                    trafic = "TraficJenis1";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = trafic;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

                }

                if (checkbox_videojenis1.Checked == true)
                {
                    video = "VideoJenis1";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = video;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

                }

                if (checkbox_conversionjenis1.Checked == true)
                {
                    conversion = "ConversionJenis1";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = conversion;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

                }

                if (checkbox_brandawarenesjenis1.Checked == true)
                {
                    brandawarenes = "BrandAwarenesJenis1";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = brandawarenes;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

                }

                if (checkbox_engagementjenis1.Checked == true)
                {
                    engagement = "EngagementJenis1";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = engagement;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

                }

                if (checkbox_leadjenis1.Checked == true)
                {
                    lead = "LeadJenis1";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = lead;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

                }

                if (checkbox_reachjenis1.Checked == true)
                {
                    reach = "ReachJenis1";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = reach;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

                }

                if (checkbox_dpajenis1.Checked == true)
                {
                    DPA = "DPAJenis1";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = DPA;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

                }

                if (checkbox_otherjenis1.Checked == true)
                {
                    other = "OtherJenis1";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = other;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

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

        public void GroupSaveIklanGoogleAds()
        {
            try
            {
                TR_FORM4_GDR_IKLAN_DA TrForm4GdrIklan = new DataLayer.TR_FORM4_GDR_IKLAN_DA();
                DataSet Ds = new DataSet();

                TR_FORM4_GDR_IKLAN trform4gdriklan = new TR_FORM4_GDR_IKLAN();

                //Group Google
                string noform = text_noform.Text;
                string kodeiklan = "Google";
                string namaiklan = "Google";
                string youtubeadsjenis = "";
                string googledisplaynetwork = "";
                string googleadwords = "";
                string brandawarenes = "";
                string productbrandsconsideration = "";

                string KET = "";

                if (radio_googlejenis2.Checked == true)
                {
                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = kodeiklan;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);
                }

                if (checkbox_youtubeadsjenis2.Checked == true)
                {
                    youtubeadsjenis = "YoutubeAdsJenis2";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = youtubeadsjenis;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

                }

                if (checkbox_googledisplaynetworkjenis2.Checked == true)
                {
                    googledisplaynetwork = "GoogleDisplayNetworkJenis2";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = googledisplaynetwork;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

                }


                if (checkbox_googleadwordsjenis2.Checked == true)
                {
                    googleadwords = "GoogleAdWordsJenis2";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = googleadwords;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

                }

                if (checkbox_brandawarenesjenis2.Checked == true)
                {
                    brandawarenes = "BrandAwarenesJenis2";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = brandawarenes;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

                }

                if (checkbox_productbrandsjenis2.Checked == true)
                {
                    productbrandsconsideration = "Product&BrandsConsiderationJenis2";

                    trform4gdriklan.NO_FORM = noform;
                    trform4gdriklan.KODE_IKLAN = kodeiklan;
                    trform4gdriklan.NAMA_IKLAN = namaiklan;
                    trform4gdriklan.DETAIL = productbrandsconsideration;
                    trform4gdriklan.KET = "";
                    trform4gdriklan.ISPILIH = "Yes";
                    trform4gdriklan.PIC = "";
                    TrForm4GdrIklan.Insert(trform4gdriklan);

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

        //Update Detail Iklan Per Function
        public void GroupUpdateIklanFacebookInstagramAds()
        {
            try
            {
                TR_FORM4_GDR_IKLAN_DA TrForm4GdrIklan = new DataLayer.TR_FORM4_GDR_IKLAN_DA();
                DataSet Ds = new DataSet();

                TR_FORM4_GDR_IKLAN trform4gdrbudget = new TR_FORM4_GDR_IKLAN();

                string noform = text_noform.Text;
                string Where = string.Format("NO_FORM = '{0}' AND KODE_IKLAN = 'FBIG'", noform);
                TrForm4GdrIklan.DeleteFilter(Where);

                GroupSaveIklanFacebookInstagramAds();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }


        }

        public void GroupUpdateIklanGoogleAds()
        {
            try
            {
                TR_FORM4_GDR_IKLAN_DA TrForm4GdrIklan = new DataLayer.TR_FORM4_GDR_IKLAN_DA();
                DataSet Ds = new DataSet();

                TR_FORM4_GDR_IKLAN trform4gdrbudget = new TR_FORM4_GDR_IKLAN();

                string noform = text_noform.Text;
                string Where = string.Format("NO_FORM = '{0}' AND KODE_IKLAN = 'Google'", noform);
                TrForm4GdrIklan.DeleteFilter(Where);

                GroupSaveIklanGoogleAds();

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
        /// Add Data Detail TR_FORM4_GDR_REPAIR_BRAND
        /// </summary>
        /// 

        public void LoadDataDetailRepairBrand()
        {
            try
            {
                TR_FORM4_GDR_BRAND_DA TrForm4GdrBrand = new DataLayer.TR_FORM4_GDR_BRAND_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = "";
                string KD_BRAND = "";
                string BRAND = "";
                string Where = string.Format("NO_FORM = '{0}'", HfNO_FORM.Value);
                Ds = TrForm4GdrBrand.GetDataFilter(Where);

                int i = 0;
                int index = 0;
                foreach (DataRow Item in Ds.Tables[0].Rows)
                {
                    index = i;
                    i++;

                    if (!String.IsNullOrEmpty((Item.Field<String>("NO_FORM"))))
                    {
                        NO_FORM = Item.Field<String>("NO_FORM");
                    }
                    else
                    {
                        NO_FORM = "";
                    }


                    if (!String.IsNullOrEmpty((Item.Field<String>("KD_BRAND"))))
                    {
                        KD_BRAND = Item.Field<String>("KD_BRAND");
                    }
                    else
                    {
                        KD_BRAND = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("BRAND"))))
                    {
                        BRAND = Item.Field<String>("BRAND");
                    }
                    else
                    {
                        BRAND = "";
                    }

                    //Load Group Brand
                    if (KD_BRAND == "18") checkbox_colorbox.Checked = true;
                    if (KD_BRAND == "24") checkbox_adidas.Checked = true;
                    if (KD_BRAND == "25") checkbox_lecoq.Checked = true;
                    if (KD_BRAND == "26") checkbox_jockey.Checked = true;
                    if (KD_BRAND == "29") checkbox_tirajeans.Checked = true;
                    if (KD_BRAND == "91") checkbox_executive.Checked = true;
                    if (KD_BRAND == "92") checkbox_wood.Checked = true;
                    if (KD_BRAND == "93") checkbox_wrangler.Checked = true;
                    if (KD_BRAND == "96") checkbox_lee.Checked = true;
                    if (KD_BRAND == "99") checkbox_etcetera.Checked = true;

                    if(checkbox_colorbox.Checked == true && checkbox_adidas.Checked == true && checkbox_lecoq.Checked == true && checkbox_jockey.Checked == true && checkbox_tirajeans.Checked == true && checkbox_executive.Checked == true && checkbox_wood.Checked == true && checkbox_wrangler.Checked == true && checkbox_lee.Checked == true && checkbox_etcetera.Checked == true)
                    {
                        checkbox_checkall.Checked = true;
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

        public void SaveDetailRepairBrand()
        {
            try
            {
                GroupSaveRepairBrand();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateDetailRepairBrand()
        {
            try
            {
                GroupUpdateRepairBrand();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        public void GroupSaveRepairBrand()
        {
            try
            {
                TR_FORM4_GDR_BRAND_DA TrForm4GdrBrand = new DataLayer.TR_FORM4_GDR_BRAND_DA();
                DataSet Ds = new DataSet();

                TR_FORM4_GDR_BRAND trform4gdrbrand = new TR_FORM4_GDR_BRAND();

                //Group Save Brand
                string noform = text_noform.Text;
                string kdbrandcolorbox = "18";
                string kdbrandadidas = "24";
                string kdbrandlecoq = "25";
                string kdbrandjockey = "26";
                string kdbrandtirajeans = "29";
                string kdbrandexecutive = "91";
                string kdbrandwood = "92";
                string kdbrandwrangler = "93";
                string kdbrandlee = "96";
                string kdbrandetcetera = "99";


                string brandcolorbox = "ColorBox";
                string brandadidas = "Adidas";
                string brandlecoq = "Le Coq";
                string brandjockey = "Jockey";
                string brandtirajeans = "Tira Jeans";
                string brandexecutive = "The Executive";
                string brandwood = "Wood";
                string brandwrangler = "Wrangler";
                string brandlee = "Lee";
                string brandetcetera = "Et Cetera";


                if (checkbox_colorbox.Checked == true)
                {
                    trform4gdrbrand.NO_FORM = noform;
                    trform4gdrbrand.KD_BRAND = kdbrandcolorbox;
                    trform4gdrbrand.BRAND = brandcolorbox;
                    TrForm4GdrBrand.Insert(trform4gdrbrand);

                }

                if (checkbox_adidas.Checked == true)
                {
                    trform4gdrbrand.NO_FORM = noform;
                    trform4gdrbrand.KD_BRAND = kdbrandadidas;
                    trform4gdrbrand.BRAND = brandadidas;
                    TrForm4GdrBrand.Insert(trform4gdrbrand);

                }

                if (checkbox_lecoq.Checked == true)
                {
                    trform4gdrbrand.NO_FORM = noform;
                    trform4gdrbrand.KD_BRAND = kdbrandlecoq;
                    trform4gdrbrand.BRAND = brandlecoq;
                    TrForm4GdrBrand.Insert(trform4gdrbrand);

                }

                if (checkbox_jockey.Checked == true)
                {
                    trform4gdrbrand.NO_FORM = noform;
                    trform4gdrbrand.KD_BRAND = kdbrandjockey;
                    trform4gdrbrand.BRAND = brandjockey;
                    TrForm4GdrBrand.Insert(trform4gdrbrand);

                }

                if (checkbox_tirajeans.Checked == true)
                {
                    trform4gdrbrand.NO_FORM = noform;
                    trform4gdrbrand.KD_BRAND = kdbrandtirajeans;
                    trform4gdrbrand.BRAND = brandtirajeans;
                    TrForm4GdrBrand.Insert(trform4gdrbrand);

                }

                if (checkbox_executive.Checked == true)
                {
                    trform4gdrbrand.NO_FORM = noform;
                    trform4gdrbrand.KD_BRAND = kdbrandexecutive;
                    trform4gdrbrand.BRAND = brandexecutive;
                    TrForm4GdrBrand.Insert(trform4gdrbrand);

                }

                if (checkbox_wood.Checked == true)
                {
                    trform4gdrbrand.NO_FORM = noform;
                    trform4gdrbrand.KD_BRAND = kdbrandwood;
                    trform4gdrbrand.BRAND = brandwood;
                    TrForm4GdrBrand.Insert(trform4gdrbrand);

                }

                if (checkbox_wrangler.Checked == true)
                {
                    trform4gdrbrand.NO_FORM = noform;
                    trform4gdrbrand.KD_BRAND = kdbrandwrangler;
                    trform4gdrbrand.BRAND = brandwrangler;
                    TrForm4GdrBrand.Insert(trform4gdrbrand);

                }

                if (checkbox_lee.Checked == true)
                {
                    trform4gdrbrand.NO_FORM = noform;
                    trform4gdrbrand.KD_BRAND = kdbrandlee;
                    trform4gdrbrand.BRAND = brandlee;
                    TrForm4GdrBrand.Insert(trform4gdrbrand);

                }

                if (checkbox_etcetera.Checked == true)
                {
                    trform4gdrbrand.NO_FORM = noform;
                    trform4gdrbrand.KD_BRAND = kdbrandetcetera;
                    trform4gdrbrand.BRAND = brandetcetera;
                    TrForm4GdrBrand.Insert(trform4gdrbrand);

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

        public void GroupUpdateRepairBrand()
        {
            try
            {

                TR_FORM4_GDR_BRAND_DA TrForm4GdrBrand = new DataLayer.TR_FORM4_GDR_BRAND_DA();
                DataSet Ds = new DataSet();

                TR_FORM4_GDR_BRAND trform4gdrbrand = new TR_FORM4_GDR_BRAND();


                string noform = text_noform.Text;

                string Where = string.Format("NO_FORM = '{0}'", noform);
                TrForm4GdrBrand.DeleteFilter(Where);

                GroupSaveRepairBrand();

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
        /// Add Data Pop Up Detail TR_FORM4_GDR_MATERI
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

            //Button Revise Change Color
            if (btn_Revise.Enabled)
            {


            }
            else
            {
                btn_Revise.BackColor = Color.Gray;
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
                TR_FORM4_GDR_DA TrForm4DA = new TR_FORM4_GDR_DA();

                //Mendapatkan Status Form Berdasarkan No Form
                string NO_FORM = text_noform.Text;
                string Status = "";
                DsForm = TrForm4DA.GetDataByKey(NO_FORM);

                if (DsForm.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToString(DsForm.Tables[0].Rows[0]["STATUS"].ToString());

                    switch (Status)
                    {
                        case "Submit":
                            SendEmailOnApprovedToHD();
                            break;
                        case "Approved-GM-Markom":
                            SendEmailApprovedHDToDigitalMarketing();
                            break;
                        case "Approved-Creative-Director":
                            SendEmailAcceptedDigitalMarketingToDone();
                            break;
                        case "Done":
                            SendEmailAcceptedDigitalMarketingToDone();
                            SendEmailUserToDone();
                            break;
                        case "On-Revise":
                            SendEmailReviseToUser();
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
                    //string BRAND = text_brand.Text;
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);


                    string EmailTemplateEng = "Dear Head Department ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>" + "PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Approve", MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

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

        public void SendEmailApprovedHDToDigitalMarketing()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("KD_JABATAN = 'DM'");
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
                    //string BRAND = text_brand.Text;
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);

                    string EmailTemplateEng = "Dear Digital Marketing ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Approve", MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

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

        public void SendEmailAcceptedDigitalMarketingToDone()
        {
            try
            {
                int Id_Dept = 0;
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("ID_DEPT LIKE '%34%' And KD_JABATAN = 'DM'");
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
                    //string BRAND = text_brand.Text;
                    string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);

                    string EmailTemplateEng = "Dear Digital Marketing ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE +  "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Telah Berhasil diselesaikan <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Approve", MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
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

                TR_FORM4_GDR_DA TrForm4Gdr = new DataLayer.TR_FORM4_GDR_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("NO_FORM = '{0}' And DIBUAT = '{1}'", NO_FORM, DIBUAT);
                Ds = TrForm4Gdr.GetDataFilter(Where);

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
                    //string BRAND = text_brand.Text;
                    //string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);

                    string EmailTemplateEng = "Dear Requester,<br />" + USERNAME + "<br /><br />Dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Telah Berhasil diselesaikan <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Ticket Done", MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

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

                TR_FORM4_GDR_DA TrForm4Gdr = new DataLayer.TR_FORM4_GDR_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("NO_FORM = '{0}' And DIBUAT = '{1}'", NO_FORM, DIBUAT);
                Ds = TrForm4Gdr.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["DIBUAT"].ToString());
                    string MENYETUJUI_1 = "";
                    string DITERIMA_1 = "";

                    if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["MENYETUJUI1"].ToString())))
                    {
                        MENYETUJUI_1 = Convert.ToString(Ds.Tables[0].Rows[0]["MENYETUJUI1"].ToString());
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_1"].ToString())))
                    {
                        DITERIMA_1 = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_1"].ToString());
                    }

                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();
                    DataSet DsHeadDepartment = new DataSet();
                    DataSet DsDigitalMarketing = new DataSet();

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
                    //string BRAND = text_brand.Text;
                    //string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                    string REVISI = text_revisi.Text;


                    //Mendapatkan Urutan User Berdasarkan Jabatan
                    DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, KODE_FORM);
                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Requester,<br />" + USERNAME + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Telah Dilakukan Revisi oleh: " + HfUsername.Value + "<br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + USERNAME))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);

                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + USERNAME))));
                        MailHelper.SendMail(Email, "Email Revise", MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                    }

                    //Mendapatkan Urutan Head Department Berdasarkan Jabatan
                    DsHeadDepartment = MsUserDA.GetDataUrutanUser(MENYETUJUI_1, KODE_FORM);
                    if (DsHeadDepartment.Tables[0].Rows.Count > 0)
                    {
                        Email = DsHeadDepartment.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsHeadDepartment.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsHeadDepartment.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Head Department,<br />" + MENYETUJUI_1 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Telah Dilakukan Revisi oleh: " + HfUsername.Value + "<br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + MENYETUJUI_1))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);



                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + MENYETUJUI_1))));
                        MailHelper.SendMail(Email, "Email Revise", MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                    }

                    //Mendapatkan Urutan Digital Marketing Berdasarkan Jabatan
                    DsDigitalMarketing = MsUserDA.GetDataUrutanUser(DITERIMA_1, KODE_FORM);
                    if (DsDigitalMarketing.Tables[0].Rows.Count > 0)
                    {
                        Email = DsDigitalMarketing.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsDigitalMarketing.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsDigitalMarketing.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Digital Marketing,<br />" + DITERIMA_1 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Telah Dilakukan Revisi oleh: " + HfUsername.Value + " <br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + DITERIMA_1))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);



                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + DITERIMA_1))));
                        MailHelper.SendMail(Email, "Email Revise", MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
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

        public void SendEmailCancelToUser()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string NO_FORM = text_noform.Text;
                string DIBUAT = text_dibuat.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                TR_FORM4_GDR_DA TrForm4Gdr = new DataLayer.TR_FORM4_GDR_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("NO_FORM = '{0}' And DIBUAT = '{1}'", NO_FORM, DIBUAT);
                Ds = TrForm4Gdr.GetDataFilter(Where);

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
                    //string BRAND = text_brand.Text;
                    //string DIBUAT = text_dibuat.Text;
                    DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);


                    string EmailTemplateEng = "Dear Requester ,<br />" + USERNAME + "<br /><br />Terkait atas pengajuan dokument berikut:  <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Telah Dilakukan Cancel oleh: " + HfUsername.Value + " <br/>Mohon Konfirmasinya dengan mengklik<br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                    string TempGuid = Guid.NewGuid().ToString();
                    string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                    + (TempGuid + ("&uid=" + USERNAME))));
                    string MailMessage = string.Format(EmailTemplateEng, Url);



                    Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                    + (TempGuid + ("&uid=" + USERNAME))));
                    MailHelper.SendMail(Email, "Email Cancel", MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

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

        public void ViewOnlyState()
        {
            Pnl_Forms.Enabled = false;
            Pnl_Others1.Enabled = false;
        }

    }
}