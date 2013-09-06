CREATE TABLE [dbo].[TitleLanguage] (
    [TitleLanguageID] INT           IDENTITY (1, 1) NOT NULL,
    [TitleID]         INT           NOT NULL,
    [LanguageCode]    NVARCHAR (10) NOT NULL,
    [CreationDate]    DATETIME      CONSTRAINT [DF__TitleLang__Creat__3A0D7D32] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]  INT           CONSTRAINT [DF_TitleLanguage_CreationUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_TitleLanguage] PRIMARY KEY CLUSTERED ([TitleLanguageID] ASC),
    CONSTRAINT [FK_TitleLanguage_Language] FOREIGN KEY ([LanguageCode]) REFERENCES [dbo].[Language] ([LanguageCode]),
    CONSTRAINT [FK_TitleLanguage_Title] FOREIGN KEY ([TitleID]) REFERENCES [dbo].[Title] ([TitleID])
);


GO
CREATE NONCLUSTERED INDEX [IX_TitleLanguage_LanguageCode]
    ON [dbo].[TitleLanguage]([LanguageCode] ASC)
    INCLUDE([TitleID]);


GO
CREATE TRIGGER dbo.TitleLanguage_AuditBasic_Insert ON [dbo].[TitleLanguage]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.TitleLanguage', 'I',@UserSQL, Inserted.TitleLanguageID,Inserted.CreationUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[TitleLanguage_AuditBasic_Insert]', @order = N'last', @stmttype = N'insert';


GO
CREATE TRIGGER dbo.TitleLanguage_AuditBasic_Update ON [dbo].[TitleLanguage]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.TitleLanguage', 'U',@UserSQL, Inserted.TitleLanguageID,NULL
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[TitleLanguage_AuditBasic_Update]', @order = N'last', @stmttype = N'update';


GO
CREATE TRIGGER dbo.TitleLanguage_AuditBasic_Delete ON [dbo].[TitleLanguage]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.TitleLanguage', 'D',@UserSQL, Deleted.TitleLanguageID,NULL 
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[TitleLanguage_AuditBasic_Delete]', @order = N'last', @stmttype = N'delete';

