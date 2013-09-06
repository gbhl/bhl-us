CREATE TABLE [dbo].[Author] (
    [AuthorID]           INT            IDENTITY (1, 1) NOT NULL,
    [AuthorTypeID]       INT            NULL,
    [StartDate]          NVARCHAR (25)  CONSTRAINT [DF_Author_DOB] DEFAULT ('') NOT NULL,
    [EndDate]            NVARCHAR (25)  CONSTRAINT [DF_Author_DOD] DEFAULT ('') NOT NULL,
    [Numeration]         NVARCHAR (300) CONSTRAINT [DF_Author_Numeration] DEFAULT ('') NOT NULL,
    [Title]              NVARCHAR (200) CONSTRAINT [DF_Author_Title] DEFAULT ('') NOT NULL,
    [Unit]               NVARCHAR (300) CONSTRAINT [DF_Author_Unit] DEFAULT ('') NOT NULL,
    [Location]           NVARCHAR (200) CONSTRAINT [DF_Author_Location] DEFAULT ('') NOT NULL,
    [IsActive]           SMALLINT       CONSTRAINT [DF_Author_IsActive] DEFAULT ((1)) NOT NULL,
    [RedirectAuthorID]   INT            NULL,
    [CreationDate]       DATETIME       CONSTRAINT [DF_Author_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]   DATETIME       CONSTRAINT [DF_Author_LastModifiedDate] DEFAULT (getdate()) NULL,
    [CreationUserID]     INT            CONSTRAINT [DF_Author_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT            CONSTRAINT [DF_Author_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED ([AuthorID] ASC),
    CONSTRAINT [FK_Author_Author] FOREIGN KEY ([AuthorID]) REFERENCES [dbo].[Author] ([AuthorID]),
    CONSTRAINT [FK_Author_AuthorType] FOREIGN KEY ([AuthorTypeID]) REFERENCES [dbo].[AuthorType] ([AuthorTypeID])
);


GO
CREATE NONCLUSTERED INDEX [IA_Author_IsActive]
    ON [dbo].[Author]([IsActive] ASC)
    INCLUDE([AuthorID]);


GO
CREATE TRIGGER dbo.Author_AuditBasic_Insert ON [dbo].[Author]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.Author', 'I',@UserSQL, Inserted.AuthorID,Inserted.CreationUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[Author_AuditBasic_Insert]', @order = N'last', @stmttype = N'insert';


GO
CREATE TRIGGER dbo.Author_AuditBasic_Update ON [dbo].[Author]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.Author', 'U',@UserSQL, Inserted.AuthorID,Inserted.LastModifiedUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[Author_AuditBasic_Update]', @order = N'last', @stmttype = N'update';

