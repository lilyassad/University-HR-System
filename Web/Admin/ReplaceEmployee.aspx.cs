using System;

public partial class Admin_ReplaceEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Auth.Require(this, Auth.Admin);
        if (!IsPostBack) Show();
    }

    protected void btnRun_Click(object sender, EventArgs e)
    {
        int oldId, newId;
        DateTime from, to;
        if (!int.TryParse(txtOld.Text.Trim(), out oldId) || !int.TryParse(txtNew.Text.Trim(), out newId))
        {
            Ui.Fail(lblMsg, "Both employee IDs must be numbers.");
            return;
        }
        if (!DateTime.TryParse(txtFrom.Text, out from) || !DateTime.TryParse(txtTo.Text, out to))
        {
            Ui.Fail(lblMsg, "Pick a valid From and To date.");
            return;
        }
        if (to < from)
        {
            Ui.Fail(lblMsg, "The To date must not be before the From date.");
            return;
        }
        if (oldId == newId)
        {
            Ui.Fail(lblMsg, "An employee cannot replace themselves. Choose a different replacement.");
            return;
        }
        try
        {
            string info;
            Db.Proc("Replace_employee", out info,
                Db.P("@Emp1_ID", oldId), Db.P("@Emp2_ID", newId),
                Db.P("@from_date", from), Db.P("@to_date", to));
            Ui.Ok(lblMsg, "Employee " + newId + " now replaces employee " + oldId + " from " + from.ToString("yyyy-MM-dd") + " to " + to.ToString("yyyy-MM-dd") + "." + (info != "" ? " " + info : ""));
            Show();
        }
        catch (Exception ex) { Ui.Fail(lblMsg, "Could not save the replacement: " + Ui.Explain(ex)); }
    }

    private void Show() { try { Ui.Bind(gv, Db.Query("SELECT * FROM Employee_Replace_Employee")); } catch (Exception) { } }
}
