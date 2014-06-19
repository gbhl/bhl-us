CREATE TABLE dbo.EntityCountType
	(
	EntityCountTypeID int NOT NULL CONSTRAINT PK_EntityCountType PRIMARY KEY,
	FullName nvarchar(30) CONSTRAINT DF_EntityCountType_FullName DEFAULT('') NOT NULL,
	DisplayName nvarchar(20) CONSTRAINT DF_EntityCountType_DisplayName DEFAULT('') NOT NULL,
	Sequence int NOT NULL,
	)
