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
    public partial class Master_Jabatan : System.Web.UI.Page
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
            Response.Redirect("~/Forms_Data_Master/Master_Jabatan.aspx");
        }

        protected void btn_delete_Click(object sender, EventArgs e)
        {
            text_kodejabatan.Enabled = false;
            text_jabatan.Enabled = false;

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
            text_kodejabatan.Enabled = true;
            text_jabatan.Enabled = true;

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
                MS_JABATAN_DA msjabatanda = new DataLayer.MS_JABATAN_DA();
                DataSet Ds = new DataSet();

                Ds = msjabatanda.GetDataByKey(HfCode.Value);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    text_kodejabatan.Text = Convert.ToString(Ds.Tables[0].Rows[0]["KD_JABATAN"].ToString());
                    text_jabatan.Text = Convert.ToString(Ds.Tables[0].Rows[0]["JABATAN"].ToString());
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
                MS_JABATAN_DA msjabatanda = new DataLayer.MS_JABATAN_DA();
                DataSet Ds = new DataSet();

                string KD_JABATAN = text_kodejabatan.Text;
                string JABATAN = text_jabatan.Text;

                if (KD_JABATAN != "")
                {
                    MS_JABATAN msjabatan = new MS_JABATAN();
                    msjabatan.KD_JABATAN = KD_JABATAN;
                    msjabatan.JABATAN = JABATAN;
                    msjabatanda.Insert(msjabatan);

                    DivMessage.InnerText = "Data Berhasil Disimpan";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../Forms_Data_Master/Master_Jabatan.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                }
                else
                {
                    DivMessage.InnerText = "Kode Jabatan Tidak Boleh Kosong";
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
                MS_JABATAN_DA msjabatanda = new DataLayer.MS_JABATAN_DA();
                DataSet Ds = new DataSet();

                string KD_JABATAN = text_kodejabatan.Text;
                string JABATAN = text_jabatan.Text;

                if (KD_JABATAN != "")
                {
                    MS_JABATAN msjabatan = new MS_JABATAN();
                    msjabatan.KD_JABATAN = KD_JABATAN;
                    msjabatan.JABATAN = JABATAN;
                    msjabatanda.Update(msjabatan);

                    DivMessage.InnerText = "Data Berhasil Diupdate";
                    DivMessage.Style.Add("color", "black");
                    DivMessage.Style.Add("background-color", "skyblue");
                    DivMessage.Attributes["class"] = "error";
                    //DivMessage.Attributes["class"] = "success";
                    DivMessage.Visible = true;

                    string HomePageUrl = "../Forms_Data_Master/Master_Jabatan.aspx";
                    Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                    //Response.Redirect("~/Forms_Data/L_FormRequestGDR_Others.aspx");
                }
                else
                {
                    DivMessage.InnerText = "Kode Jabatan Tidak Boleh Kosong";
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
                MS_JABATAN_DA msjabatanda = new DataLayer.MS_JABATAN_DA();
                DataSet Ds = new DataSet();


                string KD_JABATAN = text_kodejabatan.Text;
                msjabatanda.DeleteByKey(KD_JABATAN);

                DivMessage.InnerText = "Data Berhasil Dihapus";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Forms_Data_Master/Master_Jabatan.aspx";
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
                    string _CODE = gvMain.DataKeys[rowIndex]["KD_JABATAN"].ToString();

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