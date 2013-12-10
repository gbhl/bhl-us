CREATE TABLE dbo. PageDetailStatus
        (
        PageDetailStatusID int NOT NULL CONSTRAINT PK_PageDetailStatus PRIMARY KEY CLUSTERED,
        PageDetailStatusName nvarchar (30) NOT NULL DEFAULT (''),
        PageDetailStatusDescription nvarchar (250) NOT NULL DEFAULT ('')
        )
GO
