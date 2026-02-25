IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Books] (
    [BookId] int NOT NULL IDENTITY,
    [Title] nvarchar(200) NOT NULL,
    [ISBN] nvarchar(20) NOT NULL,
    [PublicationYear] int NOT NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY ([BookId])
);

CREATE TABLE [Members] (
    [MemberId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Email] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_Members] PRIMARY KEY ([MemberId])
);

CREATE TABLE [Loans] (
    [LoanId] int NOT NULL IDENTITY,
    [BookId] int NOT NULL,
    [MemberId] int NOT NULL,
    [LoanDate] datetime2 NOT NULL,
    [ReturnDate] datetime2 NULL,
    CONSTRAINT [PK_Loans] PRIMARY KEY ([LoanId]),
    CONSTRAINT [FK_Loans_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [Books] ([BookId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Loans_Members_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [Members] ([MemberId]) ON DELETE CASCADE
);

CREATE INDEX [IX_Loans_BookId] ON [Loans] ([BookId]);

CREATE INDEX [IX_Loans_MemberId] ON [Loans] ([MemberId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260225143145_InitialSchema', N'10.0.3');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [Authors] (
    [AuthorId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Biography] nvarchar(max) NULL,
    CONSTRAINT [PK_Authors] PRIMARY KEY ([AuthorId])
);

CREATE TABLE [BookAuthors] (
    [AuthorsAuthorId] int NOT NULL,
    [BooksBookId] int NOT NULL,
    CONSTRAINT [PK_BookAuthors] PRIMARY KEY ([AuthorsAuthorId], [BooksBookId]),
    CONSTRAINT [FK_BookAuthors_Authors_AuthorsAuthorId] FOREIGN KEY ([AuthorsAuthorId]) REFERENCES [Authors] ([AuthorId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BookAuthors_Books_BooksBookId] FOREIGN KEY ([BooksBookId]) REFERENCES [Books] ([BookId]) ON DELETE CASCADE
);

CREATE INDEX [IX_BookAuthors_BooksBookId] ON [BookAuthors] ([BooksBookId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260225192219_V002_AddAuthors', N'10.0.3');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Members]') AND [c].[name] = N'Email');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Members] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [Members] ALTER COLUMN [Email] nvarchar(450) NOT NULL;

ALTER TABLE [Members] ADD [PhoneNumber] nvarchar(20) NULL;

CREATE UNIQUE INDEX [IX_Members_Email] ON [Members] ([Email]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260225214253_V003a_AddPhoneNullable', N'10.0.3');

COMMIT;
GO

