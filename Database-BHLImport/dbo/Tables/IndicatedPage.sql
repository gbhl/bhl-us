CREATE TABLE [dbo].[IndicatedPage] (
    [IndicatedPageID]          INT            IDENTITY (1, 1) NOT NULL,
    [BarCode]                  NVARCHAR (200) NOT NULL,
    [FileNamePrefix]           NVARCHAR (50)  NOT NULL,
    [SequenceOrder]            INT            NULL,
    [Sequence]                 SMALLINT       NULL,
    [ImportStatusID]           INT            NOT NULL,
    [ImportSourceID]           INT            NULL,
    [PagePrefix]               NVARCHAR (40)  NULL,
    [PageNumber]               NVARCHAR (20)  NULL,
    [Implied]                  BIT            NOT NULL,
    [ExternalCreationDate]     DATETIME       NULL,
    [ExternalLastModifiedDate] DATETIME       NULL,
    [ExternalCreationUser]     INT            CONSTRAINT [DF_IndicatedPage_ExternalCreationUserID] DEFAULT ((1)) NULL,
    [ExternalLastModifiedUser] INT            CONSTRAINT [DF_IndicatedPage_ExternalLastModifiedUserID] DEFAULT ((1)) NULL,
    [ProductionDate]           DATETIME       NULL,
    [CreatedDate]              DATETIME       CONSTRAINT [DF_IndicatedPage_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]         DATETIME       CONSTRAINT [DF_IndicatedPage_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_IndicatedPage] PRIMARY KEY CLUSTERED ([IndicatedPageID] ASC),
    CONSTRAINT [FK_IndicatedPage_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID]),
    CONSTRAINT [FK_IndicatedPage_ImportStatus] FOREIGN KEY ([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
);


GO
CREATE NONCLUSTERED INDEX [IX_IndicatedPage]
    ON [dbo].[IndicatedPage]([Sequence] ASC, [BarCode] ASC, [FileNamePrefix] ASC, [IndicatedPageID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_IndicatedPage_BarCodeImportStatusImportSource]
    ON [dbo].[IndicatedPage]([BarCode] ASC, [ImportStatusID] ASC, [ImportSourceID] ASC)
    INCLUDE([IndicatedPageID], [FileNamePrefix], [SequenceOrder], [Sequence], [PagePrefix], [PageNumber], [Implied], [ExternalCreationDate], [ExternalLastModifiedDate], [ExternalCreationUser], [ExternalLastModifiedUser]);


GO
CREATE NONCLUSTERED INDEX [IX_IndicatedPage_ImportStatusImportSource]
    ON [dbo].[IndicatedPage]([ImportStatusID] ASC, [ImportSourceID] ASC)
    INCLUDE([IndicatedPageID]);

