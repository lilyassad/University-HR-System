using System;

public partial class Admin_RemoveDayOff : System.Web.UI.Page
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
            int rows = Db.Proc("Remove_DayOff", out info, Db.P("@employee_ID", id));
            Ui.Ok(lblMsg, "Done. Removed " + rows + " attendance record(s) of unattended day off for employee " + id + "." + (info != "" ? " " + info : ""));
            Ui.Bind(gv, Db.Query("SELECT * FROM Attendance WHERE emp_ID = @id ORDER BY date DESC", Db.P("@id", id)));
        }
        catch (Exception ex) { Ui.Fail(lblMsg, "Could not remove the day off: " + Ui.Explain(ex)); }
    }
}
