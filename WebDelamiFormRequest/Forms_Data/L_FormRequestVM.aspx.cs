using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDelamiFormRequest.Forms_Data
{
    public partial class L_FormRequestVM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;
        }

        protected void btn_newvm_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms_Data/I_FormRequestVM.aspx");
        }

        #region gridview

        protected void gvMainPageChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvMainRowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        #endregion

       
    }
}