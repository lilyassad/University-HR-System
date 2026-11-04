using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class HR_Leaves : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Auth.Require(this, Auth.HR);
        if (!IsPostBack) LoadLeaves();
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e) { LoadLeaves(); }

    private void LoadLeaves()
    {
        try
        {
            string filter = ddlType.SelectedValue;
            DataTable dt = Db.Query(@"
                SELECT * FROM (
                    SELECT l.request_ID, x.emp_ID, e.first_name + ' ' + e.last_name AS emp_name, x.leave_type,
                           l.start_date, l.end_date, l.num_days, l.date_of_request
                    FROM [Leave] l
                    JOIN (
                        SELECT request_ID, emp_ID, 'Annual' AS leave_type FROM Annual_Leave
                        UNION ALL SELECT request_ID, emp_ID, 'Accidental' FROM Accidental_Leave
                        UNION ALL SELECT request_ID, Emp_ID, 'Unpaid' FROM Unpaid_Leave
                        UNION ALL SELECT request_ID, emp_ID, 'Compensation' FROM Compensation_Leave
                    ) x ON x.request_ID = l.request_ID
                    JOIN Employee e ON e.employee_ID = x.emp_ID
                    WHERE l.final_approval_status = 'Pending'
                ) q
                WHERE (@t = 'All' OR q.leave_type = @t)
                ORDER BY q.date_of_request, q.request_ID", Db.P("@t", filter));
            Ui.Bind(gv, dt);
        }
        catch (Exception ex) { Ui.Fail(lblMsg, "Could not load leaves: " + Ui.Explain(ex)); }
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Process") return;
        string[] parts = ((string)e.CommandArgument).Split('|');
        int requestId = int.Parse(parts[0]);
        string type = parts[1];

        string proc = type == "Unpaid" ? "HR_approval_Unpaid"
                    : type == "Compensation" ? "HR_approval_comp"
                    : "HR_approval_an_acc";   // annual + accidental

        try
        {
            string info;
            Db.Proc(proc, out info, Db.P("@request_ID", requestId), Db.P("@HR_ID", Auth.EmpId));
            object status = Db.Scalar("SELECT final_approval_status FROM [Leave] WHERE request_ID = @id", Db.P("@id", requestId));
            string text = "Leave " + requestId + " (" + type + ") processed. Status: " + status + "." + (info != "" ? " " + info : "");
            if (string.Equals(Convert.ToString(status), "Rejected", StringComparison.OrdinalIgnoreCase)) Ui.Info(lblMsg, text);
            else Ui.Ok(lblMsg, text);
            LoadLeaves();
        }
        catch (Exception ex) { Ui.Fail(lblMsg, "Could not process leave " + requestId + ": " + Ui.Explain(ex)); }
    }
}
