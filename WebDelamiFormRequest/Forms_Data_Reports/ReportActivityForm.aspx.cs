using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDelamiFormRequest.Forms_Data_Reports
{
    public partial class ReportActivityForm : System.Web.UI.Page
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

            string _NOFORM = Request.QueryString["NO_FORM"];

            if (_NOFORM != "")
            {
                HfNO_FORM.Value = _NOFORM;
                CT_Grid.DataBind();
            }

        }

        //protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvMain.PageIndex = e.NewPageIndex;
        //}
    }
}