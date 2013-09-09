CREATE TABLE [dbo].[TitleAssociation] (
    [TitleAssociationID] INT            IDENTITY (1, 1) NOT NULL,
    [ImportKey]          NVARCHAR (50)  CONSTRAINT [DF_TitleAssociation_ImportKey] DEFAULT ('') NOT NULL,
    [ImportStatusID]     INT            NOT NULL,
    [ImportSourceID]     INT            NOT NULL,
    [MARCTag]            NVARCHAR (20)  NOT NULL,
    [MARCIndicator2]     NCHAR (1)      CONSTRAINT [DF__TitleAsso__MARCI__172E5549] DEFAULT ('') NOT NULL,
    [Title]              NVARCHAR (500) CONSTRAINT [DF__TitleAsso__Title__18227982] DEFAULT ('') NOT NULL,
    [Section]            NVARCHAR (500) CONSTRAINT [DF__TitleAsso__Secti__19169DBB] DEFAULT ('') NOT NULL,
    [Volume]             NVARCHAR (500) CONSTRAINT [DF__TitleAsso__Volum__1A0AC1F4] DEFAULT ('') NOT NULL,
    [Active]             BIT            CONSTRAINT [DF__TitleAsso__Activ__1AFEE62D] DEFAULT ((1)) NOT NULL,
    [ProductionDate]     DATETIME       NULL,
    [CreatedDate]        DATETIME       CONSTRAINT [DF__TitleAsso__Creat__1BF30A66] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME       CONSTRAINT [DF__TitleAsso__LastM__1CE72E9F] DEFAULT (getdate()) NOT NULL,
    [Heading]            NVARCHAR (500) CONSTRAINT [DF_TitleAssociation_Heading] DEFAULT ('') NOT NULL,
    [Publication]        NVARCHAR (500) CONSTRAINT [DF_TitleAssociation_Publication] DEFAULT ('') NOT NULL,
    [Relationship]       NVARCHAR (500) CONSTRAINT [DF_TitleAssociation_Relationship] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK__TitleAssociation__163A3110] PRIMARY KEY CLUSTERED ([TitleAssociationID] ASC),
    CONSTRAINT [FK_TitleAssociation_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID]),
    CONSTRAINT [FK_TitleAssociation_ImportStatus] FOREIGN KEY ([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
);

