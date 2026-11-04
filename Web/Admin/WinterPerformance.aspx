<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="WinterPerformance.aspx.cs" Inherits="Admin_WinterPerformance" Title="Winter performance" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Winter performance</h2>
    <p class="sub">Performance evaluations of all employees in all Winter semesters.</p>
    <div class="panel">
        <asp:Button ID="btnLoad" runat="server" Text="Refresh" CssClass="btn" OnClick="btnLoad_Click" />
        <asp:Label ID="lblMsg" runat="server" Visible="false" />
    </div>
    <asp:GridView ID="gv" runat="server" CssClass="grid" AutoGenerateColumns="true" EmptyDataText="No records found." />
</asp:Content>
