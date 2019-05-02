<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="I_FormRequest_Repair.aspx.cs" MasterPageFile="~/Site.Master" Inherits="WebDelamiFormRequest.Forms_Data_Process.I_FormRequest_Repair" %>

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

        function SetTarget() {

            document.forms[0].target = "_blank";

        }

    </script>

    <script type="text/javascript">

        function multiplyCalc() {
            var budget1 = document.getElementById('<%= text_budgets1.ClientID%>');
            var budget2 = document.getElementById('<%= text_budgets2.ClientID%>');
            var budget3 = document.getElementById('<%= text_budgets3.ClientID%>');
            var budget4 = document.getElementById('<%= text_budgets4.ClientID%>');
            var budget5 = document.getElementById('<%= text_budgets5.ClientID%>');
            var budget6 = document.getElementById('<%= text_budgets6.ClientID%>');
            var budget7 = document.getElementById('<%= text_budgets7.ClientID%>');
            var budget8 = document.getElementById('<%= text_budgets8.ClientID%>');
            var budget9 = document.getElementById('<%= text_budgets9.ClientID%>');
            var budget10 = document.getElementById('<%= text_budgets10.ClientID%>');
            var totalbudgets = document.getElementById('<%= text_totalbudget.ClientID%>');
            var b1 = 0, b2 = 0, b3 = 0, b4 = 0, b5 = 0, b6 = 0, b7 = 0, b8 = 0, b9 = 0, b10 = 0;

            if (budget1.value != "") b1 = budget1.value;
            if (budget2.value != "") b2 = budget2.value;
            if (budget3.value != "") b3 = budget3.value;
            if (budget4.value != "") b4 = budget4.value;
            if (budget5.value != "") b5 = budget5.value;
            if (budget6.value != "") b6 = budget6.value;
            if (budget7.value != "") b7 = budget7.value;
            if (budget8.value != "") b8 = budget8.value;
            if (budget9.value != "") b9 = budget9.value;
            if (budget10.value != "") b10 = budget10.value;

            totalbudgets.value = parseFloat(b1) + parseFloat(b2) + parseFloat(b3) + parseFloat(b4) + parseFloat(b5) + parseFloat(b6) + parseFloat(b7) + parseFloat(b8) + parseFloat(b9) + parseFloat(b10);

        }

    </script>

    <script type="text/javascript">

        function MultiplyCalc(s, e) {
            var budget1 = parseFloat(Budget1.GetInputElement().value.replace(',', ''));
            var budget2 = parseFloat(Budget2.GetInputElement().value.replace(',', ''));
            var budget3 = parseFloat(Budget3.GetInputElement().value.replace(',', ''));
            var budget4 = parseFloat(Budget4.GetInputElement().value.replace(',', ''));
            var budget5 = parseFloat(Budget5.GetInputElement().value.replace(',', ''));
            var budget6 = parseFloat(Budget6.GetInputElement().value.replace(',', ''));
            var budget7 = parseFloat(Budget7.GetInputElement().value.replace(',', ''));
            var budget8 = parseFloat(Budget8.GetInputElement().value.replace(',', ''));
            var budget9 = parseFloat(Budget9.GetInputElement().value.replace(',', ''));
            var budget10 = parseFloat(Budget9.GetInputElement().value.replace(',', ''));
            var b1 = 0, b2 = 0, b3 = 0, b4 = 0, b5 = 0, b6 = 0, b7 = 0, b8 = 0, b9 = 0, b10 = 0;

            if (budget1.value != "") b1 = budget1;
            if (budget2.value != "") b2 = budget2;
            if (budget3.value != "") b3 = budget3;
            if (budget4.value != "") b4 = budget4;
            if (budget5.value != "") b5 = budget5;
            if (budget6.value != "") b6 = budget6;
            if (budget7.value != "") b7 = budget7;
            if (budget8.value != "") b8 = budget8;
            if (budget9.value != "") b9 = budget9;
            if (budget10.value != "") b10 = budget10;

            var totalbudgets = b1 + b2 + b3 + b4 + b5 + b6 + b7 + b8 + b9 + b10;

            //if (!isNaN(budget1)) {
            //    Budget1.SetValue(budget1);
            //}
            //else {
            //    Budget1.SetValue(0);

            //}

            //if (!isNaN(budget2)) {
            //    Budget2.SetValue(budget2);
            //}
            //else {
            //    Budget2.SetValue(0);

            //}

            //if (!isNaN(budget3)) {
            //    Budget3.SetValue(budget3);
            //}
            //else {
            //    Budget3.SetValue(0);

            //}

            //if (!isNaN(budget4)) {
            //    Budget4.SetValue(budget4);
            //}
            //else {
            //    Budget4.SetValue(0);

            //}

            //if (!isNaN(budget5)) {
            //    Budget5.SetValue(budget5);
            //}
            //else {
            //    Budget5.SetValue(0);

            //}

            //if (!isNaN(budget6)) {
            //    Budget6.SetValue(budget6);
            //}
            //else {
            //    Budget6.SetValue(0);

            //}

            //if (!isNaN(budget7)) {
            //    Budget7.SetValue(budget7);
            //}
            //else {
            //    Budget7.SetValue(0);

            //}

            //if (!isNaN(budget8)) {
            //    Budget8.SetValue(budget8);
            //}
            //else {
            //    Budget8.SetValue(0);

            //}

            //if (!isNaN(budget9)) {
            //    Budget9.SetValue(budget9);
            //}
            //else {
            //    Budget9.SetValue(0);

            //}

            //if (!isNaN(budget10)) {
            //    Budget10.SetValue(budget10);
            //}
            //else {
            //    Budget10.SetValue(0);

            //}

            if (!isNaN(totalbudgets)) {
                Totalbudgets.SetValue(totalbudgets);
            }
            else {
                Totalbudgets.SetValue(0);

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

            <h1>FORM REQUEST - REPAIR</h1>
            <hr />
            <br />
            <asp:Button ID="btn_NewStoreDesign" runat="server" Text="New Store Design" Visible="false" OnClick="btn_NewStoreDesign_Click" OnClientClick = "SetTarget();" />
            <asp:Button ID="btn_ViewStoreDesign" runat="server" Text="View Store Design" Visible="false" OnClick="btn_ViewStoreDesign_Click" />
            <div id="DivMessage" runat="server" visible="false">
            </div>

            <br />

            <table>
                <asp:Panel ID="Pnl_Forms" runat="server">
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
                            <asp:TextBox ID="text_noform" runat="server" ReadOnly="true" Font-Bold="true" ForeColor="DodgerBlue" Width="250px"></asp:TextBox>
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
                            <asp:Label ID="label_brand" runat="server" Text="Brand: (*)" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_brand" runat="server" Font-Bold="true" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_requestfor" runat="server" Text="Request For" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="3">
                            <asp:RadioButton ID="radio_relocation" runat="server" GroupName="radio" />
                            <asp:Label ID="label_relocation" runat="server" Text="Relocation Counter" Font-Size="Small"></asp:Label>
                            <asp:RadioButton ID="radio_renovation" runat="server" GroupName="radio" />
                            <asp:Label ID="label_renovation" runat="server" Text="Renovation Counter" Font-Size="Small"></asp:Label>
                            <asp:RadioButton ID="radio_repair" runat="server" GroupName="radio" />
                            <asp:Label ID="label_repair" runat="server" Text="Repair/Maintenance" Font-Size="Small"></asp:Label>
                            <asp:RadioButton ID="radio_additional" runat="server" GroupName="radio" />
                            <asp:Label ID="label_additional" runat="server" Text="Additional" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_picrequester" runat="server" Text="PIC Requester: (*)" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_picrequester" runat="server" Font-Bold="true" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_jenis" runat="server" Text="Store Type Request: "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:DropDownList ID="ddljenis" runat="server" Width="206px" Height="30px" AutoPostBack="true" OnSelectedIndexChanged="ddljenis_SelectedIndexChanged1">
                                <asp:ListItem Text="Building" Value="BUILDING" Selected="True" />
                                <asp:ListItem Text="Store" Value="STORE" />
                                <asp:ListItem Text="Counter" Value="COUNTER" />
                                <asp:ListItem Text="Bazzar" Value="BAZZAR" />
                                <asp:ListItem Text="RFS" Value="RFS" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_depstoremall" runat="server" Text="Dept.Store / Mall: "></asp:Label>
                        </td>
                        <td class="FV270" colspan="6">
                            <div class="EU_TableScrollCustom" style="display: block; max-height: 200px; max-width: 1750px; width: 750px">
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

                <asp:Panel ID="Pnl_PermintaanPerbaikan" runat="server">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_permintaanperbaikan" runat="server" Text="Repair Request (*) 1." Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="13">
                            <asp:Label ID="label_descriptionrepair1" runat="server" Text="Description Of Repair" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_permintaanperbaikan1" runat="server" Width="260px" BackColor="Yellow"></asp:TextBox>
                            <asp:Label ID="label_descriptionrepair2_1" runat="server" Text="Description Of Repair 2" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_descriptionrepair2_1" runat="server" Width="200px" BackColor="LightGreen"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_picselected1" runat="server" Text="PIC Selected" Font-Size="XX-Small"></asp:Label>
                            <asp:DropDownList ID="ddlpicperbaikan1" runat="server" Width="280px" DataSourceID="CT_GridUser" DataValueField="USERNAME" DataTextField="USERNAME" BackColor="LightGreen">
                            </asp:DropDownList>
                            <%--                   <asp:TextBox ID="text_picperbaikan1" runat="server" Width="280px" BackColor="LightGreen"></asp:TextBox>--%>
                            <asp:Label ID="label_budgets1" runat="server" Text="Budget" Font-Size="XX-Small"></asp:Label>
                            <dx:ASPxSpinEdit ID="text_budgets1" runat="server" Width="130px" MinValue="0" MaxValue="999999999" DisplayFormatString="#,#0.##" Font-Bold="true" Font-Size="Small" Theme="DevEx" CssClass="InputButton" BackColor="LightGreen" ClientInstanceName="Budget1">
                                <ClientSideEvents KeyUp="MultiplyCalc" />
                            </dx:ASPxSpinEdit>
                            <%--  <asp:TextBox ID="text_budgets1" runat="server" Width="100px" onkeyup="javascript:MultiplyCalc();" BackColor="LightGreen" TextMode="Number"></asp:TextBox>--%>
                            <br />

                            <asp:Label ID="label_completedate1" runat="server" Text="Complete Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_completedate1" runat="server" Width="130px" TextMode="Date" BackColor="LightGreen"></asp:TextBox>
                            <asp:Label ID="label_actualfinishdate1" runat="server" Text="Actual Finish Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_actualfinishdate1" runat="server" TextMode="Date" Width="130px" BackColor="Orange"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_uploadfile1" runat="server" Text="Upload File" Font-Size="XX-Small"></asp:Label>
                            <asp:FileUpload ID="btn_uploadfile1" runat="server" Text="Upload File 1" Font-Size="X-Small" Width="200px" BackColor="Yellow" />
                            <asp:LinkButton ID="linkbtn_filename1" runat="server" Text="-" OnClick="linkbtn_filename1_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" colspan="4">
                            <hr runat="server" id="hr4" style="height: 5px; background-color: black" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_permintaan2" runat="server" Text="2. " Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:Label ID="label_descriptionrepair2" runat="server" Text="Description Of Repair" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_permintaanperbaikan2" runat="server" Width="260px" BackColor="Yellow"></asp:TextBox>
                            <asp:Label ID="label_descriptionrepair2_2" runat="server" Text="Description Of Repair 2" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_descriptionrepair2_2" runat="server" Width="200px" BackColor="LightGreen"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_picselected2" runat="server" Text="PIC Selected" Font-Size="XX-Small"></asp:Label>
                            <asp:DropDownList ID="ddlpicperbaikan2" runat="server" Width="280px" DataSourceID="CT_GridUser" DataValueField="USERNAME" DataTextField="USERNAME" BackColor="LightGreen">
                            </asp:DropDownList>
                            <%--                 <asp:TextBox ID="text_picperbaikan2" runat="server" Width="280px" BackColor="LightGreen">
                            </asp:TextBox>--%>
                            <asp:Label ID="label_budgets2" runat="server" Text="Budget" Font-Size="XX-Small"></asp:Label>
                            <dx:ASPxSpinEdit ID="text_budgets2" runat="server" Width="130px" MinValue="0" MaxValue="999999999" DisplayFormatString="#,#0.##" Font-Bold="true" Font-Size="Small" Theme="DevEx" CssClass="InputButton" BackColor="LightGreen" ClientInstanceName="Budget2">
                                <ClientSideEvents KeyUp="MultiplyCalc" />
                            </dx:ASPxSpinEdit>
                            <br />

                            <asp:Label ID="label_completedate2" runat="server" Text="Complete Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_completedate2" runat="server" Width="130px" TextMode="Date" BackColor="LightGreen"></asp:TextBox>
                            <asp:Label ID="label_actualfinishdate2" runat="server" Text="Actual Finish Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_actualfinishdate2" runat="server" TextMode="Date" Width="130px" BackColor="Orange"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_uploadfile2" runat="server" Text="Upload File" Font-Size="XX-Small"></asp:Label>
                            <asp:FileUpload ID="btn_uploadfile2" runat="server" Text="Upload File 2" Font-Size="X-Small" Width="200px" BackColor="Yellow" />
                            <asp:LinkButton ID="linkbtn_filename2" runat="server" Text="-" OnClick="linkbtn_filename2_Click"></asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%" colspan="4">
                            <hr runat="server" id="hr5" style="height: 5px; background-color: black" />
                        </td>
                    </tr>

                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_permintaan3" runat="server" Text="3. " Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:Label ID="label_descriptionrepair3" runat="server" Text="Description Of Repair" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_permintaanperbaikan3" runat="server" Width="260px" BackColor="Yellow"></asp:TextBox>
                            <asp:Label ID="label_descriptionrepair2_3" runat="server" Text="Description Of Repair 2" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_descriptionrepair2_3" runat="server" Width="200px" BackColor="LightGreen"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_picselected3" runat="server" Text="PIC Selected" Font-Size="XX-Small"></asp:Label>
                            <asp:DropDownList ID="ddlpicperbaikan3" runat="server" Width="280px" DataSourceID="CT_GridUser" DataValueField="USERNAME" DataTextField="USERNAME" BackColor="LightGreen">
                            </asp:DropDownList>
                            <%--         <asp:TextBox ID="text_picperbaikan3" runat="server" Width="280px" BackColor="LightGreen">
                            </asp:TextBox>--%>
                            <asp:Label ID="label_budgets3" runat="server" Text="Budget" Font-Size="XX-Small"></asp:Label>
                            <dx:ASPxSpinEdit ID="text_budgets3" runat="server" Width="130px" MinValue="0" MaxValue="999999999" DisplayFormatString="#,#0.##" Font-Bold="true" Font-Size="Small" Theme="DevEx" CssClass="InputButton" BackColor="LightGreen" ClientInstanceName="Budget3">
                                <ClientSideEvents KeyUp="MultiplyCalc" />
                            </dx:ASPxSpinEdit>
                            <br />

                            <asp:Label ID="label_completedate3" runat="server" Text="Complete Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_completedate3" runat="server" Width="130px" TextMode="Date" BackColor="LightGreen"></asp:TextBox>
                            <asp:Label ID="label_actualfinishdate3" runat="server" Text="Actual Finish Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_actualfinishdate3" runat="server" TextMode="Date" Width="130px" BackColor="Orange"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_uploadfile3" runat="server" Text="Upload File" Font-Size="XX-Small"></asp:Label>
                            <asp:FileUpload ID="btn_uploadfile3" runat="server" Text="Upload File 3" Font-Size="X-Small" Width="200px" BackColor="Yellow" />
                            <asp:LinkButton ID="linkbtn_filename3" runat="server" Text="-" OnClick="linkbtn_filename3_Click"></asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%" colspan="4">
                            <hr runat="server" id="hr6" style="height: 5px; background-color: black" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_permintaan4" runat="server" Text="4. " Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:Label ID="label_descriptionrepair4" runat="server" Text="Description Of Repair" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_permintaanperbaikan4" runat="server" Width="260px" BackColor="Yellow"></asp:TextBox>
                            <asp:Label ID="label_descriptionrepair2_4" runat="server" Text="Description Of Repair 2" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_descriptionrepair2_4" runat="server" Width="200px" BackColor="LightGreen"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_picselected4" runat="server" Text="PIC Selected" Font-Size="XX-Small"></asp:Label>
                            <asp:DropDownList ID="ddlpicperbaikan4" runat="server" Width="280px" DataSourceID="CT_GridUser" DataValueField="USERNAME" DataTextField="USERNAME" BackColor="LightGreen">
                            </asp:DropDownList>
                            <%--           <asp:TextBox ID="text_picperbaikan4" runat="server" Width="280px" BackColor="LightGreen">
                            </asp:TextBox>--%>
                            <asp:Label ID="label_budgets4" runat="server" Text="Budget" Font-Size="XX-Small"></asp:Label>
                            <dx:ASPxSpinEdit ID="text_budgets4" runat="server" Width="130px" MinValue="0" MaxValue="999999999" DisplayFormatString="#,#0.##" Font-Bold="true" Font-Size="Small" Theme="DevEx" CssClass="InputButton" BackColor="LightGreen" ClientInstanceName="Budget4">
                                <ClientSideEvents KeyUp="MultiplyCalc" />
                            </dx:ASPxSpinEdit>
                            <br />

                            <asp:Label ID="label_completedate4" runat="server" Text="Complete Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_completedate4" runat="server" Width="130px" TextMode="Date" BackColor="LightGreen"></asp:TextBox>
                            <asp:Label ID="label_actualfinishdate4" runat="server" Text="Actual Finish Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_actualfinishdate4" runat="server" TextMode="Date" Width="130px" BackColor="Orange"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_uploadfile4" runat="server" Text="Upload File" Font-Size="XX-Small"></asp:Label>
                            <asp:FileUpload ID="btn_uploadfile4" runat="server" Text="Upload File 4" Font-Size="X-Small" Width="200px" BackColor="Yellow" />
                            <asp:LinkButton ID="linkbtn_filename4" runat="server" Text="-" OnClick="linkbtn_filename4_Click"></asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%" colspan="4">
                            <hr runat="server" id="hr7" style="height: 5px; background-color: black" />
                        </td>
                    </tr>

                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_permintaan5" runat="server" Text="5. " Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:Label ID="label_descriptionrepair5" runat="server" Text="Description Of Repair" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_permintaanperbaikan5" runat="server" Width="260px" BackColor="Yellow"></asp:TextBox>
                            <asp:Label ID="label_descriptionrepair2_5" runat="server" Text="Description Of Repair 2" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_descriptionrepair2_5" runat="server" Width="200px" BackColor="LightGreen"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_picselected5" runat="server" Text="PIC Selected" Font-Size="XX-Small"></asp:Label>
                            <asp:DropDownList ID="ddlpicperbaikan5" runat="server" Width="280px" DataSourceID="CT_GridUser" DataValueField="USERNAME" DataTextField="USERNAME" BackColor="LightGreen">
                            </asp:DropDownList>
                            <%--     <asp:TextBox ID="text_picperbaikan5" runat="server" Width="280px" BackColor="LightGreen">
                            </asp:TextBox>--%>
                            <asp:Label ID="label_budgets5" runat="server" Text="Budget" Font-Size="XX-Small"></asp:Label>
                            <dx:ASPxSpinEdit ID="text_budgets5" runat="server" Width="130px" MinValue="0" MaxValue="999999999" DisplayFormatString="#,#0.##" Font-Bold="true" Font-Size="Small" Theme="DevEx" CssClass="InputButton" BackColor="LightGreen" ClientInstanceName="Budget5">
                                <ClientSideEvents KeyUp="MultiplyCalc" />
                            </dx:ASPxSpinEdit>
                            <br />

                            <asp:Label ID="label_completedate5" runat="server" Text="Complete Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_completedate5" runat="server" Width="130px" TextMode="Date" BackColor="LightGreen"></asp:TextBox>
                            <asp:Label ID="label_actualfinishdate5" runat="server" Text="Actual Finish Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_actualfinishdate5" runat="server" TextMode="Date" Width="130px" BackColor="Orange"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_uploadfile5" runat="server" Text="Upload File" Font-Size="XX-Small"></asp:Label>
                            <asp:FileUpload ID="btn_uploadfile5" runat="server" Text="Upload File 5" Font-Size="X-Small" Width="200px" BackColor="Yellow" />
                            <asp:LinkButton ID="linkbtn_filename5" runat="server" Text="-" OnClick="linkbtn_filename5_Click"></asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%" colspan="4">
                            <hr runat="server" id="hr8" style="height: 5px; background-color: black" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_permintaan6" runat="server" Text="6. " Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:Label ID="label_descriptionrepair6" runat="server" Text="Description Of Repair" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_permintaanperbaikan6" runat="server" Width="260px" BackColor="Yellow"></asp:TextBox>
                            <asp:Label ID="label_descriptionrepair2_6" runat="server" Text="Description Of Repair 2" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_descriptionrepair2_6" runat="server" Width="200px" BackColor="LightGreen"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_picselected6" runat="server" Text="PIC Selected" Font-Size="XX-Small"></asp:Label>
                            <asp:DropDownList ID="ddlpicperbaikan6" runat="server" Width="280px" DataSourceID="CT_GridUser" DataValueField="USERNAME" DataTextField="USERNAME" BackColor="LightGreen">
                            </asp:DropDownList>
                            <%--        <asp:TextBox ID="text_picperbaikan6" runat="server" Width="280px" BackColor="LightGreen">
                            </asp:TextBox>--%>
                            <asp:Label ID="label_budgets6" runat="server" Text="Budget" Font-Size="XX-Small"></asp:Label>
                            <dx:ASPxSpinEdit ID="text_budgets6" runat="server" Width="130px" MinValue="0" MaxValue="999999999" DisplayFormatString="#,#0.##" Font-Bold="true" Font-Size="Small" Theme="DevEx" CssClass="InputButton" BackColor="LightGreen" ClientInstanceName="Budget6">
                                <ClientSideEvents KeyUp="MultiplyCalc" />
                            </dx:ASPxSpinEdit>
                            <br />

                            <asp:Label ID="label_completedate6" runat="server" Text="Complete Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_completedate6" runat="server" Width="130px" TextMode="Date" BackColor="LightGreen"></asp:TextBox>
                            <asp:Label ID="label_actualfinishdate6" runat="server" Text="Actual Finish Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_actualfinishdate6" runat="server" TextMode="Date" Width="130px" BackColor="Orange"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_uploadfile6" runat="server" Text="Upload File" Font-Size="XX-Small"></asp:Label>
                            <asp:FileUpload ID="btn_uploadfile6" runat="server" Text="Upload File 6" Font-Size="X-Small" Width="200px" BackColor="Yellow" />
                            <asp:LinkButton ID="linkbtn_filename6" runat="server" Text="-" OnClick="linkbtn_filename6_Click"></asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%" colspan="4">
                            <hr runat="server" id="hr9" style="height: 5px; background-color: black" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_permintaan7" runat="server" Text="7. " Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:Label ID="label_descriptionrepair7" runat="server" Text="Description Of Repair" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_permintaanperbaikan7" runat="server" Width="260px" BackColor="Yellow"></asp:TextBox>
                            <asp:Label ID="label_descriptionrepair2_7" runat="server" Text="Description Of Repair 2" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_descriptionrepair2_7" runat="server" Width="200px" BackColor="LightGreen"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_picselected7" runat="server" Text="PIC Selected" Font-Size="XX-Small"></asp:Label>
                            <asp:DropDownList ID="ddlpicperbaikan7" runat="server" Width="280px" DataSourceID="CT_GridUser" DataValueField="USERNAME" DataTextField="USERNAME" BackColor="LightGreen">
                            </asp:DropDownList>
                            <%--                <asp:TextBox ID="text_picperbaikan7" runat="server" Width="280px" BackColor="LightGreen">
                            </asp:TextBox>--%>
                            <asp:Label ID="label_budgets7" runat="server" Text="Budget" Font-Size="XX-Small"></asp:Label>
                            <dx:ASPxSpinEdit ID="text_budgets7" runat="server" Width="130px" MinValue="0" MaxValue="999999999" DisplayFormatString="#,#0.##" Font-Bold="true" Font-Size="Small" Theme="DevEx" CssClass="InputButton" BackColor="LightGreen" ClientInstanceName="Budget7">
                                <ClientSideEvents KeyUp="MultiplyCalc" />
                            </dx:ASPxSpinEdit>
                            <br />

                            <asp:Label ID="label_completedate7" runat="server" Text="Complete Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_completedate7" runat="server" Width="130px" TextMode="Date" BackColor="LightGreen"></asp:TextBox>
                            <asp:Label ID="label_actualfinishdate7" runat="server" Text="Actual Finish Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_actualfinishdate7" runat="server" TextMode="Date" Width="130px" BackColor="Orange"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_uploadfile7" runat="server" Text="Upload File" Font-Size="XX-Small"></asp:Label>
                            <asp:FileUpload ID="btn_uploadfile7" runat="server" Text="Upload File 7" Font-Size="X-Small" Width="200px" BackColor="Yellow" />
                            <asp:LinkButton ID="linkbtn_filename7" runat="server" Text="-" OnClick="linkbtn_filename7_Click"></asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%" colspan="4">
                            <hr runat="server" id="hr10" style="height: 5px; background-color: black" />
                        </td>
                    </tr>

                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_permintaan8" runat="server" Text="8. " Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:Label ID="label_descriptionrepair8" runat="server" Text="Description Of Repair" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_permintaanperbaikan8" runat="server" Width="260px" BackColor="Yellow"></asp:TextBox>
                            <asp:Label ID="label_descriptionrepair2_8" runat="server" Text="Description Of Repair 2" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_descriptionrepair2_8" runat="server" Width="200px" BackColor="LightGreen"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_picselected8" runat="server" Text="PIC Selected" Font-Size="XX-Small"></asp:Label>
                            <asp:DropDownList ID="ddlpicperbaikan8" runat="server" Width="280px" DataSourceID="CT_GridUser" DataValueField="USERNAME" DataTextField="USERNAME" BackColor="LightGreen">
                            </asp:DropDownList>
                            <%--                          <asp:TextBox ID="text_picperbaikan8" runat="server" Width="280px" BackColor="LightGreen">
                            </asp:TextBox>--%>
                            <asp:Label ID="label_budgets8" runat="server" Text="Budget" Font-Size="XX-Small"></asp:Label>
                            <dx:ASPxSpinEdit ID="text_budgets8" runat="server" Width="130px" MinValue="0" MaxValue="999999999" DisplayFormatString="#,#0.##" Font-Bold="true" Font-Size="Small" Theme="DevEx" CssClass="InputButton" BackColor="LightGreen" ClientInstanceName="Budget8">
                                <ClientSideEvents KeyUp="MultiplyCalc" />
                            </dx:ASPxSpinEdit>
                            <br />

                            <asp:Label ID="label_completedate8" runat="server" Text="Complete Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_completedate8" runat="server" Width="130px" TextMode="Date" BackColor="LightGreen"></asp:TextBox>
                            <asp:Label ID="label_actualfinishdate8" runat="server" Text="Actual Finish Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_actualfinishdate8" runat="server" TextMode="Date" Width="130px" BackColor="Orange"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_uploadfile8" runat="server" Text="Upload File" Font-Size="XX-Small"></asp:Label>
                            <asp:FileUpload ID="btn_uploadfile8" runat="server" Text="Upload File 8" Font-Size="X-Small" Width="200px" BackColor="Yellow" />
                            <asp:LinkButton ID="linkbtn_filename8" runat="server" Text="-" OnClick="linkbtn_filename8_Click"></asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%" colspan="4">
                            <hr runat="server" id="hr11" style="height: 5px; background-color: black" />
                        </td>
                    </tr>

                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_permintaan9" runat="server" Text="9. " Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:Label ID="label_descriptionrepair9" runat="server" Text="Description Of Repair" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_permintaanperbaikan9" runat="server" Width="260px" BackColor="Yellow"></asp:TextBox>
                            <asp:Label ID="label_descriptionrepair2_9" runat="server" Text="Description Of Repair 2" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_descriptionrepair2_9" runat="server" Width="200px" BackColor="LightGreen"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_picselected9" runat="server" Text="PIC Selected" Font-Size="XX-Small"></asp:Label>
                            <asp:DropDownList ID="ddlpicperbaikan9" runat="server" Width="280px" DataSourceID="CT_GridUser" DataValueField="USERNAME" DataTextField="USERNAME" BackColor="LightGreen">
                            </asp:DropDownList>
                            <%--                        <asp:TextBox ID="text_picperbaikan9" runat="server" Width="280px" BackColor="LightGreen">
                            </asp:TextBox>--%>
                            <asp:Label ID="label_budgets9" runat="server" Text="Budget" Font-Size="XX-Small"></asp:Label>
                            <dx:ASPxSpinEdit ID="text_budgets9" runat="server" Width="130px" MinValue="0" MaxValue="999999999" DisplayFormatString="#,#0.##" Font-Bold="true" Font-Size="Small" Theme="DevEx" CssClass="InputButton" BackColor="LightGreen" ClientInstanceName="Budget9">
                                <ClientSideEvents KeyUp="MultiplyCalc" />
                            </dx:ASPxSpinEdit>
                            <br />

                            <asp:Label ID="label_completedate9" runat="server" Text="Complete Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_completedate9" runat="server" Width="130px" TextMode="Date" BackColor="LightGreen"></asp:TextBox>
                            <asp:Label ID="label_actualfinishdate9" runat="server" Text="Actual Finish Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_actualfinishdate9" runat="server" TextMode="Date" Width="130px" BackColor="Orange"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_uploadfile9" runat="server" Text="Upload File" Font-Size="XX-Small"></asp:Label>
                            <asp:FileUpload ID="btn_uploadfile9" runat="server" Text="Upload File 9" Font-Size="X-Small" Width="200px" BackColor="Yellow" />
                            <asp:LinkButton ID="linkbtn_filename9" runat="server" Text="-" OnClick="linkbtn_filename9_Click"></asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%" colspan="4">
                            <hr runat="server" id="hr12" style="height: 5px; background-color: black" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_permintaan10" runat="server" Text="10. " Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:Label ID="label_descriptionrepair10" runat="server" Text="Description Of Repair" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_permintaanperbaikan10" runat="server" Width="260px" BackColor="Yellow"></asp:TextBox>
                            <asp:Label ID="label_descriptionrepair2_10" runat="server" Text="Description Of Repair 2" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_descriptionrepair2_10" runat="server" Width="200px" BackColor="LightGreen"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_picselected10" runat="server" Text="PIC Selected" Font-Size="XX-Small"></asp:Label>
                            <asp:DropDownList ID="ddlpicperbaikan10" runat="server" Width="280px" DataSourceID="CT_GridUser" DataValueField="USERNAME" DataTextField="USERNAME" BackColor="LightGreen">
                            </asp:DropDownList>
                            <%--                            <asp:TextBox ID="text_picperbaikan10" runat="server" Width="280px" BackColor="LightGreen">
                            </asp:TextBox>--%>
                            <asp:Label ID="label_budgets10" runat="server" Text="Budget" Font-Size="XX-Small"></asp:Label>
                            <dx:ASPxSpinEdit ID="text_budgets10" runat="server" Width="130px" MinValue="0" MaxValue="999999999" DisplayFormatString="#,#0.##" Font-Bold="true" Font-Size="Small" Theme="DevEx" CssClass="InputButton" BackColor="LightGreen" ClientInstanceName="Budget10">
                                <ClientSideEvents KeyUp="MultiplyCalc" />
                            </dx:ASPxSpinEdit>
                            <br />

                            <asp:Label ID="label_completedate10" runat="server" Text="Complete Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_completedate10" runat="server" Width="130px" TextMode="Date" BackColor="LightGreen"></asp:TextBox>
                            <asp:Label ID="label_actualfinishdate10" runat="server" Text="Actual Finish Date" Font-Size="XX-Small"></asp:Label>
                            <asp:TextBox ID="text_actualfinishdate10" runat="server" TextMode="Date" Width="130px" BackColor="Orange"></asp:TextBox>
                            <br />
                            <asp:Label ID="label_uploadfile10" runat="server" Text="Upload File" Font-Size="XX-Small"></asp:Label>
                            <asp:FileUpload ID="btn_uploadfile10" runat="server" Text="Upload File 10" Font-Size="X-Small" Width="200px" BackColor="Yellow" />
                            <asp:LinkButton ID="linkbtn_filename10" runat="server" Text="-" OnClick="linkbtn_filename10_Click"></asp:LinkButton>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%" colspan="4">
                            <hr runat="server" id="hr13" style="height: 5px; background-color: black" />
                        </td>
                    </tr>

                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_totalbudget" runat="server" Text="Total Budget: (*)" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <dx:ASPxSpinEdit ID="text_totalbudget" runat="server" Width="250px" Height="30px" MinValue="0" MaxValue="999999999" DisplayFormatString="#,#0.##" Font-Bold="true" Font-Size="Small" Theme="DevEx" ReadOnly="true" ClientInstanceName="Totalbudgets">
                                <ClientSideEvents KeyUp="MultiplyCalc" />
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_keterangan" runat="server" Text="Additional Information:" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:TextBox ID="text_keterangan" runat="server" Width="750px" TextMode="MultiLine" Columns="25" Rows="4"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="Pnl_Others1" runat="server">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_overbudgetvalue" runat="server" Text="Over Budget Value: (*)" Font-Size="Small" ForeColor="DodgerBlue"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <dx:ASPxSpinEdit ID="text_overbudgetvalue" runat="server" Width="250px" Height="30px" MinValue="0" MaxValue="999999999" DisplayFormatString="#,#0.##" Font-Bold="true" Font-Size="Small" Theme="DevEx"></dx:ASPxSpinEdit>
                            <%--                            <asp:TextBox ID="text_overbudgetvalue" runat="server" Width="250px" TextMode="Number"></asp:TextBox>--%>
                        </td>
                    </tr>

                    <asp:Panel ID="Pnl_OverBudget" runat="server" Visible="false">
                        <tr>
                            <td class="FN150">
                                <asp:Label ID="label_overbudget" runat="server" Text="Over Budget Request : "></asp:Label>
                            </td>
                            <td class="FV270">
                                <asp:DropDownList ID="ddloverbudget" runat="server" Width="206px" Height="30px">
                                    <asp:ListItem Text="Yes" Value="Yes" />
                                    <asp:ListItem Text="No" Value="No" Selected="True" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </asp:Panel>

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
                            <asp:Label ID="label_tanggaldibuat" runat="server" Text="Date Created Form:" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggaldibuat" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_menyetujui1" runat="server" Text="Operation Manager: " Width="170px"></asp:Label>
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
                            <asp:Label ID="label_diterima1" runat="server" Text="Adm.Project / Maintenance: " Width="170px"></asp:Label>
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
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_diterima2" runat="server" Text="Budget Control: " Width="210px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_diterima2" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalditerima2" runat="server" Text="Date Checker Budget:" Width="170px"></asp:Label>
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
                            <asp:Label ID="label_tanggalditerima3" runat="server" Text="Date Approved Form" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalditerima3" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_diterima4" runat="server" Text="PIC: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_diterima4" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalditerima4" runat="server" Text="Date Finish Repair:" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalditerima4" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>

                <tr>
                    <td style="width: 100%" colspan="4">
                        <hr runat="server" id="hr3" style="height: 5px; background-color: black" />
                    </td>
                </tr>

                <asp:Panel ID="Pnl_OverBudgetApprove" runat="server" Visible="true">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_diterimalain1" runat="server" Text="Brand Manager: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_diterimalain1" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalditerimalain1" runat="server" Text="Date Approved Over Budget:" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalditerimalain1" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_diterimalain2" runat="server" Text="Comercial Director : " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_diterimalain2" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalditerimalain2" runat="server" Text="Date Approved Over Budget:" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalditerimalain2" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
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
                            <asp:Button ID="btn_ToRevise" runat="server" Text="To Revise" Visible="false" OnClick="btn_ToRevise_Click" />
                            <asp:Button ID="btn_UpdateSubmit" runat="server" Text="Update And Submit" OnClick="btn_UpdateSubmit_Click" OnClientClick="Confirm()" />
                            <asp:Button ID="btn_Reject" runat="server" Text="Reject" OnClick="btn_Reject_Click" Visible="false" OnClientClick="Confirm()" />
                            <asp:Button ID="btn_Delete" runat="server" Text="Delete" OnClick="btn_Delete_Click" Visible="false" />
                        </td>
                    </tr>

                </asp:Panel>
            </table>
        </div>
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

            <tr>
                <td class="FV270" colspan="3">
                    <div id="DivMessageRevise" runat="server" visible="false">
                    </div>
                </td>
            </tr>
        </table>

    </asp:Panel>


    <asp:SqlDataSource ID="CT_GridUser" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand=" SELECT 'Choose' AS Username FROM MS_USER
                         UNION
                         SELECT  Username  FROM MS_USER WHERE ID_DEPT = '38' OR ID_DEPT = '39'"></asp:SqlDataSource>

    <div id="HiddenFields">
        <asp:HiddenField ID="HfNO_FORM" runat="server" />
        <asp:HiddenField ID="HfUsername" runat="server" />
        <asp:HiddenField ID="HfID_DEPT" runat="server" />
        <asp:HiddenField ID="HfRequiredDate" runat="server" />
        <asp:HiddenField ID="HfActionGeneral" runat="server" />
    </div>
</asp:Content>


