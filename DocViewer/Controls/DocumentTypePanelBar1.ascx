<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="~/Controls/DocumentTypePanelBar1.ascx.cs" Inherits="DocViewer.Controls.DocumentTypePanelBar1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadPersistenceManagerProxy ID="RadPersistenceManagerProxy1" runat="server" OnUnload="RadPersistenceManagerProxy1_OnUnload" OnLoad="RadPersistenceManagerProxy1_Load">
    <PersistenceSettings>
        <telerik:PersistenceSetting ControlID="RadPanelBar1" />
    </PersistenceSettings>
</telerik:RadPersistenceManagerProxy>
<telerik:RadAjaxManagerProxy runat="server" ID="DTPB1RM_Proxy">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadPanelBar1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelCssClass="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>

</telerik:RadAjaxManagerProxy>
<div>

    <asp:HiddenField runat="server" ID="currBridgeGd" />
    <asp:HiddenField runat="server" ID="currInspevntGd" />
    <asp:HiddenField runat="server" ID="currEventGd" />
    <asp:HiddenField runat="server" ID="currDocTypeKey" />
    <asp:HiddenField runat="server" ID="currDocSubTypeKey" />
    <asp:HiddenField runat="server" ID="currPonAppUsersGd" />

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
        <telerik:RadCodeBlock runat="server" ID="DTPB1RCB_Proxy">
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


                


                function showItemAttributes(sender, args) {


                    var level = getRPBLevel(args);//args.get_item().get_level();
                    var label0 = document.getElementById("ContentPlaceHolder1_Label0");
                    if (level) {

                        label0.innerText = `Item Level: ${level}`;
                    }
                    else
                        label0.innerText = `Item Level: ${0}`;

                    var text = args.get_item().get_text();
                    if (text) {
                        var label1 = document.getElementById("ContentPlaceHolder1_Label1");
                        label1.innerText = `Item Text: ${text}`;
                    }

                    var itemValue = args.get_item().get_value();
                    if (itemValue) {
                        var label2 = document.getElementById("ContentPlaceHolder1_Label2");
                        label2.innerText = `Item Value: ${itemValue}`;
                    }

                    var navigateUrl = args.get_item().get_navigateUrl();

                    var label3 = document.getElementById("ContentPlaceHolder1_Label3");
                    if (navigateUrl) {

                        label3.innerText = `URL: ${navigateUrl}`;
                    } else {
                        label3.innerText = `URL: ${"not set"}`;
                    };

                    var attributes = args.get_item().get_attributes();
                    var docTypekey = attributes.getAttribute("DocTypeKey");
                    var label4 = document.getElementById("ContentPlaceHolder1_Label4");
                    if (docTypekey) {

                        label4.innerText = `DocTypeKey: ${docTypekey}`;
                    }
                    else
                        label4.innerText = `DocTypeKey: ${"not set"}`;

                    var label5 = document.getElementById("ContentPlaceHolder1_Label5");
                    var docSubtypekey = attributes.getAttribute("DocSubTypeKey");
                    if (docSubtypekey) {

                        label5.innerText = `DocSubTypeKey: ${docSubtypekey}`;
                    }
                    else
                        label5.innerText = `DocSubTypeKey: ${"not set"}`;

                    var documentClass = attributes.getAttribute("DocumentClass");
                    if (documentClass) {
                        var label6 = document.getElementById("ContentPlaceHolder1_Label6");
                        label6.innerText = `DocumentClass: ${documentClass}`;
                    }

                    var menuItemId = attributes.getAttribute("MenuItemId");
                    if (menuItemId) {
                        var label7 = document.getElementById("ContentPlaceHolder1_Label7");
                        label7.innerText = `MenuItemId: ${menuItemId}`;
                    }
                    var label8 = document.getElementById("ContentPlaceHolder1_Label8");
                    var filterExpression = attributes.getAttribute("FilterExpression");
                    if (filterExpression) {

                        label8.innerText = `FilterExpression: ${filterExpression}`;
                    }
                    else
                        label8.innerText = `FilterExpression: ${"not set"}`;


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
                OnClientItemClicked="openDialogWin2"
                OnClientMouseOver="showItemAttributes"
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

