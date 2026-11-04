<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="LoginPage" Title="Sign in" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel" style="max-width:420px;margin:40px auto">
        <h2>Sign in</h2>
        <p class="sub">University HR Management System</p>
        <div class="row">
            <label>I am a</label>
            <asp:DropDownList ID="ddlRole" runat="server">
                <asp:ListItem Value="Employee">Academic / medical employee</asp:ListItem>
                <asp:ListItem Value="HR">HR employee</asp:ListItem>
                <asp:ListItem Value="Admin">Admin</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="row"><label>ID</label><asp:TextBox ID="txtId" runat="server" /></div>
        <div class="row"><label>Password</label><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" /></div>
        <asp:Button ID="btnLogin" runat="server" Text="Sign in" CssClass="btn" OnClick="btnLogin_Click" />
        <asp:Label ID="lblMsg" runat="server" Visible="false" />
    </div>
</asp:Content>
