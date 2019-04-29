using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDelamiFormRequest.DataLayer;
using WebDelamiFormRequest.Domain;

namespace WebDelamiFormRequest.Forms_Data_Master
{
    public partial class Master_Departemen : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string _CODE = Request.QueryString["CODE"];

            if (Page.IsPostBack)
                return;


            if (Session["Username"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            HfUsername.Value = Session["Username"].ToString();

        }

        protected void btn_new_Click(object sender, EventArgs e)
        {
            C_PnlInput.Visible = true;
            btn_delete.Enabled = false;

            gvMain.Visible = false;
            C_GridPanel.Visible = false;

            text_id.Text = "Auto";
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if(HfCode.Value == "")
            {
                SaveData();
            }
            else
            {
                UpdateData();
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms_Data_Master/Master_Departemen.aspx");
        }

        protected void btn_delete_Click(object sender, EventArgs e)
        {
            text_kodedept.Enabled = false;
            text_dept.Enabled = false;
            text_ket.Enabled = false;
            ddlstatus.Enabled = false;

            btn_yes.Visible = true;
            btn_no.Visible = true;

            btn_save.Enabled = false;
            btn_delete.Enabled = false;
        }

        protected void btn_yes_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        protected void btn_no_Click(object sender, EventArgs e)
        {
            text_kodedept.Enabled = true;
            text_dept.Enabled = true;
            text_ket.Enabled = true;
            ddlstatus.Enabled = true;

            btn_yes.Visible = false;
            btn_no.Visible = false;

            btn_save.Enabled = true;
            btn_delete.Enabled = true;
        }

        #region All Functions

        public void LoadData()
        {
            try
            {
                MS_DEPT_DA msdeptda = new DataLayer.MS_DEPT_DA();
                DataSet Ds = new DataSet();

                Ds = msdeptda.GetDataByKey(Convert.ToInt32(HfCode.Value));
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    text_id.Text = Convert.ToString(Ds.Tables[0].Rows[0]["ID"].ToString());
                    text_kodedept.Text = Convert.ToString(Ds.Tables[0].Rows[0]["KODE_DEPT"].ToString());
                    text_dept.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT"].ToString());
                    text_ket.Text = Convert.ToString(Ds.Tables[0].Rows[0]["KET"].ToString());
                    ddlstatus.Text = Convert.ToString(Ds.Tables[0].Rows[0]["STATUS"].ToString()).Trim();
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


        public void SaveData()
        {
            try
            {
                MS_DEPT_DA msdeptda = new DataLayer.MS_DEPT_DA();
                DataSet Ds = new DataSet();

                string KODE_DEPT = text_kodedept.Text;
                string DEPT = text_dept.Text;
                string KET = text_ket.Text;
                string STATUS = ddlstatus.Text;

                MS_DEPT msdept = new MS_DEPT();
                msdept.KODE_DEPT = KODE_DEPT;
                msdept.DEPT = DEPT;
                msdept.KET = KET;
                msdept.CREATED_BY = HfUsername.Value;
                msdept.CREATED_DATE = DateTime.Now;
                msdept.STATUS = STATUS;
                msdeptda.Insert(msdept);

                DivMessage.InnerText = "Data Berhasil Disimpan";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Forms_Data_Master/Master_Departemen.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");


            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }



        }

        public void UpdateData()
        {
            try
            {
                MS_DEPT_DA msdeptda = new DataLayer.MS_DEPT_DA();
                DataSet Ds = new DataSet();

                string ID = text_id.Text;
                string KODE_DEPT = text_kodedept.Text;
                string DEPT = text_dept.Text;
                string KET = text_ket.Text;
                string STATUS = ddlstatus.Text;

                MS_DEPT msdept = new MS_DEPT();
                msdept.ID = ID;
                msdept.KODE_DEPT = KODE_DEPT;
                msdept.DEPT = DEPT;
                msdept.KET = KET;
                msdept.CREATED_BY = HfUsername.Value;
                msdept.CREATED_DATE = DateTime.Now;
                msdept.STATUS = STATUS;
                msdeptda.Update(msdept);

                DivMessage.InnerText = "Data Berhasil Diupdate";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Forms_Data_Master/Master_Departemen.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");


            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }



        }

        public void DeleteData()
        {
            try
            {
                MS_DEPT_DA msdeptda = new DataLayer.MS_DEPT_DA();
                DataSet Ds = new DataSet();


                string ID = text_id.Text;
                msdeptda.DeleteByKey(Convert.ToInt16(ID));

                DivMessage.InnerText = "Data Berhasil Dihapus";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Forms_Data_Master/Master_Departemen.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
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

        #region gridview

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
        }

        protected void gvMainRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() != "page")
                {
                    GridViewRow grv = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    int rowIndex = grv.RowIndex;
                    string _CODE = gvMain.DataKeys[rowIndex]["ID"].ToString();

                    if (e.CommandName == "ClickRow")
                    {
                        HfCode.Value = _CODE;

                        if (_CODE!= "")
                        {
                            //Response.Redirect("~/Forms_Data_Master/Master_Departemen.aspx?CODE=" + HfCode.Value);
                            LoadData();
                            C_PnlInput.Visible = true;
                            C_GridPanel.Visible = false;
                            gvMain.Visible = false;
                            btn_save.Text = "Update";
                        }
                        else
                        {
                            DivMessage.InnerText = "Gagal Load Data";
                            DivMessage.Attributes["class"] = "error";
                            //DiIklanessage.Attributes["class"] = "success";
                            DivMessage.Visible = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                DivMessage.InnerText = ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DiIklanessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }

        }

        #endregion

     
    }
}