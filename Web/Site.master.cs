using System;
using System.Text;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string role = Auth.Role;
        StringBuilder nav = new StringBuilder();

        if (role == null)
        {
            lblWho.Text = "Not signed in";
        }
        else
        {
            nav.Append(Link("~/Default.aspx", "Home"));

            if (role == Auth.Admin)
            {
                lblWho.Text = "Admin";
                nav.Append(Link("~/Admin/AttendanceYesterday.aspx", "Yesterday's attendance"));
                nav.Append(Link("~/Admin/WinterPerformance.aspx", "Winter performance"));
                nav.Append(Link("~/Admin/RemoveHoliday.aspx", "Holiday cleanup"));
                nav.Append(Link("~/Admin/RemoveDayOff.aspx", "Remove day off"));
                nav.Append(Link("~/Admin/RemoveApprovedLeaves.aspx", "Remove approved leaves"));
                nav.Append(Link("~/Admin/ReplaceEmployee.aspx", "Replace employee"));
                nav.Append(Link("~/Admin/UpdateEmploymentStatus.aspx", "Employment status"));
            }
            else if (role == Auth.Employee)
            {
                lblWho.Text = "Employee #" + Auth.EmpId;
                nav.Append(Link("~/Academic/SubmitLeave.aspx", "Apply for leave"));
                if (Auth.IsUpperBoardApprover)
                    nav.Append(Link("~/Academic/UpperBoard.aspx", Auth.HasRole("Dean") ? "Approvals & evaluation" : "Approvals"));
            }
            else if (role == Auth.HR)
            {
                lblWho.Text = "HR #" + Auth.EmpId;
                nav.Append(Link("~/HR/Leaves.aspx", "Leaves"));
                nav.Append(Link("~/HR/Deductions.aspx", "Deductions"));
                nav.Append(Link("~/HR/Payroll.aspx", "Payroll"));
            }
            lnkLogout.Visible = true;
        }
        litNav.Text = nav.ToString();
    }

    private string Link(string url, string text)
    {
        return "<a href=\"" + ResolveUrl(url) + "\">" + text + "</a>";
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/Login.aspx");
    }
}
