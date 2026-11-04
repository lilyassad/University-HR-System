<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RemoveHoliday.aspx.cs" Inherits="Admin_RemoveHoliday" Title="Holiday cleanup" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Holiday cleanup</h2>
    <p class="sub">Remove attendance records that fall inside official holidays.</p>
    <div class="panel">
        <asp:Button ID="btnRun" runat="server" Text="Remove holiday attendance" CssClass="btn red" OnClick="btnRun_Click" />
        <asp:Label ID="lblMsg" runat="server" Visible="false" />
    </div>
    <asp:GridView ID="gv" runat="server" CssClass="grid" AutoGenerateColumns="true" EmptyDataText="No records found." />
</asp:Content>
