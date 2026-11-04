using System;
using System.Text;

public partial class HomePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string role = Auth.Role;
        if (role == null) Response.Redirect("~/Login.aspx", true);

        StringBuilder sb = new StringBuilder();
        if (role == Auth.Admin)
        {
            lblSub.Text = "Admin tools";
            sb.Append(Card("Yesterday's attendance", "Attendance of all employees for yesterday.", "~/Admin/AttendanceYesterday.aspx"));
            sb.Append(Card("Winter performance", "Performance of all employees in Winter semesters.", "~/Admin/WinterPerformance.aspx"));
            sb.Append(Card("Holiday cleanup", "Remove attendance recorded during official holidays.", "~/Admin/RemoveHoliday.aspx"));
            sb.Append(Card("Remove day off", "Remove an employee's unattended day off this month.", "~/Admin/RemoveDayOff.aspx"));
            sb.Append(Card("Remove approved leaves", "Remove approved leaves from an employee's attendance.", "~/Admin/RemoveApprovedLeaves.aspx"));
            sb.Append(Card("Replace employee", "Assign a replacement for a date range.", "~/Admin/ReplaceEmployee.aspx"));
            sb.Append(Card("Employment status", "Refresh employees' on-leave / active status.", "~/Admin/UpdateEmploymentStatus.aspx"));
        }
        else if (role == Auth.Employee)
        {
            lblSub.Text = "Employee #" + Auth.EmpId;
            sb.Append(Card("Apply for leave", "Annual, accidental, medical, unpaid or compensation leave.", "~/Academic/SubmitLeave.aspx"));
            if (Auth.IsUpperBoardApprover)
                sb.Append(Card("Approvals" + (Auth.HasRole("Dean") ? " & evaluation" : ""), "Approve or reject annual and unpaid leaves" + (Auth.HasRole("Dean") ? ", evaluate your department." : "."), "~/Academic/UpperBoard.aspx"));
        }
        else if (role == Auth.HR)
        {
            lblSub.Text = "HR #" + Auth.EmpId;
            sb.Append(Card("Leaves", "Approve or reject annual, accidental, unpaid and compensation leaves.", "~/HR/Leaves.aspx"));
            sb.Append(Card("Deductions", "Add missing-hours, missing-days or unpaid-leave deductions.", "~/HR/Deductions.aspx"));
            sb.Append(Card("Payroll", "Generate the monthly payroll.", "~/HR/Payroll.aspx"));
        }
        litCards.Text = sb.ToString();
    }

    private string Card(string title, string text, string url)
    {
        return "<div class=\"card\"><h3>" + title + "</h3><p>" + text + "</p><a href=\"" + ResolveUrl(url) + "\">Open</a></div>";
    }
}
