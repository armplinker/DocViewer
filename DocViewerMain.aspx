<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="DocViewerMain.aspx.cs" Inherits="DocViewer.DocViewerMain" %>

<%--<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>--%>
<%@ Register Src="~/Controls/UserControl1.ascx" TagPrefix="uc1" TagName="UserControl1" %>
<%@ Register Src="~/Controls/UcDocumentTypeChooser.ascx" TagPrefix="ucDocsRPB" TagName="UcDocumentTypeChooser" %>

<asp:Content ID="DVM_Head_Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Document Viewer Dialog</title>
    <%--<link href="styles/grid.css" rel="stylesheet" />--%>
   
</asp:Content>


<%--COMMON CONTENT--%>
<asp:Content runat="server" ID="DVM_CommonContent1" ContentPlaceHolderID="CommonContentPlaceHolder1">

    <asp:ScriptManagerProxy runat="server" ID="DVM_ScriptManagerProxy1"/>
    <telerik:RadAjaxManagerProxy runat="server" ID="DVM_RadAjaxManagerProxy1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="SaveButton">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel2" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ResetButton">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel2" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="LoadButton">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel2" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Panel2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel2" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindowManager runat="server" ID="DVM_RadWindowManager1"/>
    <telerik:RadPersistenceManagerProxy runat="server" Id="DVM_RadPersistenceManagerProxy1"/>
    <telerik:RadAjaxLoadingPanel ID="DVM_RadAjaxLoadingPanel1" runat="server" Skin="Default"/>

    <telerik:RadCodeBlock runat="server" ID="DVM_RadCodeBlock1">
        <script type="text/javascript">

            // simplified utility function to register an event handler cross-browser
            function setEventHandler(obj, name, fn) {
                if (typeof obj == "string") {
                    obj = document.getElementById(obj);
                }
                if (obj.addEventListener) {
                    return (obj.addEventListener(name, fn));
                } else if (obj.attachEvent) {
                    return (obj.attachEvent("on" + name, function () { return (fn.call(obj)); }));
                }
                return false;
            };



            //$.ajax(
            //    {
            //        url: window.urlLogError,
            //        type: "POST",
            //        dataType: "text",
            //        cache: false,
            //        data: {
            //            errorMessage: errorMessage
            //        }
            //    });

            function LogFromJavaScript(logMessage) {
                const oManager = $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>");
                if (oManager) {
                    log_debug(logMessage);
                    oManager.ajaxRequest(logMessage);
                }
              
            }

           // window.onload = restoreState;
            // register your event handler
            // setEventHandler(this, "focusin", restoreState); // register your event handler - no on in the event name
            function restoreState() {
              
                 
                const oManager = $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>");
                if (oManager) {
                    // ReSharper disable once StringLiteralTypo
                    const msg = `${"DVM_restoreState"}*${"logdebug"}*${"ajaxRequest window.onload"}`;
                    window.LogFromJavaScript(msg);
                    // ReSharper disable once StringLiteralTypo
                    oManager.ajaxRequest(`${"DVM_restoreState"}*${"action"}*${"loadstatex"}*${""}`);
                }
                //return false;
            }

             //window.onbeforeunload = confirmExit;
            // register your event handler
            //setEventHandler(this, "beforeunload", confirmExit); // register your event handler - no on in the event name
            function confirmExit() {
                const oManager = $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>");
                if (oManager) {
                    const msg = `${"DVM_confirmExit"}*${"logdebug"}*${"ajaxRequest window.onbeforeunload!"}`;
                    window.LogFromJavaScript(msg);
                    oManager.ajaxRequest(`${"DVM_confirmExit"}*${"action"}*${"savestatex"}*${""}`);
                }
               // return false;
            }
           

            //window.onfocusout = confirmExit2;
            // register your event handler
            //setEventHandler(this, "focusout", confirmExit2); // register your event handler - no on in the event name
            function confirmExit2() {
 
               const oManager = $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>");
               if (oManager) {
                    const msg = `${"DVM_confirmExit2"}*${"logdebug"}*${"ajaxRequest window.onfocusout!"}`;
                    window.LogFromJavaScript(msg);
                    oManager.ajaxRequest(`${"DVM_confirmExit2"}*${"action"}*${"savestatex"}*${""}`);
                }
                //return false;
            }

            //window.onblur = confirmExit3;
            // register your event handler
            //setEventHandler(this, "focusout", confirmExit2); // register your event handler - no on in the event name
            function confirmExit3() {
                const oManager = $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>");
                if (oManager) {
                    const msg = `${"DVM_confirmExit3"}*${"logdebug"}*${"ajaxRequest window.onblur!"}`;
                    window.LogFromJavaScript(msg);
                    oManager.ajaxRequest(`${"DVM_confirmExit3"}*${"action"}*${"savestatex"}*${""}`);
                }
                //return false;
            }
        </script>
    </telerik:RadCodeBlock>

</asp:Content>

<asp:Content ID="DVM_Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <asp:Panel runat="server" ID="Panel1">
            <uc1:UserControl1 runat="server" ID="UserControl1" />
        </asp:Panel>

        <telerik:RadCodeBlock ID="DVM_RadCodeBlock2" runat="server">
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
                    var oManager = window.GetRadWindowManager();
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
        <br />
        <telerik:RadTextBox ID="RadTextBoxBrkey1" runat="server" EmptyMessage="Enter a bridge key" Label="BRKEY:" LabelWidth="64px" Resize="None" Width="320px" RenderMode="Auto" ToolTip="Enter a BRKEY to show documents" />
        <br />

        <telerik:RadTextBox ID="RadTextBoxInspkey1" runat="server" EmptyMessage="Enter an inspection key" Label="INSPKEY:" LabelWidth="64px" Resize="None" Width="320px" RenderMode="Auto" ToolTip="Enter a INSPKEY to show inspection documents" />
        <br />
        <br />
        <div>
            <telerik:RadButton RenderMode="Auto" ID="SaveButton" runat="server" Text="Save state" OnClick="SaveButton_Click" />
            <telerik:RadButton RenderMode="Auto" ID="ResetButton" runat="server" Text="Reset state" OnClick="ResetButton_Click" />
            <telerik:RadButton RenderMode="Auto" ID="LoadButton" runat="server" Text="Load state" OnClick="LoadButton_Click"  />
        </div>
        <br />
        <br />
        <asp:Panel runat="server" ID="Panel2">

            <ucDocsRPB:UcDocumentTypeChooser runat="server" ID="UcDocumentTypeChooser1" />

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
       <%-- <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>--%>
    </div>

</asp:Content>

