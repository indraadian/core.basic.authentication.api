# Basic Authentication API in .NET Core

This project is a simple API implementing basic authentication using **ASP.NET Core**. It provides endpoints for user registration and login, leveraging hashed passwords with salt to securely store and verify credentials. The API supports authentication via HTTP headers, and it's built to demonstrate how basic authentication can be integrated with ASP.NET Core and Swagger.

## Features

- User registration with hashed passwords.
- Login with password verification.
- Basic authentication middleware.
- API documentation via Swagger.

## Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet)
- A database (SQL Server is used in this example, but can be configured with other DB providers).
- [Swagger UI](https://swagger.io/tools/swagger-ui/) for API testing.

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/basic-authentication-api.git
cd basic-authentication-api
