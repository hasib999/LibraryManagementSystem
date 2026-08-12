# Library Management System

## About the Project

Library Management System is a complete, intentionally simple university Database Management System project developed with ASP.NET Core MVC and SQL Server. Its direct controller-to-EF Core design makes it easy for students to understand, demonstrate, and maintain.

## Features

- Secure login with Admin and Librarian roles
- Dashboard with live library totals and recent issues
- Book Management (CRUD, stock status, and search)
- Member Management (CRUD, deactivation, and search)
- Issue Books and Return Books
- Automatic late Fine Calculation at BDT 10 per day
- Filterable Transaction History
- Currently issued, overdue, returned, fine, category, and member Reports

## Technology Stack

- .NET 10
- ASP.NET Core MVC
- Razor Views
- Entity Framework Core 10 Code First
- SQL Server
- ASP.NET Core Identity
- Bootstrap 5 and Bootstrap Icons

## Database Tables

The application uses `Books`, `Members`, `BookIssues`, and `BookReturns`, together with the standard ASP.NET Core Identity tables for users and roles. ISBN and member code are unique, and each issue can have at most one return.

## Database Relationships

```text
Books
   |
   | 1
   |
   | *
BookIssues
   | *
   |
   | 1
Members

BookIssues
   |
   | 1
   |
   | 0..1
BookReturns
```

Transaction foreign keys use restricted deletion so borrowing history is not accidentally removed.

## Requirements

- .NET 10 SDK
- SQL Server
- Visual Studio 2026 or VS Code
- EF Core CLI (`dotnet tool install --global dotnet-ef`), if not already installed

## How to Run

```bash
git clone <repository-url>
cd LibraryManagementSystem/LMS/LMS
dotnet restore
dotnet ef database update
dotnet run
```

The app also applies pending migrations and seeds its demonstration data at startup. Change the SQL Server connection string in `LMS/LMS/appsettings.json` when necessary.

## Default Login

```text
Admin
Email: admin@library.com
Password: Admin@123

Librarian
Email: librarian@library.com
Password: Library@123
```

**These credentials are for demonstration purposes only.** Change them before any non-demonstration deployment.

## Seed Data

The idempotent initializer creates two roles, the two users above, 10 books, 8 members, three current issues, and two returned issues. The sample data includes one overdue issue and one late return.

## Project Modules

- **Dashboard** — totals, overdue/fine indicators, and recent activity.
- **Books** — searchable catalogue and inventory CRUD.
- **Members** — searchable member records and safe deactivation.
- **Book Issue** — active-member and available-book selection.
- **Book Return** — return processing and automatic fines.
- **Transactions** — all, issued, returned, and overdue filters.
- **Reports** — operational and summary HTML tables.

## Screenshots

Screenshots can be added later inside `docs/screenshots/`.

### Dashboard

_Add screenshot here._

### Books

_Add screenshot here._

### Members

_Add screenshot here._

### Issue Book

_Add screenshot here._

### Return Book

_Add screenshot here._

## Contributors

- Student Name: ____________________
- Student ID: ______________________
- Department: ______________________
- University: ______________________

## Academic Purpose

This project was developed for academic and educational purposes as part of a Database Management System course.
