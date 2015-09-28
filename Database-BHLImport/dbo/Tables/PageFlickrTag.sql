CREATE TABLE [dbo].[PageFlickrTag](
	[PageFlickrTagID] [int] IDENTITY(1,1) NOT NULL,
	[PageID] [int] NOT NULL,
	[PhotoID] [nvarchar](50) CONSTRAINT [DF_PageFlickTagPhotoID]  DEFAULT ('') NOT NULL,
	[IsMachineTag] [smallint] CONSTRAINT [DF_PageFlickTagIsMachineTag]  DEFAULT ((0)) NOT NULL,
	[TagValue] [nvarchar](1000) CONSTRAINT [DF_PageFlickTagTagValue]  DEFAULT ('') NOT NULL,
	[FlickrAuthorID] [nvarchar](100) CONSTRAINT [DF_PageFlickTagFlickrAuthorID]  DEFAULT ('') NOT NULL,
	[FlickrAuthorName] [nvarchar](150) CONSTRAINT [DF_PageFlickTagFlickrAuthorName]  DEFAULT ('') NOT NULL,
	[IsActive] [tinyint] CONSTRAINT [DF_PageFlickrTagIsActive]  DEFAULT ((1)) NOT NULL,
	[CreationDate] [datetime] CONSTRAINT [DF_PageFlickrTagCreationDate]  DEFAULT (getdate()) NOT NULL,
	[LastModifiedDate] [datetime] CONSTRAINT [DF_PageFlickrTagLastModifiedDate] DEFAULT (getdate()) NOT NULL,
	[DeleteDate] [datetime] NULL,
 CONSTRAINT [PK_PageFlickrTag] PRIMARY KEY CLUSTERED 
(
	[PageFlickrTagID] ASC
)
)


