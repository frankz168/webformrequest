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
    public partial class Master_Form : System.Web.UI.Page
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
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (HfCode.Value == "")
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
            Response.Redirect("~/Forms_Data_Master/Master_Form.aspx");
        }

        protected void btn_delete_Click(object sender, EventArgs e)
        {
            text_kodeform.Enabled = false;
            text_nmform.Enabled = false;
            text_formtype.Enabled = false;

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
            text_kodeform.Enabled = true;
            text_nmform.Enabled = true;
            text_formtype.Enabled = true;

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
                MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                DataSet Ds = new DataSet();

                Ds = msformda.GetDataByKey(HfCode.Value);
                if (Ds.Tables[0].Rows.Count > 0)
                {
  
                    text_kodeform.Text = Convert.ToString(Ds.Tables[0].Rows[0]["KODE_FORM"].ToString());
                    text_nmform.Text = Convert.ToString(Ds.Tables[0].Rows[0]["NM_FORM"].ToString());
                    text_formtype.Text = Convert.ToString(Ds.Tables[0].Rows[0]["FORM_TYPE"].ToString());
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
                MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                DataSet Ds = new DataSet();

                string KODE_FORM = text_kodeform.Text;
                string NM_FORM = text_nmform.Text;
                string FORM_TYPE = text_formtype.Text;

                if(KODE_FORM != "")
                {
                    MS_FORM msform = new MS_FORM();
                    msform.KODE_FORM = KODE_FORM;
                    msform.NM_FORM = NM_FORM;
                    msform.FORM_TYPE = FORM_TYPE;
                    msformda.Insert(msform);

                    DivMessage.InnerText = "Data Berhasil Disimpan";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../Forms_Data_Master/Master_Form.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                }
                else
                {
                    DivMessage.InnerText = "Kode Form Tidak Boleh Kosong";
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
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

        public void UpdateData()
        {
            try
            {
                MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                DataSet Ds = new DataSet();

                string KODE_FORM = text_kodeform.Text;
                string NM_FORM = text_nmform.Text;
                string FORM_TYPE = text_formtype.Text;

                if (KODE_FORM != "")
                {
                    MS_FORM msform = new MS_FORM();
                    msform.KODE_FORM = KODE_FORM;
                    msform.NM_FORM = NM_FORM;
                    msform.FORM_TYPE = FORM_TYPE;
                    msformda.Update(msform);

                    DivMessage.InnerText = "Data Berhasil Diupdate";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../Forms_Data_Master/Master_Form.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                }
                else
                {
                    DivMessage.InnerText = "Kode Form Tidak Boleh Kosong";
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;
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

        public void DeleteData()
        {
            try
            {
                MS_FORM_DA msformda = new DataLayer.MS_FORM_DA();
                DataSet Ds = new DataSet();


                string KODE_FORM = text_kodeform.Text;
                msformda.DeleteByKey(KODE_FORM);

                DivMessage.InnerText = "Data Berhasil Dihapus";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Forms_Data_Master/Master_Form.aspx";
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
                    string _CODE = gvMain.DataKeys[rowIndex]["KODE_FORM"].ToString();

                    if (e.CommandName == "ClickRow")
                    {
                        HfCode.Value = _CODE;

                        if (_CODE != "")
                        {
                            //Response.Redirect("~/Forms_Data_Master/Master_Form.aspx?CODE=" + HfCode.Value);
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