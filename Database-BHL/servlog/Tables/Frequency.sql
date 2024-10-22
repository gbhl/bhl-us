CREATE TABLE servlog.Frequency
	(
	FrequencyID int IDENTITY(1,1)
	,[Name] nvarchar(30) CONSTRAINT DF_Frequency_Name DEFAULT('') NOT NULL
	,[Label] nvarchar(30) CONSTRAINT DF_Frequency_Label DEFAULT('') NOT NULL
	,IntervalInMinutes int CONSTRAINT DF_Frequency_IntervalInMinutes DEFAULT(1440) NOT NULL
	,CreationDate datetime CONSTRAINT DF_Frequency_CreationDate DEFAULT GETDATE() NOT NULL
	,CreationUserID int CONSTRAINT DF_Frequency_CreationUserID DEFAULT (1) NOT NULL
	,LastModifiedDate datetime CONSTRAINT DF_Frequency_LastModifiedDate DEFAULT GETDATE() NOT NULL
	,LastModifiedUserID int CONSTRAINT DF_Frequency_LastModifiedUserID DEFAULT(1) NOT NULL
    ,CONSTRAINT [PK_ServLogFrequency] PRIMARY KEY CLUSTERED ([FrequencyID] ASC)
	)
GO
