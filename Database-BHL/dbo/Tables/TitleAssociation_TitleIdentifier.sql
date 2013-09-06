CREATE TABLE [dbo].[TitleAssociation_TitleIdentifier] (
    [TitleAssociation_TitleIdentifierID] INT           IDENTITY (1, 1) NOT NULL,
    [TitleAssociationID]                 INT           NOT NULL,
    [TitleIdentifierID]                  INT           NOT NULL,
    [IdentifierValue]                    VARCHAR (125) CONSTRAINT [DF_TitleAssociation_TitleIdentifier_IdentifierValue] DEFAULT ('') NOT NULL,
    [CreationDate]                       DATETIME      CONSTRAINT [DF_TitleAssociation_TitleIdentifier_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]                   DATETIME      CONSTRAINT [DF_TitleAssociation_TitleIdentifier_LastModifiedDate] DEFAULT (getdate()) NULL,
    [CreationUserID]                     INT           CONSTRAINT [DF_TitleAssociation_TitleIdentifier_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID]                 INT           CONSTRAINT [DF_TitleAssociation_TitleIdentifier_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_TitleAssociation_TitleIdentifier] PRIMARY KEY CLUSTERED ([TitleAssociation_TitleIdentifierID] ASC),
    CONSTRAINT [FK_TitleAssociation_TitleIdentifier_Identifier] FOREIGN KEY ([TitleIdentifierID]) REFERENCES [dbo].[Identifier] ([IdentifierID]),
    CONSTRAINT [FK_TitleAssociation_TitleIdentifier_TitleAssociation] FOREIGN KEY ([TitleAssociationID]) REFERENCES [dbo].[TitleAssociation] ([TitleAssociationID]) ON DELETE CASCADE
);


GO
CREATE TRIGGER dbo.TitleAssociation_TitleIdentifier_AuditBasic_Insert ON [dbo].[TitleAssociation_TitleIdentifier]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.TitleAssociation_TitleIdentifier', 'I',@UserSQL, Inserted.TitleAssociation_TitleIdentifierID,Inserted.CreationUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[TitleAssociation_TitleIdentifier_AuditBasic_Insert]', @order = N'last', @stmttype = N'insert';


GO
CREATE TRIGGER dbo.TitleAssociation_TitleIdentifier_AuditBasic_Update ON [dbo].[TitleAssociation_TitleIdentifier]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.TitleAssociation_TitleIdentifier', 'U',@UserSQL, Inserted.TitleAssociation_TitleIdentifierID,Inserted.LastModifiedUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[TitleAssociation_TitleIdentifier_AuditBasic_Update]', @order = N'last', @stmttype = N'update';


GO
CREATE TRIGGER dbo.TitleAssociation_TitleIdentifier_AuditBasic_Delete ON [dbo].[TitleAssociation_TitleIdentifier]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.TitleAssociation_TitleIdentifier', 'D',@UserSQL, Deleted.TitleAssociation_TitleIdentifierID,NULL 
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[TitleAssociation_TitleIdentifier_AuditBasic_Delete]', @order = N'last', @stmttype = N'delete';

