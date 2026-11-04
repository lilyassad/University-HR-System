<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UpperBoard.aspx.cs" Inherits="Academic_UpperBoard" Title="Approvals" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Pending leaves for review</h2>
    <p class="sub">Annual and unpaid leaves. Pressing Review applies the university rules and sets the result.</p>
    <asp:Label ID="lblMsg" runat="server" Visible="false" />
    <asp:GridView ID="gvLeaves" runat="server" CssClass="grid" AutoGenerateColumns="false"
        EmptyDataText="No pending annual or unpaid leaves." OnRowCommand="gvLeaves_RowCommand">
        <Columns>
            <asp:BoundField DataField="request_ID" HeaderText="Request" />
            <asp:BoundField DataField="emp_name" HeaderText="Employee" />
            <asp:BoundField DataField="leave_type" HeaderText="Type" />
            <asp:BoundField DataField="start_date" HeaderText="From" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="end_date" HeaderText="To" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="replacement_emp" HeaderText="Replacement" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button runat="server" Text="Review" CssClass="btn" CommandName="Review"
                        CommandArgument='<%# Eval("request_ID") + "|" + Eval("leave_type") + "|" + Eval("replacement_emp") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Panel ID="pnlEval" runat="server" Visible="false">
        <h2 style="margin-top:32px">Evaluate an employee (Dean)</h2>
        <p class="sub">Employees of your department.</p>
        <div class="panel">
            <div class="row"><label>Employee</label><asp:DropDownList ID="ddlEmployees" runat="server" /></div>
            <div class="row"><label>Rating (1-5)</label><asp:TextBox ID="txtRating" runat="server" TextMode="Number" /></div>
            <div class="row"><label>Semester (3 characters, e.g. W25 or S26)</label><asp:TextBox ID="txtSemester" runat="server" MaxLength="3" /></div>
            <div class="row"><label>Comment</label><asp:TextBox ID="txtComment" runat="server" MaxLength="50" /></div>
            <asp:Button ID="btnEval" runat="server" Text="Submit evaluation" CssClass="btn green" OnClick="btnEval_Click" />
            <asp:Label ID="lblEval" runat="server" Visible="false" />
        </div>
    </asp:Panel>
</asp:Content>
