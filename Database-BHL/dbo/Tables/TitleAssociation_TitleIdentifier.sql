CREATE TABLE [dbo].[TitleAssociation_TitleIdentifier] (
    [TitleAssociation_TitleIdentifierID] INT           IDENTITY (1, 1) NOT NULL,
    [TitleAssociationID]                 INT           NOT NULL,
    [TitleIdentifierID]                  INT           NOT NULL,
    [IdentifierValue]                    VARCHAR (125) CONSTRAINT [DF_TitleAssociation_TitleIdentifier_IdentifierValue] DEFAULT ('') NOT NULL,
    [CreationDate]                       DATETIME      CONSTRAINT [DF_TitleAssociation_TitleIdentifier_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]                   DATETIME      CONSTRAINT [DF_TitleAssociation_TitleIdentifier_LastModifiedDate] DEFAULT (getdate()) NULL,
    [CreationUserID]                     INT           CONSTRAINT [DF_TitleAssociation_TitleIdentifier_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID]                 INT           CONSTRAINT [DF_TitleAssociation_TitleIdentifier_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_TitleAssociation_TitleIdentifier] PRIMARY KEY CLUSTERED ([TitleAssociation_TitleIdentifierID] ASC),
    CONSTRAINT [FK_TitleAssociation_TitleIdentifier_Identifier] FOREIGN KEY ([TitleIdentifierID]) REFERENCES [dbo].[Identifier] ([IdentifierID]),
    CONSTRAINT [FK_TitleAssociation_TitleIdentifier_TitleAssociation] FOREIGN KEY ([TitleAssociationID]) REFERENCES [dbo].[TitleAssociation] ([TitleAssociationID]) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [IX_TitleAssociationTitleIdentifier_TitleAssociation] 
	ON [dbo].[TitleAssociation_TitleIdentifier] ([TitleAssociationID] ASC)
GO
