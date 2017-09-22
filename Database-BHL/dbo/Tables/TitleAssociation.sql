CREATE TABLE [dbo].[TitleAssociation] (
    [TitleAssociationID]     INT            IDENTITY (1, 1) NOT NULL,
    [TitleID]                INT            NOT NULL,
    [TitleAssociationTypeID] INT            NOT NULL,
    [Title]                  NVARCHAR (500) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF_TitleAssociation_Title] DEFAULT ('') NOT NULL,
    [Section]                NVARCHAR (500) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF_TitleAssociation_Section] DEFAULT ('') NOT NULL,
    [Volume]                 NVARCHAR (500) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF_TitleAssociation_Volume] DEFAULT ('') NOT NULL,
    [Active]                 BIT            CONSTRAINT [DF_TitleAssociation_Active] DEFAULT ((1)) NOT NULL,
    [AssociatedTitleID]      INT            NULL,
    [CreationDate]           DATETIME       CONSTRAINT [DF_TitleAssociation_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]       DATETIME       CONSTRAINT [DF_TitleAssociation_LastModifiedDate] DEFAULT (getdate()) NULL,
    [Heading]                NVARCHAR (500) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF_TitleAssociation_Heading] DEFAULT ('') NOT NULL,
    [Publication]            NVARCHAR (500) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF_TitleAssociation_PublicationInfo] DEFAULT ('') NOT NULL,
    [Relationship]           NVARCHAR (500) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF_TitleAssociation_Relationship] DEFAULT ('') NOT NULL,
    [CreationUserID]         INT            CONSTRAINT [DF_TitleAssociation_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID]     INT            CONSTRAINT [DF_TitleAssociation_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_TitleAssociation] PRIMARY KEY CLUSTERED ([TitleAssociationID] ASC),
    CONSTRAINT [FK_TitleAssociation_Title] FOREIGN KEY ([TitleID]) REFERENCES [dbo].[Title] ([TitleID]),
    CONSTRAINT [FK_TitleAssociation_Title1] FOREIGN KEY ([AssociatedTitleID]) REFERENCES [dbo].[Title] ([TitleID]),
    CONSTRAINT [FK_TitleAssociation_TitleAssociationType] FOREIGN KEY ([TitleAssociationTypeID]) REFERENCES [dbo].[TitleAssociationType] ([TitleAssociationTypeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_TitleAssociation]
    ON [dbo].[TitleAssociation]([TitleID] ASC, [Active] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TitleAssociation_Title]
    ON [dbo].[TitleAssociation]([Title] ASC, [Active] ASC)
    INCLUDE([TitleID]);


GO
