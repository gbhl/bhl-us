CREATE TABLE [dbo].[Identifier] (
    [IdentifierID]       INT           IDENTITY (1, 1) NOT NULL,
    [IdentifierType]     NVARCHAR (40) CONSTRAINT [DF_Identifier_IdentifierType] DEFAULT ('') NOT NULL,
    [IdentifierName]     NVARCHAR (40) CONSTRAINT [DF_Identifier_IdentifierName] DEFAULT ('') NOT NULL,
    [IdentifierLabel]    NVARCHAR (50) CONSTRAINT [DF_Identifier_IdentifierLabel] DEFAULT ('') NOT NULL,
	[Prefix]             NVARCHAR(100) CONSTRAINT [DF_Identifier_Prefix] DEFAULT ('') NOT NULL,
	[PatternExpression]  NVARCHAR(200) CONSTRAINT [DF_Identifier_PatternExpression] DEFAULT ('') NOT NULL,
	[PatternDescription] NVARCHAR(500) CONSTRAINT [DF_Identifier_PatternDescription] DEFAULT ('') NOT NULL,
	[MaximumPerEntity]   int           CONSTRAINT [DF_Identifier_MaximumPerEntity] DEFAULT ((0)) NOT NULL,
    [Display]            SMALLINT      CONSTRAINT [DF_Identifier_Display] DEFAULT ((1)) NOT NULL,
    [CreationDate]       DATETIME      CONSTRAINT [DF_Identifier_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME      CONSTRAINT [DF_Identifier_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT           CONSTRAINT [DF_Identifier_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT           CONSTRAINT [DF_Identifier_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Identifier] PRIMARY KEY CLUSTERED ([IdentifierID] ASC)
);

GO
