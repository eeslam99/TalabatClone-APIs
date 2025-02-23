# **TalabatClone APIs**  

## **Overview**  

**TalabatClone** is a **scalable eCommerce Web API** built with **ASP.NET Core**, designed to manage orders, baskets, carts, products, and payments. It follows the **Onion Architecture** to ensure modularity, maintainability, and separation of concerns.  

---

## **Features**  

### 🛠 **Admin Dashboard**  

- **User Management**  
  - Create, update, delete, and manage user accounts.  
  - Assign and modify user roles (**Admin/User**).  

- **Role Management**  
  - Create, update, delete, and view roles.  
  - Assign roles to users dynamically.  

---

## **Project Architecture**  

TalabatClone follows **Onion Architecture**, ensuring a well-structured and scalable codebase:  

### 🏗 **Core Layer**  
- **Entities**: Defines domain models like `Product`, `Order`, and `CustomerBasket`.  
- **Interfaces**: Contracts for data access and business logic services.  

### 🛢 **Infrastructure Layer**  
- Handles **data access**, **repository pattern**, and **database migrations**.  
- Uses `StoreContext` for **Entity Framework Core (EF Core)** integration.  

### ⚙ **Service Layer**  
- Contains **business logic** for orders, payments, and cart management.  

### 🌐 **API Layer**  
- Includes **Controllers**, **Middleware**, and **RESTful API Endpoints** for client interaction.  

---

## **Database & Models**  

- **Database**: Microsoft SQL Server  
- **Key Models**:  
  - `ApplicationUser`  
  - `Order Aggregate`  
  - `BasketItem`  
  - `CustomerBasket`  
  - `Product`  
  - `ProductBrand`  
  - `ProductCategory`  
- **Migrations**: Used for managing database schema updates.  

---

## **Tech Stack**  

🔹 **ASP.NET Core Web API** – Backend framework  
🔹 **Entity Framework Core (EF Core)** – ORM for database interactions  
🔹 **Identity** – Authentication and Authorization  
🔹 **Dependency Injection (DI)** – Improves maintainability and testability  

---

## **Getting Started 🚀**  

To set up and run the project locally, follow these steps:  

1️⃣ **Clone the repository:**  
   ```sh
   git clone https://github.com/eeslam99/TalabatClone-APIs.git
   ```  

2️⃣ **Open the solution** in Visual Studio or your preferred IDE.  

3️⃣ **Add a new migration:**  
   ```sh
   dotnet ef migrations add InitialCreate
   ```  

4️⃣ **Update the database:**  
   ```sh
   dotnet ef database update
   ```  

5️⃣ **Run the application** using:  
   ```sh
   dotnet run
   ```  

---

### 🎯 **Ready to Build & Scale!**  
TalabatClone is designed for extensibility and performance, making it a solid foundation for any **eCommerce application**. Happy coding! 🚀💻  
