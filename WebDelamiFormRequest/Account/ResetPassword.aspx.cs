using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDelamiFormRequest.DataLayer;
using WebDelamiFormRequest.Domain;


namespace WebDelamiFormRequest.Account
{
    public partial class ResetPassword : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Page.IsPostBack)
            //    return;

            string _ID = Request.QueryString["uid"];
            string _TOKEN = Request.QueryString["st"];

            if ((string)Session["Username"] == null)
            {
                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("USERNAME = '{0}'", _ID);
                Ds = MsUserDA.GetDataFilter(Where);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Session["Username"] = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    text_Username.Text = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    Session["Dept"] = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Session["KD_BRAND"] = Convert.ToString(Ds.Tables[0].Rows[0]["KD_BRAND"].ToString());
                    Session["KD_JABATAN"] = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                }
            }
            else
            {
                text_Username.Text = Session["Username"].ToString();
            }
        }

        protected void btn_resetpassword_Click(object sender, EventArgs e)
        {
            ResetPasswordProcess();
        }

        #region All Function

        public void ResetPasswordProcess()
        {
            try
            {
                LOGIN_DA loginDA = new LOGIN_DA();

                string username = text_Username.Text;
                string password = text_Password.Text;

                if (password != "")
                {
                    label_errorpassword.Visible = false;

                    MS_USER user = new MS_USER();
                    user.USERNAME = username;
                    user.PASSWORD = password;
                    loginDA.changePass(user);
                    UpdateLastPasswordChange();
                    DivMessage.InnerText = "Password Berhasil Diganti";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../Default.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                }
                else
                {
                    label_errorpassword.Visible = true;
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

        public void UpdateLastPasswordChange()
        {
            try
            {
                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string username = text_Username.Text;
                string Where = string.Format("USERNAME = '{0}'", username);
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    Int16 ID_DEPT = Convert.ToInt16(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    //Int16 ID_HANDLE = Convert.ToInt16(Ds.Tables[0].Rows[0]["ID_HANDLE"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string PASSWORD = Convert.ToString(Ds.Tables[0].Rows[0]["PASSWORD"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    string EMAIL = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string CREATED_BY = Convert.ToString(Ds.Tables[0].Rows[0]["CREATED_BY"].ToString());
                    DateTime CREATED_DATE = Convert.ToDateTime(Ds.Tables[0].Rows[0]["CREATED_DATE"].ToString());
                    bool STATUS = Convert.ToBoolean(Ds.Tables[0].Rows[0]["Status"].ToString());
                    string FORGOT_PASSWORD_TOKEN = Convert.ToString(Ds.Tables[0].Rows[0]["FORGOT_PASSWORD_TOKEN"].ToString());
                    string KD_BRAND = Convert.ToString(Ds.Tables[0].Rows[0]["KD_BRAND"].ToString());
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());

                    MS_USER msuser = new MS_USER();
                    msuser.ID = ID;
                    msuser.ID_DEPT = ID_DEPT;
                    msuser.USERNAME = USERNAME;
                    msuser.PASSWORD = PASSWORD;
                    msuser.DEPT = DEPT;
                    msuser.EMAIL = EMAIL;
                    msuser.CREATED_BY = CREATED_BY;
                    msuser.CREATED_DATE = CREATED_DATE;
                    msuser.STATUS = STATUS;
                    msuser.FORGOT_PASSWORD = "Yes";
                    msuser.FORGOT_PASSWORD_TOKEN = FORGOT_PASSWORD_TOKEN;
                    msuser.LAST_PASSWORD_CHANGE = DateTime.Now;
                    msuser.KD_BRAND = KD_BRAND;
                    msuser.KD_JABATAN = KD_JABATAN;
                    MsUserDA.Update(msuser);

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
    }
}