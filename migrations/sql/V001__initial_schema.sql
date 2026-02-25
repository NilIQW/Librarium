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

