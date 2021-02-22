<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="~/Controls/DocumentTypePanelBar1.ascx.cs" Inherits="DocViewer.Controls.DocumentTypePanelBar1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadPersistenceManagerProxy ID="RadPersistenceManagerProxy1" runat="server" OnUnload="RadPersistenceManagerProxy1_OnUnload" OnLoad="RadPersistenceManagerProxy1_Load">
    <PersistenceSettings>
        <telerik:PersistenceSetting ControlID="RadPanelBar1" />
    </PersistenceSettings>
</telerik:RadPersistenceManagerProxy>
<telerik:RadAjaxManagerProxy runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadPanelBar1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelCssClass="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>

</telerik:RadAjaxManagerProxy>
<div>
    <asp:HiddenField runat="server" ID="currPonAppUsersGd" />
    <asp:HiddenField runat="server" ID="currBridgeGd" />
    <asp:HiddenField runat="server" ID="currInspevntGd" />
    <asp:HiddenField runat="server" ID="currEventGd" />
    <asp:HiddenField runat="server" ID="currDocTypeKey" />
    <asp:HiddenField runat="server" ID="currDocSubTypeKey1" />

    <div class="demo-settings">
        <telerik:RadButton RenderMode="Lightweight" ID="SaveButton" runat="server" Text="Save state" OnClick="SaveButton_Click" />
        <telerik:RadButton RenderMode="Lightweight" ID="ResetButton" runat="server" Text="Reset state" OnClick="ResetButton_Click" />
        <telerik:RadButton RenderMode="Lightweight" ID="LoadButton" runat="server" Text="Load state" OnClick="LoadButton_Click" />
    </div>
    <div>
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/MenuItems.xml" XPath="menutree/Items/Item"></asp:XmlDataSource>
        <%--onClientItemClicked="collapseRoots"
              OnClientMouseOver="expandItem"
        --%>
        <telerik:RadCodeBlock runat="server">
            <script type="text/javascript">
                //this function is used to get a reference to the RadWindow in which the content page is opened
                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow)
                        oWindow = window.radWindow;
                    else if (window.frameElement.radWindow)
                        oWindow = window.frameElement.radWindow;
                    return oWindow;
                }
                function openWin2(sender, args) {
                    //var docTypKey = args.get_item(0);
                    console.log('%0', sender);
                    console.log(JSON.stringify(args, null, 2));
                    var oWnd = window.radopen("Testing/Url_Values_Dialog.aspx?bridge_gd=AAA&inspevnt_gd=BBBB&doctypekey=" + "10", "RadWindow1");
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxPanel runat="server" ID="RAPRPB1" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
            <telerik:RadPanelBar ID="RadPanelBar1" runat="server"
                DataSourceID="XmlDataSource1"
                DataTextField="Text"
                DataValueField="Text"
                DataNavigateUrlField="Url"
                AllowCollapseAllItems="True"
                OnClientItemClicked="openWin2"
                OnItemDataBound="RadPanelBar1_ItemDataBound"
                OnItemClick="RadPanelBar1_ItemClick"
                OnItemCreated="RadPanelBar1_ItemCreated"
                OnDataBinding="RadPanelBar1_DataBinding"
                OnDataBound="RadPanelBar1_DataBound"
                OnLoad="RadPanelBar1_Load"
                OnUnload="RadPanelBar1_Unload"
                OnDisposed="RadPanelBar1_Disposed"
                ExpandMode="SingleExpandedItem"
                EnableItemTextHtmlEncoding="True"
                EnableAriaSupport="True"
                EnableAjaxSkinRendering="True" Skin="WebBlue" RenderMode="Lightweight">
                <CollapseAnimation Duration="100" Type="None" />
                <ExpandAnimation Duration="100" Type="None" />
            </telerik:RadPanelBar>
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
    </div>
</div>
