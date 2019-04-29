using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDelamiFormRequest.DataLayer;
using WebDelamiFormRequest.Domain;

namespace WebDelamiFormRequest.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            RememberMeProcess();
            LoginProcess();
        }

        public void RememberMeProcess()
        {
            if (check_rememberme.Checked)
            {
                HttpCookie co = new HttpCookie(text_Username.Text, text_Password.Text);
                co.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(co);

                HttpCookie co1 = new HttpCookie("lastid", text_Username.Text);
                co1.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(co1);

                HttpCookie co2 = new HttpCookie("lastpass", text_Password.Text);
                co2.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(co2);
            }

            Session["user"] = text_Username.Text;
            Session["password"] = text_Password.Text;

        }

        public void LoginProcess()
        {
            LOGIN_DA loginDA = new LOGIN_DA();
            List<MS_USER> listUser = new List<MS_USER>();

            string username = text_Username.Text;
            string password = text_Password.Text;

            listUser = loginDA.getMsUserLogin(username, password);

            if (listUser.Count > 0)
            {
                MS_USER user = new MS_USER();
                user = listUser.First();

                string usertest = user.USERNAME;
                insertSession(user);

                string ID_DEPT = user.ID_DEPT;
                string KD_JABATAN = user.KD_JABATAN;

                if (user.ID_DEPT == "34")
                {
                    Response.Redirect("~/MainMenu.aspx");
                }
                else
                {
                    Response.Redirect("~/MainMenu.aspx");
                }
            }
            else
            {
                lbWarning.Text = "Username atau Password salah!";
                lbWarning.Visible = true;
                text_Username.Text = "";
                text_Password.Text = "";
            }
        }

        protected void insertSession(MS_USER user)
        {
            Session["Username"] = user.USERNAME;
            Session["USERFULLNAME"] = user.FULL_NAME;
            Session["Password"] = user.PASSWORD;
            Session["Dept"] = user.DEPT;
            Session["KD_JABATAN"] = user.KD_JABATAN;
            Session["KD_BRAND"] = user.KD_BRAND;
            Session["ID_DEPT"] = user.ID_DEPT;
            Session["UserProfileId"] = user.UserProfileId;
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx");
        }

    }
}