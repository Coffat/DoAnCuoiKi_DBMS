# Setup Instructions for HR Management System

## Required Packages

This project requires the following NuGet packages:

1. **Guna.UI2.WinForms 2.0.4.7**
2. **System.Data.SqlClient 4.9.0**

## Installation Methods

### Method 1: Using NuGet Package Manager (Recommended)

1. Open the solution in Visual Studio
2. Right-click on the project → "Manage NuGet Packages"
3. Install the following packages:
   - `Guna.UI2.WinForms` version 2.0.4.7
   - `System.Data.SqlClient` version 4.9.0

### Method 2: Using Package Manager Console

```powershell
Install-Package Guna.UI2.WinForms -Version 2.0.4.7
Install-Package System.Data.SqlClient -Version 4.9.0
```

### Method 3: Manual Package Restore

If you have the packages downloaded:

1. Create a `packages` folder in the solution root
2. Extract packages to:
   - `packages/Guna.UI2.WinForms.2.0.4.7/`
   - `packages/System.Data.SqlClient.4.9.0/`

## Database Setup

1. Ensure SQL Server is running
2. Create database `QLNhanSuSieuThiMini`
3. Update connection string in `App.config` if needed:
   ```xml
   <connectionStrings>
     <add name="HrDb"
          connectionString="Data Source=localhost;Initial Catalog=QLNhanSuSieuThiMini;User ID=sa;Password=1234;TrustServerCertificate=True"
          providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

## Build Instructions

1. Restore NuGet packages
2. Build solution (Ctrl+Shift+B)
3. Run the application

## Troubleshooting

- If Guna.UI2 reference errors occur, ensure the package is properly installed
- Check that .NET Framework 4.7.2 is installed
- Verify SQL Server connection string is correct
