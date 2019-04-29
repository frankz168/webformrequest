<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportFormGdrVM.aspx.cs" MasterPageFile="~/Site.Master" Inherits="WebDelamiFormRequest.Forms_Data_Reports.ReportFormGdrVM" %>

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

            <h1>REPORT FORM GDR VM</h1>
            <hr />
            <div id="DivMessage" runat="server" visible="false">
            </div>

            <br />

            <dx:ASPxGridView ID="gvMain" runat="server" AutoGenerateColumns="False" DataSourceID="C_GridUserVM" OnRowCommand="gvMain_RowCommand" KeyFieldName="NO_FORM;KODE_FORM">
                <Settings ShowFilterRow="True" />
                <%--               <Styles Cell-Wrap="False" Header-Wrap="False">
                    <Header Wrap="False" />
                    <Cell Wrap="False" />
                </Styles>--%>
                <SettingsBehavior AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsBehavior AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                <SettingsPager Mode="ShowPager" PageSize="25" Position="TopAndBottom" AlwaysShowPager="True" />
                <Settings ShowFooter="True" />
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" Width="10px">
                        <ClearFilterButton Visible="True">
                        </ClearFilterButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="KODE_FORM" VisibleIndex="10" Caption="KODE_FORM" HeaderStyle-Wrap="True" Visible="false">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="NO_FORM" VisibleIndex="1" Caption="NO_FORM" HeaderStyle-Wrap="True">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="BRAND" VisibleIndex="2" Caption="BRAND" HeaderStyle-Wrap="True">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DIBUAT" VisibleIndex="3" Caption="PIC Create Form" HeaderStyle-Wrap="True">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="TGL_REQUEST" VisibleIndex="4" Caption="Tanggal Request" HeaderStyle-Wrap="True">
                        <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy HH:mm:ss" EditFormatString="dd-MMM-yyyy HH:mm:ss" />
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="DEPT" VisibleIndex="5" Caption="DEPARTEMEN" HeaderStyle-Wrap="True">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="TGL_REQUIRED" VisibleIndex="6" Caption="Tanggal Required" HeaderStyle-Wrap="True">
                        <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy HH:mm:ss" EditFormatString="dd-MMM-yyyy HH:mm:ss" />
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="STATUS" VisibleIndex="7" Caption="Status" HeaderStyle-Wrap="True">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="STATUS_VER" VisibleIndex="8" Caption="Status Ver" HeaderStyle-Wrap="True">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="NEXT_USER" VisibleIndex="9" Caption="Next Procesor" HeaderStyle-Wrap="True">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn Caption="Action" VisibleIndex="10" Width="15%">
                        <DataItemTemplate>
                            <asp:Button ID="btn_view" runat="server" Text="View Data" CommandName="ClickRow" />
                            <asp:Button ID="btn_viewactivity" runat="server" Text="View Activity Data" CommandName="ActivityRow" />
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                </Columns>
            </dx:ASPxGridView>

            <%--    <div class="EU_TableScroll" style="display: block; max-height: 1000px;">
                <asp:GridView ID="gvMain" runat="server" AllowPaging="True" PagerSettings-Position="TopAndBottom" ShowHeaderWhenEmpty="True"
                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AutoGenerateColumns="False"
                    DataKeyNames="NO_FORM" EmptyDataText="- No Data Found -" OnPageIndexChanging="gvMainPageChanging" DataSourceID="C_GridUser" OnRowCommand="gvMainRowCommand">
                    <Columns>
                        <asp:BoundField DataField="NO_FORM" HeaderText="No Form" SortExpression="NO_FORM" Visible="false" />
                        <asp:BoundField DataField="DIBUAT" HeaderText="PIC Create Form" />
                        <asp:BoundField DataField="TGL_REQUEST" HeaderText="Tanggal Request" />
                        <asp:BoundField DataField="NO_FORM" HeaderText="No Form" />
                        <asp:BoundField DataField="DEPT" HeaderText="Departemen" />
                        <asp:BoundField DataField="TGL_DIBUAT" HeaderText="Date Create Form" />
                        <asp:BoundField DataField="STATUS" HeaderText="Status" />
                        <asp:BoundField DataField="STATUS_VER" HeaderText="Status Ver" />
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


            </div>--%>
        </div>



        <div id="GridFilterByKodeForm">


            <asp:SqlDataSource ID="C_GridUserFormFilterKodeForm" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT * FROM tf_ReportUserByKodeForm(@Username, @KODE_FORM) Order by TGL_REQUEST DESC ">
                <SelectParameters>
                    <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
                    <asp:ControlParameter ControlID="HfKodeForm" Name="KODE_FORM" PropertyName="Value" />
                </SelectParameters>
            </asp:SqlDataSource>


        </div>

        <asp:SqlDataSource ID="C_GridKodeForm" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="  SELECT 'ALL' AS KODE_FORM, 'ALL' AS NM_FORM FROM MS_FORM
                                  UNION
                                  SELECT KODE_FORM, KODE_FORM + '-' + NM_FORM AS NM_FORM FROM MS_FORM  WHERE KODE_FORM &lt;&gt; 'FRM-0004'"></asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridKodeFormIklan" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand=" SELECT KODE_FORM, KODE_FORM + '-' + NM_FORM AS NM_FORM FROM MS_FORM  WHERE KODE_FORM = 'FRM-0004'"></asp:SqlDataSource>
        <br />
    </asp:Panel>

    <div id="HiddenFields">
        <asp:HiddenField ID="HfNO_FORM" runat="server" />
        <asp:HiddenField ID="HfUsername" runat="server" />
        <asp:HiddenField ID="HfID_DEPT" runat="server" />
        <asp:HiddenField ID="HfKD_BRAND" runat="server" />
        <asp:HiddenField ID="HfPagesName" runat="server" />
        <asp:HiddenField ID="HfKodeForm" runat="server" />
    </div>

</asp:Content>



