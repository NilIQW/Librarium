# Librarium

Librarium is a backend system for a library management platform.  
It exposes a REST API for managing books, members, loans, and authors.

The project demonstrates database schema evolution using migrations while maintaining API compatibility.

---

# Requirements

You must have installed:

- Docker
- .NET 10 SDK

Check installation:

docker --version
docker compose version
dotnet --version

---

# Running the Database

The project uses **SQL Server in Docker**.

Start the database container:

docker compose up -d (from the project root)

This will start the SQL Server container defined in `docker-compose.yml`.


---

# Running the Application

1. Restore dependencies

dotnet restore

2. Apply database migrations

dotnet ef database update --project Librarium.Data --startup-project Librarium.Api

3. Run the API

dotnet run --project Librarium.Api

The API will start on:

https://localhost:5001/swagger (with swagger)

---

# Database Migrations

Migrations are implemented using **Entity Framework Core**.

Migration artifacts are stored in:

/migrations/sql

Each migration includes:

- schema changes
- data transformations
- deployment-safe operations

The migration decisions and tradeoffs are documented in:

/migrations/README.md

---

# Notes

- The system uses **code-first migrations** with SQL artifacts.
- Schema evolution decisions are documented in the migration log.
- API compatibility with existing clients was preserved during schema changes.