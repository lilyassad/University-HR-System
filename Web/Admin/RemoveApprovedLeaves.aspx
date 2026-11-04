<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RemoveApprovedLeaves.aspx.cs" Inherits="Admin_RemoveApprovedLeaves" Title="Remove approved leaves" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Remove approved leaves</h2>
    <p class="sub">Remove an employee's approved leaves from the attendance records.</p>
    <div class="panel">
        <div class="row"><label>Employee ID</label><asp:TextBox ID="txtEmp" runat="server" /></div>
        <asp:Button ID="btnRun" runat="server" Text="Remove approved leaves" CssClass="btn red" OnClick="btnRun_Click" />
        <asp:Label ID="lblMsg" runat="server" Visible="false" />
    </div>
    <asp:GridView ID="gv" runat="server" CssClass="grid" AutoGenerateColumns="true" EmptyDataText="No records found." />
</asp:Content>
