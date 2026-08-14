# Campus Lost & Found — Backend API

## 1. Project Overview

REST API for the Campus Lost & Found hackathon project. Students can report lost or
found items, browse/search reports, view a single item, and mark an item as
`RETURNED` once it's recovered. The React frontend (developed separately) consumes
this API.

## 2. Technology Stack

- ASP.NET Core 8 Web API (C#, controllers)
- Entity Framework Core 8 + Npgsql provider
- PostgreSQL, hosted on [Neon](https://neon.tech)
- Swagger / OpenAPI (dev only)

## 3. Configuring the Neon PostgreSQL Connection

The connection string is **never hardcoded**. The app reads it from the
`DATABASE_URL` environment variable (Neon's standard `postgresql://` URL format).

1. In the Neon console: **Project → Connect → Connection string**, copy the value.
   It looks like:
   ```
   postgresql://<user>:<password>@<host>/<database>?sslmode=require
   ```
2. Copy `backend/.env.example` to `backend/.env` and paste your real value in:
   ```
   DATABASE_URL=postgresql://<user>:<password>@<host>/<database>?sslmode=require
   ```
   `.env` is gitignored — it is loaded automatically at startup (both for `dotnet run`
   and `dotnet ef` commands) and never committed.

   Alternatively, export it directly in your shell instead of using `.env`:
   ```powershell
   $env:DATABASE_URL = "postgresql://<user>:<password>@<host>/<database>?sslmode=require"
   ```
   ```bash
   export DATABASE_URL="postgresql://<user>:<password>@<host>/<database>?sslmode=require"
   ```

If `DATABASE_URL` isn't set, the app falls back to `ConnectionStrings:DefaultConnection`
in configuration (e.g. via `dotnet user-secrets` or `appsettings.Development.json`,
which is also gitignored).

**CORS** allowed origins are likewise config-driven, in `appsettings.json`:
```json
"Cors": { "AllowedOrigins": [ "http://localhost:3000", "http://localhost:5173" ] }
```
Add your frontend's dev URL there if it differs.

## 4. Running Migrations

```bash
cd backend
dotnet ef database update
```

This creates the `LostFoundItems` table on Neon and inserts the 5 seed rows. To
create a new migration after changing a model:

```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

(Requires the `dotnet-ef` global tool: `dotnet tool install --global dotnet-ef`.)

## 5. Starting the API

```bash
cd backend
dotnet run
```

By default this runs in the `Development` environment (via `launchSettings.json`)
on `http://localhost:5158`, opening Swagger automatically.

## 6. Swagger URL

```
http://localhost:5158/swagger
```

## 7. API Endpoints

| Method | Route             | Description                                  |
|--------|-------------------|-----------------------------------------------|
| GET    | `/api/items`      | List items. Optional query params: `search`, `type`, `status`, `category` (combinable) |
| GET    | `/api/items/{id}` | Get a single item by id. 404 if missing.      |
| POST   | `/api/items`      | Create an item. `status` always defaults to `ACTIVE`. |
| PUT    | `/api/items/{id}` | Update an item (any subset of fields). 404 if missing. |
| DELETE | `/api/items/{id}` | Delete an item. 404 if missing.               |

Filter examples:
```
GET /api/items?search=wallet
GET /api/items?type=LOST
GET /api/items?status=ACTIVE
GET /api/items?category=Electronics
GET /api/items?type=LOST&status=ACTIVE&category=Electronics
```

## 8. Example Request Bodies

**POST /api/items**
```json
{
  "title": "Black Wallet",
  "description": "Small black leather wallet with a few cards inside.",
  "category": "Accessories",
  "location": "Library",
  "dateReported": "2026-08-10",
  "type": "LOST",
  "contactName": "Alex Chen",
  "contactEmail": "alex.chen@example.edu"
}
```
Response: `201 Created` with the created item (`status: "ACTIVE"` set automatically —
the client cannot set it).

**PUT /api/items/{id}** — mark as returned:
```json
{
  "status": "RETURNED"
}
```
Any other fields included in the body are updated too; omitted fields are left
unchanged.

Validation errors return `400` with a field-by-field error list, e.g.:
```json
{
  "errors": {
    "Title": ["The Title field is required."],
    "ContactEmail": ["The ContactEmail field is not a valid e-mail address."]
  }
}
```

## 9. Running Locally — Quick Start

```bash
cd backend
cp .env.example .env      # then edit .env with your real Neon DATABASE_URL
dotnet restore
dotnet ef database update
dotnet run
```

Then open `http://localhost:5158/swagger` to try the API, or point Postman /
the React frontend at `http://localhost:5158/api/items`.
