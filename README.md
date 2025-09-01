# Product Controller


An ASP.NET Core MVC web application for managing products. Users can view, create, edit, and delete products.

# Features

# View
<img width="1665" height="877" alt="image" src="https://github.com/user-attachments/assets/93d710d7-3d23-464a-94c7-580b2826dc80" />

# Create
<img width="1156" height="691" alt="image" src="https://github.com/user-attachments/assets/82e8afc4-dabc-4c8e-acd8-70664491b438" />

# Delete
<img width="659" height="448" alt="image" src="https://github.com/user-attachments/assets/91d4f7d4-b875-4324-94f8-05ea04228bef" />


# Dependencies

This project relies on the following libraries and frameworks:

- .NET 6.0 SDK – Required to build and run the application.
- ASP.NET Core MVC – For building web interfaces and handling routing.
- Entity Framework Core – For database access.
- Microsoft SQL Server – Backend database.

# How to run

- Make sure you have all the dependices installed.


- Clone the repo https://github.com/isaacmitten-dotcom/Product-ControllerMVC.git
into Visual Studio

- Build the program

# Separation of Concerns and DI Choices

I chose to separate the CRUD logic out from the controllers and into the ProductService. This is to better facilitate separation of concerns, as it allows the controller to only handle HTTP requests. This makes the code more maintainable.




