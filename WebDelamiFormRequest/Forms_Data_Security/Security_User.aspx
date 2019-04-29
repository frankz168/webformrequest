<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Security_User.aspx.cs" MasterPageFile="~/Site.Master" Inherits="WebDelamiFormRequest.Forms_Data_Security.Security_User" %>

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
            <h1>USER</h1>
            <hr />
            <div id="DivMessage" runat="server" visible="false">
            </div>

            <div id="C_PnlInput" runat="server" visible="false">

                <table>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_username" runat="server" Text="Username : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_username" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_fullname" runat="server" Text="Full Name : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_fullname" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_password" runat="server" Text="Password : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_password" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_kodedepartemen" runat="server" Text="Departemen : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:DropDownList ID="ddlkodedepartemen" runat="server" Width="206px" Height="30px" DataSourceID="CT_GridDept" DataValueField="KODE_DEPT" DataTextField="DEPT">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_kodebrand" runat="server" Text="Brand : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_kodebrand" runat="server" Width="250px"></asp:TextBox>
                            <asp:Label ID="label_ketkodebrand" runat="server" Text="Kode Brand dapat dilihat di Master Brand. Contoh Format pengisian(91,92)"></asp:Label>
                            <%--     <asp:DropDownList ID="ddlkodebrand" runat="server" Width="206px" Height="30px" DataSourceID="CT_GridBrand" DataValueField="KD_BRAND" DataTextField="BRAND">
                            </asp:DropDownList>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_kodejabatan" runat="server" Text="Jabatan : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:DropDownList ID="ddlkodejabatan" runat="server" Width="206px" Height="30px" DataSourceID="CT_GridJabatan" DataValueField="KD_JABATAN" DataTextField="JABATAN">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_email" runat="server" Text="Email : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_email" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_userprofileid" runat="server" Text="User Profile Id: "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:DropDownList ID="ddluserprofileid" runat="server" Width="206px" Height="30px" DataSourceID="CT_GridUserProfileId" DataValueField="UserProfileId" DataTextField="UserProfileId">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_status" runat="server" Text="Status : "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:DropDownList ID="ddlstatus" runat="server" Width="206px" Height="30px">
                                <asp:ListItem Text="Aktif" Value="1" Selected="True" />
                                <asp:ListItem Text="Tidak Aktif" Value="0" />
                            </asp:DropDownList>
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
                        DataKeyNames="USERNAME" EmptyDataText="- No Data Found -" OnPageIndexChanging="gvMain_PageIndexChanging" DataSourceID="C_Grid" OnRowCommand="gvMainRowCommand">
                        <Columns>
                            <asp:BoundField DataField="USERNAME" HeaderText="Username" />
                            <asp:BoundField DataField="FULL_NAME" HeaderText="Full Name" />
                            <asp:BoundField DataField="EMAIL" HeaderText="Email" />
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
                        SelectCommand="SELECT * FROM MS_USER"></asp:SqlDataSource>
                </div>
            </div>

        </div>

        <br />
    </asp:Panel>

    <asp:SqlDataSource ID="CT_GridDept" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT * FROM MS_DEPT"></asp:SqlDataSource>
    <asp:SqlDataSource ID="CT_GridBrand" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT * FROM BRAND"></asp:SqlDataSource>
    <asp:SqlDataSource ID="CT_GridJabatan" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT * FROM MS_JABATAN"></asp:SqlDataSource>
    <asp:SqlDataSource ID="CT_GridUserProfileId" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT * FROM MS_USER_PROFILE"></asp:SqlDataSource>

    <div id="HiddenFields">
        <asp:HiddenField ID="HfCode" runat="server" />
        <asp:HiddenField ID="HfUsername" runat="server" />
        <asp:HiddenField ID="HfID" runat="server" />
    </div>

</asp:Content>
