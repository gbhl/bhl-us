CREATE TABLE [dbo].[PageName] (
    [PageNameID]             INT            IDENTITY (1, 1) NOT NULL,
    [ImportStatusID]         INT            NOT NULL,
    [ImportSourceID]         INT            NULL,
    [BarCode]                NVARCHAR (40)  NOT NULL,
    [FileNamePrefix]         NVARCHAR (50)  NOT NULL,
    [SequenceOrder]          INT            NULL,
    [Source]                 NVARCHAR (50)  NULL,
    [NameFound]              NVARCHAR (100) NULL,
    [NameConfirmed]          NVARCHAR (100) NULL,
    [NameBankID]             INT            NULL,
    [Active]                 BIT            NULL,
    [ExternalCreateDate]     DATETIME       NULL,
    [ExternalLastUpdateDate] DATETIME       NULL,
    [IsCommonName]           BIT            NULL,
    [ProductionDate]         DATETIME       NULL,
    [CreatedDate]            DATETIME       CONSTRAINT [DF_PageName_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]       DATETIME       CONSTRAINT [DF_PageName_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PageName] PRIMARY KEY CLUSTERED ([PageNameID] ASC),
    CONSTRAINT [FK_PageName_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID]),
    CONSTRAINT [FK_PageName_ImportStatus] FOREIGN KEY ([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
);


GO
CREATE NONCLUSTERED INDEX [IX_PageName_ImportStatusImportSource]
    ON [dbo].[PageName]([ImportStatusID] ASC, [ImportSourceID] ASC)
    INCLUDE([PageNameID]);


GO
CREATE NONCLUSTERED INDEX [IX_PageName]
    ON [dbo].[PageName]([BarCode] ASC, [FileNamePrefix] ASC, [SequenceOrder] ASC, [NameFound] ASC, [ImportSourceID] ASC, [ImportStatusID] ASC, [PageNameID] ASC);

