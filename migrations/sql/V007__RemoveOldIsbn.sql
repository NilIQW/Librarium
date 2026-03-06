-- Migration: V007__RemoveOldIsbn
-- Replaces incorrect ISBN column with new string column IsbnText
-- Existing corrupted ISBN values are discarded

BEGIN TRANSACTION;
DECLARE @var2 nvarchar(max);
SELECT @var2 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'ISBN');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT ' + @var2 + ';');
ALTER TABLE [Books] DROP COLUMN [ISBN];

DECLARE @var3 nvarchar(max);
SELECT @var3 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'IsbnText');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT ' + @var3 + ';');
UPDATE [Books] SET [IsbnText] = N'' WHERE [IsbnText] IS NULL;
ALTER TABLE [Books] ALTER COLUMN [IsbnText] nvarchar(20) NOT NULL;
ALTER TABLE [Books] ADD DEFAULT N'' FOR [IsbnText];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260305135526_V007__RemoveOldIsbn', N'10.0.3');

COMMIT;
GO

