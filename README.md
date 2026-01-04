# Medical Records API

A .NET 10.0 Web API for managing medical records, doctors, and patients using PostgreSQL and Entity Framework Core.

## Prerequisites

- .NET 10.0 SDK
- Docker and Docker Compose (for containerized setup)
- PostgreSQL 16 (if running without Docker)

## Development

### Local Development

1. Start the PostgreSQL database:
   ```bash
   docker compose up postgres -d
   ```

2. Apply database migrations:
   ```bash
   cd MedicalRecords
   dotnet ef database update
   ```

3. Run the API:
   ```bash
   dotnet run
   ```

The API will be available at `http://localhost:5000` (or `https://localhost:5001`). Swagger UI is available at `/swagger` in Development mode.

### Docker Compose

Run the entire stack (API + PostgreSQL):
```bash
docker compose up
```

The API will be available at `http://localhost:5050`.

## Build

### Local Build
```bash
cd MedicalRecords
dotnet build
```

### Docker Build
```bash
docker compose build
```

## API Documentation

Swagger UI is available at `/swagger` when running in Development mode.

## Database

- **Host**: localhost (or `postgres` in Docker)
- **Port**: 5432
- **Database**: medicalrecordsdb
- **Username**: medicaluser
- **Password**: medicalpass123

