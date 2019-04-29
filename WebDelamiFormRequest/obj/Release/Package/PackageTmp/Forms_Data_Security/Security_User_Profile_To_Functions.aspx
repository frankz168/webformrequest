<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Security_User_Profile_To_Functions.aspx.cs" MasterPageFile="~/Site.Master" Inherits="WebDelamiFormRequest.Forms_Data_Security.Security_User_Profile_To_Functions" %>

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
            <h1>PROFILE FUNCTIONS</h1>
            <hr />
            <div id="DivMessage" runat="server" visible="false">
            </div>

            <div id="C_PnlInput" runat="server" visible="false">

                <table>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_userprofileid" runat="server" Text="Profile Id: "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_userprofileid" runat="server" Width="250px" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_functionid" runat="server" Text="Function Id : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_functionid" runat="server" Width="250px" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_permission" runat="server" Text="Permission : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:DropDownList ID="ddlpermission" runat="server" Width="206px" Height="30px">
                                <asp:ListItem Text="Full Access" Value="Full Access" Selected="True" />
                                <asp:ListItem Text="View Only" Value="View Only" />
                                <asp:ListItem Text="No Access" Value="No Access" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>

                <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" />
                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClick="btn_cancel_Click" />
            </div>


            <div id="C_GridPanel" runat="server">

                <div class="EU_TableScroll" style="display: block; max-height: 1000px;">
                    <asp:GridView ID="gvMain" runat="server" AllowPaging="True" PagerSettings-Position="TopAndBottom" ShowHeaderWhenEmpty="True"
                        CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AutoGenerateColumns="False"
                        DataKeyNames="Id" EmptyDataText="- No Data Found -" OnPageIndexChanging="gvMain_PageIndexChanging" DataSourceID="C_Grid" OnRowCommand="gvMainRowCommand">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                            <asp:BoundField DataField="UserProfileId" HeaderText="User Profile" />
                            <asp:BoundField DataField="FunctionId" HeaderText="Function Id" />
                            <asp:BoundField DataField="Permission" HeaderText="Permission" />
                            <asp:TemplateField ShowHeader="False" HeaderText="Action">
                                <ItemTemplate>
                                    <div>
                                        <asp:Button ID="btn_view" runat="server" Text="Change" CommandName="ClickRow" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="C_Grid" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        SelectCommand="SELECT * FROM [MS_USER_PROFILE_TO_FUNCTIONS] WHERE [UserProfileId] = @UserProfileId And (@ParentId = 'ALL' OR [FunctionId] In (Select [FunctionId] From [MS_FUNCTIONS] Where [ParentFunctionId] = @ParentId And [Active] = 'Yes')) ORDER BY [FunctionId]">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="UserProfileId" QueryStringField="UPID" />
                            <asp:QueryStringParameter Name="ParentId" QueryStringField="PRID" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>

        </div>

        <br />
    </asp:Panel>

    <div id="HiddenFields">
        <asp:HiddenField ID="HfCode" runat="server" />
        <asp:HiddenField ID="HfUpid" runat="server" />
        <asp:HiddenField ID="HfPrid" runat="server" />
        <asp:HiddenField ID="HfUsername" runat="server" />
    </div>

</asp:Content>



