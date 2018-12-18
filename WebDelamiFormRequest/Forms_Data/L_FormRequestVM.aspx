<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="L_FormRequestVM.aspx.cs" MasterPageFile="~/Site.Master" Inherits="WebDelamiFormRequest.Forms_Data.L_FormRequestVM" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="PanelMain" runat="server">
        <div>
            <h1>DATA SEMUA FORM REQUEST VM</h1>
            <hr />
            <div id="DivMessage" runat="server" visible="false">
            </div>

            <asp:Button ID="btn_newvm" runat="server" Text="New Form Request VM" OnClick="btn_newvm_Click" />

            <div class="EU_TableScroll" style="display: block; max-height: 1000px;">
                <asp:GridView ID="gvMain" runat="server" AllowPaging="True" PagerSettings-Position="TopAndBottom" ShowHeaderWhenEmpty="true"
                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AutoGenerateColumns="False"
                    DataKeyNames="NO_FORM" EmptyDataText="- No Data Found -" OnPageIndexChanging="gvMainPageChanging" DataSourceID="C_Grid" OnRowCommand="gvMainRowCommand">
                    <Columns>
                        <asp:BoundField DataField="NO_FORM" HeaderText="No Form" SortExpression="NO_FORM" Visible="false" />
                        <%-- <asp:TemplateField ShowHeader="False" HeaderText="Action">
                            <ItemTemplate>
                                <div>
                                    <asp:ImageButton ID="imgView" runat="server" CausesValidation="False" ToolTip="View Data Customer"
                                        CommandName="ViewRow" Style="width: 10px; height: 10px"
                                        ImageUrl="~/Images/checkmark.png" Text="View" />
                                    <asp:ImageButton ID="imgTransaction" runat="server" CausesValidation="False" ToolTip="View Data Transaction"
                                        CommandName="TransRow" Style="width: 10px; height: 10px"
                                        ImageUrl="~/Images/b_memo1.png" Text="View" />&nbsp;
                            <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" ToolTip="Edit Point Customer"
                                CommandName="EditRow" Style="width: 10px; height: 10px"
                                ImageUrl="~/Images/b_edit.png" Text="Edit" />
                                    &nbsp;<asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandName="DeleteRow" Visible="false"
                                        ImageUrl="~/Image/delete.png" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this user?');" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:BoundField DataField="NO_FORM" HeaderText="No Form" />
                        <asp:BoundField DataField="JENIS" HeaderText="Jenis" />
                        <asp:BoundField DataField="TGL_REQUEST" HeaderText="Tanggal Request" />
                        <asp:TemplateField ShowHeader="False" HeaderText="Action">
                            <ItemTemplate>
                                <div>
                                    <asp:Button ID="btn_view" runat="server" Text="View Data" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="C_Grid" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="SELECT * FROM TR_VM"></asp:SqlDataSource>
            </div>

            <br />
    </asp:Panel>
</asp:Content>

