CREATE TABLE [dbo].[TitleVariant] (
    [TitleVariantID]     INT            IDENTITY (1, 1) NOT NULL,
    [TitleID]            INT            NOT NULL,
    [TitleVariantTypeID] INT            NOT NULL,
    [Title]              NVARCHAR (MAX) CONSTRAINT [DF_TitleVariant_Title] DEFAULT ('') NOT NULL,
    [TitleRemainder]     NVARCHAR (MAX) CONSTRAINT [DF_TitleVariant_TitleRemainder] DEFAULT ('') NOT NULL,
    [PartNumber]         NVARCHAR (255) CONSTRAINT [DF_TitleVariant_PartNumber] DEFAULT ('') NOT NULL,
    [PartName]           NVARCHAR (255) CONSTRAINT [DF_TitleVariant_PartName] DEFAULT ('') NOT NULL,
    [CreationDate]       DATETIME       CONSTRAINT [DF_TitleVariant_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME       CONSTRAINT [DF_TitleVariant_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT            NULL,
    [LastModifiedUserID] INT            NULL,
    CONSTRAINT [PK_TitleVariant] PRIMARY KEY CLUSTERED ([TitleVariantID] ASC),
    CONSTRAINT [FK_TitleVariant_Title] FOREIGN KEY ([TitleID]) REFERENCES [dbo].[Title] ([TitleID]),
    CONSTRAINT [FK_TitleVariant_TitleVariantType] FOREIGN KEY ([TitleVariantTypeID]) REFERENCES [dbo].[TitleVariantType] ([TitleVariantTypeID])
);


GO
CREATE TRIGGER dbo.TitleVariant_AuditBasic_Insert ON [dbo].[TitleVariant]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.TitleVariant', 'I',@UserSQL, Inserted.TitleVariantID,Inserted.CreationUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[TitleVariant_AuditBasic_Insert]', @order = N'last', @stmttype = N'insert';


GO
CREATE TRIGGER dbo.TitleVariant_AuditBasic_Update ON [dbo].[TitleVariant]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.TitleVariant', 'U',@UserSQL, Inserted.TitleVariantID,Inserted.LastModifiedUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[TitleVariant_AuditBasic_Update]', @order = N'last', @stmttype = N'update';


GO
CREATE TRIGGER dbo.TitleVariant_AuditBasic_Delete ON [dbo].[TitleVariant]
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
 INSERT audit.AuditBasic (AuditDate, SystemUserID, EntityName, Operation, SQLStatement, EntityKey1, ApplicationUserID)
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.TitleVariant', 'D',@UserSQL, Deleted.TitleVariantID,NULL 
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[TitleVariant_AuditBasic_Delete]', @order = N'last', @stmttype = N'delete';

