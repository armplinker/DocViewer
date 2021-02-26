<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="~/Controls/UserControl1.ascx.cs" Inherits="DocViewer.Controls.UserControl1" %>
 

<telerik:RadAjaxManagerProxy runat="server" ID="UCDEMORAM1_Proxy">
    <AjaxSettings>
       
        <telerik:AjaxSetting AjaxControlID="XmlDataSource1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadPanelBar1"  />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RadPanelBar1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>

<telerik:RadPersistenceManagerProxy ID="UCDEMO_RadPersistenceManagerProxy1" runat="server" /> <%--UniqueKey="UCDEMORRPM1"/>--%>

<asp:Label runat="server" id="lblPlaceholder1">1.</asp:Label>
<asp:Button ID="Button1" runat="server" Text="Open RadWindow" OnClientClick="openWin(); return false;" />
<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>