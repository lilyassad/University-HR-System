<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Payroll.aspx.cs" Inherits="HR_Payroll" Title="Payroll" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Generate monthly payroll</h2>
    <p class="sub">Creates a payroll record for every employee who has not resigned and does not already have one for the chosen month.</p>
    <div class="panel">
        <div class="row">
            <label>Month</label>
            <asp:DropDownList ID="ddlMonth" runat="server">
                <asp:ListItem Value="1">January</asp:ListItem><asp:ListItem Value="2">February</asp:ListItem>
                <asp:ListItem Value="3">March</asp:ListItem><asp:ListItem Value="4">April</asp:ListItem>
                <asp:ListItem Value="5">May</asp:ListItem><asp:ListItem Value="6">June</asp:ListItem>
                <asp:ListItem Value="7">July</asp:ListItem><asp:ListItem Value="8">August</asp:ListItem>
                <asp:ListItem Value="9">September</asp:ListItem><asp:ListItem Value="10">October</asp:ListItem>
                <asp:ListItem Value="11">November</asp:ListItem><asp:ListItem Value="12">December</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="row"><label>Year</label><asp:TextBox ID="txtYear" runat="server" MaxLength="4" /></div>
        <asp:Button ID="btnGenerate" runat="server" Text="Generate payroll" CssClass="btn" OnClick="btnGenerate_Click" />
        <asp:Label ID="lblMsg" runat="server" Visible="false" />
    </div>
    <asp:GridView ID="gv" runat="server" CssClass="grid" AutoGenerateColumns="true" EmptyDataText="No payroll records for this month yet." />
</asp:Content>
