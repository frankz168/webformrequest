<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderMenu.ascx.cs" Inherits="WebDelamiFormRequest.HeaderMenu" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<div>
    <dx:ASPxLabel ID="C_AppName" runat="server" Text="" Font-Bold="true" ForeColor="Black"></dx:ASPxLabel>
    &nbsp;
    <dx:ASPxLabel ID="C_LoginInfo" runat="server" Text="" Font-Bold="false"></dx:ASPxLabel>
    &nbsp;
    <dx:ASPxHyperLink ID="C_LogOff" runat="server" Text="Logout" NavigateUrl="~/D0101Home/D010103Logout.aspx" Visible="false">
    </dx:ASPxHyperLink>
    &nbsp;
    <dx:ASPxLabel ID="C_ScreenName" runat="server" Text="" Font-Bold="true" ForeColor="Black"></dx:ASPxLabel>
    &nbsp;
      <dx:ASPxMenu ID="C_Menu" runat="server" Width="100%" SyncSelectionMode="None" ItemStyle-Wrap="False" SubMenuItemStyle-Wrap="False" SubMenuStyle-Wrap="False" Visible="true">
          <Items>
              <dx:MenuItem NavigateUrl="../MainMenu.aspx" Text="HOME" Visible="false" Name="D0101">
                  <ItemStyle VerticalAlign="Middle" Width="120px" HorizontalAlign="Center" />
              </dx:MenuItem>
              <dx:MenuItem Text="SECURITY" Visible="false" Name="D0102">
                  <ItemStyle VerticalAlign="Middle" Width="120px" HorizontalAlign="Center" />
                  <Items>
                      <dx:MenuItem Text="APPLICATION SETTINGS" NavigateUrl="~/Forms_Data_Security/Default_Admin.aspx" Visible="false" Name="D010201" />
                      <dx:MenuItem Text="FUNCTIONS" NavigateUrl="~/Forms_Data_Security/Default_Admin.aspx" Visible="false" Name="D010202" />
                      <dx:MenuItem Text="USER PROFILE" NavigateUrl="~/Forms_Data_Security/Security_User_Profile.aspx" Visible="false" Name="D010203" />
                      <dx:MenuItem Text="USER" NavigateUrl="~/Forms_Data_Security/Security_User.aspx" Visible="true" Name="D010204" />
                  </Items>
              </dx:MenuItem>
              <dx:MenuItem Text="MASTER" Visible="false" Name="D0103">
                  <ItemStyle VerticalAlign="Middle" Width="120px" HorizontalAlign="Center" />
                  <Items>
                      <dx:MenuItem Text="HELP DESIGNER" NavigateUrl="~/D0103Administration/D010301HelpDesigner.aspx" Visible="false" Name="D010301" />
                      <dx:MenuItem Text="MASTER JABATAN" NavigateUrl="~/Forms_Data_Master/Master_Jabatan.aspx" Visible="true" Name="D010302" />
                      <dx:MenuItem Text="MASTER BRAND" NavigateUrl="~/Forms_Data_Master/Master_Brand.aspx" Visible="true" Name="D010305" />
                      <dx:MenuItem Text="MASTER DEPARTEMEN" NavigateUrl="~/Forms_Data_Master/Master_Departemen.aspx" Visible="true" Name="D010303" />
                      <dx:MenuItem Text="MASTER FORM" NavigateUrl="~/Forms_Data_Master/Master_Form.aspx" Visible="true" Name="D010304" />
                  </Items>
              </dx:MenuItem>
              <dx:MenuItem Text="REQUEST FORM" Visible="false" Name="D0104">

                  <ItemStyle Width="120px" HorizontalAlign="Center" />
                  <Items>
                      <dx:MenuItem Text="FORM GRAPHIC DESIGN" NavigateUrl="~/Menu_Forms_Detail/FormRequestGraphicDesign.aspx" Visible="false" Name="D010401" />
                      <dx:MenuItem Text="FORM REPAIR" NavigateUrl="~/Menu_Forms_Detail/FormRequestRepair.aspx" Visible="false" Name="D010402" />
                      <dx:MenuItem Text="FORM ADS IKLAN" NavigateUrl="~/Menu_Forms_Detail/FormRequestDigitalAds.aspx" Visible="false" Name="D010403" />
                  </Items>
              </dx:MenuItem>
              <dx:MenuItem Text="EXTERNAL PROCESS" Visible="false" Name="D0105">
                  <ItemStyle VerticalAlign="Middle" Width="120px" HorizontalAlign="Center" />
                  <Items>
                      <dx:MenuItem Text="PENDING JOB" NavigateUrl="~/D0105ExternalProcess/D010501PendingJob.aspx" Visible="false" Name="D010501" />
                  </Items>
              </dx:MenuItem>
              <%--  <dx:MenuItem Text="HOUSEKEEPING" Visible="false" Name="D0106">
                <ItemStyle VerticalAlign="Middle" Width="120px" HorizontalAlign="Center" />
                <Items>
                    <dx:MenuItem Text="SCRIPT RUNNER" NavigateUrl="~/D0106Housekeeping/D010601ScriptRunner.aspx" Visible="false" Name="D010601" />
                    <dx:MenuItem Text="ARCHIVE" NavigateUrl="~/D0106Housekeeping/D010602Archive.aspx" Visible="false" Name="D010602" />
                </Items>
            </dx:MenuItem>--%>
              <dx:MenuItem Text="REPORT" Visible="false" Name="D0107">
                  <ItemStyle VerticalAlign="Middle" Width="120px" HorizontalAlign="Center" />
                  <Items>
                      <dx:MenuItem BeginGroup="True" Text="REPORT GRAPHIC DESIGN" Visible="false" Name="D010700">
                          <ItemStyle Width="220px" HorizontalAlign="Left" />
                          <Items>
                              <dx:MenuItem Text="REPORT FORM GDR VM" NavigateUrl="~/Forms_Data_Reports/ReportFormGdrVM.aspx" Visible="true" Name="D010706" />
                              <dx:MenuItem Text="REPORT FORM GDR MARKOM" NavigateUrl="~/Forms_Data_Reports/ReportFormGdrMarkom.aspx" Visible="true" Name="D010707" />
                              <dx:MenuItem Text="REPORT FORM GDR OTHERS" NavigateUrl="~/Forms_Data_Reports/ReportFormGdrOthers.aspx" Visible="true" Name="D010708" />
                              <dx:MenuItem Text="REPORT ALL FORM DESIGN" NavigateUrl="~/Forms_Data_Reports/ReportFormAllFormDesign.aspx" Visible="true" Name="D010711" />
                              <dx:MenuItem Text="REPORT REQUEST APPROVED BAMBANG" NavigateUrl="~/Forms_Data_Reports/ReportPDC_VM.aspx" Visible="false" Name="D010713" />
                          </Items>
                      </dx:MenuItem>
                      <dx:MenuItem Text="REPORT FORM DIGITAL MARKETING" NavigateUrl="~/Forms_Data_Reports/ReportFormDigitalMarketing.aspx" Visible="false" Name="D010710" />
                      <dx:MenuItem Text="REPORT FORM REPAIR" NavigateUrl="~/Forms_Data_Reports/ReportFormRepair.aspx" Visible="false" Name="D010709" />
                      <dx:MenuItem Text="REPORT ALL FORM" NavigateUrl="~/Forms_Data_Reports/ReportAllForm.aspx" Visible="false" Name="D010712" />
                      <dx:MenuItem Text="REPORT ADS IKLAN FORM" NavigateUrl="~/Forms_Data_Reports/ReportDigitalAdsForm.aspx" Visible="false" Name="D010703" />
                      <dx:MenuItem Text="REPORT ACTIVITY FORM" NavigateUrl="~/Forms_Data_Reports/ReportActivityForm.aspx" Visible="false" Name="D010704" />
                      <dx:MenuItem Text="REPORT STATUS FORM" NavigateUrl="~/Forms_Data_Reports/ReportStatusForm.aspx" Visible="false" Name="D010705" />
                  </Items>
              </dx:MenuItem>
              <dx:MenuItem Text="VIEW ALL TASK" Visible="false" Name="D0108">
                  <ItemStyle VerticalAlign="Middle" Width="120px" HorizontalAlign="Center" />
                  <Items>
                      <%--                      <dx:MenuItem Text="VIEW ALL TASK FORM" NavigateUrl="~/Forms_Data_Reports/ReportAllForm.aspx" Visible="false" Name="D010801" />--%>
                      <dx:MenuItem Text="VIEW ALL TASK GRAPHIC DESIGN FORM" NavigateUrl="~/Forms_Data_Reports/ReportGraphicDesignForm.aspx" Visible="false" Name="D010802" />
                      <dx:MenuItem Text="VIEW ALL TASK DIGITAL MARKETING FORM" NavigateUrl="~/Forms_Data_Reports/ReportAllDigitalAdsForm.aspx" Visible="false" Name="D010803" />
                  </Items>
              </dx:MenuItem>
              <dx:MenuItem Text="MANUAL INSTRUCTION" Visible="true" Name="D0109">
                  <ItemStyle VerticalAlign="Middle" Width="120px" HorizontalAlign="Center" />
                  <Items>
                      <dx:MenuItem BeginGroup="True" Text="FORM GRAPHIC DESIGN" Visible="true" Name="D0109001">
                          <ItemStyle Width="220px" HorizontalAlign="Left" />
                          <Items>
                              <dx:MenuItem Text="MANUAL BOOK" NavigateUrl="~/Template/Manual-Book-Form-Graphic-Design.pdf" Visible="true" Name="D010901" Target="_blank" />
                              <dx:MenuItem Text="FLOW" NavigateUrl="~/Template/Flow-Form-Graphic-Design.pdf" Visible="true" Name="D010901" Target="_blank" />
                          </Items>
                      </dx:MenuItem>
                      <dx:MenuItem BeginGroup="True" Text="FORM DIGITAL MARKETING" Visible="true" Name="D0109002">
                          <ItemStyle Width="220px" HorizontalAlign="Left" />
                          <Items>
                              <dx:MenuItem Text="MANUAL BOOK" NavigateUrl="~/Template/Manual-Book-Form-Digital-Advertising.pdf" Visible="true" Name="D010902" Target="_blank" />
                              <dx:MenuItem Text="FLOW" NavigateUrl="~/Template/Flow-Form-Digital-Advertising.pdf" Visible="true" Name="D010903" Target="_blank" />
                          </Items>
                      </dx:MenuItem>
                      <dx:MenuItem BeginGroup="True" Text="FORM REPAIR" Visible="true" Name="D0109003">
                          <ItemStyle Width="220px" HorizontalAlign="Left" />
                          <Items>
                              <dx:MenuItem Text="MANUAL BOOK" NavigateUrl="~/Template/Manual-Book-Form-Repair.pdf" Visible="true" Name="D010904" Target="_blank" />
                              <dx:MenuItem Text="FLOW" NavigateUrl="~/Template/Flow-Form-Repair.pdf" Visible="true" Name="D010905" Target="_blank" />
                          </Items>
                      </dx:MenuItem>
                  </Items>
              </dx:MenuItem>
              <%--<dx:MenuItem NavigateUrl="~/D0101Home/D010104ChangePassword.aspx" Text="CHANGE PASSWORD" Visible="false" Name="D010104">
                                        <ItemStyle VerticalAlign="Middle" Width="120px" HorizontalAlign="Center" />
                                    </dx:MenuItem>
                                    <dx:MenuItem NavigateUrl="~/D0101Home/D010103Logout.aspx" Text="LOGOUT" Visible="false" Name="D010103">
                                        <ItemStyle VerticalAlign="Middle" Width="120px" HorizontalAlign="Center" />
                                    </dx:MenuItem>--%>
              <dx:MenuItem Text=" " Enabled="false" ItemStyle-Width="100%" ItemStyle-HorizontalAlign="Right" ItemStyle-ForeColor="Red" ItemStyle-Font-Bold="true">
                  <ItemStyle Width="100%"></ItemStyle>
              </dx:MenuItem>
          </Items>
      </dx:ASPxMenu>
</div>
