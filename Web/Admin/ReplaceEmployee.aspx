<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ReplaceEmployee.aspx.cs" Inherits="Admin_ReplaceEmployee" Title="Replace an employee" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Replace an employee</h2>
    <p class="sub">Employee 1 is replaced by Employee 2 for the chosen dates.</p>
    <div class="panel">
        <div class="row"><label>Employee being replaced (ID)</label><asp:TextBox ID="txtOld" runat="server" /></div>
        <div class="row"><label>Replacement employee (ID)</label><asp:TextBox ID="txtNew" runat="server" /></div>
        <div class="row"><label>From</label><asp:TextBox ID="txtFrom" runat="server" TextMode="Date" /></div>
        <div class="row"><label>To</label><asp:TextBox ID="txtTo" runat="server" TextMode="Date" /></div>
        <asp:Button ID="btnRun" runat="server" Text="Replace" CssClass="btn" OnClick="btnRun_Click" />
        <asp:Label ID="lblMsg" runat="server" Visible="false" />
    </div>
    <asp:GridView ID="gv" runat="server" CssClass="grid" AutoGenerateColumns="true" EmptyDataText="No records found." />
</asp:Content>
