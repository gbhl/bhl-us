CREATE TABLE [dbo].[Title_TitleIdentifier] (
    [Title_TitleIdentifierID]  INT            IDENTITY (1, 1) NOT NULL,
    [ImportKey]                NVARCHAR (50)  CONSTRAINT [DF_Title_TitleIdentifier_ImportKey] DEFAULT ('') NOT NULL,
    [ImportStatusID]           INT            NOT NULL,
    [ImportSourceID]           INT            NULL,
    [IdentifierName]           NVARCHAR (40)  NOT NULL,
    [IdentifierValue]          NVARCHAR (125) CONSTRAINT [DF_Title_TitleIdentifier_IdentifierValue] DEFAULT ('') NOT NULL,
    [ExternalCreationDate]     DATETIME       NULL,
    [ExternalLastModifiedDate] DATETIME       NULL,
    [ProductionDate]           DATETIME       NULL,
    [CreatedDate]              DATETIME       CONSTRAINT [DF_Title_TitleIdentifier_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]         DATETIME       CONSTRAINT [DF_Title_TitleIdentifier_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Title_TitleIdentifier] PRIMARY KEY CLUSTERED ([Title_TitleIdentifierID] ASC),
    CONSTRAINT [FK_Title_TitleIdentifier_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID]),
    CONSTRAINT [FK_Title_TitleIdentifier_ImportStatus] FOREIGN KEY ([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
);

