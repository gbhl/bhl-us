CREATE TABLE [dbo].[Item] (
    [ItemID]                    INT            IDENTITY (1, 1) NOT NULL,
    [PrimaryTitleID]            INT            NOT NULL,
    [BarCode]                   NVARCHAR (40)  NOT NULL,
    [MARCItemID]                NVARCHAR (50)  NULL,
    [CallNumber]                NVARCHAR (100) NULL,
    [Volume]                    NVARCHAR (100) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [InstitutionCode]           NVARCHAR (10)  CONSTRAINT [DF__Item__Institutio__68D28DBC] DEFAULT ('MO') NULL,
    [LanguageCode]              NVARCHAR (10)  NULL,
    [ItemDescription]           NTEXT          NULL,
    [ScannedBy]                 INT            NULL,
    [PDFSize]                   INT            NULL,
    [VaultID]                   INT            NULL,
    [NumberOfFiles]             SMALLINT       NULL,
    [Note]                      NVARCHAR (255) NULL,
    [CreationDate]              DATETIME       CONSTRAINT [DF__Item__Created__6BAEFA67] DEFAULT (getdate()) NULL,
    [LastModifiedDate]          DATETIME       CONSTRAINT [DF__Item__Changed__6CA31EA0] DEFAULT (getdate()) NULL,
    [CreationUserID]            INT            CONSTRAINT [DF_Item_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID]        INT            CONSTRAINT [DF_Item_LastModifiedUserID] DEFAULT ((1)) NULL,
    [ItemStatusID]              INT            CONSTRAINT [DF_Item_ItemStatusID] DEFAULT ((10)) NOT NULL,
    [ScanningUser]              NVARCHAR (100) NULL,
    [ScanningDate]              DATETIME       NULL,
    [PaginationCompleteUserID]  INT            NULL,
    [PaginationCompleteDate]    DATETIME       NULL,
    [PaginationStatusID]        INT            NULL,
    [PaginationStatusUserID]    INT            NULL,
    [PaginationStatusDate]      DATETIME       NULL,
    [LastPageNameLookupDate]    DATETIME       NULL,
    [ItemSourceID]              INT            NULL,
    [Year]                      NVARCHAR (20)  NULL,
    [IdentifierBib]             NVARCHAR (50)  NULL,
    [FileRootFolder]            NVARCHAR (250) CONSTRAINT [DF_Item_FileRootFolder] DEFAULT ('') NULL,
    [ZQuery]                    NVARCHAR (200) NULL,
    [Sponsor]                   NVARCHAR (100) NULL,
    [LicenseUrl]                NVARCHAR (MAX) CONSTRAINT [DF_Item_LicenseUrl] DEFAULT ('') NULL,
    [Rights]                    NVARCHAR (MAX) CONSTRAINT [DF_Item_Rights] DEFAULT ('') NULL,
    [DueDiligence]              NVARCHAR (MAX) CONSTRAINT [DF_Item_DueDiligence] DEFAULT ('') NULL,
    [CopyrightStatus]           NVARCHAR (MAX) CONSTRAINT [DF_Item_CopyrightStatus] DEFAULT ('') NULL,
    [CopyrightRegion]           NVARCHAR (50)  CONSTRAINT [DF_Item_CopyrightRegion] DEFAULT ('') NULL,
    [CopyrightComment]          NVARCHAR (MAX) CONSTRAINT [DF_Item_CopyrightComment] DEFAULT ('') NULL,
    [CopyrightEvidence]         NVARCHAR (MAX) CONSTRAINT [DF_Item_CopyrightEvidence] DEFAULT ('') NULL,
    [CopyrightEvidenceOperator] NVARCHAR (100) CONSTRAINT [DF_Item_CopyrightEvidenceOperator] DEFAULT ('') NULL,
    [CopyrightEvidenceDate]     NVARCHAR (30)  CONSTRAINT [DF_Item_CopyrightEvidenceDate] DEFAULT ('') NULL,
    [ThumbnailPageID]           INT            NULL,
    [RedirectItemID]            INT            NULL,
    [ExternalUrl]               NVARCHAR (500) CONSTRAINT [DF_Item_ExternalUrl] DEFAULT ('') NULL,
	[EndYear]					NVARCHAR(20)   CONSTRAINT [DF_Item_EndYear]  DEFAULT ('') NOT NULL,
	[StartVolume]				NVARCHAR(10)   CONSTRAINT [DF_Item_StartVolume]  DEFAULT ('') NOT NULL,
	[EndVolume]					NVARCHAR(10)   CONSTRAINT [DF_Item_EndVolume]  DEFAULT ('') NOT NULL,
	[StartIssue]				NVARCHAR(10)   CONSTRAINT [DF_Item_StartIssue]  DEFAULT ('') NOT NULL,
	[EndIssue]					NVARCHAR(10)   CONSTRAINT [DF_Item_EndIssue]  DEFAULT ('') NOT NULL,
	[StartNumber]				NVARCHAR(10)   CONSTRAINT [DF_Item_StartNumber]  DEFAULT ('') NOT NULL,
	[EndNumber]					NVARCHAR(10)   CONSTRAINT [DF_Item_EndNumber]  DEFAULT ('') NOT NULL,
	[StartSeries]				NVARCHAR(10)   CONSTRAINT [DF_Item_StartSeries]  DEFAULT ('') NOT NULL,
	[EndSeries]					NVARCHAR(10)   CONSTRAINT [DF_Item_EndSeries]  DEFAULT ('') NOT NULL,
	[StartPart]					NVARCHAR(10)   CONSTRAINT [DF_Item_StartPart]  DEFAULT ('') NOT NULL,
	[EndPart]					NVARCHAR(10)   CONSTRAINT [DF_Item_EndPart]  DEFAULT ('') NOT NULL,
	[VolumeReviewed]			TINYINT        CONSTRAINT [DF_Item_VolumeReviewed]  DEFAULT ((0)) NOT NULL,
	[VolumeReviewedDate]		DATETIME       NULL,
	[VolumeReviewedUserID]		INT            NULL,
    CONSTRAINT [aaaaaItem_PK] PRIMARY KEY CLUSTERED ([ItemID] ASC),
    CONSTRAINT [FK_Item_Institution] FOREIGN KEY ([InstitutionCode]) REFERENCES [dbo].[Institution] ([InstitutionCode]),
    CONSTRAINT [FK_Item_Item] FOREIGN KEY ([RedirectItemID]) REFERENCES [dbo].[Item] ([ItemID]),
    CONSTRAINT [FK_Item_ItemSource] FOREIGN KEY ([ItemSourceID]) REFERENCES [dbo].[ItemSource] ([ItemSourceID]),
    CONSTRAINT [FK_Item_ItemStatus] FOREIGN KEY ([ItemStatusID]) REFERENCES [dbo].[ItemStatus] ([ItemStatusID]),
    CONSTRAINT [FK_Item_Language] FOREIGN KEY ([LanguageCode]) REFERENCES [dbo].[Language] ([LanguageCode]),
    CONSTRAINT [FK_Item_PaginationStatus] FOREIGN KEY ([PaginationStatusID]) REFERENCES [dbo].[PaginationStatus] ([PaginationStatusID]),
    CONSTRAINT [FK_Item_Title] FOREIGN KEY ([PrimaryTitleID]) REFERENCES [dbo].[Title] ([TitleID]),
    CONSTRAINT [FK_Item_Vault] FOREIGN KEY ([VaultID]) REFERENCES [dbo].[Vault] ([VaultID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [BarCode]
    ON [dbo].[Item]([BarCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Item_TitleID]
    ON [dbo].[Item]([PrimaryTitleID] ASC)
    INCLUDE([InstitutionCode], [LanguageCode]);


GO
CREATE NONCLUSTERED INDEX [IX_Item_StatusItemVolume]
    ON [dbo].[Item]([ItemStatusID] ASC, [ItemID] ASC, [Volume] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Item_StatusItemTitle]
    ON [dbo].[Item]([ItemStatusID] ASC, [ItemID] ASC, [PrimaryTitleID] ASC)
    INCLUDE([Volume], [Year], [LanguageCode], [CallNumber]);


GO
CREATE NONCLUSTERED INDEX [IX_Item_ItemStatusID]
    ON [dbo].[Item]([ItemStatusID] ASC);


GO
CREATE TRIGGER dbo.Item_AuditBasic_Insert ON [dbo].[Item]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.Item', 'I',@UserSQL, Inserted.ItemID,Inserted.CreationUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[Item_AuditBasic_Insert]', @order = N'last', @stmttype = N'insert';


GO
CREATE TRIGGER dbo.Item_AuditBasic_Update ON [dbo].[Item]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.Item', 'U',@UserSQL, Inserted.ItemID,Inserted.LastModifiedUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[Item_AuditBasic_Update]', @order = N'last', @stmttype = N'update';

