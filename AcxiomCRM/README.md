# AcxiomCRM — Windows Setup Guide

A .NET 8 MVC CRM starter implementation based on the supplied AcxiomCRM assignment. It includes ASP.NET Core Identity, SQL Server/EF Core, role-aware dashboard, customer/lead/opportunity/follow-up/activity screens, audit events, reports, and REST APIs.

## Prerequisites

- Windows 10/11
- .NET 8 SDK
- SQL Server Express (`SQLEXPRESS`) or SQL Server LocalDB
- Visual Studio 2022 (ASP.NET and web development workload) or VS Code with C# Dev Kit
- Internet connection for Bootstrap, Chart.js, jQuery validation and Bootstrap Icons CDN assets

## Run locally

1. Extract the ZIP.
2. Open `AcxiomCRM.csproj` in Visual Studio, or open this folder in a terminal.
3. Ensure SQL Server Express is running. The Development connection string uses `Server=.\SQLEXPRESS`. If you use LocalDB, copy the connection string from `appsettings.json` into `appsettings.Development.json`.
4. In PowerShell, from this folder:

   ```powershell
   dotnet restore
   dotnet build
   $env:ACXIOM_SEED_DEMO="1"
   dotnet run
   ```

5. Open the local HTTPS URL printed by `dotnet run`. The app creates the database schema on first run using EF Core EnsureCreated. This is convenient for a fresh demo database; for ongoing schema evolution, switch to migrations as described below.

### Create the initial database migration

To move from EnsureCreated to a migration-managed database, first remove the EnsureCreated call in Program.cs and then run after restoring packages:

```powershell
dotnet tool install --global dotnet-ef --version 8.0.11
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Then run the application again. Keep the generated `Migrations` folder in your submission/source repository. Do not mix EnsureCreated and migrations on the same existing database.

## Development demo users

Demo accounts are seeded only when `ACXIOM_SEED_DEMO=1` is set before startup:

| Role | Email | Temporary password |
|---|---|---|
| Admin | `admin@acxiom.local` | `ChangeMe123!` |
| Manager | `manager@acxiom.local` | `ChangeMe123!` |
| SalesExecutive | `sales@acxiom.local` | `ChangeMe123!` |

These are development-only credentials. Do not deploy them. Change/remove them before any shared or production deployment. Public registration creates SalesExecutive users only.

## Connection string

Edit `ConnectionStrings:DefaultConnection` in `appsettings.Development.json`. Do not commit production secrets. Use Windows Authentication locally when possible.

## APIs

When `EnableSwagger` is true, Swagger UI is available at `/swagger` in a local run. API routes include:

- `GET/POST /api/customers`
- `GET/PUT/DELETE /api/customers/{id}`
- `GET/POST /api/leads`
- `GET/POST /api/opportunities`

Protected API routes require the app's configured authentication cookie. For external clients, configure an appropriate bearer-token strategy before production use.

## Included features

- ASP.NET Core Identity password hashing, lockout and role assignment
- Admin, Manager and SalesExecutive roles
- Responsive light UI with blue accents
- EF Core entity models and unique indexes for customer email/phone and CRM codes
- Client-side validation assets and server-side business validation
- Dashboard KPI queries and Chart.js charts
- CRM list/create/edit/details/deactivate workflows
- Audit logging for core CRUD and authentication actions
- Customer CSV export
- Swagger/OpenAPI

## Important implementation notes / limitations

- The code has not been compiled in the generation environment because the .NET SDK is not installed there. Run `dotnet restore` and `dotnet build` on your Windows machine and fix any environment-specific errors before submission.
- Generate and commit EF Core migrations using the commands above before relying on first-run automatic migration.
- Role-aware dashboard and record filtering are implemented, but this is a baseline project rather than a security-certified production system. Review every controller's record ownership checks, API scopes, and report scopes before production use.
- Entity list search uses explicit per-entity searchable fields.
- The assignment calls for richer reporting, workflow transition rules, and user/role administration. Extend these areas and add tests before claiming all evaluation criteria have passed.
- Third-party frontend assets are loaded from CDNs; bundle local copies if the demonstration must work offline.

## Suggested verification checklist

- Register a new user and verify the SalesExecutive role is assigned.
- Test invalid login and account lockout.
- Test duplicate customer email/phone.
- Test opportunity amount, probability, and close-date validation.
- Test past follow-up date rejection.
- Verify audit entries after login and CRUD operations.
- Test Admin, Manager and SalesExecutive authorization using separate browser sessions.
- Exercise the API and verify validation and status codes.
