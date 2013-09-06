CREATE TABLE [dbo].[TitleAssociation] (
    [TitleAssociationID]     INT            IDENTITY (1, 1) NOT NULL,
    [TitleID]                INT            NOT NULL,
    [TitleAssociationTypeID] INT            NOT NULL,
    [Title]                  NVARCHAR (500) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF_TitleAssociation_Title] DEFAULT ('') NOT NULL,
    [Section]                NVARCHAR (500) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF_TitleAssociation_Section] DEFAULT ('') NOT NULL,
    [Volume]                 NVARCHAR (500) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF_TitleAssociation_Volume] DEFAULT ('') NOT NULL,
    [Active]                 BIT            CONSTRAINT [DF_TitleAssociation_Active] DEFAULT ((1)) NOT NULL,
    [AssociatedTitleID]      INT            NULL,
    [CreationDate]           DATETIME       CONSTRAINT [DF_TitleAssociation_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]       DATETIME       CONSTRAINT [DF_TitleAssociation_LastModifiedDate] DEFAULT (getdate()) NULL,
    [Heading]                NVARCHAR (500) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF_TitleAssociation_Heading] DEFAULT ('') NOT NULL,
    [Publication]            NVARCHAR (500) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF_TitleAssociation_PublicationInfo] DEFAULT ('') NOT NULL,
    [Relationship]           NVARCHAR (500) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF_TitleAssociation_Relationship] DEFAULT ('') NOT NULL,
    [CreationUserID]         INT            CONSTRAINT [DF_TitleAssociation_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID]     INT            CONSTRAINT [DF_TitleAssociation_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_TitleAssociation] PRIMARY KEY CLUSTERED ([TitleAssociationID] ASC),
    CONSTRAINT [FK_TitleAssociation_Title] FOREIGN KEY ([TitleID]) REFERENCES [dbo].[Title] ([TitleID]),
    CONSTRAINT [FK_TitleAssociation_Title1] FOREIGN KEY ([AssociatedTitleID]) REFERENCES [dbo].[Title] ([TitleID]),
    CONSTRAINT [FK_TitleAssociation_TitleAssociationType] FOREIGN KEY ([TitleAssociationTypeID]) REFERENCES [dbo].[TitleAssociationType] ([TitleAssociationTypeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_TitleAssociation]
    ON [dbo].[TitleAssociation]([TitleID] ASC, [Active] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TitleAssociation_Title]
    ON [dbo].[TitleAssociation]([Title] ASC, [Active] ASC)
    INCLUDE([TitleID]);


GO
CREATE TRIGGER dbo.TitleAssociation_AuditBasic_Update ON [dbo].[TitleAssociation]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.TitleAssociation', 'U',@UserSQL, Inserted.TitleAssociationID,Inserted.LastModifiedUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[TitleAssociation_AuditBasic_Update]', @order = N'last', @stmttype = N'update';


GO
CREATE TRIGGER dbo.TitleAssociation_AuditBasic_Delete ON [dbo].[TitleAssociation]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.TitleAssociation', 'D',@UserSQL, Deleted.TitleAssociationID,NULL 
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[TitleAssociation_AuditBasic_Delete]', @order = N'last', @stmttype = N'delete';


GO
CREATE TRIGGER dbo.TitleAssociation_AuditBasic_Insert ON [dbo].[TitleAssociation]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.TitleAssociation', 'I',@UserSQL, Inserted.TitleAssociationID,Inserted.CreationUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[TitleAssociation_AuditBasic_Insert]', @order = N'last', @stmttype = N'insert';

