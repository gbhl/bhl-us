CREATE TABLE dbo.EntityCount
	(
	EntityCountID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_EntityCount PRIMARY KEY,
	EntityCountTypeID int NOT NULL,
	CountValue int NOT NULL,
	CreationDate datetime DEFAULT(GETDATE()),
	CONSTRAINT FK_EntityCount_EntityCountType FOREIGN KEY (EntityCountTypeID) REFERENCES dbo.EntityCountType(EntityCountTypeID)
	)
