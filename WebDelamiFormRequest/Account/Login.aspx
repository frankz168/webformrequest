<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebDelamiFormRequest.Account.Login" %>

<!doctype html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>Web Delami Form Request</title>
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,700' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" type="text/css" href="lib/bootstrap/css/bootstrap.css">
    <link rel="stylesheet" href="lib/font-awesome/css/font-awesome.css">

    <script src="lib/jquery-1.11.1.min.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" href="stylesheets/theme.css">
    <link rel="stylesheet" type="text/css" href="stylesheets/premium.css">

    <script type="text/javascript">

        function showPass() {
            var cookies = docum.cookie;
            var allcookie = cookies.split(";");
            for (ctr = 0; ctr < allcookie.lenght; ctr++) {
                var dt = allcookie[ctr];
                var str = dt.split("=");
                if (str[0].trim() == document.getElementById("text_Username").value.trim()) {
                    document.getElementById("text_Password").value = str[1];
                    break;
                }
            }

        }


        function showBoth() {
            var cookies = docum.cookie;
            var allcookie = cookies.split(";");
            for (c = 0; c < allcookie.lenght; c++) {
                var a = allcookie[c];
                var v = a.split("=");
                if (v[0].trim().split == "lastid")
                    document.getElementById("text_Username").value = v[1];
                if (v[0].trim().split == "lastpass")
                    document.getElementById("text_Password").value = v[1];
                break;
            }
        }
    </script>
</head>
<body class=" theme-blue" onload="showBoth()">

    <!-- Demo page code -->

    <script type="text/javascript">
        $(function () {
            var match = document.cookie.match(new RegExp('color=([^;]+)'));
            if (match) var color = match[1];
            if (color) {
                $('body').removeClass(function (index, css) {
                    return (css.match(/\btheme-\S+/g) || []).join(' ')
                })
                $('body').addClass('theme-' + color);
            }

            $('[data-popover="true"]').popover({ html: true });

        });
    </script>
    <style type="text/css">
        #line-chart {
            height: 300px;
            width: 800px;
            margin: 0px auto;
            margin-top: 1em;
        }

        .navbar-default .navbar-brand, .navbar-default .navbar-brand:hover {
            color: #fff;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            var uls = $('.sidebar-nav > ul > *').clone();
            uls.addClass('visible-xs');
            $('#main-menu').append(uls.clone());
        });
    </script>

    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->

    <!-- Le fav and touch icons -->
    <link rel="shortcut icon" href="../assets/ico/favicon.ico">
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="../assets/ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="../assets/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="../assets/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="../assets/ico/apple-touch-icon-57-precomposed.png">


    <!--[if lt IE 7 ]> <body class="ie ie6"> <![endif]-->
    <!--[if IE 7 ]> <body class="ie ie7 "> <![endif]-->
    <!--[if IE 8 ]> <body class="ie ie8 "> <![endif]-->
    <!--[if IE 9 ]> <body class="ie ie9 "> <![endif]-->
    <!--[if (gt IE 9)|!(IE)]><!-->

    <!--<![endif]-->

    <form id="Form1" runat="server">
        <div class="navbar navbar-default" role="navigation" style="height: 60px;">
            <div class="navbar-header">

                <a class="" href="Login.aspx"><span class="navbar-brand"><span>
                    <asp:Image ID="img_logo" runat="server" ImageUrl="~/Images/logo.png" Height="35px" />
                </span>
                    <asp:Label ID="label_logo" runat="server" Text="Web Delami Form Request" Font-Size="Medium" Font-Bold="true"></asp:Label></span></a>

            </div>

            <div class="navbar-collapse collapse" style="height: 1px;">
            </div>
        </div>

        <div class="dialog">
            <div class="panel panel-default">
                <p class="panel-heading no-collapse">
                    <asp:Label ID="label_login" runat="server" Text="Log In" ForeColor="White"></asp:Label>
                </p>
                <div class="panel-body">
                    <h2>
                        <asp:Label ID="lbWarning" runat="server" Text="This is warning!" Visible="false" Style="color: red;"></asp:Label>
                    </h2>
                    <div class="form-group">
                        <label>Username</label>
                        <asp:TextBox ID="text_Username" runat="server" CssClass="form-control span12" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="text_Username" CssClass="field-validation-error" ErrorMessage="The user name field is required." ForeColor="Red" />
                    </div>
                    <div class="form-group">
                        <label>Password</label>
                        <asp:TextBox ID="text_Password" runat="server" CssClass="form-controlspan12 form-control" TextMode="Password" onfocus="showPass()" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="text_Password" CssClass="field-validation-error" ErrorMessage="The password field is required." ForeColor="Red" />
                    </div>
                    <asp:Button ID="btnSignIn" runat="server" Text="Sign In" CssClass="btn btn-primary pull-right" OnClick="btnSignIn_Click" />

                    <asp:CheckBox ID="check_rememberme" runat="server" Text="Remember me" />

                    <%--   <label class="remember-me">
                        <input type="checkbox">
                        Remember me</label>--%>
                    <div class="clearfix"></div>
                </div>
            </div>
            <%--   <p class="pull-right" style=""><a href="http://www.portnine.com" target="blank" style="font-size: .75em; margin-top: .25em;">Design by Portnine</a></p>--%>
            <p>
                <a href="ForgotPassword.aspx">
                    <asp:LinkButton ID="btnForgotPassword" runat="server" OnClick="btnForgotPassword_Click">Forgot your password?</asp:LinkButton></a>
            </p>
        </div>
    </form>


    <script src="lib/bootstrap/js/bootstrap.js"></script>
    <script type="text/javascript">
        $("[rel=tooltip]").tooltip();
        $(function () {
            $('.demo-cancel-click').click(function () { return false; });
        });
    </script>


</body>
</html>
