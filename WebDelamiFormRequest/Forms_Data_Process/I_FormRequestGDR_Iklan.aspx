<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="I_FormRequestGDR_Iklan.aspx.cs" MasterPageFile="~/Site.Master" Inherits="WebDelamiFormRequest.Forms_Data_Process.I_FormRequestGDR_Iklan" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

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
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save / update data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Selects all enabled checkboxes
            $("#<%= checkbox_checkall.ClientID %>").click(function () {
                var checkedStatus = this.checked;
                $(".myCheckBox input[type='checkbox']").attr('checked', checkedStatus);
            });
        });
    </script>
    <script type="text/javascript">

        function move_up() {
            scroll_clipper.scrollTop = 0;
        }

    </script>
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

            <h1>FORM REQUEST - DIGITAL ADVERTISING</h1>
            <hr />
            <div id="DivMessage" runat="server" visible="false">
            </div>

            <br />

            <table>
                <asp:Panel ID="Pnl_Forms" runat="server">
                    <tr>

                        <td class="FN150">
                            <asp:Label ID="label_department" runat="server" Text="Department: (*)"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:Label ID="label_departmentvalue" runat="server" ReadOnly="true" Width="200px" ForeColor="DodgerBlue" Font-Bold="true"></asp:Label>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_status" runat="server" Text="Status: (*)"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:Label ID="label_statusvalue" runat="server" ReadOnly="true" Width="200px" Text="-" Font-Bold="true" ForeColor="DodgerBlue"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_noform" runat="server" Text="No Form: (*)"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_noform" runat="server" ReadOnly="true" Font-Bold="true" ForeColor="DodgerBlue" Width="250px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalrequest" runat="server" Text="Request Date: (*)"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalrequest" runat="server" TextMode="Date" Font-Bold="true" ForeColor="DodgerBlue" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_brand" runat="server" Text="Brand: (*)" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:CheckBox ID="checkbox_checkall" runat="server" Text="" Font-Size="xx-Small" CssClass="myCheckAll" />
                            <asp:Label ID="label_checkall" runat="server" Text="All" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:CheckBox ID="checkbox_colorbox" runat="server" Text="" Font-Size="xx-Small" CssClass="myCheckBox" />
                            <asp:Label ID="label_colorbox" runat="server" Text="ColorBox" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:CheckBox ID="checkbox_adidas" runat="server" Text="" Font-Size="xx-Small" CssClass="myCheckBox" />
                            <asp:Label ID="label_adidas" runat="server" Text="Adidas" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:CheckBox ID="checkbox_lecoq" runat="server" Text="" Font-Size="xx-Small" CssClass="myCheckBox" />
                            <asp:Label ID="label_lecoq" runat="server" Text="Le Coq" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:CheckBox ID="checkbox_jockey" runat="server" Text="" Font-Size="xx-Small" CssClass="myCheckBox" />
                            <asp:Label ID="label_jockey" runat="server" Text="Jockey" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:CheckBox ID="checkbox_tirajeans" runat="server" Text="" Font-Size="xx-Small" CssClass="myCheckBox" />
                            <asp:Label ID="label_tirajeans" runat="server" Text="Tira Jeans" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:CheckBox ID="checkbox_executive" runat="server" Text="" Font-Size="xx-Small" CssClass="myCheckBox" />
                            <asp:Label ID="label_executive" runat="server" Text="The Executive" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:CheckBox ID="checkbox_wood" runat="server" Text="" Font-Size="xx-Small" CssClass="myCheckBox" />
                            <asp:Label ID="label_wood" runat="server" Text="Wood" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:CheckBox ID="checkbox_wrangler" runat="server" Text="" Font-Size="xx-Small" CssClass="myCheckBox" />
                            <asp:Label ID="label_wrangler" runat="server" Text="Wrangler" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:CheckBox ID="checkbox_lee" runat="server" Text="" Font-Size="xx-Small" CssClass="myCheckBox" />
                            <asp:Label ID="label_lee" runat="server" Text="Lee" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:CheckBox ID="checkbox_etcetera" runat="server" Text="" Font-Size="xx-Small" CssClass="myCheckBox" />
                            <asp:Label ID="label_etcetera" runat="server" Text="Et Cetera" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_periodecampaignfrom" runat="server" Text="Periode Campaign : (*)" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_periodecampaignfrom" runat="server" TextMode="Date"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_periodecampaignto" runat="server" Text="To : (*)" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_periodecampaignto" runat="server" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_budget" runat="server" Text="Budget Value: (*)" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <dx:ASPxSpinEdit ID="text_budget" runat="server" Width="250px" Height="30px" MinValue="0" MaxValue="999999999" DisplayFormatString="#,#0.##" Font-Bold="true" Font-Size="Small" Theme="DevEx"></dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_jenisiklan" runat="server" Text="Type Of Request" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FN150">
                            <asp:RadioButton ID="radio_facebookinstagramadsjenis1" runat="server" />
                            <asp:Label ID="label_facebookinstagramjenis1" runat="server" Text="Facebook / Instagram Ads" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">
                            <asp:CheckBox ID="checkbox_sponsorjenis1" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_sponsorjenis1" runat="server" Text="Sponsored" Font-Size="Small"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="label_objectivejenis1" runat="server" Text="-Objective: " Font-Size="Small" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_trafficjenis1" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_trafficjenis1" runat="server" Text="Trafic" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_videojenis1" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_videojenis1" runat="server" Text="Video" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_conversionjenis1" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label1_conversionjenis1" runat="server" Text="Conversion" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_brandawarenesjenis1" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_brandawarenesjenis1" runat="server" Text="Brand Awarenes" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <br />
                            <br />
                        </td>

                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">
                            <asp:CheckBox ID="checkbox_boostpostjenis1" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_boostpostjenis1" runat="server" Text="Boost Post" Font-Size="Small"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="label2" runat="server" Text=" " Font-Size="Small" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_engagementjenis1" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_engagementjenis1" runat="server" Text="Engagement" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_leadjenis1" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_leadjenis1" runat="server" Text="Lead" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_reachjenis1" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_reachjenis1" runat="server" Text="Reach" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_dpajenis1" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_dpajenis1" runat="server" Text="DPA" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_otherjenis1" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_otherjenis1" runat="server" Text="Other" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <br />
                            <br />
                        </td>

                    </tr>

                    <tr>
                        <td class="FN150"></td>
                        <td class="FN150">
                            <asp:RadioButton ID="radio_googlejenis2" runat="server" />
                            <asp:Label ID="label_googlejenis2" runat="server" Text="Google" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">
                            <asp:CheckBox ID="checkbox_youtubeadsjenis2" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_youtubeadsjenis2" runat="server" Text="Youtube Ads" Font-Size="Small"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="label_objectivejenis2" runat="server" Text="-Objective: " Font-Size="Small" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_brandawarenesjenis2" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_brandawarenesjenis2" runat="server" Text="Trafic" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_productbrandsjenis2" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_productbrandsjenis2" runat="server" Text="Product & Brands Consideration" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">
                            <asp:CheckBox ID="checkbox_googledisplaynetworkjenis2" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_googledisplaynetworkjenis2" runat="server" Text="Google Display Network" Font-Size="Small"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">
                            <asp:CheckBox ID="checkbox_googleadwordsjenis2" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_googleadwordsjenis" runat="server" Text="Google Adwords" Font-Size="Small"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <br />
                            <br />
                        </td>

                    </tr>
                </asp:Panel>

                <asp:Panel ID="Pnl_UploadFileRequester" runat="server">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_fotoiklan" runat="server" Text="Picture Ads:" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">

                            <asp:FileUpload ID="btn_uploadfile1" runat="server" Text="Upload File 1" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename1" runat="server" Text="-" OnClick="linkbtn_filename1_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile2" runat="server" Text="Upload File 2" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename2" runat="server" Text="-" OnClick="linkbtn_filename2_Click"></asp:LinkButton>

                        </td>
                        <td class="FV270" colspan="2">
                            <asp:FileUpload ID="btn_uploadfile3" runat="server" Text="Upload File 3" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename3" runat="server" Text="-" OnClick="linkbtn_filename3_Click"></asp:LinkButton>

                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile4" runat="server" Text="Upload File 4" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename4" runat="server" Text="-" OnClick="linkbtn_filename4_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile5" runat="server" Text="Upload File 5" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename5" runat="server" Text="-" OnClick="linkbtn_filename5_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile6" runat="server" Text="Upload File 6" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename6" runat="server" Text="-" OnClick="linkbtn_filename6_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile7" runat="server" Text="Upload File 7" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename7" runat="server" Text="-" OnClick="linkbtn_filename7_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile8" runat="server" Text="Upload File 8" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename8" runat="server" Text="-" OnClick="linkbtn_filename8_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile9" runat="server" Text="Upload File 9" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename9" runat="server" Text="-" OnClick="linkbtn_filename9_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile10" runat="server" Text="Upload File 10" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename10" runat="server" Text="-" OnClick="linkbtn_filename10_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile11" runat="server" Text="Upload File 11" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename11" runat="server" Text="-" OnClick="linkbtn_filename11_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile12" runat="server" Text="Upload File 12" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename12" runat="server" Text="-" OnClick="linkbtn_filename12_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile13" runat="server" Text="Upload File 13" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename13" runat="server" Text="-" OnClick="linkbtn_filename13_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile14" runat="server" Text="Upload File 14" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename14" runat="server" Text="-" OnClick="linkbtn_filename14_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile15" runat="server" Text="Upload File 15" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename15" runat="server" Text="-" OnClick="linkbtn_filename15_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile16" runat="server" Text="Upload File 16" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename16" runat="server" Text="-" OnClick="linkbtn_filename16_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile17" runat="server" Text="Upload File 17" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename17" runat="server" Text="-" OnClick="linkbtn_filename17_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile18" runat="server" Text="Upload File 18" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename18" runat="server" Text="-" OnClick="linkbtn_filename18_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile19" runat="server" Text="Upload File 19" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename19" runat="server" Text="-" OnClick="linkbtn_filename19_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile20" runat="server" Text="Upload File 20" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename20" runat="server" Text="-" OnClick="linkbtn_filename20_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile21" runat="server" Text="Upload File 21" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename21" runat="server" Text="-" OnClick="linkbtn_filename21_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile22" runat="server" Text="Upload File 22" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename22" runat="server" Text="-" OnClick="linkbtn_filename22_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile23" runat="server" Text="Upload File 23" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename23" runat="server" Text="-" OnClick="linkbtn_filename23_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile24" runat="server" Text="Upload File 24" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename24" runat="server" Text="-" OnClick="linkbtn_filename24_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile25" runat="server" Text="Upload File 25" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename25" runat="server" Text="-" OnClick="linkbtn_filename25_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile26" runat="server" Text="Upload File 26" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename26" runat="server" Text="-" OnClick="linkbtn_filename26_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile27" runat="server" Text="Upload File 27" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename27" runat="server" Text="-" OnClick="linkbtn_filename27_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile28" runat="server" Text="Upload File 28" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename28" runat="server" Text="-" OnClick="linkbtn_filename28_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile29" runat="server" Text="Upload File 29" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename29" runat="server" Text="-" OnClick="linkbtn_filename29_Click"></asp:LinkButton>
                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile30" runat="server" Text="Upload File 30" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename30" runat="server" Text="-" OnClick="linkbtn_filename30_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="Pnl_Others1" runat="server">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_campaign" runat="server" Text="Campaign:" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:TextBox ID="text_campaign" runat="server" Width="500px" TextMode="MultiLine" Columns="25" Rows="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_caption" runat="server" Text="Caption:" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:TextBox ID="text_caption" runat="server" Width="500px" TextMode="MultiLine" Columns="25" Rows="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_targetlokasi" runat="server" Text="Target Location:" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:TextBox ID="text_targetlokasi" runat="server" Width="500px" TextMode="MultiLine" Columns="25" Rows="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_umur" runat="server" Text="Age:" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:TextBox ID="text_umur" runat="server" Width="500px" TextMode="MultiLine" Columns="25" Rows="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_jeniskelamin" runat="server" Text="Gender: "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:DropDownList ID="ddljeniskelamin" runat="server" Width="206px" Height="30px">
                                <asp:ListItem Text="All" Value="All" Selected="True" />
                                <asp:ListItem Text="Pria" Value="Pria" />
                                <asp:ListItem Text="Wanita" Value="Wanita" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_interestminat" runat="server" Text="Interest / Minat:" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:TextBox ID="text_interestminat" runat="server" Width="500px" TextMode="MultiLine" Columns="25" Rows="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_informasitambahan" runat="server" Text="Additional Information:" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:TextBox ID="text_informasitambahan" runat="server" Width="500px" TextMode="MultiLine" Columns="25" Rows="4"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%" colspan="4">
                            <hr runat="server" id="hr1" style="height: 5px; background-color: black" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_dibuat" runat="server" Text="Requester: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_dibuat" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggaldibuat" runat="server" Text="Date Create Form:" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggaldibuat" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_menyetujui1" runat="server" Text="Head Department: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_menyetujui1" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalmenyetujui1" runat="server" Text="Date Approved Form:" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalmenyetujui1" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_diterima1" runat="server" Text="Digital Marketing: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_diterima1" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggalditerima1" runat="server" Text="Date Accepted Form:" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalditerima1" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="Pnl_RevisiLoad" runat="server" Visible="false">
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_revisiload" runat="server" Text="Revise: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_revisiload" runat="server" TextMode="MultiLine" Rows="4" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                </asp:Panel>

                <tr>
                    <td style="width: 100%" colspan="4">
                        <hr runat="server" id="hr2" style="height: 5px; background-color: black" />
                    </td>
                </tr>

                <asp:Panel ID="Pnl_Buttons" runat="server">
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">
                            <asp:Panel ID="Pnl_Created" runat="server" Visible="false">
                                <asp:Button ID="btn_Save" runat="server" Text="Submit" OnClick="btn_Save_Click" OnClientClick="Confirm()" />
                                <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" OnClick="btn_Cancel_Click" />
                            </asp:Panel>
                            <asp:Button ID="btn_Done" runat="server" Text="Done" Visible="false" OnClick="btn_Done_Click" OnClientClick="Confirm()" />
                            <asp:Button ID="btn_Approved" runat="server" Text="Approved" Visible="false" OnClick="btn_Approved_Click" OnClientClick="Confirm()" />
                            <asp:Button ID="btn_Accepted" runat="server" Text="Approved" Visible="false" OnClick="btn_Accepted_Click" OnClientClick="Confirm()" />
                            <asp:Button ID="btn_ToRevise" runat="server" Text="To Revise" Visible="false" OnClick="btn_ToRevise_Click" />
                            <asp:Button ID="btn_UpdateSubmit" runat="server" Text="Update And Submit" OnClick="btn_UpdateSubmit_Click" OnClientClick="Confirm()" />
                            <asp:Button ID="btn_Reject" runat="server" Text="Reject" OnClick="btn_Reject_Click" Visible="false" OnClientClick="Confirm()" />
                            <asp:Button ID="btn_Delete" runat="server" Text="Delete" OnClick="btn_Delete_Click" Visible="false" />
                        </td>
                    </tr>

                </asp:Panel>
            </table>
        </div>
    </asp:Panel>

    <!--Pop Revise-->
    <asp:ModalPopupExtender ID="ModalRevise" runat="server" TargetControlID="btn_ToRevise"
        Drag="true" PopupControlID="PanelRevise" CancelControlID="btn_CloseRevise" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelRevise" runat="server" BackColor="WhiteSmoke" CssClass="ModalWindow"
        BorderStyle="Ridge" BorderColor="BlanchedAlmond"
        Style="display: none; top: 684px; left: 39px; width: 50%;">
        <br />
        <asp:HiddenField ID="HiddenField1" runat="server" />

        <table>
            <asp:Panel ID="Pnl_Revisi" runat="server" Visible="true">
                <tr>
                    <td class="FN150">
                        <asp:Label ID="label_revisi" runat="server" Text="Revise: " Width="170px"></asp:Label>
                    </td>
                    <td class="FV270">
                        <asp:TextBox ID="text_revisi" runat="server" TextMode="MultiLine" Rows="4" Width="300px"></asp:TextBox>
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td class="FN150"></td>
                <td class="FV270" colspan="3">
                    <asp:Button ID="btn_Revise" runat="server" Text="Revise" Visible="true" OnClick="btn_Revise_Click" OnClientClick="Confirm()" />

                    <asp:Button ID="btn_CloseRevise" runat="server" Text="Close" Width="100px" OnClick="btn_CloseRevise_Click" />
                </td>

            </tr>
        </table>

    </asp:Panel>

    <div id="HiddenFields">
        <asp:HiddenField ID="HfNO_FORM" runat="server" />
        <asp:HiddenField ID="HfUsername" runat="server" />
        <asp:HiddenField ID="HfID_DEPT" runat="server" />
    </div>
</asp:Content>

