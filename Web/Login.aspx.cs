using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class LoginPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Request.QueryString["denied"] == "1")
            Ui.Info(lblMsg, "Please sign in with the right account type to open that page.");
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string role = ddlRole.SelectedValue;
        string id = txtId.Text.Trim();
        string pass = txtPassword.Text;

        if (id == "" || pass == "")
        {
            Ui.Fail(lblMsg, "Enter both your ID and password.");
            return;
        }

        try
        {
            if (role == Auth.Admin)
            {
                if (id == ConfigurationManager.AppSettings["AdminID"] && pass == ConfigurationManager.AppSettings["AdminPassword"])
                {
                    Session["Role"] = Auth.Admin;
                    Session["EmpID"] = 0;
                    Response.Redirect("~/Default.aspx", true);
                }
                Ui.Fail(lblMsg, "Wrong admin ID or password.");
                return;
            }

            int empId;
            if (!int.TryParse(id, out empId))
            {
                Ui.Fail(lblMsg, "Employee and HR IDs are numbers. Enter your numeric ID.");
                return;
            }

            string fn = role == Auth.HR ? "dbo.HRLoginValidation" : "dbo.EmployeeLoginValidation";
            object ok = Db.Scalar("SELECT " + fn + "(@id, @pw)", Db.P("@id", empId), Db.P("@pw", pass));
            if (ok == null || ok == DBNull.Value || !Convert.ToBoolean(ok))
            {
                Ui.Fail(lblMsg, role == Auth.HR
                    ? "Wrong ID or password, or this ID is not an HR employee."
                    : "Wrong ID or password, or this ID belongs to an HR employee (choose 'HR employee').");
                return;
            }

            Session["Role"] = role;
            Session["EmpID"] = empId;

            // Remember the person's roles so pages can show Dean / Vice Dean / President features.
            DataTable roles = Db.Query("SELECT role_name FROM Employee_Role WHERE emp_ID = @id", Db.P("@id", empId));
            StringBuilder sb = new StringBuilder("|");
            foreach (DataRow r in roles.Rows) sb.Append(r["role_name"].ToString()).Append("|");
            Session["EmpRoles"] = sb.ToString();

            Response.Redirect("~/Default.aspx", true);
        }
        catch (System.Threading.ThreadAbortException) { throw; }
        catch (Exception ex)
        {
            Ui.Fail(lblMsg, "Could not sign in: " + Ui.Explain(ex) + " (Check the connection string in Web.config and that the database script was run.)");
        }
    }
}
