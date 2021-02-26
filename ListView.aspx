<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="ListView.aspx.cs" Inherits="DocViewer.ListView" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<%--COMMON CONTENT--%>
<asp:Content runat="server" ID="CommonContent1" ContentPlaceHolderID="CommonContentPlaceHolder1">
    <asp:ScriptManagerProxy runat="server" ID="LV_ScriptManagerProxy1"></asp:ScriptManagerProxy>
    <telerik:RadAjaxManagerProxy runat="server" ID="LV_RadAjaxManager1">
    </telerik:RadAjaxManagerProxy>
    <telerik:RadPersistenceManagerProxy runat="server" ID="LV_RadPersistenceManagerProxy1" UniqueKey="LVRPM1"></telerik:RadPersistenceManagerProxy>
    <telerik:RadWindowManager runat="server" ID="LV_RadWindowManager1"></telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="LV_RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
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
                    const msg = `${"LV_restoreState"}*${"logdebug"}*${"ajaxRequest window.onload"}`;
                    window.LogFromJavaScript(msg);
                    // ReSharper disable once StringLiteralTypo
                    oManager.ajaxRequest(`${"LV_restoreState"}*${"action"}*${"loadstatex"}*${""}`);
                }

            }


           // window.onbeforeunload = confirmExit;
            // register your event handler
            //setEventHandler(this, "beforeunload", confirmExit); // register your event handler - no on in the event name
            function confirmExit() {

                const oManager = $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>");

                if (oManager) {
                    const msg = `${"LV_confirmExit"}*${"logdebug"}*${"ajaxRequest window.onbeforeunload"}`;
                    window.LogFromJavaScript(msg);
                    oManager.ajaxRequest(`${"LV_confirmExit"}*${"action"}*${"savestatex"}*${""}`);
                }

            }

            //window.onfocusout = confirmExit2;
            // register your event handler
            //setEventHandler(this, "focusout", confirmExit2); // register your event handler - no on in the event name
            function confirmExit2() {

                const oManager = $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>");

                if (oManager) {
                    const msg = `${"LV_confirmExit2"}*${"logdebug"}*${"ajaxRequest window.onfocusout"}`;
                    window.LogFromJavaScript(msg);
                    oManager.ajaxRequest(`${"LV_confirmExit2"}*${"action"}*${"savestatex"}*${""}`);
                }

            }

            //window.onblur = confirmExit3;
            // register your event handler
            //setEventHandler(this, "focusout", confirmExit2); // register your event handler - no on in the event name
            function confirmExit3() {

                const oManager = $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>");
                if (oManager) {
                    const msg = `${"LV_confirmExit3"}*${"logdebug"}*${"ajaxRequest window.onblur"}`;
                    window.LogFromJavaScript(msg);
                    oManager.ajaxRequest(`${"LV_confirmExit3"}*${"action"}*${"savestatex"}*${""}`);
                }

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

</asp:Content>

