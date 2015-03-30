CREATE TABLE dbo.TitleNote (
	TitleNoteID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TitleNote PRIMARY KEY CLUSTERED,
	NoteText nvarchar(max) NOT NULL CONSTRAINT DF_TitleNote_NoteText DEFAULT(''),
	ImportKey nvarchar(50) NOT NULL,
	ImportStatusID int NOT NULL,
	ImportSourceID int NULL,
	MarcDataFieldTag nvarchar(5) NULL,
	MarcIndicator1 nvarchar(5) NULL,
	NoteSequence smallint NULL,
	ExternalCreationDate datetime NULL,
	ExternalLastModifiedDate datetime NULL,
	ProductionDate datetime NULL,
	CreatedDate datetime NOT NULL CONSTRAINT DF_TitleNote_CreatedDate DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_TitleNote_LastModifiedDate DEFAULT(GETDATE()),
)
GO

ALTER TABLE dbo.TitleNote  WITH CHECK ADD CONSTRAINT FK_TitleNote_ImportSource FOREIGN KEY(ImportSourceID)
REFERENCES dbo.ImportSource (ImportSourceID)
GO

ALTER TABLE dbo.TitleNote CHECK CONSTRAINT FK_TitleNote_ImportSource
GO

ALTER TABLE dbo.TitleNote WITH CHECK ADD CONSTRAINT FK_TitleNote_ImportStatus FOREIGN KEY(ImportStatusID)
REFERENCES dbo.ImportStatus (ImportStatusID)
GO

ALTER TABLE dbo.TitleNote CHECK CONSTRAINT FK_TitleNote_ImportStatus
GO
