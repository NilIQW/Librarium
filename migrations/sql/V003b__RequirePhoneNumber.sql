--V003b_RequirePhoneNumber

BEGIN TRANSACTION;
DROP INDEX [IX_Members_Email] ON [Members];

DECLARE @var1 nvarchar(max);
SELECT @var1 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Members]') AND [c].[name] = N'PhoneNumber');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Members] DROP CONSTRAINT ' + @var1 + ';');
UPDATE [Members] SET [PhoneNumber] = N'' WHERE [PhoneNumber] IS NULL;
ALTER TABLE [Members] ALTER COLUMN [PhoneNumber] nvarchar(20) NOT NULL;
ALTER TABLE [Members] ADD DEFAULT N'' FOR [PhoneNumber];

CREATE INDEX [IX_Members_Email] ON [Members] ([Email]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260225221040_V003b_RequirePhoneNumber', N'10.0.3');

COMMIT;
GO

