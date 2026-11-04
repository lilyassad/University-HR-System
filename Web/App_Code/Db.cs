using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Shared data access + UI helpers used by every page (one place for the connection string).
public static class Db
{
    public static string ConnStr
    {
        get { return ConfigurationManager.ConnectionStrings["HRConnection"].ConnectionString; }
    }

    public static SqlParameter P(string name, object value)
    {
        return new SqlParameter(name, value == null ? DBNull.Value : value);
    }

    public static DataTable Query(string sql, params SqlParameter[] ps)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConnStr))
        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
            if (ps != null) cmd.Parameters.AddRange(ps);
            new SqlDataAdapter(cmd).Fill(dt);
        }
        return dt;
    }

    public static object Scalar(string sql, params SqlParameter[] ps)
    {
        using (SqlConnection con = new SqlConnection(ConnStr))
        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
            if (ps != null) cmd.Parameters.AddRange(ps);
            con.Open();
            return cmd.ExecuteScalar();
        }
    }

    // Runs a stored procedure. 'info' collects PRINT messages the procedure emits
    // (many of the M2 procedures report rejections through PRINT, not errors).
    public static int Proc(string name, out string info, params SqlParameter[] ps)
    {
        StringBuilder sb = new StringBuilder();
        int rows;
        using (SqlConnection con = new SqlConnection(ConnStr))
        using (SqlCommand cmd = new SqlCommand(name, con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            if (ps != null) cmd.Parameters.AddRange(ps);
            con.InfoMessage += delegate(object s, SqlInfoMessageEventArgs e)
            {
                if (sb.Length > 0) sb.Append(" ");
                sb.Append(e.Message);
            };
            con.Open();
            rows = cmd.ExecuteNonQuery();
        }
        info = sb.ToString();
        return rows;
    }
}

public static class Auth
{
    public const string Admin = "Admin";
    public const string Employee = "Employee";
    public const string HR = "HR";

    public static string Role
    {
        get { return HttpContext.Current.Session["Role"] as string; }
    }

    public static int EmpId
    {
        get
        {
            object o = HttpContext.Current.Session["EmpID"];
            return o == null ? 0 : (int)o;
        }
    }

    public static bool HasRole(string roleName)
    {
        string roles = HttpContext.Current.Session["EmpRoles"] as string;
        return roles != null && roles.IndexOf("|" + roleName + "|", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    public static bool IsUpperBoardApprover
    {
        get { return HasRole("Dean") || HasRole("Vice Dean") || HasRole("President"); }
    }

    // Call first thing in Page_Load. Sends the visitor to the login page if the role does not match.
    public static void Require(Page page, params string[] roles)
    {
        string r = Role;
        if (r != null)
        {
            foreach (string allowed in roles)
                if (allowed == r) return;
        }
        page.Response.Redirect("~/Login.aspx?denied=1", true);
    }
}

public static class Ui
{
    public static void Ok(Label l, string text) { l.Text = text; l.CssClass = "msg ok"; l.Visible = true; }
    public static void Fail(Label l, string text) { l.Text = text; l.CssClass = "msg err"; l.Visible = true; }
    public static void Info(Label l, string text) { l.Text = text; l.CssClass = "msg info"; l.Visible = true; }

    public static void Bind(GridView gv, DataTable dt)
    {
        gv.DataSource = dt;
        gv.DataBind();
    }

    // Turns a SQL / .NET exception into something a user can act on.
    public static string Explain(Exception ex)
    {
        SqlException sx = ex as SqlException;
        if (sx != null && sx.Number == 547)
            return "That ID (or a related value) does not exist or violates a rule. Check the IDs/values and try again. (" + sx.Message + ")";
        if (sx != null && sx.Number == 2627)
            return "That record already exists. (" + sx.Message + ")";
        return ex.Message;
    }
}
