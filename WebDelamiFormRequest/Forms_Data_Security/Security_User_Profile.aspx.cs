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
    public partial class Security_User_Profile : System.Web.UI.Page
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
            Response.Redirect("~/Forms_Data_Security/Security_User_Profile.aspx");
        }

        protected void btn_delete_Click(object sender, EventArgs e)
        {
            text_userprofileid.Enabled = false;
            text_description.Enabled = false;
            ddlactive.Enabled = false;

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
            text_userprofileid.Enabled = true;
            text_description.Enabled = true;
            ddlactive.Enabled = true;

            btn_yes.Visible = false;
            btn_no.Visible = false;

            btn_save.Enabled = true;
            btn_delete.Enabled = true;
        }

        #region All Functions

        public void LoadData()
        {
            try
            {
                MS_USER_PROFILE_DA msuserprofileda = new DataLayer.MS_USER_PROFILE_DA();
                DataSet Ds = new DataSet();

                Ds = msuserprofileda.GetDataByKey(HfCode.Value);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    text_userprofileid.Text = Convert.ToString(Ds.Tables[0].Rows[0]["UserProfileId"].ToString());
                    text_description.Text = Convert.ToString(Ds.Tables[0].Rows[0]["Description"].ToString());
                    ddlactive.Text = Convert.ToString(Ds.Tables[0].Rows[0]["Active"].ToString());
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
                MS_USER_PROFILE_DA msuserprofileda = new DataLayer.MS_USER_PROFILE_DA();
                DataSet Ds = new DataSet();

                string UserProfileId = text_userprofileid.Text;
                string Description = text_description.Text;
                string Active = ddlactive.SelectedValue.ToString();

                DateTime startdate = new DateTime(1900, 01, 01);

                if (UserProfileId != "")
                {
                    MS_USER_PROFILE msuserprofile = new MS_USER_PROFILE();
                    msuserprofile.UserProfileId = UserProfileId;
                    msuserprofile.Description = Description;
                    msuserprofile.Active = Active;
                    msuserprofile.CB = HfUsername.Value;
                    msuserprofile.CO = DateTime.Now;
                    msuserprofile.MB = "";
                    msuserprofile.MO = startdate;
                    msuserprofileda.Insert(msuserprofile);

                    DivMessage.InnerText = "Data Berhasil Disimpan";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../Forms_Data_Master/Security_User_Profile.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                }
                else
                {
                    DivMessage.InnerText = "User Profile Tidak Boleh Kosong";
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
                MS_USER_PROFILE_DA msuserprofileda = new DataLayer.MS_USER_PROFILE_DA();
                DataSet Ds = new DataSet();

                string UserProfileId = text_userprofileid.Text;
                string Description = text_description.Text;
                string Active = ddlactive.SelectedValue.ToString();

                if (UserProfileId != "")
                {
                    MS_USER_PROFILE msuserprofile = new MS_USER_PROFILE();
                    msuserprofile.UserProfileId = UserProfileId;
                    msuserprofile.Description = Description;
                    msuserprofile.Active = Active;
                    msuserprofile.MB = HfUsername.Value;
                    msuserprofile.MO = DateTime.Now;
                    msuserprofileda.Update(msuserprofile);

                    DivMessage.InnerText = "Data Berhasil Diupdate";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../Forms_Data_Master/Security_User_Profile.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                }
                else
                {
                    DivMessage.InnerText = "User Profile Tidak Boleh Kosong";
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
                MS_USER_PROFILE_DA msuserprofileda = new DataLayer.MS_USER_PROFILE_DA();
                DataSet Ds = new DataSet();


                string UserProfileId = text_userprofileid.Text;
                msuserprofileda.DeleteByKey(UserProfileId);

                DivMessage.InnerText = "Data Berhasil Dihapus";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Forms_Data_Master/Security_User_Profile.aspx";
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
                    string _CODE = gvMain.DataKeys[rowIndex]["UserProfileId"].ToString();

                    HfCode.Value = _CODE;

                    if (e.CommandName == "ClickRow")
                    {
   
                        if (_CODE != "")
                        {
                            //Response.Redirect("~/Forms_Data_Security/Security_User_Profile.aspx?CODE=" + HfCode.Value);
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

                    if (e.CommandName == "FunctionRow")
                    {
                        Response.Redirect("~/Forms_Data_Security/Security_User_Profile_To_Functions.aspx?UPID=" + HfCode.Value + "&PRID=ALL");
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