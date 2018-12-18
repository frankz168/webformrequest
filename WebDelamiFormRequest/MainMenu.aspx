<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MainMenu.aspx.cs" Inherits="WebDelamiFormRequest.MainMenu" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="PanelMain" runat="server">
        <div>
            <h1>MENU UTAMA</h1>
            <hr />
            <div id="DivMessage" runat="server" visible="false">
            </div>
        </div>
        <br />

        <asp:Button ID="btn_RequestForm" runat="server" Text="Request Form" OnClick="btn_RequestForm_Click" />
        <br />
        <asp:Button ID="btn_ReportStatus" runat="server" Text="Status Form-REPORT" OnClick="btn_ReportStatus_Click" />
    </asp:Panel>

    <div id="HiddenFields">
        <asp:HiddenField ID="HfUsername" runat="server" />
        <asp:HiddenField ID="HfID_DEPT" runat="server" />
        <asp:HiddenField ID="HfKDBRAND" runat="server" />
    </div>

</asp:Content>



