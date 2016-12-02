CREATE TABLE [dbo].[Page_PageType] (
    [PageID]             INT      NOT NULL,
    [PageTypeID]         INT      NOT NULL,
    [Verified]           BIT      CONSTRAINT [DF_Page_PageType_Verified] DEFAULT ((0)) NOT NULL,
    [CreationDate]       DATETIME CONSTRAINT [DF_Page_PageType_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]   DATETIME CONSTRAINT [DF_Page_PageType_LastModifiedDate] DEFAULT (getdate()) NULL,
    [CreationUserID]     INT      CONSTRAINT [DF_Page_PageType_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT      CONSTRAINT [DF_Page_PageType_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Page_PageType] PRIMARY KEY CLUSTERED ([PageID] ASC, [PageTypeID] ASC),
    CONSTRAINT [Page_PageType_FK00] FOREIGN KEY ([PageID]) REFERENCES [dbo].[Page] ([PageID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [Page_PageType_FK01] FOREIGN KEY ([PageTypeID]) REFERENCES [dbo].[PageType] ([PageTypeID]) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [IX_PagePageType_PageType] 
	ON [dbo].[Page_PageType] ([PageTypeID] ASC)
	INCLUDE ([PageID]);
GO
