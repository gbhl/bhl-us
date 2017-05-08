CREATE TABLE dbo.PageFlickrNote(
	PageFlickrNoteID int IDENTITY(1,1) NOT NULL,
	PageID int NOT NULL,
	PhotoID nvarchar(50) DEFAULT('') NOT NULL,
	FlickrNoteID nvarchar(100) DEFAULT('') NOT NULL,
	FlickrAuthorID nvarchar(100) DEFAULT('') NOT NULL,
	FlickrAuthorName nvarchar(150) DEFAULT('') NOT NULL,
	FlickrAuthorRealName nvarchar(150) DEFAULT('') NOT NULL,
	AuthorIsPro smallint DEFAULT(0) NOT NULL,
	XCoord int NULL,
	YCoord int NULL,
	Width int NULL,
	Height int NULL,
	NoteValue nvarchar(max) DEFAULT('') NOT NULL,
	IsActive tinyint DEFAULT(1) NOT NULL,
	CreationDate datetime DEFAULT(GETDATE()) NOT NULL,
	LastModifiedDate datetime DEFAULT(GETDATE()) NOT NULL,
	DeleteDate datetime NULL,
  CONSTRAINT PK_PageFlickrNote PRIMARY KEY CLUSTERED 
( 
	PageFlickrNoteID ASC 
)
)
