<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Master_Brand.aspx.cs" MasterPageFile="~/Site.Master" Inherits="WebDelamiFormRequest.Forms_Data_Master.Master_Brand" %>

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
            <h1>MASTER BRAND</h1>
            <hr />
            <div id="DivMessage" runat="server" visible="false">
            </div>

            <div id="C_PnlInput" runat="server" visible="false">

                <table>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_kodebrand" runat="server" Text="Kode Brand : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_kodebrand" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_brand" runat="server" Text="Brand : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_brand" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                </table>

                <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" />
                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClick="btn_cancel_Click" />
                <asp:Button ID="btn_delete" runat="server" Text="Delete" OnClick="btn_delete_Click" />
                <asp:Button ID="btn_yes" runat="server" Text="Yes" OnClick="btn_yes_Click" Visible="false" />
                <asp:Button ID="btn_no" runat="server" Text="No" OnClick="btn_no_Click" Visible="false" />
            </div>

            <br />

            <div id="C_GridPanel" runat="server">
                <asp:Button ID="btn_new" runat="server" Text="Create New" OnClick="btn_new_Click" />

                <div class="EU_TableScroll" style="display: block; max-height: 1000px;">
                    <asp:GridView ID="gvMain" runat="server" AllowPaging="True" PagerSettings-Position="TopAndBottom" ShowHeaderWhenEmpty="True"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AutoGenerateColumns="False"
                        DataKeyNames="KD_BRAND" EmptyDataText="- No Data Found -" OnPageIndexChanging="gvMain_PageIndexChanging" DataSourceID="C_Grid" OnRowCommand="gvMainRowCommand">
                        <Columns>
                            <asp:BoundField DataField="KD_BRAND" HeaderText="Kode Brand" />
                            <asp:BoundField DataField="BRAND" HeaderText="Jabatan" />
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
                                        <asp:Button ID="btn_view" runat="server" Text="View Data" CommandName="ClickRow" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="C_Grid" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="SELECT * FROM BRAND"></asp:SqlDataSource>
                </div>
            </div>

        </div>

        <br />
    </asp:Panel>

    <div id="HiddenFields">
        <asp:HiddenField ID="HfCode" runat="server" />
        <asp:HiddenField ID="HfUsername" runat="server" />
    </div>

</asp:Content>


