# Electronics Shop Information System

A Windows Forms desktop application for managing electronics shop reports, user accounts, password recovery, and activity logs. The app uses MySQL for storage and can generate report previews and Excel workbook exports.

## Features

- Login with active account validation
- Seeded administrator account on first run
- Password recovery by registered email address
- User management for adding, updating, activating, and inactivating accounts
- Activity log / audit trail for login, logout, account changes, report generation, and exports
- Report generator for:
  - Sales Transaction
  - Purchase Receiving
  - Inventory Count
- Data grid report preview with date filters
- Excel `.xlsx` export with report data, logo support, and chart sheet

## Tech Stack

- C# Windows Forms
- .NET `net10.0-windows`
- MySQL
- `MySqlConnector` 2.4.0

## Requirements

- Windows
- Visual Studio or the .NET SDK that supports `net10.0-windows`
- MySQL Server running locally
- A MySQL database named `electronicshop_db`

## Database Setup

Create the database before running the app:

```sql
CREATE DATABASE electronicshop_db;
```

The application creates the required tables automatically when the related modules are opened:

- `accounts`
- `activity_logs`
- `sales_transactions`
- `purchase_transactions`
- `inventory_counts`

Sample report records are inserted automatically if the transaction tables are empty.

Database connection settings are currently defined in `DatabaseConnection.cs`:

```csharp
Server=localhost;
Database=electronicshop_db;
User ID=root;
Password=QRRQHZKRZLOOEH;
```

Update these values if your local MySQL credentials are different.

## Default Login

When the `accounts` table is empty, the app creates this administrator account:

```text
Username: administrator
Password: admin123
Email: admin@electronicshop.local
Role: Admin
```

Change the default password after the first login.

## Run the Application

Restore and build:

```powershell
dotnet restore
dotnet build
```

Run:

```powershell
dotnet run
```

You can also open `electronics.csproj` or `electronics.slnx` in Visual Studio and start the project from there.

## Report Preview SQL

The file `generate_report_preview.sql` contains a MySQL query that matches the report preview output. Edit these variables before running it:

```sql
SET @report_type = 'All';
SET @from_date = '2026-05-01';
SET @to_date = '2026-05-31';
```

Supported report types are:

- `Sales Transaction`
- `Purchase Receiving`
- `Inventory Count`
- `All`

## Project Structure

```text
Program.cs                    Application entry point
Form1.cs                      Login screen
Form2.cs                      Password recovery
Form3.cs                      About screen
Form4.cs                      Dashboard
Form5.cs                      Report generator
AccountManagementForm.cs      User management
ActivityLogForm.cs            Audit trail viewer
AccountRepository.cs          Account database operations
AuditLogRepository.cs         Activity log database operations
TransactionRepository.cs      Report data tables, seed data, and queries
ExcelReportExporter.cs        Excel workbook export
DatabaseConnection.cs         MySQL connection string
PasswordHasher.cs             PBKDF2 password hashing
logo.png                      Application/report logo
```

## Notes

- Passwords are stored as PBKDF2 hashes, not plain text.
- `logo.png` is copied to the build output and used in the login screen and Excel report export.
- Audit logging is best-effort: login, logout, report, and export workflows continue even if the audit table is temporarily unavailable.
