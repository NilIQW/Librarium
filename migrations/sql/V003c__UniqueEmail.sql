--V003_UniqueEmail

BEGIN TRANSACTION;
DROP INDEX [IX_Members_Email] ON [Members];

CREATE UNIQUE INDEX [IX_Members_Email] ON [Members] ([Email]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260225221228_V003c_UniqueEmail', N'10.0.3');

COMMIT;
GO

