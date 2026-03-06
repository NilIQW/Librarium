--V003_AddPhoneNullable

BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Members]') AND [c].[name] = N'Email');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Members] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [Members] ALTER COLUMN [Email] nvarchar(450) NOT NULL;

ALTER TABLE [Members] ADD [PhoneNumber] nvarchar(20) NULL;


INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260225214253_V003a_AddPhoneNullable', N'10.0.3');

COMMIT;
GO

