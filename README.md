# Route.Talabat.G03.Solution

# Talabat 

**Talabat** is an ECommerce ASP.NET Web API project that follows Onion architecture, designed for Managing orders, Basket, Cart, Products, and Payment Modules.

## Features

### Admin Dashboard

- **User Management**:
  - Create, update, delete, and view user accounts.
  - Manage user roles (Admin/User).

- **Role Management**:
  - Create, update, delete, and view roles.
  - Assign roles to users.

## Project Structure

The project adheres to an Onion architecture:

- **Infrastructure**:
  - Manages data access, including database context, Implements the Generic Repository design pattern and Unit of Work for efficient data manipulation, and migrations.
  - Key models include `StoreContext`, `data seeding `

- **core**:
  - Entites, interfaces

- **Service**:
  -  Houses business logic of all modules .

- **APIs**:
  - Controllers.
  - Middlewares
  - real web API Endpoints 

## Database

- **Database**: Microsoft SQL Server
- **Models**: `ApplicationUser`, `Order Aggregate`, `BasketItem`, `CustomerBasket`, `Product`, `ProductBrand`, `ProductCategory`
- **Migrations**: Manage database schema changes efficiently.

## Technologies Used

- **ASP.NET API**: The core framework for building the web application.
- **Entity Framework Core**: Handles data access and database operations.
- **Identity**: Manages authentication and authorization.
- **Dependency Injection**: Enhances code maintainability and scalability.

## Getting Started

To run the application locally, follow these steps:

1. Clone this repository to your local machine:

   ``` shell
   git clone https://github.com/youssefessam117/Route.Talabat.G03.Solution.git
   ```

2. Open the solution in Visual Studio or your preferred IDE.

3. Configure the database connection string in the `appsettings.json` file:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "your-connection-string"
   }
   ```

4. Run the application.
