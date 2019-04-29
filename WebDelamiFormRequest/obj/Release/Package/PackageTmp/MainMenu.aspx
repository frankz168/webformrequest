<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MainMenu.aspx.cs" Inherits="WebDelamiFormRequest.MainMenu" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Src="~/UserControl/HeaderMenu.ascx" TagPrefix="TVBUC1" TagName="HeaderMenu" %>
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
            <div id="Menu">
                <TVBUC1:HeaderMenu runat="server" ID="HM" />
            </div>

            <%--            <h1>MENU UTAMA</h1>
            <hr />--%>
            <div id="DivMessage" runat="server" visible="false">
            </div>
        </div>

        <asp:Button ID="btn_RequestForm" runat="server" Text="Request Form" OnClick="btn_RequestForm_Click" Visible="false" />
        <br />
        <asp:Button ID="btn_ReportStatus" runat="server" Text="View All Own Task" OnClick="btn_ReportStatus_Click" Visible="false" />
        <br />
        <asp:Button ID="btn_ReportGraphicDesign" runat="server" Text="View All Task Design" OnClick="btn_ReportGraphicDesign_Click" Visible="false" />
        <asp:Button ID="btn_ReportDigitalAds" runat="server" Text="View All Task Request Digital Advertising" OnClick="btn_ReportDigitalAds_Click" Visible="false" />
        <asp:Button ID="btn_ReportUserAll" runat="server" Text="View All Task Form Request" OnClick="btn_ReportUserAll_Click" Visible="false" />
    </asp:Panel>

    <div>
        <h1>MY DASHBOARD</h1>
        <hr />
    </div>
    <br />


    <dx:ASPxGridView ID="gvMain" runat="server" AutoGenerateColumns="False" DataSourceID="C_GridDashUser" OnRowCommand="gvMain_RowCommand" KeyFieldName="NO_FORM;KODE_FORM">
        <Settings ShowFilterRow="True" />
        <%--                       <Styles Cell-Wrap="False" Header-Wrap="False">
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
            <dx:GridViewDataTextColumn FieldName="KODE_FORM" VisibleIndex="1" Caption="KODE FORM" HeaderStyle-Wrap="True">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="NO_FORM" VisibleIndex="2" Caption="NO_FORM" HeaderStyle-Wrap="True">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="BRAND" VisibleIndex="3" Caption="BRAND" HeaderStyle-Wrap="True">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="DIBUAT" VisibleIndex="4" Caption="PIC Create Form" HeaderStyle-Wrap="True">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="TGL_REQUEST" VisibleIndex="5" Caption="Tanggal Request" HeaderStyle-Wrap="True">
                <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy HH:mm:ss" EditFormatString="dd-MMM-yyyy HH:mm:ss" />
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="DEPT" VisibleIndex="6" Caption="DEPARTEMEN" HeaderStyle-Wrap="True">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="Last_Activity" VisibleIndex="7" Caption="Last Activity" HeaderStyle-Wrap="True">
                <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy HH:mm:ss" EditFormatString="dd-MMM-yyyy HH:mm:ss" />
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="Hari" VisibleIndex="8" Caption="Hari" HeaderStyle-Wrap="True">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="USER_CURRENT" VisibleIndex="9" Caption="Current Procesor" HeaderStyle-Wrap="True">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="NEXT_USER" VisibleIndex="10" Caption="Next Procesor" HeaderStyle-Wrap="True">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <%--            <dx:GridViewDataTextColumn FieldName="kdjbt" VisibleIndex="11" Caption="Jabatan" HeaderStyle-Wrap="True">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>--%>
            <dx:GridViewDataDateColumn FieldName="TGL_REQUIRED" VisibleIndex="11" Caption="Tanggal Required" HeaderStyle-Wrap="True">
                <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy HH:mm:ss" EditFormatString="dd-MMM-yyyy HH:mm:ss" />
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="STATUS" VisibleIndex="12" Caption="Status" HeaderStyle-Wrap="True">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn Caption="Action" VisibleIndex="13" Width="15%">
                <DataItemTemplate>
                    <asp:Button ID="btn_view" runat="server" Text="View Data" CommandName="ClickRow" />
                    <asp:Button ID="btn_viewactivity" runat="server" Text="View Activity Data" CommandName="ActivityRow" />
                </DataItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>
    </dx:ASPxGridView>

    <%-- <div class="EU_TableScroll" style="display: block; max-height: 1000px;">
        <asp:GridView ID="gvMain" runat="server" AllowPaging="True" PagerSettings-Position="TopAndBottom" ShowHeaderWhenEmpty="True"
            CssClass="table table-bordered EU_DataTable" PagerStyle-BackColor="Black" AutoGenerateColumns="False"
            DataKeyNames="NO_FORM, KODE_FORM" EmptyDataText="- No Data Found -" OnPageIndexChanging="gvMain_PageIndexChanging" DataSourceID="C_GridDashUser" OnRowCommand="gvMain_RowCommand">
            <Columns>
                <asp:BoundField DataField="NO_FORM" HeaderText="No Form" SortExpression="NO_FORM" Visible="false" />
                <asp:BoundField DataField="DIBUAT" HeaderText="PIC Create Form" />
                <asp:BoundField DataField="TGL_REQUEST" HeaderText="Tanggal Request" />
                <asp:BoundField DataField="KODE_FORM" HeaderText="Kode Form" HeaderStyle-Width="150px" />
                <asp:BoundField DataField="NO_FORM" HeaderText="No Form" />
                <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                <asp:BoundField DataField="DEPT" HeaderText="Departemen" />
                <asp:BoundField DataField="Last_Activity" HeaderText="Last Activity" />
                <asp:BoundField DataField="Hari" HeaderText="Hari" />
                <asp:BoundField DataField="USERNAME" HeaderText="Current Procesor" />
                <asp:BoundField DataField="usernext" HeaderText="Next Procesor" ItemStyle-ForeColor="Blue" ItemStyle-Font-Bold="true" />
                  <asp:BoundField DataField="kdjbt" HeaderText="Jabatan" />
                <asp:BoundField DataField="TGL_DIBUAT" HeaderText="Date Create Form" />
                <asp:BoundField DataField="STATUS" HeaderText="Status" />
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

    <div id="GridGDR">
        <asp:SqlDataSource ID="C_GridDashUser" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashGdrUser(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashHeadDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashGdrHeadDepartment(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashBrandManager" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashGdrBrandManager(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashHeadVM" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashGdrHeadVM() Order by Last_Activity"></asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashPhotographer" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashGdrPhotoGrapher(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashDI" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashGdrDI(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashPDC" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashGdrPDC(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>



        <asp:SqlDataSource ID="C_GridDashHeadDesigner" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashGdrHeadDesigner() Order by Last_Activity"></asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashGraphicDesigner" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashGdrGraphicDesigner(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashCreativeManagerMarkom" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="SELECT * FROM tf_DashGdrCreativeManagerMarkom() Order by Last_Activity"></asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashCreativeManagerOthers" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="SELECT * FROM tf_DashGdrCreativeManagerOthers() Order by Last_Activity"></asp:SqlDataSource>
    </div>


    <div id="GridIklan">
        <asp:SqlDataSource ID="C_GridDashUserIklan" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashGdrUserIklan(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashHeadDepartmentIklan" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashGdrHeadDepartmentIklan(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashDigitalMarketingIklan" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashGdrDigitalMarketingIklan(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

    </div>

    <div id="GridRepair">
        <asp:SqlDataSource ID="C_GridDashUserRepair" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashGdrUser(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashHeadDepartmentRepair" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashGdrHeadDepartment(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

          <asp:SqlDataSource ID="C_GridDashStoreDesignRepair" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashStoreDesignRepair(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>

        </asp:SqlDataSource>
        <asp:SqlDataSource ID="C_GridDashProjectRepair" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashProjectRepair(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

          <asp:SqlDataSource ID="C_GridDashProjectRepairPIC" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashProjectRepairPIC(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashBrandManagerRepair" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashGdrBrandManager(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashComercialDirectorRepair" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashComercialDirectorRepair(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashBudgetControlRepair" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="select * from tf_DashBudgetControlRepair(@Username) Order by Last_Activity">
            <SelectParameters>
                <asp:ControlParameter ControlID="HfUsername" Name="Username" PropertyName="Value" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="C_GridDashCreativeManagerRepair" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="SELECT * FROM tf_DashGdrCreativeManagerOthers() Order by Last_Activity"></asp:SqlDataSource>

    </div>

    <div id="HiddenFields">
        <asp:HiddenField ID="HfUsername" runat="server" />
        <asp:HiddenField ID="HfID_DEPT" runat="server" />
        <asp:HiddenField ID="HfKDBRAND" runat="server" />
        <asp:HiddenField ID="HfKODE_FORM" runat="server" />
        <asp:HiddenField ID="HfNO_FORM" runat="server" />
        <asp:HiddenField ID="HfPagesName" runat="server" />
        <asp:HiddenField ID="HfUserProfileId" runat="server" />
    </div>
</asp:Content>



