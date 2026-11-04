<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Leaves.aspx.cs" Inherits="HR_Leaves" Title="Leave approvals" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Leave approvals</h2>
    <p class="sub">Pending annual, accidental, unpaid and compensation leaves. Pressing Process runs the HR approval rules (balances, documents, replacements).</p>
    <div class="row">
        <label>Leave type</label>
        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
            <asp:ListItem Value="All">All</asp:ListItem>
            <asp:ListItem Value="Annual">Annual</asp:ListItem>
            <asp:ListItem Value="Accidental">Accidental</asp:ListItem>
            <asp:ListItem Value="Unpaid">Unpaid</asp:ListItem>
            <asp:ListItem Value="Compensation">Compensation</asp:ListItem>
        </asp:DropDownList>
    </div>
    <asp:Label ID="lblMsg" runat="server" Visible="false" />
    <asp:GridView ID="gv" runat="server" CssClass="grid" AutoGenerateColumns="false"
        EmptyDataText="No pending leaves of this type." OnRowCommand="gv_RowCommand">
        <Columns>
            <asp:BoundField DataField="request_ID" HeaderText="Request" />
            <asp:BoundField DataField="emp_ID" HeaderText="Employee ID" />
            <asp:BoundField DataField="emp_name" HeaderText="Name" />
            <asp:BoundField DataField="leave_type" HeaderText="Type" />
            <asp:BoundField DataField="start_date" HeaderText="From" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="end_date" HeaderText="To" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="num_days" HeaderText="Days" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button runat="server" Text="Process" CssClass="btn" CommandName="Process"
                        CommandArgument='<%# Eval("request_ID") + "|" + Eval("leave_type") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
