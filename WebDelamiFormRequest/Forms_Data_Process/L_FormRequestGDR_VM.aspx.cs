using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDelamiFormRequest.DataLayer;

namespace WebDelamiFormRequest.Forms_Data_Process
{
    public partial class L_FormRequestGDR_VM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;


            if (Session["Username"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            LoadDataUser();

            HfUsername.Value = Session["Username"].ToString();
        }

        #region gridview

        protected void gvMainRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string NoForm = gvMain.DataKeys[rowIndex]["NO_FORM"].ToString();

                    if (e.CommandName == "ClickRow")
                    {
                        HfNO_FORM.Value = NoForm;


                        Response.Redirect("~/Forms_Data_Process/I_FormRequestGDR_VM.aspx?NO_FORM=" + HfNO_FORM.Value);
                    }

                    if (e.CommandName == "ActivityRow")
                    {
                        HfNO_FORM.Value = NoForm;


                        Response.Redirect("~/Reports/ReportActivityForm.aspx?NO_FORM=" + HfNO_FORM.Value);
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

        protected void gvMainPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
        }

        #endregion

        #region All Function

        public void LoadDataUser()
        {
            HfUsername1.Value = Session["USERNAME1"].ToString();
            //HfUsername2.Value = Session["USERNAME2"].ToString();
            HfKODE_DEPT.Value = Session["KODE_DEPT"].ToString();
            HfKD_BRAND.Value = Session["KD_BRAND"].ToString();

            label_headdepartment1value.Text = HfUsername1.Value;

            MS_USER_DA MsUserDA = new MS_USER_DA();
            MS_JABATAN_DA MsJabatanDA = new MS_JABATAN_DA();
            MS_DEPT_DA MsDeptDA = new MS_DEPT_DA();
            BRAND_DA BrandDA = new BRAND_DA();
            DataSet Ds = new DataSet();

            string WhereDept = string.Format("KODE_DEPT = '{0}'", HfKODE_DEPT.Value);
            Ds = MsDeptDA.GetDataFilter(WhereDept);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                label_departmentvalue.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                Session["ID_DEPT"] = Convert.ToString(Ds.Tables[0].Rows[0]["KODE_DEPT"].ToString());
                Session["DepartemenName"] = label_departmentvalue.Text;

            }

            string WhereBrand = string.Format("KD_BRAND = '{0}'", HfKD_BRAND.Value);
            Ds = BrandDA.GetDataFilter(WhereBrand);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                label_brandvalue.Text = Convert.ToString(Ds.Tables[0].Rows[0]["BRAND"].ToString());
                Session["KD_BRAND"] = Convert.ToString(Ds.Tables[0].Rows[0]["KD_BRAND"].ToString());
                Session["BrandName"] = label_brandvalue.Text;

            }

        }

        #endregion

        protected void btn_newgdr_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms_Data_Process/I_FormRequestGDR_VM.aspx");
        }


    }
}