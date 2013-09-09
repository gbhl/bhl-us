CREATE TABLE [dbo].[TitleAssociation_TitleIdentifier] (
    [TitleAssociation_TitleIdentifierID] INT            IDENTITY (1, 1) NOT NULL,
    [ImportKey]                          NVARCHAR (50)  CONSTRAINT [DF_TitleAssociation_TitleIdentifier_ImportKey] DEFAULT ('') NOT NULL,
    [ImportStatusID]                     INT            NOT NULL,
    [ImportSourceID]                     INT            NOT NULL,
    [MARCTag]                            NVARCHAR (20)  NOT NULL,
    [MARCIndicator2]                     NCHAR (1)      CONSTRAINT [DF__TitleAsso__MARCI__1FC39B4A] DEFAULT ('') NOT NULL,
    [Title]                              NVARCHAR (500) CONSTRAINT [DF__TitleAsso__Title__20B7BF83] DEFAULT ('') NOT NULL,
    [Section]                            NVARCHAR (500) CONSTRAINT [DF__TitleAsso__Secti__21ABE3BC] DEFAULT ('') NOT NULL,
    [Volume]                             NVARCHAR (500) CONSTRAINT [DF__TitleAsso__Volum__22A007F5] DEFAULT ('') NOT NULL,
    [IdentifierName]                     NVARCHAR (40)  NOT NULL,
    [IdentifierValue]                    NVARCHAR (125) NOT NULL,
    [ProductionDate]                     DATETIME       NULL,
    [CreatedDate]                        DATETIME       CONSTRAINT [DF__TitleAsso__Creat__23942C2E] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]                   DATETIME       CONSTRAINT [DF__TitleAsso__LastM__24885067] DEFAULT (getdate()) NOT NULL,
    [Heading]                            NVARCHAR (500) CONSTRAINT [DF_TitleAssociation_TitleIdentifier_Heading] DEFAULT ('') NOT NULL,
    [Publication]                        NVARCHAR (500) CONSTRAINT [DF_TitleAssociation_TitleIdentifier_Publication] DEFAULT ('') NOT NULL,
    [Relationship]                       NVARCHAR (500) CONSTRAINT [DF_TitleAssociation_TitleIdentifier_Relationship] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK__TitleAssociation__1ECF7711] PRIMARY KEY CLUSTERED ([TitleAssociation_TitleIdentifierID] ASC),
    CONSTRAINT [FK_TitleAssociation_TitleIdentifier_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID]),
    CONSTRAINT [FK_TitleAssociation_TitleIdentifier_ImportStatus] FOREIGN KEY ([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
);

