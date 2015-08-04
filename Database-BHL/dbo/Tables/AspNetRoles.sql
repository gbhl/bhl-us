CREATE TABLE [dbo].[AspNetRoles] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (256) NOT NULL,
    [DisplaySequence] INT            CONSTRAINT [DF_AspNetRoles_DisplaySequence] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);
GO

