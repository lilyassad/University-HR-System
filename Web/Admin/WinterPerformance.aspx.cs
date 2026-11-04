using System;

public partial class Admin_WinterPerformance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Auth.Require(this, Auth.Admin);
        if (!IsPostBack) LoadData();
    }
    protected void btnLoad_Click(object sender, EventArgs e) { LoadData(); }

    private void LoadData()
    {
        try
        {
            System.Data.DataTable dt = Db.Query("SELECT * FROM allPerformance");
            Ui.Bind(gv, dt);
            Ui.Ok(lblMsg, "Loaded " + dt.Rows.Count + " Winter performance record(s).");
        }
        catch (Exception ex) { Ui.Fail(lblMsg, "Could not load performance: " + Ui.Explain(ex)); }
    }
}
