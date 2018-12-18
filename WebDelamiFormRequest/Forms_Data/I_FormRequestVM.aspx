<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="I_FormRequestVM.aspx.cs" MasterPageFile="~/Site.Master" Inherits="WebDelamiFormRequest.Forms_Data.I_FormRequestVM" %>

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
            <asp:Panel ID="PanelMainInputForm" runat="server">
                <div>
                    <h1>FORM REQUEST VM</h1>
                    <hr />
                    <div id="DivMessage" runat="server" visible="false">
                    </div>
                    <br />
                    <table>
                        <tr>
                            <td class="FN150">
                                <asp:Label ID="label_noform" runat="server" Text="No Form: "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:TextBox ID="text_noform" runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="FN150">
                                <asp:Label ID="label_tanggalrequest" runat="server" Text="Tanggal Request: "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:TextBox ID="text_tanggalrequest" runat="server" TextMode="Date"></asp:TextBox>
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
                                <asp:Label ID="label_kodecust" runat="server" Text="Dept.Store / Mall: "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:DropDownList ID="ddlkodecust" runat="server" Width="150px" Height="30px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150">
                                <asp:Label ID="label_brand" runat="server" Text="Brand: "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:TextBox ID="text_brand" runat="server"></asp:TextBox>
                            </td>
                            <td class="FN150">
                                <asp:Label ID="label_alamatlengkap" runat="server" Text="Alamat Lengkap : "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:TextBox ID="text_alamatlengkap" runat="server" TextMode="MultiLine" Columns="25" Rows="4" Height="45px" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150">
                                <asp:Label ID="label_luasarea" runat="server" Text="Luas Area: "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:TextBox ID="text_luasarea" runat="server"></asp:TextBox>
                            </td>
                            <td class="FN150">
                                <asp:Label ID="label_alokasibudget" runat="server" Text="Alokasi Budget: "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:TextBox ID="text_alokasibudget" runat="server" TextMode="Date"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td class="FN150">
                                <asp:Label ID="label_kodebudget" runat="server" Text="Kode Budget: "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:TextBox ID="text_kodebudget" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150">
                                <asp:Label ID="label_produkdijual" runat="server" Text="Produk Yang Dijual: "></asp:Label>
                            </td>
                            <td class="FN10">
                                <asp:TextBox ID="text_tops" runat="server" Width="20px"></asp:TextBox>
                                <asp:Label ID="label_percenttops" runat="server" Text="Tops %" Font-Size="Small" Width="90px"></asp:Label>
                                <asp:TextBox ID="text_bottoms" runat="server" Width="20px"></asp:TextBox>
                                <asp:Label ID="label_percentbottoms" runat="server" Text="Bottoms %" Font-Size="Small" Width="90px"></asp:Label>
                            </td>
                            <td class="FN10" colspan="2">
                                <asp:TextBox ID="text_blazer" runat="server" Width="20px"></asp:TextBox>
                                <asp:Label ID="label_percentblazer" runat="server" Text="Blazer %" Font-Size="Small" Width="100px"></asp:Label>
                                <asp:TextBox ID="text_aksesoris" runat="server" Width="20px"></asp:TextBox>
                                <asp:Label ID="label_aksesoris" runat="server" Text="Aksesoris %" Font-Size="Small" Width="100px"></asp:Label>
                                <asp:TextBox ID="text_shoessandal" runat="server" Width="20px"></asp:TextBox>
                                <asp:Label ID="label_shoessandal" runat="server" Text="Shoes %" Font-Size="Small" Width="100px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150">
                                <%--  <asp:Label ID="label1" runat="server" Text="Produk Yang Dijual: "></asp:Label>--%>
                            </td>
                            <td class="FN10">
                                <asp:TextBox ID="text_underwear" runat="server" Width="20px"></asp:TextBox>
                                <asp:Label ID="label_underwear" runat="server" Text="Underwear %" Font-Size="Small" Width="90px"></asp:Label>
                                <asp:TextBox ID="text_sportswear" runat="server" Width="20px"></asp:TextBox>
                                <asp:Label ID="label_sportswear" runat="server" Text="Sportswear %" Font-Size="Small" Width="90px"></asp:Label>
                            </td>
                            <td class="FN10" colspan="2">
                                <asp:TextBox ID="text_bras" runat="server" Width="20px"></asp:TextBox>
                                <asp:Label ID="label_bras" runat="server" Text="Bras %" Font-Size="Small" Width="100px"></asp:Label>
                                <asp:TextBox ID="text_panties" runat="server" Width="20px"></asp:TextBox>
                                <asp:Label ID="label_panties" runat="server" Text="Panties %" Font-Size="Small" Width="100px"></asp:Label>
                                <asp:TextBox ID="text_loungewear" runat="server" Width="20px"></asp:TextBox>
                                <asp:Label ID="label_loungewear" runat="server" Text="Loungewear %" Font-Size="Small" Width="110px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150">
                                <%--  <asp:Label ID="label1" runat="server" Text="Produk Yang Dijual: "></asp:Label>--%>
                            </td>
                            <td class="FN10">
                                <asp:TextBox ID="text_sleepwear" runat="server" Width="20px"></asp:TextBox>
                                <asp:Label ID="label_sleepwear" runat="server" Text="Sleepwear %" Font-Size="Small" Width="90px"></asp:Label>

                            </td>
                            <td class="FN10" colspan="2">
                                <asp:TextBox ID="text_other1" runat="server" Width="80px"></asp:TextBox>
                                <asp:TextBox ID="text_other2" runat="server" Width="80px"></asp:TextBox>
                                <asp:TextBox ID="text_other3" runat="server" Width="80px"></asp:TextBox>
                                <asp:TextBox ID="text_other4" runat="server" Width="80px"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td class="FN150">
                                <asp:Label ID="label_jenispekerjaan" runat="server" Text="Jenis Pekerjaan: "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:CheckBox ID="check_temaregular" runat="server" Text="" Font-Size="xx-Small" />
                                <asp:Label ID="label_temaregular" runat="server" Text="Tema Regular (CNY, Vintine)"></asp:Label>
                            </td>
                            <td class="FV270" colspan="2">
                                <asp:CheckBox ID="check_temaproduk" runat="server" Text="" Font-Size="xx-Small" />
                                <asp:Label ID="label_temaproduk" runat="server" Text="Tema Product (Prod P / Editor P, Wild one, Off Duty)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150">
                                <%--  <asp:Label ID="label1" runat="server" Text="Jenis Pekerjaan: "></asp:Label>--%>
                            </td>
                            <td class="FV270">
                                <asp:CheckBox ID="check_areapromosi" runat="server" Text="" Font-Size="xx-Small" />
                                <asp:Label ID="label_areapromosi" runat="server" Text="Area Promosi / Bazar / Temporer"></asp:Label>
                            </td>
                            <td class="FV270" colspan="2">
                                <asp:CheckBox ID="check_gantiposter" runat="server" Text="" Font-Size="xx-Small" />
                                <asp:Label ID="label_gantiposter" runat="server" Text="Ganti Poster" Width="100px"></asp:Label>
                                <asp:CheckBox ID="check_tambahkirimproperty" runat="server" Text="" Font-Size="xx-Small" />
                                <asp:Label ID="label_tambahkirimproperty" runat="server" Text="Tambah / Kirim Property"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150">
                                <%--  <asp:Label ID="label1" runat="server" Text="Jenis Pekerjaan: "></asp:Label>--%>
                            </td>
                            <td class="FV270">
                                <asp:CheckBox ID="check_mockuphanger" runat="server" Text="" Font-Size="xx-Small" />
                                <asp:Label ID="label_mockuphanger" runat="server" Text="Mock Up Hanger / Props"></asp:Label>
                            </td>
                            <td class="FV270" colspan="2">
                                <asp:CheckBox ID="check_lainlain" runat="server" Text="" Font-Size="xx-Small" />
                                <asp:Label ID="label_lainlain" runat="server" Text="Lain-Lain" Width="100px"></asp:Label>
                                <asp:TextBox ID="text_lainlain" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150">
                                <asp:Label ID="label_konsepdisain" runat="server" Text="Konsep Disain: "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:DropDownList ID="ddlkonsepdisain" runat="server" Width="150px" Height="30px">
                                    <asp:ListItem Text="Konsep Lama" Value="KONSEP_LAMA" Selected="True" />
                                    <asp:ListItem Text="Konsep Baru" Value="KONSEP_BARU" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150">
                                <asp:Label ID="label_kelengkapaninfo" runat="server" Text="Kelengkapan Info, gambar & ukuran yang diberikan: "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:CheckBox ID="check_keyplan" runat="server" Text="" Font-Size="xx-Small" />
                                <asp:Label ID="label_keyplan" runat="server" Text="Key Plan" Width="80px"></asp:Label>
                                <asp:CheckBox ID="check_fotolokasi" runat="server" Text="" Font-Size="xx-Small" />
                                <asp:Label ID="label_fotolokasi" runat="server" Text="Foto Lokasi"></asp:Label>
                            </td>
                            <td class="FV270" colspan="2">
                                <asp:CheckBox ID="check_layout" runat="server" Text="" Font-Size="xx-Small" />
                                <asp:Label ID="label_layout" runat="server" Text="Lay Out" Width="80px"></asp:Label>
                                <asp:CheckBox ID="check_brandcompetitor" runat="server" Text="" Font-Size="xx-Small" />
                                <asp:Label ID="label_brandcompetitor" runat="server" Text="Brand competitor di sekitar lokasi"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150">
                                <asp:Label ID="label_contohkelengkapanpendukung" runat="server" Text="Contoh Kelengkapan pendukung yang akan dipakai: "></asp:Label>
                            </td>
                            <td class="FV270" colspan="3">
                                <asp:Button ID="btn_KelengkapanPendukung" runat="server" Text="Klik disini untuk mengisi kelengkapan pendukung" />
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150">
                                <%--<asp:Label ID="label1" runat="server" Text="Contoh Kelengkapan pendukung yang akan dipakai: "></asp:Label>--%>
                            </td>
                            <td class="FV270" colspan="3">
                                <div class="EU_TableScrollCustom" style="display: block; max-height: 1000px;">
                                    <asp:GridView ID="gvMain" runat="server" AllowPaging="True" PagerSettings-Position="TopAndBottom" ShowHeaderWhenEmpty="true"
                                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AutoGenerateColumns="False"
                                        DataKeyNames="NO_FORM" EmptyDataText="- No Data Found -" OnPageIndexChanging="gvMainPageChanging" DataSourceID="C_Grid" OnRowCommand="gvMainRowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="NO_FORM" HeaderText="No Form" SortExpression="NO_FORM" Visible="false" />
                                            <asp:BoundField DataField="NO_FORM" HeaderText="No Form" />
                                            <asp:BoundField DataField="JENIS_ITEM_PENDUKUNG" HeaderText="Jenis Item Pendukung VM" />
                                            <asp:BoundField DataField="UKURAN" HeaderText="Ukuran" />
                                            <asp:BoundField DataField="JUMLAH" HeaderText="Jumlah" />
                                            <asp:BoundField DataField="KETERANGAN" HeaderText="Keterangan" />
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
                                        SelectCommand="SELECT * FROM TR_VM_PENDUKUNG"></asp:SqlDataSource>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td class="FN150">
                                <asp:Label ID="label_jadwalpekerjaan" runat="server" Text="Jadwal/Pekerjaan: "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:TextBox ID="text_jadwalpekerjaan" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150">
                                <asp:Label ID="label_jadwalsetup" runat="server" Text="Jadwal set up: "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:TextBox ID="text_jadwalsetup" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150">
                                <asp:Label ID="label_jadwalacara" runat="server" Text="Jadwal acara: "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:TextBox ID="text_jadwalacara" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150">
                                <asp:Label ID="label_informasitambahan" runat="server" Text="Informasi Tambahan (bila ada): "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:TextBox ID="text_informasitambahan" runat="server" TextMode="MultiLine" Columns="25" Rows="4" Height="60px" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150">
                                <asp:Label ID="label_dibuatordisetujui" runat="server" Text="Dibuat / Disetujui oleh: " Width="170px"></asp:Label>
                            </td>
                            <td class="FV270" colspan="3">

                                <asp:DropDownList ID="ddrdibuatordisetujui" runat="server" Width="150px" Height="30px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="FN150"></td>
                            <td class="FV270" colspan="3">
                                <asp:Button ID="btn_update" runat="server" Text="Approved" Visible="false" />
                                <asp:Button ID="btn_save" runat="server" Text="Save" />
                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClick="btn_cancel_Click" />
                                <asp:Button ID="btn_delete" runat="server" Text="Delete" />
                            </td>
                        </tr>
                    </table>


                </div>
            </asp:Panel>
        </div>

        <br />
    </asp:Panel>

    <!--Pop KelengkapanPendukung-->
    <asp:ModalPopupExtender ID="ModalKelengkapanPendukung" runat="server" TargetControlID="btn_KelengkapanPendukung"
        Drag="true" PopupControlID="PanelKelengkapanPendukung" CancelControlID="btn_Close" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelKelengkapanPendukung" runat="server" BackColor="WhiteSmoke" CssClass="ModalWindow"
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
                    <asp:Label ID="label_jenisitempendukungvm" runat="server" Text="Jenis Item Pendukung VM: " Width="170px"></asp:Label>
                    <asp:TextBox ID="text_jenisitempendukungvm" runat="server"></asp:TextBox>
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
                    <asp:Label ID="label_jumlah" runat="server" Text="Jumlah: " Width="170px"></asp:Label>
                    <asp:TextBox ID="text_jumlah" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="FN150">
                    <%--<asp:Label ID="label1" runat="server" Text="Contoh Kelengkapan pendukung yang akan dipakai: "></asp:Label>--%>
                </td>
                <td class="FV270" colspan="3">
                    <asp:Label ID="label_keterangan" runat="server" Text="Keterangan: " Width="170px"></asp:Label>
                    <asp:TextBox ID="text_keterangan" runat="server"></asp:TextBox></td>
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
</asp:Content>
