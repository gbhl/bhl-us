CREATE TABLE [dbo].[Page] (
    [PageID]                 INT            IDENTITY (1, 1) NOT NULL,
    [ItemID]                 INT            NOT NULL,
    [FileNamePrefix]         NVARCHAR (50)  NOT NULL,
    [SequenceOrder]          INT            NULL,
    [PageDescription]        NVARCHAR (255) NULL,
    [Illustration]           BIT            CONSTRAINT [DF_Page_Illustration] DEFAULT ((0)) NOT NULL,
    [Note]                   NVARCHAR (255) NULL,
    [FileSize_Temp]          INT            NULL,
    [FileExtension]          NVARCHAR (5)   NULL,
    [CreationDate]           DATETIME       CONSTRAINT [DF__Page__Created__2610A626] DEFAULT (getdate()) NULL,
    [LastModifiedDate]       DATETIME       CONSTRAINT [DF__Page__Changed__2704CA5F] DEFAULT (getdate()) NULL,
    [CreationUserID]         INT            CONSTRAINT [DF_Page_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID]     INT            CONSTRAINT [DF_Page_LastModifiedUserID] DEFAULT ((1)) NULL,
    [Active]                 BIT            CONSTRAINT [DF_Item_Active] DEFAULT ((1)) NOT NULL,
    [Year]                   NVARCHAR (20)  NULL,
    [Series]                 NVARCHAR (20)  NULL,
    [Volume]                 NVARCHAR (20)  NULL,
    [Issue]                  NVARCHAR (20)  NULL,
    [ExternalURL]            NVARCHAR (500) NULL,
    [IssuePrefix]            NVARCHAR (20)  NULL,
    [LastPageNameLookupDate] DATETIME       NULL,
    [PaginationUserID]       INT            NULL,
    [PaginationDate]         DATETIME       NULL,
    [AltExternalURL]         NVARCHAR (500) NULL,
    CONSTRAINT [aaaaaPage_PK] PRIMARY KEY CLUSTERED ([PageID] ASC),
    CONSTRAINT [Page_FK00] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Item] ([ItemID]) ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [ItemPage]
    ON [dbo].[Page]([ItemID] ASC);


GO
CREATE NONCLUSTERED INDEX [Sequence]
    ON [dbo].[Page]([FileNamePrefix] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Page_Active]
    ON [dbo].[Page]([Active] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Page_ActiveItemID]
    ON [dbo].[Page]([Active] ASC, [ItemID] ASC)
    INCLUDE([PageID], [CreationDate]);


GO
CREATE NONCLUSTERED INDEX [IX_Page_LastPageNameLookupDate]
    ON [dbo].[Page]([LastPageNameLookupDate] ASC)
    INCLUDE([PageID], [ItemID], [FileNamePrefix], [SequenceOrder]);


GO
CREATE NONCLUSTERED INDEX [IX_Page_ItemIDActiveSeqOrder]
    ON [dbo].[Page]([ItemID] ASC, [Active] ASC, [SequenceOrder] ASC)
    INCLUDE([PageID], [FileNamePrefix], [Illustration], [Year], [Series], [Volume], [Issue], [ExternalURL], [IssuePrefix]);


GO
CREATE NONCLUSTERED INDEX [IX_Page_PageIDActiveSequence]
    ON [dbo].[Page]([PageID] ASC, [Active] ASC, [SequenceOrder] ASC)
    INCLUDE([ItemID], [Year]);


GO
CREATE NONCLUSTERED INDEX [IX_Page_PageIDActiveItem]
    ON [dbo].[Page]([PageID] ASC, [Active] ASC, [ItemID] ASC);


GO
CREATE TRIGGER dbo.Page_AuditBasic_Insert ON [dbo].[Page]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.Page', 'I',@UserSQL, Inserted.PageID,Inserted.CreationUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[Page_AuditBasic_Insert]', @order = N'last', @stmttype = N'insert';


GO
CREATE TRIGGER [dbo].[Page_AuditBasic_Update] ON [dbo].[Page]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.Page', 'U',@UserSQL, Inserted.PageID,Inserted.LastModifiedUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[Page_AuditBasic_Update]', @order = N'last', @stmttype = N'update';

