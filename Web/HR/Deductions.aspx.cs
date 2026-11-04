using System;

public partial class HR_Deductions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) { Auth.Require(this, Auth.HR); }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int emp;
        if (!int.TryParse(txtEmp.Text.Trim(), out emp)) { Ui.Fail(lblMsg, "Enter a numeric employee ID."); return; }

        string proc = ddlType.SelectedValue == "hours" ? "Deduction_hours"
                    : ddlType.SelectedValue == "days" ? "Deduction_days"
                    : "Deduction_unpaid";
        try
        {
            if (Db.Scalar("SELECT COUNT(*) FROM Employee WHERE employee_ID = @id", Db.P("@id", emp)).ToString() == "0")
            {
                Ui.Fail(lblMsg, "No employee with ID " + emp + ". Check the ID and try again.");
                return;
            }
            int before = Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM Deduction WHERE emp_ID = @id", Db.P("@id", emp)));
            string info;
            Db.Proc(proc, out info, Db.P("@employee_ID", emp));
            int after = Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM Deduction WHERE emp_ID = @id", Db.P("@id", emp)));

            if (after > before)
                Ui.Ok(lblMsg, (after - before) + " deduction record(s) added for employee " + emp + "." + (info != "" ? " " + info : ""));
            else
                Ui.Info(lblMsg, "Nothing to deduct for employee " + emp + " (" + ddlType.SelectedItem.Text.ToLower() + "), so no record was added." + (info != "" ? " " + info : ""));

            gv.DataSource = Db.Query("SELECT * FROM Deduction WHERE emp_ID = @id ORDER BY date DESC", Db.P("@id", emp));
            gv.DataBind();
        }
        catch (Exception ex) { Ui.Fail(lblMsg, "The deduction was not added: " + Ui.Explain(ex)); }
    }
}
