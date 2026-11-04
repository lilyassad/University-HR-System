using System;
using System.Data;

public partial class Admin_UpdateEmploymentStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Auth.Require(this, Auth.Admin);
        if (!IsPostBack) Show();
    }

    protected void btnRun_Click(object sender, EventArgs e)
    {
        try
        {
            // The M2 procedure takes ONE employee ID, so run it for every employee.
            DataTable ids = Db.Query("SELECT employee_ID FROM Employee");
            string info;
            int done = 0;
            foreach (DataRow r in ids.Rows)
            {
                Db.Proc("Update_Employment_Status", out info, Db.P("@Employee_ID", (int)r["employee_ID"]));
                done++;
            }
            Ui.Ok(lblMsg, "Employment status refreshed for " + done + " employee(s).");
            Show();
        }
        catch (Exception ex) { Ui.Fail(lblMsg, "Could not update employment status: " + Ui.Explain(ex)); }
    }

    private void Show()
    {
        try { Ui.Bind(gv, Db.Query("SELECT employee_ID, first_name, last_name, dept_name, employment_status FROM Employee ORDER BY employee_ID")); }
        catch (Exception) { }
    }
}
