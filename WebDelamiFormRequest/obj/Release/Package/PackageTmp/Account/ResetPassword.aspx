<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" MasterPageFile="~/Site.Master" Inherits="WebDelamiFormRequest.Account.ResetPassword" %>

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
            <h1>RESET PASSWORD </h1>
            <hr />
            <div id="DivMessage" runat="server" visible="false">
            </div>

            <table>
                <tr>
                    <td class="FN150">
                        <label>Username</label>
                    </td>
                    <td class="FV520">
                        <asp:TextBox ID="text_Username" runat="server" CssClass="form-control span12" ReadOnly="true" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="text_Username" CssClass="field-validation-error" ErrorMessage="The user name field is required." ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td class="FN150">
                        <label>Password</label>
                    </td>
                    <td class="FV520">
                        <asp:TextBox ID="text_Password" runat="server" CssClass="form-controlspan12 form-control" TextMode="Password" />
                        <asp:Label ID="label_errorpassword" runat="server" Text="The password field is required." ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="text_Password" CssClass="field-validation-error" ErrorMessage="The password field is required." ForeColor="Red" />--%>
                    </td>

                </tr>
            </table>
            <br />

            <asp:Button ID="btn_resetpassword" runat="server" Text="Reset Password" OnClick="btn_resetpassword_Click" />
    </asp:Panel>
</asp:Content>

