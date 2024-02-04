# Project Overview 🌍

You are required to create a full-stack application using .NET 6 for the backend.
The application will be a simple user management system
with CRUD (Create, Read, Update, Delete) functionalities

## Instructions 📝
1. Use Entity Framework Core to interact with the database
2. Implement a REST API with the following endpoints:
    - GET /users
    - GET /users/{id}
    - POST /users
    - PUT /users/{id}
    - DELETE /users/{id}
3. Map the database entities to DTOs (Data Transfer Objects)
4. Use the Repository Pattern to access the database
5. Add Dependency Injection configuration to the project for necessary services note: use the right lifetime for each service
6. Use Dependency Injection to inject the repositories into the controllers (Bonus Use MediatR in the controllers)

## How to run the project 🚀

1. Clone the repository
2. Open the project in Visual Studio or any other IDE of your choice
3. Make sure you have MSSQL Server installed and running on your machine


5. To Create the database run the following command:
```shell
dotnet ef migrations add InitialCreate --project UsersApi --startup-project UsersApi
dotnet ef database update --project UsersApi --startup-project UsersApi
```

6. Run the project with the following command:
```shell
dotnet run 
```


## Good luck! 🍀

