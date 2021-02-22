<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeBehind="DocViewerMain.aspx.cs" Inherits="DocViewer.DocViewerMain" %>

<%--<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>--%>
<%@ Register Src="~/Controls/UserControl1.ascx" TagPrefix="uc1" TagName="UserControl1" %>
<%@ Register Src="~/Controls/DocumentTypePanelBar1.ascx" TagPrefix="ucDocsRPB" TagName="DocumentTypePanelBar1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Document Viewer Dialog</title>
    <link href="styles/grid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
           
            /* RadPanelBar */
            function collapseRoots(sender, args) {
                if (args.get_item().get_items().get_count() < 1 && args.get_item().get_parent().get_parent() == null) {
                    for (var i = 0; i < sender.get_items().get_count(); i++) {
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
           

        </script>
    </telerik:RadScriptBlock>

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="Button2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>


    <div>

        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" OnClientClose="OnClientClose">
        </telerik:RadWindowManager>

        <uc1:UserControl1 runat="server" ID="UserControl1" />

        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript"> 
                

                function openWin( ) {
                   
                    var oWnd = window.radopen("Testing/Url_Values_Dialog.aspx", "RadWindow1");
                }
                function OnClientClose(sender, args) {
                    var txtField = $get("<%= UserControl1.TextBox1.ClientID %>");
                    txtField.value = args.get_argument();

                }

            </script>

        </telerik:RadCodeBlock>

        <ucDocsRPB:DocumentTypePanelBar1 runat="server" ID="DocumentTypePanelBar1" />

    </div>

</asp:Content>
