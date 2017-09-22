CREATE TABLE [dbo].[Page] (
    [PageID]                 INT            IDENTITY (1, 1) NOT NULL,
    [ItemID]                 INT            NOT NULL,
    [FileNamePrefix]         NVARCHAR (50)  NOT NULL,
    [SequenceOrder]          INT            NULL,
    [PageDescription]        NVARCHAR (255) NULL,
    [Illustration]           BIT            CONSTRAINT [DF_Page_Illustration] DEFAULT ((0)) NOT NULL,
    [Note]                   NVARCHAR (255) NULL,
    [FileSize_Temp]          INT            NULL,
    [FileExtension]          NVARCHAR (5)   NULL,
    [CreationDate]           DATETIME       CONSTRAINT [DF__Page__Created__2610A626] DEFAULT (getdate()) NULL,
    [LastModifiedDate]       DATETIME       CONSTRAINT [DF__Page__Changed__2704CA5F] DEFAULT (getdate()) NULL,
    [CreationUserID]         INT            CONSTRAINT [DF_Page_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID]     INT            CONSTRAINT [DF_Page_LastModifiedUserID] DEFAULT ((1)) NULL,
    [Active]                 BIT            CONSTRAINT [DF_Item_Active] DEFAULT ((1)) NOT NULL,
    [Year]                   NVARCHAR (20)  NULL,
    [Series]                 NVARCHAR (20)  NULL,
    [Volume]                 NVARCHAR (20)  NULL,
    [Issue]                  NVARCHAR (20)  NULL,
    [ExternalURL]            NVARCHAR (500) NULL,
    [IssuePrefix]            NVARCHAR (20)  NULL,
    [LastPageNameLookupDate] DATETIME       NULL,
    [PaginationUserID]       INT            NULL,
    [PaginationDate]         DATETIME       NULL,
    [AltExternalURL]         NVARCHAR (500) NULL,
    CONSTRAINT [aaaaaPage_PK] PRIMARY KEY CLUSTERED ([PageID] ASC),
    CONSTRAINT [Page_FK00] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Item] ([ItemID]) ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [ItemPage]
    ON [dbo].[Page]([ItemID] ASC);


GO
CREATE NONCLUSTERED INDEX [Sequence]
    ON [dbo].[Page]([FileNamePrefix] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Page_Active]
    ON [dbo].[Page]([Active] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Page_ActiveItemID]
    ON [dbo].[Page]([Active] ASC, [ItemID] ASC)
    INCLUDE([PageID], [CreationDate]);


GO
CREATE NONCLUSTERED INDEX [IX_Page_LastPageNameLookupDate]
    ON [dbo].[Page]([LastPageNameLookupDate] ASC)
    INCLUDE([PageID], [ItemID], [FileNamePrefix], [SequenceOrder]);


GO
CREATE NONCLUSTERED INDEX [IX_Page_ItemIDActiveSeqOrder] 
	ON [dbo].[Page]([ItemID] ASC, [Active] ASC, [SequenceOrder] ASC)
	INCLUDE([PageID], [FileNamePrefix], [Illustration], [Year], [Series], [Volume], [Issue], [ExternalURL], [IssuePrefix], [AltExternalURL]);


GO
CREATE NONCLUSTERED INDEX [IX_Page_PageIDActiveSequence]
    ON [dbo].[Page]([PageID] ASC, [Active] ASC, [SequenceOrder] ASC)
    INCLUDE([ItemID], [Year]);


GO
CREATE NONCLUSTERED INDEX [IX_Page_PageIDActiveItem]
    ON [dbo].[Page]([PageID] ASC, [Active] ASC, [ItemID] ASC);


GO
CREATE NONCLUSTERED INDEX IX_Page_ItemIDSequence 
	ON [dbo].[Page]([ItemID],[SequenceOrder])
	INCLUDE([PageID])


GO
