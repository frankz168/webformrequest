<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="I_FormRequest_Repair_SD.aspx.cs" MasterPageFile="~/Site.Master" Inherits="WebDelamiFormRequest.Forms_Data_Process.I_FormRequest_Repair_SD" %>


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

            <h1>FORM REQUEST - REPAIR-STORE-DESIGN</h1>
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
                            <asp:Label ID="label_tanggalrequired" runat="server" Text="Required Date: (*)"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggalrequired" runat="server" TextMode="Date" Font-Bold="true" ForeColor="DodgerBlue"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_brand" runat="server" Text="Brand: (*)" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_brand" runat="server" ReadOnly="true" Font-Bold="true" ForeColor="DodgerBlue" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
<%--                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_location" runat="server" Text="Location" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_location" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_concept" runat="server" Text="Concept" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_concept" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_jenis" runat="server" Text="Store Type Request: "></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:DropDownList ID="ddljenis" runat="server" Width="206px" Height="30px">
                                <asp:ListItem Text="Building" Value="BUILDING" Selected="True" />
                                <asp:ListItem Text="Store" Value="STORE" />
                                <asp:ListItem Text="Counter" Value="COUNTER" />
                                <asp:ListItem Text="Bazzar" Value="BAZZAR" />
                                <asp:ListItem Text="RFS" Value="RFS" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_depstoremall" runat="server" Text="Dept.Store / Mall: "></asp:Label>
                        </td>
                        <td class="FV270" colspan="6">
                            <div class="EU_TableScrollCustom" style="display: block; max-height: 200px; max-width: 1750px; width: 750px">
                                <dx:ASPxGridView ID="gvCustCt" runat="server" AutoGenerateColumns="False" DataSourceID="C_GridCustCt" KeyFieldName="ID">
                                    <Settings ShowFilterRow="True" />
                                    <SettingsBehavior AllowSelectByRowClick="true" />
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <SettingsBehavior AllowSelectByRowClick="true" />
                                    <Columns>

                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True" VisibleIndex="0" SelectAllCheckboxMode="AllPages">
                                            <ClearFilterButton Visible="True">
                                            </ClearFilterButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="ID" VisibleIndex="1" Caption="ID" HeaderStyle-Wrap="True" Visible="false">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="KODE_FORM" VisibleIndex="2" Caption="Kode Form" HeaderStyle-Wrap="True" Visible="false">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NO_FORM" VisibleIndex="3" Caption="No Form" HeaderStyle-Wrap="True" Visible="false">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="kode_cust" VisibleIndex="4" Caption="Kode Toko" HeaderStyle-Wrap="True">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="kode_ct" VisibleIndex="5" Caption="Kode CT" HeaderStyle-Wrap="True">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="site" VisibleIndex="6" Caption="Site" HeaderStyle-Wrap="True">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="nama_cust" VisibleIndex="7" Caption="Nama Cust" HeaderStyle-Wrap="True">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="nama_ct" VisibleIndex="8" Caption="Nama CT" HeaderStyle-Wrap="True">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                                <%--                                <asp:GridView ID="gvCustCt" runat="server" AllowPaging="False" PagerSettings-Position="TopAndBottom" ShowHeaderWhenEmpty="true"
                                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AutoGenerateColumns="False"
                                    DataKeyNames="ID" EmptyDataText="- No Data Found -" OnPageIndexChanging="gvCustCt_PageIndexChanging" OnRowDataBound="gvCustCt_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRow" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="Id" SortExpression="ID" Visible="false" />
                                        <asp:BoundField DataField="KODE_FORM" HeaderText="Kode Form" Visible="false" />
                                        <asp:BoundField DataField="NO_FORM" HeaderText="No Form" Visible="false" />
                                        <asp:BoundField DataField="kode_cust" HeaderText="Kode Toko" HeaderStyle-Width="150px" />
                                        <asp:BoundField DataField="kode_ct" HeaderText="Kode CT" HeaderStyle-Width="150px" />
                                        <asp:BoundField DataField="site" HeaderText="Site" HeaderStyle-Width="150px" />
                                        <asp:BoundField DataField="nama_cust" HeaderText="Nama Toko" HeaderStyle-Width="350px" />
                                        <asp:BoundField DataField="nama_ct" HeaderText="Nama CT" HeaderStyle-Width="150px" />
                                    </Columns>
                                </asp:GridView>--%>
                                <asp:SqlDataSource ID="C_GridCustCt" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                    SelectCommand="SELECT * FROM TR_FORM_GDR_CUST_TEMP WHERE NO_FORM = @NO_FORM">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="HfNO_FORM" Name="NO_FORM" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_sqm" runat="server" Text="SQM" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_sqm" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_typeofwork" runat="server" Text="Type Of Work" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="3">
                            <asp:CheckBox ID="checkbox_new" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_new" runat="server" Text="New" Font-Size="Small"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_relocation" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_relocation" runat="server" Text="Relocation" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_relayout" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_relayout" runat="server" Text="Re-Layout" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_renovation" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_renovation" runat="server" Text="Renovation" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_prepektif" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_prepektif" runat="server" Text="Prepektif" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                              <asp:CheckBox ID="checkbox_meplan" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_meplan" runat="server" Text="ME Plan" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                            <br />
                            <br />
                        </td>

                    </tr>

                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_projectrequest" runat="server" Text="Project Request" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="3">
                            <asp:CheckBox ID="checkbox_bq" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_bq" runat="server" Text="BQ" Font-Size="Small"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_ph" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_ph" runat="server" Text="PH" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_spk" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_spk" runat="server" Text="SPK" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_fittingout" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_fittingout" runat="server" Text="Fitting Out" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                        <asp:CheckBox ID="checkbox_settingup" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_settingup" runat="server" Text="Setting Up" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                              <asp:CheckBox ID="checkbox_opening" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_opening" runat="server" Text="Opening" Font-Size="Small"></asp:Label>&nbsp;&nbsp;
                            <br />
                        </td>

                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_provideinformation" runat="server" Text="Provide Information" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="3">

                            <asp:CheckBox ID="checkbox_sif" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_sif" runat="server" Text="SIF (Budget / Approval Management)" Font-Size="Small"></asp:Label>&nbsp;&nbsp;

                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">

                            <asp:CheckBox ID="checkbox_keyplan" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_keyplan" runat="server" Text="Key Plan ( With Indication of Main Entrance And TrafficCirculation)" Font-Size="Small"></asp:Label>&nbsp;&nbsp;

                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">

                            <asp:CheckBox ID="checkbox_basedrawing" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_basedrawing" runat="server" Text="Base Drawing With Measurement (Layout, Elevation, ME Plan)" Font-Size="Small"></asp:Label>&nbsp;&nbsp;

                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">

                            <asp:CheckBox ID="checkbox_sitemeasurement" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_sitemeasurement" runat="server" Text="Site Measurement (If Any)" Font-Size="Small"></asp:Label>&nbsp;&nbsp;

                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">

                            <asp:CheckBox ID="checkbox_actualphoto" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_actualphoto" runat="server" Text="Actual Foto (If Any)" Font-Size="Small"></asp:Label>&nbsp;&nbsp;

                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">

                            <asp:CheckBox ID="checkbox_numberofexistingstock" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_numberofexistingstock" runat="server" Text=" Number Of Existing Stock (For Existing Project)" Font-Size="Small"></asp:Label>&nbsp;&nbsp;

                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">

                            <asp:CheckBox ID="checkbox_fitoutguidance" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_fitoutguidance" runat="server" Text="Fit Out Guidance (FSS)" Font-Size="Small"></asp:Label>&nbsp;&nbsp;

                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">

                            <asp:CheckBox ID="checkbox_deptstoreguidance" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_checkbox_deptstoreguidance" runat="server" Text="Dept Store Guidance (SIS)" Font-Size="Small"></asp:Label>&nbsp;&nbsp;

                        </td>
                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">

                            <asp:CheckBox ID="checkbox_shopfrontguidance" runat="server" Text="" Font-Size="XX-Small" />
                            <asp:Label ID="label_shopfrontguidance" runat="server" Text="Shop Front Guidance" Font-Size="Small"></asp:Label>&nbsp;&nbsp;

                        </td>
                    </tr>
                </asp:Panel>

                <asp:Panel ID="Pnl_UploadFileStoreDesign" runat="server">
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

                    </tr>
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile3" runat="server" Text="Upload File 3" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename3" runat="server" Text="-" OnClick="linkbtn_filename3_Click"></asp:LinkButton>

                        </td>
                        <td class="FV270">
                            <asp:FileUpload ID="btn_uploadfile4" runat="server" Text="Upload File 4" Font-Size="X-Small" />
                            <br />
                            <asp:LinkButton ID="linkbtn_filename4" runat="server" Text="-" OnClick="linkbtn_filename4_Click"></asp:LinkButton>
                        </td>
                </asp:Panel>

                <asp:Panel ID="Pnl_Others1" runat="server">
                    <tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_description" runat="server" Text="Description:" Font-Size="Small"></asp:Label>
                        </td>
                        <td class="FV270" colspan="4">
                            <asp:TextBox ID="text_description" runat="server" Width="500px" TextMode="MultiLine" Columns="25" Rows="4"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%" colspan="4">
                            <hr runat="server" id="hr1" style="height: 5px; background-color: black" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FN150">
                            <asp:Label ID="label_dibuatsd" runat="server" Text="Store Design: " Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_dibuatsd" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="FN150">
                            <asp:Label ID="label_tanggaldibuatsd" runat="server" Text="Date Store Design:" Width="170px"></asp:Label>
                        </td>
                        <td class="FV270">
                            <asp:TextBox ID="text_tanggaldibuatsd" runat="server" ReadOnly="true" TextMode="Date"></asp:TextBox>
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
                    <td style="width: 100%">
                        <hr runat="server" id="hr2" style="height: 5px; background-color: black" />
                    </td>
                </tr>

                <asp:Panel ID="Pnl_Buttons" runat="server">
                    <tr>
                        <td class="FN150"></td>
                        <td class="FV270" colspan="3">
                            <asp:Panel ID="Pnl_Created" runat="server" Visible="false">
                                <asp:Button ID="btn_Save" runat="server" Text="Submit" OnClick="btn_Save_Click" OnClientClick="Confirm()" />
                                <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" OnClick="btn_Cancel_Click" Visible="false" />
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
        <asp:HiddenField ID="HfRequiredDate" runat="server" />
        <asp:HiddenField ID="HfActionGeneral" runat="server" />
    </div>
</asp:Content>


