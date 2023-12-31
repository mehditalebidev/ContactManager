# ContactManager.WebApi

ContactManager.WebApi is a RESTful API for managing contact information, built using .NET 6 with a Clean Architecture approach.

## How to Run the Project

### Prerequisites

- .NET 6 SDK
- PostgreSQL installed and running on your local machine or a remote server.
- A suitable IDE, such as Visual Studio 2022, VS Code, or JetBrains Rider.

### Steps

1. Clone the repository to your local machine.
2. Navigate to the `ContactManager.WebApi` directory.
3. Update the `appsettings.json` file with your PostgreSQL connection string.
4. Open the terminal and run the following command to install the EF Core CLI tools if you haven't already:
    ```sh
    dotnet tool install --global dotnet-ef
    ```
5. Run the following commands to apply the database migrations:
    ```sh
    dotnet ef database update
    ```
6. To start the project, run:
    ```sh
    dotnet run
    ```

## Project Architecture

This project follows Clean Architecture principles, organized into the following projects:


### Domain
This layer contains all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application
The application layer contains all application logic. It defines interfaces that are implemented by the outside layers, primarily the infrastructure layer. It has no dependencies on any other layer or project, ensuring that the core of the application remains independent and decoupled from external concerns.

### Infrastructure
This layer depends on the Application layer and implements the interfaces defined therein. It contains classes for accessing external resources such as databases, file systems, web services, SMTP, etc. For this project, it includes database access logic using EF Core and PostgreSQL.


### WebApi
This serves as the entry point of the application. It is responsible for API routing, request/response processing, and dependency injection setup. The controllers are kept lean, with the heavy lifting done by handlers and services defined in the Application layer.

## Patterns and Practices

- **Mediator Pattern**: Implemented using the MediatR library, reducing dependencies between microservices.
- **CQRS**: Command Query Responsibility Segregation is used to separate read and write operations for more scalable and maintainable code.
  
## Packages Used

- **Entity Framework Core**: Used for ORM and database context management with PostgreSQL.
- **Mapster**: An object-to-object mapping which can be integrated with the DI container.
- **MediatR & MediatR.Extensions.Microsoft.DependencyInjection**: For implementing the Mediator pattern and its dependency injection support.
- **FluentValidation & FluentValidation.AspNetCore**: For implementing validation rules for models and their automatic validation within ASP.NET Core.
- **ErrorOr**: A package that provides an error or result response type, used for handling domain errors.
- **Swashbuckle.AspNetCore**: Provides Swagger UI for API documentation and interaction.
- And various Microsoft.Extensions packages for options, configurations, and DI abstractions.

Make sure to check the `.csproj` file for exact versions and additional packages.
