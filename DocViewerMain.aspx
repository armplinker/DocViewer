<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="DocViewerMain.aspx.cs" Inherits="DocViewer.DocViewerMain" %>

<%--<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>--%>
<%@ Register Src="~/Controls/UserControl1.ascx" TagPrefix="uc1" TagName="UserControl1" %>
<%@ Register Src="~/Controls/DocumentTypePanelBar1.ascx" TagPrefix="ucDocsRPB" TagName="DocumentTypePanelBar1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Document Viewer Dialog</title>
    <link href="styles/grid.css" rel="stylesheet" />

</asp:Content>
<asp:Content runat="server" ID="CommonContent1" ContentPlaceHolderID="CommonContentPlaceHolder1">
    

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnableScriptCombine="True">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />


            <%--added this to turn on and off debug and provide a wrapper for log.debug....--%>
            <telerik:RadScriptReference Path="~/Content/js/toggle-debug.js" />
            <telerik:RadScriptReference Path="~/Content/js/form-setup.js" />
            <telerik:RadScriptReference Path="~/Content/js/heart-beat.js" />

            <%--Font-Awesome--%>
            <telerik:RadScriptReference Path="~/Content/vendor/fa/js/fontawesome.min.js" />
            <telerik:RadScriptReference Path="~/Content/vendor/fa/js/solid.min.js" />

        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" 
                            OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="LoadButton">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel2" UpdatePanelCssClass="" />

                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ResetButton">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel2" UpdatePanelCssClass="" />

                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Panel1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Panel2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel2" UpdatePanelCssClass="" />

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock runat="server" ID="RadScriptBlock1">
        <script type="text/javascript">

            //window.onbeforeunload = confirmExit;
            window.onpagehide = confirmExit;
            function confirmExit() {
                var oManager = $find("RadAjaxManager1");
                if (oManager) {
                    oManager.ajaxRequest("unloading");
                }
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" OnClientClose="OnClientClose">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="True" OnClientClose="OnClientClose" Style="display: none;">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadPersistenceManager ID="RadPersistenceManager1" runat="server"   
                                   OnSaveCustomSettings="RadPersistenceManager1_SaveCustomSettings"
                                   OnLoadCustomSettings="RadPersistenceManager1_LoadCustomSettings"
                                   OnLoadSettings="RadPersistenceManager1_LoadSettings"
                                   OnSaveSettings="RadPersistenceManager1_SaveSettings">
    </telerik:RadPersistenceManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <asp:Panel runat="server" ID="Panel1" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel2">
            <uc1:UserControl1 runat="server" ID="UserControl1" />
        </asp:Panel>

        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">

                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow)
                        oWindow = window.radWindow;
                    else if (window.frameElement.radWindow)
                        oWindow = window.frameElement.radWindow;
                    return oWindow;
                }

                function openWin() {
                    var url = "Testing/Url_Values_Dialog.aspx";
                    var oManager = GetRadWindowManager();
                    if (oManager) {
                        var oWnd = oManager.open(url, "RadWindow1");
                        oWnd.set_destroyOnClose(true);
                    }

                };

                function OnClientClose(sender, args) {
                    var txtField = $get("<%= UserControl1.TextBox1.ClientID %>");
                    txtField.value = args.get_argument();
                };

            </script>

        </telerik:RadCodeBlock>


        <telerik:RadTextBox ID="RadTextBoxBrkey1" runat="server" EmptyMessage="Enter a bridge key" Label="BRKEY: " LabelWidth="64px" Resize="None" Width="320px" RenderMode="Auto" ToolTip="Enter a BRKEY to show documents">
        </telerik:RadTextBox>
        <br />
        <telerik:RadTextBox ID="RadTextBoxInspkey1" runat="server" EmptyMessage="Enter an inspection key" Label="INSPKEY: " LabelWidth="64px" Resize="None" Width="320px" RenderMode="Auto" ToolTip="Enter a INSPKEY to show inspection documents">
        </telerik:RadTextBox>
        <br />
        <div>
            <telerik:RadButton RenderMode="Auto" ID="SaveButton" runat="server" Text="Save state" OnClick="SaveButton_Click" />
            <telerik:RadButton RenderMode="Auto" ID="ResetButton" runat="server" Text="Reset state" OnClick="ResetButton_Click" />
            <telerik:RadButton RenderMode="Auto" ID="LoadButton" runat="server" Text="Load state" OnClick="LoadButton_Click" />
        </div>
        <br />

        <asp:Panel runat="server" ID="Panel2">


            <ucDocsRPB:DocumentTypePanelBar1 runat="server" ID="DocumentTypePanelBar1" />

            <p>
                <asp:Label ID="Label0" runat="server" Text="Label0"></asp:Label>
            </p>
            <p>
                <asp:Label ID="Label1" runat="server" Text="Label1"></asp:Label>
            </p>
            <p>
                <asp:Label ID="Label2" runat="server" Text="Label2"></asp:Label>
            </p>
            <p>
                <asp:Label ID="Label3" runat="server" Text="Label3"></asp:Label>

            </p>
            <p>
                <asp:Label ID="Label4" runat="server" Text="Label4"></asp:Label>
            </p>
            <p>
                <asp:Label ID="Label5" runat="server" Text="Label5"></asp:Label>
            </p>
            <p>
                <asp:Label ID="Label6" runat="server" Text="Label6"></asp:Label>
            </p>
            <p>
                <asp:Label ID="Label7" runat="server" Text="Label7"></asp:Label>
            </p>
            <p>
                <asp:Label ID="Label8" runat="server" Text="Label8"></asp:Label>
            </p>
        </asp:Panel>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>


    </div>
   
</asp:Content>

