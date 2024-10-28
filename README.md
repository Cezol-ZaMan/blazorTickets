# Blazor Tickets

A self-hosted support ticket management system built with Blazor and Entity Framework Core. This application enables organizations to manage support requests internally without relying on external vendor solutions.

## Features

- Track and manage support tickets
- Email notifications
- Role-based access control
- Technician assignment system
- Self-hosted solution

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/en-us/download) (version 8.0 or later)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (2019 or later)
- (Optional) [Visual Studio](https://visualstudio.microsoft.com/) 2022 or later for enhanced development experience

## Getting Started
### Installation

1. **Clone the repository:**
 ```bash
 git clone https://github.com/Cezol-ZaMan/blazorTickets.git
 cd blazorTickets
 ```

2. **Restore dependencies:**
```bash
dotnet restore
```

3. **Configure the application:**
- Create appsettings.json in the root folder
- Update settings according to the [Configuration](#configuration) section.


4. **Set up the database:**
```bash
dotnet ef database update
```

### Running the Application

Start the application using:
```bash
dotnet run --urls=http://localhost:5000/
```
Access the application at http://localhost:5000 in your web browser.

### Configuration
#### Application Settings
Create an appsettings.json file in the root directory with the following structure:
```json
{
  "Technicians": {
    "Emails": [
      "example1@domain.com",
      "example2@domain.com"
    ]
  },
  "Smtp": {
    "Host": "smtp.domain.com",
    "Port": 465,
    "EnableSsl": true,
    "Username": "example3@domain.com",
    "Password": "P@ssword"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=Tickets;Trusted_Connection=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

#### Configuration Sections

- Technicians: List of email addresses that will automatically receive technician role upon registration
- Smtp: Email server configuration for notifications
- ConnectionStrings: Database connection settings
- Logging: Application logging configuration

### Database Management
#### Creating New Migrations

When modifying data models:

1. Create a migration:
```bash
dotnet ef migrations add YourMigrationName
```

2. Apply the migration:
```bash
dotnet ef database update
```

## Deployment

### Development Commands

1. Build the project:
```bash
dotnet build
```

2. Run tests:
```bash
dotnet test
```

3. Create production build:
```bash
dotnet publish --configuration Release
```


### Production Configuration

1. **Environment Variables:**

    - Replace sensitive configuration with environment variables
    - Update Program.cs:
    ```csharp
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
    ```

2. **Security Considerations**

    - Use HTTPS in production
    - Secure database connection strings
    - Implement proper authentication
    - Regular security updates


## Troubleshooting
Common issues and solutions:

- **Database Connection Failed:** Verify connection string and SQL Server instance

- **Migration Errors:** Ensure database is up to date with dotnet ef database update

- **Email Sending Failed:** Check SMTP settings and firewall configuration

## Contributing
Contributions are welcome! Please feel free to submit a Pull Request or Issue. For changes:

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request 

## License
This project is licensed under the MIT License - see the LICENSE file for details.