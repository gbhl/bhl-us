CREATE TABLE import.ImportFile
	(
	ImportFileID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ImportFile PRIMARY KEY,
	ImportFileStatusID int NOT NULL,
	ImportFileName nvarchar(200) NOT NULL,
	ContributorCode nvarchar(10) NULL,
	SegmentGenreID int NULL,
	CreationDate datetime NOT NULL CONSTRAINT DF_ImportFile_CreationDate DEFAULT(GETDATE()),
	LastModifiedDate datetime NOT NULL CONSTRAINT DF_ImportFile_LastModifiedDate DEFAULT(GETDATE()),
	CreationUserID int NOT NULL CONSTRAINT DF_ImportFile_CreationUserID DEFAULT(1),
	LastModifiedUserID int NOT NULL CONSTRAINT DF_ImportFile_LastModifiedUserID DEFAULT(1)
	CONSTRAINT FK_ImportFile_ImportFileStatus FOREIGN KEY (ImportFileStatusID) 
			REFERENCES import.ImportFileStatus (ImportFileStatusID)
	)
