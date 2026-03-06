--V002_AddAuthors

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

