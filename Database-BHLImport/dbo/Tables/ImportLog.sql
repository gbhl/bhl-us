CREATE TABLE [dbo].[ImportLog] (
	[ImportLogID]                           int           IDENTITY(1,1) NOT NULL,
	[ImportDate]                            datetime      NOT NULL,
	[ImportSourceID]                        int           NULL,
	[BarCode]                               nvarchar(40)  NULL,
	[ImportResult]                          nvarchar(30)  NOT NULL,
	[TableName]                             nvarchar(100) NOT NULL,
	[Action]                                nvarchar(20)  NOT NULL,
	[Rows]                                  int           NOT NULL
    CONSTRAINT [PK_ImportLog] PRIMARY KEY CLUSTERED ([ImportLogID] ASC)
);

ALTER TABLE dbo.ImportLog ADD CONSTRAINT DF_ImportLog_ImportDate DEFAULT GETDATE() FOR ImportDate;
ALTER TABLE dbo.ImportLog ADD CONSTRAINT DF_ImportLog_ImportResult DEFAULT('') FOR ImportResult;
ALTER TABLE dbo.ImportLog ADD CONSTRAINT DF_ImportLog_TableName DEFAULT('') FOR TableName;
ALTER TABLE dbo.ImportLog ADD CONSTRAINT DF_ImportLog_Action DEFAULT('') FOR [Action];
ALTER TABLE dbo.ImportLog ADD CONSTRAINT DF_ImportLog_Rows DEFAULT(0) FOR [Rows];
