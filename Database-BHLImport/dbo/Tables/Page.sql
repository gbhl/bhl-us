CREATE TABLE [dbo].[Page] (
    [PageID]                   INT            IDENTITY (1, 1) NOT NULL,
    [ImportStatusID]           INT            NOT NULL,
    [ImportSourceID]           INT            NULL,
    [BarCode]                  NVARCHAR (200) NOT NULL,
    [FileNamePrefix]           NVARCHAR (50)  NOT NULL,
    [SequenceOrder]            INT            NULL,
    [PageDescription]          NVARCHAR (255) NULL,
    [Note]                     NVARCHAR (255) NULL,
    [FileSize_Temp]            INT            NULL,
    [FileExtension]            NVARCHAR (5)   NULL,
    [Active]                   BIT            CONSTRAINT [DF_Item_Active] DEFAULT ((1)) NOT NULL,
    [Year]                     NVARCHAR (20)  NULL,
    [Series]                   NVARCHAR (20)  NULL,
    [Volume]                   NVARCHAR (20)  NULL,
    [Issue]                    NVARCHAR (20)  NULL,
    [ExternalURL]              NVARCHAR (500) NULL,
    [IssuePrefix]              NVARCHAR (20)  NULL,
    [LastPageNameLookupDate]   DATETIME       NULL,
    [PaginationUserID]         INT            NULL,
    [PaginationDate]           DATETIME       NULL,
    [ExternalCreationDate]     DATETIME       NULL,
    [ExternalLastModifiedDate] DATETIME       NULL,
    [ExternalCreationUser]     INT            CONSTRAINT [DF_Page_CreationUserID] DEFAULT ((1)) NULL,
    [ExternalLastModifiedUser] INT            CONSTRAINT [DF_Page_LastModifiedUserID] DEFAULT ((1)) NULL,
    [ProductionDate]           DATETIME       NULL,
    [CreatedDate]              DATETIME       CONSTRAINT [DF_Page_CreatedDate_1] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]         DATETIME       CONSTRAINT [DF_Page_LastModifiedDate_1] DEFAULT (getdate()) NOT NULL,
    [Illustration]             BIT            CONSTRAINT [DF_Page_Illustration] DEFAULT ((0)) NULL,
    [AltExternalURL]           NVARCHAR (500) NULL,
    CONSTRAINT [aaaaaPage_PK] PRIMARY KEY NONCLUSTERED ([PageID] ASC),
    CONSTRAINT [FK_Page_ImportSource] FOREIGN KEY ([ImportSourceID]) REFERENCES [dbo].[ImportSource] ([ImportSourceID]),
    CONSTRAINT [FK_Page_ImportStatus] FOREIGN KEY ([ImportStatusID]) REFERENCES [dbo].[ImportStatus] ([ImportStatusID])
);


GO
CREATE CLUSTERED INDEX [IX_Page]
    ON [dbo].[Page]([BarCode] ASC, [SequenceOrder] ASC, [ImportSourceID] ASC, [ImportStatusID] ASC, [FileNamePrefix] ASC);


GO
CREATE NONCLUSTERED INDEX [BarCodePage]
    ON [dbo].[Page]([BarCode] ASC);


GO
CREATE NONCLUSTERED INDEX [Sequence]
    ON [dbo].[Page]([FileNamePrefix] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Page_ImportStatusImportSource]
    ON [dbo].[Page]([ImportStatusID] ASC, [ImportSourceID] ASC)
    INCLUDE([PageID]);

