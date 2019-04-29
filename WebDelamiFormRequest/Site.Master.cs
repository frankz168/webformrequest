using DevExpress.Web.ASPxMenu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDelamiFormRequest.DataLayer;

namespace WebDelamiFormRequest
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string SN = Convert.ToString(Session["Username"]);
            if (SN == "")
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                string[] urlContent = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
                string url = urlContent[urlContent.Count() - 1].ToLower();

                string user = Session["Username"].ToString();
                string userfullname = Session["USERFULLNAME"].ToString();
                string kodedept = Session["ID_DEPT"].ToString();
                string dept = "";
                //string dept = Session["DEPT"].ToString();
                string kodejabatan = Session["KD_JABATAN"].ToString();
                string kodebrand = Session["KD_BRAND"].ToString();

                MS_JABATAN_DA MsJabatanDA = new MS_JABATAN_DA();
                MS_DEPT_DA MsDeptDA = new MS_DEPT_DA();
                BRAND_DA BrandDA = new BRAND_DA();
                DataSet Ds = new DataSet();

                string WhereDept = string.Format("KODE_DEPT = '{0}'", kodedept);
                Ds = MsDeptDA.GetDataFilter(WhereDept);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    dept = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());

                }
                else
                {
                    dept = Session["DEPT"].ToString();
                }


                string WhereJabatan = string.Format("KD_JABATAN = '{0}'", kodejabatan);
                Ds = MsJabatanDA.GetDataFilter(WhereJabatan);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    label_Jabatan.Text = "Jabatan: " + Convert.ToString(Ds.Tables[0].Rows[0]["JABATAN"].ToString());

                }

                string WhereBrand = string.Format("KD_BRAND = '{0}'", kodebrand);
                Ds = BrandDA.GetDataFilter(WhereBrand);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    label_brand.Text = "Brand: " + Convert.ToString(Ds.Tables[0].Rows[0]["BRAND"].ToString());

                }


                //label_Welcome.Text = "Welcome: " + user + "";
                label_Username.Text = user;
                label_UserFullName.Text = userfullname;
                label_Department.Text = "Departemen: " + dept;

                //PrepareApplicationWideMenuVisibility();

                if (!Page.IsPostBack)
                {
                    //func.addLog("Show Page: " + url, user);
                    //PrepareApplicationWideMenuVisibility();
                }
            }
        }

        public void PrepareApplicationWideMenuVisibility()
        {
            try
            {
                MS_USER_PROFILE_TO_FUNCTIONS_DA MsUserProfileToFunctionsDA = new MS_USER_PROFILE_TO_FUNCTIONS_DA();
                DataSet Ds = new DataSet();

                Ds = MsUserProfileToFunctionsDA.GetDataFilter(String.Format("[UserProfileId] = '{0}'", Session["UserProfileId"].ToString()));

                int i = 0;
                int index = 0;
                foreach (DataRow Item in Ds.Tables[0].Rows)
                {
                    index = i;
                    i++;


                    string FunctionId = "";
                    string Permission = "";
                    if (!String.IsNullOrEmpty((Item.Field<String>("FunctionId"))))
                    {
                        FunctionId = Item.Field<String>("FunctionId");
                    }
                    else
                    {
                        FunctionId = "";
                    }

                    if (!String.IsNullOrEmpty((Item.Field<String>("Permission"))))
                    {
                        Permission = Item.Field<String>("Permission");
                    }
                    else
                    {
                        Permission = "";
                    }

                    //ASPxMenu MyMenu = ((ASPxMenu)(Master.FindControl("C_Menu")));


                    //if (object.ReferenceEquals(C_Menu.Items.FindByName("").Text, null))
                    //{

                    //}
                    //else
                    //{

                    //}

                    //string a = C_Menu.Items.FindByName(FunctionId).Text;

                    //if (!String.IsNullOrEmpty(Convert.ToString(a)))
                    //{
                    //    C_Menu.Items.FindByName(FunctionId).Visible = ((Permission == EPermission.NoAccess) ? false : true);
                    //}

                }
            }
            catch (Exception Ex)
            {

            }

        }

        //protected void ASPxMenu1_Init(object sender, EventArgs e)
        //{
        //    PrepareApplicationWideMenuVisibility();
        //}

        protected void lbLogoutClick(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Account/Login.aspx");
        }

        protected void label_resetpassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/ResetPassword.aspx");
        }

        protected void C_Menu_Load(object sender, EventArgs e)
        {
            //PrepareApplicationWideMenuVisibility();
        }
    }
}