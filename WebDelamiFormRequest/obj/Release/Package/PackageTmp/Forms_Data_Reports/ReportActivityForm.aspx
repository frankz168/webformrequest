<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportActivityForm.aspx.cs" MasterPageFile="~/Site.Master" Inherits="WebDelamiFormRequest.Forms_Data_Reports.ReportActivityForm" %>

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

            <h1>REPORT DATA HISTORY ACTIVITY</h1>
            <hr />
            <div id="DivMessage" runat="server" visible="false">
            </div>

            <br />

            <dx:ASPxGridView ID="gvMain" runat="server" AutoGenerateColumns="False" DataSourceID="CT_Grid" KeyFieldName="ID">
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
                    <dx:GridViewDataTextColumn FieldName="ID" VisibleIndex="1" Caption="ID" Visible="false">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="USERNAME" VisibleIndex="2" Caption="USERNAME">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="ACTIVITY_TIME" VisibleIndex="3" Caption="ACTIVITY TIME">
                        <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy HH:mm:ss" EditFormatString="dd-MMM-yyyy HH:mm:ss" />
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="STATUS" VisibleIndex="4" Caption="STATUS">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DESCRIPTION" VisibleIndex="5" Caption="DESCRIPTION">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="REVISION" VisibleIndex="6" Caption="REVISION">
                        <Settings AutoFilterCondition="Contains" />
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>

            <%--            <div class="EU_TableScroll" style="display: block; max-height: 1000px;">
                <asp:GridView ID="gvMain" runat="server" AllowPaging="True" PagerSettings-Position="TopAndBottom" ShowHeaderWhenEmpty="True"
                    CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AutoGenerateColumns="False"
                    DataKeyNames="NO_FORM" EmptyDataText="- No Data Found -" OnPageIndexChanging="gvMain_PageIndexChanging" DataSourceID="CT_Grid">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                        <asp:BoundField DataField="USERNAME" HeaderText="USERNAME" SortExpression="USERNAME" />
                        <asp:BoundField DataField="ACTIVITY_TIME" HeaderText="ACTIVITY_TIME" SortExpression="ACTIVITY_TIME" />
                        <asp:BoundField DataField="NO_FORM" HeaderText="No Form" SortExpression="NO_FORM" />
                        <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS" />
                        <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" SortExpression="DESCRIPTION" />
                        <asp:BoundField DataField="REVISION" HeaderText="REVISION" SortExpression="REVISION" />
                    </Columns>
                </asp:GridView>
            </div>--%>

            <asp:SqlDataSource ID="CT_Grid" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT * FROM TR_FORM_GDR_ACTIVITY WHERE NO_FORM = @NO_FORM Order By ID ASC">
                <SelectParameters>
                    <asp:ControlParameter ControlID="HfNO_FORM" Name="NO_FORM" PropertyName="Value" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>

        <br />
    </asp:Panel>


    <div id="HiddenFields">
        <asp:HiddenField ID="HfNO_FORM" runat="server" />
        <asp:HiddenField ID="HfUsername" runat="server" />
    </div>

</asp:Content>

