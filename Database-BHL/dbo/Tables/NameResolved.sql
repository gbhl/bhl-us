CREATE TABLE [dbo].[NameResolved] (
    [NameResolvedID]      INT            IDENTITY (1, 1) NOT NULL,
    [ResolvedNameString]  NVARCHAR (100) CONSTRAINT [DF_NameResolved_ResolvedNameString] DEFAULT ('') NOT NULL,
    [CanonicalNameString] NVARCHAR (100) CONSTRAINT [DF_NameResolved_CanonicalNameString] DEFAULT ('') NOT NULL,
    [IsPreferred]         SMALLINT       CONSTRAINT [DF_NameResolved_IsPreferred] DEFAULT ((1)) NOT NULL,
    [CreationDate]        DATETIME       CONSTRAINT [DF_Table_1_CreationDateTime] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]    DATETIME       CONSTRAINT [DF_NameResolved_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]      INT            NULL,
    [LastModifiedUserID]  INT            NULL,
    CONSTRAINT [PK_NameResolved] PRIMARY KEY CLUSTERED ([NameResolvedID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_NameResolved_ResolvedNameString]
    ON [dbo].[NameResolved]([ResolvedNameString] ASC);

CREATE NONCLUSTERED INDEX IX_NameResolved_CanonicalNameString 
	ON [dbo].[NameResolved] ([CanonicalNameString]);
