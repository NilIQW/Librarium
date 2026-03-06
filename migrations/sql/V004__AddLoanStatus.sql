--V004_AddLoanStatus
--Added the code manually to update the existing rows to active/returned

BEGIN TRANSACTION;
ALTER TABLE [Loans] ADD [Status] int NOT NULL DEFAULT 0;

-- Populate existing rows
UPDATE [Loans]
SET [Status] = CASE
    WHEN [ReturnDate] IS NULL THEN 0 -- Active
    ELSE 1 -- Returned
END;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260227151127_V004_AddLoanStatus', N'10.0.3');

COMMIT;
GO

