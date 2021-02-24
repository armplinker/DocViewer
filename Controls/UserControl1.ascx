<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="~/Controls/UserControl1.ascx.cs" Inherits="DocViewer.Controls.UserControl1" %>
 
<asp:Label runat="server" id="lblPlaceholder1">1.</asp:Label>
<asp:Button ID="Button1" runat="server" Text="Open RadWindow" OnClientClick="openWin(); return false;" />
<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>