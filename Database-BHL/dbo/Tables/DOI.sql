CREATE TABLE [dbo].[DOI] (
    [DOIID]            INT             IDENTITY (1, 1) NOT NULL,
    [DOIEntityTypeID]  INT             NOT NULL,
    [EntityID]         INT             NOT NULL,
    [DOIStatusID]      INT             NOT NULL,
    [DOIBatchID]       NVARCHAR (50)   CONSTRAINT [DF_DOI_DOIBatchID] DEFAULT ('') NOT NULL,
    [DOIName]          NVARCHAR (50)   CONSTRAINT [DF_DOIName] DEFAULT ('') NOT NULL,
    [StatusDate]       DATETIME        CONSTRAINT [DF_DOI_StatusDate] DEFAULT (getdate()) NOT NULL,
    [StatusMessage]    NVARCHAR (1000) CONSTRAINT [DF_DOI_StatusMessage] DEFAULT ('') NOT NULL,
    [IsValid]          SMALLINT        CONSTRAINT [DF_DOI_IsValid] DEFAULT ((0)) NOT NULL,
    [CreationDate]     DATETIME        CONSTRAINT [DF_DOI_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate] DATETIME        CONSTRAINT [DF_DOI_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_DOI] PRIMARY KEY CLUSTERED ([DOIID] ASC),
    CONSTRAINT [FK_DOI_DOIEntityType] FOREIGN KEY ([DOIEntityTypeID]) REFERENCES [dbo].[DOIEntityType] ([DOIEntityTypeID]),
    CONSTRAINT [FK_DOI_DOIStatus] FOREIGN KEY ([DOIStatusID]) REFERENCES [dbo].[DOIStatus] ([DOIStatusID])
);


GO
CREATE NONCLUSTERED INDEX [IX_DOI_TypeIDEntityID]
    ON [dbo].[DOI]([DOIEntityTypeID] ASC, [EntityID] ASC)
    INCLUDE([DOIName]);


GO
CREATE NONCLUSTERED INDEX [IX_DOI_EntityIsValid]
    ON [dbo].[DOI]([EntityID] ASC, [IsValid] ASC);


GO
CREATE TRIGGER dbo.DOI_AuditBasic_Insert ON [dbo].[DOI]
 AFTER Insert
 NOT FOR REPLICATION
 AS 
 BEGIN 
 SET NOCOUNT ON 
 SET ARITHABORT ON 
 -- patterned after AutoAudit created by Paul Nielsen 
 -- www.SQLServerBible.com 

DECLARE @AuditTime DATETIME
SET @AuditTime = GetDate()

 BEGIN TRY 
 DECLARE @UserSQL nvarchar(max)
 SET @UserSQL = ''
 IF (SUSER_NAME() <> 'BotanicusService' AND SUSER_NAME() <> 'BHLWebUser' AND SUSER_NAME() <> 'MOBOT\SQLSERVER')
 BEGIN
  -- capture SQL Statement
  DECLARE @ExecStr varchar(50)
  DECLARE  @inputbuffer TABLE (EventType nvarchar(30), Parameters int, EventInfo nvarchar(max))
  SET @ExecStr = 'DBCC INPUTBUFFER(@@SPID) with no_infomsgs'
  INSERT INTO @inputbuffer EXEC (@ExecStr)
  SELECT @UserSQL = EventInfo FROM @inputbuffer
 END

 INSERT audit.AuditBasic (AuditDate, SystemUserID, EntityName, Operation, SQLStatement, EntityKey1, ApplicationUserID)
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.DOI', 'I',@UserSQL, Inserted.DOIID,NULL
 FROM Inserted

 END TRY 
 BEGIN CATCH 
   DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT
   SET @ErrorMessage = ERROR_MESSAGE()  
   SET @ErrorSeverity = ERROR_SEVERITY() 
   SET @ErrorState = ERROR_STATE()  
   RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) WITH LOG
 END CATCH 
 END 
GO
EXECUTE sp_settriggerorder @triggername = N'[dbo].[DOI_AuditBasic_Insert]', @order = N'last', @stmttype = N'insert';


GO
CREATE TRIGGER dbo.DOI_AuditBasic_Update ON [dbo].[DOI]
 AFTER Update
 NOT FOR REPLICATION
 AS 
 BEGIN 
 SET NOCOUNT ON 
 -- patterned after AutoAudit created by Paul Nielsen 
 -- www.SQLServerBible.com 

DECLARE @AuditTime DATETIME, @IsDirty BIT
SET @AuditTime = GetDate()

SET @IsDirty = 0

 BEGIN TRY 
 DECLARE @UserSQL nvarchar(max)
 SET @UserSQL = ''
 IF (SUSER_NAME() <> 'BotanicusService' AND SUSER_NAME() <> 'BHLWebUser' AND SUSER_NAME() <> 'MOBOT\SQLSERVER')
 BEGIN
  -- capture SQL Statement
  DECLARE @ExecStr varchar(50)
  DECLARE  @inputbuffer TABLE (EventType nvarchar(30), Parameters int, EventInfo nvarchar(max))
  SET @ExecStr = 'DBCC INPUTBUFFER(@@SPID) with no_infomsgs'
  INSERT INTO @inputbuffer EXEC (@ExecStr)
  SELECT @UserSQL = EventInfo FROM @inputbuffer
 END

 INSERT audit.AuditBasic (AuditDate, SystemUserID, EntityName, Operation, SQLStatement, EntityKey1, ApplicationUserID)
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.DOI', 'U',@UserSQL, Inserted.DOIID,NULL
 FROM Inserted

 END TRY 
 BEGIN CATCH 
   DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT
   SET @ErrorMessage = ERROR_MESSAGE()  
   SET @ErrorSeverity = ERROR_SEVERITY() 
   SET @ErrorState = ERROR_STATE()  
   RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) WITH LOG
 END CATCH 
 END 
GO
EXECUTE sp_settriggerorder @triggername = N'[dbo].[DOI_AuditBasic_Update]', @order = N'last', @stmttype = N'update';

