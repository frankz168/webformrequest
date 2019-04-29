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
    public partial class HeaderMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PrepareApplicationWideMenuVisibility();
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

                    string a = C_Menu.Items.FindByName(FunctionId).Text;

                    if (!String.IsNullOrEmpty(Convert.ToString(a)))
                    {
                        C_Menu.Items.FindByName(FunctionId).Visible = ((Permission == EPermission.NoAccess) ? false : true);
                    }

                }
            }
            catch (Exception Ex)
            {

            }

        }


    }
}