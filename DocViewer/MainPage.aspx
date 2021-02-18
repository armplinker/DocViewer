<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="DocViewer.MainPage" %>

<%@ Register Src="~/Controls/UserControl1.ascx" TagPrefix="uc1" TagName="UserControl1" %>
<%@ Register Src="~/Controls/DocumentTypePanelBar1.ascx" TagPrefix="ucDocsRPB" TagName="DocumentTypePanelBar1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <script type="text/javascript">
            //Put your JavaScript code here.
        </script>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="Button2">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadPersistenceManager ID="RadPersistenceManager1" runat="server"></telerik:RadPersistenceManager>
        
       

        <div>

            <telerik:RadWindowManager ID="RadWindowManager1" runat="server" OnClientClose="OnClientClose">
            </telerik:RadWindowManager>
            <uc1:UserControl1 runat="server" ID="UserControl1" />
         
            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
               

                <script type="text/javascript">
                    window.$=$telerik.$;
                    function openWin() {
                        var oWnd = radopen("MainPage_Dialog.aspx", "RadWindow1");
                    }
                    function OnClientClose(sender, args) {
                        var txtField = $get("<%= UserControl1.TextBox1.ClientID %>");
                        txtField.value = args.get_argument();

                    }

                </script>

            </telerik:RadCodeBlock>
            <ucDocsRPB:DocumentTypePanelBar1  runat="server" ID="DocumentTypePanelBar1" />
        </div>
    </form>
</body>
</html>
