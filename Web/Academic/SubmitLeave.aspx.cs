using System;
using System.IO;

public partial class Academic_SubmitLeave : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Auth.Require(this, Auth.Employee);
        if (!IsPostBack) { ApplyType(); ShowStatus(); }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e) { ApplyType(); }

    private void ApplyType()
    {
        string t = ddlType.SelectedValue;
        pnlReplacement.Visible = t == "Annual";
        pnlMedical.Visible = t == "Medical";
        pnlDoc.Visible = t == "Medical" || t == "Unpaid";
        pnlCompensation.Visible = t == "Compensation";
        pnlEnd.Visible = t != "Accidental" && t != "Compensation";
        lblStartCaption.Text = t == "Compensation" ? "Compensation (day off) date" : (t == "Accidental" ? "Date" : "Start date");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string t = ddlType.SelectedValue;
        if (t == "") { Ui.Fail(lblMsg, "Choose a leave type first."); return; }

        DateTime start, end;
        if (!DateTime.TryParse(txtStart.Text, out start)) { Ui.Fail(lblMsg, "Pick a valid date."); return; }
        end = start;
        if (t == "Annual" || t == "Medical" || t == "Unpaid")
        {
            if (!DateTime.TryParse(txtEnd.Text, out end)) { Ui.Fail(lblMsg, "Pick a valid end date."); return; }
            if (end < start) { Ui.Fail(lblMsg, "The end date must not be before the start date."); return; }
        }

        int me = Auth.EmpId;
        string info = "";
        try
        {
            if (t == "Annual")
            {
                int rep;
                if (!int.TryParse(txtReplacement.Text.Trim(), out rep)) { Ui.Fail(lblMsg, "Enter the replacement employee's numeric ID."); return; }
                Db.Proc("Submit_annual", out info, Db.P("@employee_ID", me), Db.P("@replacement_emp", rep), Db.P("@start_date", start), Db.P("@end_date", end));
            }
            else if (t == "Accidental")
            {
                Db.Proc("Submit_accidental", out info, Db.P("@employee_ID", me), Db.P("@start_date", start), Db.P("@end_date", start));
            }
            else if (t == "Medical")
            {
                string saved;
                if (!ValidateUpload(out saved)) return;
                Db.Proc("Submit_medical", out info, Db.P("@employee_ID", me), Db.P("@start_date", start), Db.P("@end_date", end),
                    Db.P("@medical_type", ddlMedicalType.SelectedValue), Db.P("@insurance_status", chkInsurance.Checked),
                    Db.P("@disability_details", txtDisability.Text.Trim()), Db.P("@document_description", txtDocDesc.Text.Trim()), Db.P("@file_name", saved));
                SaveUpload("Medical", saved);
            }
            else if (t == "Unpaid")
            {
                string saved;
                if (!ValidateUpload(out saved)) return;
                Db.Proc("Submit_unpaid", out info, Db.P("@employee_ID", me), Db.P("@start_date", start), Db.P("@end_date", end),
                    Db.P("@document_description", txtDocDesc.Text.Trim()), Db.P("@file_name", saved));
                SaveUpload("Unpaid", saved);
            }
            else if (t == "Compensation")
            {
                DateTime original;
                int rep;
                if (!DateTime.TryParse(txtOriginal.Text, out original)) { Ui.Fail(lblMsg, "Pick the date of the original work day."); return; }
                if (!int.TryParse(txtCompReplacement.Text.Trim(), out rep)) { Ui.Fail(lblMsg, "Enter the replacement employee's numeric ID."); return; }
                if (txtReason.Text.Trim() == "") { Ui.Fail(lblMsg, "A compensation leave needs a reason."); return; }
                Db.Proc("Submit_compensation", out info, Db.P("@employee_ID", me), Db.P("@compensation_date", start),
                    Db.P("@reason", txtReason.Text.Trim()), Db.P("@date_of_original_workday", original), Db.P("@rep_emp_id", rep));
            }

            Ui.Ok(lblMsg, t + " leave request submitted and sent for approval." + (info != "" ? " Note: " + info : ""));
            ShowStatus();
        }
        catch (Exception ex)
        {
            Ui.Fail(lblMsg, "The request was not submitted: " + Ui.Explain(ex));
        }
    }

    // The procedures store the file name in a varchar(50) column.
    private bool ValidateUpload(out string fileName)
    {
        fileName = "";
        if (!fuDoc.HasFile)
        {
            Ui.Fail(lblMsg, "Attach the supporting document (this leave type requires one).");
            return false;
        }
        fileName = Path.GetFileName(fuDoc.PostedFile.FileName);
        if (fileName.Length > 50)
        {
            Ui.Fail(lblMsg, "The file name is longer than 50 characters. Rename the file and upload it again.");
            return false;
        }
        if (txtDocDesc.Text.Trim() == "")
        {
            Ui.Fail(lblMsg, "Add a short description of the document.");
            return false;
        }
        return true;
    }

    private void SaveUpload(string folder, string fileName)
    {
        string dir = Server.MapPath("~/Uploads/" + folder + "/");
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        fuDoc.SaveAs(Path.Combine(dir, fileName));
    }

    private void ShowStatus()
    {
        try { Ui.Bind(gvStatus, Db.Query("SELECT * FROM dbo.status_leaves(@id)", Db.P("@id", Auth.EmpId))); }
        catch (Exception) { }
    }
}
