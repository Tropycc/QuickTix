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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250926171609_InitialMigration'
)
BEGIN
    CREATE TABLE [Category] (
        [CategoryId] INTEGER NOT NULL,
        [Name] TEXT NOT NULL,
        CONSTRAINT [PK_Category] PRIMARY KEY ([CategoryId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250926171609_InitialMigration'
)
BEGIN
    CREATE TABLE [Owner] (
        [OwnerId] INTEGER NOT NULL,
        [Name] TEXT NOT NULL,
        [ContactInfo] TEXT NOT NULL,
        CONSTRAINT [PK_Owner] PRIMARY KEY ([OwnerId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250926171609_InitialMigration'
)
BEGIN
    CREATE TABLE [Listing] (
        [ListingId] INTEGER NOT NULL,
        [CategoryId] INTEGER NOT NULL,
        [OwnerId] INTEGER NOT NULL,
        [Title] TEXT NOT NULL,
        [Description] TEXT NOT NULL,
        [Category] TEXT NOT NULL,
        [Location] TEXT NOT NULL,
        [Owner] TEXT NOT NULL,
        [ListingDate] TEXT NOT NULL,
        CONSTRAINT [PK_Listing] PRIMARY KEY ([ListingId]),
        CONSTRAINT [FK_Listing_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([CategoryId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Listing_Owner_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Owner] ([OwnerId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250926171609_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Listing_CategoryId] ON [Listing] ([CategoryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250926171609_InitialMigration'
)
BEGIN
    CREATE INDEX [IX_Listing_OwnerId] ON [Listing] ([OwnerId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250926171609_InitialMigration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250926171609_InitialMigration', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251010160858_SecondMigration'
)
BEGIN
    ALTER TABLE [Listing] ADD [PhotoFileName] TEXT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251010160858_SecondMigration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251010160858_SecondMigration', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251010165152_ThirdMigration'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Listing]') AND [c].[name] = N'Category');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [Listing] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [Listing] DROP COLUMN [Category];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251010165152_ThirdMigration'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Listing]') AND [c].[name] = N'Owner');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Listing] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Listing] DROP COLUMN [Owner];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251010165152_ThirdMigration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251010165152_ThirdMigration', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251010173838_photoUpdates'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251010173838_photoUpdates', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026175346_InitAzure'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251026175346_InitAzure', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026180159_SyncModelChanges'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Owner]') AND [c].[name] = N'Name');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Owner] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Owner] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026180159_SyncModelChanges'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Owner]') AND [c].[name] = N'ContactInfo');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Owner] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Owner] ALTER COLUMN [ContactInfo] nvarchar(max) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026180159_SyncModelChanges'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Owner]') AND [c].[name] = N'OwnerId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Owner] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Owner] ALTER COLUMN [OwnerId] int NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026180159_SyncModelChanges'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Listing]') AND [c].[name] = N'Title');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Listing] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Listing] ALTER COLUMN [Title] nvarchar(max) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026180159_SyncModelChanges'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Listing]') AND [c].[name] = N'PhotoFileName');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Listing] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Listing] ALTER COLUMN [PhotoFileName] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026180159_SyncModelChanges'
)
BEGIN
    DROP INDEX [IX_Listing_OwnerId] ON [Listing];
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Listing]') AND [c].[name] = N'OwnerId');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Listing] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Listing] ALTER COLUMN [OwnerId] int NOT NULL;
    CREATE INDEX [IX_Listing_OwnerId] ON [Listing] ([OwnerId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026180159_SyncModelChanges'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Listing]') AND [c].[name] = N'Location');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Listing] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Listing] ALTER COLUMN [Location] nvarchar(max) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026180159_SyncModelChanges'
)
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Listing]') AND [c].[name] = N'ListingDate');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Listing] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Listing] ALTER COLUMN [ListingDate] datetime2 NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026180159_SyncModelChanges'
)
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Listing]') AND [c].[name] = N'Description');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Listing] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Listing] ALTER COLUMN [Description] nvarchar(max) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026180159_SyncModelChanges'
)
BEGIN
    DROP INDEX [IX_Listing_CategoryId] ON [Listing];
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Listing]') AND [c].[name] = N'CategoryId');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Listing] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Listing] ALTER COLUMN [CategoryId] int NOT NULL;
    CREATE INDEX [IX_Listing_CategoryId] ON [Listing] ([CategoryId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026180159_SyncModelChanges'
)
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Listing]') AND [c].[name] = N'ListingId');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Listing] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Listing] ALTER COLUMN [ListingId] int NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026180159_SyncModelChanges'
)
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Category]') AND [c].[name] = N'Name');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Category] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [Category] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026180159_SyncModelChanges'
)
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Category]') AND [c].[name] = N'CategoryId');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Category] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [Category] ALTER COLUMN [CategoryId] int NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251026180159_SyncModelChanges'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251026180159_SyncModelChanges', N'9.0.10');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20251027175516_SqlServerSync'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20251027175516_SqlServerSync', N'9.0.10');
END;

COMMIT;
GO

