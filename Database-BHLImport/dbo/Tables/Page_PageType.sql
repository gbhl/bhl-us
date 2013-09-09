CREATE TABLE [dbo].[Page_PageType] (
    [PagePageTypeID]           INT           IDENTITY (1, 1) NOT NULL,
    [BarCode]                  NVARCHAR (40) NOT NULL,
    [FileNamePrefix]           NVARCHAR (50) NOT NULL,
    [SequenceOrder]            INT           NULL,
    [PageTypeID]               INT           NOT NULL,
    [ImportStatusID]           INT           NOT NULL,
    [ImportSourceID]           INT           NULL,
    [Verified]                 BIT           CONSTRAINT [DF_Page_PageType_Verified] DEFAULT ((0)) NOT NULL,
    [ExternalCreationDate]     DATETIME      NULL,
    [ExternalLastModifiedDate] DATETIME      NULL,
    [ExternalCreationUser]     INT           CONSTRAINT [DF_Page_PageType_ExternalCreationUserID] DEFAULT ((1)) NULL,
    [ExternalLastModifiedUser] INT           CONSTRAINT [DF_Page_PageType_ExternalLastModifiedUserID] DEFAULT ((1)) NULL,
    [ProductionDate]           DATETIME      NULL,
    [CreatedDate]              DATETIME      CONSTRAINT [DF_Page_PageType_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]         DATETIME      CONSTRAINT [DF_Page_PageType_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Page_PageType] PRIMARY KEY CLUSTERED ([PagePageTypeID] ASC),
    CONSTRAINT [FK_Page_PageType_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID]),
    CONSTRAINT [FK_PagePageType_ImportStatus] FOREIGN KEY ([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Page_PageType]
    ON [dbo].[Page_PageType]([BarCode] ASC, [FileNamePrefix] ASC, [PageTypeID] ASC, [PagePageTypeID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PagePageType_BarCodeImportStatusImportSource]
    ON [dbo].[Page_PageType]([BarCode] ASC, [ImportStatusID] ASC, [ImportSourceID] ASC)
    INCLUDE([PagePageTypeID], [FileNamePrefix], [SequenceOrder], [PageTypeID], [Verified], [ExternalCreationDate], [ExternalLastModifiedDate], [ExternalCreationUser], [ExternalLastModifiedUser]);


GO
CREATE NONCLUSTERED INDEX [IX_PagePageType_ImportStatusImportSource]
    ON [dbo].[Page_PageType]([ImportStatusID] ASC, [ImportSourceID] ASC)
    INCLUDE([PagePageTypeID]);

