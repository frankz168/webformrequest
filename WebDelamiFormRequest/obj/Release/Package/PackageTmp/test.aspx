<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="WebDelamiFormRequest.test" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #fff;
            border: 3px solid #ccc;
            padding: 10px;
            width: 300px;
        }
    </style>
</head>
<body style="height: 3000px">
    <form id="form1" runat="server">
        <div class="custom-file">
            <asp:FileUpload ID="FileUpload1" CssClass="custom-file-input" runat="server" />
            <label class="custom-file-label"></label>
        </div>
        <label id="filename"></label>
        <div class="input-group-append">
            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-outline-primary" Text="Upload" OnClick="btnUpload_Click" />
        </div>
    </form>
</body>
</html>
