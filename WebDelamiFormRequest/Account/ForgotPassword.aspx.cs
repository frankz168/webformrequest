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
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            ResetPassword();
        }

        #region All Function

        public void ResetPassword()
        {
            try
            {
                string Username = "";
                string Email = "";
                DateTime startdate = new DateTime(1999, 01, 01);

                if ((text_Username.Text != ""))
                {
                    Username = text_Username.Text;
                }

                string EmailTemplateEng = "Hi,<br /><br /><br />Someone has requested a password reset. If you think you are not making such request, please ignore this email, otherwise click the link below to resume Password Reset procedure.<br /><br />{0}<br /><br /><br />Regards,<br /><b>Automatic Delami Sender</b>";
                string PasswordResetPath = "http://localhost:2961/{0}";
                string PasswordResetSenderName = "Delami Auto Email Sender";
                string PasswordResetSMTP = "smtp.gmail.com";
                string PasswordResetSMTPPort = "587";
                string PasswordResetSenderEmail = "franky.sutanto93@gmail.com";
                string PasswordResetSenderPassword = "olordjesus943971382";

                string ForgotPasswordToken = Guid.NewGuid().ToString();
                string Url = string.Format(PasswordResetPath, ("Account/ResetPassword.aspx?st="
                                + (ForgotPasswordToken + ("&uid=" + Username))));
                string MailMessage = string.Format(EmailTemplateEng, Url);

                MS_USER_DA MsUserDA = new MS_USER_DA();
                DataSet Ds = new DataSet();

                string Where = string.Format("USERNAME = '{0}'", Username);
                Ds = MsUserDA.GetDataFilter(Where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    Int64 ID = Convert.ToInt64(Ds.Tables[0].Rows[0]["ID"].ToString());
                    Int16 ID_DEPT = Convert.ToInt16(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    //Int16 ID_HANDLE = Convert.ToInt16(Ds.Tables[0].Rows[0]["ID_HANDLE"].ToString());
                    string USERNAME = Convert.ToString(Ds.Tables[0].Rows[0]["USERNAME"].ToString());
                    string PASSWORD = Convert.ToString(Ds.Tables[0].Rows[0]["PASSWORD"].ToString());
                    string DEPT = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    Email = Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    string CREATED_BY = Convert.ToString(Ds.Tables[0].Rows[0]["CREATED_BY"].ToString());
                    DateTime CREATED_DATE = Convert.ToDateTime(Ds.Tables[0].Rows[0]["CREATED_DATE"].ToString());
                    bool STATUS = Convert.ToBoolean(Ds.Tables[0].Rows[0]["Status"].ToString());
                    string KD_BRAND = Convert.ToString(Ds.Tables[0].Rows[0]["KD_BRAND"].ToString());
                    string KD_JABATAN = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());


                    MS_USER msuser = new MS_USER();
                    msuser.ID = ID;
                    msuser.ID_DEPT = ID_DEPT;
                    msuser.USERNAME = USERNAME;
                    msuser.PASSWORD = PASSWORD;
                    msuser.DEPT = DEPT;
                    msuser.EMAIL = Email;
                    msuser.CREATED_BY = CREATED_BY;
                    msuser.CREATED_DATE = CREATED_DATE;
                    msuser.STATUS = STATUS;
                    msuser.FORGOT_PASSWORD = "Yes";
                    msuser.FORGOT_PASSWORD_TOKEN = ForgotPasswordToken;
                    msuser.LAST_PASSWORD_CHANGE = startdate;
                    msuser.KD_BRAND = KD_BRAND;
                    msuser.KD_JABATAN = KD_JABATAN;
                    MsUserDA.Update(msuser);

                    Url = string.Format(PasswordResetPath, ("Account/ResetPassword.aspx?st="
                    + (ForgotPasswordToken + ("&uid=" + Username))));
                    MailHelper.SendMail(Email, "Password Reset", MailMessage, "", PasswordResetSenderName, PasswordResetSMTP, PasswordResetSMTPPort, 1000000, PasswordResetSenderEmail, PasswordResetSenderPassword);
                    Response.Redirect("Login.aspx");

                }

            }
            catch (Exception Ex)
            {

            }
        }

        #endregion
    }
}