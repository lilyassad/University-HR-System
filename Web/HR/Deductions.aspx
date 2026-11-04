<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Deductions.aspx.cs" Inherits="HR_Deductions" Title="Deductions" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Add deduction</h2>
    <p class="sub">The amount is calculated by the database from the employee's salary and records.</p>
    <div class="panel">
        <div class="row"><label>Employee ID</label><asp:TextBox ID="txtEmp" runat="server" /></div>
        <div class="row">
            <label>Deduction type</label>
            <asp:DropDownList ID="ddlType" runat="server">
                <asp:ListItem Value="hours">Missing hours</asp:ListItem>
                <asp:ListItem Value="days">Missing days</asp:ListItem>
                <asp:ListItem Value="unpaid">Unpaid leave</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Button ID="btnAdd" runat="server" Text="Add deduction" CssClass="btn" OnClick="btnAdd_Click" />
        <asp:Label ID="lblMsg" runat="server" Visible="false" />
    </div>
    <h3>Deductions of this employee</h3>
    <asp:GridView ID="gv" runat="server" CssClass="grid" AutoGenerateColumns="true" EmptyDataText="No deductions yet." />
</asp:Content>
