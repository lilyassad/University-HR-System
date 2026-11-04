<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SubmitLeave.aspx.cs" Inherits="Academic_SubmitLeave" Title="Apply for leave" %>
<asp:Content ID="c1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Apply for leave</h2>
    <p class="sub">Your request is routed to the right approvers automatically.</p>
    <div class="panel">
        <div class="row">
            <label>Leave type</label>
            <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                <asp:ListItem Value="">-- Select leave type --</asp:ListItem>
                <asp:ListItem Value="Annual">Annual</asp:ListItem>
                <asp:ListItem Value="Accidental">Accidental (1 day)</asp:ListItem>
                <asp:ListItem Value="Medical">Medical</asp:ListItem>
                <asp:ListItem Value="Unpaid">Unpaid</asp:ListItem>
                <asp:ListItem Value="Compensation">Compensation</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="row"><label><asp:Label ID="lblStartCaption" runat="server" Text="Start date" /></label><asp:TextBox ID="txtStart" runat="server" TextMode="Date" /></div>
        <asp:Panel ID="pnlEnd" runat="server" CssClass="row"><label>End date</label><asp:TextBox ID="txtEnd" runat="server" TextMode="Date" /></asp:Panel>

        <asp:Panel ID="pnlReplacement" runat="server" Visible="false">
            <div class="row"><label>Replacement employee ID</label><asp:TextBox ID="txtReplacement" runat="server" /></div>
        </asp:Panel>

        <asp:Panel ID="pnlMedical" runat="server" Visible="false">
            <div class="row"><label>Medical leave type</label>
                <asp:DropDownList ID="ddlMedicalType" runat="server">
                    <asp:ListItem Value="sick">Sick</asp:ListItem>
                    <asp:ListItem Value="maternity">Maternity</asp:ListItem>
                </asp:DropDownList></div>
            <div class="row"><label>Insurance</label><asp:CheckBox ID="chkInsurance" runat="server" Text=" Covered by insurance" /></div>
            <div class="row"><label>Disability details</label><asp:TextBox ID="txtDisability" runat="server" MaxLength="50" /></div>
        </asp:Panel>

        <asp:Panel ID="pnlDoc" runat="server" Visible="false">
            <div class="row"><label>Document description</label><asp:TextBox ID="txtDocDesc" runat="server" MaxLength="50" /></div>
            <div class="row"><label>Document file</label><asp:FileUpload ID="fuDoc" runat="server" /></div>
        </asp:Panel>

        <asp:Panel ID="pnlCompensation" runat="server" Visible="false">
            <div class="row"><label>Reason</label><asp:TextBox ID="txtReason" runat="server" MaxLength="50" /></div>
            <div class="row"><label>Date of the original (extra) work day</label><asp:TextBox ID="txtOriginal" runat="server" TextMode="Date" /></div>
            <div class="row"><label>Replacement employee ID</label><asp:TextBox ID="txtCompReplacement" runat="server" /></div>
        </asp:Panel>

        <asp:Button ID="btnSubmit" runat="server" Text="Submit request" CssClass="btn" OnClick="btnSubmit_Click" />
        <asp:Label ID="lblMsg" runat="server" Visible="false" />
    </div>

    <h3>My annual and accidental leaves this month</h3>
    <asp:GridView ID="gvStatus" runat="server" CssClass="grid" AutoGenerateColumns="true" EmptyDataText="No annual or accidental leave requests this month." />
</asp:Content>
