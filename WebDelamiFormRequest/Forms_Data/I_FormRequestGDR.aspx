<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="I_FormRequestGDR.aspx.cs" MasterPageFile="~/Site.Master" Inherits="WebDelamiFormRequest.Forms_Data.I_FormRequestGDR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .hidden {
            display: none;
        }

        .divWaiting {
            position: absolute;
            background-color: #FAFAFA;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            padding-top: 20%;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="../assets/css/style.css">
    <link rel="stylesheet" type="text/css" href="../Content/Site.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="PanelMain" runat="server">
        <div>
            <h1>FORM REQUEST GRAFIS - GRAPHIC DESIGN (GDR)</h1>
            <hr />
            <div id="DivMessage" runat="server" visible="false">
            </div>

            <br />

            <table>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_department" runat="server" Text="Department: "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:Label ID="label_departmentvalue" runat="server" ReadOnly="true" Width="200px" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="FN150">
                        <asp:Label ID="label_status" runat="server" Text="Status: "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:Label ID="label_statusvalue" runat="server" ReadOnly="true" Width="200px" Text="-" Font-Bold="true" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_noform" runat="server" Text="No Form: "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_noform" runat="server" ReadOnly="true" Font-Bold="true" ForeColor="Red"></asp:TextBox>
                    </td>
                    <td class="FN150">
                        <asp:Label ID="label_tanggalrequest" runat="server" Text="Tanggal Request: "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_tanggalrequest" runat="server" TextMode="Date" Font-Bold="true" ForeColor="Red"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_jenis" runat="server" Text="Jenis: "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:DropDownList ID="ddljenis" runat="server" Width="206px" Height="30px">
                            <asp:ListItem Text="Show Room" Value="SHOWROOM" Selected="True" />
                            <asp:ListItem Text="Counter" Value="COUNTER" />
                            <asp:ListItem Text="Putus" Value="PUTUS" />
                        </asp:DropDownList>
                    </td>
                    <td class="FN150">
                        <asp:Label ID="label_depstoremall" runat="server" Text="Dept.Store / Mall: "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_depstoremall" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_brand" runat="server" Text="Brand: "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_brand" runat="server" Font-Bold="true" ForeColor="Red" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="FN150">
                        <asp:Label ID="label_kota" runat="server" Text="Kota: "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_kota" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_alokasibudget" runat="server" Text="Alokasi Budget: "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_alokasibudget" runat="server" TextMode="Date" Font-Bold="true" ForeColor="Red"></asp:TextBox>
                    </td>
                    <td class="FN150">


                        <asp:Label ID="label_jadwalpergantianimage" runat="server" Text="Jadwal pergantian image/signature yang terakhir: "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_jadwalpergantianimage" runat="server" TextMode="Date" Font-Bold="true" ForeColor="Red"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_untuktoko" runat="server" Text="Untuk Toko: "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:DropDownList ID="ddruntuktoko" runat="server" Width="206px" Height="30px">
                            <asp:ListItem Text="Baru" Value="BARU" Selected="True" />
                            <asp:ListItem Text="Existing" Value="EXISTING" />
                            <asp:ListItem Text="Renovasi" Value="RENOVASI" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_jadwalacara" runat="server" Text="Jadwal Acara: "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_jadwalacara" runat="server" TextMode="Date" Font-Bold="true" ForeColor="Red"></asp:TextBox>
                    </td>
                    <td class="FN150">
                        <asp:Label ID="label_jadwalbukatoko" runat="server" Text="Jadwal buka toko: "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_jadwalbukatoko" runat="server" TextMode="Date" Font-Bold="true" ForeColor="Red"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_jenismateri" runat="server" Text="Beri gambaran / penjelasan / sketsa singkat ttg materi cetak yang diperlukan, sesuai pilihan anda." Font-Size="Small"></asp:Label>
                    </td>
                    <td class="FV270" colspan="3">
                        <asp:Button ID="btn_jenismateri" runat="server" Text="Klik disini untuk mengisi jenis materi cetak" />
                    </td>

                </tr>
                <tr>
                    <td class="FN150">
                        <%--<asp:Label ID="label1" runat="server" Text="Beri gambaran / penjelasan / sketsa singkat ttg materi cetak yang diperlukan, sesuai pilihan anda. bila perlu dibuat pada lembaran lain" Font-Size="X-Small"></asp:Label>--%>
                    </td>
                    <td class="FV270" colspan="3">
                        <div class="EU_TableScrollCustom" style="display: block; max-height: 1000px;">
                            <asp:GridView ID="gvMain" runat="server" AllowPaging="True" PagerSettings-Position="TopAndBottom" ShowHeaderWhenEmpty="true"
                                CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AutoGenerateColumns="False"
                                DataKeyNames="ID_materi" EmptyDataText="- No Data Found -" OnPageIndexChanging="gvMainPageChanging" DataSourceID="C_Grid" OnRowCommand="gvMainRowCommand">
                                <Columns>
                                    <asp:BoundField DataField="ID_materi" HeaderText="Id materi" SortExpression="NO_FORM" Visible="false" />
                                    <asp:BoundField DataField="NO_FORM" HeaderText="No Form" />
                                    <asp:BoundField DataField="JENIS_materi_CETAK" HeaderText="Jenis materi Cetak" />
                                    <asp:BoundField DataField="UKURAN" HeaderText="Ukuran" />
                                    <asp:BoundField DataField="JUMLAH_A4" HeaderText="Jumlah A4" />
                                    <asp:BoundField DataField="JUMLAH_A5" HeaderText="Jumlah A5" />
                                    <asp:BoundField DataField="JUMLAH_QTY" HeaderText="Jumlah QTY" />
                                    <asp:BoundField DataField="PENJELASAN" HeaderText="Penjelasan ttg isi / mekanisme Promosi / Tema / Wording / dll" HeaderStyle-Width="400px" />
                                    <%--  <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Button ID="btn_view" runat="server" Text="View Data" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="C_Grid" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                SelectCommand="SELECT * FROM TR_FORM1_GDR_MATERI"></asp:SqlDataSource>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_referensidisain" runat="server" Text="Referensi disain dari website/majalah/catalog/iklan/dll. Lampirkan di dalam file:" Font-Size="Small"></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:Button ID="btn_uploadfile1" runat="server" Text="Upload File 1" />
                    </td>
                    <td class="FV270" colspan="3">
                        <asp:Button ID="btn_uploadfile2" runat="server" Text="Upload File 2" />
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <%--<asp:Label ID="label1" runat="server" Text="Referensi disain dari website/majalah/catalog/iklan/dll. Lampirkan di dalam file:" Font-Size="Small"></asp:Label>--%>
                    </td>
                    <td class="FV270">
                        <asp:Button ID="btn_uploadfile3" runat="server" Text="Upload File 3" />
                    </td>
                    <td class="FV270" colspan="3">
                        <asp:Button ID="btn_uploadfile4" runat="server" Text="Upload File 4" />
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_jadwalpekerjaan" runat="server" Text="Jadwal Pekerjaan: " Font-Size="Small"></asp:Label>
                    </td>
                    <td class="FV270">
                        <br />
                        <asp:Label ID="label_jadwalselesaidisain" runat="server" Text="Jadwal selesai disain: " Font-Size="Small"></asp:Label>
                        <asp:TextBox ID="text_jadwalselesaidisain" runat="server" TextMode="Date" Font-Bold="true" ForeColor="Red"></asp:TextBox>
                    </td>
                    <td class="FV270">
                        <br />
                        <asp:Label ID="label_jadwalproduksi" runat="server" Text="Jadwal produksi / cetak: " Font-Size="Small"></asp:Label>
                        <asp:TextBox ID="text_jadwalproduksi" runat="server" TextMode="Date" Font-Bold="true" ForeColor="Red"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <%-- <asp:Label ID="label1" runat="server" Text="Jadwal Pekerjaan: " Font-Size="Small"></asp:Label>--%>
                    </td>
                    <td class="FV270">
                        <asp:Label ID="label_jadwalkirim" runat="server" Text="Jadwal kirim: " Font-Size="Small"></asp:Label>
                        <asp:TextBox ID="text_jadwalkirim" runat="server" TextMode="Date" Font-Bold="true" ForeColor="Red"></asp:TextBox>
                    </td>
                    <td class="FV270">
                        <asp:Label ID="label_jadwalpasang" runat="server" Text="Jadwal pasang: " Font-Size="Small"></asp:Label>
                        <asp:TextBox ID="text_jadwalpasang" runat="server" TextMode="Date" Font-Bold="true" ForeColor="Red"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_contactperson" runat="server" Text="Contact Person Pengiriman materi cetak: " Font-Size="Small"></asp:Label>
                    </td>
                    <td class="FV270">
                        <br />
                        <asp:Label ID="label_nama" runat="server" Text="Nama: " Font-Size="Small"></asp:Label>
                        <asp:TextBox ID="text_nama" runat="server"></asp:TextBox>
                    </td>
                    <td class="FV270">
                        <br />
                        <asp:Label ID="label_jabatan" runat="server" Text="Jabatan : " Font-Size="Small"></asp:Label>
                        <asp:TextBox ID="text_jabatan" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <%--   <asp:Label ID="label1" runat="server" Text="Contact Person Pengiriman materi cetak: " Font-Size="Small"></asp:Label>--%>
                    </td>
                    <td class="FV270">
                        <asp:Label ID="label_alamatkirim" runat="server" Text="Alamat Kirim: " Font-Size="Small"></asp:Label>
                        <asp:TextBox ID="text_alamatkirim" runat="server"></asp:TextBox>
                    </td>
                    <%--  <td class="FV270" colspan="3">
                        <asp:Label ID="label_jalannomorkota" runat="server" Text="jalan-nomor-kota, lantai-nomor counter/shr) : " Font-Size="Small"></asp:Label>
                        <asp:TextBox ID="text_jalannomorkota" runat="server"></asp:TextBox>
                    </td>--%>
                </tr>

                <tr>
                    <td class="FN150">
                        <%--   <asp:Label ID="label1" runat="server" Text="Contact Person Pengiriman materi cetak: " Font-Size="Small"></asp:Label>--%>
                    </td>
                    <td class="FV270">
                        <asp:Label ID="label_notelponkantor" runat="server" Text="No. Telp Ktr/HP/Fax: " Font-Size="Small"></asp:Label>
                        <asp:TextBox ID="text_notelponkantor" runat="server"></asp:TextBox>
                    </td>
                    <td class="FV270">
                        <asp:Label ID="label_email" runat="server" Text="Email: " Font-Size="Small"></asp:Label>
                        <asp:TextBox ID="text_email" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_dibuat" runat="server" Text="Dibuat oleh: " Width="170px"></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_dibuat" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="FN150">
                        <asp:Label ID="label_tanggaldibuat" runat="server" Text="Tanggal Dibuat:" Width="170px"></asp:Label>
                    </td>
                    <td class="FV270" colspan="3">
                        <asp:TextBox ID="text_tanggaldibuat" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_menyetujui1" runat="server" Text="Menyetujui Head Department: " Width="170px"></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_menyetujui1" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="FN150">
                        <asp:Label ID="label_tanggalmenyetujui1" runat="server" Text="Tanggal Menyetujui Head Department:" Width="170px"></asp:Label>
                    </td>
                    <td class="FV270" colspan="3">
                        <asp:TextBox ID="text_tanggalmenyetujui1" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_menyetujui2" runat="server" Text="Menyetujui Brand Manager: " Width="170px"></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_menyetujui2" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="FN150">
                        <asp:Label ID="label_tanggalmenyetujui2" runat="server" Text="Tanggal Menyetujui Brand Manager:" Width="170px"></asp:Label>
                    </td>
                    <td class="FV270" colspan="3">
                        <asp:TextBox ID="text_tanggalmenyetujui2" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_diterima1" runat="server" Text="Diterima Adm. Design: " Width="170px"></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_diterima1" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="FN150">
                        <asp:Label ID="label_tanggalditerima1" runat="server" Text="Tanggal Diterima Adm. Design:" Width="170px"></asp:Label>
                    </td>
                    <td class="FV270" colspan="3">
                        <asp:TextBox ID="text_tanggalditerima1" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_diterima2" runat="server" Text="Diterima Graphic Design: " Width="170px"></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_diterima2" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="FN150">
                        <asp:Label ID="label_tanggalditerima2" runat="server" Text="Tanggal Diterima Graphic Design:" Width="170px"></asp:Label>
                    </td>
                    <td class="FV270" colspan="3">
                        <asp:TextBox ID="text_tanggalditerima2" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_diterima3" runat="server" Text="Diterima Creative Manager: " Width="170px"></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_diterima3" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="FN150">
                        <asp:Label ID="label_tanggalditerima3" runat="server" Text="Tanggal Diterima Creative Manager:" Width="170px"></asp:Label>
                    </td>
                    <td class="FV270" colspan="3">
                        <asp:TextBox ID="text_tanggalditerima3" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                    </td>
                </tr>
                <asp:Panel ID="Pnl_Revisi" runat="server" Visible="false">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_revisi" runat="server" Text="Revisi: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_revisi" runat="server" TextMode="MultiLine" Rows="4" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td class="FN150"></td>
                    <td class="FV270" colspan="3">
                        <asp:Button ID="btn_Approved" runat="server" Text="Approved" Visible="false" OnClick="btn_Approved_Click" />
                        <asp:Button ID="btn_ToRevise" runat="server" Text="To Revise" Visible="false" OnClick="btn_ToRevise_Click" />
                        <asp:Button ID="btn_Revise" runat="server" Text="Revise" Visible="false" OnClick="btn_Revise_Click" />
                        <asp:Button ID="btn_Accepted" runat="server" Text="Accepted" Visible="false" OnClick="btn_Accepted_Click" />
                        <asp:Button ID="btn_Save" runat="server" Text="Posting" OnClick="btn_Save_Click" />
                        <asp:Button ID="btn_UpdatePosting" runat="server" Text="Update And Posting" OnClick="btn_UpdatePosting_Click" />
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" OnClick="btn_Cancel_Click" />
                        <asp:Button ID="btn_CancelPosting" runat="server" Text="Cancel Posting" OnClick="btn_Cancel_Posting_Click" Visible="false" />
                        <asp:Button ID="btn_Delete" runat="server" Text="Delete" OnClick="btn_Delete_Click" Visible="false" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <!--Pop Jenis materi-->
    <asp:ModalPopupExtender ID="ModalJenisMateri" runat="server" TargetControlID="btn_jenismateri"
        Drag="true" PopupControlID="PanelJenisMateri" CancelControlID="btn_Close" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelJenisMateri" runat="server" BackColor="WhiteSmoke" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: none; top: 684px; left: 39px; width: 50%;">
        <br />
        <asp:HiddenField ID="hdnTID" runat="server" />

        <table>
            <tr>
                <td class="FN150">
                    <%--<asp:Label ID="label1" runat="server" Text="Contoh Kelengkapan pendukung yang akan dipakai: "></asp:Label>--%>
                </td>
                <td class="FV270" colspan="3">
                    <asp:Label ID="label_jenismatericetak" runat="server" Text="Jenis : " Width="170px"></asp:Label>
                    <asp:TextBox ID="text_jenismatericetak" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="FN150">
                    <%--<asp:Label ID="label1" runat="server" Text="Contoh Kelengkapan pendukung yang akan dipakai: "></asp:Label>--%>
                </td>
                <td class="FV270" colspan="3">
                    <asp:Label ID="label_ukuran" runat="server" Text="Ukuran: " Width="170px"></asp:Label>
                    <asp:TextBox ID="text_ukuran" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="FN150">
                    <%--<asp:Label ID="label1" runat="server" Text="Contoh Kelengkapan pendukung yang akan dipakai: "></asp:Label>--%>
                </td>
                <td class="FV270" colspan="3">
                    <asp:Label ID="label_material" runat="server" Text="Material: " Width="170px"></asp:Label>
                    <asp:TextBox ID="text_material" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="FN150">
                    <%--<asp:Label ID="label1" runat="server" Text="Contoh Kelengkapan pendukung yang akan dipakai: "></asp:Label>--%>
                </td>
                <td class="FV270" colspan="3">
                    <asp:Label ID="label_jumlahA4" runat="server" Text="Jumlah A4: " Width="170px"></asp:Label>
                    <asp:TextBox ID="text_jumlahA4" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="FN150">
                    <%--<asp:Label ID="label1" runat="server" Text="Contoh Kelengkapan pendukung yang akan dipakai: "></asp:Label>--%>
                </td>
                <td class="FV270" colspan="3">
                    <asp:Label ID="label_jumlahA5" runat="server" Text="Jumlah A5: " Width="170px"></asp:Label>
                    <asp:TextBox ID="text_jumlahA5" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="FN150">
                    <%--<asp:Label ID="label1" runat="server" Text="Contoh Kelengkapan pendukung yang akan dipakai: "></asp:Label>--%>
                </td>
                <td class="FV270" colspan="3">
                    <asp:Label ID="label_jumlahQTY" runat="server" Text="Jumlah QTY: " Width="170px"></asp:Label>
                    <asp:TextBox ID="text_jumlahQTY" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="FN150">
                    <%--<asp:Label ID="label1" runat="server" Text="Contoh Kelengkapan pendukung yang akan dipakai: "></asp:Label>--%>
                </td>
                <td class="FV270" colspan="3">
                    <asp:Label ID="label_penjelasan" runat="server" Text="Penjelasan ttg isi: " Width="170px"></asp:Label>
                    <asp:TextBox ID="text_penjelasan" runat="server" TextMode="MultiLine" Columns="25" Rows="4"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="FN150">
                    <%--<asp:Label ID="label1" runat="server" Text="Contoh Kelengkapan pendukung yang akan dipakai: "></asp:Label>--%>
                </td>
                <td class="FV270" colspan="3">
                    <asp:Button ID="btn_AddData" runat="server" Text="Add" />
                    <asp:Button ID="btn_Close" runat="server" Text="Cancel" Width="100px" />
                </td>

            </tr>
        </table>

    </asp:Panel>

    <div id="HiddenFields">
        <asp:HiddenField ID="HfNO_FORM" runat="server" />
        <asp:HiddenField ID="HfUsername" runat="server" />
    </div>

</asp:Content>
