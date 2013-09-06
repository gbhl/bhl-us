CREATE TABLE [dbo].[IndicatedPage] (
    [PageID]             INT           NOT NULL,
    [Sequence]           SMALLINT      NOT NULL,
    [PagePrefix]         NVARCHAR (40) NULL,
    [PageNumber]         NVARCHAR (20) NULL,
    [Implied]            BIT           NOT NULL,
    [CreationDate]       DATETIME      CONSTRAINT [DF_IndicatedPage_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]   DATETIME      CONSTRAINT [DF_IndicatedPage_LastModifiedDate] DEFAULT (getdate()) NULL,
    [CreationUserID]     INT           CONSTRAINT [DF_IndicatedPage_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT           CONSTRAINT [DF_IndicatedPage_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_IndicatedPage] PRIMARY KEY CLUSTERED ([PageID] ASC, [Sequence] ASC),
    CONSTRAINT [CK IndicatedPage Sequence] CHECK ([Sequence]>=(1) AND [Sequence]<=(4)),
    CONSTRAINT [IndicatedPage_FK00] FOREIGN KEY ([PageID]) REFERENCES [dbo].[Page] ([PageID]) ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_IndicatedPage_PageNumber]
    ON [dbo].[IndicatedPage]([PageNumber] ASC)
    INCLUDE([PageID], [PagePrefix]);


GO
CREATE TRIGGER dbo.IndicatedPage_AuditBasic_Insert ON [dbo].[IndicatedPage]
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

 INSERT audit.AuditBasic (AuditDate, SystemUserID, EntityName, Operation, SQLStatement, EntityKey1, EntityKey2, ApplicationUserID)
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.IndicatedPage', 'I',@UserSQL, Inserted.PageID, Inserted.Sequence,Inserted.CreationUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[IndicatedPage_AuditBasic_Insert]', @order = N'last', @stmttype = N'insert';


GO
CREATE TRIGGER dbo.IndicatedPage_AuditBasic_Update ON [dbo].[IndicatedPage]
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

 INSERT audit.AuditBasic (AuditDate, SystemUserID, EntityName, Operation, SQLStatement, EntityKey1, EntityKey2, ApplicationUserID)
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.IndicatedPage', 'U',@UserSQL, Inserted.PageID, Inserted.Sequence,Inserted.LastModifiedUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[IndicatedPage_AuditBasic_Update]', @order = N'last', @stmttype = N'update';


GO
CREATE TRIGGER dbo.IndicatedPage_AuditBasic_Delete ON [dbo].[IndicatedPage]
 AFTER Delete
 NOT FOR REPLICATION
 AS 
 BEGIN 
 SET NOCOUNT ON 
 -- patterned after AutoAudit created by Paul Nielsen 
 -- www.SQLServerBible.com 

DECLARE @AuditTime DATETIME
SET @AuditTime = GetDate()

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

 BEGIN TRY 
 INSERT audit.AuditBasic (AuditDate, SystemUserID, EntityName, Operation, SQLStatement, EntityKey1, EntityKey2, ApplicationUserID)
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.IndicatedPage', 'D',@UserSQL, Deleted.PageID, Deleted.Sequence,NULL 
 FROM Deleted 

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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[IndicatedPage_AuditBasic_Delete]', @order = N'last', @stmttype = N'delete';

