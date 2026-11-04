using System;

public partial class Admin_RemoveApprovedLeaves : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) { Auth.Require(this, Auth.Admin); }

    protected void btnRun_Click(object sender, EventArgs e)
    {
        int id;
        if (!int.TryParse(txtEmp.Text.Trim(), out id))
        {
            Ui.Fail(lblMsg, "Enter a numeric employee ID.");
            return;
        }
        try
        {
            string info;
            int rows = Db.Proc("Remove_Approved_Leaves", out info, Db.P("@employee_id", id));
            Ui.Ok(lblMsg, "Done. Removed " + rows + " approved-leave attendance record(s) for employee " + id + "." + (info != "" ? " " + info : ""));
            Ui.Bind(gv, Db.Query("SELECT * FROM Attendance WHERE emp_ID = @id ORDER BY date DESC", Db.P("@id", id)));
        }
        catch (Exception ex) { Ui.Fail(lblMsg, "Could not remove approved leaves: " + Ui.Explain(ex)); }
    }
}
