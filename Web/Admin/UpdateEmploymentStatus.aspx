<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UpdateEmploymentStatus.aspx.cs" Inherits="Admin_UpdateEmploymentStatus" Title="Update employment status" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Update employment status</h2>
    <p class="sub">Marks employees as on leave or active for today. Runs the update for every employee.</p>
    <div class="panel">
        <asp:Button ID="btnRun" runat="server" Text="Update status now" CssClass="btn" OnClick="btnRun_Click" />
        <asp:Label ID="lblMsg" runat="server" Visible="false" />
    </div>
    <asp:GridView ID="gv" runat="server" CssClass="grid" AutoGenerateColumns="true" EmptyDataText="No records found." />
</asp:Content>
