# n5now API & Web Application

## Backend (n5now-api)

The n5now-api is a .NET 6 web application that serves as the backend for the n5now project. It utilizes the CQRS pattern and integrates various technologies and packages, including:

- MediatR: A library for implementing the CQRS pattern.
- NEST (Elasticsearch): Used for Elasticsearch integration.
- Entity Framework: As the Object-Relational Mapping (ORM) tool for working with SQL Server.
- XUnit: Used for writing unit tests.

### Prerequisites

Before running the API, ensure you have the following prerequisites installed:

- Visual Studio
- Docker

### Running the API

To run the API, follow these steps:

1. Open the `n5now-api` project in Visual Studio.
2. Navigate to the `script` folder and locate the `docker-compose.yml` file. This file contains the configuration for Docker containers needed for the project.
3. Open a PowerShell terminal for Visual Studio developers and execute the following command: `docker-compose up -d` This command will create the required containers based on the configuration in docker-compose.yml.
4. In the same script folder, you'll find a file named db.sql. Execute this SQL script using the following credentials:

   Server Name: `localhost, 1434`
   Authentication: `SQL Server`
   Username: `sa`
   Password: `123*N5now`
   You can also find these credentials in the docker-compose.yml and appsettings.Development.json files.

5. After executing the SQL script, restore the NuGet packages. Right-click on the n5now-api project in Visual Studio and choose "Restore NuGet Packages."

6. Start the API project. The API is now running and ready to accept requests.

### Database Initialization

The database contains two tables. The PermissionTypes table is preloaded with data using a class named DbCreated. It populates the table with four permission types to work.

### Unit Testing

The project includes unit tests located in the n5now-test project. Inside the ApiTests.cs class, you'll find unit tests that can be executed to validate the API's functionality.

### Frontend (n5now-web)

The n5now-web project is a React application built with Vite. It utilizes libraries such as Axios for API consumption and Material-UI for UI components.

## Running the Frontend

To run the frontend project, follow these steps:

1. Navigate to the root directory of the n5now-web project in your terminal.
2. Install the project dependencies by running: npm install
3. Start the development server with: npm run dev

Note: Vite may assign a different port, but the default is "http://localhost:5173/". Be sure to check the console output for the correct URL.

Now, you can access the n5now web application in your web browser.
