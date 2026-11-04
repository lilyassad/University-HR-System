<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RemoveDayOff.aspx.cs" Inherits="Admin_RemoveDayOff" Title="Remove unattended day off" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Remove unattended day off</h2>
    <p class="sub">Remove an employee's unattended official day off from this month's attendance.</p>
    <div class="panel">
        <div class="row"><label>Employee ID</label><asp:TextBox ID="txtEmp" runat="server" /></div>
        <asp:Button ID="btnRun" runat="server" Text="Remove day off" CssClass="btn red" OnClick="btnRun_Click" />
        <asp:Label ID="lblMsg" runat="server" Visible="false" />
    </div>
    <asp:GridView ID="gv" runat="server" CssClass="grid" AutoGenerateColumns="true" EmptyDataText="No records found." />
</asp:Content>
