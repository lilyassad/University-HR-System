using System;

public partial class Admin_RemoveHoliday : System.Web.UI.Page
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
            string info;
            int rows = Db.Proc("Remove_Holiday", out info);
            Ui.Ok(lblMsg, "Done. Removed " + rows + " attendance record(s) that fell on official holidays." + (info != "" ? " " + info : ""));
            Show();
        }
        catch (Exception ex)
        {
            Ui.Fail(lblMsg, "Could not remove holiday attendance: " + Ui.Explain(ex) + " (If the Holiday table does not exist yet, run Create_Holiday and Add_Holiday first.)");
        }
    }

    private void Show() { try { Ui.Bind(gv, Db.Query("SELECT * FROM Attendance ORDER BY date DESC, emp_ID")); } catch (Exception) { } }
}
