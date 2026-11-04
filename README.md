# University HR Management System 

## Project Structure

- **Database/**: SQL Server script (schema, views, procedures, sample data)
- **Web/**: ASP.NET Web Forms site
  - `Web.config`: connection string and admin login
  - `Site.master`: shared layout and role-based menu
  - `Login.aspx`: single sign-in for all roles
  - `Default.aspx`: home page
  - `App_Code/Db.cs`: data access and login helpers
  - `Content/site.css`: styling
  - **Admin/**: attendance, performance, holidays, days off, leaves, employee replacement, employment status
  - **Academic/**: leave submission, approvals and Dean evaluation
  - **HR/**: leave approvals, deductions, payroll
