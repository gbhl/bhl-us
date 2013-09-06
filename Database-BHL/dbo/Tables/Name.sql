CREATE TABLE [dbo].[Name] (
    [NameID]             INT            IDENTITY (1, 1) NOT NULL,
    [NameSourceID]       INT            NOT NULL,
    [NameString]         NVARCHAR (100) CONSTRAINT [DF_Name_NameString] DEFAULT ('') NOT NULL,
    [IsActive]           SMALLINT       CONSTRAINT [DF_Name_IsActive] DEFAULT ((1)) NOT NULL,
    [CreationDate]       DATETIME       CONSTRAINT [DF_Name_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME       CONSTRAINT [DF_Name_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT            NULL,
    [LastModifiedUserID] INT            NULL,
    [NameResolvedID]     INT            NULL,
    CONSTRAINT [PK_Name] PRIMARY KEY CLUSTERED ([NameID] ASC),
    CONSTRAINT [FK_Name_NameResolved] FOREIGN KEY ([NameResolvedID]) REFERENCES [dbo].[NameResolved] ([NameResolvedID]),
    CONSTRAINT [FK_Name_NameSource] FOREIGN KEY ([NameSourceID]) REFERENCES [dbo].[NameSource] ([NameSourceID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Name_IsActive]
    ON [dbo].[Name]([IsActive] ASC)
    INCLUDE([NameResolvedID], [NameID]);


GO
CREATE NONCLUSTERED INDEX [IX_Name_NameString]
    ON [dbo].[Name]([NameString] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Name_NameResolvedID]
    ON [dbo].[Name]([NameResolvedID] ASC);

