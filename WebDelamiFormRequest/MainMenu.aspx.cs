using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDelamiFormRequest.DataLayer;

namespace WebDelamiFormRequest
{
    public partial class MainMenu : System.Web.UI.Page
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
            HfID_DEPT.Value = Session["ID_DEPT"].ToString();
            HfKDBRAND.Value = Session["KD_BRAND"].ToString();
            HfUserProfileId.Value = Session["UserProfileId"].ToString();

            CheckUrutan();
            if (HfID_DEPT.Value == "34")
            {
                LoadGridFormGdrIklan();
            }
            else if (HfID_DEPT.Value == "01,02,03,04,05")
            {
                if (HfUsername.Value != "BudgetControl" && HfUsername.Value != "Store.Design")
                {
                    HfUsername.Value = "adm.maintenance";
                    LoadGridFormRepair();
                }
                else
                {
                    LoadGridFormRepair();
                }
            }
            else if (HfID_DEPT.Value == "40")
            {
                LoadGridFormRepair();
            }
            else if (HfID_DEPT.Value == "38" || HfID_DEPT.Value == "39")
            {
                LoadGridFormRepairPIC();
            }
            else
            {
                LoadGridFormGdr();
            }

        }

        protected void btn_RequestForm_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Menu_Forms_Detail/Default.aspx");
        }

        protected void btn_ReportStatus_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms_Data_Reports/ReportStatusForm.aspx");
        }
        protected void btn_ReportGraphicDesign_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms_Data_Reports/ReportGraphicDesignForm.aspx");
        }

        protected void btn_ReportDigitalAds_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms_Data_Reports/ReportDigitalAdsForm.aspx");
        }

        protected void btn_ReportUserAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms_Data_Reports/ReportAllForm.aspx");
        }

        #region All Function

        public void CheckUrutan()
        {

            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, "ALL");

                int URUTAN = 0;
                string KD_JABATAN = "";
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                    KD_JABATAN = DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString();

                    if (URUTAN == 1 && HfID_DEPT.Value != "34" && HfID_DEPT.Value != "ALL")
                    {
                        //btn_RequestForm.Visible = true;

                    }
                    else if (KD_JABATAN == "GD")
                    {
                        //btn_RequestForm.Visible = false;
                        //btn_ReportGraphicDesign.Visible = true;
                    }
                    else if (URUTAN == 1 && HfID_DEPT.Value == "34")
                    {
                        //btn_RequestForm.Visible = false;
                        //btn_ReportDigitalAds.Visible = true;
                    }
                    else if (URUTAN != 1 && HfID_DEPT.Value == "34")
                    {
                        //btn_RequestForm.Visible = false;
                        //btn_ReportDigitalAds.Visible = true;
                    }
                    else if (URUTAN == 1 && HfID_DEPT.Value == "ALL")
                    {
                        //btn_RequestForm.Visible = false;
                        //btn_ReportStatus.Visible = false;
                        //btn_ReportGraphicDesign.Visible = false;
                        //btn_ReportDigitalAds.Visible = false;
                        //btn_ReportUserAll.Visible = true;

                    }
                    else
                    {
                        //btn_RequestForm.Visible = false;

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

        public void LoadGridFormGdr()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, "ALL");
                int URUTAN = 0;
                string KD_JABATAN = "";
                string PAGE_NAME = "";
                string SP = "";
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                    KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                    PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                    SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());

                    switch (KD_JABATAN)
                    {
                        case "USR":
                            gvMain.DataSourceID = "C_GridDashUser";
                            break;
                        case "HDP":
                            gvMain.DataSourceID = "C_GridDashHeadDepartment";
                            break;
                        case "BM":
                            gvMain.DataSourceID = "C_GridDashBrandManager";
                            break;
                        case "HDVM":
                            gvMain.DataSourceID = "C_GridDashHeadVM";
                            break;
                        case "PHOTO":
                            gvMain.DataSourceID = "C_GridDashPhotographer";
                            break;
                        case "DI":
                            gvMain.DataSourceID = "C_GridDashDI";
                            break;
                        case "ADM-CREATIVE":
                            gvMain.DataSourceID = "C_GridDashHeadDesigner";
                            break;
                        case "GD":
                            gvMain.DataSourceID = "C_GridDashGraphicDesigner";
                            break;
                        case "CM":
                            gvMain.DataSourceID = "C_GridDashCreativeManagerOthers";
                            break;
                        case "PDC":
                            gvMain.DataSourceID = "C_GridDashPDC";
                            break;
                        default:
                            Console.WriteLine("Daftar Report Status");
                            break;
                    }
                }

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

        public void LoadGridFormGdrIklan()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, "ALL");
                int URUTAN = 0;
                string KD_JABATAN = "";
                string PAGE_NAME = "";
                string SP = "";
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                    KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                    PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                    SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());

                    switch (KD_JABATAN)
                    {
                        case "USR":
                            gvMain.DataSourceID = "C_GridDashUserIklan";
                            break;
                        case "HDP":
                            gvMain.DataSourceID = "C_GridDashHeadDepartmentIklan";
                            break;
                        case "DM":
                            gvMain.DataSourceID = "C_GridDashDigitalMarketingIklan";
                            break;
                        default:
                            Console.WriteLine("Daftar Report Status");
                            break;
                    }
                }

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

        public void LoadGridFormRepair()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, "ALL");
                int URUTAN = 0;
                string KD_JABATAN = "";
                string PAGE_NAME = "";
                string SP = "";
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                    KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                    PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                    SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());

                    switch (KD_JABATAN)
                    {
                        case "USR":
                            gvMain.DataSourceID = "C_GridDashUserRepair";
                            break;
                        case "HDP":
                            gvMain.DataSourceID = "C_GridDashHeadDepartmentRepair";
                            break;
                        case "STORE-DESIGN":
                            gvMain.DataSourceID = "C_GridDashStoreDesignRepair";
                            break;
                        case "PROJECT":
                            gvMain.DataSourceID = "C_GridDashProjectRepair";
                            break;
                        case "BM":
                            gvMain.DataSourceID = "C_GridDashBrandManagerRepair";
                            break;
                        case "CLD":
                            gvMain.DataSourceID = "C_GridDashComercialDirectorRepair";
                            break;
                        case "BC":
                            gvMain.DataSourceID = "C_GridDashBudgetControlRepair";
                            break;
                        case "CM":
                            gvMain.DataSourceID = "C_GridDashCreativeManagerRepair";
                            break;
                        default:
                            Console.WriteLine("Daftar Report Status");
                            break;
                    }
                }

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

        public void LoadGridFormRepairPIC()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, "ALL");
                int URUTAN = 0;
                string KD_JABATAN = "";
                string PAGE_NAME = "";
                string SP = "";
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                    KD_JABATAN = Convert.ToString(DsUser.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                    PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                    SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());

                    switch (KD_JABATAN)
                    {
                        case "PROJECT":
                            gvMain.DataSourceID = "C_GridDashProjectRepairPIC";
                            break;

                        default:
                            Console.WriteLine("Daftar Report Status");
                            break;
                    }
                }

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

        public void LoadDataForm()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value, HfKODE_FORM.Value);
                int URUTAN = 0;
                string PAGE_NAME = "";
                string SP = "";
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());
                    PAGE_NAME = Convert.ToString(DsUser.Tables[0].Rows[0]["PAGE_NAME"].ToString());
                    SP = Convert.ToString(DsUser.Tables[0].Rows[0]["SP"].ToString());
                }
                HfPagesName.Value = PAGE_NAME;

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
        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
        }

        //protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName.ToLower() != "page")
        //        {
        //            GridViewRow grv = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
        //            int rowIndex = grv.RowIndex;
        //            string NoForm = gvMain.DataKeys[rowIndex]["NO_FORM"].ToString();
        //            string KodeForm = gvMain.DataKeys[rowIndex]["KODE_FORM"].ToString();
        //            if (e.CommandName == "ClickRow")
        //            {
        //                HfNO_FORM.Value = NoForm;
        //                HfKODE_FORM.Value = KodeForm;
        //                LoadDataForm();


        //                Response.Redirect("~/Forms_Data_Process/" + HfPagesName.Value + "?NO_FORM=" + HfNO_FORM.Value);
        //            }

        //            if (e.CommandName == "ActivityRow")
        //            {
        //                HfNO_FORM.Value = NoForm;
        //                HfKODE_FORM.Value = KodeForm;
        //                LoadDataForm();

        //                Response.Redirect("~/Forms_Data_Reports/ReportActivityForm.aspx?NO_FORM=" + HfNO_FORM.Value);
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
                    string NoForm = gvMain.GetRowValuesByKeyValue(id, "NO_FORM").ToString();
                    string KodeForm = gvMain.GetRowValuesByKeyValue(id, "KODE_FORM").ToString();
                    if (commandName == "ClickRow")
                    {
                        HfNO_FORM.Value = NoForm;
                        HfKODE_FORM.Value = KodeForm;
                        LoadDataForm();


                        Response.Redirect("~/Forms_Data_Process/" + HfPagesName.Value + "?NO_FORM=" + HfNO_FORM.Value);
                    }

                    if (commandName == "ActivityRow")
                    {
                        HfNO_FORM.Value = NoForm;
                        HfKODE_FORM.Value = KodeForm;
                        LoadDataForm();

                        Response.Redirect("~/Forms_Data_Reports/ReportActivityForm.aspx?NO_FORM=" + HfNO_FORM.Value);
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

        #endregion

    }
}