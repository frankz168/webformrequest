using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDelamiFormRequest.DataLayer;
using WebDelamiFormRequest.Domain;

namespace WebDelamiFormRequest.Forms_Data_Security
{
    public partial class Security_User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _CODE = Request.QueryString["CODE"];

            if (Page.IsPostBack)
                return;


            if (Session["Username"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            HfUsername.Value = Session["Username"].ToString();

        }

        protected void btn_new_Click(object sender, EventArgs e)
        {
            C_PnlInput.Visible = true;
            btn_delete.Enabled = false;

            gvMain.Visible = false;
            C_GridPanel.Visible = false;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (HfCode.Value == "")
            {
                SaveData();
            }
            else
            {
                UpdateData();
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms_Data_Security/Security_User.aspx");
        }

        protected void btn_delete_Click(object sender, EventArgs e)
        {
            text_username.Enabled = false;
            text_fullname.Enabled = false;
            text_password.Enabled = false;
            ddlkodedepartemen.Enabled = false;
            ddlkodebrand.Enabled = false;
            ddlkodejabatan.Enabled = false;
            text_email.Enabled = false;
            ddluserprofileid.Enabled = false;
            ddlstatus.Enabled = false;

            btn_yes.Visible = true;
            btn_no.Visible = true;

            btn_save.Enabled = false;
            btn_delete.Enabled = false;
        }

        protected void btn_yes_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        protected void btn_no_Click(object sender, EventArgs e)
        {
            text_username.Enabled = true;
            text_fullname.Enabled = true;
            text_password.Enabled = true;
            ddlkodedepartemen.Enabled = true;
            ddlkodebrand.Enabled = true;
            ddlkodejabatan.Enabled = true;
            text_email.Enabled = true;
            ddluserprofileid.Enabled = true;
            ddlstatus.Enabled = true;

            btn_yes.Visible = false;
            btn_no.Visible = false;

            btn_save.Enabled = true;
            btn_delete.Enabled = true;
        }

        #region All Functions

        public void CreateIDAuto()
        {
            try
            {
                string where = string.Format("ID <> '' ORDER BY NO_FORM DESC");

                string ID = "";
                DataSet Ds = new DataSet();
                MS_USER_DA msuserda = new DataLayer.MS_USER_DA();

                Ds = msuserda.GetDataFilter(where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    decimal angkaakhir = Convert.ToDecimal(ID) + 1;
                    
                    HfID.Value = Convert.ToString(angkaakhir);
                }
                else
                {
                    ID = "1";
                    HfID.Value = ID;

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

        public void LoadData()
        {
            try
            {
                MS_USER_DA msuserda = new DataLayer.MS_USER_DA();
                DataSet Ds = new DataSet();

                Ds = msuserda.GetDataByUserKey(HfCode.Value);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    text_username.Text = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    text_fullname.Text = Convert.ToString(Ds.Tables[0].Rows[0]["FULL_NAME"].ToString());
                    text_password.Text = Convert.ToString(Ds.Tables[0].Rows[0]["PASSWORD"].ToString());
                    ddlkodedepartemen.Text = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    ddlkodebrand.Text = Convert.ToString(Ds.Tables[0].Rows[0]["KD_BRAND"].ToString());
                    ddlkodejabatan.Text = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                    text_email.Text = Convert.ToString(Ds.Tables[0].Rows[0]["EMAIL"].ToString());
                    ddluserprofileid.Text = Convert.ToString(Ds.Tables[0].Rows[0]["UserProfileId"].ToString());
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

        public void SaveData()
        {
            try
            {
                MS_USER_DA msuserda = new DataLayer.MS_USER_DA();
                DataSet Ds = new DataSet();

                string USERNAME = text_username.Text;
                string FULL_NAME = text_fullname.Text;
                string PASSWORD = text_password.Text;
                string ID_DEPT = ddlkodedepartemen.SelectedValue.ToString();
                string DEPT = ddlkodedepartemen.Text;
                string KD_BRAND = ddlkodebrand.SelectedValue.ToString();
                string KD_JABATAN = ddlkodejabatan.SelectedValue.ToString();
                string EMAIL = text_email.Text;
                string UserProfileId = ddluserprofileid.Text;
                string STATUS = ddlstatus.SelectedValue.ToString();
                DateTime startdate = new DateTime(1900, 01, 01);


                if (USERNAME != "")
                {
                    CreateIDAuto();

                    MS_USER msuser = new MS_USER();
                    //msuser.ID = Convert.ToInt16(HfID.Value);
                    msuser.ID_DEPT = ID_DEPT;
                    msuser.USERNAME = USERNAME;
                    msuser.PASSWORD = PASSWORD;
                    msuser.DEPT = DEPT;
                    msuser.EMAIL = EMAIL;
                    msuser.CREATED_BY = HfUsername.Value;
                    msuser.CREATED_DATE = DateTime.Now;
                    msuser.STATUS = true;
                    msuser.FORGOT_PASSWORD = "";
                    msuser.FORGOT_PASSWORD_TOKEN = "";
                    msuser.LAST_PASSWORD_CHANGE = startdate;
                    msuser.KD_BRAND = KD_BRAND;
                    msuser.KD_JABATAN = KD_JABATAN;
                    msuser.FULL_NAME = FULL_NAME;
                    msuser.UserProfileId = UserProfileId;
                    msuserda.Insert(msuser);

                    DivMessage.InnerText = "Data Berhasil Disimpan";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../Forms_Data_Security/Security_User.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                }
                else
                {
                    DivMessage.InnerText = "Username Tidak Boleh Kosong";
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

        public void UpdateData()
        {
            try
            {
                MS_USER_DA msuserda = new DataLayer.MS_USER_DA();
                DataSet Ds = new DataSet();

                string USERNAME = text_username.Text;
                string FULL_NAME = text_fullname.Text;
                string PASSWORD = text_password.Text;
                string ID_DEPT = ddlkodedepartemen.SelectedValue.ToString();
                string DEPT = ddlkodedepartemen.Text;
                string KD_BRAND = ddlkodebrand.SelectedValue.ToString();
                string KD_JABATAN = ddlkodejabatan.SelectedValue.ToString();
                string EMAIL = text_email.Text;
                string UserProfileId = ddluserprofileid.Text;
                string STATUS = ddlstatus.SelectedValue.ToString();
                DateTime startdate = new DateTime(1900, 01, 01);

                if (USERNAME != "")
                {
                    MS_USER msuser = new MS_USER();
                    //msuser.ID = Convert.ToInt16(HfID.Value);
                    msuser.ID_DEPT = ID_DEPT;
                    msuser.USERNAME = USERNAME;
                    msuser.PASSWORD = PASSWORD;
                    msuser.DEPT = DEPT;
                    msuser.EMAIL = EMAIL;
                    msuser.STATUS = true;
                    msuser.KD_BRAND = KD_BRAND;
                    msuser.KD_JABATAN = KD_JABATAN;
                    msuser.FULL_NAME = FULL_NAME;
                    msuser.UserProfileId = UserProfileId;
                    msuserda.Update(msuser);

                    DivMessage.InnerText = "Data Berhasil Diupdate";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../Forms_Data_Security/Security_User.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                }
                else
                {
                    DivMessage.InnerText = "Kode Jabatan Tidak Boleh Kosong";
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

        public void DeleteData()
        {
            try
            {
                MS_USER_DA msuserda = new DataLayer.MS_USER_DA();
                DataSet Ds = new DataSet();


                string USERNAME = text_username.Text;
                msuserda.DeleteByKey(USERNAME);

                DivMessage.InnerText = "Data Berhasil Dihapus";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Forms_Data_Security/Security_User.aspx";
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
        #endregion

        #region gridview

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
        }

        protected void gvMainRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string _CODE = gvMain.DataKeys[rowIndex]["USERNAME"].ToString();

                    if (e.CommandName == "ClickRow")
                    {
                        HfCode.Value = _CODE;

                        if (_CODE != "")
                        {
                            //Response.Redirect("~/Forms_Data_Master/Master_Jabatan.aspx?CODE=" + HfCode.Value);
                            LoadData();
                            C_PnlInput.Visible = true;
                            C_GridPanel.Visible = false;
                            gvMain.Visible = false;
                            btn_save.Text = "Update";
                        }
                        else
                        {
                            DivMessage.InnerText = "Gagal Load Data";
                            DivMessage.Attributes["class"] = "error";
                            //DiIklanessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                DivMessage.InnerText = ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DiIklanessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        #endregion
    }
}