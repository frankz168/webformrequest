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

            HfUsername.Value = Session["Username"].ToString();
            HfID_DEPT.Value = Session["ID_DEPT"].ToString();
            HfKDBRAND.Value = Session["KD_BRAND"].ToString();

            CheckUrutan();
        }

        protected void btn_RequestForm_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btn_ReportStatus_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/ReportStatusForm.aspx");
        }

        #region All Function

        public void CheckUrutan()
        {

            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value);

                int URUTAN = 0;
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                    if (URUTAN == 1)
                    {
                        btn_RequestForm.Visible = true;

                    }
                    else
                    {
                        btn_RequestForm.Visible = false;

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