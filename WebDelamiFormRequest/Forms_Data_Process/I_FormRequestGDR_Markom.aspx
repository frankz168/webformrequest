<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="I_FormRequestGDR_Markom.aspx.cs" MasterPageFile="~/Site.Master" Inherits="WebDelamiFormRequest.Forms_Data_Process.I_FormRequestGDR_Markom" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/HeaderMenu.ascx" TagPrefix="TVBUC1" TagName="HeaderMenu" %>

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
    <%-- <script type="text/javascript">
        function SelectAll(id) {
            //get reference of GridView control
            var grid = document.getElementById("<%= gvCustCt.ClientID %>");
            //variable to contain the cell of the grid
            var cell;

            if (grid.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid.rows.length; i++) {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];

                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++) {
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type == "checkbox") {
                            //assign the status of the Select All checkbox to the cell 
                            //checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }
    </script>--%>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save / update data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">

        function move_up() {
            scroll_clipper.scrollTop = 0;
        }

    </script>
    <script type="text/javascript">

        function onCalendarHidden() {
            var cal = $find("CalendeExtenderAlokasiBudget");

            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarShown() {

            var cal = $find("CalendeExtenderAlokasiBudget");

            cal._switchMode("months", true);

            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("CalendeExtenderAlokasiBudget");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    //cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }
    </script>
    <script type="text/javascript">
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUpload.ClientID %>").click();
            }
        }
    </script>
    <script type="text/javascript">
        function UploadFileMaterial(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUploadMaterial.ClientID %>").click();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="PanelMain" runat="server">
        <div>
            <div id="Menu">
                <TVBUC1:HeaderMenu runat="server" ID="HM" />
            </div>

            <br />
            <h1>FORM REQUEST GRAFIS - GRAPHIC DESIGN (GDR)-Markom</h1>
            <hr />
            <div id="DivMessage" runat="server" visible="false">
            </div>

            <br />

            <table>
                <asp:Panel ID="Pnl_Forms" runat="server">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_permintaandesign" runat="server" Text="Type Of Request : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:DropDownList ID="ddlpermintaandesign" runat="server" Width="206px" Height="30px">
                                <asp:ListItem Text="Repeat Design" Value="REPEAT_DESIGN" Selected="True" />
                                <asp:ListItem Text="New Design" Value="NEW_DESIGN" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="Pnl_KategoriPermintaan" runat="server" Visible="true">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_kategoripermintaan" runat="server" Text="Category Request: " Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:CheckBox ID="checkbox_mediacetak" runat="server" Text="" Font-Size="xx-Small" GroupName="KP" />
                            <asp:Label ID="label_mediacetak" runat="server" Text="Media Cetak (Brosur/Pamflet/Flyer/Banner/Umbul2/Poster)" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">
                            <asp:CheckBox ID="checkbox_digitaladvertising" runat="server" Text="" Font-Size="xx-Small" GroupName="KP" />
                            <asp:Label ID="label_digitaladvertising" runat="server" Text="Digital Advertising (Iklan Online/Sosial Media/Etc)" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">
                            <asp:CheckBox ID="checkbox_socialmedia" runat="server" Text="" Font-Size="xx-Small" GroupName="KP" />
                            <asp:Label ID="label_socialmedia" runat="server" Text="Social Media (Instagram/Facebook/Etc)" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:CheckBox ID="checkbox_other" runat="server" Text="" Font-Size="xx-Small" GroupName="KP" />
                            <asp:Label ID="label_other" runat="server" Text="Other (Booth/POP-Up store/Corporate/Adpage/Baliho/Voucher)" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:CheckBox ID="checkbox_others1Markom" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:TextBox ID="text_others1Markom" runat="server" Text="" Font-Size="Small" Width="130px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="Pnl_FotograferSelect" runat="server" Visible="true">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_photographer" runat="server" Text="Design Required Photographer? : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:DropDownList ID="ddlphotographer" runat="server" Width="206px" Height="30px" AutoPostBack="true" OnSelectedIndexChanged="ddlphotographer_SelectedIndexChanged">
                                <asp:ListItem Text="Yes" Value="Yes" />
                                <asp:ListItem Text="No" Value="No" Selected="True" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="Pnl_DigitalImagingSelect" runat="server" Visible="true">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_digitalimaging" runat="server" Text="Design Required DI : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:DropDownList ID="ddldigitalimaging" runat="server" Width="206px" Height="30px" AutoPostBack="true" OnSelectedIndexChanged="ddldigitalimaging_SelectedIndexChanged">
                                <asp:ListItem Text="Yes" Value="Yes" />
                                <asp:ListItem Text="No" Value="No" Selected="True" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="Pnl_Production" runat="server" Visible="true">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_production" runat="server" Text="Material Printed or No ? : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:DropDownList ID="ddlproduction" runat="server" Width="206px" Height="30px" AutoPostBack="true" OnSelectedIndexChanged="ddlproduction_SelectedIndexChanged">
                                <asp:ListItem Text="Yes" Value="Yes" />
                                <asp:ListItem Text="No" Value="No" Selected="True" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="Pnl_FormOthers1" runat="server">
                    <tr>

                        <td class="FN150">
                            <asp:Label ID="label_department" runat="server" Text="Department: (*)"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:Label ID="label_departmentvalue" runat="server" ReadOnly="true" Width="200px" ForeColor="DodgerBlue" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_status" runat="server" Text="Status: (*)"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:Label ID="label_statusvalue" runat="server" ReadOnly="true" Width="200px" Text="-" Font-Bold="true" ForeColor="DodgerBlue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_noform" runat="server" Text="No Form: (*)"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_noform" runat="server" ReadOnly="true" Font-Bold="true" ForeColor="DodgerBlue"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalrequest" runat="server" Text="Request Date: (*)"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalrequest" runat="server" TextMode="Date" Font-Bold="true" ForeColor="DodgerBlue" ReadOnly="true"></asp:TextBox>

                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalrequired" runat="server" Text="Required Date: (*)"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalrequired" runat="server" TextMode="Date" Font-Bold="true" ForeColor="DodgerBlue"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_jenis" runat="server" Text="Store Type: "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:DropDownList ID="ddljenis" runat="server" Width="206px" Height="30px" AutoPostBack="true" OnSelectedIndexChanged="ddljenis_SelectedIndexChanged1">
                                <asp:ListItem Text="Opening Store" Value="OPENING_STORE" />
                                <asp:ListItem Text="Renovasi Store" Value="RENOVASI_STORE" />
                                <asp:ListItem Text="Existing Store" Value="EXISTING_STORE" />
                                <asp:ListItem Text="Event/Bazzar" Value="BAZZAR" />
                                <asp:ListItem Text="Opening Counter" Value="OPENING_COUNTER" />
                                <asp:ListItem Text="Renovasi Counter" Value="RENOVASI_COUNTER" />
                                <asp:ListItem Text="Existing Counter" Value="EXISTING_COUNTER" />
                                <asp:ListItem Text="Dealers" Value="DEALERS" Selected="True" />
                                <asp:ListItem Text="NO STORE" Value="NO_STORE" />
                                <%--         <asp:ListItem Text="Digital Marketing" Value="DIGITAL_MARKETING" />--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_uploadfilestore" runat="server" Text="Upload File Store: "></asp:Label>
                        </td>
                        <td class="FV270">

                            <asp:FileUpload ID="btn_uploadfilestore" runat="server" Text="Upload File Store" Font-Size="X-Small" ViewStateMode="Enabled" />
                            <asp:LinkButton ID="link_filenameuploadstore" runat="server" Text="-"></asp:LinkButton>
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            </asp:UpdatePanel>
                            <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />
                            <asp:Button ID="btnFilenameStoreClear" runat="server" Text="Clear" OnClick="btnFilenameStoreClear_Click" PostBackUrl="~/Forms_Data_Process/I_FormRequestGDR_Markom.aspx"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_depstoremall" runat="server" Text="Dept.Store / Mall: "></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <div class="EU_TableScrollCustom" style="display: block; max-height: 200px; max-width: 1400px; width: 750px">
                                <dx:ASPxGridView ID="gvCustCt" runat="server" AutoGenerateColumns="False" DataSourceID="C_GridCustCt" KeyFieldName="ID">
                                    <Settings ShowFilterRow="True" />
                                    <SettingsBehavior AllowSelectByRowClick="true" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <SettingsBehavior AllowSelectByRowClick="true" />
                                    <Columns>

                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" SelectAllCheckboxMode="AllPages">
                                            <ClearFilterButton Visible="True">
                                            </ClearFilterButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="ID" VisibleIndex="1" Caption="ID" HeaderStyle-Wrap="True" Visible="false">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="KODE_FORM" VisibleIndex="2" Caption="Kode Form" HeaderStyle-Wrap="True" Visible="false">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NO_FORM" VisibleIndex="3" Caption="No Form" HeaderStyle-Wrap="True" Visible="false">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="kode_cust" VisibleIndex="4" Caption="Kode Toko" HeaderStyle-Wrap="True">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="kode_ct" VisibleIndex="5" Caption="Kode CT" HeaderStyle-Wrap="True">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="site" VisibleIndex="6" Caption="Site" HeaderStyle-Wrap="True">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="nama_cust" VisibleIndex="7" Caption="Nama Cust" HeaderStyle-Wrap="True">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="nama_ct" VisibleIndex="8" Caption="Nama CT" HeaderStyle-Wrap="True">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                                <%--                                <asp:GridView ID="gvCustCt" runat="server" AllowPaging="False" PagerSettings-Position="TopAndBottom" ShowHeaderWhenEmpty="true"
                                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AutoGenerateColumns="False"
                                    DataKeyNames="ID" EmptyDataText="- No Data Found -" OnPageIndexChanging="gvCustCt_PageIndexChanging" OnRowDataBound="gvCustCt_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRow" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="Id" SortExpression="ID" Visible="false" />
                                        <asp:BoundField DataField="KODE_FORM" HeaderText="Kode Form" Visible="false" />
                                        <asp:BoundField DataField="NO_FORM" HeaderText="No Form" Visible="false" />
                                        <asp:BoundField DataField="kode_cust" HeaderText="Kode Toko" HeaderStyle-Width="150px" />
                                        <asp:BoundField DataField="kode_ct" HeaderText="Kode CT" HeaderStyle-Width="150px" />
                                        <asp:BoundField DataField="site" HeaderText="Site" HeaderStyle-Width="150px" />
                                        <asp:BoundField DataField="nama_cust" HeaderText="Nama Toko" HeaderStyle-Width="350px" />
                                        <asp:BoundField DataField="nama_ct" HeaderText="Nama CT" HeaderStyle-Width="150px" />
                                    </Columns>
                                </asp:GridView>--%>
                                <asp:SqlDataSource ID="C_GridCustCt" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                    SelectCommand="SELECT * FROM TR_FORM_GDR_CUST_TEMP WHERE NO_FORM = @NO_FORM">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="text_noform" Name="NO_FORM" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </td>
                    </tr>

                </asp:Panel>

                <asp:Panel ID="Pnl_Acarabktoko" runat="server" Visible="false">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_jadwalacarabktoko" runat="server" Text="Schedule Opening Store/Counter/Bazzar: (*)"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_jadwalacarabktoko" runat="server" TextMode="Date" Font-Bold="true" ForeColor="DodgerBlue"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="Pnl_FormOthers2" runat="server">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_brand" runat="server" Text="Brand: (*)"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_brand" runat="server" Font-Bold="true" ForeColor="DodgerBlue" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_alokasibudget" runat="server" Text="Budget Allocation: (*)"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_alokasibudget" runat="server" />
                            <asp:CalendarExtender ID="CalendeExtenderAlokasiBudget" runat="server" Enabled="true" Format="yyMM" ClientIDMode="Static"
                                TargetControlID="text_alokasibudget" DefaultView="Months" OnClientShown="onCalendarShown"
                                OnClientHidden="onCalendarHidden">
                            </asp:CalendarExtender>
                            <%-- <asp:TextBox ID="text_alokasibudget" runat="server" TextMode="Date" Font-Bold="true" ForeColor="DodgerBlue"></asp:TextBox>--%>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_jadwalpergantianimage" runat="server" Text="The last schedule for changing images: (*)"></asp:Label>
                            <asp:RadioButton ID="radio_yesjadwalpergantianimage" runat="server" GroupName="radioimage" OnCheckedChanged="radio_yesjadwalpergantianimage_CheckedChanged" Checked="true" AutoPostBack="true" />
                            <asp:Label ID="label_yesjadwalpergantianimage" runat="server" Text="Yes" Font-Size="Small"></asp:Label>
                            <asp:RadioButton ID="radio_nojadwalpergantianimage" runat="server" GroupName="radioimage" OnCheckedChanged="radio_nojadwalpergantianimage_CheckedChanged" Checked="false" AutoPostBack="true" />
                            <asp:Label ID="label_nojadwalpergantianimage" runat="server" Text="No" Font-Size="Small"></asp:Label>
                        </td>
                        <asp:Panel ID="Pnl_JadwalPergantianImage" runat="server" Visible="false">
                            <td class="FV270">
                                <asp:TextBox ID="text_jadwalpergantianimage" runat="server" TextMode="Date" Font-Bold="true" ForeColor="DodgerBlue"></asp:TextBox>
                            </td>
                        </asp:Panel>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_jenismatericetakcheckbox" runat="server" Text="Details Of Content: " Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_jenismateri" runat="server" Text="Details Of Content (*)" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:Button ID="btn_jenismateri" runat="server" Text="Add Content" />
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_uploadfilematerial" runat="server" Text="Upload File material: "></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">

                            <asp:FileUpload ID="btn_uploadfilematerial" runat="server" Text="Upload File material" Font-Size="X-Small" ViewStateMode="Enabled" />
                            <asp:LinkButton ID="link_filenameuploadmaterial" runat="server" Text="-"></asp:LinkButton>
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            </asp:UpdatePanel>
                            <asp:Button ID="btnUploadMaterial" Text="Upload" runat="server" OnClick="UploadMaterial" Style="display: none" />
                            <asp:Button ID="btnFilenamematerialClear" runat="server" Text="Clear" OnClick="btnFilenamematerialClear_Click" PostBackUrl="~/Forms_Data_Process/I_FormRequestGDR_VM.aspx"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <%--<asp:Label ID="label1" runat="server" Text="Beri gambaran / penjelasan / sketsa singkat ttg materi cetak yang diperlukan, sesuai pilihan anda. bila perlu dibuat pada lembaran lain" Font-Size="X-Small"></asp:Label>--%>
                        </td>
                        <td class="FV270" colspan="3">
                            <div class="EU_TableScrollCustom" style="display: block; max-height: 800px;">
                                <asp:GridView ID="gvMain" runat="server" ShowHeaderWhenEmpty="true"
                                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AutoGenerateColumns="False"
                                    DataKeyNames="ID_materi" EmptyDataText="- No Data Found -" OnPageIndexChanging="gvMainPageChanging" OnRowCommand="gvMainRowCommand" OnRowDeleting="gvMain_RowDeleting" OnRowCancelingEdit="gvMain_RowCancelingEdit" OnRowEditing="gvMain_RowEditing" OnRowUpdating="gvMain_RowUpdating" OnRowDataBound="gvMain_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="ID_materi" HeaderText="Id materi" SortExpression="NO_FORM" Visible="false" />
                                        <asp:BoundField DataField="NO_FORM" HeaderText="No Form" Visible="false" />
                                        <asp:BoundField DataField="site" HeaderText="Site" HeaderStyle-Width="50px" />
                                        <asp:BoundField DataField="nama_cust" HeaderText="Nama Site" HeaderStyle-Width="150px" />
                                        <asp:BoundField DataField="JENIS_MATERIAL_CETAK" HeaderText="Content" HeaderStyle-Width="150px" />
                                        <asp:BoundField DataField="UKURAN" HeaderText="Size" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="MATERIAL" HeaderText="Material" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="JUMLAH_QTY" HeaderText="QTY" HeaderStyle-Width="50px" />
                                        <asp:BoundField DataField="PENJELASAN" HeaderText="Note" HeaderStyle-Width="400px" />
                                        <asp:CommandField ShowEditButton="True" ButtonType="Button" HeaderText="Action" />
                                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" HeaderText="Action" />
                                        <%--    <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Button ID="btn_delete" runat="server" Text="Delete" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="C_Grid" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                    SelectCommand="SELECT * FROM TR_FORM2_GDR_MATERI"></asp:SqlDataSource>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_referensidisain" runat="server" Text="Concept or Reference Design:" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_referensidesign" runat="server" Font-Bold="true" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="Pnl_UploadFileRequester" runat="server">
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">

                            <asp:FileUpload ID="btn_uploadfile1" runat="server" Text="Upload File 1" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename1" runat="server" Text="-" OnClick="linkbtn_filename1_Click"></asp:LinkButton>
                            <%--<asp:LinkButton ID="linkbtn_deletefilename1" runat="server" Text="Delete" OnClick="linkbtn_deletefilename1_Click"></asp:LinkButton>--%>
                        </td>
                        <td class="FV270" colspan="2">
                            <asp:FileUpload ID="btn_uploadfile2" runat="server" Text="Upload File 2" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename2" runat="server" Text="-" OnClick="linkbtn_filename2_Click"></asp:LinkButton>
                            <%--                        <asp:LinkButton ID="linkbtn_deletefilename2" runat="server" Text="Delete"></asp:LinkButton>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <%--<asp:Label ID="label1" runat="server" Text="Referensi disain dari website/majalah/catalog/iklan/dll. Lampirkan di dalam file:" Font-Size="Small"></asp:Label>--%>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile3" runat="server" Text="Upload File 3" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename3" runat="server" Text="-" OnClick="linkbtn_filename3_Click"></asp:LinkButton>
                            <%--                        <asp:LinkButton ID="linkbtn_deletefilename3" runat="server" Text="Delete"></asp:LinkButton>--%>
                        </td>
                        <td class="FV270" colspan="2">
                            <asp:FileUpload ID="btn_uploadfile4" runat="server" Text="Upload File 4" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename4" runat="server" Text="-" OnClick="linkbtn_filename4_Click"></asp:LinkButton>
                            <%--                        <asp:LinkButton ID="linkbtn_deletefilename4" runat="server" Text="Delete"></asp:LinkButton>--%>
                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="Pnl_JadwalPekerjaan" runat="server">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_jadwalpekerjaan" runat="server" Text="Working Schedule: " Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <br />
                            <asp:Label ID="label_jadwalfoto" runat="server" Text="Time Complete Photographer: (*)" Font-Size="Small"></asp:Label>
                            <asp:TextBox ID="text_jadwalfoto" runat="server" TextMode="Date" Font-Bold="true" ForeColor="DodgerBlue" Width="200px"></asp:TextBox>

                        </td>
                        <td class="FV270">
                            <br />
                            <asp:Label ID="label_jadwaldi" runat="server" Text="Time Complete Digital Imaging: (*)" Font-Size="Small"></asp:Label>
                            <asp:TextBox ID="text_jadwaldi" runat="server" TextMode="Date" Font-Bold="true" ForeColor="DodgerBlue" Width="200px"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <%-- <asp:Label ID="label1" runat="server" Text="Jadwal Pekerjaan: " Font-Size="Small"></asp:Label>--%>
                        </td>
                        <td class="FV270">
                            <asp:Label ID="label_jadwaladmcreative" runat="server" Text="Validation Adm Creative: (*)" Font-Size="Small"></asp:Label>
                            <asp:TextBox ID="text_jadwaladmcreative" runat="server" TextMode="Date" Font-Bold="true" ForeColor="DodgerBlue" Width="200px"></asp:TextBox>
                        </td>
                        <td class="FV270">
                            <asp:Label ID="label_jadwalselesaidisain" runat="server" Text="Time Complete Graphic Design: (*)" Font-Size="Small"></asp:Label>
                            <asp:TextBox ID="text_jadwalselesaidisain" runat="server" TextMode="Date" Font-Bold="true" ForeColor="DodgerBlue" Width="200px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:Label ID="label_jadwalproduksi" runat="server" Text="Time Complete Production: (*)" Font-Size="Small"></asp:Label>
                            <asp:TextBox ID="text_jadwalproduksi" runat="server" TextMode="Date" Font-Bold="true" ForeColor="DodgerBlue" Width="200px"></asp:TextBox>
                        </td>
                        <td class="FV270">
                            <asp:Label ID="label_jadwalkirim" runat="server" Text="Time Complete Distribution: (*)" Font-Size="Small"></asp:Label>
                            <asp:TextBox ID="text_jadwalkirim" runat="server" TextMode="Date" Font-Bold="true" ForeColor="DodgerBlue" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" colspan="4">
                            <hr runat="server" id="hr1" style="height: 5px; background-color: black" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_dibuat" runat="server" Text="Requester: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_dibuat" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggaldibuat" runat="server" Text="Date Create Form: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggaldibuat" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_menyetujui1" runat="server" Text="Head Department: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_menyetujui1" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalmenyetujui1" runat="server" Text="Date Approved Form:" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalmenyetujui1" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_menyetujui2" runat="server" Text="Brand Manager: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_menyetujui2" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalmenyetujui2" runat="server" Text="Date Approved Form:" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalmenyetujui2" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_diterima1" runat="server" Text="Admin.Creative: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_diterima1" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalditerima1" runat="server" Text="Date Accepted Form:" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalditerima1" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>

                </asp:Panel>

                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_diterima2" runat="server" Text="Graphic Design: " Width="210px"></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:DropDownList ID="ddlditerima2" runat="server" Width="200px" Height="30px" Enabled="false" DataSourceID="C_GridGraphicDesigner" DataValueField="Username" DataTextField="UserCustom"></asp:DropDownList>
                    </td>
                    <td class="FN150">
                        <asp:Label ID="label_tanggalditerima2" runat="server" Text="Date Posting Design:" Width="170px"></asp:Label>
                    </>
                    <td class="FV270">
                        <asp:TextBox ID="text_tanggalditerima2" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_diterima3" runat="server" Text="Creative Director: " Width="170px"></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_diterima3" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="FN150">
                        <asp:Label ID="label_tanggalditerima3" runat="server" Text="Date Approved Creative Director:" Width="170px"></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_tanggalditerima3" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                    </td>
                </tr>

                <asp:Panel ID="Pnl_ProductionApprove" runat="server" Visible="true">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_diterimalain5materi" runat="server" Text="Production (Printed Material): " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_diterimalain5materi" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalditerimalain5materi" runat="server" Text="Date Print Material:" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalditerimalain5materi" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_diterimalain5" runat="server" Text="Production (Distribution Material): " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_diterimalain5" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalditerimalain5" runat="server" Text="Date Distribution Material:" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalditerimalain5" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>

                <tr>
                    <td style="width: 100%" colspan="4">
                        <hr runat="server" id="hr3" style="height: 5px; background-color: black" />
                    </td>
                </tr>

                <asp:Panel ID="Pnl_PhotoGrapher" runat="server" Visible="true">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_diterimalain1" runat="server" Text="Photographer: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_diterimalain1" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalditerimalain1" runat="server" Text="Date Posting Photo:" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalditerimalain1" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_diterimalain2" runat="server" Text="Creative Director: " Width="270px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_diterimalain2" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalditerimalain2" runat="server" Text="Date Approved Photo:" Width="270px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalditerimalain2" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>

                <tr>
                    <td style="width: 100%" colspan="4">
                        <hr runat="server" id="hr4" style="height: 5px; background-color: black" />
                    </td>
                </tr>

                <asp:Panel ID="Pnl_DigitalImaging" runat="server" Visible="true">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_diterimalain3" runat="server" Text="Digital Imaging: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_diterimalain3" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalditerimalain3" runat="server" Text="Date Posting Digital Imaging:" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalditerimalain3" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_diterimalain4" runat="server" Text="Creative Director: " Width="270px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_diterimalain4" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalditerimalain4" runat="server" Text="Date Approved Digital Imaging:" Width="270px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalditerimalain4" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>



                <asp:Panel ID="Pnl_UploadFilePG" runat="server" Visible="false">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_uploadfilepg" runat="server" Text="Upload File Photographer: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfilepg1" runat="server" Text="Upload File 1" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filenamepg1" runat="server" Text="-" OnClick="linkbtn_filenamepg1_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270" colspan="2">
                            <asp:FileUpload ID="btn_uploadfilepg2" runat="server" Text="Upload File 2" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filenamepg2" runat="server" Text="-" OnClick="linkbtn_filenamepg2_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfilepg3" runat="server" Text="Upload File 3" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filenamepg3" runat="server" Text="-" OnClick="linkbtn_filenamepg3_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270" colspan="2">
                            <asp:FileUpload ID="btn_uploadfilepg4" runat="server" Text="Upload File 4" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filenamepg4" runat="server" Text="-" OnClick="linkbtn_filenamepg4_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="Pnl_UploadFileDI" runat="server" Visible="false">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_uploadfiledi" runat="server" Text="Upload File Digital Imaging: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfiledi1" runat="server" Text="Upload File 1" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filenamedi1" runat="server" Text="-" OnClick="linkbtn_filenamedi1_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270" colspan="2">
                            <asp:FileUpload ID="btn_uploadfiledi2" runat="server" Text="Upload File 2" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filenamedi2" runat="server" Text="-" OnClick="linkbtn_filenamedi2_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfiledi3" runat="server" Text="Upload File 3" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filenamedi3" runat="server" Text="-" OnClick="linkbtn_filenamedi3_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270" colspan="2">
                            <asp:FileUpload ID="btn_uploadfiledi4" runat="server" Text="Upload File 4" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filenamedi4" runat="server" Text="-" OnClick="linkbtn_filenamedi4_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="Pnl_UploadFileGD" runat="server" Visible="false">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_uploadfilegd" runat="server" Text="Upload File Image Graphic Design: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfilegd1" runat="server" Text="Upload File 1" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filenamegd1" runat="server" Text="-" OnClick="linkbtn_filenamegd1_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270" colspan="2">
                            <asp:FileUpload ID="btn_uploadfilegd2" runat="server" Text="Upload File 2" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filenamegd2" runat="server" Text="-" OnClick="linkbtn_filename2_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfilegd3" runat="server" Text="Upload File 3" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filenamegd3" runat="server" Text="-" OnClick="linkbtn_filenamegd3_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270" colspan="2">
                            <asp:FileUpload ID="btn_uploadfilegd4" runat="server" Text="Upload File 4" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filenamegd4" runat="server" Text="-" OnClick="linkbtn_filenamegd4_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </asp:Panel>


                <asp:Panel ID="Pnl_StatusVer" runat="server" Visible="false">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_statusver" runat="server" Text="Status Ver: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_statusver" runat="server" ReadOnly="true" Font-Bold="true" ForeColor="DodgerBlue"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="Pnl_RevisiLoad" runat="server" Visible="false">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_revisiload" runat="server" Text="Revise: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_revisiload" runat="server" TextMode="MultiLine" Rows="4" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="Pnl_Commentar" runat="server" Visible="false">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_commentar" runat="server" Text="Commentar: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_commentar" runat="server" TextMode="MultiLine" Rows="4" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>

                <tr>
                    <td style="width: 100%" colspan="4">
                        <hr runat="server" id="hr2" style="height: 5px; background-color: black" />
                    </td>
                </tr>

                <asp:Panel ID="Pnl_Buttons" runat="server">
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">
                            <asp:Panel ID="Pnl_Created" runat="server" Visible="false">
                                <asp:Button ID="btn_Save" runat="server" Text="Submit" OnClick="btn_Save_Click" OnClientClick="Confirm()" />
                                <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" OnClick="btn_Cancel_Click" />
                            </asp:Panel>
                            <asp:Button ID="btn_Done" runat="server" Text="Done" Visible="false" OnClick="btn_Done_Click" OnClientClick="Confirm()" />
                            <asp:Button ID="btn_Approved" runat="server" Text="Approved" Visible="false" OnClick="btn_Approved_Click" OnClientClick="Confirm()" />
                            <asp:Button ID="btn_Accepted" runat="server" Text="Approved" Visible="false" OnClick="btn_Accepted_Click" OnClientClick="Confirm()" />
                            <asp:Button ID="btn_UpdateSubmit" runat="server" Text="Update And Submit" OnClick="btn_UpdateSubmit_Click" OnClientClick="Confirm()" />
                            <asp:Button ID="btn_UpdateSubmitDesign" runat="server" Text="Update And Submit Design" OnClick="btn_UpdateSubmitDesign_Click" OnClientClick="Confirm()" />
                            <asp:Button ID="btn_ToRevise" runat="server" Text="To Revise" Visible="false" OnClick="btn_ToRevise_Click" />
                            <asp:Button ID="btn_ToReviseDesign" runat="server" Text="To Revise Design" Visible="false" OnClick="btn_ToReviseDesign_Click" />
                            <asp:Button ID="btn_ReviseContent" runat="server" Text="On Revise Content" Visible="false" OnClick="btn_ReviseContent_Click" OnClientClick="Confirm()" />
                            <asp:Button ID="btn_Reject" runat="server" Text="Reject" OnClick="btn_Reject_Click" Visible="false" OnClientClick="Confirm()" />
                            <asp:Button ID="btn_Delete" runat="server" Text="Delete" OnClick="btn_Delete_Click" Visible="false" />
                        </td>
                    </tr>

                </asp:Panel>
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
                <td class="FN150"></td>
                <td class="FV270" colspan="3">
                    <asp:Label ID="label_jenismatericetak" runat="server" Text="Content : (*)" Width="170px"></asp:Label>
                    <asp:TextBox ID="text_jenismatericetak" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="FN150"></td>
                <td class="FV270" colspan="3">
                    <asp:Label ID="label_ukuran" runat="server" Text="Size: (*)" Width="170px"></asp:Label>
                    <asp:TextBox ID="text_ukuran" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="FN150"></td>
                <td class="FV270" colspan="3">
                    <asp:Label ID="label_material" runat="server" Text="Material: (*)" Width="170px"></asp:Label>
                    <asp:TextBox ID="text_material" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="FN150"></td>
                <td class="FV270" colspan="3">
                    <asp:Label ID="label_jumlahQTY" runat="server" Text="QTY: (*)" Width="170px"></asp:Label>
                    <asp:TextBox ID="text_jumlahQTY" runat="server" TextMode="Number" Text="0"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="FN150"></td>
                <td class="FV270" colspan="3">
                    <asp:Label ID="label_penjelasan" runat="server" Text="Note: " Width="170px"></asp:Label>
                    <asp:TextBox ID="text_penjelasan" runat="server" TextMode="MultiLine" Columns="25" Rows="4"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="FN150"></td>
                <td class="FV270" colspan="3">
                    <asp:Button ID="btn_AddData" runat="server" Text="Add" OnClick="btn_AddData_Click" />
                    <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" OnClick="btn_Close_Click" />
                </td>

            </tr>
            <tr>
                <td class="FV270" colspan="3">
                    <div id="DivMessageMaterial" runat="server" visible="false">
                    </div>
                </td>
            </tr>
        </table>

    </asp:Panel>

    <!--Pop Revise-->
    <asp:ModalPopupExtender ID="ModalRevise" runat="server" TargetControlID="btn_ToRevise"
        Drag="true" PopupControlID="PanelRevise" CancelControlID="btn_CloseRevise" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelRevise" runat="server" BackColor="WhiteSmoke" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: none; top: 684px; left: 39px; width: 50%;">
        <br />
        <asp:HiddenField ID="HiddenField1" runat="server" />

        <table>
            <asp:Panel ID="Pnl_Revisi" runat="server" Visible="true">
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_revisi" runat="server" Text="Revise: " Width="170px"></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_revisi" runat="server" TextMode="MultiLine" Rows="4" Width="300px"></asp:TextBox>
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td class="FN150"></td>
                <td class="FV270" colspan="3">
                    <asp:Button ID="btn_Revise" runat="server" Text="Revise" Visible="true" OnClick="btn_Revise_Click" OnClientClick="Confirm()" />

                    <asp:Button ID="btn_CloseRevise" runat="server" Text="Close" Width="100px" OnClick="btn_CloseRevise_Click" />
                </td>

            </tr>
        </table>

    </asp:Panel>

    <!--Pop Revise Design-->
    <asp:ModalPopupExtender ID="ModalReviseDesign" runat="server" TargetControlID="btn_ToReviseDesign"
        Drag="true" PopupControlID="PanelReviseDesign" CancelControlID="btn_CloseReviseDesign" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelReviseDesign" runat="server" BackColor="WhiteSmoke" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: none; top: 684px; left: 39px; width: 50%;">
        <br />
        <asp:HiddenField ID="HiddenField2" runat="server" />

        <table>
            <asp:Panel ID="Pnl_RevisiDesign" runat="server" Visible="true">
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_revisidesign" runat="server" Text="Revisi Design: " Width="170px"></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_revisidesign" runat="server" TextMode="MultiLine" Rows="4" Width="300px"></asp:TextBox>
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td class="FN150"></td>
                <td class="FV270" colspan="3">
                    <asp:Button ID="btn_ReviseDesign" runat="server" Text="Revise Design" Visible="true" OnClick="btn_ReviseDesign_Click" OnClientClick="Confirm()" />
                    <asp:Button ID="btn_CloseReviseDesign" runat="server" Text="Close" Width="100px" OnClick="btn_CloseReviseDesign_Click" />
                </td>

            </tr>
        </table>

    </asp:Panel>

    <%--    <asp:SqlDataSource ID="C_GridGraphicDesigner" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT users.*, users.USERNAME + '-' +brand.BRAND As UserCustom
                        FROM MS_USER As users
                        LEFT OUTER JOIN BRAND As brand On users.KD_BRAND = brand.KD_BRAND
                        WHERE KD_JABATAN = 'GD'"></asp:SqlDataSource>--%>

    <asp:SqlDataSource ID="C_GridGraphicDesigner" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT users.*, users.USERNAME AS UserCustom FROM MS_USER As users
                        WHERE KD_JABATAN = 'GD'"></asp:SqlDataSource>

    <div id="HiddenFields">
        <asp:HiddenField ID="HfNO_FORM" runat="server" />
        <asp:HiddenField ID="HfUsername" runat="server" />
        <asp:HiddenField ID="HfID_DEPT" runat="server" />
        <asp:HiddenField ID="HfStatusVer" runat="server" />
        <asp:HiddenField ID="HfPhotoGrapher" runat="server" />
        <asp:HiddenField ID="HfActionGeneral" runat="server" />
    </div>

</asp:Content>
