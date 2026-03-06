-- Migration: V006_AddIsbnTextColumn

BEGIN TRANSACTION;
ALTER TABLE [Books] ADD [IsbnText] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260305133327_V006_AddIsbnTextColumn', N'10.0.3');

COMMIT;
GO

