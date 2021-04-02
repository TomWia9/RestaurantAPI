# RestaurantAPI

## Description
This project is a **.NET 5** implemented Web API for restaurants management.

**RestaurantAPI** enables communication with the **Microsoft SQL Server** database consisting of sending and receiving data regarding users, restaurants and dishes. Application can be used by users who may create account and login to the application.


## Stack
It uses **Entity Framework Core** to communicate with a database, which contains required data tables like:
* Restaurants - where informations about restaurants are stored 
* Addresses -  where informations about restaurant addresses are stored
* Tables from ASP.NET Core Identity -  where informations about users, roles, claims etc. are stored


Other tools used in project:
* **JwtBearer** - for authorization
* **Open API** - for API documentation
* **AutoMapper** - for mapping DTO-s and EntityModels data
* **Fluent Validation** - for data validation


## Installation
Make sure you have the **.NET 5.0 SDK** installed on your machine. Then do:  
>`git clone https://github.com/TomWia9/RestaurantAPI.git`  
`cd RestaurantAPI`  
`dotnet run`

## Configuration
This will need to be perfored before running the application for the first time
1. You have to change ConnectionString in **appsettings.Development.json** for ConnectionString that allow you to connect with database in your computer.
2. Issue the Entity Framework command to update the database  
`dotnet ef database update`
 
## License
[MIT](https://choosealicense.com/licenses/mit/)
