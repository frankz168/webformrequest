using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDelamiFormRequest.DataLayer;
using WebDelamiFormRequest.Domain;

namespace WebDelamiFormRequest.Forms_Data_Process
{
    public partial class I_FormRequest_Repair_SD : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        DataTable dtToko = new DataTable();
        public string KODE_FORM = "FRM-0005";

        protected void Page_Load(object sender, EventArgs e)
        {
            string _NO_FORM = Request.QueryString["NO_FORM"];
            string _STATUS = Request.QueryString["STATUS"];

            if (Page.IsPostBack)
                return;

            if (Session["Username"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (_STATUS == EYesNo.No)
            {
                HfNO_FORM.Value = _NO_FORM;
                StartFirstTime();
            }
            else
            {
                HfNO_FORM.Value = _NO_FORM;
                HfUsername.Value = Session["Username"].ToString();
                LoadDataFormRequestRepairSD();
                if (label_statusvalue.Text == EApprovalStatus.ApprovedStoreDesign && HfUsername.Value == "Store.Design")
                {
                    Pnl_Created.Visible = true;
                    btn_Save.Visible = true;
                    btn_Save.Enabled = true;
                }
                else
                {
                    btn_Save.Enabled = false;
                    btn_UpdateSubmit.Enabled = false;
                }
                ChangeButtonColor();
            }

            HfID_DEPT.Value = Convert.ToString(Session["ID_DEPT"].ToString());
            HfUsername.Value = Session["Username"].ToString();

        }

        protected void ddljenis_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "move_up()", true);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                SaveFormRequestRepairSD();
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
                //UpdateApproveAll();
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
                //UpdateSubmitFormRequestGDR();
                //SendEmailAllType();
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
                //UpdateCancelAll();
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
                //UpdateReviseAll();
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadSD/")
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadSD/")
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadSD/")
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
                Response.TransmitFile(Server.MapPath("~/Uploaded/FileUploadSD/")
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

        #region All Functions

        // Action User
        public void StartFirstTime()
        {
            try
            {
                CreateNoFormRepairSD();
                string NO_FORM = text_noform.Text;
                label_departmentvalue.Text = Session["DepartemenName"].ToString();
                text_tanggalrequired.Text = Session["REQUIRED_DATE"].ToString();
                text_brand.Text = Session["BrandName"].ToString();
                text_dibuatsd.Text = Session["Username"].ToString();
                Pnl_Created.Visible = true;
                btn_UpdateSubmit.Visible = false;
                ddljenis.Text = Session["STORE_TYPE_REQUEST"].ToString();
                ddljenis.Enabled = false;
                LoadDataDetailCustCt();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void LoadDataFormRequestRepairSD()
        {
            try
            {
                LoadDataDetailCustCt();

                DataSet Ds = new DataSet();
                DataSet DsDept = new DataSet();
                DataSet DsBrand = new DataSet();
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();
                MS_JABATAN_DA MsJabatanDA = new MS_JABATAN_DA();
                MS_DEPT_DA MsDeptDA = new MS_DEPT_DA();
                BRAND_DA BrandDA = new BRAND_DA();
                TR_FORM5_REPAIR_STORE_DESIGN_DA TrForm5RepairSD = new DataLayer.TR_FORM5_REPAIR_STORE_DESIGN_DA();

                Ds = TrForm5RepairSD.GetDataFilter(string.Format("NO_FORM = '{0}'", HfNO_FORM.Value));

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    text_noform.Text = Convert.ToString(Ds.Tables[0].Rows[0]["NO_FORM_SD"].ToString());
                    string Status = Convert.ToString(Ds.Tables[0].Rows[0]["STATUS"].ToString());
                    label_statusvalue.Text = Convert.ToString(Ds.Tables[0].Rows[0]["STATUS"].ToString());
                    text_tanggalrequired.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["REQUIRED_DATE"].ToString()).ToString("yyyy-MM-dd");
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

                    //text_location.Text = Convert.ToString(Ds.Tables[0].Rows[0]["LOCATION"].ToString());
                    text_concept.Text = Convert.ToString(Ds.Tables[0].Rows[0]["CONCEPT"].ToString());
                    ddljenis.Text = Convert.ToString(Ds.Tables[0].Rows[0]["STORE_TYPE"].ToString());
                    ddljenis.Enabled = false;
                    text_sqm.Text = Convert.ToString(Ds.Tables[0].Rows[0]["SQM_1"].ToString());

                    //Lampiran File Ada 4
                    linkbtn_filename1.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN1"].ToString());
                    linkbtn_filename2.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN2"].ToString());
                    linkbtn_filename3.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN3"].ToString());
                    linkbtn_filename4.Text = Convert.ToString(Ds.Tables[0].Rows[0]["RFR_LAMPIRAN4"].ToString());

                    text_description.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DESCRIPTION"].ToString());

                    text_dibuatsd.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DIBUAT_SD"].ToString());
                    text_tanggaldibuatsd.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DIBUAT_SD"].ToString()).ToString("yyyy-MM-dd");

                    if (text_tanggaldibuatsd.Text == "1900-01-01")
                    {
                        text_tanggaldibuatsd.Text = "";
                    }


                    LoadDataDetailRepairStoreDesign();

                    //Mengambil Posisi CM. Karena CM Ada terdapat 3 action sekaligus.
                    string KodeJabatan = "";

                    Ds = MsUserDA.GetDataFilter(string.Format("USERNAME = '{0}'", HfUsername.Value));
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        KodeJabatan = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                    }

                    string ACTIONGENERAL = "Approved";


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

                        if (URUTAN != 0)
                        {
                            btn_Save.Visible = false;
                            btn_UpdateSubmit.Enabled = false;
                            btn_Reject.Enabled = false;
                            btn_Cancel.Enabled = false;
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

        public void CreateNoFormRepairSD()
        {
            try
            {
                string where = string.Format("NO_FORM_SD <> '' ORDER BY NO_FORM_SD DESC");

                string NO_FORM = "";
                DataSet Ds = new DataSet();
                TR_FORM5_REPAIR_STORE_DESIGN_DA TrForm5RepairSD = new DataLayer.TR_FORM5_REPAIR_STORE_DESIGN_DA();

                DataSet DsForm = new DataSet();

                Ds = TrForm5RepairSD.GetDataFilter(where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    string noform = Ds.Tables[0].Rows[0]["NO_FORM_SD"].ToString();
                    int noformangka = Convert.ToInt32(noform.Substring(noform.Length - 4));
                    decimal angkaakhir = Convert.ToDecimal(noformangka) + 1;
                    NO_FORM = HfNO_FORM.Value + "-" + "SD-" + angkaakhir.ToString("0000");
                    text_noform.Text = NO_FORM;
                    label_statusvalue.Text = "New Data";
                }
                else
                {
                    NO_FORM = HfNO_FORM.Value + "-" + "SD-" + "0001";
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

        public void SaveFormRequestRepairSD()
        {
            try
            {
                TR_FORM5_REPAIR_DA trform5repairsda = new DataLayer.TR_FORM5_REPAIR_DA();
                DataSet DsRepair = new DataSet();
                TR_FORM5_REPAIR_STORE_DESIGN_DA trform5repairsdda = new DataLayer.TR_FORM5_REPAIR_STORE_DESIGN_DA();
                DataSet Ds = new DataSet();

                TR_FORM5_REPAIR_STORE_DESIGN_DETAIL_DA trform5repairsddetailda = new DataLayer.TR_FORM5_REPAIR_STORE_DESIGN_DETAIL_DA();
                DataSet DsSDDetail = new DataSet();
               
                string NO_FORM_SD = text_noform.Text;

                Ds = trform5repairsdda.GetDataFilter(string.Format("NO_FORM_SD = '{0}'", NO_FORM_SD));
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    string WhereSd = string.Format("NO_FORM_SD = '{0}'", NO_FORM_SD);
                    trform5repairsdda.DeleteFilter(WhereSd);

                    string WhereSdDetail = string.Format("NO_FORM_SD = '{0}'", NO_FORM_SD);
                    trform5repairsddetailda.DeleteFilter(WhereSdDetail);

                    CreateNoFormRepairSD();
                }

                string KODE_FORM = "FRM-0005";
                string ID_DEPT = Convert.ToString(Session["KODE_DEPT"].ToString());
                string KD_BRAND = Session["KD_BRAND"].ToString();
                string LOCATION = "-";
                string CONCEPT = text_concept.Text;
                string STORE_TYPE_REQUEST = ddljenis.Text;
                string SQM = text_sqm.Text;
                string RFR_LAMPIRAN1 = "";
                string RFR_LAMPIRAN2 = "";
                string RFR_LAMPIRAN3 = "";
                string RFR_LAMPIRAN4 = "";
                string DESCRIPTION = text_description.Text;
                string DIBUAT_SD = text_dibuatsd.Text;
                DateTime TGL_DIBUAT_SD = DateTime.Now;
                DateTime startdate = new DateTime(1900, 01, 01);
                DateTime temp;

                if (DateTime.TryParse(text_tanggalrequired.Text, out temp))
                {
                    DateTime TANGGAL_REQUIRED = Convert.ToDateTime(text_tanggalrequired.Text);
                    var todaysDate = DateTime.Today;
                    int result = DateTime.Compare(TANGGAL_REQUIRED, todaysDate);

                    if (result > 0)
                    {
                        DateTime? TGL_REQUIRED = Convert.ToDateTime(text_tanggalrequired.Text);

                        TR_FORM5_REPAIR_STORE_DESIGN TrForm5RepairSD = new TR_FORM5_REPAIR_STORE_DESIGN();
                        TrForm5RepairSD.NO_FORM_SD = NO_FORM_SD;
                        TrForm5RepairSD.NO_FORM = HfNO_FORM.Value;
                        TrForm5RepairSD.REQUIRED_DATE = TGL_REQUIRED;
                        TrForm5RepairSD.REQUESTER_NAME = "";
                        TrForm5RepairSD.ID_DEPT = ID_DEPT;
                        TrForm5RepairSD.KD_BRAND = KD_BRAND;
                        TrForm5RepairSD.LOCATION = LOCATION;
                        TrForm5RepairSD.CONCEPT = CONCEPT;
                        TrForm5RepairSD.STORE_TYPE = STORE_TYPE_REQUEST;
                        TrForm5RepairSD.SQM_1 = SQM;
                        TrForm5RepairSD.SQM_2 = SQM;

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
                  .SaveAs(Server.MapPath("~/Uploaded/FileUploadSD/") + RFR_LAMPIRAN1);
                                }
                            }
                        }

                        TrForm5RepairSD.RFR_LAMPIRAN1 = RFR_LAMPIRAN1;
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
                                        .SaveAs(Server.MapPath("~/Uploaded/FileUploadSD/") + RFR_LAMPIRAN2);
                                }

                            }
                        }
                        TrForm5RepairSD.RFR_LAMPIRAN2 = RFR_LAMPIRAN2;
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
                                        .SaveAs(Server.MapPath("~/Uploaded/FileUploadSD/") + RFR_LAMPIRAN3);
                                }
                            }
                        }
                        TrForm5RepairSD.RFR_LAMPIRAN3 = RFR_LAMPIRAN3;
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
                                        .SaveAs(Server.MapPath("~/Uploaded/FileUploadSD/") + RFR_LAMPIRAN4);
                                }
                            }

                        }
                        TrForm5RepairSD.RFR_LAMPIRAN4 = RFR_LAMPIRAN4;
                        TrForm5RepairSD.DESCRIPTION = DESCRIPTION;
                        TrForm5RepairSD.DIBUAT_SD = DIBUAT_SD;
                        TrForm5RepairSD.TGL_DIBUAT_SD = TGL_DIBUAT_SD;
                        TrForm5RepairSD.STATUS = EApprovalStatus.ApprovedStoreDesign;
                        TrForm5RepairSD.REVISI = "";

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

                        TrForm5RepairSD.USER_CURRENT = DIBUAT_SD;
                        TrForm5RepairSD.NEXT_USER = USERNEXT;
                        TrForm5RepairSD.URUTAN_USER_CURRENT = URUTAN;
                        TrForm5RepairSD.URUTAN_NEXT_USER = URUTAN_NEXT;
                        trform5repairsdda.Insert(TrForm5RepairSD);

                        TR_FORM5_REPAIR TrForm5Repair = new TR_FORM5_REPAIR();
                        TrForm5Repair.NO_FORM = HfNO_FORM.Value;
                        TrForm5Repair.STATUS = EApprovalStatus.ApprovedStoreDesign;
                        TrForm5Repair.USER_CURRENT = DIBUAT_SD;
                        TrForm5Repair.NEXT_USER = USERNEXT;
                        TrForm5Repair.URUTAN_USER_CURRENT = URUTAN;
                        TrForm5Repair.URUTAN_NEXT_USER = URUTAN_NEXT;
                        trform5repairsda.UpdateDibuatSD(TrForm5Repair);

                        SaveDetailRepairStoreDesign();
                        SendEmailAllType();

                        TR_FORM_GDR_ACTIVITY_DA trformgdrActivityDa = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                        DataSet DsFormActivity = new DataSet();

                        TR_FORM_GDR_ACTIVITY trformgdractivity = new TR_FORM_GDR_ACTIVITY();
                        trformgdractivity.USERNAME = HfUsername.Value;
                        trformgdractivity.ACTIVITY_TIME = DateTime.Now;
                        trformgdractivity.KODE_FORM = "FRM-0005";
                        trformgdractivity.NO_FORM = HfNO_FORM.Value;
                        trformgdractivity.STATUS = EApprovalStatus.ApprovedStoreDesign;
                        trformgdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.ApprovedStoreDesign;
                        trformgdractivity.REVISION = "-";
                        trformgdractivity.URUTAN = 99;
                        trformgdractivity.SP = "*";
                        trformgdractivity.USER_CURRENT = DIBUAT_SD;
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
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateSubmitFormRequestRepairSD()
        {
            try
            {

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
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

                string NO_FORM = HfNO_FORM.Value;
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
                string ACTIONGENERAL = "Approved";

                HfActionGeneral.Value = ACTIONGENERAL;

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUserCustom(HfUsername.Value, KODE_FORM, HfActionGeneral.Value);

                int URUTAN = 0;
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                    if (URUTAN == 99)
                    {

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
                string ACTIONGENERAL = "Approved";

                HfActionGeneral.Value = ACTIONGENERAL;

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUserCustom(HfUsername.Value, KODE_FORM, HfActionGeneral.Value);

                int URUTAN = 0;
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                    if (URUTAN == 99)
                    {

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
                string ACTIONGENERAL = "Approved";

                HfActionGeneral.Value = ACTIONGENERAL;

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUserCustom(HfUsername.Value, KODE_FORM, HfActionGeneral.Value);

                int URUTAN = 0;
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                    if (URUTAN == 2)
                    {

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

        /// <summary>
        /// Update Status Cancel Function
        /// </summary>
        public void UpdateStatusCancel()
        {
            try
            {
                TR_FORM5_REPAIR_STORE_DESIGN_DA TrForm5RepairSD = new DataLayer.TR_FORM5_REPAIR_STORE_DESIGN_DA();
                DataSet DsForm = new DataSet();

                string NO_FORM_SD = text_noform.Text;
                string DIBUAT_SD = text_dibuatsd.Text;
                DateTime TGL_DIBUAT_SD = DateTime.Now;

                TR_FORM5_REPAIR_STORE_DESIGN TrForm5RepairStoreDesign = new TR_FORM5_REPAIR_STORE_DESIGN();
                TrForm5RepairStoreDesign.NO_FORM_SD = NO_FORM_SD;
                TrForm5RepairStoreDesign.DIBUAT_SD = DIBUAT_SD;
                TrForm5RepairStoreDesign.TGL_DIBUAT_SD = TGL_DIBUAT_SD;
                TrForm5RepairStoreDesign.STATUS = EApprovalStatus.Cancel;
                //TrForm3gdr.REVISI = "";

                TrForm5RepairStoreDesign.USER_CURRENT = HfUsername.Value;
                TrForm5RepairStoreDesign.NEXT_USER = "-";
                TrForm5RepairStoreDesign.URUTAN_USER_CURRENT = 99;
                TrForm5RepairStoreDesign.URUTAN_NEXT_USER = 0;
                TrForm5RepairSD.UpdateDibuat(TrForm5RepairStoreDesign);


                TR_FORM_GDR_ACTIVITY_DA TrForm5GdrActivity = new DataLayer.TR_FORM_GDR_ACTIVITY_DA();
                DataSet DsFormActivity = new DataSet();

                TR_FORM_GDR_ACTIVITY TrForm5gdractivity = new TR_FORM_GDR_ACTIVITY();
                TrForm5gdractivity.USERNAME = HfUsername.Value;
                TrForm5gdractivity.ACTIVITY_TIME = DateTime.Now;
                TrForm5gdractivity.KODE_FORM = "FRM-0005";
                TrForm5gdractivity.NO_FORM = NO_FORM_SD;
                TrForm5gdractivity.STATUS = EApprovalStatus.Cancel;
                TrForm5gdractivity.DESCRIPTION = "Update Status To " + EApprovalStatus.Cancel;
                TrForm5gdractivity.REVISION = "-";
                TrForm5gdractivity.URUTAN = 99;
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


        public void ShowButtonUrutan1()
        {

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
        /// Add Data Detail TR_FORM3_GDR_KATEGORI
        /// </summary>
        /// 

        public void LoadDataDetailRepairStoreDesign()
        {
            try
            {
                TR_FORM5_REPAIR_STORE_DESIGN_DETAIL_DA TrForm5RepairStoreDesignDetail = new DataLayer.TR_FORM5_REPAIR_STORE_DESIGN_DETAIL_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string TYPE_OF_WORK = "";
                string PROJECT_REQUEST = "";
                string PROVIDE_INFORMATION = "";
                string Where = string.Format("NO_FORM_SD = '{0}'", NO_FORM);
                Ds = TrForm5RepairStoreDesignDetail.GetDataFilter(Where);

                int i = 0;
                int index = 0;
                foreach (DataRow Item in Ds.Tables[0].Rows)
                {
                    index = i;
                    i++;

                    if (!String.IsNullOrEmpty((Item.Field<String>("TYPE_OF_WORK"))))
                    {
                        TYPE_OF_WORK = Item.Field<String>("TYPE_OF_WORK");
                    }
                    else
                    {
                        TYPE_OF_WORK = "";
                    }


                    if (!String.IsNullOrEmpty((Item.Field<String>("PROJECT_REQUEST"))))
                    {
                        PROJECT_REQUEST = Item.Field<String>("PROJECT_REQUEST");
                    }
                    else
                    {
                        PROJECT_REQUEST = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("PROVIDE_INFORMATION"))))
                    {
                        PROVIDE_INFORMATION = Item.Field<String>("PROVIDE_INFORMATION");
                    }
                    else
                    {
                        PROVIDE_INFORMATION = "";
                    }

                    //Load Group Type Of Work
                    if (TYPE_OF_WORK == "New") checkbox_new.Checked = true;
                    if (TYPE_OF_WORK == "Relocation") checkbox_relocation.Checked = true;
                    if (TYPE_OF_WORK == "Re-Layout") checkbox_relayout.Checked = true;
                    if (TYPE_OF_WORK == "Renovation") checkbox_renovation.Checked = true;
                    if (TYPE_OF_WORK == "Prepektif") checkbox_prepektif.Checked = true;
                    if (TYPE_OF_WORK == "ME-Plan") checkbox_meplan.Checked = true;

                    //Load Group Type Project Request
                    if (PROJECT_REQUEST == "BQ") checkbox_bq.Checked = true;
                    if (PROJECT_REQUEST == "PH") checkbox_ph.Checked = true;
                    if (PROJECT_REQUEST == "SPK") checkbox_spk.Checked = true;
                    if (PROJECT_REQUEST == "Fitting-Out") checkbox_fittingout.Checked = true;
                    if (PROJECT_REQUEST == "Setting-Up") checkbox_settingup.Checked = true;
                    if (PROJECT_REQUEST == "Opening") checkbox_opening.Checked = true;

                    //Load Group Type Provide Information
                    if (PROVIDE_INFORMATION == "SIF") checkbox_sif.Checked = true;
                    if (PROVIDE_INFORMATION == "KeyPlan") checkbox_keyplan.Checked = true;
                    if (PROVIDE_INFORMATION == "BaseDrawing") checkbox_basedrawing.Checked = true;
                    if (PROVIDE_INFORMATION == "SiteMeasurement") checkbox_sitemeasurement.Checked = true;
                    if (PROVIDE_INFORMATION == "ActualPhoto") checkbox_actualphoto.Checked = true;
                    if (PROVIDE_INFORMATION == "NumberOfExistingStock") checkbox_numberofexistingstock.Checked = true;
                    if (PROVIDE_INFORMATION == "FitOutGuidance") checkbox_fitoutguidance.Checked = true;
                    if (PROVIDE_INFORMATION == "DeptStoreGuidance") checkbox_deptstoreguidance.Checked = true;
                    if (PROVIDE_INFORMATION == "ShopFrontGuidance") checkbox_shopfrontguidance.Checked = true;

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

        public void SaveDetailRepairStoreDesign()
        {
            try
            {
                GroupSaveDetailRepairStoreDesign();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateDetailRepairStoreDesign()
        {
            try
            {
                GroupUpdateRepairStoreDesign();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        public void GroupSaveDetailRepairStoreDesign()
        {
            try
            {
                TR_FORM5_REPAIR_STORE_DESIGN_DETAIL_DA TrForm5RepairStoreDesignDetail = new DataLayer.TR_FORM5_REPAIR_STORE_DESIGN_DETAIL_DA();
                DataSet Ds = new DataSet();

                TR_FORM5_REPAIR_STORE_DESIGN_DETAIL trform5repairstoredetail = new TR_FORM5_REPAIR_STORE_DESIGN_DETAIL();

                //Group Save Kategori
                string noformsd = text_noform.Text;

                //variable type of work
                string typeofwork_new = "New";
                string typeofwork_relocation = "Relocation";
                string typeofwork_relayout = "Re-Layout";
                string typeofwork_renovation = "Renovation";
                string typeofwork_prepektif = "Prepektif";
                string typeofwork_meplan = "ME-Plan";

                //variable Type Project Request
                string projectrequest_BQ = "BQ";
                string projectrequest_PH = "PH";
                string projectrequest_SPK = "SPK";
                string projectrequest_FittingOut = "Fitting-Out";
                string projectrequest_SettingUp = "Setting-Up";
                string projectrequest_Opening = "Opening";

                //variable Type Provide Information
                string provideinformation_SIF = "SIF";
                string provideinformation_keyplan = "KeyPlan";
                string provideinformation_basedrawing = "BaseDrawing";
                string provideinformation_sitemeasurement = "SiteMeasurement";
                string provideinformation_actualphoto = "ActualPhoto";
                string provideinformation_numberofexistingstock = "NumberOfExistingStock";
                string provideinformation_fitoutguidance = "FitOutGuidance";
                string provideinformation_deptstoreguidance = "DeptStoreGuidance";
                string provideinformation_shopfrontguidance = "ShopFrontGuidance";

                //checkbox variable type of work

                if (checkbox_new.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = typeofwork_new;
                    trform5repairstoredetail.PROJECT_REQUEST = "-";
                    trform5repairstoredetail.PROVIDE_INFORMATION = "-";
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_relocation.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = typeofwork_relocation;
                    trform5repairstoredetail.PROJECT_REQUEST = "-";
                    trform5repairstoredetail.PROVIDE_INFORMATION = "-";
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_relayout.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = typeofwork_relayout;
                    trform5repairstoredetail.PROJECT_REQUEST = "-";
                    trform5repairstoredetail.PROVIDE_INFORMATION = "-";
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_renovation.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = typeofwork_renovation;
                    trform5repairstoredetail.PROJECT_REQUEST = "-";
                    trform5repairstoredetail.PROVIDE_INFORMATION = "-";
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_prepektif.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = typeofwork_prepektif;
                    trform5repairstoredetail.PROJECT_REQUEST = "-";
                    trform5repairstoredetail.PROVIDE_INFORMATION = "-";
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_meplan.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = typeofwork_meplan;
                    trform5repairstoredetail.PROJECT_REQUEST = "-";
                    trform5repairstoredetail.PROVIDE_INFORMATION = "-";
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                //checkbox variable type of project request

                if (checkbox_bq.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = "-";
                    trform5repairstoredetail.PROJECT_REQUEST = projectrequest_BQ;
                    trform5repairstoredetail.PROVIDE_INFORMATION = "-";
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }


                if (checkbox_ph.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = "-";
                    trform5repairstoredetail.PROJECT_REQUEST = projectrequest_PH;
                    trform5repairstoredetail.PROVIDE_INFORMATION = "-";
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_spk.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = "-";
                    trform5repairstoredetail.PROJECT_REQUEST = projectrequest_SPK;
                    trform5repairstoredetail.PROVIDE_INFORMATION = "-";
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }


                if (checkbox_fittingout.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = "-";
                    trform5repairstoredetail.PROJECT_REQUEST = projectrequest_FittingOut;
                    trform5repairstoredetail.PROVIDE_INFORMATION = "-";
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_settingup.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = "-";
                    trform5repairstoredetail.PROJECT_REQUEST = projectrequest_SettingUp;
                    trform5repairstoredetail.PROVIDE_INFORMATION = "-";
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_opening.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = "-";
                    trform5repairstoredetail.PROJECT_REQUEST = projectrequest_Opening;
                    trform5repairstoredetail.PROVIDE_INFORMATION = "-";
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                //checkbox variable type of provide Information
                if (checkbox_sif.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = "-";
                    trform5repairstoredetail.PROJECT_REQUEST = "-";
                    trform5repairstoredetail.PROVIDE_INFORMATION = provideinformation_SIF;
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_keyplan.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = "-";
                    trform5repairstoredetail.PROJECT_REQUEST = "-";
                    trform5repairstoredetail.PROVIDE_INFORMATION = provideinformation_keyplan;
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_basedrawing.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = "-";
                    trform5repairstoredetail.PROJECT_REQUEST = "-";
                    trform5repairstoredetail.PROVIDE_INFORMATION = provideinformation_basedrawing;
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_sitemeasurement.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = "-";
                    trform5repairstoredetail.PROJECT_REQUEST = "-";
                    trform5repairstoredetail.PROVIDE_INFORMATION = provideinformation_sitemeasurement;
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_actualphoto.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = "-";
                    trform5repairstoredetail.PROJECT_REQUEST = "-";
                    trform5repairstoredetail.PROVIDE_INFORMATION = provideinformation_actualphoto;
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_numberofexistingstock.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = "-";
                    trform5repairstoredetail.PROJECT_REQUEST = "-";
                    trform5repairstoredetail.PROVIDE_INFORMATION = provideinformation_numberofexistingstock;
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_fitoutguidance.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = "-";
                    trform5repairstoredetail.PROJECT_REQUEST = "-";
                    trform5repairstoredetail.PROVIDE_INFORMATION = provideinformation_fitoutguidance;
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_deptstoreguidance.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = "-";
                    trform5repairstoredetail.PROJECT_REQUEST = "-";
                    trform5repairstoredetail.PROVIDE_INFORMATION = provideinformation_deptstoreguidance;
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
                }

                if (checkbox_shopfrontguidance.Checked == true)
                {
                    trform5repairstoredetail.NO_FORM_SD = noformsd;
                    trform5repairstoredetail.TYPE_OF_WORK = "-";
                    trform5repairstoredetail.PROJECT_REQUEST = "-";
                    trform5repairstoredetail.PROVIDE_INFORMATION = provideinformation_shopfrontguidance;
                    TrForm5RepairStoreDesignDetail.Insert(trform5repairstoredetail);
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

        public void GroupUpdateRepairStoreDesign()
        {
            try
            {

                TR_FORM5_REPAIR_STORE_DESIGN_DETAIL_DA TrForm5RepairStoreDesignDetail = new DataLayer.TR_FORM5_REPAIR_STORE_DESIGN_DETAIL_DA();
                DataSet Ds = new DataSet();

                TR_FORM5_REPAIR_STORE_DESIGN_DETAIL trform5repairstoredetail = new TR_FORM5_REPAIR_STORE_DESIGN_DETAIL();

                string noform = text_noform.Text;

                string Where = string.Format("NO_FORM_SD = '{0}'", noform);
                TrForm5RepairStoreDesignDetail.DeleteFilter(Where);

                GroupSaveDetailRepairStoreDesign();

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
                string NO_FORM = HfNO_FORM.Value;
                string Status = "";
                DsForm = TrForm5DA.GetDataByKey(NO_FORM);

                if (DsForm.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToString(DsForm.Tables[0].Rows[0]["STATUS"].ToString());

                    switch (Status)
                    {
                        case "Approved-Store-Design":
                            SendEmailApprovedStoreDesignToProject();
                            break;
                        //case "Approved-Head-Department":
                        //    //SendEmailApprovedHDToHeadDesign();
                        //    break;
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

        public void SendEmailApprovedStoreDesignToProject()
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
                    string DIBUAT_SD = text_dibuatsd.Text;
                    DateTime? TGL_REQUIRED = string.IsNullOrEmpty(text_tanggalrequired.Text) ? (DateTime?)null : DateTime.Parse(text_tanggalrequired.Text);


                    string EmailTemplateEng = "Dear Project ,<br />" + USERNAME + "<br /><br />Mohon konfirmasi untuk persetujuan dokumen berikut: <br />No Dokumen: " + NO_FORM + "<br />Jenis Form:  " + FORM_TYPE + "<br/>Brand: " + BRAND + "<br/>PIC: " + DIBUAT_SD + "<br/>Required Date: " + TGL_REQUIRED + "<br/>Untuk persetujuan dapat melakukan klik <br/> {0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";

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

        #endregion

        public void ViewOnlyState()
        {
            Pnl_Forms.Enabled = false;
            Pnl_Others1.Enabled = false;
        }

    }
}