﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportFormAllFormDesign.aspx.cs" MasterPageFile="~/Site.Master" Inherits="WebDelamiFormRequest.Forms_Data_Reports.ReportFormAllFormDesign" %>


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

            <h1>REPORT ALL FORM DESIGN</h1>
            <hr />
            <div id="DivMessage" runat="server" visible="false">
            </div>

            <br />

            <dx:ASPxGridView ID="gvMain" runat="server" AutoGenerateColumns="False" DataSourceID="CT_GridFormAll" OnRowCommand="gvMain_RowCommand" KeyFieldName="NO_FORM">
                <Settings ShowFilterRow="True" />
                <%--                <Styles Cell-Wrap="False" Header-Wrap="False">
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
                    <dx:GridViewDataTextColumn FieldName="KODE_FORM" VisibleIndex="1" Caption="KODE_FORM" HeaderStyle-Wrap="True" Visible="false">
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
                    <dx:GridViewDataDateColumn FieldName="TGL_DIBUAT" VisibleIndex="6" Caption="Date Create Form" HeaderStyle-Wrap="True">
                        <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy HH:mm:ss" EditFormatString="dd-MMM-yyyy HH:mm:ss" />
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="STATUS" VisibleIndex="7" Caption="Status" HeaderStyle-Wrap="True">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="NEXT_USER" VisibleIndex="8" Caption="Next Procesor" HeaderStyle-Wrap="True">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataColumn Caption="Action" VisibleIndex="9" Width="15%">
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
                    DataKeyNames="NO_FORM" EmptyDataText="- No Data Found -" OnPageIndexChanging="gvMain_PageIndexChanging" DataSourceID="C_GridGraphicDesignAll" OnRowCommand="gvMain_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="NO_FORM" HeaderText="No Form" SortExpression="NO_FORM" Visible="false" />
                        <asp:BoundField DataField="DIBUAT" HeaderText="PIC Create Form" />
                        <asp:BoundField DataField="TGL_REQUEST" HeaderText="Tanggal Request" />
                        <asp:BoundField DataField="NO_FORM" HeaderText="No Form" />
                        <asp:BoundField DataField="DEPT" HeaderText="Departemen" />
                        <asp:BoundField DataField="TGL_DIBUAT" HeaderText="Date Create Form" />
                        <asp:BoundField DataField="STATUS" HeaderText="Status" />
                                <asp:TemplateField ShowHeader="False" HeaderText="Action">
                            <ItemTemplate>
                                <div>
                                    <asp:Button ID="btn_view" runat="server" Text="View Data" CommandName="ClickRow"  Enabled="false"/>
                                    <asp:Button ID="btn_viewactivity" runat="server" Text="View Activity Data" CommandName="ActivityRow" Enabled="false"/>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>--%>

            <asp:SqlDataSource ID="CT_GridFormAll" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="select * from tf_ReportUserByAllGraphicDesign(@Username)  order by TGL_REQUEST DESC">
                <SelectParameters>
                    <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
                </SelectParameters>
            </asp:SqlDataSource>

        </div>

        <br />
    </asp:Panel>


    <div id="HiddenFields">
        <asp:HiddenField ID="HfNO_FORM" runat="server" />
        <asp:HiddenField ID="HfUsername" runat="server" />
        <asp:HiddenField ID="HfPagesName" runat="server" />
        <asp:HiddenField ID="HfKodeForm" runat="server" />
    </div>

</asp:Content>

