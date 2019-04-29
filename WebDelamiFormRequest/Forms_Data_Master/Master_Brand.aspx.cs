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
    public partial class Master_Brand : System.Web.UI.Page
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
            Response.Redirect("~/Forms_Data_Master/Master_Brand.aspx");
        }

        protected void btn_delete_Click(object sender, EventArgs e)
        {
            text_kodebrand.Enabled = false;
            text_brand.Enabled = false;

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
            text_kodebrand.Enabled = true;
            text_brand.Enabled = true;

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
                BRAND_DA msbrandda = new DataLayer.BRAND_DA();
                DataSet Ds = new DataSet();

                Ds = msbrandda.GetDataByKey(HfCode.Value);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    text_kodebrand.Text = Convert.ToString(Ds.Tables[0].Rows[0]["KD_BRAND"].ToString());
                    text_brand.Text = Convert.ToString(Ds.Tables[0].Rows[0]["BRAND"].ToString());
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
                BRAND_DA msbrandda = new DataLayer.BRAND_DA();
                DataSet Ds = new DataSet();

                string KD_BRAND = text_kodebrand.Text;
                string BRAND = text_brand.Text;

                if (KD_BRAND != "")
                {
                    MS_BRAND msbrand = new MS_BRAND();
                    msbrand.KD_BRAND = KD_BRAND;
                    msbrand.BRAND = BRAND;
                    msbrandda.Insert(msbrand);

                    DivMessage.InnerText = "Data Berhasil Disimpan";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../Forms_Data_Master/Master_Brand.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                }
                else
                {
                    DivMessage.InnerText = "Kode Brand Tidak Boleh Kosong";
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
                BRAND_DA msbrandda = new DataLayer.BRAND_DA();
                DataSet Ds = new DataSet();

                string KD_BRAND = text_kodebrand.Text;
                string BRAND = text_brand.Text;

                if (KD_BRAND != "")
                {
                    MS_BRAND msbrand = new MS_BRAND();
                    msbrand.KD_BRAND = KD_BRAND;
                    msbrand.BRAND = BRAND;
                    msbrandda.Update(msbrand);

                    DivMessage.InnerText = "Data Berhasil Diupdate";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../Forms_Data_Master/Master_Brand.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                }
                else
                {
                    DivMessage.InnerText = "Kode Brand Tidak Boleh Kosong";
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
                BRAND_DA msbrandda = new DataLayer.BRAND_DA();
                DataSet Ds = new DataSet();

                string KD_BRAND = text_kodebrand.Text;
                msbrandda.DeleteByKey(KD_BRAND);

                DivMessage.InnerText = "Data Berhasil Dihapus";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Forms_Data_Master/Master_Brand.aspx";
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
                    string _CODE = gvMain.DataKeys[rowIndex]["KD_BRAND"].ToString();

                    if (e.CommandName == "ClickRow")
                    {
                        HfCode.Value = _CODE;

                        if (_CODE != "")
                        {
                            //Response.Redirect("~/Forms_Data_Master/Master_Jabatan.aspx?CODE=" + HfCode.Value);
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