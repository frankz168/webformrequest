<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="FormRequestRepair.aspx.cs" Inherits="WebDelamiFormRequest.Menu_Forms_Detail.FormRequestRepair" %>

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
    <link rel="stylesheet" type="text/css" href="assets/css/style.css">
    <link rel="stylesheet" type="text/css" href="Content/Site.css">
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
            <h1>DATA SEMUA FORM REQUEST REPAIR</h1>
            <hr />
            <div id="DivMessage" runat="server" visible="false">
            </div>

            <table>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_department" runat="server" Text="Department : "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:DropDownList ID="ddldepartment" runat="server" Width="206px" Height="30px" DataSourceID="CT_GridMS_DEPT" DataValueField="KODE_DEPT" DataTextField="DEPT" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" Enabled="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_brand" runat="server" Text="Brand : "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:DropDownList ID="ddlbrand" runat="server" Width="206px" Height="30px" DataSourceID="CT_GridBRAND" DataValueField="KD_BRAND" DataTextField="BRAND" AutoPostBack="true" OnSelectedIndexChanged="ddlbrand_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_headdepartment" runat="server" Text="Head Department : "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:DropDownList ID="ddlheaddepartment" runat="server" Width="206px" Height="30px" DataSourceID="CT_GridUserHeadDepartment" DataValueField="USERNAME" DataTextField="USERNAME" Enabled="false">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div class="EU_TableScroll" style="display: block; max-height: 1000px;">
                <asp:GridView ID="gvMain" runat="server" AllowPaging="True" PagerSettings-Position="TopAndBottom" ShowHeaderWhenEmpty="true"
                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AutoGenerateColumns="False"
                    DataKeyNames="KODE_FORM" EmptyDataText="- No Data Found -" OnPageIndexChanging="gvMainPageChanging" DataSourceID="C_Grid" OnRowCommand="gvMainRowCommand">
                    <Columns>
                        <asp:BoundField DataField="KODE_FORM" HeaderText="Kode Form" SortExpression="KODE_FORM" Visible="false" />
                        <asp:BoundField DataField="KODE_FORM" HeaderText="Kode Form" />
                        <asp:BoundField DataField="NM_FORM" HeaderText="Nama Form" />
                        <asp:TemplateField ShowHeader="False" HeaderText="Action">
                            <ItemTemplate>
                                <div>
                                    <asp:Button ID="btn_click" runat="server" Text="Click To Create This Form" CommandName="ClickRow" />
                                </div>
                                <div>
                                    <asp:Button ID="btn_clickflowchart" runat="server" Text="Click To View FlowChart" CommandName="ClickRowFlowChart" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>

        </div>
        <br />
    </asp:Panel>
    <div id="DataSources">
        <asp:SqlDataSource ID="C_Grid" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="SELECT * FROM MS_FORM WHERE KODE_FORM ='FRM-0005'"></asp:SqlDataSource>

        <asp:SqlDataSource ID="CT_GridMS_DEPT" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="SELECT * FROM MS_DEPT"></asp:SqlDataSource>
        <asp:SqlDataSource ID="CT_GridBRAND" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="SELECT * FROM BRAND"></asp:SqlDataSource>
        <asp:SqlDataSource ID="CT_GridUserHeadDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="SELECT users.*, userhandle.KD_JABATAN 
                            FROM MS_USER As users
                            INNER JOIN MS_USER_HANDLE As userhandle On users.KD_JABATAN = userhandle.KD_JABATAN
                            WHERE
                            userhandle.KD_JABATAN ='HDP'"></asp:SqlDataSource>
    </div>

    <div id="HiddenFields">
        <asp:HiddenField ID="HfUsername" runat="server" />
        <asp:HiddenField ID="HfKodeForm" runat="server" />
        <asp:HiddenField ID="HfID_DEPT" runat="server" />
    </div>

</asp:Content>
