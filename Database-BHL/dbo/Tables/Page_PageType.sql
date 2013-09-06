CREATE TABLE [dbo].[Page_PageType] (
    [PageID]             INT      NOT NULL,
    [PageTypeID]         INT      NOT NULL,
    [Verified]           BIT      CONSTRAINT [DF_Page_PageType_Verified] DEFAULT ((0)) NOT NULL,
    [CreationDate]       DATETIME CONSTRAINT [DF_Page_PageType_CreationDate] DEFAULT (getdate()) NULL,
    [LastModifiedDate]   DATETIME CONSTRAINT [DF_Page_PageType_LastModifiedDate] DEFAULT (getdate()) NULL,
    [CreationUserID]     INT      CONSTRAINT [DF_Page_PageType_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID] INT      CONSTRAINT [DF_Page_PageType_LastModifiedUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_Page_PageType] PRIMARY KEY CLUSTERED ([PageID] ASC, [PageTypeID] ASC),
    CONSTRAINT [Page_PageType_FK00] FOREIGN KEY ([PageID]) REFERENCES [dbo].[Page] ([PageID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [Page_PageType_FK01] FOREIGN KEY ([PageTypeID]) REFERENCES [dbo].[PageType] ([PageTypeID]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE TRIGGER dbo.Page_PageType_AuditBasic_Insert ON [dbo].[Page_PageType]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.Page_PageType', 'I',@UserSQL, Inserted.PageID, Inserted.PageTypeID,Inserted.CreationUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[Page_PageType_AuditBasic_Insert]', @order = N'last', @stmttype = N'insert';


GO
CREATE TRIGGER dbo.Page_PageType_AuditBasic_Update ON [dbo].[Page_PageType]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.Page_PageType', 'U',@UserSQL, Inserted.PageID, Inserted.PageTypeID,Inserted.LastModifiedUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[Page_PageType_AuditBasic_Update]', @order = N'last', @stmttype = N'update';


GO
CREATE TRIGGER dbo.Page_PageType_AuditBasic_Delete ON [dbo].[Page_PageType]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.Page_PageType', 'D',@UserSQL, Deleted.PageID, Deleted.PageTypeID,NULL 
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[Page_PageType_AuditBasic_Delete]', @order = N'last', @stmttype = N'delete';


GO
EXECUTE sp_addextendedproperty @name = N'AllowZeroLength', @value = N'False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'Attributes', @value = N'1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'CollatingOrder', @value = N'1033', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'ColumnHidden', @value = N'False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'ColumnOrder', @value = N'0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'ColumnWidth', @value = N'-1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'DataUpdatable', @value = N'False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'GUID', @value = N'験ム䰗䐨⒩犑�嶖', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Caption', @value = N'Page ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DecimalPlaces', @value = N'0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for each Page record.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DisplayControl', @value = N'109', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'Name', @value = N'PageID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'OrdinalPosition', @value = N'1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'Required', @value = N'True', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'Size', @value = N'4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'SourceField', @value = N'PageID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'SourceTable', @value = N'Page_PageType_local', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'Type', @value = N'4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'AllowZeroLength', @value = N'False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'Attributes', @value = N'1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'CollatingOrder', @value = N'1033', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'ColumnHidden', @value = N'False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'ColumnOrder', @value = N'0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'ColumnWidth', @value = N'-1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'DataUpdatable', @value = N'False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'GUID', @value = N'茶갑螵䱝㪲ퟲ솥Ꞥ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Caption', @value = N'Page Type ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DecimalPlaces', @value = N'0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for each Page Type record.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DisplayControl', @value = N'109', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'Name', @value = N'PageTypeID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'OrdinalPosition', @value = N'2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'Required', @value = N'True', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'Size', @value = N'4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'SourceField', @value = N'PageTypeID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'SourceTable', @value = N'Page_PageType_local', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'Type', @value = N'4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Page_PageType', @level2type = N'COLUMN', @level2name = N'PageTypeID';

