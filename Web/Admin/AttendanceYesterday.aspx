<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AttendanceYesterday.aspx.cs" Inherits="Admin_AttendanceYesterday" Title="Yesterday's attendance" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Yesterday's attendance</h2>
    <p class="sub">Attendance records of all employees for yesterday.</p>
    <div class="panel">
        <asp:Button ID="btnLoad" runat="server" Text="Refresh" CssClass="btn" OnClick="btnLoad_Click" />
        <asp:Label ID="lblMsg" runat="server" Visible="false" />
    </div>
    <asp:GridView ID="gv" runat="server" CssClass="grid" AutoGenerateColumns="true" EmptyDataText="No records found." />
</asp:Content>
