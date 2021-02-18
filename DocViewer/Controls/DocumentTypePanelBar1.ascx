<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="~/Controls/DocumentTypePanelBar1.ascx.cs" Inherits="DocViewer.Controls.DocumentTypePanelBar1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadPersistenceManagerProxy ID="RadPersistenceManagerProxy1" runat="server">
    <PersistenceSettings>
        <telerik:PersistenceSetting ControlID="RadPanelBar1" />
    </PersistenceSettings>
</telerik:RadPersistenceManagerProxy>
<div>
 
    <div class="demo-settings">
        <telerik:RadButton RenderMode="Lightweight" ID="SaveButton" runat="server" Text="Save state" OnClick="SaveButton_Click" />
        <telerik:RadButton RenderMode="Lightweight" ID="ResetButton" runat="server" Text="Reset state" OnClick="ResetButton_Click" />
        <telerik:RadButton RenderMode="Lightweight" ID="LoadButton" runat="server" Text="Load state" OnClick="LoadButton_Click" />
    </div>
    <div>  
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/MenuItems.xml" XPath="/menutree/Items/Item"></asp:XmlDataSource>
        <telerik:RadPanelBar ID="RadPanelBar1" runat="server" DataSourceID="XmlDataSource1" DataTextField="Text" DataValueField="Text" DataModelID="Url" DataNavigateUrlField="Url" RenderMode="Lightweight"></telerik:RadPanelBar>
    </div>
</div>
