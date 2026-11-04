<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="HomePage" Title="Home" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Welcome</h2>
    <p class="sub"><asp:Label ID="lblSub" runat="server" /></p>
    <div class="cards"><asp:Literal ID="litCards" runat="server" /></div>
</asp:Content>
