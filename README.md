# University HR Management System 

One project: SQL Server database (Milestone 2) plus an ASP.NET Web Forms site (Milestone 3) that uses it.

```
HR_System/
  Database/University_HR_ManagementSystem_Team_20.sql   <- the latest of your three SQL files
  Web/                                                   <- the unified website
    Web.config          one connection string + admin login
    Site.master         shared layout and role-based menu
    Login.aspx          one sign-in for Admin / Employee / HR
    Default.aspx        home page with cards per role
    App_Code/Db.cs      shared data access, login/role helpers, message helpers
    Admin/  Academic/  HR/
```

## Run it
1. In SQL Server (Management Studio): open the `.sql` file and run it. It creates the database, runs `createAllTables`, then loads the views, procedures, functions and sample data. Run it once on a fresh server; it starts with `CREATE DATABASE`.
2. In `Web/Web.config` set `Data Source` to your SQL Server instance (default is `.\SQLEXPRESS`).
3. In Visual Studio: **File > Open > Web Site**, pick the `Web` folder, press F5. Start at `Login.aspx`.
4. Sign in as Admin with the `AdminID` / `AdminPassword` values in `Web.config` (defaults `admin` / `admin123`, change them), or with an employee ID and password from the sample data.

This is a Web Site project (no .csproj, no designer files), so the code uses C# 5 syntax only. Nothing here was compiled or run against SQL Server in the environment it was built in, so do one test pass before relying on it.

## Where each M3 part stands
| Part | Status |
|---|---|
| Admin part 2 (attendance yesterday, Winter performance, remove holiday attendance, remove day off, remove approved leaves, replace employee, update employment status) | Included |
| Academic part 2 (accidental / medical / unpaid / compensation leave, approve annual and unpaid, Dean evaluation) | Included. Annual leave application added to the same form |
| HR (login, approve annual/accidental, unpaid, compensation, 3 deductions, payroll) | Included |
| Admin part 1 (view profiles, employees per department, rejected medicals, remove resigned deductions, update today's attendance, add holiday, initiate attendance) | **Not provided yet.** The M2 views and procedures exist (`allEmployeeProfiles`, `NoEmployeeDept`, `allRejectedMedicals`, `Remove_Deductions`, `Update_Attendance`, `Add_Holiday`, `Initiate_Attendance`). Admin login is built in |
| Academic part 1 (my performance, my attendance, last month's payroll, attendance deductions, annual leave status) | **Not provided yet.** Employee login is built in. `MyPerformance`, `MyAttendance`, `Last_month_payroll`, `Deductions_Attendance` exist in SQL. Annual-leave status shows on the Apply for leave page |
| M1 (EERD and schema PDFs) | Not provided |

Add each missing page under `Admin/` or `Academic/` as a content page of `Site.master`, call `Auth.Require(this, ...)` in `Page_Load`, use `Db.Query` / `Db.Proc` for data, and add its link in `Site.master.cs` and `Default.aspx.cs`.

## What changed from your original pages
- One namespace-free page set, one master page, one connection string (HR pages pointed to a database called `University_HR_ManagementSystem`, the others to `MyDatabaseConnection` / `UniversityHRConnection`).
- HR leaves, deductions and payroll now call the M2 procedures. Before, they ran their own `UPDATE`/`INSERT` and skipped the balance and approval rules, and the deduction page offered types the database rejects ("Late", "Penalty").
- Apply for leave: the old page inserted into `Leave` itself and passed parameters that do not match the M2 procedures, so it would have failed. It now calls `Submit_*` with the exact M2 signatures. Added the missing insurance checkbox.
- Upper Board: Dean evaluation sent `"Winter"` as the semester (the column is `char(3)`), and annual approval used a hard-coded replacement ID `1`. It now takes a 3-character semester and the leave's real replacement.
- Update employment status: the old page called the procedure with no ID, but the procedure needs one. It now runs it for every employee.
- Missing `RemoveDayOff` code-behind written. Duplicate pasted pages dropped. Malformed `<br />` in `HRLogin.aspx` gone.
- Every action reports success or the reason it failed; PRINT messages from procedures are shown to the user.

## Problems in the SQL (left as submitted, please review)
1. **`dropAllProceduresFunctionsViews` is out of date.** It drops 14 names that do not exist in the script (`allApprovedLeaves`, `allDeductionUnpaid`, `allEmployeeRank`, `allValidContracts`, `Dean_approve_annual`, `DeanandHR_Evaluation`, `Delete_Doc_Employee`, `EmployeePayroll`, `Medical_approval`, `MyResignation`, `New_Department`, `New_Role`, `Personal_info`, `Intitiate_Attendance`) and misses 8 that do (`Dean_andHR_Evaluation`, `Deduction_days`, `getCorrespondingHR`, `getCorrespondingHR_Manager`, `getHigherRankEmployee`, `Initiate_Attendance`, `Update_Employment_Status`, `Upperboard_approve_annual`).
2. **Procedure name.** The M2 brief spells it `Intitiate_Attendance`; your script defines `Initiate_Attendance`. The brief says names must match exactly.
3. **`Add_Payroll`** sums deductions for the current month, ignoring its `@from` / `@to` parameters, so payroll for another month uses the wrong deductions.
4. Older versions you uploaded (`milestone_3.sql`, `finalllllllSub.sql`) are superseded: `milestone_3.sql` is cut off mid-file, and `finalllllllSub.sql` has the older `Create_Holiday` / `HR_approval_comp`.
