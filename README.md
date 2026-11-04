# University HR Management System 

## Project Structure

```
university-hr-system/
├── Database/
│   └── University_HR_ManagementSystem_Team_20.sql   # Schema, views, procedures, functions, sample data
├── Web/
│   ├── Web.config                  # Connection string and admin login
│   ├── Site.master                 # Shared layout and role-based menu
│   ├── Site.master.cs
│   ├── Login.aspx(.cs)             # Single sign-in for Admin / Academic / HR
│   ├── Default.aspx(.cs)           # Home page with cards per role
│   ├── App_Code/
│   │   └── Db.cs                   # Data access, auth/role helpers, message helpers
│   ├── Content/
│   │   └── site.css                # Site styling
│   ├── Admin/
│   │   ├── AttendanceYesterday.aspx(.cs)
│   │   ├── WinterPerformance.aspx(.cs)
│   │   ├── RemoveHoliday.aspx(.cs)
│   │   ├── RemoveDayOff.aspx(.cs)
│   │   ├── RemoveApprovedLeaves.aspx(.cs)
│   │   ├── ReplaceEmployee.aspx(.cs)
│   │   └── UpdateEmploymentStatus.aspx(.cs)
│   ├── Academic/
│   │   ├── SubmitLeave.aspx(.cs)   # Apply for annual, accidental, medical, unpaid, compensation leave
│   │   └── UpperBoard.aspx(.cs)    # Approvals and Dean evaluation
│   └── HR/
│       ├── Leaves.aspx(.cs)        # Approve leave requests
│       ├── Deductions.aspx(.cs)
│       └── Payroll.aspx(.cs)
├── .gitignore
└── README.md
```
