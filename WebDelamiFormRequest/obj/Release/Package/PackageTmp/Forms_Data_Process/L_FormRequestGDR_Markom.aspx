<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="L_FormRequestGDR_Markom.aspx.cs" MasterPageFile="~/Site.Master" Inherits="WebDelamiFormRequest.Forms_Data_Process.L_FormRequestGDR_Markom" %>


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
            <h1>DATA SEMUA FORM REQUEST GDR</h1>
            <hr />
            <div id="DivMessage" runat="server" visible="false">
            </div>

            <table>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_department" runat="server" Text="Department : "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:Label ID="label_departmentvalue" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_brand" runat="server" Text="Brand : "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:Label ID="label_brandvalue" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_headdepartment" runat="server" Text="Head Department : "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:Label ID="label_headdepartment1value" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <%--  <tr>
                    <td class="FN150">
                        <asp:Label ID="label_headdepartment2" runat="server" Text="Head Department 2: "></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:Label ID="label_headdepartment2value" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>--%>
            </table>

            <br />

            <asp:Button ID="btn_newgdr" runat="server" Text="New Form Request GDR - Markom" OnClick="btn_newgdr_Click" />

            <div class="EU_TableScroll" style="display: block; max-height: 1000px;">
                <asp:GridView ID="gvMain" runat="server" AllowPaging="True" PagerSettings-Position="TopAndBottom" ShowHeaderWhenEmpty="True"
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
                                    <asp:Button ID="btn_view" runat="server" Text="View Data" CommandName="ClickRow" />
                                    <asp:Button ID="btn_viewactivity" runat="server" Text="View Activity Data" CommandName="ActivityRow" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="C_Grid" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="SELECT * FROM TR_FORM2_GDR WHERE DIBUAT = @USERNAME">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HfUsername" Name="USERNAME" PropertyName="Value" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>
        <br />
    </asp:Panel>

    <div id="HiddenFields">
        <asp:HiddenField ID="HfNO_FORM" runat="server" />
        <asp:HiddenField ID="HfUsername" runat="server" />
        <asp:HiddenField ID="HfUsername1" runat="server" />
        <asp:HiddenField ID="HfUsername2" runat="server" />
        <asp:HiddenField ID="HfBrandManager" runat="server" />
        <asp:HiddenField ID="HfHeadDesigner" runat="server" />
        <asp:HiddenField ID="HfCreativeManager" runat="server" />
        <asp:HiddenField ID="HfKODE_DEPT" runat="server" />
        <asp:HiddenField ID="HfKD_BRAND" runat="server" />
    </div>

</asp:Content>

