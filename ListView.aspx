<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="ListView.aspx.cs" Inherits="DocViewer.ListView" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="scripts/scripts.js"></script>
    <%-- <link href="styles/listView.css" rel="stylesheet" />--%>
    <telerik:RadStyleSheetManager runat="server" ID="LV_RadStyleSheetManager1">
        <StyleSheets>
            <telerik:StyleSheetReference Path="~/Content/css/tailwind.css" OrderIndex="1" IsRequiredCss="True" IsCommonCss="True" />
            <telerik:StyleSheetReference Path="~/Content/css/fontawesome.min.css" OrderIndex="2" IsRequiredCss="True" IsCommonCss="True" />
            <telerik:StyleSheetReference Path="~/Content/css/solid.min.css" OrderIndex="3" IsRequiredCss="True" IsCommonCss="True" />
            <telerik:StyleSheetReference Path="~/styles/listView.css" OrderIndex="4" IsRequiredCss="True" IsCommonCss="True" />
        </StyleSheets>
    </telerik:RadStyleSheetManager>
</asp:Content>

<%--COMMON CONTENT--%>
<asp:Content runat="server" ID="CommonContent1" ContentPlaceHolderID="CommonContentPlaceHolder1">

    <telerik:RadScriptManager ID="LV_RadScriptManager1" runat="server" EnableScriptCombine="True">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            <%--added this to turn on and off debug and provide a wrapper for log.debug....--%>
            <telerik:RadScriptReference Path="~/Content/js/toggle-debug.js" />
            <%--Font-Awesome--%>
            <telerik:RadScriptReference Path="~/Content/vendor/fa/js/fontawesome.min.js" />
            <telerik:RadScriptReference Path="~/Content/vendor/fa/js/solid.min.js" />
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="LV_RadAjaxManager1" runat="server"
        DefaultLoadingPanelID="LV_RadAjaxLoadingPanel1"
        OnAjaxRequest="LV_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="Panel1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1"  />
                </UpdatedControls>
            </telerik:AjaxSetting>
           <%-- <telerik:AjaxSetting AjaxControlID="SaveButton">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel2"  />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ResetButton">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel2"  />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="LoadButton">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel2"  />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="Panel2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel2"  />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadPersistenceManager ID="LV_RadPersistenceManager1" runat="server"
        OnSaveCustomSettings="LVRPM1_SaveCustomSettings"
        OnLoadCustomSettings="LVRPM1_LoadCustomSettings"
        OnLoadSettings="LVRPM1_LoadSettings"
        OnSaveSettings="LVRPM1_SaveSettings">
    </telerik:RadPersistenceManager>
    <telerik:RadWindowManager ID="LV_RadWindowManager1" runat="server" OnClientClose="OnClientClose">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Modal="True" OnClientClose="OnClientClose" Style="display: none;">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadScriptBlock runat="server" ID="LV_RadScriptBlock1">
        <script type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.radWindow;
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow;
                return oWindow;
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadCodeBlock runat="server" ID="LV_RadCodeBlock1">
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


 
            function LogFromJavaScript(logMessage) {
                const oManager = $find("<%=LV_RadAjaxManager1.ClientID%>");
                if (oManager) {
                    log_debug(logMessage);
                    oManager.ajaxRequest(logMessage);
                }
                return false;
            }

            window.onload = restoreState;
            // register your event handler
            // setEventHandler(this, "focusin", restoreState); // register your event handler - no on in the event name
            function restoreState() {

                const oManager = $find("<%=LV_RadAjaxManager1.ClientID%>");
                if (oManager) {
                    // ReSharper disable once StringLiteralTypo
                    const msg = `${"ListView_restoreState"}*${"logdebug"}*${"ajaxRequest window.onload"}`;
                    window.LogFromJavaScript(msg);
                    // ReSharper disable once StringLiteralTypo
                    oManager.ajaxRequest(`${"action"}*${"loadstatex"}*${""}`);
                }
                return false;
            }

           
            //window.onbeforeunload = confirmExit;
            // register your event handler
            //setEventHandler(this, "beforeunload", confirmExit); // register your event handler - no on in the event name
            function confirmExit() {
                alert("window.onbeforeunload");
                const oManager = $find("<%=LV_RadAjaxManager1.ClientID%>");
                if (oManager) {
                    const msg = `${"LV_confirmExit"}*${"logdebug"}*${"ajaxRequest window.onbeforeunload!"}`;
                    window.LogFromJavaScript(msg);
                    oManager.ajaxRequest(`${"LV_confirmExit"}*${"action"}*${"savestatex"}*${""}`);
                }
                return false;
            }

            window.onfocusout = confirmExit2;
            // register your event handler
            //setEventHandler(this, "focusout", confirmExit2); // register your event handler - no on in the event name
            function confirmExit2() {
                alert("window.onfocusout");
                const oManager = $find("<%=LV_RadAjaxManager1.ClientID%>");
                if (oManager) {
                    const msg = `${"LV_confirmExit2"}*${"logdebug"}*${"ajaxRequest window.onfocusout!"}`;
                    window.LogFromJavaScript(msg);
                    oManager.ajaxRequest(`${"LV_confirmExit2"}*${"action"}*${"savestatex"}*${""}`);
                }
                return false;
            }
            

            function OnClientClose(sender, args) {
                <%--  var txtField = $get("<%= UserControl1.TextBox1.ClientID %>");
                   txtField.value = args.get_argument();--%>
                return false;
            };
        </script>
    </telerik:RadCodeBlock>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadPageLayout runat="server" ID="JumbotronLayout" CssClass="jumbotron" GridType="Fluid">
        <Rows>
            <telerik:LayoutRow>
                <Columns>
                    <telerik:LayoutColumn Span="10" SpanMd="12" SpanSm="12" SpanXs="12">
                        <h1>H1 title, font size 36px. Duis nibh dolor, rhoncus in euismod at, feugiat id magna.</h1>
                        <h2>H2 Title, font size 30 px.</h2>
                        <telerik:RadButton runat="server" ID="RadButton0" Text="Button" ButtonType="SkinnedButton"></telerik:RadButton>
                    </telerik:LayoutColumn>
                    <telerik:LayoutColumn Span="2" HiddenMd="true" HiddenSm="true" HiddenXs="true">
                        <img src="images/Thumbnails/Desert.jpg" />
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <telerik:RadListView runat="server" OnItemDataBound="RadListViewImages_ItemDataBound" OnNeedDataSource="RadListViewImages_NeedDataSource" ID="RadListViewImages" AllowPaging="true" PageSize="3">
        <LayoutTemplate>
            <div class="listView1">
                <asp:Panel ID="itemPlaceholder" runat="server">
                </asp:Panel>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="listViewItem" onclick="example.imageClicked('<%# Eval("ID") %>')">
                <asp:Image ImageUrl='<%# Eval("ThumbnailUrl") %>' Width="200px" Height="150px" runat="server" ToolTip="Click to view larger image" />
                <p>
                    <asp:Literal runat="server" ID="LabelShortDescription"></asp:Literal>
                </p>

            </div>
        </ItemTemplate>
    </telerik:RadListView>
    <hr />
    <telerik:RadListView runat="server" OnNeedDataSource="RadListViewArticles_NeedDataSource" ID="RadListViewArticles" AllowPaging="true" PageSize="4">
        <LayoutTemplate>
            <div class="listView2">
                <asp:Panel ID="itemPlaceholder" runat="server">
                </asp:Panel>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="listViewItem article">
                <h4><%# Eval("Title") %></h4>
                <%# Eval("Description") %>
            </div>
        </ItemTemplate>
    </telerik:RadListView>
    <telerik:RadLightBox DataImageUrlField="ImageUrl" DataDescriptionField="Description" DataTitleField="Name" runat="server" ID="RadLightBoxImageDetails">
        <ClientSettings>
            <ClientEvents OnLoad="example.radLightBoxLoad" />
        </ClientSettings>
    </telerik:RadLightBox>
    <telerik:RadAjaxLoadingPanel ID="LV_RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>

