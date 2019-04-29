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

namespace WebDelamiFormRequest.Forms_Data_Process
{
    public partial class I_FormRequest_Repair : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dtToko = new DataTable();
        public string KODE_FORM = "FRM-0005";

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

            if (_NO_FORM == null)
            {
                StartFirstTime();
            }
            else
            {
                HfNO_FORM.Value = _NO_FORM;
                HfID_DEPT.Value = Convert.ToString(Session["ID_DEPT"].ToString());
                HfUsername.Value = Session["Username"].ToString();
                LoadDataFormRequestRepair();
                ChangeButtonColor();
            }

            HfID_DEPT.Value = Convert.ToString(Session["ID_DEPT"].ToString());
            HfUsername.Value = Session["Username"].ToString();
        }

        protected void ddljenis_SelectedIndexChanged1(object sender, EventArgs e)
        {
            LoadDataFormMSCustToGridCustCt();

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "move_up()", true);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                SaveFormRequestRepair();
            }
            else
            {

            }
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("L_FormRequest_Repair.aspx");
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
            //DivMessage.Visible = false;
            ModalRevise.Show();
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
                //UpdateAcceptedAll();
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
                UpdateSubmitFormRequestRepair();
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

        protected void btn_NewStoreDesign_Click(object sender, EventArgs e)
        {
            //label_noform.Text = Session["NO_FORM"].ToString();
            if (text_tanggalrequired.Text != "")
            {
                Session["REQUIRED_DATE"] = text_tanggalrequired.Text;
            }
            else
            {
                Session["REQUIRED_DATE"] = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
            }

            Session["DepartemenName"] = label_departmentvalue.Text;
            Session["BrandName"] = text_brand.Text;

            DataSet DsDept = new DataSet();
            MS_DEPT_DA DeptDA = new MS_DEPT_DA();
            DsDept = DeptDA.GetDataFilter(string.Format("DEPT = '{0}'", label_departmentvalue.Text));
            if (DsDept.Tables[0].Rows.Count > 0)
            {
                Session["KODE_DEPT"] = Convert.ToString(DsDept.Tables[0].Rows[0]["KODE_DEPT"].ToString());
            }

            DataSet DsBrand = new DataSet();
            BRAND_DA BrandDA = new BRAND_DA();

            DsBrand = BrandDA.GetDataFilter(string.Format("BRAND = '{0}'", text_brand.Text));
            if (DsBrand.Tables[0].Rows.Count > 0)
            {
                Session["KD_BRAND"] = Convert.ToString(DsBrand.Tables[0].Rows[0]["KD_BRAND"].ToString());
            }

            Session["STORE_TYPE_REQUEST"] = ddljenis.Text;

            Response.Redirect(string.Format("I_FormRequest_Repair_SD.aspx?NO_FORM=" + text_noform.Text + "&STATUS=No"));

        }

        protected void btn_ViewStoreDesign_Click(object sender, EventArgs e)
        {
            //label_noform.Text = Session["NO_FORM"].ToString();
            if (text_tanggalrequired.Text != "")
            {
                Session["REQUIRED_DATE"] = text_tanggalrequired.Text;
            }
            else
            {
                Session["REQUIRED_DATE"] = new DateTime(1900, 01, 01).ToString("yyyy-MM-dd");
            }

            Session["DepartemenName"] = label_departmentvalue.Text;
            Session["BrandName"] = text_brand.Text;

            DataSet DsDept = new DataSet();
            MS_DEPT_DA DeptDA = new MS_DEPT_DA();
            DsDept = DeptDA.GetDataFilter(string.Format("DEPT = '{0}'", label_departmentvalue.Text));
            if (DsDept.Tables[0].Rows.Count > 0)
            {
                Session["KODE_DEPT"] = Convert.ToString(DsDept.Tables[0].Rows[0]["KODE_DEPT"].ToString());
            }

            DataSet DsBrand = new DataSet();
            BRAND_DA BrandDA = new BRAND_DA();

            DsBrand = BrandDA.GetDataFilter(string.Format("BRAND = '{0}'", text_brand.Text));
            if (DsBrand.Tables[0].Rows.Count > 0)
            {
                Session["KD_BRAND"] = Convert.ToString(DsBrand.Tables[0].Rows[0]["KD_BRAND"].ToString());
            }

            Session["STORE_TYPE_REQUEST"] = ddljenis.Text;

            Response.Redirect(string.Format("I_FormRequest_Repair_SD.aspx?NO_FORM=" + text_noform.Text + "&STATUS=Yes"));
        }

        protected void linkbtn_filename1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename="
                    + linkbtn_filename1.Text);
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadRPR/")
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadRPR/")
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadRPR/")
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadRPR/")
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadRPR/")
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadRPR/")
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadRPR/")
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadRPR/")
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadRPR/")
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadRPR/")
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

        #region All Functions

        // Action User
        public void StartFirstTime()
        {
            try
            {
                CreateNoFormRepair();
                string NO_FORM = text_noform.Text;
                TR_FORM_GDR_CUST_TEMP_DA TrFormGdrCustTemp = new DataLayer.TR_FORM_GDR_CUST_TEMP_DA();
                TrFormGdrCustTemp.DeleteFilter(string.Format("KODE_FORM = '{0}' AND NO_FORM = '{1}'", KODE_FORM, NO_FORM));
                label_departmentvalue.Text = Session["DepartemenName"].ToString();
                text_brand.Text = Session["BrandName"].ToString();

                //Data Description Of Repair And Upload File
                text_descriptionrepair2_1.Enabled = false;
                ddlpicperbaikan1.Enabled = false;
                text_budgets1.Enabled = false;
                text_actualfinishdate1.Enabled = false;
                text_completedate1.Enabled = false;

                text_descriptionrepair2_2.Enabled = false;
                ddlpicperbaikan2.Enabled = false;
                text_budgets2.Enabled = false;
                text_actualfinishdate2.Enabled = false;
                text_completedate2.Enabled = false;

                text_descriptionrepair2_3.Enabled = false;
                ddlpicperbaikan3.Enabled = false;
                text_budgets3.Enabled = false;
                text_actualfinishdate3.Enabled = false;
                text_completedate3.Enabled = false;

                text_descriptionrepair2_4.Enabled = false;
                ddlpicperbaikan4.Enabled = false;
                text_budgets4.Enabled = false;
                text_actualfinishdate4.Enabled = false;
                text_completedate4.Enabled = false;

                text_descriptionrepair2_5.Enabled = false;
                ddlpicperbaikan5.Enabled = false;
                text_budgets5.Enabled = false;
                text_actualfinishdate5.Enabled = false;
                text_completedate5.Enabled = false;

                text_descriptionrepair2_6.Enabled = false;
                ddlpicperbaikan6.Enabled = false;
                text_budgets6.Enabled = false;
                text_actualfinishdate6.Enabled = false;
                text_completedate6.Enabled = false;

                text_descriptionrepair2_7.Enabled = false;
                ddlpicperbaikan7.Enabled = false;
                text_budgets7.Enabled = false;
                text_actualfinishdate7.Enabled = false;
                text_completedate7.Enabled = false;

                text_descriptionrepair2_8.Enabled = false;
                ddlpicperbaikan8.Enabled = false;
                text_budgets8.Enabled = false;
                text_actualfinishdate8.Enabled = false;
                text_completedate8.Enabled = false;

                text_descriptionrepair2_9.Enabled = false;
                ddlpicperbaikan9.Enabled = false;
                text_budgets9.Enabled = false;
                text_actualfinishdate9.Enabled = false;
                text_completedate9.Enabled = false;

                text_descriptionrepair2_10.Enabled = false;
                ddlpicperbaikan10.Enabled = false;
                text_budgets10.Enabled = false;
                text_actualfinishdate10.Enabled = false;
                text_completedate10.Enabled = false;

                ddloverbudget.Enabled = false;
                text_tanggalrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_dibuat.Text = Session["Username"].ToString();
                text_overbudgetvalue.Enabled = false;
                Pnl_Created.Visible = true;
                btn_UpdateSubmit.Visible = false;
                PICChooseAll();

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void LoadDataFormRequestRepair()
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
                TR_FORM5_REPAIR_DA TrForm5Repair = new DataLayer.TR_FORM5_REPAIR_DA();

                Ds = TrForm5Repair.GetDataByKey(HfNO_FORM.Value);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    PICChooseAll();
                    text_noform.Text = Convert.ToString(Ds.Tables[0].Rows[0]["NO_FORM"].ToString());

                    ddljenis.Text = Convert.ToString(Ds.Tables[0].Rows[0]["JENIS"].ToString());
                    text_tanggalrequest.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_REQUEST"].ToString()).ToString("yyyy-MM-dd");

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

                    text_picrequester.Text = Convert.ToString(Ds.Tables[0].Rows[0]["PIC_REQUESTER"].ToString());
                    //text_permintaanperbaikan1.Text = Convert.ToString(Ds.Tables[0].Rows[0]["PERMINTAAN_PERBAIKAN_1"].ToString());
                    //text_permintaanperbaikan2.Text = Convert.ToString(Ds.Tables[0].Rows[0]["PERMINTAAN_PERBAIKAN_2"].ToString());
                    //text_permintaanperbaikan3.Text = Convert.ToString(Ds.Tables[0].Rows[0]["PERMINTAAN_PERBAIKAN_3"].ToString());
                    //text_permintaanperbaikan4.Text = Convert.ToString(Ds.Tables[0].Rows[0]["PERMINTAAN_PERBAIKAN_4"].ToString());
                    //text_permintaanperbaikan5.Text = Convert.ToString(Ds.Tables[0].Rows[0]["PERMINTAAN_PERBAIKAN_5"].ToString());
                    //text_permintaanperbaikan6.Text = Convert.ToString(Ds.Tables[0].Rows[0]["PERMINTAAN_PERBAIKAN_6"].ToString());
                    //text_permintaanperbaikan7.Text = Convert.ToString(Ds.Tables[0].Rows[0]["PERMINTAAN_PERBAIKAN_7"].ToString());
                    //text_permintaanperbaikan8.Text = Convert.ToString(Ds.Tables[0].Rows[0]["PERMINTAAN_PERBAIKAN_8"].ToString());
                    //text_permintaanperbaikan9.Text = Convert.ToString(Ds.Tables[0].Rows[0]["PERMINTAAN_PERBAIKAN_9"].ToString());
                    //text_permintaanperbaikan10.Text = Convert.ToString(Ds.Tables[0].Rows[0]["PERMINTAAN_PERBAIKAN_10"].ToString());

                    text_keterangan.Text = Convert.ToString(Ds.Tables[0].Rows[0]["KETERANGAN"].ToString());
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

                    text_diterima2.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_2"].ToString());
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
                    text_diterima4.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_4"].ToString());
                    text_tanggalditerima4.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DITERIMA_4"].ToString()).ToString("yyyy-MM-dd");

                    if (text_tanggalditerima4.Text == "1900-01-01")
                    {
                        text_tanggalditerima4.Text = "";

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


                    label_statusvalue.Text = Convert.ToString(Ds.Tables[0].Rows[0]["STATUS"].ToString());
                    text_revisiload.Text = Convert.ToString(Ds.Tables[0].Rows[0]["REVISI"].ToString());
                    string Status = Convert.ToString(Ds.Tables[0].Rows[0]["STATUS"].ToString());
                    if (Status == EApprovalStatus.ApprovedHDSD)
                    {
                        btn_NewStoreDesign.Visible = true;
                        btn_ViewStoreDesign.Visible = false;
                        btn_Approved.Enabled = false;
                        btn_Save.Enabled = false;
                        btn_Accepted.Enabled = false;
                    }
                    text_overbudgetvalue.Text = Convert.ToString(Convert.ToDecimal(Ds.Tables[0].Rows[0]["OVER_BUDGET"]).ToString("#,#0.##"));
                    string REQUEST_FOR = Convert.ToString(Ds.Tables[0].Rows[0]["REQUEST_FOR"].ToString());
                    if (REQUEST_FOR == "Relocation")
                    {
                        radio_relocation.Checked = true;
                    }
                    else if (REQUEST_FOR == "Renovation")
                    {
                        radio_renovation.Checked = true;
                    }
                    else if (REQUEST_FOR == "Repair/Maintenance")
                    {
                        radio_repair.Checked = true;
                    }
                    else if (REQUEST_FOR == "Additional")
                    {
                        radio_additional.Checked = true;
                    }

                    ddloverbudget.Text = Convert.ToString(Ds.Tables[0].Rows[0]["OVER_BUDGET_REQUEST"].ToString());

                    TR_FORM5_REPAIR_STORE_DESIGN_DA TrForm5RepairSD = new DataLayer.TR_FORM5_REPAIR_STORE_DESIGN_DA();
                    Ds = TrForm5RepairSD.GetDataFilter(string.Format("NO_FORM = '{0}'", HfNO_FORM.Value));
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        btn_ViewStoreDesign.Visible = true;
                        btn_NewStoreDesign.Visible = false;
                    }

                    LoadDataDetailCustCt();
                    LoadDataDetailPermintaanPerbaikan();

                    //Mengambil Posisi CM. Karena CM Ada terdapat 3 action sekaligus.
                    string KodeJabatan = "";

                    Ds = MsUserDA.GetDataFilter(string.Format("USERNAME = '{0}'", HfUsername.Value));
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        KodeJabatan = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                    }

                    string ACTIONGENERAL = "";
                    if (KodeJabatan == "USR")
                    {
                        ACTIONGENERAL = "Created";
                    }
                    else if (KodeJabatan == "PROJECT" && Status == EApprovalStatus.OnWorking)
                    {
                        ACTIONGENERAL = "Done";
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
                            ShowButtonUrutan1();
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
                                Pnl_Others1.Enabled = true;
                                Pnl_RevisiLoad.Visible = true;
                                Pnl_RevisiLoad.Enabled = false;
                                btn_Save.Visible = false;
                                btn_UpdateSubmit.Enabled = true;
                                btn_Reject.Enabled = true;
                                btn_Cancel.Enabled = false;
                            }
                            //else if (Status == EApprovalStatus.OnApprovedBM)
                            //{
                            //    Pnl_Forms.Enabled = true;
                            //    Pnl_Others1.Enabled = true;
                            //    Pnl_RevisiLoad.Visible = true;
                            //    Pnl_RevisiLoad.Enabled = false;
                            //    btn_Save.Visible = false;
                            //    btn_UpdateSubmit.Enabled = true;
                            //    btn_Reject.Enabled = true;
                            //    btn_Cancel.Enabled = false;
                            //}
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
                            text_diterima1.Text = HfUsername.Value;
                            ShowButtonUrutan3();
                            if (Status == EApprovalStatus.ApprovedHD || Status == EApprovalStatus.ApprovedStoreDesign)
                            {
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = true;
                                btn_Revise.Enabled = true;
                                btn_Reject.Enabled = true;
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
                        else if (URUTAN == 4)
                        {
                            text_diterimalain1.Text = HfUsername.Value;
                            ShowButtonUrutan4();
                            if (Status == EApprovalStatus.OnApprovedBM)
                            {
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = true;
                            }
                            //else if (Status == EApprovalStatus.OnApprovedBM)
                            //{
                            //    Pnl_Forms.Enabled = true;
                            //    Pnl_Others1.Enabled = true;
                            //    Pnl_RevisiLoad.Visible = true;
                            //    Pnl_RevisiLoad.Enabled = false;
                            //    btn_Save.Visible = false;
                            //    btn_UpdateSubmit.Enabled = true;
                            //    btn_Reject.Enabled = true;
                            //    btn_Cancel.Enabled = false;
                            //}
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
                            text_diterimalain2.Text = HfUsername.Value;
                            ShowButtonUrutan5();
                            if (Status == EApprovalStatus.ApprovedBMBudget)
                            {
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = true;
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
                            text_diterima2.Text = HfUsername.Value;
                            ShowButtonUrutan6();
                            if (Status == EApprovalStatus.ApprovedProject)
                            {
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = true;
                                btn_Revise.Enabled = true;
                                btn_Reject.Enabled = true;
                            }
                            else if (Status == EApprovalStatus.ApprovedComercialDirectorBudget)
                            {
                                btn_Approved.Enabled = true;
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
                        else if (URUTAN == 7)
                        {

                            text_diterima3.Text = HfUsername.Value;
                            ShowButtonUrutan7();
                            if (Status == EApprovalStatus.ApprovedBudgetControl)
                            {
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = true;
                            }

                            else
                            {
                                btn_Approved.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_Reject.Enabled = false;
                            }

                        }
                        else if (URUTAN == 8)
                        {

                            text_diterima4.Text = HfUsername.Value;
                            ShowButtonUrutan8();
                            if (Status == EApprovalStatus.OnWorking)
                            {
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
                        else
                        {
                            ShowButtonUrutan99();
                            btn_Approved.Enabled = false;
                            btn_ToRevise.Enabled = true;
                            btn_Revise.Enabled = true;
                            btn_Reject.Enabled = true;
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

        public void CreateNoFormRepair()
        {
            try
            {
                string where = string.Format("NO_FORM <> '' ORDER BY NO_FORM DESC");

                string NO_FORM = "";
                DataSet Ds = new DataSet();
                TR_FORM5_REPAIR_DA TrForm5Repair = new DataLayer.TR_FORM5_REPAIR_DA();

                DataSet DsForm = new DataSet();
                MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                string FORM_TYPE = "";
                DsForm = msformda.GetDataByKey(KODE_FORM);
                if (DsForm.Tables[0].Rows.Count > 0)
                {
                    FORM_TYPE = DsForm.Tables[0].Rows[0]["FORM_TYPE"].ToString();
                }

                Ds = TrForm5Repair.GetDataFilter(where);

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

        public void SaveFormRequestRepair()
        {
            try
            {
                TR_FORM5_REPAIR_DA trform5repairda = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                //TR_FORM_GDR_CUST_DA TrFormGdrCust = new DataLayer.TR_FORM_GDR_CUST_DA();
                //DataSet DsCust = new DataSet();

                string NO_FORM = text_noform.Text;
                string KODE_FORM = "FRM-0005";
                string ID_DEPT = Convert.ToString(Session["ID_DEPT"].ToString());
                string KD_BRAND = Session["KD_BRAND"].ToString();
                string PIC_REQUESTER = text_picrequester.Text;
                string JENIS = ddljenis.Text;
                DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                string BRAND = text_brand.Text;
                string PERMINTAAN_PERBAIKAN_1 = text_permintaanperbaikan1.Text;
                string PERMINTAAN_PERBAIKAN_2 = text_permintaanperbaikan2.Text;
                string PERMINTAAN_PERBAIKAN_3 = text_permintaanperbaikan3.Text;
                string PERMINTAAN_PERBAIKAN_4 = text_permintaanperbaikan4.Text;
                string PERMINTAAN_PERBAIKAN_5 = text_permintaanperbaikan5.Text;
                string PERMINTAAN_PERBAIKAN_6 = text_permintaanperbaikan6.Text;
                string PERMINTAAN_PERBAIKAN_7 = text_permintaanperbaikan7.Text;
                string PERMINTAAN_PERBAIKAN_8 = text_permintaanperbaikan8.Text;
                string PERMINTAAN_PERBAIKAN_9 = text_permintaanperbaikan9.Text;
                string PERMINTAAN_PERBAIKAN_10 = text_permintaanperbaikan10.Text;
                string KETERANGAN = text_keterangan.Text;
                string OVER_BUDGET = Convert.ToString(text_overbudgetvalue.Text);
                if (OVER_BUDGET != "")
                {
                    OVER_BUDGET = Convert.ToDecimal(text_overbudgetvalue.Text).ToString();
                }
                else
                {
                    OVER_BUDGET = "0.00";
                }

                string OVERBUDGET = ddloverbudget.Text;
                string REQUEST_FOR = "-";

                if (radio_relocation.Checked == true)
                {
                    REQUEST_FOR = "Relocation";
                }
                else if (radio_renovation.Checked == true)
                {
                    REQUEST_FOR = "Renovation";
                }
                else if (radio_repair.Checked == true)
                {
                    REQUEST_FOR = "Repair/Maintenance";
                }
                else if (radio_additional.Checked == true)
                {
                    REQUEST_FOR = "Additional";
                }
                else
                {
                    REQUEST_FOR = "-";
                }

                string DIBUAT = text_dibuat.Text;
                DateTime TGL_DIBUAT = DateTime.Now;
                DateTime startdate = new DateTime(1900, 01, 01);
                DateTime temp;

                if (REQUEST_FOR != "-")
                {
                    if (DateTime.TryParse(text_tanggalrequired.Text, out temp))
                    {
                        DateTime TANGGAL_REQUIRED = Convert.ToDateTime(text_tanggalrequired.Text);
                        var todaysDate = DateTime.Today;
                        int result = DateTime.Compare(TANGGAL_REQUIRED, todaysDate);

                        if (result > 0)
                        {
                            if (PIC_REQUESTER != "")
                            {
                                if (PERMINTAAN_PERBAIKAN_1 != "" || PERMINTAAN_PERBAIKAN_2 != "" || PERMINTAAN_PERBAIKAN_3 != "" || PERMINTAAN_PERBAIKAN_4 != "" || PERMINTAAN_PERBAIKAN_5 != "" || PERMINTAAN_PERBAIKAN_6 != "" || PERMINTAAN_PERBAIKAN_7 != "" || PERMINTAAN_PERBAIKAN_8 != "" || PERMINTAAN_PERBAIKAN_9 != "" || PERMINTAAN_PERBAIKAN_10 != "")
                                {

                                    TR_FORM_GDR_CUST_DA TrFormGdrCust = new DataLayer.TR_FORM_GDR_CUST_DA();

                                    int totalgridjenis = 0;
                                    //Check Store/Counter Or BUILDING
                                    if (JENIS == "STORE")
                                    {

                                        for (var i = 0; i < gvCustCt.VisibleRowCount; ++i)
                                        {
                                            if (gvCustCt.Selection.IsRowSelected(i))
                                            {
                                                totalgridjenis = 1;
                                            }
                                        }
                                    }
                                    else if (JENIS == "COUNTER")
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
                                    else if (JENIS == "RFS")
                                    {
                                        for (var i = 0; i < gvCustCt.VisibleRowCount; ++i)
                                        {
                                            if (gvCustCt.Selection.IsRowSelected(i))
                                            {
                                                totalgridjenis = 1;
                                            }
                                        }

                                    }
                                    else if (JENIS == "BUILDING")
                                    {
                                        totalgridjenis = 1;
                                    }


                                    if (totalgridjenis == 1)
                                    {

                                        DateTime? TGL_REQUIRED = Convert.ToDateTime(text_tanggalrequired.Text);

                                        TR_FORM5_REPAIR TrForm5Repair = new TR_FORM5_REPAIR();

                                        TrForm5Repair.NO_FORM = NO_FORM;
                                        TrForm5Repair.KODE_FORM = KODE_FORM;
                                        TrForm5Repair.JENIS = JENIS;
                                        TrForm5Repair.TGL_REQUEST = TGL_REQUEST;
                                        TrForm5Repair.TGL_REQUIRED = TGL_REQUIRED;
                                        TrForm5Repair.ID_DEPT = ID_DEPT;
                                        TrForm5Repair.KD_BRAND = KD_BRAND;
                                        TrForm5Repair.PIC_REQUESTER = PIC_REQUESTER;
                                        TrForm5Repair.KETERANGAN = KETERANGAN;
                                        TrForm5Repair.OVER_BUDGET = Convert.ToDecimal(OVER_BUDGET);
                                        TrForm5Repair.DIBUAT = DIBUAT;
                                        TrForm5Repair.TGL_DIBUAT = TGL_DIBUAT;
                                        TrForm5Repair.MENYETUJUI1 = "";
                                        TrForm5Repair.TGL_MENYETUJUI1 = startdate;
                                        TrForm5Repair.DITERIMA_1 = "";
                                        TrForm5Repair.TGL_DITERIMA_1 = startdate;
                                        TrForm5Repair.DITERIMA_2 = "";
                                        TrForm5Repair.TGL_DITERIMA_2 = startdate;
                                        TrForm5Repair.DITERIMA_3 = "";
                                        TrForm5Repair.TGL_DITERIMA_3 = startdate;
                                        TrForm5Repair.DITERIMA_4 = "";
                                        TrForm5Repair.TGL_DITERIMA_4 = startdate;
                                        TrForm5Repair.DITERIMA_LAIN_1 = "";
                                        TrForm5Repair.TGL_DITERIMA_LAIN_1 = startdate;
                                        TrForm5Repair.DITERIMA_LAIN_2 = "";
                                        TrForm5Repair.TGL_DITERIMA_LAIN_2 = startdate;
                                        TrForm5Repair.STATUS = EApprovalStatus.OnApprovedHD;
                                        TrForm5Repair.REVISI = "";
                                        TrForm5Repair.OVER_BUDGET_REQUEST = OVERBUDGET;

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

                                        TrForm5Repair.USER_CURRENT = DIBUAT;
                                        TrForm5Repair.NEXT_USER = USERNEXT;
                                        TrForm5Repair.URUTAN_USER_CURRENT = URUTAN;
                                        TrForm5Repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                                        TrForm5Repair.OVER_BUDGET_REQUEST = EYesNo.No;
                                        TrForm5Repair.REQUEST_FOR = REQUEST_FOR;
                                        trform5repairda.Insert(TrForm5Repair);

                                        SaveDetailPermintaanPerbaikan();
                                        SaveDetailCustCt();
                                        SendEmailAllType();

                                        TR_FORM_GDR_ACTIVITY_DA trformgdrActivityDa = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                                        DataSet DsFormActivity = new DataSet();

                                        TR_FORM_GDR_ACTIVITY trformgdractivity = new TR_FORM_GDR_ACTIVITY();
                                        trformgdractivity.USERNAME = HfUsername.Value;
                                        trformgdractivity.ACTIVITY_TIME = DateTime.Now;
                                        trformgdractivity.KODE_FORM = "FRM-0005";
                                        trformgdractivity.NO_FORM = NO_FORM;
                                        trformgdractivity.STATUS = EApprovalStatus.OnApprovedHD;
                                        trformgdractivity.DESCRIPTION = "Insert New Data. Status To " + EApprovalStatus.ApprovedHD;
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

                                        string HomePageUrl = "../Forms_Data_Process/L_FormRequest_Repair.aspx";
                                        Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                                        //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");

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
                                    DivMessage.InnerText = "Request for Repair Cannot Be Empty";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                }
                            }
                            else
                            {
                                DivMessage.InnerText = "PIC Requester Cannot Be Empty";
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
                        DivMessage.InnerText = "Required Date Cannot Be Empty";
                        DivMessage.Attributes["class"] = "error";
                        //DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                    }
                }
                else
                {
                    DivMessage.InnerText = "Request For Cannot Be Empty";
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

        public void UpdateSubmitFormRequestRepair()
        {
            try
            {
                TR_FORM5_REPAIR_DA trform5repairda = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                TR_FORM5_REPAIR_STORE_DESIGN_DA trform5repairstoredesignda = new DataLayer.TR_FORM5_REPAIR_STORE_DESIGN_DA();
                DataSet DsSD = new DataSet();

                  TR_FORM5_REPAIR_STORE_DESIGN_DETAIL_DA trform5repairstoredesigndetailda = new DataLayer.TR_FORM5_REPAIR_STORE_DESIGN_DETAIL_DA();
                DataSet DsSDDetail = new DataSet();

                TR_FORM_GDR_CUST_DA TrFormGdrCust = new DataLayer.TR_FORM_GDR_CUST_DA();
                DataSet DsCust = new DataSet();

                string NO_FORM = text_noform.Text;
                string KODE_FORM = "FRM-0005";
                string ID_DEPT = Convert.ToString(Session["ID_DEPT"].ToString());
                //string KD_BRAND = Session["KD_BRAND"].ToString();
                string JENIS = ddljenis.Text;
                DateTime? TGL_REQUEST = string.IsNullOrEmpty(text_tanggalrequest.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequest.Text);
                string BRAND = text_brand.Text;
                string PERMINTAAN_PERBAIKAN_1 = text_permintaanperbaikan1.Text;
                string PERMINTAAN_PERBAIKAN_2 = text_permintaanperbaikan2.Text;
                string PERMINTAAN_PERBAIKAN_3 = text_permintaanperbaikan3.Text;
                string PERMINTAAN_PERBAIKAN_4 = text_permintaanperbaikan4.Text;
                string PERMINTAAN_PERBAIKAN_5 = text_permintaanperbaikan5.Text;
                string PERMINTAAN_PERBAIKAN_6 = text_permintaanperbaikan6.Text;
                string PERMINTAAN_PERBAIKAN_7 = text_permintaanperbaikan7.Text;
                string PERMINTAAN_PERBAIKAN_8 = text_permintaanperbaikan8.Text;
                string PERMINTAAN_PERBAIKAN_9 = text_permintaanperbaikan9.Text;
                string PERMINTAAN_PERBAIKAN_10 = text_permintaanperbaikan10.Text;
                string PIC_REQUESTER = text_picrequester.Text;
                string KETERANGAN = text_keterangan.Text;
                string OVER_BUDGET = Convert.ToString(text_overbudgetvalue.Text);
                if (OVER_BUDGET != "")
                {
                    OVER_BUDGET = Convert.ToDecimal(text_overbudgetvalue.Text).ToString();
                }
                else
                {
                    OVER_BUDGET = "0";
                }

                string REQUEST_FOR = "-";

                if (radio_relocation.Checked == true)
                {
                    REQUEST_FOR = "Relocation";
                }
                else if (radio_renovation.Checked == true)
                {
                    REQUEST_FOR = "Renovation";
                }
                else if (radio_repair.Checked == true)
                {
                    REQUEST_FOR = "Repair/Maintenance";
                }
                else if (radio_additional.Checked == true)
                {
                    REQUEST_FOR = "Additional";
                }
                else
                {
                    REQUEST_FOR = "-";
                }

                string DIBUAT = text_dibuat.Text;
                DateTime TGL_DIBUAT = DateTime.Now;
                DateTime startdate = new DateTime(1900, 01, 01);
                DateTime temp;

                if (REQUEST_FOR != "-")
                {

                    if (PERMINTAAN_PERBAIKAN_1 != "" || PERMINTAAN_PERBAIKAN_2 != "" || PERMINTAAN_PERBAIKAN_3 != "" || PERMINTAAN_PERBAIKAN_4 != "" || PERMINTAAN_PERBAIKAN_5 != "" || PERMINTAAN_PERBAIKAN_6 != "" || PERMINTAAN_PERBAIKAN_7 != "" || PERMINTAAN_PERBAIKAN_8 != "" || PERMINTAAN_PERBAIKAN_9 != "" || PERMINTAAN_PERBAIKAN_10 != "")
                    {

                        DateTime? TGL_REQUIRED = Convert.ToDateTime(text_tanggalrequired.Text);

                        TR_FORM5_REPAIR TrForm5Repair = new TR_FORM5_REPAIR();

                        TrForm5Repair.NO_FORM = NO_FORM;
                        TrForm5Repair.KODE_FORM = KODE_FORM;
                        TrForm5Repair.JENIS = JENIS;
                        TrForm5Repair.TGL_REQUEST = TGL_REQUEST;
                        TrForm5Repair.TGL_REQUIRED = TGL_REQUIRED;
                        TrForm5Repair.ID_DEPT = ID_DEPT;
                        TrForm5Repair.PIC_REQUESTER = PIC_REQUESTER;
                        //TrForm5Repair.KD_BRAND = KD_BRAND;
                        TrForm5Repair.KETERANGAN = KETERANGAN;
                        TrForm5Repair.OVER_BUDGET = Convert.ToDecimal(OVER_BUDGET);


                        string OVERBUDGET = ddloverbudget.Text;
                        string STATUS = "";
                        string DESCRIPTION = "";
                        if (OVERBUDGET == EYesNo.Yes)
                        {
                            TrForm5Repair.DIBUAT = DIBUAT;
                            TrForm5Repair.TGL_DIBUAT = TGL_DIBUAT;
                            TrForm5Repair.DITERIMA_LAIN_1 = "";
                            TrForm5Repair.TGL_DITERIMA_LAIN_1 = startdate;
                            TrForm5Repair.DITERIMA_LAIN_2 = "";
                            TrForm5Repair.TGL_DITERIMA_LAIN_2 = startdate;

                            STATUS = EApprovalStatus.OnApprovedBM;
                            DESCRIPTION = "Update Status From " + EApprovalStatus.OnRevise + " To " + EApprovalStatus.OnApprovedBM;

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


                            BRAND_DA MsBrandDA = new BRAND_DA();
                            DataSet DsBrand = new DataSet();
                            string WhereBrand = string.Format("BRAND = '{0}'", BRAND);

                            string KodeBrand = "";
                            DsBrand = MsBrandDA.GetDataFilter(WhereBrand);
                            if (DsBrand.Tables[0].Rows.Count > 0)
                            {
                                KodeBrand = Convert.ToString(DsBrand.Tables[0].Rows[0]["KD_BRAND"].ToString());
                            }

                            MS_USER_DA MsUserDA = new MS_USER_DA();
                            DataSet DsUser = new DataSet();

                            string Where = string.Format("KD_BRAND LIKE '%{0}%' And KD_JABATAN = 'BM'", KodeBrand);

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

                            TrForm5Repair.STATUS = STATUS;
                            TrForm5Repair.REVISI = "";
                            TrForm5Repair.USER_CURRENT = DIBUAT;
                            TrForm5Repair.NEXT_USER = USERNEXT;
                            TrForm5Repair.URUTAN_USER_CURRENT = URUTAN;
                            TrForm5Repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                            TrForm5Repair.OVER_BUDGET_REQUEST = OVERBUDGET;
                            TrForm5Repair.REQUEST_FOR = REQUEST_FOR;
                            trform5repairda.UpdateBudget(TrForm5Repair);

                            //SaveDetailPermintaanPerbaikan();

                            TR_FORM_GDR_ACTIVITY_DA trformgdrActivityDa = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                            DataSet DsFormActivity = new DataSet();

                            TR_FORM_GDR_ACTIVITY trformgdractivity = new TR_FORM_GDR_ACTIVITY();
                            trformgdractivity.USERNAME = HfUsername.Value;
                            trformgdractivity.ACTIVITY_TIME = DateTime.Now;
                            trformgdractivity.KODE_FORM = "FRM-0005";
                            trformgdractivity.NO_FORM = NO_FORM;
                            trformgdractivity.STATUS = STATUS;
                            trformgdractivity.DESCRIPTION = DESCRIPTION;
                            trformgdractivity.REVISION = "-";
                            trformgdractivity.URUTAN = 1;
                            trformgdractivity.SP = "*";
                            trformgdractivity.USER_CURRENT = DIBUAT;
                            trformgdractivity.NEXT_USER = USERNEXT;
                            trformgdractivity.URUTAN_USER_CURRENT = URUTAN;
                            trformgdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                            trformgdrActivityDa.Insert(trformgdractivity);


                        }
                        else
                        {
                            TrForm5Repair.DIBUAT = DIBUAT;
                            TrForm5Repair.TGL_DIBUAT = TGL_DIBUAT;
                            TrForm5Repair.MENYETUJUI1 = "";
                            TrForm5Repair.TGL_MENYETUJUI1 = startdate;
                            TrForm5Repair.DITERIMA_1 = "";
                            TrForm5Repair.TGL_DITERIMA_1 = startdate;
                            TrForm5Repair.DITERIMA_2 = "";
                            TrForm5Repair.TGL_DITERIMA_2 = startdate;
                            TrForm5Repair.DITERIMA_3 = "";
                            TrForm5Repair.TGL_DITERIMA_3 = startdate;
                            TrForm5Repair.DITERIMA_4 = "";
                            TrForm5Repair.TGL_DITERIMA_4 = startdate;
                            TrForm5Repair.DITERIMA_LAIN_1 = "";
                            TrForm5Repair.TGL_DITERIMA_LAIN_1 = startdate;
                            TrForm5Repair.DITERIMA_LAIN_2 = "";
                            TrForm5Repair.TGL_DITERIMA_LAIN_2 = startdate;

                            STATUS = EApprovalStatus.OnApprovedHD;
                            DESCRIPTION = DESCRIPTION = "Update Status From " + EApprovalStatus.OnRevise + " To " + EApprovalStatus.OnApprovedHD;

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

                            TrForm5Repair.STATUS = STATUS;
                            TrForm5Repair.REVISI = "";
                            TrForm5Repair.USER_CURRENT = DIBUAT;
                            TrForm5Repair.NEXT_USER = USERNEXT;
                            TrForm5Repair.URUTAN_USER_CURRENT = URUTAN;
                            TrForm5Repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                            TrForm5Repair.OVER_BUDGET_REQUEST = OVERBUDGET;
                            TrForm5Repair.REQUEST_FOR = REQUEST_FOR;
                            trform5repairda.Update(TrForm5Repair);

                            UpdateDetailPermintaanPerbaikan();

                            TR_FORM_GDR_ACTIVITY_DA trformgdrActivityDa = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                            DataSet DsFormActivity = new DataSet();

                            TR_FORM_GDR_ACTIVITY trformgdractivity = new TR_FORM_GDR_ACTIVITY();
                            trformgdractivity.USERNAME = HfUsername.Value;
                            trformgdractivity.ACTIVITY_TIME = DateTime.Now;
                            trformgdractivity.KODE_FORM = "FRM-0005";
                            trformgdractivity.NO_FORM = NO_FORM;
                            trformgdractivity.STATUS = STATUS;
                            trformgdractivity.DESCRIPTION = DESCRIPTION;
                            trformgdractivity.REVISION = "-";
                            trformgdractivity.URUTAN = 1;
                            trformgdractivity.SP = "*";
                            trformgdractivity.USER_CURRENT = DIBUAT;
                            trformgdractivity.NEXT_USER = USERNEXT;
                            trformgdractivity.URUTAN_USER_CURRENT = URUTAN;
                            trformgdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                            trformgdrActivityDa.Insert(trformgdractivity);


                        }

                        if (JENIS == "BUILDING")
                        {
                            string WhereCust = string.Format("KODE_FORM = '{0}' AND NO_FORM = '{1}'", KODE_FORM, NO_FORM);
                            TrFormGdrCust.DeleteFilter(WhereCust);
                        }
                        else
                        {
                            SaveDetailCustCt();
                        }

                        if (REQUEST_FOR != "Relocation" && REQUEST_FOR != "Renovation")
                        {
                            DsSD = trform5repairstoredesignda.GetDataFilter(string.Format("NO_FORM = '{0}'", NO_FORM));
                            if (DsSD.Tables[0].Rows.Count > 0)
                            {
                                string NO_FORM_SD = Convert.ToString(DsSD.Tables[0].Rows[0]["NO_FORM_SD"].ToString());
                                string WhereSd = string.Format("NO_FORM = '{0}'", NO_FORM);
                                trform5repairstoredesignda.DeleteFilter(WhereSd);

                                string WhereSdDetail = string.Format("NO_FORM_SD = '{0}'", NO_FORM_SD);
                                trform5repairstoredesigndetailda.DeleteFilter(WhereSdDetail);
                            }

                        }

                        SendEmailAllType();

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
                        DivMessage.InnerText = "Request for Repair Cannot Be Empty";
                        DivMessage.Attributes["class"] = "error";
                        //DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                    }
                }
                else
                {
                    DivMessage.InnerText = "Request For Cannot Be Empty";
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

        public void CheckLockPICForm()
        {
            try
            {

                TR_FORM5_REPAIR_PERMINTAAN_DA TrForm5RepairPermintaan = new DataLayer.TR_FORM5_REPAIR_PERMINTAAN_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = "";
                string NO_PERMINTAAN = "";
                string PERMINTAAN_PERBAIKAN = "";
                string PERMINTAAN_PERBAIKAN_2 = "";
                string PIC = "";
                string UPLOAD_FILE = "";

                string NO_PERMINTAAN_1 = "1";
                string NO_PERMINTAAN_2 = "2";
                string NO_PERMINTAAN_3 = "3";
                string NO_PERMINTAAN_4 = "4";
                string NO_PERMINTAAN_5 = "5";
                string NO_PERMINTAAN_6 = "6";
                string NO_PERMINTAAN_7 = "7";
                string NO_PERMINTAAN_8 = "8";
                string NO_PERMINTAAN_9 = "9";
                string NO_PERMINTAAN_10 = "10";

                string PERMINTAAN_PERBAIKAN_1 = text_permintaanperbaikan1.Text;
                string PERMINTAAN_PERBAIKAN_2_B = text_permintaanperbaikan2.Text;
                string PERMINTAAN_PERBAIKAN_3 = text_permintaanperbaikan3.Text;
                string PERMINTAAN_PERBAIKAN_4 = text_permintaanperbaikan4.Text;
                string PERMINTAAN_PERBAIKAN_5 = text_permintaanperbaikan5.Text;
                string PERMINTAAN_PERBAIKAN_6 = text_permintaanperbaikan6.Text;
                string PERMINTAAN_PERBAIKAN_7 = text_permintaanperbaikan7.Text;
                string PERMINTAAN_PERBAIKAN_8 = text_permintaanperbaikan8.Text;
                string PERMINTAAN_PERBAIKAN_9 = text_permintaanperbaikan9.Text;
                string PERMINTAAN_PERBAIKAN_10 = text_permintaanperbaikan10.Text;
                string PERMINTAAN_PERBAIKAN_2_1 = text_descriptionrepair2_1.Text;
                string PERMINTAAN_PERBAIKAN_2_2 = text_descriptionrepair2_2.Text;
                string PERMINTAAN_PERBAIKAN_2_3 = text_descriptionrepair2_3.Text;
                string PERMINTAAN_PERBAIKAN_2_4 = text_descriptionrepair2_4.Text;
                string PERMINTAAN_PERBAIKAN_2_5 = text_descriptionrepair2_5.Text;
                string PERMINTAAN_PERBAIKAN_2_6 = text_descriptionrepair2_6.Text;
                string PERMINTAAN_PERBAIKAN_2_7 = text_descriptionrepair2_7.Text;
                string PERMINTAAN_PERBAIKAN_2_8 = text_descriptionrepair2_8.Text;
                string PERMINTAAN_PERBAIKAN_2_9 = text_descriptionrepair2_9.Text;
                string PERMINTAAN_PERBAIKAN_2_10 = text_descriptionrepair2_10.Text;



                text_budgets1.Text = "0";
                text_budgets2.Text = "0";
                text_budgets3.Text = "0";
                text_budgets4.Text = "0";
                text_budgets5.Text = "0";
                text_budgets6.Text = "0";
                text_budgets7.Text = "0";
                text_budgets8.Text = "0";
                text_budgets9.Text = "0";
                text_budgets10.Text = "0";

                string PIC_PERBAIKAN_1 = ddlpicperbaikan1.Text;
                string PIC_PERBAIKAN_2 = ddlpicperbaikan2.Text;
                string PIC_PERBAIKAN_3 = ddlpicperbaikan3.Text;
                string PIC_PERBAIKAN_4 = ddlpicperbaikan4.Text;
                string PIC_PERBAIKAN_5 = ddlpicperbaikan5.Text;
                string PIC_PERBAIKAN_6 = ddlpicperbaikan6.Text;
                string PIC_PERBAIKAN_7 = ddlpicperbaikan7.Text;
                string PIC_PERBAIKAN_8 = ddlpicperbaikan8.Text;
                string PIC_PERBAIKAN_9 = ddlpicperbaikan9.Text;
                string PIC_PERBAIKAN_10 = ddlpicperbaikan10.Text;


                //string TGL_SELESAI = "";
                string Where = string.Format("NO_FORM = '{0}'", HfNO_FORM.Value);
                Ds = TrForm5RepairPermintaan.GetDataFilter(Where);

                int i = 0;
                int index = 0;
                foreach (DataRow Item in Ds.Tables[0].Rows)
                {
                    index = i;
                    i++;

                    if (!String.IsNullOrEmpty((Item.Field<String>("NO_PERMINTAAN"))))
                    {
                        NO_PERMINTAAN = Item.Field<String>("NO_PERMINTAAN");
                    }
                    else
                    {
                        NO_PERMINTAAN = "";
                    }


                    if (!String.IsNullOrEmpty((Item.Field<String>("PERMINTAAN_PERBAIKAN"))))
                    {
                        PERMINTAAN_PERBAIKAN = Item.Field<String>("PERMINTAAN_PERBAIKAN");
                    }
                    else
                    {
                        PERMINTAAN_PERBAIKAN = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("PERMINTAAN_PERBAIKAN_2"))))
                    {
                        PERMINTAAN_PERBAIKAN_2 = Item.Field<String>("PERMINTAAN_PERBAIKAN_2");
                    }
                    else
                    {
                        PERMINTAAN_PERBAIKAN_2 = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("PIC"))))
                    {
                        PIC = Item.Field<String>("PIC");
                    }
                    else
                    {
                        PIC = "Choose";
                    }

                    DateTime? COMPLETE_DATE = Item.Field<DateTime?>("COMPLETE_DATE");
                    if (COMPLETE_DATE == null)
                    {
                        COMPLETE_DATE = new DateTime(1900, 01, 01);
                    }

                    DateTime? ACTUAL_FINISH_DATE = Item.Field<DateTime?>("ACTUAL_FINISH_DATE");
                    if (ACTUAL_FINISH_DATE == null)
                    {
                        ACTUAL_FINISH_DATE = new DateTime(1900, 01, 01);
                    }

                    decimal? BUDGET = Item.Field<decimal?>("BUDGET");
                    if (BUDGET.HasValue)
                    {
                        BUDGET = Item.Field<decimal>("BUDGET");
                    }
                    else
                    {
                        BUDGET = 0;

                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("UPLOAD_FILE"))))
                    {
                        UPLOAD_FILE = Item.Field<String>("UPLOAD_FILE");
                    }
                    else
                    {
                        UPLOAD_FILE = "";
                    }

                    if (PERMINTAAN_PERBAIKAN_2_1 != "" && NO_PERMINTAAN == "1")
                    {
                        //ddlpicperbaikan1.Text = PIC;
                        if (PIC == HfUsername.Value)
                        {
                            text_actualfinishdate1.Enabled = true;
                        }
                    }

                    if (PERMINTAAN_PERBAIKAN_2_2 != "" && NO_PERMINTAAN == "2")
                    {
                        //ddlpicperbaikan1.Text = PIC;
                        if (PIC == HfUsername.Value)
                        {
                            text_actualfinishdate2.Enabled = true;
                        }
                    }

                    if (PERMINTAAN_PERBAIKAN_2_3 != "" && NO_PERMINTAAN == "3")
                    {
                        //ddlpicperbaikan1.Text = PIC;
                        if (PIC == HfUsername.Value)
                        {
                            text_actualfinishdate3.Enabled = true;
                        }
                    }

                    if (PERMINTAAN_PERBAIKAN_2_4 != "" && NO_PERMINTAAN == "4")
                    {
                        //ddlpicperbaikan1.Text = PIC;
                        if (PIC == HfUsername.Value)
                        {
                            text_actualfinishdate4.Enabled = true;
                        }
                    }

                    if (PERMINTAAN_PERBAIKAN_2_5 != "" && NO_PERMINTAAN == "5")
                    {
                        //ddlpicperbaikan1.Text = PIC;
                        if (PIC == HfUsername.Value)
                        {
                            text_actualfinishdate5.Enabled = true;
                        }
                    }

                    if (PERMINTAAN_PERBAIKAN_2_6 != "" && NO_PERMINTAAN == "6")
                    {
                        //ddlpicperbaikan1.Text = PIC;
                        if (PIC == HfUsername.Value)
                        {
                            text_actualfinishdate6.Enabled = true;
                        }
                    }

                    if (PERMINTAAN_PERBAIKAN_2_7 != "" && NO_PERMINTAAN == "7")
                    {
                        //ddlpicperbaikan1.Text = PIC;
                        if (PIC == HfUsername.Value)
                        {
                            text_actualfinishdate7.Enabled = true;
                        }
                    }

                    if (PERMINTAAN_PERBAIKAN_2_8 != "" && NO_PERMINTAAN == "8")
                    {
                        //ddlpicperbaikan1.Text = PIC;
                        if (PIC == HfUsername.Value)
                        {
                            text_actualfinishdate8.Enabled = true;
                        }
                    }

                    if (PERMINTAAN_PERBAIKAN_2_9 != "" && NO_PERMINTAAN == "9")
                    {
                        //ddlpicperbaikan1.Text = PIC;
                        if (PIC == HfUsername.Value)
                        {
                            text_actualfinishdate9.Enabled = true;
                        }
                    }

                    if (PERMINTAAN_PERBAIKAN_2_10 != "" && NO_PERMINTAAN == "10")
                    {
                        //ddlpicperbaikan1.Text = PIC;
                        if (PIC == HfUsername.Value)
                        {
                            text_actualfinishdate10.Enabled = true;
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

                if (KodeJabatan == "PROJECT" && Status == EApprovalStatus.OnWorking)
                {
                    ACTIONGENERAL = "Done";

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
                    else if (URUTAN == 3)
                    {
                        int checkokay = 0;

                        if (text_descriptionrepair2_1.Text != "" || ddlpicperbaikan1.Text != "Choose" || text_budgets1.Text != "" || text_completedate1.Text != "" || text_descriptionrepair2_2.Text != "" || ddlpicperbaikan2.Text != "Choose" || text_budgets2.Text != "" || text_completedate2.Text != "" || text_descriptionrepair2_3.Text != "" || ddlpicperbaikan3.Text != "Choose" || text_budgets3.Text != "" || text_completedate3.Text != "" || text_descriptionrepair2_4.Text != "" || ddlpicperbaikan4.Text != "Choose" || text_budgets4.Text != "" || text_completedate4.Text != "" || text_descriptionrepair2_5.Text != "" || ddlpicperbaikan5.Text != "Choose" || text_budgets5.Text != "" || text_completedate5.Text != "" || text_descriptionrepair2_6.Text != "" || ddlpicperbaikan6.Text != "Choose" || text_budgets6.Text != "" || text_completedate6.Text != "" || text_descriptionrepair2_7.Text != "" || ddlpicperbaikan7.Text != "Choose" || text_budgets7.Text != "" || text_completedate7.Text != "" || text_descriptionrepair2_8.Text != "" || ddlpicperbaikan8.Text != "Choose" || text_budgets8.Text != "" || text_completedate8.Text != "" || text_descriptionrepair2_9.Text != "" || ddlpicperbaikan9.Text != "Choose" || text_budgets9.Text != "" || text_completedate9.Text != "" || text_descriptionrepair2_10.Text != "" || ddlpicperbaikan10.Text != "Choose" || text_budgets10.Text != "" || text_completedate10.Text != "")
                        {

                            //1
                            if (text_descriptionrepair2_1.Text != "" || ddlpicperbaikan1.Text != "Choose" || (text_budgets1.Text != "0") || text_completedate1.Text != "")
                            {
                                if (text_descriptionrepair2_1.Text != "")
                                {
                                    if (ddlpicperbaikan1.Text != "Choose")
                                    {
                                        if (text_budgets1.Text != "")
                                        {
                                            if (text_completedate1.Text != "")
                                            {
                                                checkokay = 1;
                                            }
                                            else
                                            {
                                                checkokay = 0;
                                                DivMessage.InnerText = "Complete Date 1 Cannot Be Empty";
                                                DivMessage.Attributes["class"] = "error";
                                                //DivMessage.Attributes["class"] = "success";
                                                DivMessage.Visible = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            checkokay = 0;
                                            DivMessage.InnerText = "Budget 1 Cannot Be Empty";
                                            DivMessage.Attributes["class"] = "error";
                                            //DivMessage.Attributes["class"] = "success";
                                            DivMessage.Visible = true;
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        checkokay = 0;
                                        DivMessage.InnerText = "Pic Selected 1 Cannot Be Empty";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                }
                                else
                                {
                                    checkokay = 0;
                                    DivMessage.InnerText = "Repair Request Project 1 Cannot Be Empty";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                    return;
                                }
                            }



                            //2
                            if (text_descriptionrepair2_2.Text != "" || ddlpicperbaikan2.Text != "Choose" || (text_budgets2.Text != "0") || text_completedate2.Text != "")
                            {
                                if (text_descriptionrepair2_2.Text != "")
                                {
                                    if (ddlpicperbaikan2.Text != "Choose")
                                    {
                                        if (text_budgets2.Text != "")
                                        {
                                            if (text_completedate2.Text != "")
                                            {
                                                checkokay = 2;
                                            }
                                            else
                                            {
                                                checkokay = 0;
                                                DivMessage.InnerText = "Complete Date 2 Cannot Be Empty";
                                                DivMessage.Attributes["class"] = "error";
                                                //DivMessage.Attributes["class"] = "success";
                                                DivMessage.Visible = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            checkokay = 0;
                                            DivMessage.InnerText = "Budget 2 Cannot Be Empty";
                                            DivMessage.Attributes["class"] = "error";
                                            //DivMessage.Attributes["class"] = "success";
                                            DivMessage.Visible = true;
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        checkokay = 0;
                                        DivMessage.InnerText = "Pic Selected 2 Cannot Be Empty";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                }
                                else
                                {
                                    checkokay = 0;
                                    DivMessage.InnerText = "Repair Request Project 2 Cannot Be Empty";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                    return;
                                }
                            }


                            //3
                            if (text_descriptionrepair2_3.Text != "" || ddlpicperbaikan3.Text != "Choose" || (text_budgets3.Text != "0") || text_completedate3.Text != "")
                            {
                                if (text_descriptionrepair2_3.Text != "")
                                {
                                    if (ddlpicperbaikan3.Text != "Choose")
                                    {
                                        if (text_budgets3.Text != "")
                                        {
                                            if (text_completedate3.Text != "")
                                            {
                                                checkokay = 3;
                                            }
                                            else
                                            {
                                                checkokay = 0;
                                                DivMessage.InnerText = "Complete Date 3 Cannot Be Empty";
                                                DivMessage.Attributes["class"] = "error";
                                                //DivMessage.Attributes["class"] = "success";
                                                DivMessage.Visible = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            checkokay = 0;
                                            DivMessage.InnerText = "Budget 3 Cannot Be Empty";
                                            DivMessage.Attributes["class"] = "error";
                                            //DivMessage.Attributes["class"] = "success";
                                            DivMessage.Visible = true;
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        checkokay = 0;
                                        DivMessage.InnerText = "Pic Selected 3 Cannot Be Empty";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                }
                                else
                                {
                                    checkokay = 0;
                                    DivMessage.InnerText = "Repair Request Project 3 Cannot Be Empty";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                    return;
                                }
                            }


                            //4
                            if (text_descriptionrepair2_4.Text != "" || ddlpicperbaikan4.Text != "Choose" || (text_budgets4.Text != "0") || text_completedate4.Text != "")
                            {
                                if (text_descriptionrepair2_4.Text != "")
                                {
                                    if (ddlpicperbaikan4.Text != "Choose")
                                    {
                                        if (text_budgets4.Text != "")
                                        {
                                            if (text_completedate4.Text != "")
                                            {
                                                checkokay = 4;
                                            }
                                            else
                                            {
                                                checkokay = 0;
                                                DivMessage.InnerText = "Complete Date 4 Cannot Be Empty";
                                                DivMessage.Attributes["class"] = "error";
                                                //DivMessage.Attributes["class"] = "success";
                                                DivMessage.Visible = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            checkokay = 0;
                                            DivMessage.InnerText = "Budget 4 Cannot Be Empty";
                                            DivMessage.Attributes["class"] = "error";
                                            //DivMessage.Attributes["class"] = "success";
                                            DivMessage.Visible = true;
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        checkokay = 0;
                                        DivMessage.InnerText = "Pic Selected 4 Cannot Be Empty";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                }
                                else
                                {
                                    checkokay = 0;
                                    DivMessage.InnerText = "Repair Request Project 4 Cannot Be Empty";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                    return;
                                }
                            }


                            //5
                            if (text_descriptionrepair2_5.Text != "" || ddlpicperbaikan5.Text != "Choose" || (text_budgets5.Text != "0") || text_completedate5.Text != "")
                            {
                                if (text_descriptionrepair2_5.Text != "")
                                {
                                    if (ddlpicperbaikan5.Text != "Choose")
                                    {
                                        if (text_budgets5.Text != "")
                                        {
                                            if (text_completedate5.Text != "")
                                            {
                                                checkokay = 5;
                                            }
                                            else
                                            {
                                                checkokay = 0;
                                                DivMessage.InnerText = "Complete Date 5 Cannot Be Empty";
                                                DivMessage.Attributes["class"] = "error";
                                                //DivMessage.Attributes["class"] = "success";
                                                DivMessage.Visible = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            checkokay = 0;
                                            DivMessage.InnerText = "Budget 5 Cannot Be Empty";
                                            DivMessage.Attributes["class"] = "error";
                                            //DivMessage.Attributes["class"] = "success";
                                            DivMessage.Visible = true;
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        checkokay = 0;
                                        DivMessage.InnerText = "Pic Selected 5 Cannot Be Empty";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                }
                                else
                                {
                                    checkokay = 0;
                                    DivMessage.InnerText = "Repair Request Project 5 Cannot Be Empty";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                    return;
                                }
                            }


                            //6
                            if (text_descriptionrepair2_6.Text != "" || ddlpicperbaikan6.Text != "Choose" || (text_budgets6.Text != "0") || text_completedate6.Text != "")
                            {
                                if (text_descriptionrepair2_6.Text != "")
                                {
                                    if (ddlpicperbaikan6.Text != "Choose")
                                    {
                                        if (text_budgets6.Text != "")
                                        {
                                            if (text_completedate6.Text != "")
                                            {
                                                checkokay = 6;
                                            }
                                            else
                                            {
                                                checkokay = 0;
                                                DivMessage.InnerText = "Complete Date 6 Cannot Be Empty";
                                                DivMessage.Attributes["class"] = "error";
                                                //DivMessage.Attributes["class"] = "success";
                                                DivMessage.Visible = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            checkokay = 0;
                                            DivMessage.InnerText = "Budget 6 Cannot Be Empty";
                                            DivMessage.Attributes["class"] = "error";
                                            //DivMessage.Attributes["class"] = "success";
                                            DivMessage.Visible = true;
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        checkokay = 0;
                                        DivMessage.InnerText = "Pic Selected 6 Cannot Be Empty";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                }
                                else
                                {
                                    checkokay = 0;
                                    DivMessage.InnerText = "Repair Request Project 6 Cannot Be Empty";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                    return;
                                }
                            }


                            //7
                            if (text_descriptionrepair2_7.Text != "" || ddlpicperbaikan7.Text != "Choose" || (text_budgets7.Text != "0") || text_completedate7.Text != "")
                            {
                                if (text_descriptionrepair2_7.Text != "")
                                {

                                    if (ddlpicperbaikan7.Text != "Choose")
                                    {
                                        if (text_budgets7.Text != "")
                                        {
                                            if (text_completedate7.Text != "")
                                            {
                                                checkokay = 7;
                                            }
                                            else
                                            {
                                                checkokay = 0;
                                                DivMessage.InnerText = "Complete Date 7 Cannot Be Empty";
                                                DivMessage.Attributes["class"] = "error";
                                                //DivMessage.Attributes["class"] = "success";
                                                DivMessage.Visible = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            checkokay = 0;
                                            DivMessage.InnerText = "Budget 7 Cannot Be Empty";
                                            DivMessage.Attributes["class"] = "error";
                                            //DivMessage.Attributes["class"] = "success";
                                            DivMessage.Visible = true;
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        checkokay = 0;
                                        DivMessage.InnerText = "Pic Selected 7 Cannot Be Empty";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                }
                                else
                                {
                                    checkokay = 0;
                                    DivMessage.InnerText = "Repair Request Project 7 Cannot Be Empty";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                    return;
                                }
                            }


                            //8
                            if (text_descriptionrepair2_8.Text != "" || ddlpicperbaikan8.Text != "Choose" || (text_budgets8.Text != "0") || text_completedate8.Text != "")
                            {
                                if (text_descriptionrepair2_8.Text != "")
                                {

                                    if (ddlpicperbaikan8.Text != "Choose")
                                    {
                                        if (text_budgets8.Text != "")
                                        {
                                            if (text_completedate8.Text != "")
                                            {
                                                checkokay = 8;
                                            }
                                            else
                                            {
                                                checkokay = 0;
                                                DivMessage.InnerText = "Complete Date 8 Cannot Be Empty";
                                                DivMessage.Attributes["class"] = "error";
                                                //DivMessage.Attributes["class"] = "success";
                                                DivMessage.Visible = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            checkokay = 0;
                                            DivMessage.InnerText = "Budget 8 Cannot Be Empty";
                                            DivMessage.Attributes["class"] = "error";
                                            //DivMessage.Attributes["class"] = "success";
                                            DivMessage.Visible = true;
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        checkokay = 0;
                                        DivMessage.InnerText = "Pic Selected 8 Cannot Be Empty";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                }
                                else
                                {
                                    checkokay = 0;
                                    DivMessage.InnerText = "Repair Request Project 8 Cannot Be Empty";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                    return;
                                }
                            }


                            //9
                            if (text_descriptionrepair2_9.Text != "" || ddlpicperbaikan9.Text != "Choose" || (text_budgets9.Text != "0") || text_completedate9.Text != "")
                            {
                                if (text_descriptionrepair2_9.Text != "")
                                {

                                    if (ddlpicperbaikan9.Text != "Choose")
                                    {
                                        if (text_budgets9.Text != "")
                                        {
                                            if (text_completedate9.Text != "")
                                            {
                                                checkokay = 9;
                                            }
                                            else
                                            {
                                                checkokay = 0;
                                                DivMessage.InnerText = "Complete Date 9 Cannot Be Empty";
                                                DivMessage.Attributes["class"] = "error";
                                                //DivMessage.Attributes["class"] = "success";
                                                DivMessage.Visible = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            checkokay = 0;
                                            DivMessage.InnerText = "Budget 9 Cannot Be Empty";
                                            DivMessage.Attributes["class"] = "error";
                                            //DivMessage.Attributes["class"] = "success";
                                            DivMessage.Visible = true;
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        checkokay = 0;
                                        DivMessage.InnerText = "Pic Selected 9 Cannot Be Empty";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                }
                                else
                                {
                                    checkokay = 0;
                                    DivMessage.InnerText = "Repair Request Project 9 Cannot Be Empty";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                    return;
                                }
                            }


                            //10
                            if (text_descriptionrepair2_10.Text != "" || ddlpicperbaikan10.Text != "Choose" || (text_budgets10.Text != "0") || text_completedate10.Text != "")
                            {
                                if (text_descriptionrepair2_10.Text != "")
                                {
                                    if (ddlpicperbaikan10.Text != "Choose")
                                    {
                                        if (text_budgets10.Text != "")
                                        {
                                            if (text_completedate10.Text != "")
                                            {
                                                checkokay = 10;
                                            }
                                            else
                                            {
                                                checkokay = 0;
                                                DivMessage.InnerText = "Complete Date 10 Cannot Be Empty";
                                                DivMessage.Attributes["class"] = "error";
                                                //DivMessage.Attributes["class"] = "success";
                                                DivMessage.Visible = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            checkokay = 0;
                                            DivMessage.InnerText = "Budget 10 Cannot Be Empty";
                                            DivMessage.Attributes["class"] = "error";
                                            //DivMessage.Attributes["class"] = "success";
                                            DivMessage.Visible = true;
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        checkokay = 0;
                                        DivMessage.InnerText = "Pic Selected 10 Cannot Be Empty";
                                        DivMessage.Attributes["class"] = "error";
                                        //DivMessage.Attributes["class"] = "success";
                                        DivMessage.Visible = true;
                                        return;
                                    }
                                }
                                else
                                {
                                    checkokay = 0;
                                    DivMessage.InnerText = "Repair Request Project 10 Cannot Be Empty";
                                    DivMessage.Attributes["class"] = "error";
                                    //DivMessage.Attributes["class"] = "success";
                                    DivMessage.Visible = true;
                                    return;
                                }


                            }


                        }

                        if (checkokay != 0)
                        {
                            UpdateStatusApprovedProject();
                            SendEmailAllType();
                        }

                    }
                    else if (URUTAN == 4)
                    {
                        UpdateStatusApprovedBrandManager();
                        SendEmailAllType();
                    }
                    else if (URUTAN == 5)
                    {
                        UpdateStatusApprovedComercialDirector();
                        SendEmailAllType();
                    }
                    else if (URUTAN == 6)
                    {
                        if ((text_overbudgetvalue.Text == "" || text_overbudgetvalue.Text == "0.00" || text_overbudgetvalue.Text == "0") || label_statusvalue.Text == EApprovalStatus.ApprovedComercialDirectorBudget)
                        {
                            UpdateStatusApprovedBudgetControl();
                            SendEmailAllType();
                        }
                        else
                        {
                            DivMessage.InnerText = "Over Budget Must Be Empty Or 0";
                            DivMessage.Attributes["class"] = "error";
                            //DivMessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;
                        }
                    }
                    else if (URUTAN == 7)
                    {
                        UpdateStatusApprovedCreativeManager();
                        SendEmailAllType();
                        SendEmailPICRepairForm();
                    }
                    else if (URUTAN == 8)
                    {
                        UpdateStatusApprovedAdminMaintenanceProjectNormal();
                        //SendEmailAllType();

                        TR_FORM5_REPAIR_PERMINTAAN_DA TrForm5RepairPermintaan = new DataLayer.TR_FORM5_REPAIR_PERMINTAAN_DA();
                        DataSet DsPermintaan = new DataSet();

                        int angkaok = 0;
                        //string TGL_SELESAI = "";
                        string WherePermintaan = string.Format("NO_FORM = '{0}' AND ACTUAL_FINISH_DATE = '1900-01-01'", HfNO_FORM.Value);
                        DsPermintaan = TrForm5RepairPermintaan.GetDataFilter(WherePermintaan);

                        //int i = 0;
                        //int index = 0;
                        //foreach (DataRow Item in DsPermintaan.Tables[0].Rows)
                        //{
                        //    index = i;
                        //    i++;

                        //    DateTime? ACTUAL_FINISH_DATE = Item.Field<DateTime?>("ACTUAL_FINISH_DATE");
                        //    if (ACTUAL_FINISH_DATE == Convert.ToDateTime("1900-01-01"))
                        //    {
                        //        ACTUAL_FINISH_DATE = new DateTime(1900, 01, 01);
                        //        angkaok = 0;
                        //    }
                        //    else
                        //    {
                        //        angkaok = 1;
                        //    }

                        //}

                        if (DsPermintaan.Tables[0].Rows.Count > 0)
                        {
                            angkaok = 0;
                        }
                        else
                        {
                            angkaok = 1;
                        }

                        if (angkaok == 1)
                        {

                            UpdateStatusApprovedAdminMaintenanceProjectDone();
                            SendEmailAllType();
                        }
                        else
                        {
                            UpdateStatusApprovedAdminMaintenanceProjectNormal();
                            SendEmailAllType();
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
                if (KodeJabatan == "PROJECT" && Status == EApprovalStatus.OnWorking)
                {
                    ACTIONGENERAL = "Done";

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
                        UpdateStatusCancelProject();
                        SendEmailAllType();
                    }
                    else if (URUTAN == 4)
                    {
                        UpdateStatusCancelBrandManager();
                        SendEmailAllType();
                    }
                    else if (URUTAN == 5)
                    {
                        UpdateStatusCancelComercialDirector();
                        SendEmailAllType();
                    }
                    else if (URUTAN == 6)
                    {

                    }
                    else if (URUTAN == 7)
                    {
                        UpdateStatusCancelCreativeManager();
                        SendEmailAllType();
                    }
                    else if (URUTAN == 99)
                    {
                        UpdateStatusCancelStoreDesign();
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

                //Mengambil Posisi CM. Karena CM Ada terdapat 3 action sekaligus.
                string KodeJabatan = "";

                DsUser = MsUserDA.GetDataFilter(string.Format("USERNAME = '{0}'", HfUsername.Value));
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    KodeJabatan = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                }

                string Status = label_statusvalue.Text;
                string ACTIONGENERAL = "";
                if (KodeJabatan == "PROJECT" && Status == EApprovalStatus.OnWorking)
                {
                    ACTIONGENERAL = "Done";

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
                        UpdateStatusRevisiProject();
                        SendEmailAllType();

                    }
                    else if (URUTAN == 4)
                    {

                    }
                    else if (URUTAN == 5)
                    {
                        //

                    }
                    else if (URUTAN == 6)
                    {
                        if (text_overbudgetvalue.Text != "" && (text_overbudgetvalue.Text != "0" && text_overbudgetvalue.Text != "0.00"))
                        {
                            UpdateStatusRevisiBudgetControl();
                            SendEmailAllType();
                        }
                        else
                        {
                            DivMessage.InnerText = "Value Over Budget must be filled.";
                            DivMessage.Attributes["class"] = "error";
                            //DivMessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;
                        }

                    }
                    else if (URUTAN == 7)
                    {

                    }
                    else if (URUTAN == 99)
                    {
                        UpdateStatusRevisiStoreDesign();
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
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string REQUEST_FOR = "-";

                if (radio_relocation.Checked == true)
                {
                    REQUEST_FOR = "Relocation";
                }
                else if (radio_renovation.Checked == true)
                {
                    REQUEST_FOR = "Renovation";
                }
                else if (radio_repair.Checked == true)
                {
                    REQUEST_FOR = "Repair/Maintenance";
                }
                else if (radio_additional.Checked == true)
                {
                    REQUEST_FOR = "Additional";
                }
                string MENYETUJUI1 = text_menyetujui1.Text;
                DateTime TGL_MENYETUJUI1 = DateTime.Now;
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


                //if (REQUEST_FOR == "1")
                if (REQUEST_FOR == "Relocation" || REQUEST_FOR == "Renovation")
                {
                    STATUS = EApprovalStatus.ApprovedHDSD;
                    DESCRIPTION = "Update Status From " + EApprovalStatus.OnApprovedHD + " To " + EApprovalStatus.ApprovedHDSD;

                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    string Where = string.Format("KD_JABATAN = 'STORE-DESIGN'");
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

                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    string Where = string.Format("KD_JABATAN = 'PROJECT'");
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

                TR_FORM5_REPAIR TrForm5repair = new TR_FORM5_REPAIR();
                TrForm5repair.NO_FORM = NO_FORM;
                TrForm5repair.MENYETUJUI1 = MENYETUJUI1;
                TrForm5repair.TGL_MENYETUJUI1 = TGL_MENYETUJUI1;
                TrForm5repair.STATUS = STATUS;
                //TrForm3gdr.REVISI = "";

                TrForm5repair.USER_CURRENT = HfUsername.Value;
                TrForm5repair.NEXT_USER = USERNEXT;
                TrForm5repair.URUTAN_USER_CURRENT = URUTAN;
                TrForm5repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5RepairDA.UpdateMenyetujui1(TrForm5repair);

                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM;
                TrForm5gdractivity.STATUS = STATUS;
                TrForm5gdractivity.DESCRIPTION = DESCRIPTION;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 2;
                TrForm5gdractivity.SP = "";
                TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm5gdractivity.NEXT_USER = USERNEXT;
                TrForm5gdractivity.URUTAN_USER_CURRENT = URUTAN;
                TrForm5gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5GdrActivity.Insert(TrForm5gdractivity);


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

        //Action Project
        public void UpdateStatusApprovedProject()
        {
            try
            {
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;

                string DITERIMA_1 = text_diterima1.Text;
                DateTime TGL_DITERIMA_1 = DateTime.Now;


                TR_FORM5_REPAIR TrForm5repair = new TR_FORM5_REPAIR();
                TrForm5repair.NO_FORM = NO_FORM;
                TrForm5repair.DITERIMA_1 = DITERIMA_1;
                TrForm5repair.TGL_DITERIMA_1 = TGL_DITERIMA_1;
                TrForm5repair.STATUS = EApprovalStatus.ApprovedProject;
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

                string Where = string.Format("KD_JABATAN = 'BC'");
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
                TrForm5repair.USER_CURRENT = HfUsername.Value;
                TrForm5repair.NEXT_USER = USERNEXT;
                TrForm5repair.URUTAN_USER_CURRENT = URUTAN;
                TrForm5repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5RepairDA.UpdateDiterima1(TrForm5repair);

                UpdateDetailPermintaanPerbaikanProject();

                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM;
                TrForm5gdractivity.STATUS = EApprovalStatus.ApprovedProject;
                TrForm5gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.ApprovedHD + " To " + EApprovalStatus.ApprovedProject;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 3;
                TrForm5gdractivity.SP = "";
                TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm5gdractivity.NEXT_USER = USERNEXT;
                TrForm5gdractivity.URUTAN_USER_CURRENT = URUTAN;
                TrForm5gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5GdrActivity.Insert(TrForm5gdractivity);


                DivMessage.InnerText = "Data Successful Approved Project";
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

        //Action Budget Control
        public void UpdateStatusApprovedBudgetControl()
        {
            try
            {
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;

                string DITERIMA_2 = text_diterima2.Text;
                DateTime TGL_DITERIMA_2 = DateTime.Now;

                TR_FORM5_REPAIR TrForm5repair = new TR_FORM5_REPAIR();
                TrForm5repair.NO_FORM = NO_FORM;
                TrForm5repair.OVER_BUDGET = 0;
                TrForm5repair.OVER_BUDGET_REQUEST = EYesNo.No;
                TrForm5repair.DITERIMA_2 = DITERIMA_2;
                TrForm5repair.TGL_DITERIMA_2 = TGL_DITERIMA_2;
                TrForm5repair.STATUS = EApprovalStatus.ApprovedBudgetControl;
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
                TrForm5repair.USER_CURRENT = HfUsername.Value;
                TrForm5repair.NEXT_USER = USERNEXT;
                TrForm5repair.URUTAN_USER_CURRENT = URUTAN;
                TrForm5repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5RepairDA.UpdateDiterima2(TrForm5repair);

                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM;
                TrForm5gdractivity.STATUS = EApprovalStatus.ApprovedBudgetControl;
                TrForm5gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.ApprovedProject + " To " + EApprovalStatus.ApprovedBudgetControl;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 6;
                TrForm5gdractivity.SP = "";
                TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm5gdractivity.NEXT_USER = USERNEXT;
                TrForm5gdractivity.URUTAN_USER_CURRENT = URUTAN;
                TrForm5gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5GdrActivity.Insert(TrForm5gdractivity);


                DivMessage.InnerText = "Data Successful Approved Budget Control";
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

        //Action Creative Director
        public void UpdateStatusApprovedCreativeManager()
        {
            try
            {
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_3 = text_diterima3.Text;
                DateTime TGL_DITERIMA_3 = DateTime.Now;

                TR_FORM5_REPAIR TrForm5repair = new TR_FORM5_REPAIR();
                TrForm5repair.NO_FORM = NO_FORM;
                TrForm5repair.DITERIMA_3 = DITERIMA_3;
                TrForm5repair.TGL_DITERIMA_3 = TGL_DITERIMA_3;
                TrForm5repair.STATUS = EApprovalStatus.OnWorking;
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
                TrForm5repair.USER_CURRENT = HfUsername.Value;
                TrForm5repair.NEXT_USER = "PIC";
                TrForm5repair.URUTAN_USER_CURRENT = URUTAN;
                TrForm5repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5RepairDA.UpdateDiterima3(TrForm5repair);

                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM;
                TrForm5gdractivity.STATUS = EApprovalStatus.OnWorking;
                TrForm5gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.ApprovedBudgetControl + " To " + EApprovalStatus.OnWorking;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 7;
                TrForm5gdractivity.SP = "";
                TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm5gdractivity.NEXT_USER = USERNEXT;
                TrForm5gdractivity.URUTAN_USER_CURRENT = URUTAN;
                TrForm5gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5GdrActivity.Insert(TrForm5gdractivity);


                DivMessage.InnerText = "Data Successful Approved Creative Director";
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

        //Action Admin Maintenance/Project
        public void UpdateStatusApprovedAdminMaintenanceProjectDone()
        {
            try
            {
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_4 = text_diterima4.Text;
                DateTime TGL_DITERIMA_4 = DateTime.Now;

                TR_FORM5_REPAIR TrForm5repair = new TR_FORM5_REPAIR();
                TrForm5repair.NO_FORM = NO_FORM;
                TrForm5repair.DITERIMA_4 = DITERIMA_4;
                TrForm5repair.TGL_DITERIMA_4 = TGL_DITERIMA_4;
                TrForm5repair.STATUS = EApprovalStatus.Done;
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

                string Where = string.Format("KD_JABATAN = 'PROJECT'");
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
                TrForm5repair.USER_CURRENT = HfUsername.Value;
                TrForm5repair.NEXT_USER = USERNEXT;
                TrForm5repair.URUTAN_USER_CURRENT = URUTAN;
                TrForm5repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5RepairDA.UpdateDiterima4(TrForm5repair);

                UpdateDetailPermintaanPerbaikanActualDate();

                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM;
                TrForm5gdractivity.STATUS = EApprovalStatus.Done;
                TrForm5gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.OnWorking + " To " + EApprovalStatus.Done;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 8;
                TrForm5gdractivity.SP = "";
                TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm5gdractivity.NEXT_USER = USERNEXT;
                TrForm5gdractivity.URUTAN_USER_CURRENT = URUTAN;
                TrForm5gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5GdrActivity.Insert(TrForm5gdractivity);


                DivMessage.InnerText = "Data Successful finish Admin Maintenance/Project";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));




            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        public void UpdateStatusApprovedAdminMaintenanceProjectNormal()
        {
            try
            {
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_4 = text_diterima4.Text;
                DateTime TGL_DITERIMA_4 = DateTime.Now;

                TR_FORM5_REPAIR TrForm5repair = new TR_FORM5_REPAIR();
                TrForm5repair.NO_FORM = NO_FORM;
                TrForm5repair.DITERIMA_4 = DITERIMA_4;
                TrForm5repair.TGL_DITERIMA_4 = TGL_DITERIMA_4;
                TrForm5repair.STATUS = EApprovalStatus.OnWorking;
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

                string Where = string.Format("KD_JABATAN = 'PROJECT'");
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
                TrForm5repair.USER_CURRENT = HfUsername.Value;
                TrForm5repair.NEXT_USER = USERNEXT;
                TrForm5repair.URUTAN_USER_CURRENT = URUTAN;
                TrForm5repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5RepairDA.UpdateDiterima4(TrForm5repair);

                UpdateDetailPermintaanPerbaikanActualDate();

                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM;
                TrForm5gdractivity.STATUS = EApprovalStatus.OnWorking;
                TrForm5gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.OnWorking + " To " + EApprovalStatus.OnWorking;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 8;
                TrForm5gdractivity.SP = "";
                TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm5gdractivity.NEXT_USER = USERNEXT;
                TrForm5gdractivity.URUTAN_USER_CURRENT = URUTAN;
                TrForm5gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5GdrActivity.Insert(TrForm5gdractivity);


                DivMessage.InnerText = "Data Successful finish Admin Maintenance/Project";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../MainMenu.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));

            }

            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        //Approval Over Budget

        //Action Brand Manager
        public void UpdateStatusApprovedBrandManager()
        {
            try
            {
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_LAIN_1 = text_diterimalain1.Text;
                DateTime TGL_DITERIMA_LAIN_1 = DateTime.Now;

                TR_FORM5_REPAIR TrForm5repair = new TR_FORM5_REPAIR();
                TrForm5repair.NO_FORM = NO_FORM;
                TrForm5repair.DITERIMA_LAIN_1 = DITERIMA_LAIN_1;
                TrForm5repair.TGL_DITERIMA_LAIN_1 = TGL_DITERIMA_LAIN_1;
                TrForm5repair.STATUS = EApprovalStatus.ApprovedBMBudget;
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

                string Where = string.Format("KD_JABATAN = 'CLD'");
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
                TrForm5repair.USER_CURRENT = HfUsername.Value;
                TrForm5repair.NEXT_USER = USERNEXT;
                TrForm5repair.URUTAN_USER_CURRENT = URUTAN;
                TrForm5repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5RepairDA.UpdateDiterimaLain1(TrForm5repair);

                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM;
                TrForm5gdractivity.STATUS = EApprovalStatus.ApprovedBMBudget;
                TrForm5gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.OnApprovedHD + " To " + EApprovalStatus.ApprovedBMBudget;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 4;
                TrForm5gdractivity.SP = "";
                TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm5gdractivity.NEXT_USER = USERNEXT;
                TrForm5gdractivity.URUTAN_USER_CURRENT = URUTAN;
                TrForm5gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5GdrActivity.Insert(TrForm5gdractivity);


                DivMessage.InnerText = "Data Successful Approved Brand Manager";
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

        //Action Comercial Director
        public void UpdateStatusApprovedComercialDirector()
        {
            try
            {
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_LAIN_2 = text_diterimalain2.Text;
                DateTime TGL_DITERIMA_LAIN_2 = DateTime.Now;

                TR_FORM5_REPAIR TrForm5repair = new TR_FORM5_REPAIR();
                TrForm5repair.NO_FORM = NO_FORM;
                TrForm5repair.DITERIMA_LAIN_2 = DITERIMA_LAIN_2;
                TrForm5repair.TGL_DITERIMA_LAIN_2 = TGL_DITERIMA_LAIN_2;
                TrForm5repair.STATUS = EApprovalStatus.ApprovedComercialDirectorBudget;
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

                string Where = string.Format("KD_JABATAN = 'BC'");
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
                TrForm5repair.USER_CURRENT = HfUsername.Value;
                TrForm5repair.NEXT_USER = USERNEXT;
                TrForm5repair.URUTAN_USER_CURRENT = URUTAN;
                TrForm5repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5RepairDA.UpdateDiterimaLain2(TrForm5repair);

                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM;
                TrForm5gdractivity.STATUS = EApprovalStatus.ApprovedComercialDirectorBudget;
                TrForm5gdractivity.DESCRIPTION = "Update Status From " + EApprovalStatus.ApprovedBMBudget + " To " + EApprovalStatus.ApprovedComercialDirectorBudget;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 5;
                TrForm5gdractivity.SP = "";
                TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm5gdractivity.NEXT_USER = USERNEXT;
                TrForm5gdractivity.URUTAN_USER_CURRENT = URUTAN;
                TrForm5gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                TrForm5GdrActivity.Insert(TrForm5gdractivity);


                DivMessage.InnerText = "Data Successful Approved Comercial Director";
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
        /// Update Status Cancel Function
        /// </summary>

        public void UpdateStatusCancel()
        {
            try
            {
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DIBUAT = text_dibuat.Text;
                DateTime TGL_DIBUAT = DateTime.Now;

                TR_FORM5_REPAIR TrForm5repair = new TR_FORM5_REPAIR();
                TrForm5repair.NO_FORM = NO_FORM;
                TrForm5repair.DIBUAT = DIBUAT;
                TrForm5repair.TGL_DIBUAT = TGL_DIBUAT;
                TrForm5repair.STATUS = EApprovalStatus.Cancel;
                //TrForm3gdr.REVISI = "";

                TrForm5repair.USER_CURRENT = HfUsername.Value;
                TrForm5repair.NEXT_USER = "-";
                TrForm5repair.URUTAN_USER_CURRENT = 1;
                TrForm5repair.URUTAN_NEXT_USER = 0;
                TrForm5RepairDA.UpdateDibuat(TrForm5repair);


                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM;
                TrForm5gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm5gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 1;
                TrForm5gdractivity.SP = "";
                TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm5gdractivity.NEXT_USER = "-";
                TrForm5gdractivity.URUTAN_USER_CURRENT = 1;
                TrForm5gdractivity.URUTAN_NEXT_USER = 0;
                TrForm5GdrActivity.Insert(TrForm5gdractivity);


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
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string MENYETUJUI1 = text_menyetujui1.Text;
                DateTime TGL_MENYETUJUI1 = DateTime.Now;

                TR_FORM5_REPAIR TrForm5repair = new TR_FORM5_REPAIR();
                TrForm5repair.NO_FORM = NO_FORM;
                TrForm5repair.MENYETUJUI1 = MENYETUJUI1;
                TrForm5repair.TGL_MENYETUJUI1 = TGL_MENYETUJUI1;
                TrForm5repair.STATUS = EApprovalStatus.Cancel;
                //TrForm3gdr.REVISI = "";
                TrForm5repair.USER_CURRENT = HfUsername.Value;
                TrForm5repair.NEXT_USER = "-";
                TrForm5repair.URUTAN_USER_CURRENT = 1;
                TrForm5repair.URUTAN_NEXT_USER = 0;
                TrForm5RepairDA.UpdateMenyetujui1(TrForm5repair);

                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM;
                TrForm5gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm5gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 2;
                TrForm5gdractivity.SP = "";
                TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm5gdractivity.NEXT_USER = "-";
                TrForm5gdractivity.URUTAN_USER_CURRENT = 1;
                TrForm5gdractivity.URUTAN_NEXT_USER = 0;
                TrForm5GdrActivity.Insert(TrForm5gdractivity);

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

        public void UpdateStatusCancelProject()
        {
            try
            {
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_1 = text_diterima1.Text;
                DateTime TGL_DITERIMA_1 = DateTime.Now;

                TR_FORM5_REPAIR TrForm5repair = new TR_FORM5_REPAIR();
                TrForm5repair.NO_FORM = NO_FORM;
                TrForm5repair.DITERIMA_1 = DITERIMA_1;
                TrForm5repair.TGL_DITERIMA_1 = TGL_DITERIMA_1;
                TrForm5repair.STATUS = EApprovalStatus.Cancel;
                //TrForm3gdr.REVISI = "";
                TrForm5repair.USER_CURRENT = HfUsername.Value;
                TrForm5repair.NEXT_USER = "-";
                TrForm5repair.URUTAN_USER_CURRENT = 3;
                TrForm5repair.URUTAN_NEXT_USER = 0;
                TrForm5RepairDA.UpdateDiterima1(TrForm5repair);

                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM;
                TrForm5gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm5gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 3;
                TrForm5gdractivity.SP = "";
                TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm5gdractivity.NEXT_USER = "-";
                TrForm5gdractivity.URUTAN_USER_CURRENT = 1;
                TrForm5gdractivity.URUTAN_NEXT_USER = 0;
                TrForm5GdrActivity.Insert(TrForm5gdractivity);

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

        public void UpdateStatusCancelBrandManager()
        {
            try
            {
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_LAIN_1 = text_diterimalain1.Text;
                DateTime TGL_DITERIMA_LAIN_1 = DateTime.Now;

                TR_FORM5_REPAIR TrForm5repair = new TR_FORM5_REPAIR();
                TrForm5repair.NO_FORM = NO_FORM;
                TrForm5repair.DITERIMA_LAIN_1 = DITERIMA_LAIN_1;
                TrForm5repair.TGL_DITERIMA_LAIN_1 = TGL_DITERIMA_LAIN_1;
                TrForm5repair.STATUS = EApprovalStatus.Cancel;
                //TrForm3gdr.REVISI = "";
                TrForm5repair.USER_CURRENT = HfUsername.Value;
                TrForm5repair.NEXT_USER = "-";
                TrForm5repair.URUTAN_USER_CURRENT = 1;
                TrForm5repair.URUTAN_NEXT_USER = 0;
                TrForm5RepairDA.UpdateDiterimaLain1(TrForm5repair);

                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM;
                TrForm5gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm5gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 4;
                TrForm5gdractivity.SP = "";
                TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm5gdractivity.NEXT_USER = "-";
                TrForm5gdractivity.URUTAN_USER_CURRENT = 1;
                TrForm5gdractivity.URUTAN_NEXT_USER = 0;
                TrForm5GdrActivity.Insert(TrForm5gdractivity);

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

        public void UpdateStatusCancelComercialDirector()
        {
            try
            {
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_LAIN_2 = text_diterimalain2.Text;
                DateTime TGL_DITERIMA_LAIN_2 = DateTime.Now;

                TR_FORM5_REPAIR TrForm5repair = new TR_FORM5_REPAIR();
                TrForm5repair.NO_FORM = NO_FORM;
                TrForm5repair.DITERIMA_LAIN_2 = DITERIMA_LAIN_2;
                TrForm5repair.TGL_DITERIMA_LAIN_2 = TGL_DITERIMA_LAIN_2;
                TrForm5repair.STATUS = EApprovalStatus.Cancel;
                //TrForm3gdr.REVISI = "";
                TrForm5repair.USER_CURRENT = HfUsername.Value;
                TrForm5repair.NEXT_USER = "-";
                TrForm5repair.URUTAN_USER_CURRENT = 1;
                TrForm5repair.URUTAN_NEXT_USER = 0;
                TrForm5RepairDA.UpdateDiterimaLain2(TrForm5repair);

                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM;
                TrForm5gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm5gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 5;
                TrForm5gdractivity.SP = "";
                TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm5gdractivity.NEXT_USER = "-";
                TrForm5gdractivity.URUTAN_USER_CURRENT = 1;
                TrForm5gdractivity.URUTAN_NEXT_USER = 0;
                TrForm5GdrActivity.Insert(TrForm5gdractivity);

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
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_LAIN_2 = text_diterimalain2.Text;
                DateTime TGL_DITERIMA_LAIN_2 = DateTime.Now;

                TR_FORM5_REPAIR TrForm5repair = new TR_FORM5_REPAIR();
                TrForm5repair.NO_FORM = NO_FORM;
                TrForm5repair.DITERIMA_LAIN_2 = DITERIMA_LAIN_2;
                TrForm5repair.TGL_DITERIMA_LAIN_2 = TGL_DITERIMA_LAIN_2;
                TrForm5repair.STATUS = EApprovalStatus.Cancel;
                //TrForm3gdr.REVISI = "";
                TrForm5repair.USER_CURRENT = HfUsername.Value;
                TrForm5repair.NEXT_USER = "-";
                TrForm5repair.URUTAN_USER_CURRENT = 1;
                TrForm5repair.URUTAN_NEXT_USER = 0;
                TrForm5RepairDA.UpdateMenyetujui1(TrForm5repair);

                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM;
                TrForm5gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm5gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 7;
                TrForm5gdractivity.SP = "";
                TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm5gdractivity.NEXT_USER = "-";
                TrForm5gdractivity.URUTAN_USER_CURRENT = 1;
                TrForm5gdractivity.URUTAN_NEXT_USER = 0;
                TrForm5GdrActivity.Insert(TrForm5gdractivity);

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

        public void UpdateStatusCancelStoreDesign()
        {
            try
            {
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;

                TR_FORM5_REPAIR TrForm5repair = new TR_FORM5_REPAIR();
                TrForm5repair.NO_FORM = NO_FORM;
                TrForm5repair.STATUS = EApprovalStatus.Cancel;
                TrForm5repair.USER_CURRENT = HfUsername.Value;
                TrForm5repair.NEXT_USER = "-";
                TrForm5repair.URUTAN_USER_CURRENT = 99;
                TrForm5repair.URUTAN_NEXT_USER = 0;
                TrForm5RepairDA.UpdateDibuatSD(TrForm5repair);

                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM;
                TrForm5gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm5gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 99;
                TrForm5gdractivity.SP = "";
                TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                TrForm5gdractivity.NEXT_USER = "-";
                TrForm5gdractivity.URUTAN_USER_CURRENT = 99;
                TrForm5gdractivity.URUTAN_NEXT_USER = 0;
                TrForm5GdrActivity.Insert(TrForm5gdractivity);

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
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string MENYETUJUI1 = text_menyetujui1.Text;
                string REVISI = text_revisi.Text;
                DateTime TGL_MENYETUJUI1 = DateTime.Now;

                if (REVISI != "")
                {
                    TR_FORM5_REPAIR TrForm5Repair = new TR_FORM5_REPAIR();
                    TrForm5Repair.NO_FORM = NO_FORM;
                    TrForm5Repair.MENYETUJUI1 = MENYETUJUI1;
                    TrForm5Repair.TGL_MENYETUJUI1 = TGL_MENYETUJUI1;
                    TrForm5Repair.STATUS = EApprovalStatus.OnRevise;
                    TrForm5Repair.REVISI = REVISI;

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

                    TrForm5Repair.USER_CURRENT = HfUsername.Value;
                    TrForm5Repair.NEXT_USER = USERNEXT;
                    TrForm5Repair.URUTAN_USER_CURRENT = URUTAN;
                    TrForm5Repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm5RepairDA.UpdateRevisiMenyetujui1(TrForm5Repair);

                    TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                    TrForm5gdractivity.USERNAME = HfUsername.Value;
                    TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                    TrForm5gdractivity.KODE_FORM = "FRM-0005";
                    TrForm5gdractivity.NO_FORM = NO_FORM;
                    TrForm5gdractivity.STATUS = EApprovalStatus.OnRevise;
                    TrForm5gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.OnRevise;
                    TrForm5gdractivity.REVISION = REVISI;
                    TrForm5gdractivity.URUTAN = 2;
                    TrForm5gdractivity.SP = "";
                    TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                    TrForm5gdractivity.NEXT_USER = USERNEXT;
                    TrForm5gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    TrForm5gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm5GdrActivity.Insert(TrForm5gdractivity);

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
                    ModalRevise.Show();
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

        public void UpdateStatusRevisiProject()
        {
            try
            {
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_1 = text_diterima1.Text;
                string REVISI = text_revisi.Text;
                DateTime TGL_DITERIMA_1 = DateTime.Now;

                if (REVISI != "")
                {
                    TR_FORM5_REPAIR TrForm5Repair = new TR_FORM5_REPAIR();
                    TrForm5Repair.NO_FORM = NO_FORM;
                    TrForm5Repair.DITERIMA_1 = DITERIMA_1;
                    TrForm5Repair.TGL_DITERIMA_1 = TGL_DITERIMA_1;
                    TrForm5Repair.STATUS = EApprovalStatus.OnRevise;
                    TrForm5Repair.REVISI = REVISI;

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

                    TrForm5Repair.USER_CURRENT = HfUsername.Value;
                    TrForm5Repair.NEXT_USER = USERNEXT;
                    TrForm5Repair.URUTAN_USER_CURRENT = URUTAN;
                    TrForm5Repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm5RepairDA.UpdateDiterima1(TrForm5Repair);

                    TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                    TrForm5gdractivity.USERNAME = HfUsername.Value;
                    TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                    TrForm5gdractivity.KODE_FORM = "FRM-0005";
                    TrForm5gdractivity.NO_FORM = NO_FORM;
                    TrForm5gdractivity.STATUS = EApprovalStatus.OnRevise;
                    TrForm5gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.OnRevise;
                    TrForm5gdractivity.REVISION = REVISI;
                    TrForm5gdractivity.URUTAN = 3;
                    TrForm5gdractivity.SP = "";
                    TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                    TrForm5gdractivity.NEXT_USER = USERNEXT;
                    TrForm5gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    TrForm5gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm5GdrActivity.Insert(TrForm5gdractivity);

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
                    ModalRevise.Show();
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

        public void UpdateStatusRevisiBudgetControl()
        {
            try
            {
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string BRAND = text_brand.Text;
                string DITERIMA_LAIN_2 = text_diterimalain2.Text;
                string REVISI = text_revisi.Text;
                DateTime TGL_DITERIMA_LAIN_2 = DateTime.Now;

                string OVER_BUDGET = Convert.ToString(text_overbudgetvalue.Text);
                if (OVER_BUDGET != "")
                {
                    OVER_BUDGET = Convert.ToDecimal(text_overbudgetvalue.Text).ToString();
                }
                else
                {
                    OVER_BUDGET = "0.00";
                }

                string OVERBUDGETCHECK = EYesNo.Yes;

                if (REVISI != "")
                {

                    if (OVER_BUDGET != "0.00" || OVER_BUDGET != "" || OVER_BUDGET != "0")
                    {
                        if (OVERBUDGETCHECK == EYesNo.Yes)
                        {
                            TR_FORM5_REPAIR TrForm5Repair = new TR_FORM5_REPAIR();
                            TrForm5Repair.NO_FORM = NO_FORM;
                            TrForm5Repair.DITERIMA_LAIN_2 = DITERIMA_LAIN_2;
                            TrForm5Repair.TGL_DITERIMA_LAIN_2 = TGL_DITERIMA_LAIN_2;
                            TrForm5Repair.STATUS = EApprovalStatus.OnApprovedBM;
                            TrForm5Repair.REVISI = REVISI;
                            TrForm5Repair.OVER_BUDGET = Convert.ToDecimal(OVER_BUDGET);
                            TrForm5Repair.OVER_BUDGET_REQUEST = OVERBUDGETCHECK;

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


                            BRAND_DA MsBrandDA = new BRAND_DA();
                            DataSet DsBrand = new DataSet();
                            string WhereBrand = string.Format("BRAND = '{0}'", BRAND);

                            string KodeBrand = "";
                            DsBrand = MsBrandDA.GetDataFilter(WhereBrand);
                            if (DsBrand.Tables[0].Rows.Count > 0)
                            {
                                KodeBrand = Convert.ToString(DsBrand.Tables[0].Rows[0]["KD_BRAND"].ToString());
                            }

                            MS_USER_DA MsUserDA = new MS_USER_DA();
                            DataSet DsUser = new DataSet();

                            string Where = string.Format("KD_BRAND LIKE '%{0}%' And KD_JABATAN = 'BM'", KodeBrand);

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

                            TrForm5Repair.USER_CURRENT = HfUsername.Value;
                            TrForm5Repair.NEXT_USER = USERNEXT;
                            TrForm5Repair.URUTAN_USER_CURRENT = URUTAN;
                            TrForm5Repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                            TrForm5RepairDA.UpdateRevisiDiterima2(TrForm5Repair);

                            TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                            DataSet DsFormActivity = new DataSet();

                            TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                            TrForm5gdractivity.USERNAME = HfUsername.Value;
                            TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                            TrForm5gdractivity.KODE_FORM = "FRM-0005";
                            TrForm5gdractivity.NO_FORM = NO_FORM;
                            TrForm5gdractivity.STATUS = EApprovalStatus.OnApprovedBM;
                            TrForm5gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.OnApprovedBM;
                            TrForm5gdractivity.REVISION = REVISI;
                            TrForm5gdractivity.URUTAN = 6;
                            TrForm5gdractivity.SP = "";
                            TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                            TrForm5gdractivity.NEXT_USER = USERNEXT;
                            TrForm5gdractivity.URUTAN_USER_CURRENT = URUTAN;
                            TrForm5gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                            TrForm5GdrActivity.Insert(TrForm5gdractivity);

                            DivMessage.InnerText = "Data Successful Revise Budget Control";
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
                            DivMessage.InnerText = "Over Budget Must Input Yes";
                            DivMessage.Attributes["class"] = "error";
                            //DivMessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;
                        }
                    }
                    else
                    {
                        DivMessage.InnerText = "Over Budget Cannot Be Empty";
                        DivMessage.Attributes["class"] = "error";
                        //DivMessage.Attributes["class"] = "success";
                        DivMessage.Visible = true;
                    }


                }
                else
                {
                    ModalRevise.Show();
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

        public void UpdateStatusRevisiStoreDesign()
        {
            try
            {
                TR_FORM5_REPAIR_DA TrForm5RepairDA = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_LAIN_SD = HfUsername.Value;
                string REVISI = text_revisi.Text;
                DateTime TGL_DITERIMA_LAIN_SD = DateTime.Now;

                if (REVISI != "")
                {
                    TR_FORM5_REPAIR TrForm5Repair = new TR_FORM5_REPAIR();
                    TrForm5Repair.NO_FORM = NO_FORM;
                    TrForm5Repair.DITERIMA_LAIN_SD = DITERIMA_LAIN_SD;
                    TrForm5Repair.TGL_DITERIMA_LAIN_SD = TGL_DITERIMA_LAIN_SD;
                    TrForm5Repair.STATUS = EApprovalStatus.OnRevise;
                    TrForm5Repair.REVISI = REVISI;

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

                    TrForm5Repair.USER_CURRENT = HfUsername.Value;
                    TrForm5Repair.NEXT_USER = USERNEXT;
                    TrForm5Repair.URUTAN_USER_CURRENT = URUTAN;
                    TrForm5Repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm5RepairDA.UpdateRevisiDiterimaLainSD(TrForm5Repair);

                    TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                    DataSet DsFormActivity = new DataSet();

                    TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                    TrForm5gdractivity.USERNAME = HfUsername.Value;
                    TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                    TrForm5gdractivity.KODE_FORM = "FRM-0005";
                    TrForm5gdractivity.NO_FORM = NO_FORM;
                    TrForm5gdractivity.STATUS = EApprovalStatus.OnRevise;
                    TrForm5gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.OnRevise;
                    TrForm5gdractivity.REVISION = REVISI;
                    TrForm5gdractivity.URUTAN = 99;
                    TrForm5gdractivity.SP = "";
                    TrForm5gdractivity.USER_CURRENT = HfUsername.Value;
                    TrForm5gdractivity.NEXT_USER = USERNEXT;
                    TrForm5gdractivity.URUTAN_USER_CURRENT = URUTAN;
                    TrForm5gdractivity.URUTAN_NEXT_USER = URUTAN_NEXT;
                    TrForm5GdrActivity.Insert(TrForm5gdractivity);

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
                    ModalRevise.Show();
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

        public void PICChooseAll()
        {

            ddlpicperbaikan1.Text = "Choose";
            ddlpicperbaikan2.Text = "Choose";
            ddlpicperbaikan3.Text = "Choose";
            ddlpicperbaikan4.Text = "Choose";
            ddlpicperbaikan5.Text = "Choose";
            ddlpicperbaikan6.Text = "Choose";
            ddlpicperbaikan7.Text = "Choose";
            ddlpicperbaikan8.Text = "Choose";
            ddlpicperbaikan9.Text = "Choose";
            ddlpicperbaikan10.Text = "Choose";
        }

        public void ShowButtonUrutan1()
        {
            ddloverbudget.Enabled = false;
            text_overbudgetvalue.Enabled = false;

            Pnl_PermintaanPerbaikan.Enabled = true;

            text_permintaanperbaikan1.Enabled = true;
            text_descriptionrepair2_1.Enabled = false;
            ddlpicperbaikan1.Enabled = false;
            text_budgets1.Enabled = false;
            text_actualfinishdate1.Enabled = false;
            text_completedate1.Enabled = false;

            text_permintaanperbaikan2.Enabled = true;
            text_descriptionrepair2_2.Enabled = false;
            ddlpicperbaikan2.Enabled = false;
            text_budgets2.Enabled = false;
            text_actualfinishdate2.Enabled = false;
            text_completedate2.Enabled = false;

            text_permintaanperbaikan3.Enabled = true;
            text_descriptionrepair2_3.Enabled = false;
            ddlpicperbaikan3.Enabled = false;
            text_budgets3.Enabled = false;
            text_actualfinishdate3.Enabled = false;
            text_completedate3.Enabled = false;

            text_permintaanperbaikan4.Enabled = true;
            text_descriptionrepair2_4.Enabled = false;
            ddlpicperbaikan4.Enabled = false;
            text_budgets4.Enabled = false;
            text_actualfinishdate4.Enabled = false;
            text_completedate4.Enabled = false;

            text_permintaanperbaikan5.Enabled = true;
            text_descriptionrepair2_5.Enabled = false;
            ddlpicperbaikan5.Enabled = false;
            text_budgets5.Enabled = false;
            text_actualfinishdate5.Enabled = false;
            text_completedate5.Enabled = false;

            text_permintaanperbaikan6.Enabled = true;
            text_descriptionrepair2_6.Enabled = false;
            ddlpicperbaikan6.Enabled = false;
            text_budgets6.Enabled = false;
            text_actualfinishdate6.Enabled = false;
            text_completedate6.Enabled = false;

            text_permintaanperbaikan7.Enabled = true;
            text_descriptionrepair2_7.Enabled = false;
            ddlpicperbaikan7.Enabled = false;
            text_budgets7.Enabled = false;
            text_actualfinishdate7.Enabled = false;
            text_completedate7.Enabled = false;

            text_permintaanperbaikan8.Enabled = true;
            text_descriptionrepair2_8.Enabled = false;
            ddlpicperbaikan8.Enabled = false;
            text_budgets8.Enabled = false;
            text_actualfinishdate8.Enabled = false;
            text_completedate8.Enabled = false;

            text_permintaanperbaikan9.Enabled = true;
            text_descriptionrepair2_9.Enabled = false;
            ddlpicperbaikan9.Enabled = false;
            text_budgets9.Enabled = false;
            text_actualfinishdate9.Enabled = false;
            text_completedate9.Enabled = false;

            text_permintaanperbaikan10.Enabled = true;
            text_descriptionrepair2_10.Enabled = false;
            ddlpicperbaikan10.Enabled = false;
            text_budgets10.Enabled = false;
            text_actualfinishdate10.Enabled = false;

            text_completedate1.Enabled = false;
            linkbtn_filename1.Enabled = true;
            linkbtn_filename2.Enabled = true;
            linkbtn_filename3.Enabled = true;
            linkbtn_filename4.Enabled = true;
            linkbtn_filename5.Enabled = true;
            linkbtn_filename6.Enabled = true;
            linkbtn_filename7.Enabled = true;
            linkbtn_filename8.Enabled = true;
            linkbtn_filename9.Enabled = true;
            linkbtn_filename10.Enabled = true;
        }

        public void ShowButtonUrutan2()
        {
            ddloverbudget.Enabled = false;
            text_overbudgetvalue.Enabled = false;

            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            Pnl_OverBudget.Enabled = false;
            Pnl_Others1.Enabled = false;

            btn_ToRevise.Visible = true;
            //btn_Revise.Visible = true;
            btn_Approved.Visible = true;
            btn_Reject.Visible = true;

            Pnl_PermintaanPerbaikan.Enabled = true;
            text_permintaanperbaikan1.Enabled = false;
            text_descriptionrepair2_1.Enabled = false;
            ddlpicperbaikan1.Enabled = false;
            text_budgets1.Enabled = false;
            text_actualfinishdate1.Enabled = false;
            text_completedate1.Enabled = false;

            text_permintaanperbaikan2.Enabled = false;
            text_descriptionrepair2_2.Enabled = false;
            ddlpicperbaikan2.Enabled = false;
            text_budgets2.Enabled = false;
            text_actualfinishdate2.Enabled = false;
            text_completedate2.Enabled = false;

            text_permintaanperbaikan3.Enabled = false;
            text_descriptionrepair2_3.Enabled = false;
            ddlpicperbaikan3.Enabled = false;
            text_budgets3.Enabled = false;
            text_actualfinishdate3.Enabled = false;
            text_completedate3.Enabled = false;

            text_permintaanperbaikan4.Enabled = false;
            text_descriptionrepair2_4.Enabled = false;
            ddlpicperbaikan4.Enabled = false;
            text_budgets4.Enabled = false;
            text_actualfinishdate4.Enabled = false;
            text_completedate4.Enabled = false;

            text_permintaanperbaikan5.Enabled = false;
            text_descriptionrepair2_5.Enabled = false;
            ddlpicperbaikan5.Enabled = false;
            text_budgets5.Enabled = false;
            text_actualfinishdate5.Enabled = false;
            text_completedate5.Enabled = false;

            text_permintaanperbaikan6.Enabled = false;
            text_descriptionrepair2_6.Enabled = false;
            ddlpicperbaikan6.Enabled = false;
            text_budgets6.Enabled = false;
            text_actualfinishdate6.Enabled = false;
            text_completedate6.Enabled = false;

            text_permintaanperbaikan7.Enabled = false;
            text_descriptionrepair2_7.Enabled = false;
            ddlpicperbaikan7.Enabled = false;
            text_budgets7.Enabled = false;
            text_actualfinishdate7.Enabled = false;
            text_completedate7.Enabled = false;

            text_permintaanperbaikan8.Enabled = false;
            text_descriptionrepair2_8.Enabled = false;
            ddlpicperbaikan8.Enabled = false;
            text_budgets8.Enabled = false;
            text_actualfinishdate8.Enabled = false;
            text_completedate8.Enabled = false;

            text_permintaanperbaikan9.Enabled = false;
            text_descriptionrepair2_9.Enabled = false;
            ddlpicperbaikan9.Enabled = false;
            text_budgets9.Enabled = false;
            text_actualfinishdate9.Enabled = false;
            text_completedate9.Enabled = false;

            text_permintaanperbaikan10.Enabled = false;
            text_descriptionrepair2_10.Enabled = false;
            ddlpicperbaikan10.Enabled = false;
            text_budgets10.Enabled = false;
            text_actualfinishdate10.Enabled = false;

            text_completedate1.Enabled = false;
            linkbtn_filename1.Enabled = true;
            linkbtn_filename2.Enabled = true;
            linkbtn_filename3.Enabled = true;
            linkbtn_filename4.Enabled = true;
            linkbtn_filename5.Enabled = true;
            linkbtn_filename6.Enabled = true;
            linkbtn_filename7.Enabled = true;
            linkbtn_filename8.Enabled = true;
            linkbtn_filename9.Enabled = true;
            linkbtn_filename10.Enabled = true;
        }

        public void ShowButtonUrutan3()
        {
            ddloverbudget.Enabled = false;
            text_overbudgetvalue.Enabled = false;

            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            Pnl_OverBudget.Enabled = false;

            btn_Approved.Visible = true;
            btn_ToRevise.Visible = true;
            btn_Reject.Visible = true;
            //Pnl_Others1.Enabled = true;
            Pnl_PermintaanPerbaikan.Enabled = true;

            text_descriptionrepair2_1.Text = text_permintaanperbaikan1.Text;
            text_descriptionrepair2_2.Text = text_permintaanperbaikan2.Text;
            text_descriptionrepair2_3.Text = text_permintaanperbaikan3.Text;
            text_descriptionrepair2_4.Text = text_permintaanperbaikan4.Text;
            text_descriptionrepair2_5.Text = text_permintaanperbaikan5.Text;
            text_descriptionrepair2_6.Text = text_permintaanperbaikan6.Text;
            text_descriptionrepair2_7.Text = text_permintaanperbaikan7.Text;
            text_descriptionrepair2_8.Text = text_permintaanperbaikan8.Text;
            text_descriptionrepair2_9.Text = text_permintaanperbaikan9.Text;
            text_descriptionrepair2_10.Text = text_permintaanperbaikan10.Text;


            text_permintaanperbaikan1.Enabled = false;
            text_descriptionrepair2_1.Enabled = true;
            ddlpicperbaikan1.Enabled = true;
            text_budgets1.Enabled = true;
            text_actualfinishdate1.Enabled = false;
            text_completedate1.Enabled = true;

            text_permintaanperbaikan2.Enabled = false;
            text_descriptionrepair2_2.Enabled = true;
            ddlpicperbaikan2.Enabled = true;
            text_budgets2.Enabled = true;
            text_actualfinishdate2.Enabled = false;
            text_completedate2.Enabled = true;

            text_permintaanperbaikan3.Enabled = false;
            text_descriptionrepair2_3.Enabled = true;
            ddlpicperbaikan3.Enabled = true;
            text_budgets3.Enabled = true;
            text_actualfinishdate3.Enabled = false;
            text_completedate3.Enabled = true;

            text_permintaanperbaikan4.Enabled = false;
            text_descriptionrepair2_4.Enabled = true;
            ddlpicperbaikan4.Enabled = true;
            text_budgets4.Enabled = true;
            text_actualfinishdate4.Enabled = false;
            text_completedate4.Enabled = true;

            text_permintaanperbaikan5.Enabled = false;
            text_descriptionrepair2_5.Enabled = true;
            ddlpicperbaikan5.Enabled = true;
            text_budgets5.Enabled = true;
            text_actualfinishdate5.Enabled = false;
            text_completedate5.Enabled = true;

            text_permintaanperbaikan6.Enabled = false;
            text_descriptionrepair2_6.Enabled = true;
            ddlpicperbaikan6.Enabled = true;
            text_budgets6.Enabled = true;
            text_actualfinishdate6.Enabled = false;
            text_completedate6.Enabled = true;

            text_permintaanperbaikan7.Enabled = false;
            text_descriptionrepair2_7.Enabled = true;
            ddlpicperbaikan7.Enabled = true;
            text_budgets7.Enabled = true;
            text_actualfinishdate7.Enabled = false;
            text_completedate7.Enabled = true;

            text_permintaanperbaikan8.Enabled = false;
            text_descriptionrepair2_8.Enabled = true;
            ddlpicperbaikan8.Enabled = true;
            text_budgets8.Enabled = true;
            text_actualfinishdate8.Enabled = false;
            text_completedate8.Enabled = true;

            text_permintaanperbaikan9.Enabled = false;
            text_descriptionrepair2_9.Enabled = true;
            ddlpicperbaikan9.Enabled = true;
            text_budgets9.Enabled = true;
            text_actualfinishdate9.Enabled = false;
            text_completedate9.Enabled = true;

            text_permintaanperbaikan10.Enabled = false;
            text_descriptionrepair2_10.Enabled = true;
            ddlpicperbaikan10.Enabled = true;
            text_budgets10.Enabled = true;
            text_actualfinishdate10.Enabled = false;
            text_completedate1.Enabled = true;

            linkbtn_filename1.Enabled = true;
            linkbtn_filename2.Enabled = true;
            linkbtn_filename3.Enabled = true;
            linkbtn_filename4.Enabled = true;
            linkbtn_filename5.Enabled = true;
            linkbtn_filename6.Enabled = true;
            linkbtn_filename7.Enabled = true;
            linkbtn_filename8.Enabled = true;
            linkbtn_filename9.Enabled = true;
            linkbtn_filename10.Enabled = true;

        }

        public void ShowButtonUrutan4()
        {
            ddloverbudget.Enabled = false;
            text_overbudgetvalue.Enabled = false;

            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            Pnl_OverBudget.Enabled = false;

            btn_Approved.Visible = true;
            btn_Reject.Visible = true;

            Pnl_PermintaanPerbaikan.Enabled = true;

            text_permintaanperbaikan1.Enabled = false;
            text_descriptionrepair2_1.Enabled = false;
            ddlpicperbaikan1.Enabled = false;
            text_budgets1.Enabled = false;
            text_actualfinishdate1.Enabled = false;
            text_completedate1.Enabled = false;

            text_permintaanperbaikan2.Enabled = false;
            text_descriptionrepair2_2.Enabled = false;
            ddlpicperbaikan2.Enabled = false;
            text_budgets2.Enabled = false;
            text_actualfinishdate2.Enabled = false;
            text_completedate2.Enabled = false;

            text_permintaanperbaikan3.Enabled = false;
            text_descriptionrepair2_3.Enabled = false;
            ddlpicperbaikan3.Enabled = false;
            text_budgets3.Enabled = false;
            text_actualfinishdate3.Enabled = false;
            text_completedate3.Enabled = false;

            text_permintaanperbaikan4.Enabled = false;
            text_descriptionrepair2_4.Enabled = false;
            ddlpicperbaikan4.Enabled = false;
            text_budgets4.Enabled = false;
            text_actualfinishdate4.Enabled = false;
            text_completedate4.Enabled = false;

            text_permintaanperbaikan5.Enabled = false;
            text_descriptionrepair2_5.Enabled = false;
            ddlpicperbaikan5.Enabled = false;
            text_budgets5.Enabled = false;
            text_actualfinishdate5.Enabled = false;
            text_completedate5.Enabled = false;

            text_permintaanperbaikan6.Enabled = false;
            text_descriptionrepair2_6.Enabled = false;
            ddlpicperbaikan6.Enabled = false;
            text_budgets6.Enabled = false;
            text_actualfinishdate6.Enabled = false;
            text_completedate6.Enabled = false;

            text_permintaanperbaikan7.Enabled = false;
            text_descriptionrepair2_7.Enabled = false;
            ddlpicperbaikan7.Enabled = false;
            text_budgets7.Enabled = false;
            text_actualfinishdate7.Enabled = false;
            text_completedate7.Enabled = false;

            text_permintaanperbaikan8.Enabled = false;
            text_descriptionrepair2_8.Enabled = false;
            ddlpicperbaikan8.Enabled = false;
            text_budgets8.Enabled = false;
            text_actualfinishdate8.Enabled = false;
            text_completedate8.Enabled = false;

            text_permintaanperbaikan9.Enabled = false;
            text_descriptionrepair2_9.Enabled = false;
            ddlpicperbaikan9.Enabled = false;
            text_budgets9.Enabled = false;
            text_actualfinishdate9.Enabled = false;
            text_completedate9.Enabled = false;

            text_permintaanperbaikan10.Enabled = false;
            text_descriptionrepair2_10.Enabled = false;
            ddlpicperbaikan10.Enabled = false;
            text_budgets10.Enabled = false;
            text_actualfinishdate10.Enabled = false;

            text_completedate1.Enabled = false;


            linkbtn_filename1.Enabled = true;
            linkbtn_filename2.Enabled = true;
            linkbtn_filename3.Enabled = true;
            linkbtn_filename4.Enabled = true;
            linkbtn_filename5.Enabled = true;
            linkbtn_filename6.Enabled = true;
            linkbtn_filename7.Enabled = true;
            linkbtn_filename8.Enabled = true;
            linkbtn_filename9.Enabled = true;
            linkbtn_filename10.Enabled = true;

        }

        public void ShowButtonUrutan5()
        {
            ddloverbudget.Enabled = false;
            text_overbudgetvalue.Enabled = false;

            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            Pnl_OverBudget.Enabled = false;

            btn_Approved.Visible = true;
            btn_Reject.Visible = true;

            Pnl_PermintaanPerbaikan.Enabled = true;
            text_permintaanperbaikan1.Enabled = false;
            text_descriptionrepair2_1.Enabled = false;
            ddlpicperbaikan1.Enabled = false;
            text_budgets1.Enabled = false;
            text_actualfinishdate1.Enabled = false;
            text_completedate1.Enabled = false;

            text_permintaanperbaikan2.Enabled = false;
            text_descriptionrepair2_2.Enabled = false;
            ddlpicperbaikan2.Enabled = false;
            text_budgets2.Enabled = false;
            text_actualfinishdate2.Enabled = false;
            text_completedate2.Enabled = false;

            text_permintaanperbaikan3.Enabled = false;
            text_descriptionrepair2_3.Enabled = false;
            ddlpicperbaikan3.Enabled = false;
            text_budgets3.Enabled = false;
            text_actualfinishdate3.Enabled = false;
            text_completedate3.Enabled = false;

            text_permintaanperbaikan4.Enabled = false;
            text_descriptionrepair2_4.Enabled = false;
            ddlpicperbaikan4.Enabled = false;
            text_budgets4.Enabled = false;
            text_actualfinishdate4.Enabled = false;
            text_completedate4.Enabled = false;

            text_permintaanperbaikan5.Enabled = false;
            text_descriptionrepair2_5.Enabled = false;
            ddlpicperbaikan5.Enabled = false;
            text_budgets5.Enabled = false;
            text_actualfinishdate5.Enabled = false;
            text_completedate5.Enabled = false;

            text_permintaanperbaikan6.Enabled = false;
            text_descriptionrepair2_6.Enabled = false;
            ddlpicperbaikan6.Enabled = false;
            text_budgets6.Enabled = false;
            text_actualfinishdate6.Enabled = false;
            text_completedate6.Enabled = false;

            text_permintaanperbaikan7.Enabled = false;
            text_descriptionrepair2_7.Enabled = false;
            ddlpicperbaikan7.Enabled = false;
            text_budgets7.Enabled = false;
            text_actualfinishdate7.Enabled = false;
            text_completedate7.Enabled = false;

            text_permintaanperbaikan8.Enabled = false;
            text_descriptionrepair2_8.Enabled = false;
            ddlpicperbaikan8.Enabled = false;
            text_budgets8.Enabled = false;
            text_actualfinishdate8.Enabled = false;
            text_completedate8.Enabled = false;

            text_permintaanperbaikan9.Enabled = false;
            text_descriptionrepair2_9.Enabled = false;
            ddlpicperbaikan9.Enabled = false;
            text_budgets9.Enabled = false;
            text_actualfinishdate9.Enabled = false;
            text_completedate9.Enabled = false;

            text_permintaanperbaikan10.Enabled = false;
            text_descriptionrepair2_10.Enabled = false;
            ddlpicperbaikan10.Enabled = false;
            text_budgets10.Enabled = false;
            text_actualfinishdate10.Enabled = false;

            text_completedate1.Enabled = false;


            linkbtn_filename1.Enabled = true;
            linkbtn_filename2.Enabled = true;
            linkbtn_filename3.Enabled = true;
            linkbtn_filename4.Enabled = true;
            linkbtn_filename5.Enabled = true;
            linkbtn_filename6.Enabled = true;
            linkbtn_filename7.Enabled = true;
            linkbtn_filename8.Enabled = true;
            linkbtn_filename9.Enabled = true;
            linkbtn_filename10.Enabled = true;
        }

        public void ShowButtonUrutan6()
        {
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            Pnl_Others1.Enabled = true;
            Pnl_OverBudget.Enabled = false;
            text_overbudgetvalue.Enabled = true;
            ddloverbudget.Enabled = true;


            btn_ToRevise.Visible = true;
            btn_ToRevise.Text = "To Submit Over Budget";
            btn_Revise.Text = "Submit Over Budget";
            label_revisi.Text = "Commentar";
            //btn_Revise.Visible = true;
            btn_Approved.Visible = true;

            Pnl_PermintaanPerbaikan.Enabled = true;

            text_permintaanperbaikan1.Enabled = false;
            text_descriptionrepair2_1.Enabled = false;
            ddlpicperbaikan1.Enabled = false;
            text_budgets1.Enabled = false;
            text_actualfinishdate1.Enabled = false;
            text_completedate1.Enabled = false;

            text_permintaanperbaikan2.Enabled = false;
            text_descriptionrepair2_2.Enabled = false;
            ddlpicperbaikan2.Enabled = false;
            text_budgets2.Enabled = false;
            text_actualfinishdate2.Enabled = false;
            text_completedate2.Enabled = false;

            text_permintaanperbaikan3.Enabled = false;
            text_descriptionrepair2_3.Enabled = false;
            ddlpicperbaikan3.Enabled = false;
            text_budgets3.Enabled = false;
            text_actualfinishdate3.Enabled = false;
            text_completedate3.Enabled = false;

            text_permintaanperbaikan4.Enabled = false;
            text_descriptionrepair2_4.Enabled = false;
            ddlpicperbaikan4.Enabled = false;
            text_budgets4.Enabled = false;
            text_actualfinishdate4.Enabled = false;
            text_completedate4.Enabled = false;

            text_permintaanperbaikan5.Enabled = false;
            text_descriptionrepair2_5.Enabled = false;
            ddlpicperbaikan5.Enabled = false;
            text_budgets5.Enabled = false;
            text_actualfinishdate5.Enabled = false;
            text_completedate5.Enabled = false;

            text_permintaanperbaikan6.Enabled = false;
            text_descriptionrepair2_6.Enabled = false;
            ddlpicperbaikan6.Enabled = false;
            text_budgets6.Enabled = false;
            text_actualfinishdate6.Enabled = false;
            text_completedate6.Enabled = false;

            text_permintaanperbaikan7.Enabled = false;
            text_descriptionrepair2_7.Enabled = false;
            ddlpicperbaikan7.Enabled = false;
            text_budgets7.Enabled = false;
            text_actualfinishdate7.Enabled = false;
            text_completedate7.Enabled = false;

            text_permintaanperbaikan8.Enabled = false;
            text_descriptionrepair2_8.Enabled = false;
            ddlpicperbaikan8.Enabled = false;
            text_budgets8.Enabled = false;
            text_actualfinishdate8.Enabled = false;
            text_completedate8.Enabled = false;

            text_permintaanperbaikan9.Enabled = false;
            text_descriptionrepair2_9.Enabled = false;
            ddlpicperbaikan9.Enabled = false;
            text_budgets9.Enabled = false;
            text_actualfinishdate9.Enabled = false;
            text_completedate9.Enabled = false;

            text_permintaanperbaikan10.Enabled = false;
            text_descriptionrepair2_10.Enabled = false;
            ddlpicperbaikan10.Enabled = false;
            text_budgets10.Enabled = false;
            text_actualfinishdate10.Enabled = false;

            text_completedate1.Enabled = false;


            linkbtn_filename1.Enabled = true;
            linkbtn_filename2.Enabled = true;
            linkbtn_filename3.Enabled = true;
            linkbtn_filename4.Enabled = true;
            linkbtn_filename5.Enabled = true;
            linkbtn_filename6.Enabled = true;
            linkbtn_filename7.Enabled = true;
            linkbtn_filename8.Enabled = true;
            linkbtn_filename9.Enabled = true;
            linkbtn_filename10.Enabled = true;
        }

        public void ShowButtonUrutan7()
        {
            ddloverbudget.Enabled = false;
            text_overbudgetvalue.Enabled = false;

            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            Pnl_Others1.Enabled = false;
            Pnl_OverBudget.Enabled = false;

            btn_Approved.Visible = true;
            btn_Reject.Visible = true;

            Pnl_PermintaanPerbaikan.Enabled = true;

            text_permintaanperbaikan1.Enabled = false;
            text_descriptionrepair2_1.Enabled = false;
            ddlpicperbaikan1.Enabled = false;
            text_budgets1.Enabled = false;
            text_actualfinishdate1.Enabled = false;
            text_completedate1.Enabled = false;

            text_permintaanperbaikan2.Enabled = false;
            text_descriptionrepair2_2.Enabled = false;
            ddlpicperbaikan2.Enabled = false;
            text_budgets2.Enabled = false;
            text_actualfinishdate2.Enabled = false;
            text_completedate2.Enabled = false;

            text_permintaanperbaikan3.Enabled = false;
            text_descriptionrepair2_3.Enabled = false;
            ddlpicperbaikan3.Enabled = false;
            text_budgets3.Enabled = false;
            text_actualfinishdate3.Enabled = false;
            text_completedate3.Enabled = false;

            text_permintaanperbaikan4.Enabled = false;
            text_descriptionrepair2_4.Enabled = false;
            ddlpicperbaikan4.Enabled = false;
            text_budgets4.Enabled = false;
            text_actualfinishdate4.Enabled = false;
            text_completedate4.Enabled = false;

            text_permintaanperbaikan5.Enabled = false;
            text_descriptionrepair2_5.Enabled = false;
            ddlpicperbaikan5.Enabled = false;
            text_budgets5.Enabled = false;
            text_actualfinishdate5.Enabled = false;
            text_completedate5.Enabled = false;

            text_permintaanperbaikan6.Enabled = false;
            text_descriptionrepair2_6.Enabled = false;
            ddlpicperbaikan6.Enabled = false;
            text_budgets6.Enabled = false;
            text_actualfinishdate6.Enabled = false;
            text_completedate6.Enabled = false;

            text_permintaanperbaikan7.Enabled = false;
            text_descriptionrepair2_7.Enabled = false;
            ddlpicperbaikan7.Enabled = false;
            text_budgets7.Enabled = false;
            text_actualfinishdate7.Enabled = false;
            text_completedate7.Enabled = false;

            text_permintaanperbaikan8.Enabled = false;
            text_descriptionrepair2_8.Enabled = false;
            ddlpicperbaikan8.Enabled = false;
            text_budgets8.Enabled = false;
            text_actualfinishdate8.Enabled = false;
            text_completedate8.Enabled = false;

            text_permintaanperbaikan9.Enabled = false;
            text_descriptionrepair2_9.Enabled = false;
            ddlpicperbaikan9.Enabled = false;
            text_budgets9.Enabled = false;
            text_actualfinishdate9.Enabled = false;
            text_completedate9.Enabled = false;

            text_permintaanperbaikan10.Enabled = false;
            text_descriptionrepair2_10.Enabled = false;
            ddlpicperbaikan10.Enabled = false;
            text_budgets10.Enabled = false;
            text_actualfinishdate10.Enabled = false;

            text_completedate1.Enabled = false;


            linkbtn_filename1.Enabled = true;
            linkbtn_filename2.Enabled = true;
            linkbtn_filename3.Enabled = true;
            linkbtn_filename4.Enabled = true;
            linkbtn_filename5.Enabled = true;
            linkbtn_filename6.Enabled = true;
            linkbtn_filename7.Enabled = true;
            linkbtn_filename8.Enabled = true;
            linkbtn_filename9.Enabled = true;
            linkbtn_filename10.Enabled = true;
        }

        public void ShowButtonUrutan8()
        {
            ddloverbudget.Enabled = false;
            text_overbudgetvalue.Enabled = false;

            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            btn_Reject.Visible = false;
            Pnl_Others1.Enabled = false;
            Pnl_OverBudget.Enabled = false;

            btn_Approved.Visible = true;
            btn_Approved.Text = "Update Status Working";
            //btn_Approved.Enabled = true;

            Pnl_PermintaanPerbaikan.Enabled = true;

            text_permintaanperbaikan1.Enabled = false;
            text_descriptionrepair2_1.Enabled = false;
            ddlpicperbaikan1.Enabled = false;
            text_budgets1.Enabled = false;
            text_actualfinishdate1.Enabled = false;
            text_completedate1.Enabled = false;

            text_permintaanperbaikan2.Enabled = false;
            text_descriptionrepair2_2.Enabled = false;
            ddlpicperbaikan2.Enabled = false;
            text_budgets2.Enabled = false;
            text_actualfinishdate2.Enabled = false;
            text_completedate2.Enabled = false;

            text_permintaanperbaikan3.Enabled = false;
            text_descriptionrepair2_3.Enabled = false;
            ddlpicperbaikan3.Enabled = false;
            text_budgets3.Enabled = false;
            text_actualfinishdate3.Enabled = false;
            text_completedate3.Enabled = false;

            text_permintaanperbaikan4.Enabled = false;
            text_descriptionrepair2_4.Enabled = false;
            ddlpicperbaikan4.Enabled = false;
            text_budgets4.Enabled = false;
            text_actualfinishdate4.Enabled = false;
            text_completedate4.Enabled = false;

            text_permintaanperbaikan5.Enabled = false;
            text_descriptionrepair2_5.Enabled = false;
            ddlpicperbaikan5.Enabled = false;
            text_budgets5.Enabled = false;
            text_actualfinishdate5.Enabled = false;
            text_completedate5.Enabled = false;

            text_permintaanperbaikan6.Enabled = false;
            text_descriptionrepair2_6.Enabled = false;
            ddlpicperbaikan6.Enabled = false;
            text_budgets6.Enabled = false;
            text_actualfinishdate6.Enabled = false;
            text_completedate6.Enabled = false;

            text_permintaanperbaikan7.Enabled = false;
            text_descriptionrepair2_7.Enabled = false;
            ddlpicperbaikan7.Enabled = false;
            text_budgets7.Enabled = false;
            text_actualfinishdate7.Enabled = false;
            text_completedate7.Enabled = false;

            text_permintaanperbaikan8.Enabled = false;
            text_descriptionrepair2_8.Enabled = false;
            ddlpicperbaikan8.Enabled = false;
            text_budgets8.Enabled = false;
            text_actualfinishdate8.Enabled = false;
            text_completedate8.Enabled = false;

            text_permintaanperbaikan9.Enabled = false;
            text_descriptionrepair2_9.Enabled = false;
            ddlpicperbaikan9.Enabled = false;
            text_budgets9.Enabled = false;
            text_actualfinishdate9.Enabled = false;
            text_completedate9.Enabled = false;

            text_permintaanperbaikan10.Enabled = false;
            text_descriptionrepair2_10.Enabled = false;
            ddlpicperbaikan10.Enabled = false;
            text_budgets10.Enabled = false;
            text_actualfinishdate10.Enabled = false;
            text_completedate1.Enabled = false;


            linkbtn_filename1.Enabled = true;
            linkbtn_filename2.Enabled = true;
            linkbtn_filename3.Enabled = true;
            linkbtn_filename4.Enabled = true;
            linkbtn_filename5.Enabled = true;
            linkbtn_filename6.Enabled = true;
            linkbtn_filename7.Enabled = true;
            linkbtn_filename8.Enabled = true;
            linkbtn_filename9.Enabled = true;
            linkbtn_filename10.Enabled = true;

            if (text_descriptionrepair2_1.Text == "")
            {
                text_actualfinishdate1.Enabled = false;
            }

            if (text_descriptionrepair2_2.Text == "")
            {
                text_actualfinishdate2.Enabled = false;
            }

            if (text_descriptionrepair2_3.Text == "")
            {
                text_actualfinishdate3.Enabled = false;
            }

            if (text_descriptionrepair2_4.Text == "")
            {
                text_actualfinishdate4.Enabled = false;
            }

            if (text_descriptionrepair2_5.Text == "")
            {
                text_actualfinishdate5.Enabled = false;
            }

            if (text_descriptionrepair2_6.Text == "")
            {
                text_actualfinishdate6.Enabled = false;
            }

            if (text_descriptionrepair2_7.Text == "")
            {
                text_actualfinishdate7.Enabled = false;
            }

            if (text_descriptionrepair2_8.Text == "")
            {
                text_actualfinishdate8.Enabled = false;
            }

            if (text_descriptionrepair2_9.Text == "")
            {
                text_actualfinishdate9.Enabled = false;
            }

            if (text_descriptionrepair2_10.Text == "")
            {
                text_actualfinishdate10.Enabled = false;
            }
            CheckLockPICForm();
        }

        public void ShowButtonUrutan99()
        {
            ddloverbudget.Enabled = false;
            text_overbudgetvalue.Enabled = false;

            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdateSubmit.Visible = false;
            Pnl_OverBudget.Enabled = false;
            Pnl_Others1.Enabled = false;

            btn_ToRevise.Visible = true;
            //btn_Revise.Visible = true;
            btn_Approved.Visible = true;
            btn_Reject.Visible = true;
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

                string KODE_FORM = "FRM-0005";

                string NO_FORM = text_noform.Text;
                string KODE_CUST = "";
                string KODE_CUSTALL = "";
                string KODE_CTALL = "";
                string KODE_CT = "";
                string SITE = "";
                string NAMA_CUST = "";
                string NAMA_CT = "";


                string Where = string.Format("KODE_FORM = 'FRM-0005' AND NO_FORM = '{0}'", NO_FORM);

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

                string KODE_FORM = "FRM-0005";
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

                if (jenis == "STORE")
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

                        KODE_FORM = "FRM-0005";
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
                else if (jenis == "COUNTER")
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

                        KODE_FORM = "FRM-0005";
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

                        KODE_FORM = "FRM-0005";
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

                        KODE_FORM = "FRM-0005";
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

                KODE_FORM = "FRM-0005";
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

                //        KODE_FORM = "FRM-0005";
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
        /// Add Data Permintaan Perbaikan Di TR_FORM5_REPAIR_PERMINTAANS
        /// </summary>
        /// 

        public void LoadDataDetailPermintaanPerbaikan()
        {
            try
            {
                //PICChooseAll();

                TR_FORM5_REPAIR_PERMINTAAN_DA TrForm5RepairPermintaan = new DataLayer.TR_FORM5_REPAIR_PERMINTAAN_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = "";
                string NO_PERMINTAAN = "";
                string PERMINTAAN_PERBAIKAN = "";
                string PERMINTAAN_PERBAIKAN_2 = "";
                string PIC = "";
                string UPLOAD_FILE = "";

                text_budgets1.Text = "0";
                text_budgets2.Text = "0";
                text_budgets3.Text = "0";
                text_budgets4.Text = "0";
                text_budgets5.Text = "0";
                text_budgets6.Text = "0";
                text_budgets7.Text = "0";
                text_budgets8.Text = "0";
                text_budgets9.Text = "0";
                text_budgets10.Text = "0";

                //string TGL_SELESAI = "";
                string Where = string.Format("NO_FORM = '{0}'", HfNO_FORM.Value);
                Ds = TrForm5RepairPermintaan.GetDataFilter(Where);

                int i = 0;
                int index = 0;
                foreach (DataRow Item in Ds.Tables[0].Rows)
                {
                    index = i;
                    i++;

                    if (!String.IsNullOrEmpty((Item.Field<String>("NO_PERMINTAAN"))))
                    {
                        NO_PERMINTAAN = Item.Field<String>("NO_PERMINTAAN");
                    }
                    else
                    {
                        NO_PERMINTAAN = "";
                    }


                    if (!String.IsNullOrEmpty((Item.Field<String>("PERMINTAAN_PERBAIKAN"))))
                    {
                        PERMINTAAN_PERBAIKAN = Item.Field<String>("PERMINTAAN_PERBAIKAN");
                    }
                    else
                    {
                        PERMINTAAN_PERBAIKAN = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("PERMINTAAN_PERBAIKAN_2"))))
                    {
                        PERMINTAAN_PERBAIKAN_2 = Item.Field<String>("PERMINTAAN_PERBAIKAN_2");
                    }
                    else
                    {
                        PERMINTAAN_PERBAIKAN_2 = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("PIC"))))
                    {
                        PIC = Item.Field<String>("PIC");
                    }
                    else
                    {
                        PIC = "Choose";
                    }

                    DateTime? COMPLETE_DATE = Item.Field<DateTime?>("COMPLETE_DATE");
                    if (COMPLETE_DATE == null)
                    {
                        COMPLETE_DATE = new DateTime(1900, 01, 01);
                    }

                    DateTime? ACTUAL_FINISH_DATE = Item.Field<DateTime?>("ACTUAL_FINISH_DATE");
                    if (ACTUAL_FINISH_DATE == null)
                    {
                        ACTUAL_FINISH_DATE = new DateTime(1900, 01, 01);
                    }

                    decimal? BUDGET = Item.Field<decimal?>("BUDGET");
                    if (BUDGET.HasValue)
                    {
                        BUDGET = Item.Field<decimal>("BUDGET");
                    }
                    else
                    {
                        BUDGET = 0;

                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("UPLOAD_FILE"))))
                    {
                        UPLOAD_FILE = Item.Field<String>("UPLOAD_FILE");
                    }
                    else
                    {
                        UPLOAD_FILE = "";
                    }

                    //Load Data Berdasarkan No Permintaan Form Repair
                    if (NO_PERMINTAAN == "1")
                    {
                        text_permintaanperbaikan1.Text = PERMINTAAN_PERBAIKAN;
                        text_descriptionrepair2_1.Text = PERMINTAAN_PERBAIKAN_2;
                        //string test = ddlpicperbaikan1.SelectedValue.ToString();
                        //test = PIC;
                        //ddlpicperbaikan1.DataBind();
                        ddlpicperbaikan1.Text = PIC;

                        text_completedate1.Text = DateTime.Parse(COMPLETE_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_completedate1.Text == "1900-01-01")
                        {
                            text_completedate1.Text = "";
                        }
                        text_actualfinishdate1.Text = DateTime.Parse(ACTUAL_FINISH_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_actualfinishdate1.Text == "1900-01-01")
                        {
                            text_actualfinishdate1.Text = "";
                        }
                        text_budgets1.Text = Convert.ToDecimal(BUDGET).ToString("#,#0.##");
                        linkbtn_filename1.Text = UPLOAD_FILE;
                    }

                    if (NO_PERMINTAAN == "2")
                    {
                        text_permintaanperbaikan2.Text = PERMINTAAN_PERBAIKAN;
                        text_descriptionrepair2_2.Text = PERMINTAAN_PERBAIKAN_2;
                        ddlpicperbaikan2.Text = PIC;
                        text_completedate2.Text = DateTime.Parse(COMPLETE_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_completedate2.Text == "1900-01-01")
                        {
                            text_completedate2.Text = "";
                        }
                        text_actualfinishdate2.Text = DateTime.Parse(ACTUAL_FINISH_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_actualfinishdate2.Text == "1900-01-01")
                        {
                            text_actualfinishdate2.Text = "";
                        }
                        text_budgets2.Text = Convert.ToDecimal(BUDGET).ToString("#,#0.##");
                        linkbtn_filename2.Text = UPLOAD_FILE;
                    }

                    if (NO_PERMINTAAN == "3")
                    {
                        text_permintaanperbaikan3.Text = PERMINTAAN_PERBAIKAN;
                        text_descriptionrepair2_3.Text = PERMINTAAN_PERBAIKAN_2;
                        ddlpicperbaikan3.Text = PIC;
                        text_completedate3.Text = DateTime.Parse(COMPLETE_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_completedate3.Text == "1900-01-01")
                        {
                            text_completedate3.Text = "";
                        }
                        text_actualfinishdate3.Text = DateTime.Parse(ACTUAL_FINISH_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_actualfinishdate3.Text == "1900-01-01")
                        {
                            text_actualfinishdate3.Text = "";
                        }
                        text_budgets3.Text = Convert.ToDecimal(BUDGET).ToString("#,#0.##");
                        linkbtn_filename3.Text = UPLOAD_FILE;

                    }

                    if (NO_PERMINTAAN == "4")
                    {
                        text_permintaanperbaikan4.Text = PERMINTAAN_PERBAIKAN;
                        text_descriptionrepair2_4.Text = PERMINTAAN_PERBAIKAN_2;
                        ddlpicperbaikan4.Text = PIC;
                        text_completedate4.Text = DateTime.Parse(COMPLETE_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_completedate4.Text == "1900-01-01")
                        {
                            text_completedate4.Text = "";
                        }
                        text_actualfinishdate4.Text = DateTime.Parse(ACTUAL_FINISH_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_actualfinishdate4.Text == "1900-01-01")
                        {
                            text_actualfinishdate4.Text = "";
                        }
                        text_budgets4.Text = Convert.ToDecimal(BUDGET).ToString("#,#0.##");
                        linkbtn_filename4.Text = UPLOAD_FILE;

                    }

                    if (NO_PERMINTAAN == "5")
                    {
                        text_permintaanperbaikan5.Text = PERMINTAAN_PERBAIKAN;
                        text_descriptionrepair2_5.Text = PERMINTAAN_PERBAIKAN_2;
                        ddlpicperbaikan5.Text = PIC;
                        text_completedate5.Text = DateTime.Parse(COMPLETE_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_completedate5.Text == "1900-01-01")
                        {
                            text_completedate5.Text = "";
                        }
                        text_actualfinishdate5.Text = DateTime.Parse(ACTUAL_FINISH_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_actualfinishdate5.Text == "1900-01-01")
                        {
                            text_actualfinishdate5.Text = "";
                        }
                        text_budgets5.Text = Convert.ToDecimal(BUDGET).ToString("#,#0.##");
                        linkbtn_filename5.Text = UPLOAD_FILE;

                    }

                    if (NO_PERMINTAAN == "6")
                    {
                        text_permintaanperbaikan6.Text = PERMINTAAN_PERBAIKAN;
                        text_descriptionrepair2_6.Text = PERMINTAAN_PERBAIKAN_2;
                        ddlpicperbaikan6.Text = PIC;
                        text_completedate6.Text = DateTime.Parse(COMPLETE_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_completedate6.Text == "1900-01-01")
                        {
                            text_completedate6.Text = "";
                        }
                        text_actualfinishdate6.Text = DateTime.Parse(ACTUAL_FINISH_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_actualfinishdate6.Text == "1900-01-01")
                        {
                            text_actualfinishdate6.Text = "";
                        }
                        text_budgets6.Text = Convert.ToDecimal(BUDGET).ToString("#,#0.##");
                        linkbtn_filename6.Text = UPLOAD_FILE;
                    }

                    if (NO_PERMINTAAN == "7")
                    {
                        text_permintaanperbaikan7.Text = PERMINTAAN_PERBAIKAN;
                        text_descriptionrepair2_7.Text = PERMINTAAN_PERBAIKAN_2;
                        ddlpicperbaikan7.Text = PIC;
                        text_completedate7.Text = DateTime.Parse(COMPLETE_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_completedate7.Text == "1900-01-01")
                        {
                            text_completedate7.Text = "";
                        }
                        text_actualfinishdate7.Text = DateTime.Parse(ACTUAL_FINISH_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_actualfinishdate7.Text == "1900-01-01")
                        {
                            text_actualfinishdate7.Text = "";
                        }
                        text_budgets7.Text = Convert.ToDecimal(BUDGET).ToString("#,#0.##");
                        linkbtn_filename7.Text = UPLOAD_FILE;
                    }

                    if (NO_PERMINTAAN == "8")
                    {
                        text_permintaanperbaikan8.Text = PERMINTAAN_PERBAIKAN;
                        text_descriptionrepair2_8.Text = PERMINTAAN_PERBAIKAN_2;
                        ddlpicperbaikan8.Text = PIC;
                        text_completedate8.Text = DateTime.Parse(COMPLETE_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_completedate8.Text == "1900-01-01")
                        {
                            text_completedate8.Text = "";
                        }
                        text_actualfinishdate8.Text = DateTime.Parse(ACTUAL_FINISH_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_actualfinishdate8.Text == "1900-01-01")
                        {
                            text_actualfinishdate8.Text = "";
                        }
                        text_budgets8.Text = Convert.ToDecimal(BUDGET).ToString("#,#0.##");
                        linkbtn_filename8.Text = UPLOAD_FILE;
                    }

                    if (NO_PERMINTAAN == "9")
                    {
                        text_permintaanperbaikan9.Text = PERMINTAAN_PERBAIKAN;
                        text_descriptionrepair2_9.Text = PERMINTAAN_PERBAIKAN_2;
                        ddlpicperbaikan9.Text = PIC;
                        text_completedate9.Text = DateTime.Parse(COMPLETE_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_completedate9.Text == "1900-01-01")
                        {
                            text_completedate9.Text = "";
                        }
                        text_actualfinishdate9.Text = DateTime.Parse(ACTUAL_FINISH_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_actualfinishdate9.Text == "1900-01-01")
                        {
                            text_actualfinishdate9.Text = "";
                        }
                        text_budgets9.Text = Convert.ToDecimal(BUDGET).ToString("#,#0.##");
                        linkbtn_filename9.Text = UPLOAD_FILE;
                    }

                    if (NO_PERMINTAAN == "10")
                    {
                        text_permintaanperbaikan10.Text = PERMINTAAN_PERBAIKAN;
                        text_descriptionrepair2_10.Text = PERMINTAAN_PERBAIKAN_2;
                        ddlpicperbaikan10.Text = PIC;
                        text_completedate10.Text = DateTime.Parse(COMPLETE_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_completedate10.Text == "1900-01-01")
                        {
                            text_completedate1.Text = "";
                        }
                        text_actualfinishdate10.Text = DateTime.Parse(ACTUAL_FINISH_DATE.ToString()).ToString("yyyy-MM-dd");

                        if (text_actualfinishdate10.Text == "1900-01-01")
                        {
                            text_actualfinishdate10.Text = "";
                        }
                        text_budgets10.Text = Convert.ToDecimal(BUDGET).ToString("#,#0.##");
                        linkbtn_filename10.Text = UPLOAD_FILE;
                    }
                }

                text_totalbudget.Text = Convert.ToString(Convert.ToDecimal(text_budgets1.Text) + Convert.ToDecimal(text_budgets2.Text) + Convert.ToDecimal(text_budgets3.Text) + Convert.ToDecimal(text_budgets4.Text) + Convert.ToDecimal(text_budgets5.Text) + Convert.ToDecimal(text_budgets6.Text) + Convert.ToDecimal(text_budgets7.Text) + Convert.ToDecimal(text_budgets8.Text) + Convert.ToDecimal(text_budgets9.Text) + Convert.ToDecimal(text_budgets10.Text));


            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void SaveDetailPermintaanPerbaikan()
        {
            try
            {
                GroupSavePermintaanPerbaikan();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //public void UpdateDetailPermintaanPerbaikanProject()
        //{
        //    try
        //    {
        //        GroupUpdatePermintaanPerbaikanProject();
        //    }
        //    catch (Exception Ex)
        //    {
        //        DivMessage.InnerText = Ex.Message;
        //        DivMessage.Attributes["class"] = "error";
        //        //DivMessage.Attributes["class"] = "success";
        //        DivMessage.Visible = true;
        //    }
        //}

        public void UpdateDetailPermintaanPerbaikanActualDate()
        {
            try
            {
                GroupUpdatePermintaanPerbaikanActualDate();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateDetailPermintaanPerbaikan()
        {
            try
            {
                GroupUpdatePermintaanPerbaikan();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        public void UpdateDetailPermintaanPerbaikanProject()
        {
            try
            {
                GroupUpdatePermintaanPerbaikanProject();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        public void GroupSavePermintaanPerbaikan()
        {
            try
            {
                TR_FORM5_REPAIR_PERMINTAAN_DA TrForm5RepairPermintaan = new DataLayer.TR_FORM5_REPAIR_PERMINTAAN_DA();
                DataSet Ds = new DataSet();

                TR_FORM5_REPAIR_PERMINTAAN trform5repairpermintaan = new TR_FORM5_REPAIR_PERMINTAAN();

                //Group Save Detail Permintaan Perbaikan
                string NO_FORM = text_noform.Text;
                string NO_PERMINTAAN_1 = "1";
                string NO_PERMINTAAN_2 = "2";
                string NO_PERMINTAAN_3 = "3";
                string NO_PERMINTAAN_4 = "4";
                string NO_PERMINTAAN_5 = "5";
                string NO_PERMINTAAN_6 = "6";
                string NO_PERMINTAAN_7 = "7";
                string NO_PERMINTAAN_8 = "8";
                string NO_PERMINTAAN_9 = "9";
                string NO_PERMINTAAN_10 = "10";
                string PERMINTAAN_PERBAIKAN_1 = text_permintaanperbaikan1.Text;
                string PERMINTAAN_PERBAIKAN_2 = text_permintaanperbaikan2.Text;
                string PERMINTAAN_PERBAIKAN_3 = text_permintaanperbaikan3.Text;
                string PERMINTAAN_PERBAIKAN_4 = text_permintaanperbaikan4.Text;
                string PERMINTAAN_PERBAIKAN_5 = text_permintaanperbaikan5.Text;
                string PERMINTAAN_PERBAIKAN_6 = text_permintaanperbaikan6.Text;
                string PERMINTAAN_PERBAIKAN_7 = text_permintaanperbaikan7.Text;
                string PERMINTAAN_PERBAIKAN_8 = text_permintaanperbaikan8.Text;
                string PERMINTAAN_PERBAIKAN_9 = text_permintaanperbaikan9.Text;
                string PERMINTAAN_PERBAIKAN_10 = text_permintaanperbaikan10.Text;
                string PERMINTAAN_PERBAIKAN_2_1 = text_descriptionrepair2_1.Text;
                string PERMINTAAN_PERBAIKAN_2_2 = text_descriptionrepair2_2.Text;
                string PERMINTAAN_PERBAIKAN_2_3 = text_descriptionrepair2_3.Text;
                string PERMINTAAN_PERBAIKAN_2_4 = text_descriptionrepair2_4.Text;
                string PERMINTAAN_PERBAIKAN_2_5 = text_descriptionrepair2_5.Text;
                string PERMINTAAN_PERBAIKAN_2_6 = text_descriptionrepair2_6.Text;
                string PERMINTAAN_PERBAIKAN_2_7 = text_descriptionrepair2_7.Text;
                string PERMINTAAN_PERBAIKAN_2_8 = text_descriptionrepair2_8.Text;
                string PERMINTAAN_PERBAIKAN_2_9 = text_descriptionrepair2_9.Text;
                string PERMINTAAN_PERBAIKAN_2_10 = text_descriptionrepair2_10.Text;
                string PIC_PERBAIKAN_1 = ddlpicperbaikan1.Text;
                string PIC_PERBAIKAN_2 = ddlpicperbaikan2.Text;
                string PIC_PERBAIKAN_3 = ddlpicperbaikan3.Text;
                string PIC_PERBAIKAN_4 = ddlpicperbaikan4.Text;
                string PIC_PERBAIKAN_5 = ddlpicperbaikan5.Text;
                string PIC_PERBAIKAN_6 = ddlpicperbaikan6.Text;
                string PIC_PERBAIKAN_7 = ddlpicperbaikan7.Text;
                string PIC_PERBAIKAN_8 = ddlpicperbaikan8.Text;
                string PIC_PERBAIKAN_9 = ddlpicperbaikan9.Text;
                string PIC_PERBAIKAN_10 = ddlpicperbaikan10.Text;
                DateTime? COMPLETE_DATE1 = string.IsNullOrEmpty(text_completedate1.Text) ? (DateTime?)null : DateTime.Parse(text_completedate1.Text);
                DateTime? COMPLETE_DATE2 = string.IsNullOrEmpty(text_completedate2.Text) ? (DateTime?)null : DateTime.Parse(text_completedate2.Text);
                DateTime? COMPLETE_DATE3 = string.IsNullOrEmpty(text_completedate3.Text) ? (DateTime?)null : DateTime.Parse(text_completedate3.Text);
                DateTime? COMPLETE_DATE4 = string.IsNullOrEmpty(text_completedate4.Text) ? (DateTime?)null : DateTime.Parse(text_completedate4.Text);
                DateTime? COMPLETE_DATE5 = string.IsNullOrEmpty(text_completedate5.Text) ? (DateTime?)null : DateTime.Parse(text_completedate5.Text);
                DateTime? COMPLETE_DATE6 = string.IsNullOrEmpty(text_completedate6.Text) ? (DateTime?)null : DateTime.Parse(text_completedate6.Text);
                DateTime? COMPLETE_DATE7 = string.IsNullOrEmpty(text_completedate7.Text) ? (DateTime?)null : DateTime.Parse(text_completedate7.Text);
                DateTime? COMPLETE_DATE8 = string.IsNullOrEmpty(text_completedate8.Text) ? (DateTime?)null : DateTime.Parse(text_completedate8.Text);
                DateTime? COMPLETE_DATE9 = string.IsNullOrEmpty(text_completedate9.Text) ? (DateTime?)null : DateTime.Parse(text_completedate9.Text);
                DateTime? COMPLETE_DATE10 = string.IsNullOrEmpty(text_completedate10.Text) ? (DateTime?)null : DateTime.Parse(text_completedate10.Text);
                DateTime? ACTUAL_FINISH_DATE1 = string.IsNullOrEmpty(text_actualfinishdate1.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate1.Text);
                DateTime? ACTUAL_FINISH_DATE2 = string.IsNullOrEmpty(text_actualfinishdate2.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate2.Text);
                DateTime? ACTUAL_FINISH_DATE3 = string.IsNullOrEmpty(text_actualfinishdate3.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate3.Text);
                DateTime? ACTUAL_FINISH_DATE4 = string.IsNullOrEmpty(text_actualfinishdate4.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate4.Text);
                DateTime? ACTUAL_FINISH_DATE5 = string.IsNullOrEmpty(text_actualfinishdate5.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate5.Text);
                DateTime? ACTUAL_FINISH_DATE6 = string.IsNullOrEmpty(text_actualfinishdate6.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate6.Text);
                DateTime? ACTUAL_FINISH_DATE7 = string.IsNullOrEmpty(text_actualfinishdate7.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate7.Text);
                DateTime? ACTUAL_FINISH_DATE8 = string.IsNullOrEmpty(text_actualfinishdate8.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate8.Text);
                DateTime? ACTUAL_FINISH_DATE9 = string.IsNullOrEmpty(text_actualfinishdate9.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate9.Text);
                DateTime? ACTUAL_FINISH_DATE10 = string.IsNullOrEmpty(text_actualfinishdate10.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate10.Text);
                string BUDGET1 = text_budgets1.Text;
                string BUDGET2 = text_budgets2.Text;
                string BUDGET3 = text_budgets3.Text;
                string BUDGET4 = text_budgets4.Text;
                string BUDGET5 = text_budgets5.Text;
                string BUDGET6 = text_budgets6.Text;
                string BUDGET7 = text_budgets7.Text;
                string BUDGET8 = text_budgets8.Text;
                string BUDGET9 = text_budgets9.Text;
                string BUDGET10 = text_budgets10.Text;
                string UPLOAD_FILE_PERBAIKAN_1 = "";
                string UPLOAD_FILE_PERBAIKAN_2 = "";
                string UPLOAD_FILE_PERBAIKAN_3 = "";
                string UPLOAD_FILE_PERBAIKAN_4 = "";
                string UPLOAD_FILE_PERBAIKAN_5 = "";
                string UPLOAD_FILE_PERBAIKAN_6 = "";
                string UPLOAD_FILE_PERBAIKAN_7 = "";
                string UPLOAD_FILE_PERBAIKAN_8 = "";
                string UPLOAD_FILE_PERBAIKAN_9 = "";
                string UPLOAD_FILE_PERBAIKAN_10 = "";
                DateTime startdate = new DateTime(1900, 01, 01);


                if (PERMINTAAN_PERBAIKAN_1 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_1;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_1;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_1;
                    trform5repairpermintaan.PIC = "";
                    trform5repairpermintaan.COMPLETE_DATE = startdate;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = 0;

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
                                    UPLOAD_FILE_PERBAIKAN_1 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "1" + btn_uploadfile1.FileName;
                                    btn_uploadfile1.PostedFile
                  .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_1);
                                }
                            }
                        }
                    }
                    else
                    {
                        UPLOAD_FILE_PERBAIKAN_1 = linkbtn_filename1.Text;
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_1;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_2;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_2;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_2;
                    trform5repairpermintaan.PIC = "";
                    trform5repairpermintaan.COMPLETE_DATE = startdate;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = 0;

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
                                    UPLOAD_FILE_PERBAIKAN_2 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "2" + btn_uploadfile2.FileName;
                                    btn_uploadfile2.PostedFile
                  .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_2);
                                }
                            }
                        }
                    }
                    else
                    {
                        UPLOAD_FILE_PERBAIKAN_2 = linkbtn_filename2.Text;
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_2;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_3 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_3;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_3;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_3;
                    trform5repairpermintaan.PIC = "";
                    trform5repairpermintaan.COMPLETE_DATE = startdate;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = 0;

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
                                    UPLOAD_FILE_PERBAIKAN_3 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "3" + btn_uploadfile3.FileName;
                                    btn_uploadfile3.PostedFile
                  .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_3);
                                }
                            }
                        }
                    }
                    else
                    {
                        UPLOAD_FILE_PERBAIKAN_3 = linkbtn_filename3.Text;
                    }



                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_3;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);

                }

                if (PERMINTAAN_PERBAIKAN_4 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_4;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_4;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_4;
                    trform5repairpermintaan.PIC = "";
                    trform5repairpermintaan.COMPLETE_DATE = startdate;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = 0;

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
                                    UPLOAD_FILE_PERBAIKAN_4 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "4" + btn_uploadfile4.FileName;
                                    btn_uploadfile4.PostedFile
                  .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_4);
                                }
                            }
                        }
                    }
                    else
                    {
                        UPLOAD_FILE_PERBAIKAN_4 = linkbtn_filename4.Text;
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_4;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_5 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_5;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_5;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_5;
                    trform5repairpermintaan.PIC = "";
                    trform5repairpermintaan.COMPLETE_DATE = startdate;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = 0;

                    if (linkbtn_filename5.Text == "-")
                    {
                        if (btn_uploadfile5.HasFile)
                        {
                            int imgSize = btn_uploadfile5.PostedFile.ContentLength;
                            string ext = System.IO.Path.GetExtension(this.btn_uploadfile5.PostedFile.FileName).ToLower();
                            if (btn_uploadfile5.PostedFile != null && btn_uploadfile5.PostedFile.FileName != "")
                            {


                                if (btn_uploadfile5.PostedFile.ContentLength > 3000000)
                                {
                                    DivMessage.InnerText = "File 5 is larger than 3MB.";
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
                                    UPLOAD_FILE_PERBAIKAN_5 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "5" + btn_uploadfile5.FileName;
                                    btn_uploadfile5.PostedFile
                  .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_5);
                                }
                            }
                        }
                    }
                    else
                    {
                        UPLOAD_FILE_PERBAIKAN_5 = linkbtn_filename5.Text;
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_5;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_6 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_6;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_6;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_6;
                    trform5repairpermintaan.PIC = "";
                    trform5repairpermintaan.COMPLETE_DATE = startdate;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = 0;

                    if (linkbtn_filename6.Text == "-")
                    {
                        if (btn_uploadfile6.HasFile)
                        {
                            int imgSize = btn_uploadfile6.PostedFile.ContentLength;
                            string ext = System.IO.Path.GetExtension(this.btn_uploadfile6.PostedFile.FileName).ToLower();
                            if (btn_uploadfile6.PostedFile != null && btn_uploadfile6.PostedFile.FileName != "")
                            {


                                if (btn_uploadfile6.PostedFile.ContentLength > 3000000)
                                {
                                    DivMessage.InnerText = "File 6 is larger than 3MB.";
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
                                    UPLOAD_FILE_PERBAIKAN_6 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "6" + btn_uploadfile6.FileName;
                                    btn_uploadfile6.PostedFile
                  .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_6);
                                }
                            }
                        }
                    }
                    else
                    {
                        UPLOAD_FILE_PERBAIKAN_6 = linkbtn_filename6.Text;
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_6;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_7 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_7;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_7;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_7;
                    trform5repairpermintaan.PIC = "";
                    trform5repairpermintaan.COMPLETE_DATE = startdate;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = 0;

                    if (linkbtn_filename7.Text == "-")
                    {
                        if (btn_uploadfile7.HasFile)
                        {
                            int imgSize = btn_uploadfile7.PostedFile.ContentLength;
                            string ext = System.IO.Path.GetExtension(this.btn_uploadfile7.PostedFile.FileName).ToLower();
                            if (btn_uploadfile7.PostedFile != null && btn_uploadfile7.PostedFile.FileName != "")
                            {


                                if (btn_uploadfile7.PostedFile.ContentLength > 3000000)
                                {
                                    DivMessage.InnerText = "File 7 is larger than 3MB.";
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
                                    UPLOAD_FILE_PERBAIKAN_7 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "7" + btn_uploadfile7.FileName;
                                    btn_uploadfile7.PostedFile
                  .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_7);
                                }
                            }
                        }
                    }
                    else
                    {
                        UPLOAD_FILE_PERBAIKAN_7 = linkbtn_filename7.Text;
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_7;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_8 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_8;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_8;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_8;
                    trform5repairpermintaan.PIC = "";
                    trform5repairpermintaan.COMPLETE_DATE = startdate;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = 0;

                    if (linkbtn_filename8.Text == "-")
                    {
                        if (btn_uploadfile8.HasFile)
                        {
                            int imgSize = btn_uploadfile8.PostedFile.ContentLength;
                            string ext = System.IO.Path.GetExtension(this.btn_uploadfile8.PostedFile.FileName).ToLower();
                            if (btn_uploadfile8.PostedFile != null && btn_uploadfile8.PostedFile.FileName != "")
                            {


                                if (btn_uploadfile8.PostedFile.ContentLength > 3000000)
                                {
                                    DivMessage.InnerText = "File 8 is larger than 3MB.";
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
                                    UPLOAD_FILE_PERBAIKAN_8 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "8" + btn_uploadfile8.FileName;
                                    btn_uploadfile8.PostedFile
                  .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_8);
                                }
                            }
                        }
                    }
                    else
                    {
                        UPLOAD_FILE_PERBAIKAN_8 = linkbtn_filename8.Text;
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_8;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_9 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_9;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_9;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_9;
                    trform5repairpermintaan.PIC = "";
                    trform5repairpermintaan.COMPLETE_DATE = startdate;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = 0;

                    if (linkbtn_filename9.Text == "-")
                    {
                        if (btn_uploadfile9.HasFile)
                        {
                            int imgSize = btn_uploadfile9.PostedFile.ContentLength;
                            string ext = System.IO.Path.GetExtension(this.btn_uploadfile9.PostedFile.FileName).ToLower();
                            if (btn_uploadfile9.PostedFile != null && btn_uploadfile9.PostedFile.FileName != "")
                            {


                                if (btn_uploadfile9.PostedFile.ContentLength > 3000000)
                                {
                                    DivMessage.InnerText = "File 9 is larger than 3MB.";
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
                                    UPLOAD_FILE_PERBAIKAN_9 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "9" + btn_uploadfile9.FileName;
                                    btn_uploadfile9.PostedFile
                  .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_9);
                                }
                            }
                        }
                    }
                    else
                    {
                        UPLOAD_FILE_PERBAIKAN_9 = linkbtn_filename9.Text;
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_9;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_10 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_10;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_10;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_10;
                    trform5repairpermintaan.PIC = "";
                    trform5repairpermintaan.COMPLETE_DATE = startdate;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = 0;

                    if (linkbtn_filename10.Text == "-")
                    {
                        if (btn_uploadfile10.HasFile)
                        {
                            int imgSize = btn_uploadfile10.PostedFile.ContentLength;
                            string ext = System.IO.Path.GetExtension(this.btn_uploadfile10.PostedFile.FileName).ToLower();
                            if (btn_uploadfile10.PostedFile != null && btn_uploadfile10.PostedFile.FileName != "")
                            {


                                if (btn_uploadfile10.PostedFile.ContentLength > 3000000)
                                {
                                    DivMessage.InnerText = "File 10 is larger than 3MB.";
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
                                    UPLOAD_FILE_PERBAIKAN_10 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "10" + btn_uploadfile10.FileName;
                                    btn_uploadfile10.PostedFile
                  .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_10);
                                }
                            }
                        }
                    }
                    else
                    {
                        UPLOAD_FILE_PERBAIKAN_10 = linkbtn_filename10.Text;
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_10;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
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

        public void GroupSavePermintaanPerbaikanProject()
        {
            try
            {
                TR_FORM5_REPAIR_PERMINTAAN_DA TrForm5RepairPermintaan = new DataLayer.TR_FORM5_REPAIR_PERMINTAAN_DA();
                DataSet Ds = new DataSet();

                TR_FORM5_REPAIR_PERMINTAAN trform5repairpermintaan = new TR_FORM5_REPAIR_PERMINTAAN();

                //Group Save Detail Permintaan Perbaikan
                string NO_FORM = text_noform.Text;
                string NO_PERMINTAAN_1 = "1";
                string NO_PERMINTAAN_2 = "2";
                string NO_PERMINTAAN_3 = "3";
                string NO_PERMINTAAN_4 = "4";
                string NO_PERMINTAAN_5 = "5";
                string NO_PERMINTAAN_6 = "6";
                string NO_PERMINTAAN_7 = "7";
                string NO_PERMINTAAN_8 = "8";
                string NO_PERMINTAAN_9 = "9";
                string NO_PERMINTAAN_10 = "10";
                string PERMINTAAN_PERBAIKAN_1 = text_permintaanperbaikan1.Text;
                string PERMINTAAN_PERBAIKAN_2 = text_permintaanperbaikan2.Text;
                string PERMINTAAN_PERBAIKAN_3 = text_permintaanperbaikan3.Text;
                string PERMINTAAN_PERBAIKAN_4 = text_permintaanperbaikan4.Text;
                string PERMINTAAN_PERBAIKAN_5 = text_permintaanperbaikan5.Text;
                string PERMINTAAN_PERBAIKAN_6 = text_permintaanperbaikan6.Text;
                string PERMINTAAN_PERBAIKAN_7 = text_permintaanperbaikan7.Text;
                string PERMINTAAN_PERBAIKAN_8 = text_permintaanperbaikan8.Text;
                string PERMINTAAN_PERBAIKAN_9 = text_permintaanperbaikan9.Text;
                string PERMINTAAN_PERBAIKAN_10 = text_permintaanperbaikan10.Text;
                string PERMINTAAN_PERBAIKAN_2_1 = text_descriptionrepair2_1.Text;
                string PERMINTAAN_PERBAIKAN_2_2 = text_descriptionrepair2_2.Text;
                string PERMINTAAN_PERBAIKAN_2_3 = text_descriptionrepair2_3.Text;
                string PERMINTAAN_PERBAIKAN_2_4 = text_descriptionrepair2_4.Text;
                string PERMINTAAN_PERBAIKAN_2_5 = text_descriptionrepair2_5.Text;
                string PERMINTAAN_PERBAIKAN_2_6 = text_descriptionrepair2_6.Text;
                string PERMINTAAN_PERBAIKAN_2_7 = text_descriptionrepair2_7.Text;
                string PERMINTAAN_PERBAIKAN_2_8 = text_descriptionrepair2_8.Text;
                string PERMINTAAN_PERBAIKAN_2_9 = text_descriptionrepair2_9.Text;
                string PERMINTAAN_PERBAIKAN_2_10 = text_descriptionrepair2_10.Text;
                string PIC_PERBAIKAN_1 = ddlpicperbaikan1.Text;
                string PIC_PERBAIKAN_2 = ddlpicperbaikan2.Text;
                string PIC_PERBAIKAN_3 = ddlpicperbaikan3.Text;
                string PIC_PERBAIKAN_4 = ddlpicperbaikan4.Text;
                string PIC_PERBAIKAN_5 = ddlpicperbaikan5.Text;
                string PIC_PERBAIKAN_6 = ddlpicperbaikan6.Text;
                string PIC_PERBAIKAN_7 = ddlpicperbaikan7.Text;
                string PIC_PERBAIKAN_8 = ddlpicperbaikan8.Text;
                string PIC_PERBAIKAN_9 = ddlpicperbaikan9.Text;
                string PIC_PERBAIKAN_10 = ddlpicperbaikan10.Text;
                string COMPLETE_DATE1 = "";
                if (text_completedate1.Text != "")
                {
                    COMPLETE_DATE1 = DateTime.Parse(text_completedate1.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    COMPLETE_DATE1 = "1900-01-01";
                }

                string COMPLETE_DATE2 = "";
                if (text_completedate2.Text != "")
                {
                    COMPLETE_DATE2 = DateTime.Parse(text_completedate2.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    COMPLETE_DATE2 = "1900-01-01";
                }

                string COMPLETE_DATE3 = "";
                if (text_completedate3.Text != "")
                {
                    COMPLETE_DATE3 = DateTime.Parse(text_completedate3.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    COMPLETE_DATE3 = "1900-01-01";
                }

                string COMPLETE_DATE4 = "";
                if (text_completedate4.Text != "")
                {
                    COMPLETE_DATE4 = DateTime.Parse(text_completedate4.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    COMPLETE_DATE4 = "1900-01-01";
                }

                string COMPLETE_DATE5 = "";
                if (text_completedate5.Text != "")
                {
                    COMPLETE_DATE5 = DateTime.Parse(text_completedate5.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    COMPLETE_DATE5 = "1900-01-01";
                }

                string COMPLETE_DATE6 = "";
                if (text_completedate6.Text != "")
                {
                    COMPLETE_DATE6 = DateTime.Parse(text_completedate6.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    COMPLETE_DATE6 = "1900-01-01";
                }

                string COMPLETE_DATE7 = "";
                if (text_completedate7.Text != "")
                {
                    COMPLETE_DATE7 = DateTime.Parse(text_completedate7.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    COMPLETE_DATE7 = "1900-01-01";
                }

                string COMPLETE_DATE8 = "";
                if (text_completedate8.Text != "")
                {
                    COMPLETE_DATE8 = DateTime.Parse(text_completedate8.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    COMPLETE_DATE8 = "1900-01-01";
                }

                string COMPLETE_DATE9 = "";
                if (text_completedate9.Text != "")
                {
                    COMPLETE_DATE9 = DateTime.Parse(text_completedate9.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    COMPLETE_DATE9 = "1900-01-01";
                }

                string COMPLETE_DATE10 = "";
                if (text_completedate10.Text != "")
                {
                    COMPLETE_DATE10 = DateTime.Parse(text_completedate10.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    COMPLETE_DATE10 = "1900-01-01";
                }
                //DateTime? COMPLETE_DATE1 = string.IsNullOrEmpty(text_completedate1.Text) ? (DateTime?)null : DateTime.Parse(text_completedate1.Text);
                //DateTime? COMPLETE_DATE2 = string.IsNullOrEmpty(text_completedate2.Text) ? (DateTime?)null : DateTime.Parse(text_completedate2.Text);
                //DateTime? COMPLETE_DATE3 = string.IsNullOrEmpty(text_completedate3.Text) ? (DateTime?)null : DateTime.Parse(text_completedate3.Text);
                //DateTime? COMPLETE_DATE4 = string.IsNullOrEmpty(text_completedate4.Text) ? (DateTime?)null : DateTime.Parse(text_completedate4.Text);
                //DateTime? COMPLETE_DATE5 = string.IsNullOrEmpty(text_completedate5.Text) ? (DateTime?)null : DateTime.Parse(text_completedate5.Text);
                //DateTime? COMPLETE_DATE6 = string.IsNullOrEmpty(text_completedate6.Text) ? (DateTime?)null : DateTime.Parse(text_completedate6.Text);
                //DateTime? COMPLETE_DATE7 = string.IsNullOrEmpty(text_completedate7.Text) ? (DateTime?)null : DateTime.Parse(text_completedate7.Text);
                //DateTime? COMPLETE_DATE8 = string.IsNullOrEmpty(text_completedate8.Text) ? (DateTime?)null : DateTime.Parse(text_completedate8.Text);
                //DateTime? COMPLETE_DATE9 = string.IsNullOrEmpty(text_completedate9.Text) ? (DateTime?)null : DateTime.Parse(text_completedate9.Text);
                //DateTime? COMPLETE_DATE10 = string.IsNullOrEmpty(text_completedate10.Text) ? (DateTime?)null : DateTime.Parse(text_completedate10.Text);
                DateTime? ACTUAL_FINISH_DATE1 = string.IsNullOrEmpty(text_actualfinishdate1.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate1.Text);
                DateTime? ACTUAL_FINISH_DATE2 = string.IsNullOrEmpty(text_actualfinishdate2.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate2.Text);
                DateTime? ACTUAL_FINISH_DATE3 = string.IsNullOrEmpty(text_actualfinishdate3.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate3.Text);
                DateTime? ACTUAL_FINISH_DATE4 = string.IsNullOrEmpty(text_actualfinishdate4.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate4.Text);
                DateTime? ACTUAL_FINISH_DATE5 = string.IsNullOrEmpty(text_actualfinishdate5.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate5.Text);
                DateTime? ACTUAL_FINISH_DATE6 = string.IsNullOrEmpty(text_actualfinishdate6.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate6.Text);
                DateTime? ACTUAL_FINISH_DATE7 = string.IsNullOrEmpty(text_actualfinishdate7.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate7.Text);
                DateTime? ACTUAL_FINISH_DATE8 = string.IsNullOrEmpty(text_actualfinishdate8.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate8.Text);
                DateTime? ACTUAL_FINISH_DATE9 = string.IsNullOrEmpty(text_actualfinishdate9.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate9.Text);
                DateTime? ACTUAL_FINISH_DATE10 = string.IsNullOrEmpty(text_actualfinishdate10.Text) ? (DateTime?)null : DateTime.Parse(text_actualfinishdate10.Text);
                string BUDGET1 = text_budgets1.Text;
                string BUDGET2 = text_budgets2.Text;
                string BUDGET3 = text_budgets3.Text;
                string BUDGET4 = text_budgets4.Text;
                string BUDGET5 = text_budgets5.Text;
                string BUDGET6 = text_budgets6.Text;
                string BUDGET7 = text_budgets7.Text;
                string BUDGET8 = text_budgets8.Text;
                string BUDGET9 = text_budgets9.Text;
                string BUDGET10 = text_budgets10.Text;
                string UPLOAD_FILE_PERBAIKAN_1 = linkbtn_filename1.Text;
                string UPLOAD_FILE_PERBAIKAN_2 = linkbtn_filename2.Text;
                string UPLOAD_FILE_PERBAIKAN_3 = linkbtn_filename3.Text;
                string UPLOAD_FILE_PERBAIKAN_4 = linkbtn_filename4.Text;
                string UPLOAD_FILE_PERBAIKAN_5 = linkbtn_filename5.Text;
                string UPLOAD_FILE_PERBAIKAN_6 = linkbtn_filename6.Text;
                string UPLOAD_FILE_PERBAIKAN_7 = linkbtn_filename7.Text;
                string UPLOAD_FILE_PERBAIKAN_8 = linkbtn_filename8.Text;
                string UPLOAD_FILE_PERBAIKAN_9 = linkbtn_filename9.Text;
                string UPLOAD_FILE_PERBAIKAN_10 = linkbtn_filename10.Text;
                DateTime startdate = new DateTime(1900, 01, 01);


                if (PERMINTAAN_PERBAIKAN_2_1 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_1;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_1;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_1;
                    trform5repairpermintaan.PIC = PIC_PERBAIKAN_1;
                    trform5repairpermintaan.COMPLETE_DATE = Convert.ToDateTime(COMPLETE_DATE1);
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET1);

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
                                UPLOAD_FILE_PERBAIKAN_1 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfile1.FileName;
                                btn_uploadfile1.PostedFile
              .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_1);
                            }
                        }
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_1;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_2 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_2;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_2;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_2;
                    trform5repairpermintaan.PIC = PIC_PERBAIKAN_2;
                    trform5repairpermintaan.COMPLETE_DATE = Convert.ToDateTime(COMPLETE_DATE2);
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET2);

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
                                UPLOAD_FILE_PERBAIKAN_2 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfile2.FileName;
                                btn_uploadfile2.PostedFile
              .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_2);
                            }
                        }
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_2;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_3 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_3;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_3;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_3;
                    trform5repairpermintaan.PIC = PIC_PERBAIKAN_3;
                    trform5repairpermintaan.COMPLETE_DATE = Convert.ToDateTime(COMPLETE_DATE3);
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET3);

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
                                UPLOAD_FILE_PERBAIKAN_3 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfile3.FileName;
                                btn_uploadfile3.PostedFile
              .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_3);
                            }
                        }
                    }



                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_3;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);

                }

                if (PERMINTAAN_PERBAIKAN_2_4 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_4;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_4;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_4;
                    trform5repairpermintaan.PIC = PIC_PERBAIKAN_4;
                    trform5repairpermintaan.COMPLETE_DATE = Convert.ToDateTime(COMPLETE_DATE4);
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET4);

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
                                UPLOAD_FILE_PERBAIKAN_4 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfile4.FileName;
                                btn_uploadfile4.PostedFile
              .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_4);
                            }
                        }
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_4;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_5 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_5;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_5;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_5;
                    trform5repairpermintaan.PIC = PIC_PERBAIKAN_5;
                    trform5repairpermintaan.COMPLETE_DATE = Convert.ToDateTime(COMPLETE_DATE5);
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET5);

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
                                UPLOAD_FILE_PERBAIKAN_5 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfile5.FileName;
                                btn_uploadfile5.PostedFile
              .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_5);
                            }
                        }
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_5;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_6 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_6;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_6;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_6;
                    trform5repairpermintaan.PIC = PIC_PERBAIKAN_6;
                    trform5repairpermintaan.COMPLETE_DATE = Convert.ToDateTime(COMPLETE_DATE6);
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET6);

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
                                UPLOAD_FILE_PERBAIKAN_6 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfile6.FileName;
                                btn_uploadfile6.PostedFile
              .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_6);
                            }
                        }
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_6;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_7 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_7;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_7;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_7;
                    trform5repairpermintaan.PIC = PIC_PERBAIKAN_7;
                    trform5repairpermintaan.COMPLETE_DATE = Convert.ToDateTime(COMPLETE_DATE7);
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET7);

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
                                UPLOAD_FILE_PERBAIKAN_7 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfile7.FileName;
                                btn_uploadfile7.PostedFile
              .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_7);
                            }
                        }
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_7;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_8 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_8;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_8;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_8;
                    trform5repairpermintaan.PIC = PIC_PERBAIKAN_8;
                    trform5repairpermintaan.COMPLETE_DATE = Convert.ToDateTime(COMPLETE_DATE8);
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET8);

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
                                UPLOAD_FILE_PERBAIKAN_8 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfile8.FileName;
                                btn_uploadfile8.PostedFile
              .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_8);
                            }
                        }
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_8;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_9 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_9;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_9;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_9;
                    trform5repairpermintaan.PIC = PIC_PERBAIKAN_9;
                    trform5repairpermintaan.COMPLETE_DATE = Convert.ToDateTime(COMPLETE_DATE9);
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET9);

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
                                UPLOAD_FILE_PERBAIKAN_9 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfile9.FileName;
                                btn_uploadfile9.PostedFile
              .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_9);
                            }
                        }
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_9;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_10 != "")
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.NO_PERMINTAAN = NO_PERMINTAAN_10;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_10;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_10;
                    trform5repairpermintaan.PIC = PIC_PERBAIKAN_10;
                    trform5repairpermintaan.COMPLETE_DATE = Convert.ToDateTime(COMPLETE_DATE10);
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = startdate;
                    trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET10);

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
                                UPLOAD_FILE_PERBAIKAN_10 = text_noform.Text + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + btn_uploadfile10.FileName;
                                btn_uploadfile10.PostedFile
              .SaveAs(Server.MapPath("~/Uploaded/FileUploadRPR/") + UPLOAD_FILE_PERBAIKAN_10);
                            }
                        }
                    }

                    trform5repairpermintaan.UPLOAD_FILE = UPLOAD_FILE_PERBAIKAN_10;
                    TrForm5RepairPermintaan.Insert(trform5repairpermintaan);
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


        //public void GroupUpdatePermintaanPerbaikanProject()
        //{
        //    try
        //    {
        //        TR_FORM5_REPAIR_PERMINTAAN_DA TrForm5RepairPermintaan = new DataLayer.TR_FORM5_REPAIR_PERMINTAAN_DA();
        //        DataSet Ds = new DataSet();

        //        TR_FORM5_REPAIR_PERMINTAAN trform5repairpermintaan = new TR_FORM5_REPAIR_PERMINTAAN();

        //        //Group Save Detail Permintaan Perbaikan
        //        string NO_FORM = text_noform.Text;
        //        string PERMINTAAN_PERBAIKAN_1 = text_permintaanperbaikan1.Text;
        //        string PERMINTAAN_PERBAIKAN_2 = text_permintaanperbaikan2.Text;
        //        string PERMINTAAN_PERBAIKAN_3 = text_permintaanperbaikan3.Text;
        //        string PERMINTAAN_PERBAIKAN_4 = text_permintaanperbaikan4.Text;
        //        string PERMINTAAN_PERBAIKAN_5 = text_permintaanperbaikan5.Text;
        //        string PERMINTAAN_PERBAIKAN_6 = text_permintaanperbaikan6.Text;
        //        string PERMINTAAN_PERBAIKAN_7 = text_permintaanperbaikan7.Text;
        //        string PERMINTAAN_PERBAIKAN_8 = text_permintaanperbaikan8.Text;
        //        string PERMINTAAN_PERBAIKAN_9 = text_permintaanperbaikan9.Text;
        //        string PERMINTAAN_PERBAIKAN_10 = text_permintaanperbaikan10.Text;
        //        string PIC_PERBAIKAN_1 = text_picperbaikan1.Text;
        //        string PIC_PERBAIKAN_2 = text_picperbaikan2.Text;
        //        string PIC_PERBAIKAN_3 = text_picperbaikan3.Text;
        //        string PIC_PERBAIKAN_4 = text_picperbaikan4.Text;
        //        string PIC_PERBAIKAN_5 = text_picperbaikan5.Text;
        //        string PIC_PERBAIKAN_6 = text_picperbaikan6.Text;
        //        string PIC_PERBAIKAN_7 = text_picperbaikan7.Text;
        //        string PIC_PERBAIKAN_8 = text_picperbaikan8.Text;
        //        string PIC_PERBAIKAN_9 = text_picperbaikan9.Text;
        //        string PIC_PERBAIKAN_10 = text_picperbaikan10.Text;
        //        DateTime? COMPLETE_DATE1 = string.IsNullOrEmpty(text_completedate1.Text) ? (DateTime?)null : DateTime.Parse(text_completedate1.Text);
        //        DateTime? COMPLETE_DATE2 = string.IsNullOrEmpty(text_completedate2.Text) ? (DateTime?)null : DateTime.Parse(text_completedate2.Text);
        //        DateTime? COMPLETE_DATE3 = string.IsNullOrEmpty(text_completedate3.Text) ? (DateTime?)null : DateTime.Parse(text_completedate3.Text);
        //        DateTime? COMPLETE_DATE4 = string.IsNullOrEmpty(text_completedate4.Text) ? (DateTime?)null : DateTime.Parse(text_completedate4.Text);
        //        DateTime? COMPLETE_DATE5 = string.IsNullOrEmpty(text_completedate5.Text) ? (DateTime?)null : DateTime.Parse(text_completedate5.Text);
        //        DateTime? COMPLETE_DATE6 = string.IsNullOrEmpty(text_completedate6.Text) ? (DateTime?)null : DateTime.Parse(text_completedate6.Text);
        //        DateTime? COMPLETE_DATE7 = string.IsNullOrEmpty(text_completedate7.Text) ? (DateTime?)null : DateTime.Parse(text_completedate7.Text);
        //        DateTime? COMPLETE_DATE8 = string.IsNullOrEmpty(text_completedate8.Text) ? (DateTime?)null : DateTime.Parse(text_completedate8.Text);
        //        DateTime? COMPLETE_DATE9 = string.IsNullOrEmpty(text_completedate9.Text) ? (DateTime?)null : DateTime.Parse(text_completedate9.Text);
        //        DateTime? COMPLETE_DATE10 = string.IsNullOrEmpty(text_completedate10.Text) ? (DateTime?)null : DateTime.Parse(text_completedate10.Text);
        //        string BUDGET1 = text_budgets1.Text;
        //        string BUDGET2 = text_budgets2.Text;
        //        string BUDGET3 = text_budgets3.Text;
        //        string BUDGET4 = text_budgets4.Text;
        //        string BUDGET5 = text_budgets5.Text;
        //        string BUDGET6 = text_budgets6.Text;
        //        string BUDGET7 = text_budgets7.Text;
        //        string BUDGET8 = text_budgets8.Text;
        //        string BUDGET9 = text_budgets9.Text;
        //        string BUDGET10 = text_budgets10.Text;
        //        DateTime startdate = new DateTime(1900, 01, 01);


        //        if (PERMINTAAN_PERBAIKAN_1 != "")
        //        {
        //            trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_1;
        //            trform5repairpermintaan.NO_FORM = NO_FORM;
        //            trform5repairpermintaan.PIC = PIC_PERBAIKAN_1;
        //            trform5repairpermintaan.COMPLETE_DATE = COMPLETE_DATE1;
        //            trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET1);
        //            TrForm5RepairPermintaan.UpdateProject(trform5repairpermintaan);
        //        }


        //        if (PERMINTAAN_PERBAIKAN_2 != "")
        //        {
        //            trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_2;
        //            trform5repairpermintaan.NO_FORM = NO_FORM;
        //            trform5repairpermintaan.PIC = PIC_PERBAIKAN_2;
        //            trform5repairpermintaan.COMPLETE_DATE = COMPLETE_DATE2;
        //            trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET2);
        //            TrForm5RepairPermintaan.UpdateProject(trform5repairpermintaan);
        //        }

        //        if (PERMINTAAN_PERBAIKAN_3 != "")
        //        {
        //            trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_3;
        //            trform5repairpermintaan.NO_FORM = NO_FORM;
        //            trform5repairpermintaan.PIC = PIC_PERBAIKAN_3;
        //            trform5repairpermintaan.COMPLETE_DATE = COMPLETE_DATE3;
        //            trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET3);
        //            TrForm5RepairPermintaan.UpdateProject(trform5repairpermintaan);
        //        }

        //        if (PERMINTAAN_PERBAIKAN_4 != "")
        //        {
        //            trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_4;
        //            trform5repairpermintaan.NO_FORM = NO_FORM;
        //            trform5repairpermintaan.PIC = PIC_PERBAIKAN_4;
        //            trform5repairpermintaan.COMPLETE_DATE = COMPLETE_DATE4;
        //            trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET4);
        //            TrForm5RepairPermintaan.UpdateProject(trform5repairpermintaan);
        //        }

        //        if (PERMINTAAN_PERBAIKAN_5 != "")
        //        {
        //            trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_5;
        //            trform5repairpermintaan.NO_FORM = NO_FORM;
        //            trform5repairpermintaan.PIC = PIC_PERBAIKAN_5;
        //            trform5repairpermintaan.COMPLETE_DATE = COMPLETE_DATE5;
        //            trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET5);
        //            TrForm5RepairPermintaan.UpdateProject(trform5repairpermintaan);
        //        }

        //        if (PERMINTAAN_PERBAIKAN_6 != "")
        //        {
        //            trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_6;
        //            trform5repairpermintaan.NO_FORM = NO_FORM;
        //            trform5repairpermintaan.PIC = PIC_PERBAIKAN_6;
        //            trform5repairpermintaan.COMPLETE_DATE = COMPLETE_DATE6;
        //            trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET6);
        //            TrForm5RepairPermintaan.UpdateProject(trform5repairpermintaan);
        //        }

        //        if (PERMINTAAN_PERBAIKAN_7 != "")
        //        {
        //            trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_7;
        //            trform5repairpermintaan.NO_FORM = NO_FORM;
        //            trform5repairpermintaan.PIC = PIC_PERBAIKAN_7;
        //            trform5repairpermintaan.COMPLETE_DATE = COMPLETE_DATE7;
        //            trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET7);
        //            TrForm5RepairPermintaan.UpdateProject(trform5repairpermintaan);
        //        }

        //        if (PERMINTAAN_PERBAIKAN_8 != "")
        //        {
        //            trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_8;
        //            trform5repairpermintaan.NO_FORM = NO_FORM;
        //            trform5repairpermintaan.PIC = PIC_PERBAIKAN_8;
        //            trform5repairpermintaan.COMPLETE_DATE = COMPLETE_DATE8;
        //            trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET8);
        //            TrForm5RepairPermintaan.UpdateProject(trform5repairpermintaan);
        //        }

        //        if (PERMINTAAN_PERBAIKAN_9 != "")
        //        {
        //            trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_9;
        //            trform5repairpermintaan.NO_FORM = NO_FORM;
        //            trform5repairpermintaan.PIC = PIC_PERBAIKAN_9;
        //            trform5repairpermintaan.COMPLETE_DATE = COMPLETE_DATE9;
        //            trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET9);
        //            TrForm5RepairPermintaan.UpdateProject(trform5repairpermintaan);
        //        }

        //        if (PERMINTAAN_PERBAIKAN_10 != "")
        //        {
        //            trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_10;
        //            trform5repairpermintaan.NO_FORM = NO_FORM;
        //            trform5repairpermintaan.PIC = PIC_PERBAIKAN_10;
        //            trform5repairpermintaan.COMPLETE_DATE = COMPLETE_DATE10;
        //            trform5repairpermintaan.BUDGET = Convert.ToDecimal(BUDGET10);
        //            TrForm5RepairPermintaan.UpdateProject(trform5repairpermintaan);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        DivMessage.InnerText = Ex.Message;
        //        DivMessage.Attributes["class"] = "error";
        //        //DivMessage.Attributes["class"] = "success";
        //        DivMessage.Visible = true;
        //    }

        //}

        public void GroupUpdatePermintaanPerbaikanActualDate()
        {
            try
            {
                TR_FORM5_REPAIR_PERMINTAAN_DA TrForm5RepairPermintaan = new DataLayer.TR_FORM5_REPAIR_PERMINTAAN_DA();
                DataSet Ds = new DataSet();

                TR_FORM5_REPAIR_PERMINTAAN trform5repairpermintaan = new TR_FORM5_REPAIR_PERMINTAAN();

                //Group Save Detail Permintaan Perbaikan
                string NO_FORM = text_noform.Text;
                string PERMINTAAN_PERBAIKAN_1 = text_permintaanperbaikan1.Text;
                string PERMINTAAN_PERBAIKAN_2 = text_permintaanperbaikan2.Text;
                string PERMINTAAN_PERBAIKAN_3 = text_permintaanperbaikan3.Text;
                string PERMINTAAN_PERBAIKAN_4 = text_permintaanperbaikan4.Text;
                string PERMINTAAN_PERBAIKAN_5 = text_permintaanperbaikan5.Text;
                string PERMINTAAN_PERBAIKAN_6 = text_permintaanperbaikan6.Text;
                string PERMINTAAN_PERBAIKAN_7 = text_permintaanperbaikan7.Text;
                string PERMINTAAN_PERBAIKAN_8 = text_permintaanperbaikan8.Text;
                string PERMINTAAN_PERBAIKAN_9 = text_permintaanperbaikan9.Text;
                string PERMINTAAN_PERBAIKAN_10 = text_permintaanperbaikan10.Text;
                string PERMINTAAN_PERBAIKAN_2_1 = text_descriptionrepair2_1.Text;
                string PERMINTAAN_PERBAIKAN_2_2 = text_descriptionrepair2_2.Text;
                string PERMINTAAN_PERBAIKAN_2_3 = text_descriptionrepair2_3.Text;
                string PERMINTAAN_PERBAIKAN_2_4 = text_descriptionrepair2_4.Text;
                string PERMINTAAN_PERBAIKAN_2_5 = text_descriptionrepair2_5.Text;
                string PERMINTAAN_PERBAIKAN_2_6 = text_descriptionrepair2_6.Text;
                string PERMINTAAN_PERBAIKAN_2_7 = text_descriptionrepair2_7.Text;
                string PERMINTAAN_PERBAIKAN_2_8 = text_descriptionrepair2_8.Text;
                string PERMINTAAN_PERBAIKAN_2_9 = text_descriptionrepair2_9.Text;
                string PERMINTAAN_PERBAIKAN_2_10 = text_descriptionrepair2_10.Text;
                string ACTUAL_FINISH_DATE1 = "";
                if (text_actualfinishdate1.Text != "")
                {
                    ACTUAL_FINISH_DATE1 = DateTime.Parse(text_actualfinishdate1.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    ACTUAL_FINISH_DATE1 = "1900-01-01";
                }
                string ACTUAL_FINISH_DATE2 = "";
                if (text_actualfinishdate2.Text != "")
                {
                    ACTUAL_FINISH_DATE2 = DateTime.Parse(text_actualfinishdate2.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    ACTUAL_FINISH_DATE2 = "1900-01-01";
                }

                string ACTUAL_FINISH_DATE3 = "";
                if (text_actualfinishdate3.Text != "")
                {
                    ACTUAL_FINISH_DATE3 = DateTime.Parse(text_actualfinishdate3.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    ACTUAL_FINISH_DATE3 = "1900-01-01";
                }


                string ACTUAL_FINISH_DATE4 = "";
                if (text_actualfinishdate4.Text != "")
                {
                    ACTUAL_FINISH_DATE4 = DateTime.Parse(text_actualfinishdate4.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    ACTUAL_FINISH_DATE4 = "1900-01-01";
                }

                string ACTUAL_FINISH_DATE5 = "";
                if (text_actualfinishdate5.Text != "")
                {
                    ACTUAL_FINISH_DATE5 = DateTime.Parse(text_actualfinishdate5.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    ACTUAL_FINISH_DATE5 = "1900-01-01";
                }

                string ACTUAL_FINISH_DATE6 = "";
                if (text_actualfinishdate6.Text != "")
                {
                    ACTUAL_FINISH_DATE6 = DateTime.Parse(text_actualfinishdate6.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    ACTUAL_FINISH_DATE6 = "1900-01-01";
                }

                string ACTUAL_FINISH_DATE7 = "";
                if (text_actualfinishdate7.Text != "")
                {
                    ACTUAL_FINISH_DATE7 = DateTime.Parse(text_actualfinishdate7.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    ACTUAL_FINISH_DATE7 = "1900-01-01";
                }

                string ACTUAL_FINISH_DATE8 = "";
                if (text_actualfinishdate8.Text != "")
                {
                    ACTUAL_FINISH_DATE8 = DateTime.Parse(text_actualfinishdate8.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    ACTUAL_FINISH_DATE8 = "1900-01-01";
                }

                string ACTUAL_FINISH_DATE9 = "";
                if (text_actualfinishdate9.Text != "")
                {
                    ACTUAL_FINISH_DATE9 = DateTime.Parse(text_actualfinishdate9.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    ACTUAL_FINISH_DATE9 = "1900-01-01";
                }

                string ACTUAL_FINISH_DATE10 = "";
                if (text_actualfinishdate10.Text != "")
                {
                    ACTUAL_FINISH_DATE10 = DateTime.Parse(text_actualfinishdate10.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    ACTUAL_FINISH_DATE10 = "1900-01-01";
                }

                DateTime startdate = new DateTime(1900, 01, 01);


                if (PERMINTAAN_PERBAIKAN_2_1 != "" && ddlpicperbaikan1.Text == HfUsername.Value)
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_1;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_1;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = Convert.ToDateTime(ACTUAL_FINISH_DATE1);
                    trform5repairpermintaan.MODIFIED_BY = HfUsername.Value;
                    trform5repairpermintaan.MODIFIED_ON = DateTime.Now;
                    TrForm5RepairPermintaan.UpdateActualFinishDate(trform5repairpermintaan);
                }


                if (PERMINTAAN_PERBAIKAN_2_2 != "" && ddlpicperbaikan2.Text == HfUsername.Value)
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_2;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_2;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = Convert.ToDateTime(ACTUAL_FINISH_DATE2);
                    trform5repairpermintaan.MODIFIED_BY = HfUsername.Value;
                    trform5repairpermintaan.MODIFIED_ON = DateTime.Now;
                    TrForm5RepairPermintaan.UpdateActualFinishDate(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_3 != "" && ddlpicperbaikan3.Text == HfUsername.Value)
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_3;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_3;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = Convert.ToDateTime(ACTUAL_FINISH_DATE3);
                    trform5repairpermintaan.MODIFIED_BY = HfUsername.Value;
                    trform5repairpermintaan.MODIFIED_ON = DateTime.Now;
                    TrForm5RepairPermintaan.UpdateActualFinishDate(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_4 != "" && ddlpicperbaikan4.Text == HfUsername.Value)
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_4;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_4;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = Convert.ToDateTime(ACTUAL_FINISH_DATE4);
                    trform5repairpermintaan.MODIFIED_BY = HfUsername.Value;
                    trform5repairpermintaan.MODIFIED_ON = DateTime.Now;
                    TrForm5RepairPermintaan.UpdateActualFinishDate(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_5 != "" && ddlpicperbaikan5.Text == HfUsername.Value)
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_5;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_5;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = Convert.ToDateTime(ACTUAL_FINISH_DATE5);
                    trform5repairpermintaan.MODIFIED_BY = HfUsername.Value;
                    trform5repairpermintaan.MODIFIED_ON = DateTime.Now;
                    TrForm5RepairPermintaan.UpdateActualFinishDate(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_6 != "" && ddlpicperbaikan6.Text == HfUsername.Value)
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_6;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_6;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = Convert.ToDateTime(ACTUAL_FINISH_DATE6);
                    trform5repairpermintaan.MODIFIED_BY = HfUsername.Value;
                    trform5repairpermintaan.MODIFIED_ON = DateTime.Now;
                    TrForm5RepairPermintaan.UpdateActualFinishDate(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_7 != "" && ddlpicperbaikan7.Text == HfUsername.Value)
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_7;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_7;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = Convert.ToDateTime(ACTUAL_FINISH_DATE7);
                    trform5repairpermintaan.MODIFIED_BY = HfUsername.Value;
                    trform5repairpermintaan.MODIFIED_ON = DateTime.Now;
                    TrForm5RepairPermintaan.UpdateActualFinishDate(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_8 != "" && ddlpicperbaikan8.Text == HfUsername.Value)
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_8;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_8;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = Convert.ToDateTime(ACTUAL_FINISH_DATE8);
                    trform5repairpermintaan.MODIFIED_BY = HfUsername.Value;
                    trform5repairpermintaan.MODIFIED_ON = DateTime.Now;
                    TrForm5RepairPermintaan.UpdateActualFinishDate(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_9 != "" && ddlpicperbaikan9.Text == HfUsername.Value)
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_9;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_9;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = Convert.ToDateTime(ACTUAL_FINISH_DATE9);
                    trform5repairpermintaan.MODIFIED_BY = HfUsername.Value;
                    trform5repairpermintaan.MODIFIED_ON = DateTime.Now;
                    TrForm5RepairPermintaan.UpdateActualFinishDate(trform5repairpermintaan);
                }

                if (PERMINTAAN_PERBAIKAN_2_10 != "" && ddlpicperbaikan10.Text == HfUsername.Value)
                {
                    trform5repairpermintaan.NO_FORM = NO_FORM;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN = PERMINTAAN_PERBAIKAN_10;
                    trform5repairpermintaan.PERMINTAAN_PERBAIKAN_2 = PERMINTAAN_PERBAIKAN_2_10;
                    trform5repairpermintaan.ACTUAL_FINISH_DATE = Convert.ToDateTime(ACTUAL_FINISH_DATE10);
                    trform5repairpermintaan.MODIFIED_BY = HfUsername.Value;
                    trform5repairpermintaan.MODIFIED_ON = DateTime.Now;
                    TrForm5RepairPermintaan.UpdateActualFinishDate(trform5repairpermintaan);
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

        public void GroupUpdatePermintaanPerbaikan()
        {
            try
            {

                TR_FORM5_REPAIR_PERMINTAAN_DA TrForm5RepairPermintaan = new DataLayer.TR_FORM5_REPAIR_PERMINTAAN_DA();
                DataSet Ds = new DataSet();

                TR_FORM5_REPAIR_PERMINTAAN trform5repairpermintaan = new TR_FORM5_REPAIR_PERMINTAAN();


                string noform = text_noform.Text;

                string Where = string.Format("NO_FORM = '{0}'", noform);
                TrForm5RepairPermintaan.DeleteFilter(Where);

                GroupSavePermintaanPerbaikan();

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void GroupUpdatePermintaanPerbaikanProject()
        {
            try
            {

                TR_FORM5_REPAIR_PERMINTAAN_DA TrForm5RepairPermintaan = new DataLayer.TR_FORM5_REPAIR_PERMINTAAN_DA();
                DataSet Ds = new DataSet();

                TR_FORM5_REPAIR_PERMINTAAN trform5repairpermintaan = new TR_FORM5_REPAIR_PERMINTAAN();


                string noform = text_noform.Text;

                string Where = string.Format("NO_FORM = '{0}'", noform);
                TrForm5RepairPermintaan.DeleteFilter(Where);

                GroupSavePermintaanPerbaikanProject();

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
        /// 

        public void SendEmailAllType()
        {
            try
            {
                DataSet DsForm = new DataSet();
                TR_FORM5_REPAIR_DA TrForm5DA = new TR_FORM5_REPAIR_DA();

                //Mendapatkan Status Form Berdasarkan No Form
                string NO_FORM = text_noform.Text;
                string Status = "";
                DsForm = TrForm5DA.GetDataByKey(NO_FORM);

                if (DsForm.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToString(DsForm.Tables[0].Rows[0]["STATUS"].ToString());

                    switch (Status)
                    {
                        case "Submit":
                            SendEmailOnApprovedToHD();
                            break;
                        case "Approved-Head-Department":
                            SendEmailApprovedHDToProject();
                            break;
                        case "Approved-Head-Department-Store-Design":
                            SendEmailApprovedHDToStoreDesign();
                            break;
                        case "Approved-Project":
                            SendEmailApprovedProjectToBudgetControl();
                            break;
                        case "Approved-Budget-Control":
                            SendEmailApprovedBudgetControlToCreativeManager();
                            break;
                        case "Submit-Over-Budget":
                            SendEmailOnApprovedToBrandManager();
                            break;
                        case "Approved-Brand-Manager-Budget":
                            SendEmailApprovedBrandManagerToComercialDirector();
                            break;
                        case "Approved-Comercial-Director-Budget":
                            SendEmailApprovedComercialDirectorToBudgetControl();
                            break;
                        case "On-Working":
                            SendEmailAcceptedCreativeManagerToAdminMaintenanceProject();
                            break;
                        case "Done":
                            SendEmailAcceptedCreativeManagerToDone();
                            SendEmailUserToDone();
                            break;
                        case "On-Revise":
                            SendEmailReviseToUser();
                            break;
                        //case "On-Revise-Budget-Control":
                        //    SendEmailReviseBudgetControlToUser();
                        //    break;
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

        public void SendEmailApprovedHDToProject()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("KD_JABATAN = 'PROJECT'");
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


                    string EmailTemplateEng = "Dear Project ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

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

        public void SendEmailApprovedHDToStoreDesign()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("KD_JABATAN = 'STORE-DESIGN'");
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


                    string EmailTemplateEng = "Dear Store Design ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

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

        public void SendEmailApprovedProjectToBudgetControl()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("KD_JABATAN = 'BC'");
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


                    string EmailTemplateEng = "Dear Budget Control ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

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

        public void SendEmailApprovedBudgetControlToCreativeManager()
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


                    string EmailTemplateEng = "Dear Creative Director,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan Repair berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

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

        //Send Email OverBudget

        public void SendEmailOnApprovedToBrandManager()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string KodeBrand = "";
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                BRAND_DA MsBrandDA = new BRAND_DA();
                DataSet DsBrand = new DataSet();

                string BRAND = text_brand.Text;
                string WhereBrand = string.Format("BRAND = '{0}'", BRAND);

                DsBrand = MsBrandDA.GetDataFilter(WhereBrand);
                if (DsBrand.Tables[0].Rows.Count > 0)
                {
                    KodeBrand = Convert.ToString(DsBrand.Tables[0].Rows[0]["KD_BRAND"].ToString());
                }

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("KD_BRAND LIKE '%{0}%' And KD_JABATAN = 'BM'", KodeBrand);
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
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Brand Manager ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

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

        public void SendEmailApprovedBrandManagerToComercialDirector()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("KD_JABATAN = 'CLD'");
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


                    string EmailTemplateEng = "Dear Comercial Director ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

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

        public void SendEmailApprovedComercialDirectorToBudgetControl()
        {
            try
            {
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("KD_JABATAN = 'BC'");
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


                    string EmailTemplateEng = "Dear Budget Control ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

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

        public void SendEmailAcceptedCreativeManagerToAdminMaintenanceProject()
        {
            try
            {
                int Id_Dept = 0;
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("KD_JABATAN = 'PROJECT'");
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


                    string EmailTemplateEng = "Dear Admin Maintenance/Project ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Berhasil diselesaikan <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

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

        public void SendEmailAcceptedAdminMaintenanceProjectToDone()
        {
            try
            {
                int Id_Dept = 0;
                string Dept = label_departmentvalue.Text;
                string Email = "";
                DateTime startdate = new DateTime(1900, 01, 01);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("KD_JABATAN = 'PROJECT'");
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


                    string EmailTemplateEng = "Dear Admin Maintenance / Project ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Berhasil diselesaikan <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

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

                TR_FORM5_REPAIR_DA TrForm5Repair = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("NO_FORM = '{0}' And DIBUAT = '{1}'", NO_FORM, DIBUAT);
                Ds = TrForm5Repair.GetDataFilter(Where);

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
                    string DITERIMA_4 = "";
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

                    if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_4"].ToString())))
                    {
                        DITERIMA_4 = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_4"].ToString());
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

                    //Mendapatkan Urutan Adm.Maintenance Berdasarkan Jabatan
                    DsSectionHeadDesign = MsUserDA.GetDataUrutanUser(DITERIMA_1, KODE_FORM);
                    if (DsSectionHeadDesign.Tables[0].Rows.Count > 0)
                    {
                        Email = DsSectionHeadDesign.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsSectionHeadDesign.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsSectionHeadDesign.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Admin Maintenance,<br />" + DITERIMA_1 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Revisi oleh: " + HfUsername.Value + " <br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + DITERIMA_1))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);



                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + DITERIMA_1))));
                        MailHelper.SendMail(Email, "Email Revise-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                    }

                    //Mendapatkan Urutan Budget Control Berdasarkan Jabatan
                    DsGraphicDesign = MsUserDA.GetDataUrutanUser(DITERIMA_2, KODE_FORM);
                    if (DsGraphicDesign.Tables[0].Rows.Count > 0)
                    {
                        Email = DsGraphicDesign.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsGraphicDesign.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsGraphicDesign.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Budget Control,<br />" + DITERIMA_2 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Revisi oleh: " + HfUsername.Value + " <br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

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

        public void SendEmailReviseBudgetControlToUser()
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
                    //string MENYETUJUI_1 = "";
                    //string DITERIMA_1 = "";
                    //string DITERIMA_2 = "";
                    //string DITERIMA_3 = "";
                    //if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["MENYETUJUI1"].ToString())))
                    //{
                    //    MENYETUJUI_1 = Convert.ToString(Ds.Tables[0].Rows[0]["MENYETUJUI1"].ToString());
                    //}

                    //if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_1"].ToString())))
                    //{
                    //    DITERIMA_1 = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_1"].ToString());
                    //}
                    //if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_2"].ToString())))
                    //{
                    //    DITERIMA_2 = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_2"].ToString());
                    //}

                    //if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_3"].ToString())))
                    //{
                    //    DITERIMA_3 = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_3"].ToString());
                    //}

                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();
                    //DataSet DsHeadDepartment = new DataSet();
                    //DataSet DsSectionHeadDesign = new DataSet();
                    //DataSet DsGraphicDesign = new DataSet();
                    //DataSet DsCreativeManager = new DataSet();

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

                        string EmailTemplateEng = "Dear Requester,<br />" + USERNAME + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Revisi Budget Control oleh: " + HfUsername.Value + "<br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + USERNAME))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);

                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + USERNAME))));
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
                    string DITERIMA_4 = "";
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

                    if (!String.IsNullOrEmpty(Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_4"].ToString())))
                    {
                        DITERIMA_4 = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_4"].ToString());
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

                        string EmailTemplateEng = "Dear Requester,<br />" + USERNAME + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Cancel oleh: " + HfUsername.Value + "<br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

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

                        string EmailTemplateEng = "Dear Head Department,<br />" + MENYETUJUI_1 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Cancel oleh: " + HfUsername.Value + "<br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + MENYETUJUI_1))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);



                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + MENYETUJUI_1))));
                        MailHelper.SendMail(Email, "Email Cancel-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                    }

                    //Mendapatkan Urutan Adm.Maintenance Berdasarkan Jabatan
                    DsSectionHeadDesign = MsUserDA.GetDataUrutanUser(DITERIMA_1, KODE_FORM);
                    if (DsSectionHeadDesign.Tables[0].Rows.Count > 0)
                    {
                        Email = DsSectionHeadDesign.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsSectionHeadDesign.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsSectionHeadDesign.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Admin Maintenance,<br />" + DITERIMA_1 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Cancel oleh: " + HfUsername.Value + " <br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + DITERIMA_1))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);



                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + DITERIMA_1))));
                        MailHelper.SendMail(Email, "Email Cancel-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);
                    }

                    //Mendapatkan Urutan Budget Control Berdasarkan Jabatan
                    DsGraphicDesign = MsUserDA.GetDataUrutanUser(DITERIMA_2, KODE_FORM);
                    if (DsGraphicDesign.Tables[0].Rows.Count > 0)
                    {
                        Email = DsGraphicDesign.Tables[0].Rows[0]["EMAIL"].ToString();
                        string KD_JABATAN = Convert.ToString(DsGraphicDesign.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                        URUTAN = Convert.ToString(DsGraphicDesign.Tables[0].Rows[0]["URUTAN"].ToString());

                        string EmailTemplateEng = "Dear Budget Control,<br />" + DITERIMA_2 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Cancel oleh: " + HfUsername.Value + " <br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

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

                        string EmailTemplateEng = "Dear Creative Director,<br />" + DITERIMA_3 + "<br /><br />Terkait atas pengajuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Dilakukan Cancel oleh: " + HfUsername.Value + " <br/>dengan Alasan " + REVISI + "Mohon konfirmasinya dengan melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

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

        //Send Email PIC All In Form
        public void SendEmailPICRepairForm()
        {
            try
            {
                TR_FORM5_REPAIR_PERMINTAAN_DA TrForm5RepairPermintaan = new DataLayer.TR_FORM5_REPAIR_PERMINTAAN_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = "";
                string NO_PERMINTAAN = "";
                string PERMINTAAN_PERBAIKAN = "";
                string PERMINTAAN_PERBAIKAN_2 = "";
                string PIC = "";
                string UPLOAD_FILE = "";

                text_budgets1.Text = "0";
                text_budgets2.Text = "0";
                text_budgets3.Text = "0";
                text_budgets4.Text = "0";
                text_budgets5.Text = "0";
                text_budgets6.Text = "0";
                text_budgets7.Text = "0";
                text_budgets8.Text = "0";
                text_budgets9.Text = "0";
                text_budgets10.Text = "0";

                string PIC_PERBAIKAN_1 = ddlpicperbaikan1.Text;
                string PIC_PERBAIKAN_2 = ddlpicperbaikan2.Text;
                string PIC_PERBAIKAN_3 = ddlpicperbaikan3.Text;
                string PIC_PERBAIKAN_4 = ddlpicperbaikan4.Text;
                string PIC_PERBAIKAN_5 = ddlpicperbaikan5.Text;
                string PIC_PERBAIKAN_6 = ddlpicperbaikan6.Text;
                string PIC_PERBAIKAN_7 = ddlpicperbaikan7.Text;
                string PIC_PERBAIKAN_8 = ddlpicperbaikan8.Text;
                string PIC_PERBAIKAN_9 = ddlpicperbaikan9.Text;
                string PIC_PERBAIKAN_10 = ddlpicperbaikan10.Text;
                string Email = "";


                //string TGL_SELESAI = "";
                string Where = string.Format("NO_FORM = '{0}'", HfNO_FORM.Value);
                Ds = TrForm5RepairPermintaan.GetDataFilter(Where);

                int i = 0;
                int index = 0;
                foreach (DataRow Item in Ds.Tables[0].Rows)
                {
                    index = i;
                    i++;

                    if (!String.IsNullOrEmpty((Item.Field<String>("NO_PERMINTAAN"))))
                    {
                        NO_PERMINTAAN = Item.Field<String>("NO_PERMINTAAN");
                    }
                    else
                    {
                        NO_PERMINTAAN = "";
                    }


                    if (!String.IsNullOrEmpty((Item.Field<String>("PERMINTAAN_PERBAIKAN"))))
                    {
                        PERMINTAAN_PERBAIKAN = Item.Field<String>("PERMINTAAN_PERBAIKAN");
                    }
                    else
                    {
                        PERMINTAAN_PERBAIKAN = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("PERMINTAAN_PERBAIKAN_2"))))
                    {
                        PERMINTAAN_PERBAIKAN_2 = Item.Field<String>("PERMINTAAN_PERBAIKAN_2");
                    }
                    else
                    {
                        PERMINTAAN_PERBAIKAN_2 = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("PIC"))))
                    {
                        PIC = Item.Field<String>("PIC");
                    }
                    else
                    {
                        PIC = "Choose";
                    }

                    DateTime? COMPLETE_DATE = Item.Field<DateTime?>("COMPLETE_DATE");
                    if (COMPLETE_DATE == null)
                    {
                        COMPLETE_DATE = new DateTime(1900, 01, 01);
                    }

                    DateTime? ACTUAL_FINISH_DATE = Item.Field<DateTime?>("ACTUAL_FINISH_DATE");
                    if (ACTUAL_FINISH_DATE == null)
                    {
                        ACTUAL_FINISH_DATE = new DateTime(1900, 01, 01);
                    }

                    decimal? BUDGET = Item.Field<decimal?>("BUDGET");
                    if (BUDGET.HasValue)
                    {
                        BUDGET = Item.Field<decimal>("BUDGET");
                    }
                    else
                    {
                        BUDGET = 0;

                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("UPLOAD_FILE"))))
                    {
                        UPLOAD_FILE = Item.Field<String>("UPLOAD_FILE");
                    }
                    else
                    {
                        UPLOAD_FILE = "";
                    }

                    MS_USER_DA MsUserDA = new MS_USER_DA();
                    DataSet DsUser = new DataSet();

                    // PIC PERBAIKAN 1
                    string WhereUser = string.Format("USERNAME = '{0}' AND USERNAME <> 'Choose'", PIC);
                    DsUser = MsUserDA.GetDataFilter(WhereUser);
                    if (DsUser.Tables[0].Rows.Count > 0)
                    {
                        Email = DsUser.Tables[0].Rows[0]["EMAIL"].ToString();

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


                        string EmailTemplateEng = "Dear PIC,<br />" + PIC + "<br /><br />Dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + PIC + "<br/>Request Date: " + TGL_REQUEST + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Telah Berhasil diselesaikan <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

                        string TempGuid = Guid.NewGuid().ToString();
                        string Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                                        + (TempGuid + ("&uid=" + PIC))));
                        string MailMessage = string.Format(EmailTemplateEng, Url);



                        Url = string.Format(EmailSetting.EmailResetPath, ("Account/Login.aspx?st="
                        + (TempGuid + ("&uid=" + PIC))));
                        MailHelper.SendMail(Email, "Email PIC-" + NO_FORM, MailMessage, "", EmailSetting.EmailResetSenderName, EmailSetting.EmailResetSMTP, EmailSetting.EmailResetSMTPPort, 1000000, EmailSetting.EmailResetSenderEmail, EmailSetting.EmailResetSenderPassword);

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

        #region gridview

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

        public void ViewOnlyState()
        {
            Pnl_Forms.Enabled = false;
            Pnl_PermintaanPerbaikan.Enabled = false;
        }


    }
}