#!/bin/bash

# Check if version and migration name are passed
if [ -z "$1" ] || [ -z "$2" ]; then
  echo "Usage: ./generate-sql.sh V001 MigrationName"
  exit 1
fi

VERSION=$1
MIGRATION_NAME=$2

# Ensure migrations/sql folder exists
mkdir -p migrations/sql

echo "Generating SQL script: ${VERSION}__${MIGRATION_NAME}.sql"

dotnet ef migrations script \
  --project src/Librarium.Data \
  --startup-project src/Librarium.Api \
  -o migrations/sql/${VERSION}__${MIGRATION_NAME}.sql