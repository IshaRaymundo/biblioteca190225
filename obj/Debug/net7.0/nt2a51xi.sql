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
GO

CREATE TABLE [Libros] (
    [PkLibro] int NOT NULL IDENTITY,
    [Nombre] nvarchar(255) NOT NULL,
    [Autor] nvarchar(255) NOT NULL,
    [Anio] int NOT NULL,
    [Genero] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Libros] PRIMARY KEY ([PkLibro])
);
GO

CREATE TABLE [Roles] (
    [PkRol] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([PkRol])
);
GO

CREATE TABLE [Usuarios] (
    [PkUsuario] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [FkRol] int NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([PkUsuario]),
    CONSTRAINT [FK_Usuarios_Roles_FkRol] FOREIGN KEY ([FkRol]) REFERENCES [Roles] ([PkRol]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PkRol', N'Nombre') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] ON;
INSERT INTO [Roles] ([PkRol], [Nombre])
VALUES (1, N'sa');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PkRol', N'Nombre') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PkUsuario', N'FkRol', N'Nombre', N'Password', N'UserName') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] ON;
INSERT INTO [Usuarios] ([PkUsuario], [FkRol], [Nombre], [Password], [UserName])
VALUES (1, 1, N'Jonny', N'123', N'Usuario');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PkUsuario', N'FkRol', N'Nombre', N'Password', N'UserName') AND [object_id] = OBJECT_ID(N'[Usuarios]'))
    SET IDENTITY_INSERT [Usuarios] OFF;
GO

CREATE INDEX [IX_Usuarios_FkRol] ON [Usuarios] ([FkRol]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250305135632_AddLibroTable', N'7.0.0');
GO

COMMIT;
GO

