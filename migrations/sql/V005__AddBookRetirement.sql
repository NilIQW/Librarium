--V0005_AddBookRetirement

BEGIN TRANSACTION;
ALTER TABLE [Books] ADD [IsRetired] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260304185120_V0005_AddBookRetirement', N'10.0.3');

COMMIT;
GO