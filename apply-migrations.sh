#!/bin/bash

echo "Applying latest EF Core migrations to database..."

dotnet ef database update \
  --project src/Librarium.Data \
  --startup-project src/Librarium.Api

echo "Database updated!"