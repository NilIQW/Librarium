#!/bin/bash

# Check if migration name is passed
if [ -z "$1" ]; then
  echo "Usage: ./create-migration.sh MigrationName"
  exit 1
fi

MIGRATION_NAME=$1

echo "Creating EF Core migration: $MIGRATION_NAME"

dotnet ef migrations add $MIGRATION_NAME \
  --project src/Librarium.Data \
  --startup-project src/Librarium.Api \
  --output-dir Migrations