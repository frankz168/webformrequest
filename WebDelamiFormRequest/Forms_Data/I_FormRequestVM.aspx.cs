using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDelamiFormRequest.Forms_Data
{
    public partial class I_FormRequestVM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
            }

            text_tanggalrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
            text_alokasibudget.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        #region gridview
        protected void gvMainPageChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvMainRowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        #endregion

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("L_FormRequestVM.aspx");
        }

        public void GetTopNoForm()
        {
            try
            {


            }

            catch (Exception ex)
            {

            }

        }

        //protected void btn_KelengkapanPendukung_Click(object sender, EventArgs e)
        //{
        //    ModalKelengkapanPendukung.Show();
        //}
    }
}