CREATE TABLE [dbo].[Collection] (
    [CollectionID]          INT             IDENTITY (1, 1) NOT NULL,
    [CollectionName]        NVARCHAR (50)   CONSTRAINT [DF_Collection_CollectionName] DEFAULT ('') NOT NULL,
    [CollectionDescription] NVARCHAR (4000) CONSTRAINT [DF_Collection_CollectionDescription] DEFAULT ('') NOT NULL,
    [CollectionURL]         NVARCHAR (50)   CONSTRAINT [DF_Collection_CollectionURL] DEFAULT ('') NOT NULL,
    [HtmlContent]           NVARCHAR (MAX)  CONSTRAINT [DF_Collection_HtmlContent] DEFAULT ('') NOT NULL,
    [CanContainTitles]      SMALLINT        CONSTRAINT [DF_Collection_CanContainTitles] DEFAULT ((0)) NOT NULL,
    [CanContainItems]       SMALLINT        CONSTRAINT [DF_Collection_CanContainItems] DEFAULT ((0)) NOT NULL,
    [InstitutionCode]       NVARCHAR (10)   NULL,
    [LanguageCode]          NVARCHAR (10)   NULL,
    [Active]                SMALLINT        CONSTRAINT [DF_Collection_Active] DEFAULT ((1)) NOT NULL,
    [CreationDate]          DATETIME        CONSTRAINT [DF_Collection_CreationDate] DEFAULT (getdate()) NOT NULL,
    [LastModifiedDate]      DATETIME        CONSTRAINT [DF_Collection_LastModifiedDate] DEFAULT (getdate()) NOT NULL,
    [CollectionTarget]      NVARCHAR (30)   CONSTRAINT [DF_Collection_CollectionTargetID] DEFAULT (N'BHL') NOT NULL,
    [ITunesImageURL]        NVARCHAR (100)  CONSTRAINT [DF_Collection_iTunesImageURL] DEFAULT ('') NOT NULL,
    [ITunesURL]             NVARCHAR (100)  CONSTRAINT [DF_Collection_iTunesURL] DEFAULT ('') NOT NULL,
    [ImageURL]              NVARCHAR (100)  CONSTRAINT [DF_Collection_ImageURL] DEFAULT ('') NOT NULL,
    [Featured]              SMALLINT        CONSTRAINT [DF_Collection_Featured] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Collection] PRIMARY KEY CLUSTERED ([CollectionID] ASC),
    CONSTRAINT [FK_Collection_Institution] FOREIGN KEY ([InstitutionCode]) REFERENCES [dbo].[Institution] ([InstitutionCode]),
    CONSTRAINT [FK_Collection_Language] FOREIGN KEY ([LanguageCode]) REFERENCES [dbo].[Language] ([LanguageCode])
);


GO
CREATE TRIGGER dbo.Collection_AuditBasic_Insert ON [dbo].[Collection]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.Collection', 'I',@UserSQL, Inserted.CollectionID,NULL
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[Collection_AuditBasic_Insert]', @order = N'last', @stmttype = N'insert';


GO
CREATE TRIGGER dbo.Collection_AuditBasic_Update ON [dbo].[Collection]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.Collection', 'U',@UserSQL, Inserted.CollectionID,NULL
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[Collection_AuditBasic_Update]', @order = N'last', @stmttype = N'update';

