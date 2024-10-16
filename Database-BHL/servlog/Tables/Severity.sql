CREATE TABLE servlog.Severity
	(
	SeverityID int IDENTITY(1,1)
	,[Name] nvarchar(30) CONSTRAINT DF_Severity_Name DEFAULT('') NOT NULL
	,[Label] nvarchar(30) CONSTRAINT DF_Severity_Label DEFAULT('') NOT NULL
	,FGColorHexCode varchar(10) CONSTRAINT DF_Severity_FGColorHexCode DEFAULT('#000000') NOT NULL
	,CreationDate datetime CONSTRAINT DF_Severity_CreationDate DEFAULT GETDATE() NOT NULL
	,CreationUserID int CONSTRAINT DF_Severity_CreationUserID DEFAULT (1) NOT NULL
	,LastModifiedDate datetime CONSTRAINT DF_Severity_LastModifiedDate DEFAULT GETDATE() NOT NULL
	,LastModifiedUserID int CONSTRAINT DF_Severity_LastModifiedUserID DEFAULT (1) NOT NULL
    ,CONSTRAINT [PK_ServLogSeverity] PRIMARY KEY CLUSTERED ([SeverityID] ASC)
	)
GO
