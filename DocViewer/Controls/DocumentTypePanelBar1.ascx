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
        <telerik:RadCodeBlock runat="server" >
            <script type="text/javascript">
                
                function openWin2(sender, args) {
                    //var docTypKey = args.get_item(0);
                    var oWnd =  radopen("Testing/Url_Values_Dialog.aspx?bridge_gd=AAA&inspevnt_gd=BBBB&doctypekey="+"10" , "RadWindow1");
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadPanelBar ID="RadPanelBar1" runat="server"
            DataSourceID="XmlDataSource1"
            DataTextField="Text"
            DataValueField="Text"
            DataNavigateUrlField="Url"
                             onClientItemClicked="openWin2"
            OnItemDataBound="RadPanelBar1_ItemDataBound"
            OnItemClick="RadPanelBar1_ItemClick"
            OnItemCreated="RadPanelBar1_ItemCreated"
            OnDataBinding="RadPanelBar1_DataBinding"
            OnDataBound="RadPanelBar1_DataBound"
            OnLoad="RadPanelBar1_Load"
            OnUnload="RadPanelBar1_Unload"
            OnDisposed="RadPanelBar1_Disposed"
            RenderMode="Lightweight" ExpandMode="SingleExpandedItem">
            <CollapseAnimation Duration="100" Type="None" />
            <ExpandAnimation Duration="100" Type="None" />
        </telerik:RadPanelBar>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
    </div>
</div>
