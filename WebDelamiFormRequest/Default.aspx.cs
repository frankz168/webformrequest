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
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            HfUsername.Value = Session["Username"].ToString();
            LoadDataUser();
        }

        #region gridview

        protected void gvMainPageChanging(object sender, GridViewPageEventArgs e)
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
                    string KodeForm = gvMain.DataKeys[rowIndex]["KODE_FORM"].ToString();

                    if (e.CommandName == "ClickRow")
                    {
                        HfKodeForm.Value = KodeForm;

                        if (HfKodeForm.Value == "FRM-0001")
                        {
                            Session["KODE_DEPT"] = ddldepartment.SelectedValue;
                            Session["KD_BRAND"] = ddlbrand.SelectedValue;
                            Session["USERNAME1"] = ddlheaddepartment.SelectedValue;
                            Session["USERNAME2"] = ddlheaddepartment2.SelectedValue;

                            Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx?KODE_FORM=" +  HfKodeForm.Value);
                        }

                        else if (HfKodeForm.Value == "FRM-0002")
                        {
                            Response.Redirect("~/Forms_Data/L_FormRequestVM.aspx");
                        }
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

        #region All Function

        public void LoadDataUser()
        {

            MS_USER_DA MsUserDA = new MS_USER_DA();
            DataSet Ds = new DataSet();
            DataSet DsDept = new DataSet();
            MS_DEPT_DA MsDeptDA = new MS_DEPT_DA();

            string Where = string.Format("USERNAME = '{0}'", HfUsername.Value);
            Ds = MsUserDA.GetDataFilter(Where);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                int ID_DEPT = Convert.ToInt32(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());

                DsDept = MsDeptDA.GetDataByKey(ID_DEPT);
                if (DsDept.Tables[0].Rows.Count > 0)
                {
                    ddldepartment.SelectedValue = Convert.ToString(DsDept.Tables[0].Rows[0]["KODE_DEPT"].ToString());
                }

                ddlbrand.Text = Convert.ToString(Ds.Tables[0].Rows[0]["KD_BRAND"].ToString());

            }

        }

        #endregion


    }
}