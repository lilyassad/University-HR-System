using System;
using System.Data;

public partial class HR_Payroll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Auth.Require(this, Auth.HR);
        if (!IsPostBack)
        {
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            txtYear.Text = DateTime.Now.Year.ToString();
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        int year;
        if (!int.TryParse(txtYear.Text.Trim(), out year) || year < 2000 || year > 2100)
        {
            Ui.Fail(lblMsg, "Enter a valid 4-digit year.");
            return;
        }
        int month = int.Parse(ddlMonth.SelectedValue);
        DateTime from = new DateTime(year, month, 1);
        DateTime to = from.AddMonths(1).AddDays(-1);

        int created = 0, skipped = 0, failed = 0;
        string firstError = "";
        try
        {
            DataTable emps = Db.Query("SELECT employee_ID FROM Employee WHERE employment_status <> 'resigned'");
            foreach (DataRow r in emps.Rows)
            {
                int id = (int)r["employee_ID"];
                int existing = Convert.ToInt32(Db.Scalar(
                    "SELECT COUNT(*) FROM Payroll WHERE emp_ID = @id AND from_date = @f AND to_date = @t",
                    Db.P("@id", id), Db.P("@f", from), Db.P("@t", to)));
                if (existing > 0) { skipped++; continue; }
                try
                {
                    string info;
                    Db.Proc("Add_Payroll", out info, Db.P("@employee_ID", id), Db.P("@from", from), Db.P("@to", to));
                    created++;
                }
                catch (Exception ex)
                {
                    failed++;
                    if (firstError == "") firstError = "Employee " + id + ": " + Ui.Explain(ex);
                }
            }

            string text = "Payroll for " + ddlMonth.SelectedItem.Text + " " + year + ": " + created + " created, " + skipped + " already existed, " + failed + " failed.";
            if (failed > 0) Ui.Fail(lblMsg, text + " First error: " + firstError);
            else Ui.Ok(lblMsg, text);

            gv.DataSource = Db.Query(@"
                SELECT p.ID, p.emp_ID, e.first_name + ' ' + e.last_name AS name, p.from_date, p.to_date,
                       p.bonus_amount, p.deductions_amount, p.final_salary_amount, p.payment_date, p.comments
                FROM Payroll p JOIN Employee e ON e.employee_ID = p.emp_ID
                WHERE p.from_date = @f AND p.to_date = @t
                ORDER BY p.emp_ID", Db.P("@f", from), Db.P("@t", to));
            gv.DataBind();
        }
        catch (Exception ex) { Ui.Fail(lblMsg, "Could not generate payroll: " + Ui.Explain(ex)); }
    }
}
