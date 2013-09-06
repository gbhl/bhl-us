CREATE TABLE [dbo].[PageFlickr] (
    [PageFlickrID]   INT           IDENTITY (1, 1) NOT NULL,
    [PageID]         INT           NOT NULL,
    [FlickrURL]      VARCHAR (500) NOT NULL,
    [CreationUserID] INT           NULL,
    [CreationDate]   DATETIME      NULL,
    CONSTRAINT [PK_PageFlickr] PRIMARY KEY CLUSTERED ([PageFlickrID] ASC),
    CONSTRAINT [FK_PageFlickr_Page] FOREIGN KEY ([PageID]) REFERENCES [dbo].[Page] ([PageID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PageFlickr_PageID]
    ON [dbo].[PageFlickr]([PageID] ASC);

