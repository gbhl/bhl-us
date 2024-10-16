CREATE TABLE servlog.[Service]
	(
	ServiceID int IDENTITY(1,1)
	,[Name] nvarchar(200) CONSTRAINT DF_Service_Name DEFAULT('') NOT NULL
	,[Param] nvarchar(30) CONSTRAINT DF_Service_Param DEFAULT('') NOT NULL
	,FrequencyID int NULL
	,[Disabled] tinyint CONSTRAINT DF_Service_Disabled DEFAULT(0) NOT NULL
	,Display tinyint CONSTRAINT DF_Service_Display DEFAULT(1) NOT NULL
	,CreationDate datetime CONSTRAINT DF_Service_CreationDate DEFAULT GETDATE() NOT NULL
	,CreationUserID int CONSTRAINT DF_Service_CreationUserID DEFAULT (1) NOT NULL
	,LastModifiedDate datetime CONSTRAINT DF_Service_LastModifiedDate DEFAULT GETDATE() NOT NULL
	,LastModifiedUserID int CONSTRAINT DF_Service_LastModifiedUserID DEFAULT(1) NOT NULL
    ,CONSTRAINT [PK_ServLogService] PRIMARY KEY CLUSTERED ([ServiceID] ASC)
	)
GO
