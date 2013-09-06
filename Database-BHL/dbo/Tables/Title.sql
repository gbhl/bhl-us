CREATE TABLE [dbo].[Title] (
    [TitleID]                     INT             IDENTITY (1, 1) NOT NULL,
    [MARCBibID]                   NVARCHAR (50)   NOT NULL,
    [MARCLeader]                  NVARCHAR (24)   NULL,
    [TropicosTitleID]             INT             NULL,
    [RedirectTitleID]             INT             NULL,
    [FullTitle]                   NVARCHAR (2000) COLLATE SQL_Latin1_General_CP1_CI_AI CONSTRAINT [DF_Title_FullTitle] DEFAULT ('') NOT NULL,
    [ShortTitle]                  NVARCHAR (255)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [UniformTitle]                NVARCHAR (255)  NULL,
    [SortTitle]                   NVARCHAR (60)   COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [CallNumber]                  NVARCHAR (100)  NULL,
    [PublicationDetails]          NVARCHAR (255)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [StartYear]                   SMALLINT        NULL,
    [EndYear]                     SMALLINT        NULL,
    [Datafield_260_a]             NVARCHAR (150)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [Datafield_260_b]             NVARCHAR (255)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [Datafield_260_c]             NVARCHAR (100)  NULL,
    [InstitutionCode]             NVARCHAR (10)   CONSTRAINT [DF__Title__Instituti__7167D3BD] DEFAULT ('MO') NULL,
    [LanguageCode]                NVARCHAR (10)   NULL,
    [TitleDescription]            NTEXT           NULL,
    [TL2Author]                   NVARCHAR (100)  NULL,
    [PublishReady]                BIT             CONSTRAINT [DF__Title__PublishRe__725BF7F6] DEFAULT ((0)) NOT NULL,
    [RareBooks]                   BIT             CONSTRAINT [DF_Title_RareBooks] DEFAULT ((0)) NOT NULL,
    [Note]                        NVARCHAR (255)  NULL,
    [CreationDate]                DATETIME        CONSTRAINT [DF__Title__Created__74444068] DEFAULT (getdate()) NULL,
    [LastModifiedDate]            DATETIME        CONSTRAINT [DF__Title__Changed__753864A1] DEFAULT (getdate()) NULL,
    [CreationUserID]              INT             CONSTRAINT [DF_Title_CreationUserID] DEFAULT ((1)) NULL,
    [LastModifiedUserID]          INT             CONSTRAINT [DF_Title_LastModifiedUserID] DEFAULT ((1)) NULL,
    [OriginalCatalogingSource]    NVARCHAR (100)  NULL,
    [EditionStatement]            NVARCHAR (450)  NULL,
    [CurrentPublicationFrequency] NVARCHAR (100)  NULL,
    [PartNumber]                  NVARCHAR (255)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [PartName]                    NVARCHAR (255)  COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
    [BibliographicLevelID]        INT             NULL,
    CONSTRAINT [aaaaaTitle_PK] PRIMARY KEY NONCLUSTERED ([TitleID] ASC),
    CONSTRAINT [CK Title EndYear] CHECK ([EndYear]>=(1400) AND [EndYear]<=(2025) OR [EndYear] IS NULL),
    CONSTRAINT [CK Title StartYear] CHECK ([StartYear]>=(1400) AND [StartYear]<=(2025) OR [StartYear] IS NULL),
    CONSTRAINT [FK_Title_BibliographicLevel] FOREIGN KEY ([BibliographicLevelID]) REFERENCES [dbo].[BibliographicLevel] ([BibliographicLevelID]),
    CONSTRAINT [FK_Title_Title] FOREIGN KEY ([RedirectTitleID]) REFERENCES [dbo].[Title] ([TitleID]),
    CONSTRAINT [Title_FK00] FOREIGN KEY ([InstitutionCode]) REFERENCES [dbo].[Institution] ([InstitutionCode]) ON UPDATE CASCADE,
    CONSTRAINT [Title_FK01] FOREIGN KEY ([LanguageCode]) REFERENCES [dbo].[Language] ([LanguageCode]) ON UPDATE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Title_BibIDShortTitle]
    ON [dbo].[Title]([MARCBibID] ASC, [ShortTitle] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Title_TitleIDSortTitle]
    ON [dbo].[Title]([TitleID] ASC, [SortTitle] ASC)
    INCLUDE([ShortTitle], [MARCBibID]);


GO
CREATE NONCLUSTERED INDEX [IX_Title_PublishReadySortTitle]
    ON [dbo].[Title]([PublishReady] ASC, [SortTitle] ASC)
    INCLUDE([TitleID], [FullTitle], [ShortTitle], [PublicationDetails], [InstitutionCode], [LanguageCode]);


GO
CREATE NONCLUSTERED INDEX [IX_Title_PublishReadyStartYear]
    ON [dbo].[Title]([PublishReady] ASC, [StartYear] ASC)
    INCLUDE([PublicationDetails], [TitleID], [FullTitle], [LanguageCode], [InstitutionCode]);


GO
CREATE NONCLUSTERED INDEX [IX_Title_TitleIDCovering]
    ON [dbo].[Title]([TitleID] ASC)
    INCLUDE([FullTitle], [ShortTitle], [SortTitle], [BibliographicLevelID], [CallNumber], [StartYear], [EndYear], [PublicationDetails], [Datafield_260_a], [Datafield_260_b], [Datafield_260_c], [PublishReady], [EditionStatement], [PartNumber], [PartName]);


GO
CREATE TRIGGER dbo.Title_AuditBasic_Insert ON [dbo].[Title]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.Title', 'I',@UserSQL, Inserted.TitleID,Inserted.CreationUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[Title_AuditBasic_Insert]', @order = N'last', @stmttype = N'insert';


GO
CREATE TRIGGER dbo.Title_AuditBasic_Update ON [dbo].[Title]
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
 SELECT @AuditTime, SUSER_SNAME(), 'dbo.Title', 'U',@UserSQL, Inserted.TitleID,Inserted.LastModifiedUserID
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
EXECUTE sp_settriggerorder @triggername = N'[dbo].[Title_AuditBasic_Update]', @order = N'last', @stmttype = N'update';


GO
EXECUTE sp_addextendedproperty @name = N'MS_ConstraintText', @value = N'Value must be from 1400 to 2025, inclusive, or empty.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Title', @level2type = N'CONSTRAINT', @level2name = N'CK Title EndYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_ConstraintText', @value = N'Value must be from 1400 to 2025, inclusive, or empty.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Title', @level2type = N'CONSTRAINT', @level2name = N'CK Title StartYear';

