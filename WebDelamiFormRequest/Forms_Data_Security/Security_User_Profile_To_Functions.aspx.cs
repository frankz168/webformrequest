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
    public partial class Security_User_Profile_To_Functions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _UPID = Request.QueryString["UPID"];
            string _PRID = Request.QueryString["PRID"];

            HfUpid.Value = _UPID;
            HfPrid.Value = _PRID;

            if (Page.IsPostBack)
                return;


            if (Session["Username"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            HfUsername.Value = Session["Username"].ToString();

        }


        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (HfCode.Value == "")
            {
                //SaveData();
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


        #region All Functions

        public void LoadData()
        {
            try
            {
                MS_USER_PROFILE_TO_FUNCTIONS_DA msuserprofiletofunctionsda = new DataLayer.MS_USER_PROFILE_TO_FUNCTIONS_DA();
                MS_FUNCTIONS_DA msfunctionsda = new DataLayer.MS_FUNCTIONS_DA();
                DataSet Ds = new DataSet();
                DataSet DsFunction = new DataSet();

                Ds = msuserprofiletofunctionsda.GetDataFilter(string.Format("UserProfileId = '{0}' And Id = '{1}'", HfUpid.Value, HfCode.Value));
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    text_userprofileid.Text = Convert.ToString(Ds.Tables[0].Rows[0]["UserProfileId"].ToString());
                    string FunctionId = Convert.ToString(Ds.Tables[0].Rows[0]["FunctionId"].ToString());
                    string description = "";
                    DsFunction = msfunctionsda.GetDataByKey(FunctionId);
                    if (DsFunction.Tables[0].Rows.Count > 0)
                    {
                        description = Convert.ToString(DsFunction.Tables[0].Rows[0]["Description"].ToString());
                    }
                    text_functionid.Text = FunctionId + "-" + description;
                    ddlpermission.Text = Convert.ToString(Ds.Tables[0].Rows[0]["Permission"].ToString());
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
                MS_USER_PROFILE_TO_FUNCTIONS_DA msuserprofiletofunctionsda = new DataLayer.MS_USER_PROFILE_TO_FUNCTIONS_DA();
                DataSet Ds = new DataSet();

                string Permission = ddlpermission.SelectedValue.ToString();


                MS_USER_PROFILE_TO_FUNCTIONS msuserprofiletofunction = new MS_USER_PROFILE_TO_FUNCTIONS();
                msuserprofiletofunction.Id = Convert.ToInt16(HfCode.Value);
                msuserprofiletofunction.Permission = Permission;
                msuserprofiletofunction.MB = HfUsername.Value;
                msuserprofiletofunction.MO = DateTime.Now;
                msuserprofiletofunctionsda.Update(msuserprofiletofunction);

                DivMessage.InnerText = "Data Berhasil Diupdate";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Forms_Data_Security/Security_User_Profile.aspx";
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
                    string _CODE = gvMain.DataKeys[rowIndex]["Id"].ToString();

                    HfCode.Value = _CODE;

                    if (e.CommandName == "ClickRow")
                    {

                        if (_CODE != "")
                        {
                            //Response.Redirect("~/Forms_Data_Security/Security_User_Profile_To_Functions.aspx?CODE=" + HfCode.Value);
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