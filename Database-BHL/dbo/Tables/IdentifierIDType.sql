CREATE TABLE dbo.IdentifierIDType
(
	IdentifierIDTypeID INT IDENTITY(1,1) CONSTRAINT PK_IdentifierIDType  PRIMARY KEY NOT NULL,
	IdentifierID INT NOT NULL,
	IDTypeID INT NOT NULL,
	CreationDate DATETIME CONSTRAINT DF_IdentifierIDType_CreationDate DEFAULT (GETDATE()) NOT NULL,
	LastModifiedDate DATETIME CONSTRAINT DF_IdentifierIDType_LastModifiedDate DEFAULT (GETDATE()) NOT NULL,
	CreationUserID INT CONSTRAINT DF_IdentifierIDType_CreationUserID DEFAULT (1) NOT NULL,
	LastModifiedUserID INT CONSTRAINT DF_IdentifierIDType_LastModifiedUserID  DEFAULT (1) NOT NULL
)
GO
ALTER TABLE dbo.IdentifierIDType 
	ADD CONSTRAINT	FK_IdentifierIDType_IDType FOREIGN KEY ( IDTypeID ) 
	REFERENCES dbo.IDType ( IDTypeID )	
GO
ALTER TABLE dbo.IdentifierIDType 
	ADD CONSTRAINT FK_IdentifierIDType_Identifier FOREIGN KEY ( IdentifierID ) 
	REFERENCES dbo.Identifier ( IdentifierID )
GO
