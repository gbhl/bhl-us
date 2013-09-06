CREATE TABLE [dbo].[TitleAuthor] (
    [TitleAuthorID]      INT      IDENTITY (1, 1) NOT NULL,
    [TitleID]            INT      NOT NULL,
    [AuthorID]           INT      NOT NULL,
    [AuthorRoleID]       INT      NULL,
    [CreationDate]       DATETIME CONSTRAINT [DF_TitleAuthor_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]   DATETIME CONSTRAINT [DF_TitleAuthor_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CreationUserID]     INT      CONSTRAINT [DF_TitleAuthor_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT      CONSTRAINT [DF_TitleAuthor_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_TitleAuthor] PRIMARY KEY CLUSTERED ([TitleAuthorID] ASC),
    CONSTRAINT [FK_TitleAuthor_Author] FOREIGN KEY ([AuthorID]) REFERENCES [dbo].[Author] ([AuthorID]),
    CONSTRAINT [FK_TitleAuthor_AuthorRole] FOREIGN KEY ([AuthorRoleID]) REFERENCES [dbo].[AuthorRole] ([AuthorRoleID]),
    CONSTRAINT [FK_TitleAuthor_Title] FOREIGN KEY ([TitleID]) REFERENCES [dbo].[Title] ([TitleID])
);


GO
CREATE NONCLUSTERED INDEX [IX_TitleAuthor_AuthorID]
    ON [dbo].[TitleAuthor]([AuthorID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TitleAuthor_TitleID]
    ON [dbo].[TitleAuthor]([TitleID] ASC);


GO
CREATE TRIGGER dbo.TitleAuthor_AuditBasic_Insert ON [dbo].[TitleAuthor]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.TitleAuthor', 'I',@UserSQL, Inserted.TitleAuthorID,Inserted.CreationUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[TitleAuthor_AuditBasic_Insert]', @order = N'last', @stmttype = N'insert';


GO
CREATE TRIGGER dbo.TitleAuthor_AuditBasic_Update ON [dbo].[TitleAuthor]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.TitleAuthor', 'U',@UserSQL, Inserted.TitleAuthorID,Inserted.LastModifiedUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[TitleAuthor_AuditBasic_Update]', @order = N'last', @stmttype = N'update';


GO
CREATE TRIGGER dbo.TitleAuthor_AuditBasic_Delete ON [dbo].[TitleAuthor]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.TitleAuthor', 'D',@UserSQL, Deleted.TitleAuthorID,NULL 
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[TitleAuthor_AuditBasic_Delete]', @order = N'last', @stmttype = N'delete';

