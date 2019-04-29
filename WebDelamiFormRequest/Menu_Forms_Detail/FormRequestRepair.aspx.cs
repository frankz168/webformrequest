using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDelamiFormRequest.DataLayer;
using GemBox.Spreadsheet;
using System.IO;

namespace WebDelamiFormRequest.Menu_Forms_Detail
{
    public partial class FormRequestRepair : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;


            if (Session["Username"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            HfID_DEPT.Value = Session["ID_DEPT"].ToString();

            gvMain.DataSourceID = "C_Grid";
            gvMain.DataBind();

            HfUsername.Value = Session["Username"].ToString();
            LoadDataUser();
        }

        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlbrand_SelectedIndexChanged(object sender, EventArgs e)
        {

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

                         string KD_BRAND = ddlbrand.SelectedValue.ToString();

                         if (KD_BRAND != "0")
                         {

                             if (HfKodeForm.Value == "FRM-0005")
                             {
                                 Session["KODE_DEPT"] = ddldepartment.SelectedValue;
                                 Session["KD_BRAND"] = ddlbrand.SelectedValue;
                                 Session["USERNAME1"] = ddlheaddepartment.SelectedValue;

                                 Response.Redirect("~/Forms_Data_Process/L_FormRequest_Repair.aspx?KODE_FORM=" + HfKodeForm.Value);

                             }
                             else
                             {
                                 DivMessage.InnerText = "Nama Brand Tidak Boleh Kosong";
                                 DivMessage.Attributes["class"] = "error";
                                 //DivMessage.Attributes["class"] = "success";
                                 DivMessage.Visible = true;
                             }
                         }
                         else
                         {
                             DivMessage.InnerText = "Nama Brand Tidak Boleh Kosong";
                             DivMessage.Attributes["class"] = "error";
                             //DivMessage.Attributes["class"] = "success";
                             DivMessage.Visible = true;
                         }
                    }

                    if (e.CommandName == "ClickRowFlowChart")
                    {

                        if (KodeForm == "FRM-0005")
                        {

                            Response.Redirect("~/Template/Template_FlowChart-Repair.pdf");
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
            try
            {
                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();
                DataSet DsUser = new DataSet();
                DataSet DsUserHdp = new DataSet();
                DataSet DsUserHsd = new DataSet();
                DataSet DsUserCm = new DataSet();
                DataSet DsDept = new DataSet();
                MS_DEPT_DA MsDeptDA = new MS_DEPT_DA();

                string Where = string.Format("USERNAME = '{0}'", HfUsername.Value);
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    string ID_DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string DEPT = "";
                    DsDept = MsDeptDA.GetDataFilter(string.Format("KODE_DEPT = '{0}'", Convert.ToString(ID_DEPT)));
                    if (DsDept.Tables[0].Rows.Count > 0)
                    {
                        ddldepartment.SelectedValue = Convert.ToString(DsDept.Tables[0].Rows[0]["KODE_DEPT"].ToString());
                        DEPT = Convert.ToString(DsDept.Tables[0].Rows[0]["DEPT"].ToString());

                        //Head Department Sesuai Departemen
                        string WhereDeptHead = string.Format("ID_DEPT LIKE '%{0}%' And KD_JABATAN = 'HDP'", ID_DEPT);
                        DsUser = MsUserDA.GetDataFilter(WhereDeptHead);
                        if (DsUser.Tables[0].Rows.Count > 0)
                        {
                            ddlheaddepartment.SelectedValue = Convert.ToString(DsUser.Tables[0].Rows[0]["USERNAME"].ToString());
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

        #endregion

    }
}