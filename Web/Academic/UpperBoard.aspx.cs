using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Academic_UpperBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Auth.Require(this, Auth.Employee);
        if (!Auth.IsUpperBoardApprover) Response.Redirect("~/Default.aspx", true);

        pnlEval.Visible = Auth.HasRole("Dean");
        if (!IsPostBack)
        {
            LoadLeaves();
            if (pnlEval.Visible) LoadEmployees();
        }
    }

    private void LoadLeaves()
    {
        try
        {
            DataTable dt = Db.Query(@"
                SELECT l.request_ID, e.first_name + ' ' + e.last_name AS emp_name,
                       CASE WHEN al.request_ID IS NOT NULL THEN 'Annual' ELSE 'Unpaid' END AS leave_type,
                       l.start_date, l.end_date, al.replacement_emp
                FROM [Leave] l
                LEFT JOIN Annual_Leave al ON al.request_ID = l.request_ID
                LEFT JOIN Unpaid_Leave un ON un.request_ID = l.request_ID
                JOIN Employee e ON e.employee_ID = COALESCE(al.emp_ID, un.Emp_ID)
                WHERE l.final_approval_status = 'Pending'
                  AND (al.request_ID IS NOT NULL OR un.request_ID IS NOT NULL)
                ORDER BY l.date_of_request");
            Ui.Bind(gvLeaves, dt);
        }
        catch (Exception ex) { Ui.Fail(lblMsg, "Could not load pending leaves: " + Ui.Explain(ex)); }
    }

    protected void gvLeaves_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Review") return;
        string[] parts = ((string)e.CommandArgument).Split('|');
        int requestId = int.Parse(parts[0]);
        string type = parts[1];

        try
        {
            string info;
            if (type == "Annual")
            {
                int replacement;
                if (!int.TryParse(parts[2], out replacement))
                {
                    Ui.Fail(lblMsg, "Leave " + requestId + " has no replacement employee, so it cannot be reviewed.");
                    return;
                }
                Db.Proc("Upperboard_approve_annual", out info,
                    Db.P("@request_ID", requestId), Db.P("@Upperboard_ID", Auth.EmpId), Db.P("@replacement_ID", replacement));
            }
            else
            {
                Db.Proc("Upperboard_approve_unpaids", out info,
                    Db.P("@request_ID", requestId), Db.P("@upperboard_ID", Auth.EmpId));
            }

            object status = Db.Scalar("SELECT final_approval_status FROM [Leave] WHERE request_ID = @id", Db.P("@id", requestId));
            Ui.Ok(lblMsg, "Leave " + requestId + " reviewed. Current status: " + status + "." + (info != "" ? " " + info : ""));
            LoadLeaves();
        }
        catch (Exception ex) { Ui.Fail(lblMsg, "Could not review leave " + requestId + ": " + Ui.Explain(ex)); }
    }

    private void LoadEmployees()
    {
        try
        {
            DataTable dt = Db.Query(@"
                SELECT employee_ID, first_name + ' ' + last_name AS name
                FROM Employee
                WHERE dept_name = (SELECT dept_name FROM Employee WHERE employee_ID = @me)
                  AND employee_ID <> @me
                ORDER BY first_name", Db.P("@me", Auth.EmpId));
            ddlEmployees.DataSource = dt;
            ddlEmployees.DataTextField = "name";
            ddlEmployees.DataValueField = "employee_ID";
            ddlEmployees.DataBind();
            ddlEmployees.Items.Insert(0, new ListItem("-- Select employee --", "0"));
        }
        catch (Exception ex) { Ui.Fail(lblEval, "Could not load your department's employees: " + Ui.Explain(ex)); }
    }

    protected void btnEval_Click(object sender, EventArgs e)
    {
        int emp, rating;
        if (!int.TryParse(ddlEmployees.SelectedValue, out emp) || emp == 0) { Ui.Fail(lblEval, "Select an employee."); return; }
        if (!int.TryParse(txtRating.Text, out rating) || rating < 1 || rating > 5) { Ui.Fail(lblEval, "The rating must be a whole number from 1 to 5."); return; }
        string sem = txtSemester.Text.Trim();
        if (sem.Length != 3) { Ui.Fail(lblEval, "The semester must be exactly 3 characters, like W25 or S26."); return; }

        try
        {
            string info;
            Db.Proc("Dean_andHR_Evaluation", out info,
                Db.P("@employee_ID", emp), Db.P("@rating", rating), Db.P("@comment", txtComment.Text.Trim()), Db.P("@semester", sem));
            Ui.Ok(lblEval, "Evaluation saved for employee " + emp + " (" + sem + ", rating " + rating + ").");
            txtRating.Text = ""; txtComment.Text = "";
        }
        catch (Exception ex) { Ui.Fail(lblEval, "The evaluation was not saved: " + Ui.Explain(ex)); }
    }
}
