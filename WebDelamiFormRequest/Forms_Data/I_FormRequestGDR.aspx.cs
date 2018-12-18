using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDelamiFormRequest.DataLayer;
using WebDelamiFormRequest.Domain;

namespace WebDelamiFormRequest.Forms_Data
{
    public partial class I_FormRequestGDR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _NO_FORM = Request.QueryString["NO_FORM"];

            if (Page.IsPostBack)
                return;

            if (_NO_FORM == null)
            {
                StartFirstTime();
            }
            else
            {
                HfNO_FORM.Value = _NO_FORM;
                HfUsername.Value = Session["Username"].ToString();
                LoadDataFormRequestGDR();
            }

            HfUsername.Value = Session["Username"].ToString();
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("L_FormRequestGDR.aspx");
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            SaveFormRequestGDR();
        }

        protected void btn_UpdatePosting_Click(object sender, EventArgs e)
        {
            UpdatePostingFormRequestGDR();
        }

        protected void btn_Cancel_Posting_Click(object sender, EventArgs e)
        {
            UpdateCancelAll();
        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Approved_Click(object sender, EventArgs e)
        {
            UpdateApproveAll();
        }

        protected void btn_ToRevise_Click(object sender, EventArgs e)
        {
            Pnl_Revisi.Visible = true;
            btn_ToRevise.Visible = false;
            btn_Revise.Visible = true;
        }

        protected void btn_Revise_Click(object sender, EventArgs e)
        {
            UpdateReviseAll();
        }

        protected void btn_Accepted_Click(object sender, EventArgs e)
        {
            UpdateAcceptedAll();
            //foreach (GridViewRow row in gvMain.Rows)
            //{
            //    // here you'll get all rows with RowType=DataRow
            //    // others like Header are omitted in a foreach
            //}
        }

        #region gridview
        protected void gvMainPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
        }

        protected void gvMainRowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        #endregion

        #region All Functions

        // Action User
        public void StartFirstTime()
        {
            try
            {
                CreateNoFormGDR();
                label_departmentvalue.Text = Session["DepartemenName"].ToString();
                text_brand.Text = Session["BrandName"].ToString();
                text_tanggalrequest.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_alokasibudget.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_jadwalpergantianimage.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_jadwalacara.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_jadwalbukatoko.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_jadwalselesaidisain.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_jadwalkirim.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_jadwalproduksi.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_jadwalpasang.Text = DateTime.Now.ToString("yyyy-MM-dd");
                text_dibuat.Text = Session["Username"].ToString();
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void LoadDataFormRequestGDR()
        {
            try
            {
                DataSet Ds = new DataSet();
                DataSet DsDept = new DataSet();
                DataSet DsBrand = new DataSet();
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();
                MS_JABATAN_DA MsJabatanDA = new MS_JABATAN_DA();
                MS_DEPT_DA MsDeptDA = new MS_DEPT_DA();
                BRAND_DA BrandDA = new BRAND_DA();
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();

                Ds = TrForm1Gdr.GetDataByKey(HfNO_FORM.Value);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    text_noform.Text = Convert.ToString(Ds.Tables[0].Rows[0]["NO_FORM"].ToString());
                    ddljenis.Text = Convert.ToString(Ds.Tables[0].Rows[0]["JENIS"].ToString());
                    text_tanggalrequest.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_REQUEST"].ToString()).ToString("yyyy-MM-dd");
                    int ID_DEPT = Convert.ToInt32(Ds.Tables[0].Rows[0]["ID_DEPT"].ToString());
                    string KD_BRAND = Convert.ToString(Ds.Tables[0].Rows[0]["KD_BRAND"].ToString());
                    DsDept = MsDeptDA.GetDataByKey(ID_DEPT);
                    if (DsDept.Tables[0].Rows.Count > 0)
                    {
                        label_departmentvalue.Text = Convert.ToString(DsDept.Tables[0].Rows[0]["DEPT"].ToString());
                    }


                    DsBrand = BrandDA.GetDataByKey(KD_BRAND);
                    if (DsBrand.Tables[0].Rows.Count > 0)
                    {
                        text_brand.Text = Convert.ToString(DsBrand.Tables[0].Rows[0]["BRAND"].ToString());
                    }

                    text_depstoremall.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DEPT_STORE_MALL"].ToString());
                    text_kota.Text = Convert.ToString(Ds.Tables[0].Rows[0]["KOTA"].ToString());
                    text_alokasibudget.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["ALOKASI_BUDGET"].ToString()).ToString("yyyy-MM-dd");
                    text_jadwalpergantianimage.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["JADWAL_IMAGE"].ToString()).ToString("yyyy-MM-dd");
                    ddruntuktoko.Text = Convert.ToString(Ds.Tables[0].Rows[0]["UTK_TOKO"].ToString());
                    text_jadwalacara.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["JADWAL_ACARA"].ToString()).ToString("yyyy-MM-dd");
                    text_jadwalbukatoko.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["JADWAL_BUKA_TOKO"].ToString()).ToString("yyyy-MM-dd");
                    //Lampiran File Ada 4

                    text_jadwalselesaidisain.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["JADWAL_SELESAI_DESAIN"].ToString()).ToString("yyyy-MM-dd");
                    text_jadwalproduksi.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["JADWAL_PRODUKSI_CETAK"].ToString()).ToString("yyyy-MM-dd");
                    text_jadwalkirim.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["JADWAL_KIRIM"].ToString()).ToString("yyyy-MM-dd");
                    text_jadwalpasang.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["JADWAL_PASANG"].ToString()).ToString("yyyy-MM-dd");
                    text_nama.Text = Convert.ToString(Ds.Tables[0].Rows[0]["NAMA"].ToString());
                    text_jabatan.Text = Convert.ToString(Ds.Tables[0].Rows[0]["JABATAN"].ToString());
                    text_alamatkirim.Text = Convert.ToString(Ds.Tables[0].Rows[0]["ALAMAT_KIRIM"].ToString());
                    text_notelponkantor.Text = Convert.ToString(Ds.Tables[0].Rows[0]["NO_TELP"].ToString());
                    text_email.Text = Convert.ToString(Ds.Tables[0].Rows[0]["EMAIL"].ToString());
                    text_dibuat.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DIBUAT"].ToString());
                    text_tanggaldibuat.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DIBUAT"].ToString()).ToString("yyyy-MM-dd");
                    text_menyetujui1.Text = Convert.ToString(Ds.Tables[0].Rows[0]["MENYETUJUI1"].ToString());
                    text_tanggalmenyetujui1.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_MENYETUJUI1"].ToString()).ToString("yyyy-MM-dd");
                    text_menyetujui2.Text = Convert.ToString(Ds.Tables[0].Rows[0]["MENYETUJUI2"].ToString());
                    text_tanggalmenyetujui2.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_MENYETUJUI2"].ToString()).ToString("yyyy-MM-dd");
                    text_diterima1.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_1"].ToString());
                    text_tanggalditerima1.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DITERIMA_1"].ToString()).ToString("yyyy-MM-dd");
                    text_diterima2.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_2"].ToString());
                    text_tanggalditerima2.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DITERIMA_2"].ToString()).ToString("yyyy-MM-dd");
                    text_diterima3.Text = Convert.ToString(Ds.Tables[0].Rows[0]["DITERIMA_3"].ToString());
                    text_tanggalditerima3.Text = DateTime.Parse(Ds.Tables[0].Rows[0]["TGL_DITERIMA_3"].ToString()).ToString("yyyy-MM-dd");
                    label_statusvalue.Text = Convert.ToString(Ds.Tables[0].Rows[0]["STATUS"].ToString());
                    text_revisi.Text = Convert.ToString(Ds.Tables[0].Rows[0]["REVISI"].ToString());
                    string Status = Convert.ToString(Ds.Tables[0].Rows[0]["STATUS"].ToString());

                    //Mendapatkan Urutan User Berdasarkan Jabatan
                    Ds = MsUserDA.GetDataUrutanUser(HfUsername.Value);

                    int URUTAN = 0;
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        URUTAN = Convert.ToInt32(Ds.Tables[0].Rows[0]["URUTAN"].ToString());

                        if (URUTAN == 1)
                        {
                            if (Status == EApprovalStatus.OnApproved)
                            {
                                btn_Save.Visible = false;
                                btn_UpdatePosting.Enabled = false;
                                btn_CancelPosting.Enabled = false;
                                btn_Cancel.Enabled = false;
                            }
                            else if (Status == EApprovalStatus.OnRevise)
                            {
                                Pnl_Revisi.Visible = true;
                                btn_Save.Visible = false;
                                btn_UpdatePosting.Enabled = true;
                                btn_CancelPosting.Enabled = true;
                                btn_Cancel.Enabled = false;
                            }
                            else if (Status == EApprovalStatus.Cancel)
                            {
                                btn_Save.Visible = false;
                                btn_UpdatePosting.Enabled = false;
                                btn_CancelPosting.Enabled = false;
                                btn_Cancel.Enabled = false;
                            }
                            else
                            {
                                btn_Save.Visible = false;
                                btn_UpdatePosting.Enabled = false;
                                btn_CancelPosting.Enabled = false;
                                btn_Cancel.Enabled = false;
                            }
                        }
                        else if (URUTAN == 2)
                        {
                            text_menyetujui1.Text = HfUsername.Value;
                            ShowButtonUrutan2();

                            if (Status == EApprovalStatus.OnApproved)
                            {
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = true;
                                btn_Revise.Enabled = true;
                                btn_CancelPosting.Enabled = true;
                            }
                            else if (Status == EApprovalStatus.Approved)
                            {
                                btn_Approved.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_CancelPosting.Enabled = false;
                            }
                            else if (Status == EApprovalStatus.OnRevise)
                            {
                                Pnl_Revisi.Visible = true;
                                btn_Approved.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_CancelPosting.Enabled = false;
                            }
                            else
                            {
                                btn_Approved.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_CancelPosting.Enabled = false;
                            }


                        }
                        else if (URUTAN == 3)
                        {
                            text_menyetujui2.Text = HfUsername.Value;
                            ShowButtonUrutan3();
                            if (Status == EApprovalStatus.OnApproved)
                            {
                                btn_Approved.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_CancelPosting.Enabled = false;
                            }
                            else if (Status == EApprovalStatus.Approved)
                            {
                                btn_Approved.Enabled = true;
                                btn_ToRevise.Enabled = true;
                                btn_Revise.Enabled = true;
                                btn_CancelPosting.Enabled = true;
                            }
                            else if (Status == EApprovalStatus.OnRevise)
                            {
                                Pnl_Revisi.Visible = true;
                                btn_Approved.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_CancelPosting.Enabled = false;
                            }
                            else
                            {
                                btn_Approved.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_CancelPosting.Enabled = false;
                            }

                        }
                        else if (URUTAN == 4)
                        {
                            text_diterima1.Text = HfUsername.Value;
                            ShowButtonUrutan4();
                            if (Status == EApprovalStatus.ApprovedBM)
                            {
                                btn_Accepted.Enabled = true;
                                btn_ToRevise.Enabled = true;
                                btn_Revise.Enabled = true;
                                btn_CancelPosting.Enabled = true;
                            }

                            else if (Status == EApprovalStatus.OnRevise)
                            {
                                Pnl_Revisi.Visible = true;
                                btn_Accepted.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_CancelPosting.Enabled = false;
                            }
                            else
                            {
                                btn_Accepted.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_CancelPosting.Enabled = false;
                            }

                        }
                        else if (URUTAN == 5)
                        {
                            text_diterima2.Text = HfUsername.Value;
                            ShowButtonUrutan5();
                            if (Status == EApprovalStatus.AcceptedAdmDesign)
                            {
                                btn_Accepted.Enabled = true;
                                btn_ToRevise.Enabled = true;
                                btn_Revise.Enabled = true;
                                btn_CancelPosting.Enabled = true;
                            }

                            else if (Status == EApprovalStatus.OnRevise)
                            {
                                Pnl_Revisi.Visible = true;
                                btn_Accepted.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_CancelPosting.Enabled = false;
                            }
                            else
                            {
                                btn_Accepted.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_CancelPosting.Enabled = false;
                            }
                        }
                        else if (URUTAN == 6)
                        {
                            text_diterima3.Text = HfUsername.Value;
                            ShowButtonUrutan6();
                            if (Status == EApprovalStatus.AcceptedGraphicDesign)
                            {
                                btn_Accepted.Enabled = true;
                                btn_ToRevise.Enabled = true;
                                btn_Revise.Enabled = true;
                                btn_CancelPosting.Enabled = true;
                            }

                            else if (Status == EApprovalStatus.OnRevise)
                            {
                                Pnl_Revisi.Visible = true;
                                btn_Accepted.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_CancelPosting.Enabled = false;
                            }
                            else
                            {
                                btn_Accepted.Enabled = false;
                                btn_ToRevise.Enabled = false;
                                btn_Revise.Enabled = false;
                                btn_CancelPosting.Enabled = false;
                            }
                        }

                    }

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

        public void CreateNoFormGDR()
        {
            try
            {
                string where = string.Format("NO_FORM <> '' ORDER BY NO_FORM DESC");

                string NO_FORM = "";
                DataSet Ds = new DataSet();
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();

                Ds = TrForm1Gdr.GetDataFilter(where);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    string noform = Ds.Tables[0].Rows[0]["NO_FORM"].ToString();
                    string noformangka = noform.Substring(noform.Length - 1);
                    decimal angkaakhir = Convert.ToDecimal(noformangka) + 1;
                    NO_FORM = "FORM-" + "GDR-" + angkaakhir;
                    text_noform.Text = NO_FORM;
                    label_statusvalue.Text = "New Data";
                }
                else
                {
                    NO_FORM = "FORM-GDR-0001";
                    text_noform.Text = NO_FORM;
                    label_statusvalue.Text = "New Data";

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

        public void SaveFormRequestGDR()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string KODE_FORM = "FRM-0001";
                string JENIS = ddljenis.Text;
                DateTime? TGL_REQUEST = Convert.ToDateTime(text_tanggalrequest.Text);
                int ID_DEPT = Convert.ToInt16(Session["ID_DEPT"].ToString());
                string KD_BRAND = Session["KD_BRAND"].ToString();
                string DEPT_STORE_MALL = text_depstoremall.Text;
                string KOTA = text_kota.Text;
                DateTime? ALOKASI_BUDGET = Convert.ToDateTime(text_alokasibudget.Text);
                DateTime? JADWAL_IMAGE = Convert.ToDateTime(text_jadwalpergantianimage.Text);
                string UTK_TOKO = ddruntuktoko.Text;
                DateTime? JADWAL_ACARA = Convert.ToDateTime(text_jadwalacara.Text);
                DateTime? JADWAL_BUKA_TOKO = Convert.ToDateTime(text_jadwalbukatoko.Text);
                string RFR_LAMPIRAN1 = "";
                string RFR_LAMPIRAN2 = "";
                string RFR_LAMPIRAN3 = "";
                string RFR_LAMPIRAN4 = "";
                DateTime? JADWAL_SELESAI_DESAIN = Convert.ToDateTime(text_jadwalselesaidisain.Text);
                DateTime? JADWAL_PRODUKSI_CETAK = Convert.ToDateTime(text_jadwalproduksi.Text);
                DateTime? JADWAL_KIRIM = Convert.ToDateTime(text_jadwalkirim.Text);
                DateTime? JADWAL_PASANG = Convert.ToDateTime(text_jadwalpasang.Text);
                string NAMA = text_nama.Text;
                string JABATAN = text_jabatan.Text;
                string ALAMAT_KIRIM = text_alamatkirim.Text;
                string NO_TELP = text_notelponkantor.Text;
                string EMAIL = text_email.Text;
                string DIBUAT = text_dibuat.Text;
                DateTime TGL_DIBUAT = DateTime.Now;
                DateTime startdate = new DateTime(1999, 01, 01);

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.KODE_FORM = KODE_FORM;
                trform1gdr.JENIS = JENIS;
                trform1gdr.TGL_REQUEST = TGL_REQUEST;
                trform1gdr.ID_DEPT = ID_DEPT;
                trform1gdr.KD_BRAND = KD_BRAND;
                trform1gdr.DEPT_STORE_MALL = DEPT_STORE_MALL;
                trform1gdr.KOTA = KOTA;
                trform1gdr.ALOKASI_BUDGET = ALOKASI_BUDGET;
                trform1gdr.JADWAL_IMAGE = JADWAL_IMAGE;
                trform1gdr.UTK_TOKO = UTK_TOKO;
                trform1gdr.JADWAL_ACARA = JADWAL_ACARA;
                trform1gdr.JADWAL_BUKA_TOKO = JADWAL_BUKA_TOKO;
                trform1gdr.RFR_LAMPIRAN1 = RFR_LAMPIRAN1;
                trform1gdr.RFR_LAMPIRAN2 = RFR_LAMPIRAN2;
                trform1gdr.RFR_LAMPIRAN3 = RFR_LAMPIRAN3;
                trform1gdr.RFR_LAMPIRAN4 = RFR_LAMPIRAN4;
                trform1gdr.JADWAL_SELESAI_DESAIN = JADWAL_SELESAI_DESAIN;
                trform1gdr.JADWAL_PRODUKSI_CETAK = JADWAL_PRODUKSI_CETAK;
                trform1gdr.JADWAL_KIRIM = JADWAL_KIRIM;
                trform1gdr.JADWAL_PASANG = JADWAL_PASANG;
                trform1gdr.NAMA = NAMA;
                trform1gdr.JABATAN = JABATAN;
                trform1gdr.ALAMAT_KIRIM = ALAMAT_KIRIM;
                trform1gdr.NO_TELP = NO_TELP;
                trform1gdr.EMAIL = EMAIL;
                trform1gdr.DIBUAT = DIBUAT;
                trform1gdr.TGL_DIBUAT = TGL_DIBUAT;
                trform1gdr.MENYETUJUI1 = "";
                trform1gdr.TGL_MENYETUJUI1 = startdate;
                trform1gdr.MENYETUJUI2 = "";
                trform1gdr.TGL_MENYETUJUI2 = startdate;
                trform1gdr.DITERIMA_1 = "";
                trform1gdr.TGL_DITERIMA_1 = startdate;
                trform1gdr.DITERIMA_2 = "";
                trform1gdr.TGL_DITERIMA_2 = startdate;
                trform1gdr.DITERIMA_3 = "";
                trform1gdr.TGL_DITERIMA_3 = startdate;
                trform1gdr.STATUS = EApprovalStatus.OnApproved;
                trform1gdr.REVISI = "";
                TrForm1Gdr.Insert(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Disimpan";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Forms_Data/L_FormRequestGDR.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");

            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdatePostingFormRequestGDR()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string KODE_FORM = "FRM-0001";
                string JENIS = ddljenis.Text;
                DateTime? TGL_REQUEST = Convert.ToDateTime(text_tanggalrequest.Text);
                int ID_DEPT = Convert.ToInt16(Session["ID_DEPT"].ToString());
                string KD_BRAND = Session["KD_BRAND"].ToString();
                string DEPT_STORE_MALL = text_depstoremall.Text;
                string KOTA = text_kota.Text;
                DateTime? ALOKASI_BUDGET = Convert.ToDateTime(text_alokasibudget.Text);
                DateTime? JADWAL_IMAGE = Convert.ToDateTime(text_jadwalpergantianimage.Text);
                string UTK_TOKO = ddruntuktoko.Text;
                DateTime? JADWAL_ACARA = Convert.ToDateTime(text_jadwalacara.Text);
                DateTime? JADWAL_BUKA_TOKO = Convert.ToDateTime(text_jadwalbukatoko.Text);
                string RFR_LAMPIRAN1 = "";
                string RFR_LAMPIRAN2 = "";
                string RFR_LAMPIRAN3 = "";
                string RFR_LAMPIRAN4 = "";
                DateTime? JADWAL_SELESAI_DESAIN = Convert.ToDateTime(text_jadwalselesaidisain.Text);
                DateTime? JADWAL_PRODUKSI_CETAK = Convert.ToDateTime(text_jadwalproduksi.Text);
                DateTime? JADWAL_KIRIM = Convert.ToDateTime(text_jadwalkirim.Text);
                DateTime? JADWAL_PASANG = Convert.ToDateTime(text_jadwalpasang.Text);
                string NAMA = text_nama.Text;
                string JABATAN = text_jabatan.Text;
                string ALAMAT_KIRIM = text_alamatkirim.Text;
                string NO_TELP = text_notelponkantor.Text;
                string EMAIL = text_email.Text;
                string DIBUAT = text_dibuat.Text;
                DateTime TGL_DIBUAT = DateTime.Now;
                DateTime startdate = new DateTime(1999, 01, 01);

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.KODE_FORM = KODE_FORM;
                trform1gdr.JENIS = JENIS;
                trform1gdr.TGL_REQUEST = TGL_REQUEST;
                trform1gdr.ID_DEPT = ID_DEPT;
                trform1gdr.KD_BRAND = KD_BRAND;
                trform1gdr.DEPT_STORE_MALL = DEPT_STORE_MALL;
                trform1gdr.KOTA = KOTA;
                trform1gdr.ALOKASI_BUDGET = ALOKASI_BUDGET;
                trform1gdr.JADWAL_IMAGE = JADWAL_IMAGE;
                trform1gdr.UTK_TOKO = UTK_TOKO;
                trform1gdr.JADWAL_ACARA = JADWAL_ACARA;
                trform1gdr.JADWAL_BUKA_TOKO = JADWAL_BUKA_TOKO;
                trform1gdr.RFR_LAMPIRAN1 = RFR_LAMPIRAN1;
                trform1gdr.RFR_LAMPIRAN2 = RFR_LAMPIRAN2;
                trform1gdr.RFR_LAMPIRAN3 = RFR_LAMPIRAN3;
                trform1gdr.RFR_LAMPIRAN4 = RFR_LAMPIRAN4;
                trform1gdr.JADWAL_SELESAI_DESAIN = JADWAL_SELESAI_DESAIN;
                trform1gdr.JADWAL_PRODUKSI_CETAK = JADWAL_PRODUKSI_CETAK;
                trform1gdr.JADWAL_KIRIM = JADWAL_KIRIM;
                trform1gdr.JADWAL_PASANG = JADWAL_PASANG;
                trform1gdr.NAMA = NAMA;
                trform1gdr.JABATAN = JABATAN;
                trform1gdr.ALAMAT_KIRIM = ALAMAT_KIRIM;
                trform1gdr.NO_TELP = NO_TELP;
                trform1gdr.EMAIL = EMAIL;
                trform1gdr.DIBUAT = DIBUAT;
                trform1gdr.TGL_DIBUAT = TGL_DIBUAT;
                trform1gdr.MENYETUJUI1 = "";
                trform1gdr.TGL_MENYETUJUI1 = startdate;
                trform1gdr.MENYETUJUI2 = "";
                trform1gdr.TGL_MENYETUJUI2 = startdate;
                trform1gdr.DITERIMA_1 = "";
                trform1gdr.TGL_DITERIMA_1 = startdate;
                trform1gdr.DITERIMA_2 = "";
                trform1gdr.TGL_DITERIMA_2 = startdate;
                trform1gdr.DITERIMA_3 = "";
                trform1gdr.TGL_DITERIMA_3 = startdate;
                trform1gdr.STATUS = EApprovalStatus.OnApproved;
                trform1gdr.REVISI = "";
                TrForm1Gdr.Update(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Diupdate Dan Diposting";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateApproveAll()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value);

                int URUTAN = 0;
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                    if (URUTAN == 2)
                    {
                        UpdateStatusApproved();

                    }
                    else if (URUTAN == 3)
                    {
                        UpdateStatusApprovedBM();

                    }

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

        public void UpdateAcceptedAll()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value);

                int URUTAN = 0;
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                    if (URUTAN == 4)
                    {
                        UpdateStatusAcceptedAdmDesign();

                    }
                    else if (URUTAN == 5)
                    {
                        UpdateStatusAcceptedGraphicDesign();

                    }
                    else if (URUTAN == 6)
                    {
                        UpdateStatusAcceptedCreativeManager();
                    }

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

        public void UpdateCancelAll()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value);

                int URUTAN = 0;
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                    if (URUTAN == 1)
                    {
                        UpdateStatusCancel();

                    }
                    else if (URUTAN == 2)
                    {
                        UpdateStatusCancelHD();

                    }
                    else if (URUTAN == 3)
                    {
                        UpdateStatusCancelBM();
                    }
                    else if (URUTAN == 4)
                    {
                        UpdateStatusCancelAdmDesign();
                    }
                    else if (URUTAN == 5)
                    {
                        UpdateStatusCancelGraphicDesign();
                    }
                    else if (URUTAN == 6)
                    {
                        UpdateStatusCancelCreativeManager();
                    }

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

        public void UpdateReviseAll()
        {
            try
            {
                DataSet DsUser = new DataSet();
                MS_USER_DA MsUserDA = new MS_USER_DA();

                //Mendapatkan Urutan User Berdasarkan Jabatan
                DsUser = MsUserDA.GetDataUrutanUser(HfUsername.Value);

                int URUTAN = 0;
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    URUTAN = Convert.ToInt32(DsUser.Tables[0].Rows[0]["URUTAN"].ToString());

                    if (URUTAN == 1)
                    {
                       //

                    }
                    else if (URUTAN == 2)
                    {
                        UpdateStatusRevisiHD();

                    }
                    else if (URUTAN == 3)
                    {
                        UpdateStatusRevisiBM();
                    }
                    else if (URUTAN == 4)
                    {
                        UpdateStatusRevisiAdmDesign();
                    }
                    else if (URUTAN == 5)
                    {
                        UpdateStatusRevisiGraphicDesign();
                    }
                    else if (URUTAN == 6)
                    {
                        UpdateStatusRevisiCreativeManager();
                    }

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

        //Action Head Department
        public void UpdateStatusApproved()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string MENYETUJUI1 = text_menyetujui1.Text;
                DateTime TGL_MENYETUJUI1 = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.MENYETUJUI1 = MENYETUJUI1;
                trform1gdr.TGL_MENYETUJUI1 = TGL_MENYETUJUI1;
                trform1gdr.STATUS = EApprovalStatus.Approved;
                //trform1gdr.REVISI = "";
                TrForm1Gdr.UpdateMenyetujui1(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Approved";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //Action Brand Manager
        public void UpdateStatusApprovedBM()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string MENYETUJUI2 = text_menyetujui2.Text;
                DateTime TGL_MENYETUJUI2 = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.MENYETUJUI2 = MENYETUJUI2;
                trform1gdr.TGL_MENYETUJUI2 = TGL_MENYETUJUI2;
                trform1gdr.STATUS = EApprovalStatus.ApprovedBM;
                //trform1gdr.REVISI = "";
                TrForm1Gdr.UpdateMenyetujui2(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Approved";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //Action Adm Design
        public void UpdateStatusAcceptedAdmDesign()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_1 = text_diterima1.Text;
                DateTime TGL_DITERIMA_1 = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.DITERIMA_1 = DITERIMA_1;
                trform1gdr.TGL_DITERIMA_1 = TGL_DITERIMA_1;
                trform1gdr.STATUS = EApprovalStatus.AcceptedAdmDesign;
                //trform1gdr.REVISI = "";
                TrForm1Gdr.UpdateDiterima1(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Accept Adm Design";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //Action Graphic Design
        public void UpdateStatusAcceptedGraphicDesign()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_2 = text_diterima2.Text;
                DateTime TGL_DITERIMA_2 = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.DITERIMA_2 = DITERIMA_2;
                trform1gdr.TGL_DITERIMA_2 = TGL_DITERIMA_2;
                trform1gdr.STATUS = EApprovalStatus.AcceptedGraphicDesign;
                //trform1gdr.REVISI = "";
                TrForm1Gdr.UpdateDiterima2(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Accept Graphic Design";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        //Action Creative Manager
        public void UpdateStatusAcceptedCreativeManager()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_3 = text_diterima3.Text;
                DateTime TGL_DITERIMA_3 = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.DITERIMA_3 = DITERIMA_3;
                trform1gdr.TGL_DITERIMA_3 = TGL_DITERIMA_3;
                trform1gdr.STATUS = EApprovalStatus.Done;
                //trform1gdr.REVISI = "";
                TrForm1Gdr.UpdateDiterima3(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Accept Graphic Design";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        /// <summary>
        /// Update Status Cancel Function
        /// </summary>

        public void UpdateStatusCancel()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DIBUAT = text_dibuat.Text;
                DateTime TGL_DIBUAT = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.DIBUAT = DIBUAT;
                trform1gdr.TGL_DIBUAT = TGL_DIBUAT;
                trform1gdr.STATUS = EApprovalStatus.Cancel;
                //trform1gdr.REVISI = "";
                TrForm1Gdr.UpdateDibuat(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Cancel";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusCancelHD()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string MENYETUJUI1 = text_menyetujui1.Text;
                DateTime TGL_MENYETUJUI1 = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.MENYETUJUI1 = MENYETUJUI1;
                trform1gdr.TGL_MENYETUJUI1 = TGL_MENYETUJUI1;
                trform1gdr.STATUS = EApprovalStatus.Cancel;
                //trform1gdr.REVISI = "";
                TrForm1Gdr.UpdateMenyetujui1(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Cancel";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusCancelBM()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string MENYETUJUI2 = text_menyetujui2.Text;
                DateTime TGL_MENYETUJUI2 = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.MENYETUJUI2 = MENYETUJUI2;
                trform1gdr.TGL_MENYETUJUI2 = TGL_MENYETUJUI2;
                trform1gdr.STATUS = EApprovalStatus.Cancel;
                //trform1gdr.REVISI = "";
                TrForm1Gdr.UpdateMenyetujui2(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Cancel";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusCancelAdmDesign()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_1 = text_diterima1.Text;
                DateTime TGL_DITERIMA_1 = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.DITERIMA_1 = DITERIMA_1;
                trform1gdr.TGL_DITERIMA_1 = TGL_DITERIMA_1;
                trform1gdr.STATUS = EApprovalStatus.Cancel;
                //trform1gdr.REVISI = "";
                TrForm1Gdr.UpdateDiterima1(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Cancel";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusCancelGraphicDesign()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_2 = text_diterima2.Text;
                DateTime TGL_DITERIMA_2 = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.DITERIMA_2 = DITERIMA_2;
                trform1gdr.TGL_DITERIMA_2 = TGL_DITERIMA_2;
                trform1gdr.STATUS = EApprovalStatus.Cancel;
                //trform1gdr.REVISI = "";
                TrForm1Gdr.UpdateDiterima2(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Cancel";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusCancelCreativeManager()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_3 = text_diterima3.Text;
                DateTime TGL_DITERIMA_3 = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.DITERIMA_3 = DITERIMA_3;
                trform1gdr.TGL_DITERIMA_3 = TGL_DITERIMA_3;
                trform1gdr.STATUS = EApprovalStatus.Cancel;
                //trform1gdr.REVISI = "";
                TrForm1Gdr.UpdateDiterima3(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Cancel";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        /// <summary>
        /// Update Status Revisi Plus Revisinya
        /// </summary>
        /// 

        public void UpdateStatusRevisiHD()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string MENYETUJUI1 = text_menyetujui1.Text;
                string REVISI = text_revisi.Text;
                DateTime TGL_MENYETUJUI1 = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.MENYETUJUI1 = MENYETUJUI1;
                trform1gdr.TGL_MENYETUJUI1 = TGL_MENYETUJUI1;
                trform1gdr.STATUS = EApprovalStatus.OnRevise;
                trform1gdr.REVISI = REVISI;
                TrForm1Gdr.UpdateRevisiMenyetujui1(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Revise";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusRevisiBM()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string MENYETUJUI2 = text_menyetujui2.Text;
                string REVISI = text_revisi.Text;
                DateTime TGL_MENYETUJUI2 = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.MENYETUJUI2 = MENYETUJUI2;
                trform1gdr.TGL_MENYETUJUI2 = TGL_MENYETUJUI2;
                trform1gdr.STATUS = EApprovalStatus.OnRevise;
                trform1gdr.REVISI = REVISI;
                TrForm1Gdr.UpdateRevisiMenyetujui2(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Revise";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusRevisiAdmDesign()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_1 = text_diterima1.Text;
                string REVISI = text_revisi.Text;
                DateTime TGL_DITERIMA_1 = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.DITERIMA_1 = DITERIMA_1;
                trform1gdr.TGL_DITERIMA_1 = TGL_DITERIMA_1;
                trform1gdr.STATUS = EApprovalStatus.OnRevise;
                trform1gdr.REVISI = REVISI;
                TrForm1Gdr.UpdateRevisiDiterima1(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Revise";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusRevisiGraphicDesign()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_2 = text_diterima2.Text;
                string REVISI = text_revisi.Text;
                DateTime TGL_DITERIMA_2 = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.DITERIMA_2 = DITERIMA_2;
                trform1gdr.TGL_DITERIMA_2 = TGL_DITERIMA_2;
                trform1gdr.STATUS = EApprovalStatus.OnRevise;
                trform1gdr.REVISI = REVISI;
                TrForm1Gdr.UpdateRevisiDiterima2(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Revise";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }

        public void UpdateStatusRevisiCreativeManager()
        {
            try
            {
                TR_FORM1_GDR_DA TrForm1Gdr = new DataLayer.TR_FORM1_GDR_DA();
                DataSet Ds = new DataSet();

                string NO_FORM = text_noform.Text;
                string DITERIMA_3 = text_diterima3.Text;
                string REVISI = text_revisi.Text;
                DateTime TGL_DITERIMA_3 = DateTime.Now;

                TR_FORM1_GDR trform1gdr = new TR_FORM1_GDR();
                trform1gdr.NO_FORM = NO_FORM;
                trform1gdr.DITERIMA_3 = DITERIMA_3;
                trform1gdr.TGL_DITERIMA_3 = TGL_DITERIMA_3;
                trform1gdr.STATUS = EApprovalStatus.OnRevise;
                trform1gdr.REVISI = REVISI;
                TrForm1Gdr.UpdateRevisiDiterima3(trform1gdr);

                DivMessage.InnerText = "Data Berhasil Di Revise";
                DivMessage.Style.Add("color", "black");
                DivMessage.Style.Add("background-color", "skyblue");
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;

                string HomePageUrl = "../Reports/ReportStatusForm.aspx";
                Page.Header.Controls.Add(new LiteralControl(string.Format(@" <META http-equiv='REFRESH' content=3;url={0}> ", HomePageUrl)));
                //Response.Redirect("~/Forms_Data/L_FormRequestGDR.aspx");
            }
            catch (Exception Ex)
            {
                DivMessage.InnerText = Ex.Message;
                DivMessage.Attributes["class"] = "error";
                //DivMessage.Attributes["class"] = "success";
                DivMessage.Visible = true;
            }
        }


        /// <summary>
        /// Show Button Sesuai Urutan
        /// </summary>

        public void ShowButtonUrutan2()
        {
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdatePosting.Visible = false;

            btn_ToRevise.Visible = true;
            //btn_Revise.Visible = true;
            btn_Approved.Visible = true;
            btn_CancelPosting.Visible = true;
        }

        public void ShowButtonUrutan3()
        {
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdatePosting.Visible = false;

            btn_ToRevise.Visible = true;
            //btn_Revise.Visible = true;
            btn_Approved.Visible = true;
            btn_CancelPosting.Visible = true;
        }

        public void ShowButtonUrutan4()
        {
            btn_Save.Visible = false;
            btn_Approved.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdatePosting.Visible = false;


            btn_Accepted.Visible = true;
            btn_ToRevise.Visible = true;
            //btn_Revise.Visible = true;
            btn_CancelPosting.Visible = true;
        }

        public void ShowButtonUrutan5()
        {
            btn_Save.Visible = false;
            btn_Approved.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdatePosting.Visible = false;

            btn_Accepted.Visible = true;
            btn_ToRevise.Visible = true;
            //btn_Revise.Visible = true;
            btn_CancelPosting.Visible = true;
        }

        public void ShowButtonUrutan6()
        {
            btn_Save.Visible = false;
            btn_Approved.Visible = false;
            btn_Cancel.Visible = false;
            btn_UpdatePosting.Visible = false;

            btn_Accepted.Visible = true;
            btn_ToRevise.Visible = true;
            //btn_Revise.Visible = true;
            btn_CancelPosting.Visible = true;
        }

        #endregion


    }
}