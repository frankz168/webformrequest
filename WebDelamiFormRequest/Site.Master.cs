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
                string dept = Session["DEPT"].ToString();
                string kodejabatan = Session["KD_JABATAN"].ToString();
                string kodebrand = Session["KD_BRAND"].ToString();

                MS_JABATAN_DA MsJabatanDA = new MS_JABATAN_DA();
                BRAND_DA BrandDA = new BRAND_DA();
                DataSet Ds = new DataSet();


                string WhereJabatan = string.Format("KD_JABATAN = '{0}'", kodejabatan);
                Ds = MsJabatanDA.GetDataFilter(WhereJabatan);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    label_Jabatan.Text =  "Jabatan: " + Convert.ToString(Ds.Tables[0].Rows[0]["JABATAN"].ToString());

                }

                string WhereBrand = string.Format("KD_BRAND = '{0}'", kodebrand);
                Ds = BrandDA.GetDataFilter(WhereBrand);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    label_brand.Text = "Brand: " + Convert.ToString(Ds.Tables[0].Rows[0]["BRAND"].ToString());

                }


                //label_Welcome.Text = "Welcome: " + user + "";
                label_Username.Text = user;
                label_Department.Text = "Departemen: " + dept;
                //lbShowroom.Text = Session["UShowroom"].ToString();

                //LOGIN_DA loginDA = new LOGIN_DA();
                //List<MS_MENU> listMenu = new List<MS_MENU>();

                //listMenu = loginDA.getMenu(" where USER_LEVEL = '" + Session["ULevel"].ToString() + "' and STATUS = 1 order by ID_MENU");

                //menu(listMenu.Where(item => item.ID_PARENT == 0).ToList(), listMenu.Where(item => item.ID_PARENT != 0).ToList());

                //if (listMenu.Where(itemu => itemu.MENU_PATH.ToString().Split('/').Last().ToLower() == url.ToLower()).ToList().Count == 0)
                //{
                //    Response.Write("<script language='javascript'>alert('Access Denied');history.go(-1);</script>");
                //}
                //string uLevel = Session["ULevel"].ToString();
                //string uStore = Session["UStore"].ToString();

                //lbUName.Text = user;
                //lbULevel.Text = uLevel;
                //lbUStore.Text = uStore;

                //if (Session["ULevel"].ToString() == "Sales")
                //{
                //    if (!(url.ToLower() == "member.aspx") && !(url.ToLower() == "lapsales.aspx") && !(url.ToLower() == "changepass.aspx") && !(url.ToLower() == "sales.aspx") && !(url.ToLower() == "default.aspx") && !(url.ToLower() == "warehouse.aspx"))
                //    {
                //        Response.Write("<script language='javascript'>alert('Access Denied');history.go(-1);</script>");
                //    }

                //    //NavigationMenu.Items.Remove(NavigationMenu.Items[0]);
                //    //NavigationMenu.Items.Remove(NavigationMenu.Items[16]);
                //    //NavigationMenu.Items.Remove(NavigationMenu.Items[15]);
                //    //NavigationMenu.Items.Remove(NavigationMenu.Items[14]);
                //    //NavigationMenu.Items.Remove(NavigationMenu.Items[13]);
                //    //NavigationMenu.Items.Remove(NavigationMenu.Items[12]);
                //    //NavigationMenu.Items.Remove(NavigationMenu.Items[10]);
                //    //NavigationMenu.Items.Remove(NavigationMenu.Items[9]);
                //    //NavigationMenu.Items.Remove(NavigationMenu.Items[8]);
                //    //NavigationMenu.Items.Remove(NavigationMenu.Items[7]);
                //    //NavigationMenu.Items.Remove(NavigationMenu.Items[6]);
                //    //NavigationMenu.Items.Remove(NavigationMenu.Items[5]);
                //    //NavigationMenu.Items.Remove(NavigationMenu.Items[3]);
                //    //NavigationMenu.Items[2].ChildItems.Remove(NavigationMenu.Items[2].ChildItems[0]);
                //    //NavigationMenu.Items.Remove(NavigationMenu.Items[1]);
                //}
                //else if (Session["ULevel"].ToString().ToLower() == "admin sales" || Session["ULevel"].ToString().ToLower() == "admin counter")
                //{
                //    if (!(url.ToLower() == "member.aspx") && !(url.ToLower() == "lapsales.aspx") && !(url.ToLower() == "changepass.aspx") && !(url.ToLower() == "sales.aspx") && !(url.ToLower() == "default.aspx") && !(url.ToLower() == "warehouse.aspx"))
                //    {
                //        Response.Write("<script language='javascript'>alert('Access Denied');history.go(-1);</script>");
                //    }
                //}
                //else if (Session["ULevel"].ToString() == "Store Manager")
                //{
                //    if (!(url.ToLower() == "lapsales.aspx") && !(url.ToLower() == "member.aspx") && !(url.ToLower() == "changepass.aspx") && !(url.ToLower() == "sales.aspx") && !(url.ToLower() == "warehouse.aspx") && !(url.ToLower() == "default.aspx"))
                //    {
                //        Response.Write("<script language='javascript'>alert('Access Denied');history.go(-1);</script>");
                //    }
                //}
                //else if (Session["ULevel"].ToString() == "Warehouse")
                //{
                //    if (!(url.ToLower() == "changepass.aspx") && !(url.ToLower() == "warehouse.aspx") && !(url.ToLower() == "default.aspx") && !(url.ToLower() == "goodreceive.aspx"))
                //    {
                //        Response.Write("<script language='javascript'>alert('Access Denied');history.go(-1);</script>");
                //    }
                //}
                //else if (Session["ULevel"].ToString() == "Buyer")
                //{
                //    if (!(url.ToLower() == "member.aspx") && !(url.ToLower() == "changepass.aspx") && !(url.ToLower() == "warehouse.aspx")
                //        && !(url.ToLower() == "purchaseorder.aspx") && !(url.ToLower() == "default.aspx") && !(url.ToLower() == "article.aspx")
                //        && !(url.ToLower() == "promo.aspx"))
                //    {
                //        Response.Write("<script language='javascript'>alert('Access Denied');history.go(-1);</script>");
                //    }
                //}
                //else if (Session["ULevel"].ToString() == "Inventory")
                //{
                //    if (!(url.ToLower() == "changepass.aspx") && !(url.ToLower() == "stockopname.aspx") && !(url.ToLower() == "warehouse.aspx") && !(url.ToLower() == "default.aspx"))
                //    {
                //        Response.Write("<script language='javascript'>alert('Access Denied');history.go(-1);</script>");
                //    }
                //}
                //else if (Session["ULevel"].ToString() == "Master Data")
                //{
                //    if (!(url.ToLower() == "article.aspx") && !(url.ToLower() == "default.aspx"))
                //    {
                //        Response.Write("<script language='javascript'>alert('Access Denied');history.go(-1);</script>");
                //    }
                //}
                //else if (Session["ULevel"].ToString().ToLower() == "amnesty")
                //{
                //    if (!(url.ToLower() == "rptstock.aspx") && !(url.ToLower() == "changepass.aspx") && !(url.ToLower() == "default.aspx") && !(url.ToLower() == "rptpenjualan.aspx"))
                //    {
                //        Response.Write("<script language='javascript'>alert('Access Denied');history.go(-1);</script>");
                //    }
                //}
                //else if (Session["ULevel"].ToString().ToLower() == "admin wholesale")
                //{
                //    if (!(url.ToLower() == "wholesale.aspx") && !(url.ToLower() == "warehouse.aspx") && !(url.ToLower() == "default.aspx"))
                //    {
                //        Response.Write("<script language='javascript'>alert('Access Denied');history.go(-1);</script>");
                //    }
                //}

                if (!Page.IsPostBack)
                {
                    //func.addLog("Show Page: " + url, user);
                }
            }
        }

        protected void lbLogoutClick(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Account/Login.aspx");
        }

        protected void label_resetpassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/ResetPassword.aspx");
        }
    }
}