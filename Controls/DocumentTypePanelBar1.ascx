<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="~/Controls/DocumentTypePanelBar1.ascx.cs" Inherits="DocViewer.Controls.DocumentTypePanelBar1" %>

<telerik:RadPersistenceManagerProxy ID="RadPersistenceManagerProxy1" runat="server" UniqueKey="DTPB1" />
<asp:ScriptManagerProxy ID="DTPBScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>

<telerik:RadAjaxManagerProxy runat="server" ID="DTPB1RM_Proxy">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="DTPB1RCB_Proxy">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="DTPB1RCB_Proxy" UpdatePanelCssClass="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="XmlDataSource1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" UpdatePanelCssClass="" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RadPanelBar1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" />
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



    <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/MenuItems.xml" XPath="menutree/Items/Item"></asp:XmlDataSource>
    <%--onClientItemClicked="collapseRoots"
              OnClientMouseOver="expandItem"
    --%>
    <telerik:RadCodeBlock runat="server" ID="DTPB1RCB_Proxy">
        <script type="text/javascript">

            
            function collapseRoots(sender, args) {
                if (args.get_item().get_items().get_count() < 1 && args.get_item().get_parent().get_parent() == null) {
                    for (let i = 0; i < sender.get_items().get_count(); i++) {
                        sender.get_items().getItem(0).collapse();
                    }
                }
            }

            function expandItem(sender, args) {
                if (args.get_item().get_items().get_count() !== 0 && args.get_item().get_expanded() === false) {
                    args.get_item().set_expanded(true);
                }
                else {
                    args.get_item().set_expanded(false);
                }
            }

            function getRPBLevel(sender, args) {
                const level = args.get_item().get_level();
                return (level) ? level : 0;
            }

            function showRadPanelBarItemAttributes(sender, args) {


                const level = getRPBLevel(sender, args);//args.get_item().get_level();
                const label0 = document.getElementById("ContentPlaceHolder1_Label0");
                if (level) {

                    label0.innerText = `Item Level: ${level}`;
                }
                else
                    label0.innerText = `Item Level: ${0}`;

                const text = args.get_item().get_text();
                if (text) {
                    const label1 = document.getElementById("ContentPlaceHolder1_Label1");
                    label1.innerText = `Item Text: ${text}`;
                }

                const itemValue = args.get_item().get_value();
                if (itemValue) {
                    const label2 = document.getElementById("ContentPlaceHolder1_Label2");
                    label2.innerText = `Item Value: ${itemValue}`;
                }

                const navigateUrl = args.get_item().get_navigateUrl();

                const label3 = document.getElementById("ContentPlaceHolder1_Label3");
                if (navigateUrl) {
                    label3.innerText = `URL: ${navigateUrl}`;
                } else {
                    label3.innerText = `URL: ${"not set"}`;
                };

                const attributes = args.get_item().get_attributes();

                const docTypekey = attributes.getAttribute("DocTypeKey");
                const label4 = document.getElementById("ContentPlaceHolder1_Label4");
                if (docTypekey) {

                    label4.innerText = `DocTypeKey: ${docTypekey}`;
                }
                else
                    label4.innerText = `DocTypeKey: ${"not set"}`;

                const label5 = document.getElementById("ContentPlaceHolder1_Label5");
                const docSubtypekey = attributes.getAttribute("DocSubTypeKey");
                if (docSubtypekey) {

                    label5.innerText = `DocSubTypeKey: ${docSubtypekey}`;
                }
                else
                    label5.innerText = `DocSubTypeKey: ${"not set"}`;

                const documentClass = attributes.getAttribute("DocumentClass");
                if (documentClass) {
                    const label6 = document.getElementById("ContentPlaceHolder1_Label6");
                    label6.innerText = `DocumentClass: ${documentClass}`;
                }

                const menuItemId = attributes.getAttribute("MenuItemId");
                if (menuItemId) {
                    const label7 = document.getElementById("ContentPlaceHolder1_Label7");
                    label7.innerText = `MenuItemId: ${menuItemId}`;
                }
                const label8 = document.getElementById("ContentPlaceHolder1_Label8");
                const filterExpression = attributes.getAttribute("FilterExpression");
                if (filterExpression) {

                    label8.innerText = `FilterExpression: ${filterExpression}`;
                }
                else
                    label8.innerText = `FilterExpression: ${"not set"}`;

                return false;
            }

            function openDialog(sender, args) {

                const url = args.get_item().get_navigateUrl();

                if (url) {
                    openTestDialog(url,
                        "true",
                        600,
                        400);

                    //let openScript = "";
                    //openScript = String.format("function f(){{openTestDialog('{0}', {1}, {2}, {3});Sys.Application.remove_load(f);}}; Sys.Application.add_load(f);",
                    //    url,
                    //    "true",
                    //    600,
                    //    400);

                    //ScriptManager.RegisterStartupScript(window.Page, window.Page.GetType(), "DTPBOpenScriptKey", openScript, true);
                };
                return false;
            }
            function openTestDialog(url, modal, width, height) {

                const oWnd = window.radopen(url, "RadWindow1");
                oWnd.set_destroyOnClose(true);
                //add checks here in case parameters have not been passed
                oWnd.setSize(width, height);
                oWnd.center();
                oWnd.set_modal(modal);
                return false;
            };

            function onClientItemCollapse(sender, args) {
                return false;
            }

        </script>
    </telerik:RadCodeBlock>

    <asp:Panel runat="server" ID="RPBPanel1">
        <telerik:RadPanelBar ID="RadPanelBar1" runat="server"
            DataSourceID="XmlDataSource1"
            DataTextField="Text"
            DataValueField="Text"
            DataNavigateUrlField="Url"
            AllowCollapseAllItems="True"
            OnClientItemCollapse="onClientItemCollapse"
            OnClientItemClicked="openDialog"
            OnClientMouseOver="showRadPanelBarItemAttributes"
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
    </asp:Panel>


</div>

