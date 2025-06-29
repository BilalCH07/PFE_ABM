# PFE_ABM - ADO.NET Project with SQL Server

This is a Windows Forms project using ADO.NET to interact with a SQL Server database.

## 🛠 Requirements:
- Visual Studio
- SQL Server
- .NET Framework
- Guna UI2 library (for the UI design)

## 🔧 How to Use the Project:
1. Open the solution file `PFE_ABM.sln` in Visual Studio.
2. Make sure the database connection string is configured correctly in `App.config`.
3. Run the project starting from the `Acceuil.cs` form.

## 📁 Project Structure:
- **App.config**: Contains the database connection string
- **ADO.cs**: Class for database operations (Insert, Update, Delete, etc.)
- **Forms**: UI forms like `Categorie`, `Clients`, `Commandes`, etc.
- **Packages**: Contains Guna.UI2 library

## 🗃 Database:
- Ensure a matching database exists in your SQL Server.
- If a script is required, include it or describe how to create the database manually.

## 📦 Libraries Used:
- [Guna.UI2.WinForms](https://www.nuget.org/packages/Guna.UI2.WinForms)

## 👤 Author:
Bilal Echalhe
