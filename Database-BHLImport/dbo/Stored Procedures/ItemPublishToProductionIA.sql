﻿CREATE PROCEDURE [dbo].[ItemPublishToProductionIA]

@BarCode nvarchar(40) = NULL

AS

BEGIN

------------------------------------------------------------------------------
--
--	NOTES: This script reads from the Import tables (Title, Keyword, TitleKeyword,
--	Creator, Title_Creator, Title_Identifier, TitleNote, Item, Page, IndicatedPage, 
--	Page_PageType) and performs INSERTs and limited UPDATEs on the production
--  (biodiversity.org) database.
--
--	The first time that data from a source is put into the Import tables (a
--	particular title, for example), it will be inserted into the production 
--	database.  If another row for that data (that particular title) is later
--	put into the Import tables, that additional row will be used to update
--	selected data in the production database.
--
--	The @BarCode parameter limits this procedure to processing only data
--	related to the item identified by the specified barcode.
--
------------------------------------------------------------------------------
SET NOCOUNT ON

DECLARE @ImportKey nvarchar(50)
DECLARE @ImportSourceID int
DECLARE @ProductionDate DATETIME
DECLARE @TitleInsert int
DECLARE @TitleUpdate int
DECLARE @CreatorInsert int
DECLARE @CreatorUpdate int
DECLARE @TitleCreatorInsert int
DECLARE @TitleCreatorUpdate int
DECLARE @TitleKeywordInsert int
DECLARE @TitleKeywordUpdate int
DECLARE @TitleIdentifierInsert int
DECLARE @TitleIdentifierUpdate int
DECLARE @TitleAssociationInsert int
DECLARE @TitleAssociationTitleIdentifierInsert int
DECLARE @TitleVariantInsert int
DECLARE @ItemInsert int
DECLARE @ItemUpdate int
DECLARE @TitleItemInsert int
DECLARE @PageInsert int
DECLARE @PageUpdate int
DECLARE @IndicatedPageInsert int
DECLARE @IndicatedPageUpdate int
DECLARE @PagePageTypeInsert int
DECLARE @PagePageTypeUpdate int
DECLARE @PageNameInsert int
DECLARE @PageNameUpdate int
SET @ImportSourceID = 1
SET @ProductionDate = GETDATE()

BEGIN TRY

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Create temp tables

	CREATE TABLE #tmpTitle (
		[TitleID] [int] NOT NULL,
		[MARCBibID] [nvarchar](50) NOT NULL,
		[MARCLeader] [nvarchar](24) NULL,
		[FullTitle] [ntext]	COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[ShortTitle] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[UniformTitle] [nvarchar](255) NULL,
		[SortTitle] [nvarchar](60) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[PartNumber] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[PartName] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[CallNumber] [nvarchar](100) NULL,
		[PublicationDetails] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[StartYear] [smallint] NULL,
		[EndYear] [smallint] NULL,
		[Datafield_260_a] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[Datafield_260_b] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[Datafield_260_c] [nvarchar](100) NULL,
		[InstitutionCode] [nvarchar](10) NULL,
		[LanguageCode] [nvarchar](10) NULL,
		[TitleDescription] [ntext] NULL,
		[TL2Author] [nvarchar](100) NULL,
		[PublishReady] [bit] NULL DEFAULT (1),
		[RareBooks] [bit] NULL DEFAULT(0),
		[OriginalCatalogingSource] [nvarchar](100) NULL,
		[EditionStatement] [nvarchar](450) NULL,
		[CurrentPublicationFrequency] [nvarchar](100) NULL,
		[Note] [nvarchar](255) NULL,
		[ExternalCreationDate] [datetime] NULL,
		[ExternalLastModifiedDate] [datetime] NULL,
		[ExternalCreationUser] [int] NULL,
		[ExternalLastModifiedUser] [int] NULL,
		[ImportKey] [nvarchar](50) NULL,
		[ProductionTitleID] [int] NULL
		)

	CREATE TABLE #tmpKeyword (
		[KeywordID] [int] NOT NULL,
		[Keyword] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[MarcDataFieldTag] [nvarchar](50) NULL,
		[MarcSubFieldCode] [nvarchar](50) NULL,
		[ExternalCreationDate] [datetime] NULL,
		[ExternalLastModifiedDate] [datetime] NULL,
		[ImportKey] [nvarchar](50) NULL
		)

	CREATE TABLE #tmpTitle_TitleIdentifier(
		[Title_TitleIdentifierID] [int] NOT NULL,
		[ImportSourceID] [int] NULL,
		[IdentifierName] [nvarchar](40) NOT NULL,
		[IdentifierValue] [nvarchar](125) NOT NULL,
		[ExternalCreationDate] [datetime] NULL,
		[ExternalLastModifiedDate] [datetime] NULL,
		[ImportKey] [nvarchar](50) NULL
		)

	CREATE TABLE #tmpTitleAssociation (
		[TitleAssociationID] [int] NOT NULL,
		[ImportSourceID] [int] NULL,
		[MARCTag] [nvarchar](20) NOT NULL,
		[MARCIndicator2] [nchar](1) NOT NULL,
		[Title] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[Section] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[Volume] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[Heading] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[Publication] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[Relationship] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[Active] [bit] NOT NULL,
		[ImportKey] [nvarchar](50) NULL
		)

	CREATE TABLE #tmpTitleAssociation_TitleIdentifier (
		[TitleAssociation_TitleIdentifierID] [int] NOT NULL,
		[ImportSourceID] [int] NULL,
		[MARCTag] [nvarchar](20) NOT NULL,
		[MARCIndicator2] [nchar](1) NOT NULL,
		[Title] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[Section] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[Volume] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[Heading] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[Publication] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[Relationship] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[IdentifierName] [nvarchar](40) NOT NULL,
		[IdentifierValue] [nvarchar](125) NOT NULL,
		[ImportKey] [nvarchar](50) NULL
		)

	CREATE TABLE #tmpTitleVariant (
		[TitleVariantID] [int] NOT NULL,
		[ImportSourceID] [int] NULL,
		[MARCTag] [nvarchar](20) NOT NULL,
		[MARCIndicator2] [nchar](1) NOT NULL,
		[Title] [nvarchar](MAX) NOT NULL,
		[TitleRemainder] [nvarchar](MAX) NOT NULL,
		[PartNumber] [nvarchar](255) NOT NULL,
		[PartName] [nvarchar](255) NOT NULL,
		[ImportKey] [nvarchar](50) NULL
		)

	CREATE TABLE #tmpTitleNote (
		[TitleNoteID] [int] NOT NULL,
		[ImportSourceID] [int] NULL,
		[NoteText] [nvarchar](max) NOT NULL,
		[MarcDataFieldTag] [nvarchar](5) NULL,
		[MarcIndicator1] [nvarchar](5) NULL,
		[NoteSequence] [smallint] NULL,
		[ImportKey] [nvarchar](50) NOT NULL
		)

	CREATE TABLE #tmpCreator (
		[CreatorID] [int] NOT NULL,
		[ProductionAuthorID] [int] NULL,
		[ProductionAuthorNameID] [int] NULL,
		[ImportSourceID] [int] NOT NULL,
		[CreatorName] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[FirstNameFirst] [nvarchar](255) NULL,
		[SimpleName] [nvarchar](255) NULL,
		[DOB] [nvarchar](50) NULL,
		[DOD] [nvarchar](50) NULL,
		[Biography] [ntext] NULL,
		[CreatorNote] [nvarchar](255) NULL,
		[MARCDataFieldTag] [nvarchar](3) NULL,
		[MARCCreator_a] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_b] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_c] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_d] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_q] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_Full] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[ExternalCreationDate] [datetime] NULL,
		[ExternalLastModifiedDate] [datetime] NULL
		)

	CREATE TABLE #tmpTitle_Creator (
		[TitleCreatorID] [int] NOT NULL,
		[ProductionAuthorID] [int] NULL,
		[CreatorName] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[MARCCreator_a] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_b] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_c] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_d] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_e] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_q] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_t] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[CreatorRoleTypeID] [int] NOT NULL,
		[ExternalCreationDate] [datetime] NULL,
		[ExternalLastModifiedDate] [datetime] NULL,
		[ExternalCreationUser] [int] NULL,
		[ExternalLastModifiedUser] [int] NULL,
		[ImportKey] [nvarchar](50) NULL
		)

	CREATE TABLE #tmpTitleLanguage
	(
		[TitleLanguageID] [int] NOT NULL,
		[ImportSourceID] [int] NOT NULL,
		[ImportKey] [nvarchar](50) NOT NULL DEFAULT(''),
		[LanguageCode] [nvarchar](10) NOT NULL DEFAULT('')
	)

	CREATE TABLE #tmpItem (
		[ItemID] [int] NOT NULL,
		[ImportSourceID] [int] NOT NULL,
		[MARCBibID] [nvarchar](50) NOT NULL,
		[BarCode] [nvarchar](40) NOT NULL,
		[ItemSequence] [smallint] NULL,
		[MaxExistingItemSequence] [smallint] NULL DEFAULT(0),	-- highest existing production sequence for this barcode
		[MARCItemID] [nvarchar](50) NULL,
		[CallNumber] [nvarchar](100) NULL,
		[Volume] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[InstitutionCode] [nvarchar](10) NULL,
		[ScanningInstitutionCode] [nvarchar](10) NULL,
		[RightsHolderCode] [nvarchar](10) NULL,
		[LanguageCode] [nvarchar](10) NULL,
		[Sponsor] [nvarchar](100) NULL,
		[ItemDescription] [ntext] NULL,
		[ScannedBy] [int] NULL,
		[PDFSize] [int] NULL,
		[VaultID] [int] NULL,
		[Note] [nvarchar](255) NULL,
		[ItemStatusID] [int] NOT NULL,
		[ScanningUser] [nvarchar](100) NULL,
		[ScanningDate] [datetime] NULL,
		[Year] [nvarchar](20) NULL,
		[IdentifierBib] [nvarchar](50) NULL,
		[ZQuery] [nvarchar](200) NULL,
		[LicenseUrl] [nvarchar](max) NULL,
		[Rights] [nvarchar](max) NULL,
		[DueDiligence] [nvarchar](max) NULL,
		[CopyrightStatus] [nvarchar](max) NULL,
		[CopyrightRegion] [nvarchar](50) NULL,
		[CopyrightComment] [nvarchar](max) NULL,
		[CopyrightEvidence] [nvarchar](max) NULL,
		[CopyrightEvidenceOperator] [nvarchar](100) NULL,
		[CopyrightEvidenceDate] [nvarchar](30) NULL,
		[PaginationCompleteUserID] [int] NULL,
		[PaginationCompleteDate] [datetime] NULL,
		[PaginationStatusID] [int] NULL,
		[PaginationStatusUserID] [int] NULL,
		[PaginationStatusDate] [datetime] NULL,
		[LastPageNameLookupDate] [datetime] NULL,
		[ExternalCreationDate] [datetime] NULL,
		[ExternalLastModifiedDate] [datetime] NULL,
		[ExternalCreationUser] [int] NULL,
		[ExternalLastModifiedUser] [int] NULL,
		[ImportKey] [nvarchar](50) NULL,
		[EndYear] [nvarchar](20) NOT NULL,
		[StartVolume] [nvarchar](10) NOT NULL,
		[EndVolume] [nvarchar](10) NOT NULL,
		[StartIssue] [nvarchar](10) NOT NULL,
		[EndIssue] [nvarchar](10) NOT NULL,
		[StartNumber] [nvarchar](10) NOT NULL,
		[EndNumber] [nvarchar](10) NOT NULL,
		[StartSeries] [nvarchar](10) NOT NULL,
		[EndSeries] [nvarchar](10) NOT NULL,
		[StartPart] [nvarchar](10) NOT NULL,
		[EndPart] [nvarchar](10) NOT NULL
		)

	CREATE TABLE #tmpItemLanguage
	(
		[ItemLanguageID] [int] NOT NULL,
		[ImportSourceID] [int] NULL,
		[BarCode] [nvarchar](40) NOT NULL,
		[LanguageCode] [nvarchar](10) NOT NULL DEFAULT('')
	)

	CREATE TABLE #tmpPage (
		[PageID] [int] NOT NULL,
		[BarCode] [nvarchar](40) NOT NULL,
		[FileNamePrefix] [nvarchar](50) NOT NULL,
		[SequenceOrder] [int] NULL,
		[PageDescription] [nvarchar](255) NULL,
		[Illustration] [bit] NULL DEFAULT(0),
		[Note] [nvarchar](255) NULL,
		[FileSize_Temp] [int] NULL,
		[FileExtension] [nvarchar](5) NULL,
		[Active] [bit] NOT NULL,
		[Year] [nvarchar](20) NULL,
		[Series] [nvarchar](20) NULL,
		[Volume] [nvarchar](20) NULL,
		[Issue] [nvarchar](20) NULL,
		[ExternalURL] [nvarchar](500) NULL,
		[AltExternalURL] [nvarchar](500) NULL,
		[IssuePrefix] [nvarchar](20) NULL,
		[LastPageNameLookupDate] [datetime] NULL,
		[PaginationUserID] [int] NULL,
		[PaginationDate] [datetime] NULL,
		[ExternalCreationDate] [datetime] NULL,
		[ExternalLastModifiedDate] [datetime] NULL,
		[ExternalCreationUser] [int] NULL,
		[ExternalLastModifiedUser] [int] NULL
		)

	CREATE TABLE #tmpIndicatedPage (
		[IndicatedPageID] [int] NOT NULL,
		[BarCode] [nvarchar](40) NOT NULL,
		[FileNamePrefix] [nvarchar](50) NOT NULL,
		[SequenceOrder] [int] NULL,
		[Sequence] [smallint] NULL,
		[PagePrefix] [nvarchar](40) NULL,
		[PageNumber] [nvarchar](20) NULL,
		[Implied] [bit] NOT NULL,
		[ExternalCreationDate] [datetime] NULL,
		[ExternalLastModifiedDate] [datetime] NULL,
		[ExternalCreationUser] [int] NULL,
		[ExternalLastModifiedUser] [int] NULL
		)

	CREATE TABLE #tmpPage_PageType (
		[PagePageTypeID] [int] NOT NULL,
		[BarCode] [nvarchar](40) NOT NULL,
		[FileNamePrefix] [nvarchar](50) NOT NULL,
		[SequenceOrder] [int] NULL,
		[PageTypeID] [int] NOT NULL,
		[Verified] [bit] NOT NULL,
		[ExternalCreationDate] [datetime] NULL,
		[ExternalLastModifiedDate] [datetime] NULL,
		[ExternalCreationUser] [int] NULL,
		[ExternalLastModifiedUser] [int] NULL
		)


	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get the identifier we'll use to tie an item record to title information

	SELECT	@ImportKey = ImportKey
	FROM	dbo.Item
	WHERE	BarCode = @BarCode

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Titles

	INSERT INTO #tmpTitle
	SELECT	t.[TitleID],
			t.[MARCBibID],
			t.[MARCLeader],
			t.[FullTitle],
			t.[ShortTitle],
			t.[UniformTitle],
			t.[SortTitle],
			t.[PartNumber],
			t.[PartName],
			t.[CallNumber],
			t.[PublicationDetails],
			t.[StartYear],
			CASE WHEN t.[EndYear] = 9999 THEN NULL ELSE t.[EndYear] END,
			t.[Datafield_260_a],
			t.[Datafield_260_b],
			t.[Datafield_260_c],
			CASE WHEN t.[InstitutionCode] = '' THEN NULL ELSE t.[InstitutionCode] END,
			CASE WHEN t.[LanguageCode] = '' THEN NULL ELSE t.[LanguageCode] END,
			t.[TitleDescription],
			t.[TL2Author],
			t.[PublishReady],
			t.[RareBooks],
			t.[OriginalCatalogingSource],
			t.[EditionStatement],
			t.[CurrentPublicationFrequency],
			t.[Note],
			t.[ExternalCreationDate],
			t.[ExternalLastModifiedDate],
			t.[ExternalCreationUser],
			t.[ExternalLastModifiedUser],
			@ImportKey,
			NULL AS ProductionTitleID
	FROM	dbo.Title t
	WHERE	t.ImportStatusID = 10
	AND		t.ImportKey = @ImportKey
	AND		t.ImportSourceID = @ImportSourceID

	-- Add the necessary audit values
	UPDATE	#tmpTitle
	SET		ExternalCreationDate = CASE WHEN ExternalCreationDate IS NULL THEN GETDATE() ELSE ExternalCreationDate END,
			ExternalLastModifiedDate = CASE WHEN ExternalLastModifiedDate IS NULL THEN GETDATE() ELSE ExternalLastModifiedDate END,
			ExternalCreationUser = CASE WHEN ExternalCreationUser IS NULL THEN 1 ELSE ExternalCreationUser END,
			ExternalLastModifiedUser = CASE WHEN ExternalLastModifiedUser IS NULL THEN 1 ELSE ExternalLastModifiedUser END

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Title Tags
	
	-- Remove "from old catalog" text
	INSERT INTO #tmpKeyword
	SELECT	tt.[TitleTagID],
			RTRIM(REPLACE(tt.[TagText], '[from old catalog]', '')),
			tt.[MarcDataFieldTag],
			tt.[MarcSubFieldCode],
			tt.[ExternalCreationDate],
			tt.[ExternalLastModifiedDate],
			@ImportKey
	FROM	dbo.TitleTag tt
	WHERE	tt.ImportStatusID = 10
	AND		tt.ImportKey = @ImportKey
	AND		tt.ImportSourceID = @ImportSourceID

	-- Strip trailing periods from Keyword values
	UPDATE #tmpKeyword
	SET		Keyword = CASE WHEN RIGHT(Keyword, 1) = '.'
						THEN LEFT(Keyword, LEN(Keyword) - 1)
						ELSE Keyword
						END
	
	-- Add the necessary audit values
	UPDATE	#tmpKeyword
	SET		ExternalCreationDate = CASE WHEN ExternalCreationDate IS NULL THEN GETDATE() ELSE ExternalCreationDate END,
			ExternalLastModifiedDate = CASE WHEN ExternalLastModifiedDate IS NULL THEN GETDATE() ELSE ExternalLastModifiedDate END

	-- Remove duplicate tags
	SELECT	tt.KeywordID,
			0 AS ProductionKeywordID,
			tt.Keyword,
			MarcDataFieldTag,
			MarcSubFieldCode,
			ExternalCreationDate,
			ExternalLastModifiedDate,
			ImportKey
	INTO	#tmpKeyword2
	FROM	#tmpKeyword tt INNER JOIN (
					SELECT MIN(KeywordID) AS KeywordID, Keyword
					FROM #tmpKeyword GROUP BY Keyword) X 
				ON tt.KeywordID = X.KeywordID

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Title Identifiers
	INSERT INTO #tmpTitle_TitleIdentifier
	SELECT	ti.[Title_TitleIdentifierID],
			ti.[ImportSourceID],
			ti.[IdentifierName],
			ti.[IdentifierValue],
			ti.[ExternalCreationDate],
			ti.[ExternalLastModifiedDate],
			@ImportKey
	FROM	dbo.Title_TitleIdentifier ti
	WHERE	ti.ImportStatusID = 10
	AND		ti.ImportKey = @ImportKey
	AND		ti.ImportSourceID = @ImportSourceID

	-- Add the necessary audit values
	UPDATE	#tmpTitle_TitleIdentifier
	SET		ExternalCreationDate = CASE WHEN ExternalCreationDate IS NULL THEN GETDATE() ELSE ExternalCreationDate END,
			ExternalLastModifiedDate = CASE WHEN ExternalLastModifiedDate IS NULL THEN GETDATE() ELSE ExternalLastModifiedDate END

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Title Associations
	INSERT INTO #tmpTitleAssociation
	SELECT	a.[TitleAssociationID],
			a.[ImportSourceID],
			a.[MARCTag],
			a.[MARCIndicator2],
			a.[Title],
			a.[Section],
			a.[Volume],
			a.[Heading],
			a.[Publication],
			a.[Relationship],
			a.[Active],
			@ImportKey
	FROM	dbo.TitleAssociation a
	WHERE	a.ImportStatusID = 10
	AND		a.ImportKey = @ImportKey
	AND		a.ImportSourceID = @ImportSourceID

	-- Remove "from old catalog" text from title
	UPDATE	#tmpTitleAssociation
	SET		Title = RTRIM(REPLACE(Title, '[from old catalog]', ''))

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Title Association Title Identifiers
	INSERT INTO #tmpTitleAssociation_TitleIdentifier
	SELECT	a.[TitleAssociation_TitleIdentifierID],
			a.[ImportSourceID],
			a.[MARCTag],
			a.[MARCIndicator2],
			a.[Title],
			a.[Section],
			a.[Volume],
			a.[Heading],
			a.[Publication],
			a.[Relationship],
			a.[IdentifierName],
			a.[IdentifierValue],
			@ImportKey
	FROM	dbo.TitleAssociation_TitleIdentifier a
	WHERE	a.ImportStatusID = 10
	AND		a.ImportKey = @ImportKey
	AND		a.ImportSourceID = @ImportSourceID
	
	-- Remove "from old catalog" text from title
	UPDATE	#tmpTitleAssociation_TitleIdentifier
	SET		Title = RTRIM(REPLACE(Title, '[from old catalog]', ''))

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Title Variants
	INSERT INTO #tmpTitleVariant
	SELECT	v.[TitleVariantID],
			v.[ImportSourceID],
			v.[MARCTag],
			v.[MARCIndicator2],
			v.[Title],
			v.[TitleRemainder],
			v.[PartNumber],
			v.[PartName],
			@ImportKey
	FROM	dbo.TitleVariant v
	WHERE	v.ImportStatusID = 10
	AND		v.ImportKey = @ImportKey
	AND		v.ImportSourceID = @ImportSourceID

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Title Notes
	INSERT INTO #tmpTitleNote
	SELECT	TitleNoteID,
			ImportSourceID,
			NoteText,
			MarcDataFieldTag,
			MarcIndicator1,
			NoteSequence,
			@ImportKey
	FROM	dbo.TitleNote
	WHERE	ImportStatusID = 10
	AND		ImportKey = @ImportKey
	AND		ImportSourceID = @ImportSourceID

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Title Languages

	INSERT INTO #tmpTitleLanguage
	SELECT	tl.[TitleLanguageID],
			tl.[ImportSourceID],
			@ImportKey,
			tl.[LanguageCode]
	FROM	dbo.TitleLanguage tl
	WHERE	tl.ImportStatusID = 10
	AND		tl.ImportKey = @ImportKey
	AND		tl.ImportSourceID = @ImportSourceID

	-- Correct frequently incorrect language code for Japanese
	UPDATE	#tmpTitleLanguage
	SET		LanguageCode = 'jpn'
	WHERE	LanguageCode = 'jap'

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Title_Creators

	INSERT INTO #tmpTitle_Creator
	SELECT	tc.[TitleCreatorID],
			NULL,
			tc.[CreatorName],
			tc.[MARCCreator_a],
			tc.[MARCCreator_b],
			tc.[MARCCreator_c],
			tc.[MARCCreator_d],
			tc.[MARCCreator_e],
			tc.[MARCCreator_q],
			tc.[MARCCreator_t],
			tc.[CreatorRoleTypeID],
			tc.[ExternalCreationDate],
			tc.[ExternalLastModifiedDate],
			tc.[ExternalCreationUser],
			tc.[ExternalLastModifiedUser],
			@ImportKey
	FROM	dbo.Title_Creator tc
	WHERE	tc.ImportStatusID = 10
	AND		tc.ImportKey = @ImportKey
	AND		tc.ImportSourceID = @ImportSourceID

	-- Add the necessary audit values
	UPDATE	#tmpTitle_Creator
	SET		ExternalCreationDate = CASE WHEN ExternalCreationDate IS NULL THEN GETDATE() ELSE ExternalCreationDate END,
			ExternalLastModifiedDate = CASE WHEN ExternalLastModifiedDate IS NULL THEN GETDATE() ELSE ExternalLastModifiedDate END,
			ExternalCreationUser = CASE WHEN ExternalCreationUser IS NULL THEN 1 ELSE ExternalCreationUser END,
			ExternalLastModifiedUser = CASE WHEN ExternalLastModifiedUser IS NULL THEN 1 ELSE ExternalLastModifiedUser END

	-- Remove "from old catalog" text from creatorname and MARCCreator_q
	UPDATE	#tmpTitle_Creator
	SET		CreatorName = RTRIM(REPLACE(CreatorName, '[from old catalog]', '')),
			MARCCreator_q = RTRIM(REPLACE(MARCCreator_q, '[from old catalog]', ''))

	-- Find production Author IDs for the selected authors
	UPDATE	#tmpTitle_Creator
	SET		ProductionAuthorID = a.AuthorID
	FROM	#tmpTitle_Creator t 
			INNER JOIN dbo.BHLAuthorName n ON t.CreatorName = n.FullName COLLATE SQL_Latin1_General_CP1_CI_AI
			INNER JOIN dbo.BHLAuthor a ON a.AuthorID = n.AuthorID
	WHERE	(  -- If b is blank, match records with blank Numeration/Unit values
			(ISNULL(t.MARCCreator_b, '') = '' AND ISNULL(a.Numeration, '') = '' AND ISNULL(a.Unit, '') = '') 
			OR  -- If b is not blank, find records with matching Numeration/Unit values
			(ISNULL(t.MARCCreator_b, '') <> '' AND
				(ISNULL(t.MARCCreator_b, '') = ISNULL(a.Numeration, '') COLLATE SQL_Latin1_General_CP1_CI_AI OR
				ISNULL(t.MARCCreator_b, '') = ISNULL(a.Unit, '') COLLATE SQL_Latin1_General_CP1_CI_AI))
			)
	AND		(  -- If c is blank, match records with blank Numeration/Unit values
			(ISNULL(t.MARCCreator_c, '') = '' AND ISNULL(a.Title, '') = '' AND ISNULL(a.Location, '') = '')
			OR  -- If c is not blank, find records with matching Numeration/Unit values
			(ISNULL(t.MARCCreator_c, '') <> '' AND
				(ISNULL(t.MARCCreator_c, '') = ISNULL(a.Title, '') COLLATE SQL_Latin1_General_CP1_CI_AI OR
				ISNULL(t.MARCCreator_c, '') = ISNULL(a.Location, '') COLLATE SQL_Latin1_General_CP1_CI_AI))
			)

	-- If a selected production author ID has been redirected to a different 
	-- author, then use that author instead.  Follow the "redirect" chain up 
	-- to ten levels.
	UPDATE	#tmpTitle_Creator
	SET		ProductionAuthorID = COALESCE(a10.AuthorID, a9.AuthorID, a8.AuthorID, a7.AuthorID, a6.AuthorID,
										a5.AuthorID, a4.AuthorID, a3.AuthorID, a2.AuthorID, a1.AuthorID)
	FROM	#tmpTitle_Creator c INNER JOIN dbo.BHLAuthor a1 ON c.ProductionAuthorID = a1.AuthorID
			LEFT JOIN dbo.BHLAuthor a2 ON a1.RedirectAuthorID = a2.AuthorID
			LEFT JOIN dbo.BHLAuthor a3 ON a2.RedirectAuthorID = a3.AuthorID
			LEFT JOIN dbo.BHLAuthor a4 ON a3.RedirectAuthorID = a4.AuthorID
			LEFT JOIN dbo.BHLAuthor a5 ON a4.RedirectAuthorID = a5.AuthorID
			LEFT JOIN dbo.BHLAuthor a6 ON a5.RedirectAuthorID = a6.AuthorID
			LEFT JOIN dbo.BHLAuthor a7 ON a6.RedirectAuthorID = a7.AuthorID
			LEFT JOIN dbo.BHLAuthor a8 ON a7.RedirectAuthorID = a8.AuthorID
			LEFT JOIN dbo.BHLAuthor a9 ON a8.RedirectAuthorID = a9.AuthorID
			LEFT JOIN dbo.BHLAuthor a10 ON a9.RedirectAuthorID = a10.AuthorID
	WHERE	c.ProductionAuthorID IS NOT NULL

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Creators

	INSERT INTO #tmpCreator
	SELECT	c.[CreatorID],
			NULL,
			NULL,
			c.[ImportSourceID],
			c.[CreatorName],
			c.[FirstNameFirst],
			c.[SimpleName],
			c.[DOB],
			c.[DOD],
			c.[Biography],
			c.[CreatorNote],
			c.[MARCDataFieldTag],
			c.[MARCCreator_a],
			c.[MARCCreator_b],
			c.[MARCCreator_c],
			c.[MARCCreator_d],
			c.[MARCCreator_q],
			c.[MARCCreator_Full],
			c.[ExternalCreationDate],
			c.[ExternalLastModifiedDate]
	FROM	dbo.Creator c LEFT JOIN (
							SELECT DISTINCT CreatorName
							FROM	dbo.Title_Creator c
							WHERE	c.ImportKey = @ImportKey
							) tc
				ON c.CreatorName = tc.CreatorName
	WHERE	c.ImportStatusID = 10
	AND		c.ImportSourceID = @ImportSourceID
	AND		tc.CreatorName IS NOT NULL

	-- Add the necessary audit values
	UPDATE	#tmpCreator
	SET		ExternalCreationDate = CASE WHEN ExternalCreationDate IS NULL THEN GETDATE() ELSE ExternalCreationDate END,
			ExternalLastModifiedDate = CASE WHEN ExternalLastModifiedDate IS NULL THEN GETDATE() ELSE ExternalLastModifiedDate END

	-- Remove "from old catalog" text from creatorname and MARCCreator_q
	UPDATE	#tmpCreator
	SET		CreatorName = RTRIM(REPLACE(CreatorName, '[from old catalog]', '')),
			MARCCreator_q = RTRIM(REPLACE(MARCCreator_q, '[from old catalog]', ''))

	-- Find production Author IDs for the selected authors
	UPDATE	#tmpCreator
	SET		ProductionAuthorID = a.AuthorID,
			ProductionAuthorNameID = n.AuthorNameID
	FROM	#tmpCreator t INNER JOIN dbo.BHLAuthor a
				ON  (ISNULL(t.MARCCreator_b, '') = ISNULL(a.Numeration, '') COLLATE SQL_Latin1_General_CP1_CI_AI OR
					 ISNULL(t.MARCCreator_b, '') = ISNULL(a.Unit, '') COLLATE SQL_Latin1_General_CP1_CI_AI)
				AND (ISNULL(t.MARCCreator_c, '') = ISNULL(a.Title, '') COLLATE SQL_Latin1_General_CP1_CI_AI OR
					 ISNULL(t.MARCCreator_c, '') = ISNULL(a.Location, '') COLLATE SQL_Latin1_General_CP1_CI_AI)
				AND ISNULL(t.DOB, '') = ISNULL(a.Startdate, '')
				AND	ISNULL(t.DOD, '') = ISNULL(a.EndDate, '')
			INNER JOIN dbo.BHLAuthorName n
				ON a.AuthorID = n.AuthorID
				AND t.CreatorName = n.FullName COLLATE SQL_Latin1_General_CP1_CI_AI

	-- If a selected production Author ID has been redirected to a different 
	-- author ID, then use that author ID instead.  Follow the "redirect" chain
	-- up to ten levels.
	UPDATE	#tmpCreator
	SET		ProductionAuthorID = COALESCE(a10.AuthorID, a9.AuthorID, a8.AuthorID, a7.AuthorID, a6.AuthorID,
										a5.AuthorID, a4.AuthorID, a3.AuthorID, a2.AuthorID, a1.AuthorID)
	FROM	#tmpCreator c INNER JOIN dbo.BHLAuthor a1 ON c.ProductionAuthorID = a1.AuthorID
			LEFT JOIN dbo.BHLAuthor a2 ON a1.RedirectAuthorID = a2.AuthorID
			LEFT JOIN dbo.BHLAuthor a3 ON a2.RedirectAuthorID = a3.AuthorID
			LEFT JOIN dbo.BHLAuthor a4 ON a3.RedirectAuthorID = a4.AuthorID
			LEFT JOIN dbo.BHLAuthor a5 ON a4.RedirectAuthorID = a5.AuthorID
			LEFT JOIN dbo.BHLAuthor a6 ON a5.RedirectAuthorID = a6.AuthorID
			LEFT JOIN dbo.BHLAuthor a7 ON a6.RedirectAuthorID = a7.AuthorID
			LEFT JOIN dbo.BHLAuthor a8 ON a7.RedirectAuthorID = a8.AuthorID
			LEFT JOIN dbo.BHLAuthor a9 ON a8.RedirectAuthorID = a9.AuthorID
			LEFT JOIN dbo.BHLAuthor a10 ON a9.RedirectAuthorID = a10.AuthorID
	WHERE	c.ProductionAuthorID IS NOT NULL
	
	-- Copy production author IDs from #tmpCreator to #tmpTitle_Creator
	UPDATE	#tmpTitle_Creator
	SET		ProductionAuthorID = c.ProductionAuthorID
	FROM	#tmpTitle_Creator t INNER JOIN #tmpCreator c
				ON ISNULL(t.[MARCCreator_a], '') = ISNULL(c.[MARCCreator_a], '')
				AND ISNULL(t.[MARCCreator_b], '') = ISNULL(c.[MARCCreator_b], '')
				AND ISNULL(t.[MARCCreator_c], '') = ISNULL(c.[MARCCreator_c], '')
				AND ISNULL(t.[MARCCreator_d], '') = ISNULL(c.[MARCCreator_d], '')
				AND ISNULL(t.[MARCCreator_q], '') = ISNULL(c.[MARCCreator_q], '')

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Items

	INSERT INTO #tmpItem
	SELECT	i.[ItemID],
			i.[ImportSourceID],
			i.[MARCBibID],
			i.[BarCode],
			i.[ItemSequence],
			0,	-- MaxExistingItemSequence
			i.[MARCItemID],
			i.[CallNumber],
			i.[Volume],
			CASE WHEN i.[InstitutionCode] = '' THEN NULL ELSE i.[InstitutionCode] END,
			i.ScanningInstitutionCode,
			i.RightsHolderCode,
			CASE WHEN i.[LanguageCode] = '' THEN NULL ELSE i.[LanguageCode] END,
			i.[Sponsor],
			i.[ItemDescription],
			i.[ScannedBy],
			i.[PDFSize],
			i.[VaultID],
			i.[Note],
			i.[ItemStatusID],
			i.[ScanningUser],
			i.[ScanningDate],
			i.[Year],
			i.[IdentifierBib],
			i.[ZQuery],
			i.[LicenseUrl],
			i.[Rights],
			i.[DueDiligence],
			i.[CopyrightStatus],
			i.[CopyrightRegion],
			i.[CopyrightComment],
			i.[CopyrightEvidence],
			i.[CopyrightEvidenceOperator],
			i.[CopyrightEvidenceDate],
			i.[PaginationCompleteUserID],
			i.[PaginationCompleteDate],
			ISNULL(i.[PaginationStatusID], 5),
			i.[PaginationStatusUserID],
			i.[PaginationStatusDate],
			i.[LastPageNameLookupDate],
			i.[ExternalCreationDate],
			i.[ExternalLastModifiedDate],
			i.[ExternalCreationUser],
			i.[ExternalLastModifiedUser],
			@ImportKey,
			i.[EndYear],
			i.[StartVolume],
			i.[EndVolume],
			i.[StartIssue],
			i.[EndIssue],
			i.[StartNumber],
			i.[EndNumber],
			i.[StartSeries],
			i.[EndSeries],
			i.[StartPart],
			i.[EndPart]
	FROM	dbo.Item i
	WHERE	i.ImportStatusID = 10
	AND		i.ImportSourceID = @ImportSourceID
	AND		i.BarCode = @BarCode

	-- Add the necessary audit values
	UPDATE	#tmpItem
	SET		ExternalCreationDate = CASE WHEN ExternalCreationDate IS NULL THEN GETDATE() ELSE ExternalCreationDate END,
			ExternalLastModifiedDate = CASE WHEN ExternalLastModifiedDate IS NULL THEN GETDATE() ELSE ExternalLastModifiedDate END,
			ExternalCreationUser = CASE WHEN ExternalCreationUser IS NULL THEN 1 ELSE ExternalCreationUser END,
			ExternalLastModifiedUser = CASE WHEN ExternalLastModifiedUser IS NULL THEN 1 ELSE ExternalLastModifiedUser END

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Item Languages

	INSERT INTO #tmpItemLanguage
	SELECT	il.[ItemLanguageID],
			il.[ImportSourceID],
			@BarCode,
			il.[LanguageCode]
	FROM	dbo.ItemLanguage il
	WHERE	il.ImportStatusID = 10
	AND		il.BarCode = @BarCode
	AND		il.ImportSourceID = @ImportSourceID

	-- Correct frequently incorrect language code for Japanese
	UPDATE	#tmpItemLanguage
	SET		LanguageCode = 'jpn'
	WHERE	LanguageCode = 'jap'

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Pages

	INSERT INTO #tmpPage
	SELECT	p.[PageID],
			p.[BarCode],
			p.[FileNamePrefix],
			p.[SequenceOrder],
			p.[PageDescription],
			p.[Illustration],
			p.[Note],
			p.[FileSize_Temp],
			p.[FileExtension],
			p.[Active],
			p.[Year],
			p.[Series],
			p.[Volume],
			p.[Issue],
			p.[ExternalURL],
			p.[AltExternalURL],
			p.[IssuePrefix],
			p.[LastPageNameLookupDate],
			p.[PaginationUserID],
			p.[PaginationDate],
			p.[ExternalCreationDate],
			p.[ExternalLastModifiedDate],
			p.[ExternalCreationUser],
			p.[ExternalLastModifiedUser]
	FROM	dbo.Page p
	WHERE	p.ImportStatusID = 10
	AND		p.ImportSourceID = @ImportSourceID
	AND		p.BarCode = @BarCode

	-- Add the necessary audit values
	UPDATE	#tmpPage
	SET		ExternalCreationDate = CASE WHEN ExternalCreationDate IS NULL THEN GETDATE() ELSE ExternalCreationDate END,
			ExternalLastModifiedDate = CASE WHEN ExternalLastModifiedDate IS NULL THEN GETDATE() ELSE ExternalLastModifiedDate END,
			ExternalCreationUser = CASE WHEN ExternalCreationUser IS NULL THEN 1 ELSE ExternalCreationUser END,
			ExternalLastModifiedUser = CASE WHEN ExternalLastModifiedUser IS NULL THEN 1 ELSE ExternalLastModifiedUser END

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Indicated Pages

	INSERT INTO #tmpIndicatedPage
	SELECT	ip.[IndicatedPageID],
			ip.[BarCode],
			ip.[FileNamePrefix],
			ip.[SequenceOrder],
			ip.[Sequence],
			ip.[PagePrefix],
			ip.[PageNumber],
			ip.[Implied],
			ip.[ExternalCreationDate],
			ip.[ExternalLastModifiedDate],
			ip.[ExternalCreationUser],
			ip.[ExternalLastModifiedUser]
	FROM	dbo.IndicatedPage ip
	WHERE	ip.ImportStatusID = 10
	AND		ip.ImportSourceID = @ImportSourceID
	AND		ip.BarCode = @BarCode

	-- Add the necessary audit values
	UPDATE	#tmpIndicatedPage
	SET		ExternalCreationDate = CASE WHEN ExternalCreationDate IS NULL THEN GETDATE() ELSE ExternalCreationDate END,
			ExternalLastModifiedDate = CASE WHEN ExternalLastModifiedDate IS NULL THEN GETDATE() ELSE ExternalLastModifiedDate END,
			ExternalCreationUser = CASE WHEN ExternalCreationUser IS NULL THEN 1 ELSE ExternalCreationUser END,
			ExternalLastModifiedUser = CASE WHEN ExternalLastModifiedUser IS NULL THEN 1 ELSE ExternalLastModifiedUser END

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Page Types

	INSERT INTO #tmpPage_PageType
	SELECT	ppt.[PagePageTypeID],
			ppt.[BarCode],
			ppt.[FileNamePrefix],
			ppt.[SequenceOrder],
			ppt.[PageTypeID],
			ppt.[Verified],
			ppt.[ExternalCreationDate],
			ppt.[ExternalLastModifiedDate],
			ppt.[ExternalCreationUser],
			ppt.[ExternalLastModifiedUser]
	FROM	dbo.Page_PageType ppt
	WHERE	ppt.ImportStatusID = 10
	AND		ppt.ImportSourceID = @ImportSourceID
	AND		ppt.BarCode = @BarCode

	-- Add the necessary audit values
	UPDATE	#tmpPage_PageType
	SET		ExternalCreationDate = CASE WHEN ExternalCreationDate IS NULL THEN GETDATE() ELSE ExternalCreationDate END,
			ExternalLastModifiedDate = CASE WHEN ExternalLastModifiedDate IS NULL THEN GETDATE() ELSE ExternalLastModifiedDate END,
			ExternalCreationUser = CASE WHEN ExternalCreationUser IS NULL THEN 1 ELSE ExternalCreationUser END,
			ExternalLastModifiedUser = CASE WHEN ExternalLastModifiedUser IS NULL THEN 1 ELSE ExternalLastModifiedUser END

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Resolve titles.  

	-- Multiple attempts are made to find a matching title in production.  In
	-- order, the following criteria are used to find a match:
	--
	--	1) OCLC
	--	2) WonderFetch TitleID + Institution Code
	--	3) MARC 001 value + Institution Code
	--
	-- After titles have been resolved, any records remaining in the #tmpTitle 
	-- table without a value in the ProductionTitleID column will be inserted
	-- into the production database as new titles.

	-- Match on OCLC
	UPDATE	#tmpTitle
	SET		ProductionTitleID = bt.TitleID
	FROM	#tmpTitle t INNER JOIN #tmpTitle_TitleIdentifier tti
				ON t.ImportKey = tti.ImportKey
				AND 'OCLC' = tti.IdentifierName
			INNER JOIN dbo.BHLTitle_Identifier btti
				ON tti.IdentifierValue = btti.IdentifierValue
			INNER JOIN dbo.BHLIdentifier bti
				ON btti.IdentifierID = bti.IdentifierID
				AND tti.IdentifierName = bti.IdentifierName
			INNER JOIN dbo.BHLTitle bt
				ON btti.TitleID = bt.TitleID
	WHERE	t.ProductionTitleID IS NULL

	-- Match on WonderFetch Title ID + Institution Code
	UPDATE	#tmpTitle
	SET		ProductionTitleID = bt.TitleID
	FROM	#tmpTitle t INNER JOIN #tmpTitle_TitleIdentifier tti
				ON t.ImportKey = tti.ImportKey
				AND 'WonderFetch' = tti.IdentifierName
			INNER JOIN dbo.BHLTitle_Identifier btti
				ON tti.IdentifierValue = btti.IdentifierValue
			INNER JOIN dbo.BHLIdentifier bti
				ON btti.IdentifierID = bti.IdentifierID
				AND tti.IdentifierName = bti.IdentifierName
			INNER JOIN dbo.BHLTitle bt
				ON btti.TitleID = bt.TitleID
			INNER JOIN dbo.BHLTitleItem btitem
				ON bt.TitleID = btitem.TitleID
			INNER JOIN dbo.BHLItemInstitution bii
				ON btitem.ItemID = bii.ItemID
				AND t.InstitutionCode = bii.InstitutionCode
			INNER JOIN dbo.BHLInstitutionRole br
				ON bii.InstitutionRoleID = br.InstitutionRoleID
	WHERE	t.ProductionTitleID IS NULL
	AND		br.InstitutionRoleName = 'Holding Institution'

	-- Match on MARC 001 Value + Institution Code
	UPDATE	#tmpTitle
	SET		ProductionTitleID = bt.TitleID
	FROM	#tmpTitle t INNER JOIN #tmpTitle_TitleIdentifier tti
				ON t.ImportKey = tti.ImportKey
				AND 'MARC001' = tti.IdentifierName
			INNER JOIN dbo.BHLTitle_Identifier btti
				ON tti.IdentifierValue = btti.IdentifierValue
			INNER JOIN dbo.BHLIdentifier bti
				ON btti.IdentifierID = bti.IdentifierID
				AND tti.IdentifierName = bti.IdentifierName
			INNER JOIN dbo.BHLTitle bt
				ON btti.TitleID = bt.TitleID
			INNER JOIN dbo.BHLTitleItem btitem
				ON bt.TitleID = btitem.TitleID
			INNER JOIN dbo.BHLItemInstitution bii
				ON btitem.ItemID = bii.ItemID
				AND t.InstitutionCode = bii.InstitutionCode
			INNER JOIN dbo.BHLInstitutionRole br
				ON bii.InstitutionRoleID = br.InstitutionRoleID
	WHERE	t.ProductionTitleID IS NULL
	AND		br.InstitutionRoleName = 'Holding Institution'

	-- ** REMOVED 4/24/2015 TO PREVENT FALSE POSITIVES **
	/*
	-- Match on MARC Bib ID + Short Title
	UPDATE	#tmpTitle
	SET		ProductionTitleID = bt.TitleID
	FROM	#tmpTitle t INNER JOIN dbo.BHLTitle bt
				ON t.MARCBibID = bt.MARCBibID
				AND t.ShortTitle = bt.ShortTitle
	WHERE	t.ProductionTitleID IS NULL
	*/

	-- If the selected production title has been redirected to a different 
	-- title, then use that title instead.  Follow the "redirect" chain up 
	-- to ten levels.
	UPDATE	#tmpTitle
	SET		ProductionTitleID = COALESCE(bt10.TitleID, bt9.TitleID, bt8.TitleiD, bt7.TitleID, bt6.TitleID,
										bt5.TitleID, bt4.TitleID, bt3.TitleID, bt2.TitleID, bt1.TitleID),
			MarcBibID = COALESCE(bt10.MarcBibID, bt9.MarcBibID, bt8.MarcBibID, bt7.MarcBibID, bt6.MarcBibID,
								bt5.MarcBibID, bt4.MarcBibID, bt3.MarcBibID, bt2.MarcBibID, bt1.MarcBibID)
	FROM	#tmpTitle t INNER JOIN dbo.BHLTitle bt1
				ON t.ProductionTitleID = bt1.TitleID
			LEFT JOIN dbo.BHLTitle bt2 ON bt1.RedirectTitleID = bt2.TitleID
			LEFT JOIN dbo.BHLTitle bt3 ON bt2.RedirectTitleID = bt3.TitleID
			LEFT JOIN dbo.BHLTitle bt4 ON bt3.RedirectTitleID = bt4.TitleID
			LEFT JOIN dbo.BHLTitle bt5 ON bt4.RedirectTitleID = bt5.TitleID
			LEFT JOIN dbo.BHLTitle bt6 ON bt5.RedirectTitleID = bt6.TitleID
			LEFT JOIN dbo.BHLTitle bt7 ON bt6.RedirectTitleID = bt7.TitleID
			LEFT JOIN dbo.BHLTitle bt8 ON bt7.RedirectTitleID = bt8.TitleID
			LEFT JOIN dbo.BHLTitle bt9 ON bt8.RedirectTitleID = bt9.TitleID
			LEFT JOIN dbo.BHLTitle bt10 ON bt9.RedirectTitleID = bt10.TitleID
	WHERE	t.ProductionTitleID IS NOT NULL

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Update the production database (very few updates for data from Internet
	-- Archive... for the most part, we only *insert* data from IA)
	BEGIN TRY
		BEGIN TRAN

		-- Update existing titles in the production database.  Update only the
		-- MARCBibID so that the title will reference the correct MARC XML file.
		UPDATE	dbo.BHLTitle
		SET		MARCBibID = tmp.MARCBibID,
				LastModifiedDate = tmp.ExternalLastModifiedDate,
				LastModifiedUserID = tmp.ExternalLastModifiedUser
		FROM	#tmpTitle tmp INNER JOIN dbo.BHLTitle t
					ON tmp.ProductionTitleID = t.TitleID

		SELECT @TitleUpdate = @@ROWCOUNT

		-- Insert new titles into the production database
		INSERT INTO dbo.BHLTitle (MARCBibID, MARCLeader, FullTitle, ShortTitle,
			UniformTitle, SortTitle, PartNumber, PartName, CallNumber, PublicationDetails, 
			StartYear, EndYear, 
			Datafield_260_a, Datafield_260_b, Datafield_260_c, 
			LanguageCode, TitleDescription, TL2Author, 
			PublishReady, RareBooks, OriginalCatalogingSource, EditionStatement, 
			CurrentPublicationFrequency, Note, CreationDate, LastModifiedDate, 
			CreationUserID, LastModifiedUserID)
		SELECT	tmp.MARCBibID, tmp.MARCLeader, CONVERT(nvarchar(2000), tmp.FullTitle),
				tmp.ShortTitle, tmp.UniformTitle, SUBSTRING(tmp.SortTitle, 1, 60), 
				tmp.PartNumber, tmp.PartName, tmp.CallNumber, 
				tmp.PublicationDetails, tmp.StartYear, tmp.EndYear,	tmp.Datafield_260_a, 
				tmp.Datafield_260_b, tmp.Datafield_260_c, UPPER(tmp.LanguageCode),
				tmp.TitleDescription, tmp.TL2Author,
				tmp.PublishReady, tmp.RareBooks, tmp.OriginalCatalogingSource, tmp.EditionStatement, 
				tmp.CurrentPublicationFrequency, tmp.Note, tmp.ExternalCreationDate, 
				tmp.ExternalLastModifiedDate, tmp.ExternalCreationUser, 
				tmp.ExternalLastModifiedUser
		FROM	#tmpTitle tmp
		WHERE	tmp.ProductionTitleID IS NULL

		SELECT @TitleInsert = @@ROWCOUNT

		-- Get the IDs of the newly inserted titles
		UPDATE	#tmpTitle
		SET		ProductionTitleID = t.TitleID
		FROM	#tmpTitle tmp INNER JOIN dbo.BHLTitle t
					ON tmp.MARCBibID = t.MARCBibID
					AND ISNULL(tmp.MARCLeader, '') = ISNULL(t.MARCLeader, '')
					AND ISNULL(CONVERT(nvarchar(2000), tmp.FullTitle), '') = ISNULL(CONVERT(nvarchar(2000), t.FullTitle), '')
					AND ISNULL(tmp.ShortTitle, '') = ISNULL(t.ShortTitle, '')
					AND ISNULL(tmp.CallNumber, '') = ISNULL(t.CallNumber, '')
					AND ISNULL(tmp.PublicationDetails, '') = ISNULL(t.PublicationDetails, '')
					AND tmp.ExternalCreationDate = t.CreationDate
					AND tmp.ExternalLastModifiedDate = t.LastModifiedDate
					AND tmp.ExternalCreationUser = t.CreationUserID
					AND tmp.ExternalLastModifiedUser = t.LastModifiedUserID
		WHERE	ProductionTitleID IS NULL

		-- Throw an error (will rollback the transaction) if we didn't get a production id
		IF EXISTS(SELECT TitleID FROM #tmpTitle WHERE ProductionTitleID IS NULL)
		BEGIN
			RAISERROR ('Failed to acquire a production title identifier.', 16, 1)
		END

		-- Keep a record of the production titleid that was inserted/updated
		UPDATE	dbo.Title
		SET		ProductionTitleID = tmp.ProductionTitleID
		FROM	dbo.Title t INNER JOIN #tmpTitle tmp
					ON t.TitleID = tmp.TitleID

		-- Add a bibliographic level for any just-added/updated titles that don't already have one
		UPDATE	dbo.BHLTitle
		SET		BibliographicLevelID = b.BibliographicLevelID
		FROM	dbo.BHLTitle t INNER JOIN dbo.BHLBibliographicLevel b
					ON SUBSTRING(t.MarcLeader, 8, 1) = b.MARCCode
				INNER JOIN #tmpTitle tmp
					ON t.TitleID = tmp.ProductionTitleID
		WHERE	t.BibliographicLevelID IS NULL

		-- =======================================================================

		-- Insert new authors into the production database
		DECLARE @NewAuthorID int
		DECLARE @CreatorID int
		DECLARE @DOB nvarchar(50)
		DECLARE @DOD nvarchar(50)
		DECLARE @MARCDataFieldTag nvarchar(3)
		DECLARE @CreatorName nvarchar(255)
		DECLARE @MARCCreator_b nvarchar(450)
		DECLARE @MARCCreator_c nvarchar(450)
		DECLARE @MARCCreator_d nvarchar(450)
		DECLARE @MARCCreator_q nvarchar(450)
		DECLARE @ExternalCreationDate datetime
		DECLARE @ExternalLastModifiedDate datetime
		SET @CreatorInsert = 0

		DECLARE	curInsert CURSOR 
		FOR SELECT DISTINCT
					MIN(MARCDataFieldTag), DOB, DOD, CreatorName, MarcCreator_b, 
					MarcCreator_c, MarcCreator_d, MarcCreator_q
			FROM	#tmpCreator
			WHERE	ProductionAuthorID IS NULL
			GROUP BY DOB, DOD, CreatorName, MarcCreator_b, MarcCreator_c, MarcCreator_d, MarcCreator_q
		
		OPEN curInsert
		FETCH NEXT FROM curInsert INTO @MARCDataFieldTag, @DOB, @DOD, @CreatorName,
			@MarcCreator_b, @MarcCreator_c, @MarcCreator_d, @MarcCreator_q

		WHILE (@@fetch_status <> -1)
		BEGIN
			IF (@@fetch_status <> -2)
			BEGIN

				-- Insert a new author record into the production database
				INSERT INTO dbo.BHLAuthor (AuthorTypeID, StartDate, EndDate,
					Numeration, Title, Unit, Location, IsActive, CreationDate,
					LastModifiedDate, CreationUserID, LastModifiedUserID)
				VALUES (CASE WHEN @MARCDataFieldTag IN ('100', '700') THEN 1
							WHEN @MARCDataFieldTag IN ('110', '710') THEN 2
							WHEN @MARCDataFieldTag IN ('111', '711') THEN 3 END,
						ISNULL(@DOB, ''), ISNULL(@DOD, ''),
						CASE WHEN @MARCDataFieldTag IN ('100', '700') THEN ISNULL(@MarcCreator_b, '') ELSE '' END,
						CASE WHEN @MARCDataFieldTag IN ('100', '700') THEN ISNULL(@MarcCreator_c, '') ELSE '' END,
						CASE WHEN @MARCDataFieldTag IN ('110', '710') THEN ISNULL(@MarcCreator_b, '') ELSE '' END,
						CASE WHEN @MARCDataFieldTag IN ('110', '710', '111', '711') THEN ISNULL(@MarcCreator_c, '') ELSE '' END,
						1,
						GETDATE(),
						GETDATE(),
						1, 1)
						
				-- Save the ID of the newly inserted author record
				SELECT @NewAuthorID = SCOPE_IDENTITY()
				
				UPDATE	#tmpCreator
				SET		ProductionAuthorID = @NewAuthorID
				WHERE	ISNULL(CreatorName, '') = ISNULL(@CreatorName, '')
				AND		ISNULL(MARCCreator_b, '') = ISNULL(@MarcCreator_b, '')
				AND		ISNULL(MARCCreator_c, '') = ISNULL(@MarcCreator_c, '')
				AND		ISNULL(MARCCreator_d, '') = ISNULL(@MarcCreator_d, '')
				AND		ISNULL(MARCCreator_q, '') = ISNULL(@MarcCreator_q, '')

				SET @CreatorInsert = @CreatorInsert + 1
			END

			FETCH NEXT FROM curInsert INTO @MARCDataFieldTag, @DOB, @DOD, @CreatorName,
				@MarcCreator_b, @MarcCreator_c, @MarcCreator_d, @MarcCreator_q
		END

		CLOSE curInsert
		DEALLOCATE curInsert

		-- Copy new production author IDs to #tmpTitle_Creator
		UPDATE	#tmpTitle_Creator
		SET		ProductionAuthorID = c.ProductionAuthorID
		FROM	#tmpTitle_Creator t INNER JOIN #tmpCreator c
					ON ISNULL(t.[MARCCreator_a], '') = ISNULL(c.[MARCCreator_a], '')
					AND ISNULL(t.[MARCCreator_b], '') = ISNULL(c.[MARCCreator_b], '')
					AND ISNULL(t.[MARCCreator_c], '') = ISNULL(c.[MARCCreator_c], '')
					AND ISNULL(t.[MARCCreator_d], '') = ISNULL(c.[MARCCreator_d], '')
					AND ISNULL(t.[MARCCreator_q], '') = ISNULL(c.[MARCCreator_q], '')
		WHERE	t.ProductionAuthorID IS NULL

		-- =======================================================================
		
		-- Insert new AuthorName records into the production database
		INSERT INTO dbo.BHLAuthorName (AuthorID, FullName, FullerForm, IsPreferredName,
			CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
		SELECT DISTINCT
				ProductionAuthorID,
				CreatorName,
				CASE WHEN MARCDataFieldTag IN ('100', '700') THEN ISNULL(MarcCreator_q, '') ELSE '' END AS FullerForm,
				1,
				GETDATE(),
				GETDATE(),
				1, 1
		FROM	#tmpCreator
		WHERE	ProductionAuthorNameID IS NULL

		-- =======================================================================

		-- Insert new TitleAuthor records into the production database
		INSERT INTO dbo.BHLTitleAuthor (TitleID, AuthorID, AuthorRoleID, Relationship, 
			TitleOfWork, CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
		SELECT	t.TitleID, 
				tmpC.ProductionAuthorID, 
				tmpC.CreatorRoleTypeID,
				ISNULL(CASE WHEN RIGHT(MARCCreator_e, 1) = '.' 
					THEN LEFT(MARCCreator_e, LEN(MARCCreator_e) - 1) 
					ELSE MARCCreator_e END, ''),
				ISNULL(CASE WHEN RIGHT(MARCCreator_t, 1) = '.' 
					THEN LEFT(MARCCreator_t, LEN(MARCCreator_t) - 1) 
					ELSE MARCCreator_t END, ''),
				tmpC.ExternalCreationDate,
				tmpC.ExternalLastModifiedDate,
				tmpC.ExternalCreationUser,
				tmpC.ExternalLastModifiedUser
		FROM	#tmpTitle_Creator tmpC INNER JOIN #tmpTitle tmpT
					ON tmpC.ImportKey = tmpT.ImportKey
				INNER JOIN dbo.BHLTitle t
					ON tmpT.ProductionTitleID = t.TitleID
				LEFT JOIN dbo.BHLTitleAuthor ta
					ON t.TitleID = ta.TitleID
					AND tmpC.ProductionAuthorID = ta.AuthorID
					AND tmpC.CreatorRoleTypeID = ta.AuthorRoleID
		WHERE	ta.TitleID IS NULL
				
		SELECT @TitleCreatorInsert = @@ROWCOUNT

		-- =======================================================================

		-- Insert new title_identifier records into the production database
		INSERT INTO dbo.BHLTitle_Identifier (TitleID, IdentifierID,
			IdentifierValue, CreationDate, LastModifiedDate)
		SELECT DISTINCT t.TitleID, i.IdentifierID, tmp.IdentifierValue, 
			tmp.ExternalCreationDate, tmp.ExternalLastModifiedDate
		FROM	#tmpTitle_TitleIdentifier tmp INNER JOIN dbo.BHLIdentifier i
					ON tmp.IdentifierName = i.IdentifierName
				INNER JOIN #tmpTitle tmpT
					ON tmp.ImportKey = tmpT.ImportKey
				INNER JOIN dbo.BHLTitle t
					ON tmpT.ProductionTitleID = t.TitleID
				LEFT JOIN dbo.BHLTitle_Identifier ti
					ON t.TitleID = ti.TitleID
					AND i.IdentifierID = ti.IdentifierID
					AND tmp.IdentifierValue = ti.IdentifierValue
		WHERE	ti.TitleIdentifierID IS NULL

		SELECT @TitleIdentifierInsert = @@ROWCOUNT


		-- =======================================================================

		-- Insert new titleassociation records into the production database
		INSERT INTO dbo.BHLTitleAssociation (TitleID, TitleAssociationTypeID,
			Title, Section, Volume, Heading, Publication, Relationship, Active)
		SELECT DISTINCT	t.TitleID, at.TitleAssociationTypeID, 
			tmp.Title, tmp.Section, tmp.Volume, tmp.Heading, 
			tmp.Publication, tmp.Relationship, tmp.Active
		FROM	#tmpTitleAssociation tmp INNER JOIN #tmpTitle tmpT
					ON tmp.ImportKey = tmpT.ImportKey
				INNER JOIN dbo.BHLTitle t
					ON tmpT.ProductionTitleID = t.TitleID
				INNER JOIN dbo.BHLTitleAssociationType at
					ON tmp.MARCTag = at.MARCTag
					AND tmp.MARCIndicator2 = at.MARCIndicator2
				LEFT JOIN dbo.BHLTitleAssociation a
					ON t.TitleID = a.TitleID
					AND at.TitleAssociationTypeID = a.TitleAssociationTypeID
					AND tmp.Title = a.Title
					AND tmp.Section = a.Section
					AND tmp.Volume = a.Volume
					AND tmp.Heading = a.Heading
					AND tmp.Publication = a.Publication
					AND tmp.Relationship = a.Relationship
		WHERE	a.TitleAssociationID IS NULL

		SELECT @TitleAssociationInsert = @@ROWCOUNT

		-- =======================================================================

		-- Insert new titleassociation_titleidentifier records into the production database
		INSERT INTO dbo.BHLTitleAssociation_TitleIdentifier (TitleAssociationID,
			TitleIdentifierID, IdentifierValue)
		SELECT DISTINCT	a.TitleAssociationID, ti.IdentifierID, tmp.IdentifierValue
		FROM	#tmpTitleAssociation_TitleIdentifier tmp INNER JOIN #tmpTitle tmpT
					ON tmp.ImportKey = tmpT.ImportKey
				INNER JOIN dbo.BHLTitle t
					ON tmpT.ProductionTitleID = t.TitleID
				INNER JOIN dbo.BHLTitleAssociationType at
					ON tmp.MARCTag = at.MARCTag
					AND tmp.MARCIndicator2 = at.MARCIndicator2
				INNER JOIN dbo.BHLTitleAssociation a
					ON t.TitleID = a.TitleID
					AND at.TitleAssociationTypeID = a.TitleAssociationTypeID
					AND tmp.Title = a.Title
					AND tmp.Section = a.Section
					AND tmp.Volume = a.Volume
					AND tmp.Heading = a.Heading
					AND tmp.Publication = a.Publication
					AND tmp.Relationship = a.Relationship
				INNER JOIN dbo.BHLIdentifier ti
					ON tmp.IdentifierName = ti.IdentifierName
				LEFT JOIN dbo.BHLTitleAssociation_TitleIdentifier i
					ON a.TitleAssociationID = i.TitleAssociationID
					AND ti.IdentifierID = i.TitleIdentifierID
					AND tmp.IdentifierValue = i.IdentifierValue					
		WHERE	i.TitleAssociation_TitleIdentifierID IS NULL

		SELECT @TitleAssociationTitleIdentifierInsert = @@ROWCOUNT

		-- Update the AssociatedTitleIDs in the production TitleAssociation table
		UPDATE	dbo.BHLTitleAssociation
		SET		AssociatedTitleID = ti.TitleID,
				LastModifiedDate = GETDATE()
		FROM	dbo.BHLTitleAssociation a INNER JOIN dbo.BHLTitleAssociation_TitleIdentifier i
					ON a.TitleAssociationID = i.TitleAssociationID
				INNER JOIN dbo.BHLTitle_Identifier ti
					ON i.TitleIdentifierID = ti.IdentifierID
					AND i.IdentifierValue = ti.IdentifierValue
		WHERE	a.AssociatedTitleID IS NULL

		-- =======================================================================

		-- Insert new titlevariant records into the production database
		INSERT INTO dbo.BHLTitleVariant (TitleID, TitleVariantTypeID,
			Title, TitleRemainder, PartNumber, PartName)
		SELECT DISTINCT	t.TitleID, vt.TitleVariantTypeID, 
			tmp.Title, tmp.TitleRemainder, tmp.PartNumber, tmp.PartName
		FROM	#tmpTitleVariant tmp INNER JOIN #tmpTitle tmpT
					ON tmp.ImportKey = tmpT.ImportKey
				INNER JOIN dbo.BHLTitle t
					ON tmpT.ProductionTitleID = t.TitleID
				INNER JOIN dbo.BHLTitleVariantType vt
					ON tmp.MARCTag = vt.MARCTag
					AND tmp.MARCIndicator2 = vt.MARCIndicator2
				LEFT JOIN dbo.BHLTitleVariant v
					ON t.TitleID = v.TitleID
					AND vt.TitleVariantTypeID = v.TitleVariantTypeID
					AND tmp.Title = v.Title
					AND tmp.TitleRemainder = v.TitleRemainder
					AND tmp.PartNumber = v.PartNumber
					AND tmp.PartName = v.PartName
		WHERE	v.TitleVariantID IS NULL

		SELECT @TitleVariantInsert = @@ROWCOUNT

		-- =======================================================================

		-- Insert new titlenote records into the production database
		INSERT INTO dbo.BHLTitleNote (TitleID, NoteTypeID, NoteText, NoteSequence)
		SELECT DISTINCT t.TitleID, nt.NoteTypeID, tmp.NoteText, tmp.NoteSequence
		FROM	#tmpTitleNote tmp INNER JOIN #tmpTitle tmpT
					ON tmp.ImportKey = tmpT.ImportKey
				INNER JOIN dbo.BHLTitle t
					ON tmpT.ProductionTitleID = t.TitleID
				INNER JOIN dbo.BHLNoteType nt
					ON tmp.MarcDataFieldTag = nt.MarcDataFieldTag
					AND tmp.MarcIndicator1 = nt.MarcIndicator1
				LEFT JOIN dbo.BHLTitleNote n
					ON t.TitleID = n.TitleID
					AND nt.NoteTypeID = n.NoteTypeID
					AND tmp.NoteText = n.NoteText
		WHERE	n.TitleNoteID IS NULL

		-- =======================================================================

		-- Insert new Keywords into the production database
		INSERT	dbo.BHLKeyword (Keyword, CreationDate, LastModifiedDate)
		SELECT	DISTINCT tmp.Keyword, tmp.ExternalCreationDate, tmp.ExternalLastModifiedDate
		FROM	#tmpKeyword2 tmp LEFT JOIN dbo.BHLKeyword k
					ON tmp.Keyword = k.Keyword
		WHERE	k.KeywordID IS NULL
		
		-- Update the temp table with the production keyword identifiers
		UPDATE	#tmpKeyword2
		SET		ProductionKeywordID = k.KeywordID
		FROM	#tmpKeyword2 tmp INNER JOIN dbo.BHLKeyword k
					ON tmp.Keyword = k.Keyword
		
		-- Update existing title keywords in the production database
		UPDATE	dbo.BHLTitleKeyword
		SET		MarcDataFieldTag = tmp.MarcDataFieldTag,
				MarcSubFieldCode = tmp.MarcSubFieldCode,
				LastModifiedDate = tmp.ExternalLastModifiedDate
		FROM	#tmpKeyword2 tmp INNER JOIN #tmpTitle tmpT
					ON tmp.ImportKey = tmpT.ImportKey
				INNER JOIN dbo.BHLTitle t
					ON tmpT.ProductionTitleID = t.TitleID
				INNER JOIN dbo.BHLTitleKeyword tk
					ON tk.TitleID = t.TitleID
					AND tk.KeywordID = tmp.ProductionKeywordID
		WHERE	tk.MarcDataFieldTag <> tmp.MarcDataFieldTag
		OR		tk.MarcSubFieldCode <> tmp.MarcSubFieldCode

		SELECT @TitleKeywordUpdate = @@ROWCOUNT

		-- Insert new title keywords into the production database
		INSERT INTO dbo.BHLTitleKeyword (TitleID, KeywordID, MarcDataFieldTag, MarcSubFieldCode,
			CreationDate, LastModifiedDate)
		SELECT DISTINCT t.TitleID, tmp.ProductionKeywordID, tmp.MarcDataFieldTag, tmp.MarcSubFieldCode,
				tmp.ExternalCreationDate, tmp.ExternalLastModifiedDate
		FROM	#tmpKeyword2 tmp INNER JOIN (	SELECT	ImportKey, 
														CASE WHEN RIGHT(TagText, 1) = '.'
														THEN LEFT(TagText, LEN(TagText) - 1)
														ELSE TagText
														END TagText, 
													MIN(TitleTagID) AS TitleTagID
												FROM	dbo.TitleTag
												GROUP BY
													ImportKey, 
													CASE WHEN RIGHT(TagText, 1) = '.'
													THEN LEFT(TagText, LEN(TagText) - 1)
													ELSE TagText
													END
											) x
					ON tmp.KeywordID = x.TitleTagID
				INNER JOIN #tmpTitle tmpT
					ON tmp.ImportKey = tmpT.ImportKey
				INNER JOIN dbo.BHLTitle t
					ON tmpT.ProductionTitleID = t.TitleID
				LEFT JOIN dbo.BHLTitleKeyword tk
					ON t.TitleID = tk.TitleID
					AND tmp.ProductionKeywordID = tk.KeywordID
		WHERE	tk.TitleKeywordID IS NULL
		
		SELECT @TitleKeywordInsert = @@ROWCOUNT

		-- =======================================================================

		-- Insert new titlelanguage records into the production database
		INSERT INTO dbo.BHLTitleLanguage (TitleID, LanguageCode, CreationDate)
		SELECT DISTINCT t.TitleID, UPPER(tmp.LanguageCode), GETDATE()
		FROM	#tmpTitleLanguage tmp INNER JOIN #tmpTitle tmpT
					ON tmp.ImportKey = tmpT.ImportKey
				INNER JOIN dbo.BHLTitle t
					ON tmpT.ProductionTitleID = t.TitleID
				LEFT JOIN dbo.BHLTitleLanguage tl
					ON t.TitleID = tl.TitleID
					AND tmp.LanguageCode = tl.LanguageCode
		WHERE	tl.TitleLanguageID IS NULL

		-- =======================================================================

		-- Insert new items into the production database
		INSERT INTO dbo.BHLItem (PrimaryTitleID, BarCode, MARCItemID, CallNumber, 
			Volume, LanguageCode, Sponsor, ItemDescription, ScannedBy, 
			PDFSize, VaultID, Note, ItemStatusID, ItemSourceID, ScanningUser, 
			ScanningDate, [Year], IdentifierBib, ZQuery, FileRootFolder, 
			LicenseUrl, Rights, DueDiligence, CopyrightStatus, CopyrightRegion,
			CopyrightComment, CopyrightEvidence, CopyrightEvidenceOperator,
			CopyrightEvidenceDate,
			PaginationCompleteUserID, PaginationCompleteDate, PaginationStatusID, 
			PaginationStatusUserID, PaginationStatusDate, LastPageNameLookupDate, 
			CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID,
			EndYear, StartVolume, EndVolume, StartIssue, EndIssue, StartNumber, 
			EndNumber, StartSeries, EndSeries, StartPart, EndPart, VolumeReviewed)
		SELECT	t.TitleID, tmp.BarCode, 
				tmp.MARCItemID, tmp.CallNumber, tmp.Volume, 
				UPPER(tmp.LanguageCode), tmp.Sponsor, tmp.ItemDescription, tmp.ScannedBy, 
				tmp.PDFSize, tmp.VaultID, tmp.Note, tmp.ItemStatusID, 
				isis.BHLItemSourceID, tmp.ScanningUser, tmp.ScanningDate, 
				tmp.[Year], tmp.IdentifierBib, tmp.ZQuery, tmp.MARCBibID,
				tmp.LicenseUrl, tmp.Rights, tmp.DueDiligence, tmp.CopyrightStatus, 
				tmp.CopyrightRegion, tmp.CopyrightComment, tmp.CopyrightEvidence, 
				tmp.CopyrightEvidenceOperator, tmp.CopyrightEvidenceDate,
				tmp.PaginationCompleteUserID, tmp.PaginationCompleteDate, 
				tmp.PaginationStatusID, tmp.PaginationStatusUserID, 
				tmp.PaginationStatusDate, tmp.LastPageNameLookupDate, 
				tmp.ExternalCreationDate, tmp.ExternalLastModifiedDate, 
				tmp.ExternalCreationUser, tmp.ExternalLastModifiedUser,
				tmp.EndYear, tmp.StartVolume, tmp.EndVolume, tmp.StartIssue, 
				tmp.EndIssue, tmp.StartNumber, tmp.EndNumber, tmp.StartSeries, 
				tmp.EndSeries, tmp.StartPart, tmp.EndPart, 0
		FROM	#tmpItem tmp INNER JOIN #tmpTitle tmpT
					ON tmp.ImportKey = tmpT.ImportKey
				INNER JOIN dbo.BHLTitle t
					ON tmpT.ProductionTitleID = t.TitleID
				LEFT JOIN dbo.BHLItem i
					ON tmp.BarCode = i.BarCode
				LEFT JOIN dbo.ImportSourceItemSource isis
					ON tmp.ImportSourceID = isis.ImportSourceID
		WHERE	i.ItemID IS NULL

		SELECT @ItemInsert = @@ROWCOUNT

		-- Insert title->item relationships into the production TitleItem table
		INSERT INTO dbo.BHLTitleItem (TitleID, ItemID, ItemSequence)
		SELECT DISTINCT t.TitleID, i.ItemID, tmp.ItemSequence
		FROM	#tmpItem tmp INNER JOIN #tmpTitle tmpT
					ON tmp.ImportKey = tmpT.ImportKey
				INNER JOIN dbo.BHLTitle t 
					ON tmpT.ProductionTitleID = t.TitleID
				INNER JOIN dbo.BHLItem i
					ON tmp.BarCode = i.BarCode
				LEFT JOIN dbo.BHLTitleItem ti
					ON t.TitleID = ti.TitleID
					AND i.ItemID = ti.ItemID
		WHERE	ti.TitleItemID IS NULL

		SELECT @TitleItemInsert = @@ROWCOUNT

		-- Make sure the auto-assigned ItemSequence values are unique.
		-- Calculate the ItemSequence by ordering each title by the TitleItemID.
		UPDATE	dbo.BHLTitleItem
		SET		ItemSequence = x.Sequence
		FROM	dbo.BHLTitleItem ti
				INNER JOIN (SELECT	(ROW_NUMBER() OVER (PARTITION BY t.TitleID 
														ORDER BY ti2.TitleItemID)) + 9999 AS Sequence, 
									i.ItemID,
									t.TitleID
							FROM	#tmpItem tmp INNER JOIN #tmpTitle tmpT
										ON tmp.ImportKey = tmpT.ImportKey
									INNER JOIN dbo.BHLTitle t
										ON tmpT.ProductionTitleID = t.TitleID
									INNER JOIN dbo.BHLTitleItem ti2
										ON t.TitleID = ti2.TitleID
									INNER JOIN dbo.BHLItem i
										ON ti2.ItemID = i.ItemID
							WHERE	tmp.ImportSourceID = 1
							AND		ti2.ItemSequence >= 10000
							) x
					ON ti.ItemID = x.ItemID
					AND ti.TitleID = x.TitleID

		-- =======================================================================

		-- Insert new iteminstitution records into the production database
		DECLARE @HoldingInstitutionRoleID int
		DECLARE @ScanningInstitutionRoleID int
		DEClARE @RightsHolderRoleID int
		SELECT	@HoldingInstitutionRoleID = InstitutionRoleID FROM dbo.BHLInstitutionRole WHERE InstitutionRoleName = 'Holding Institution'
		SELECT	@ScanningInstitutionRoleID = InstitutionRoleID FROM dbo.BHLInstitutionRole WHERE InstitutionRoleName = 'Scanning Institution'
		SELECT	@RightsHolderRoleID = InstitutionRoleID FROM dbo.BHLInstitutionRole WHERE InstitutionRoleName = 'Rights Holder'

		-- Insert ItemInstitution records for each role
		INSERT	dbo.BHLItemInstitution (ItemID, InstitutionCode, InstitutionRoleID)
		SELECT	i.ItemID, tmp.InstitutionCode, @HoldingInstitutionRoleID
		FROM	#tmpItem tmp INNER JOIN dbo.BHLItem i ON tmp.BarCode = i.BarCode
		WHERE	tmp.InstitutionCode IS NOT NULL

		INSERT	dbo.BHLItemInstitution (ItemID, InstitutionCode, InstitutionRoleID)
		SELECT	i.ItemID, tmp.ScanningInstitutionCode, @ScanningInstitutionRoleID
		FROM	#tmpItem tmp INNER JOIN dbo.BHLItem i ON tmp.BarCode = i.BarCode
		WHERE	tmp.ScanningInstitutionCode IS NOT NULL

		INSERT	dbo.BHLItemInstitution (ItemID, InstitutionCode, InstitutionRoleID)
		SELECT	i.ItemID, tmp.RightsHolderCode, @RightsHolderRoleID
		FROM	#tmpItem tmp INNER JOIN dbo.BHLItem i ON tmp.BarCode = i.BarCode
		WHERE	tmp.RightsHolderCode IS NOT NULL

		-- =======================================================================

		-- Add Titles/Items to collections with Auto-Add criteria

		-- Find the new items that match the auto-add criteria for at least one item-based collection.
		INSERT	dbo.BHLItemCollection (ItemID, CollectionID)
		SELECT	x.ItemID, x.CollectionID
		FROM	(	-- Get a list of all new items and the collections they should be attached to 
					SELECT	i.ItemID, c.CollectionID
					FROM	#tmpItem tmp INNER JOIN dbo.BHLItem i
								ON tmp.BarCode = i.BarCode
							INNER JOIN dbo.BHLCollection c
								ON (tmp.InstitutionCode = c.InstitutionCode AND i.LanguageCode = c.LanguageCode)
								OR (tmp.InstitutionCode = c.InstitutionCode AND c.LanguageCode IS NULL)
								OR (c.InstitutionCode IS NULL AND i.LanguageCode = c.LanguageCode)
					WHERE	c.Active = 1
					AND		c.CanContainItems = 1) x
		LEFT JOIN (	-- Get a list of items already in collections
					SELECT	ic.ItemID, c.CollectionID
					FROM	dbo.BHLItemCollection ic INNER JOIN dbo.BHLCollection c
								ON ic.CollectionID = c.CollectionID
					WHERE	c.Active = 1
					AND		c.CanContainItems = 1) y
						ON x.ItemID = y.ItemID
						AND x.CollectionID = y.CollectionID
		WHERE	y.ItemID IS NULL	-- Only select the items not already in the collections

		-- Find titles related to new items that match the auto-add criteria for at least one title-based collection.
		INSERT	dbo.BHLTitleCollection (TitleID, CollectionID)
		SELECT	x.TitleID, x.CollectionID
		FROM	(	-- Get a list of all titles associated with new items and the collections they should be attached to 
					SELECT DISTINCT 
							t.TitleID, c.CollectionID
					FROM	#tmpItem tmp INNER JOIN dbo.BHLItem i
								ON tmp.BarCode = i.BarCode
							INNER JOIN dbo.BHLTitleItem ti
								ON i.ItemID = ti.ItemID
							INNER JOIN dbo.BHLTitle t
								ON ti.TitleID = t.TitleID
							INNER JOIN dbo.BHLCollection c
								ON (tmp.InstitutionCode = c.InstitutionCode AND i.LanguageCode = c.LanguageCode)
								OR (tmp.InstitutionCode = c.InstitutionCode AND c.LanguageCode IS NULL)
								OR (c.InstitutionCode IS NULL AND i.LanguageCode = c.LanguageCode)
					WHERE	c.Active = 1
					AND		c.CanContainTitles = 1) x
		LEFT JOIN (	-- Get a list of titles already in collections
					SELECT	tc.TitleID, c.CollectionID
					FROM	dbo.BHLTitleCollection tc INNER JOIN dbo.BHLCollection c
								ON tc.CollectionID = c.CollectionID
					WHERE	c.Active = 1
					AND		c.CanContainTitles = 1) y
						ON x.TitleID = y.TitleID
						AND x.CollectionID = y.CollectionID
		WHERE	y.TitleID IS NULL	-- Only select the titles not already in the collections

		-- =======================================================================

		-- Insert new itemlanguage records into the production database
		INSERT INTO dbo.BHLItemLanguage (ItemID, LanguageCode, CreationDate)
		SELECT DISTINCT i.ItemID, UPPER(tmp.LanguageCode), GETDATE()
		FROM	#tmpItemLanguage tmp INNER JOIN dbo.BHLItem i
					ON tmp.BarCode = i.BarCode
				LEFT JOIN dbo.BHLItemLanguage il
					ON i.ItemID = il.ItemID
					AND tmp.LanguageCode = il.LanguageCode
		WHERE	il.ItemLanguageID IS NULL

		-- =======================================================================

		-- Insert the new pages into the production database
		INSERT INTO dbo.BHLPage (ItemID, FileNamePrefix, SequenceOrder, PageDescription, 
			Illustration, Note, FileSize_Temp, FileExtension, Active, Year, Series, 
			Volume, Issue, ExternalURL, AltExternalURL, IssuePrefix, LastPageNameLookupDate, 
			PaginationUserID, PaginationDate, CreationDate, LastModifiedDate, 
			CreationUserID, LastModifiedUserID)
		SELECT	i.ItemID, t.FileNamePrefix, t.SequenceOrder, t.PageDescription, 
				t.Illustration, t.Note, t.FileSize_Temp, t.FileExtension, t.Active, 
				t.Year, t.Series, t.Volume, t.Issue, t.ExternalURL, t.AltExternalURL,
				t.IssuePrefix, t.LastPageNameLookupDate, t.PaginationUserID, 
				t.PaginationDate, t.ExternalCreationDate, t.ExternalLastModifiedDate, 
				t.ExternalCreationUser, t.ExternalLastModifiedUser
		FROM	#tmpPage t INNER JOIN dbo.BHLItem i
					ON t.BarCode = i.BarCode
				LEFT JOIN dbo.BHLPage p
					ON i.ItemID = p.ItemID
					AND t.FileNamePrefix = p.FileNamePrefix
		WHERE	p.PageID IS NULL

		SELECT @PageInsert = @@ROWCOUNT

		-- =======================================================================

		-- Insert new indicated pages into the production database
		INSERT INTO dbo.BHLIndicatedPage (PageID, Sequence, PagePrefix,
			PageNumber, Implied, CreationDate, LastModifiedDate, CreationUserID, 
			LastModifiedUserID)
		SELECT	p.PageID, t.Sequence, t.PagePrefix, t.PageNumber, t.Implied,
				t.ExternalCreationDate, t.ExternalLastModifiedDate, 
				t.ExternalCreationUser, t.ExternalLastModifiedUser
		FROM	#tmpIndicatedPage t INNER JOIN dbo.BHLItem i
					ON t.BarCode = i.BarCode
				INNER JOIN dbo.BHLPage p
					ON i.ItemID = p.ItemID
					AND t.FileNamePrefix = p.FileNamePrefix
					AND t.SequenceOrder = p.SequenceOrder
				LEFT JOIN dbo.BHLIndicatedPage ip
					ON p.PageID = ip.PageID
					AND t.Sequence = ip.Sequence
		WHERE	ip.PageID IS NULL

		SELECT @IndicatedPageInsert = @@ROWCOUNT

		-- =======================================================================

		-- Insert new page_pagetype records into the production database
		INSERT INTO dbo.BHLPagePageType (PageID, PageTypeID, Verified,
			CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
		SELECT	p.PageID, t.PageTypeID, t.Verified, t.ExternalCreationDate, 
				t.ExternalLastModifiedDate, t.ExternalCreationUser, 
				t.ExternalLastModifiedUser
		FROM	#tmpPage_PageType t INNER JOIN dbo.BHLItem i
					ON t.BarCode = i.BarCode
				INNER JOIN dbo.BHLPage p
					ON i.ItemID = p.ItemID
					AND t.FileNamePrefix = p.FileNamePrefix
					AND t.SequenceOrder = p.SequenceOrder
				LEFT JOIN dbo.BHLPagePageType ppt
					ON p.PageID = ppt.PageID
					AND t.PageTypeID = ppt.PageTypeID
		WHERE	ppt.PageID IS NULL
				
		SELECT @PagePageTypeInsert = @@ROWCOUNT

		-- =======================================================================

		-- Add the thumbnail pageid to the just-inserted item		
		-- NO LONGER NEEDED - APRIL 10, 2017
		-- EXEC dbo.ItemUpdateThumbnailPageID @BarCode

		-- =======================================================================
		DECLARE @StatusComplete INT
		SET @StatusComplete = 20
		SET @ProductionDate = GETDATE()

		-- Update the statuses of the data just loaded into production
		UPDATE	dbo.Title
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.Title t INNER JOIN #tmpTitle tmp ON t.TitleID = tmp.TitleID

		UPDATE	dbo.Creator
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.Creator c INNER JOIN #tmpCreator t ON c.CreatorID = t.CreatorID

		UPDATE	dbo.Title_Creator
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.Title_Creator c INNER JOIN #tmpTitle_Creator t ON c.TitleCreatorID = t.TitleCreatorID

		UPDATE	dbo.TitleTag
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.TitleTag tt INNER JOIN #tmpKeyword t ON tt.TitleTagID = t.KeywordID

		UPDATE	dbo.Title_TitleIdentifier
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.Title_TitleIdentifier tti INNER JOIN #tmpTitle_TitleIdentifier tmp
					ON  tti.Title_TitleIdentifierID = tmp.Title_TitleIdentifierID

		UPDATE	dbo.TitleAssociation
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.TitleAssociation a INNER JOIN #tmpTitleAssociation t
					ON a.TitleAssociationID = t.TitleAssociationID

		UPDATE	dbo.TitleAssociation_TitleIdentifier
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.TitleAssociation_TitleIdentifier a INNER JOIN #tmpTitleAssociation_TitleIdentifier t
					ON a.TitleAssociation_TitleIdentifierID = t.TitleAssociation_TitleIdentifierID

		UPDATE	dbo.TitleVariant
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.TitleVariant v INNER JOIN #tmpTitleVariant t
					ON v.TitleVariantID = t.TitleVariantID

		UPDATE	dbo.TitleNote
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.TitleNote n INNER JOIN #tmpTitleNote t
					ON n.TitleNoteID = t.TitleNoteID

		UPDATE	dbo.TitleLanguage
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.TitleLanguage l INNER JOIN #tmpTitleLanguage t
					ON l.TitleLanguageID = t.TitleLanguageID

		UPDATE	dbo.Item
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.Item i INNER JOIN #tmpItem t ON i.ItemID = t.ItemID

		UPDATE	dbo.ItemLanguage
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.ItemLanguage l INNER JOIN #tmpItemLanguage t
					ON l.ItemLanguageID = t.ItemLanguageID

		UPDATE	dbo.Page
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.Page p INNER JOIN #tmpPage t ON p.PageID = t.PageID

		UPDATE	dbo.IndicatedPage
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.IndicatedPage ip INNER JOIN #tmpIndicatedPage t ON ip.IndicatedPageID = t.IndicatedPageID

		UPDATE	dbo.Page_PageType
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.Page_PageType ppt INNER JOIN #tmpPage_PageType t ON ppt.PagePageTypeID = t.PagePageTypeID

		-- =======================================================================

		COMMIT TRAN

		-- Log results of import
		INSERT INTO dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult,
			TitleInsert, TitleUpdate, CreatorInsert, CreatorUpdate,
			TitleCreatorInsert, TitleCreatorUpdate, TitleTagInsert, TitleTagUpdate,
			TitleTitleIdentifierInsert, TitleTitleIdentifierUpdate,
			TitleAssociationInsert, TitleAssociationTitleIdentifierInsert,
			TitleVariantInsert, ItemInsert, ItemUpdate, TitleItemInsert, PageInsert, PageUpdate,
			IndicatedPageInsert, IndicatedPageUpdate, PagePageTypeInsert, PagePageTypeUpdate,
			PageNameInsert, PageNameUpdate)
		VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 
			ISNULL(@TitleInsert, 0), ISNULL(@TitleUpdate, 0), ISNULL(@CreatorInsert, 0), 
			ISNULL(@CreatorUpdate, 0), ISNULL(@TitleCreatorInsert, 0), 
			ISNULL(@TitleCreatorUpdate, 0), ISNULL(@TitleKeywordInsert, 0), 
			ISNULL(@TitleKeywordUpdate, 0), ISNULL(@TitleIdentifierInsert, 0),
			ISNULL(@TitleIdentifierUpdate, 0),	
			ISNULL(@TitleAssociationInsert, 0), ISNULL(@TitleAssociationTitleIdentifierInsert, 0),
			ISNULL(@TitleVariantInsert, 0), ISNULL(@ItemInsert, 0), ISNULL(@ItemUpdate, 0), 
			ISNULL(@TitleItemInsert, 0), ISNULL(@PageInsert, 0), ISNULL(@PageUpdate, 0), 
			ISNULL(@IndicatedPageInsert, 0), ISNULL(@IndicatedPageUpdate, 0), 
			ISNULL(@PagePageTypeInsert, 0), ISNULL(@PagePageTypeUpdate, 0), 
			ISNULL(@PageNameInsert, 0), ISNULL(@PageNameUpdate, 0))

		SELECT 1 AS Result
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK TRAN

		-- Record the error
		INSERT INTO dbo.ImportError (KeyType, KeyValue, ErrorDate, Number, Severity, 
			State, [Procedure], Line, [Message])
		SELECT	'', @BarCode, GETDATE(), ERROR_NUMBER(), ERROR_SEVERITY(),
			ERROR_STATE(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE()

		-- Log results of import
		INSERT INTO dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult,
			TitleInsert, TitleUpdate, CreatorInsert, CreatorUpdate,
			TitleCreatorInsert, TitleCreatorUpdate, TitleTagInsert, TitleTagUpdate,
			TitleTitleIdentifierInsert, TitleTitleIdentifierUpdate,
			TitleAssociationInsert, TitleAssociationTitleIdentifierInsert,
			TitleVariantInsert, ItemInsert, ItemUpdate, TitleItemInsert, PageInsert, PageUpdate,
			IndicatedPageInsert, IndicatedPageUpdate, PagePageTypeInsert, PagePageTypeUpdate,
			PageNameInsert, PageNameUpdate)
		VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Error', 
			ISNULL(@TitleInsert, 0), ISNULL(@TitleUpdate, 0), ISNULL(@CreatorInsert, 0), 
			ISNULL(@CreatorUpdate, 0), ISNULL(@TitleCreatorInsert, 0), 
			ISNULL(@TitleCreatorUpdate, 0), ISNULL(@TitleKeywordInsert, 0), 
			ISNULL(@TitleKeywordUpdate, 0), ISNULL(@TitleIdentifierInsert, 0),
			ISNULL(@TitleIdentifierUpdate, 0),	
			ISNULL(@TitleAssociationInsert, 0), ISNULL(@TitleAssociationTitleIdentifierInsert, 0),
			ISNULL(@TitleVariantInsert, 0), ISNULL(@ItemInsert, 0), ISNULL(@ItemUpdate, 0), 
			ISNULL(@TitleItemInsert, 0), ISNULL(@PageInsert, 0), ISNULL(@PageUpdate, 0), 
			ISNULL(@IndicatedPageInsert, 0), ISNULL(@IndicatedPageUpdate, 0), 
			ISNULL(@PagePageTypeInsert, 0), ISNULL(@PagePageTypeUpdate, 0), 
			ISNULL(@PageNameInsert, 0), ISNULL(@PageNameUpdate, 0))

		SELECT 0 AS Result
	END CATCH

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================

	/*
	-- DEBUGGING OUTPUT
	SELECT * FROM #tmpTitle
	SELECT * FROM #tmpKeyword
	SELECT * FROM #tmpTitle_TitleIdentifier
	SELECT * FROM #tmpTitleAssociation
	SELECT * FROM #tmpTitleAssociation_TitleIdentifier
	SELECT * FROM #tmpTitleVariant
	SELECT * FROM #tmpTitleNote
	SELECT * FROM #tmpCreator
	SELECT * FROM #tmpTitle_Creator
	SELECT * FROM #tmpItem
	SELECT * FROM #tmpPage
	SELECT * FROM #tmpIndicatedPage
	SELECT * FROM #tmpPage_PageType
	*/

	-- Clean up temp tables
	DROP TABLE #tmpTitle
	DROP TABLE #tmpKeyword
	DROP TABLE #tmpKeyword2
	DROP TABLE #tmpTitle_TitleIdentifier
	DROP TABLE #tmpTitleAssociation
	DROP TABLE #tmpTitleAssociation_TitleIdentifier
	DROP TABLE #tmpTitleVariant
	DROP TABLE #tmpTitleNote
	DROP TABLE #tmpCreator
	DROP TABLE #tmpTitle_Creator
	DROP TABLE #tmpItem
	DROP TABLE #tmpPage
	DROP TABLE #tmpIndicatedPage
	DROP TABLE #tmpPage_PageType
END TRY
BEGIN CATCH
	-- Record the error
	INSERT INTO dbo.ImportError (KeyType, KeyValue, ErrorDate, Number, Severity, 
		State, [Procedure], Line, [Message])
	SELECT	'BarCode', @BarCode, GETDATE(), ERROR_NUMBER(), ERROR_SEVERITY(),
		ERROR_STATE(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE()

	-- Log results of import
	INSERT INTO dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult,
		TitleInsert, TitleUpdate, CreatorInsert, CreatorUpdate,
		TitleCreatorInsert, TitleCreatorUpdate, TitleTagInsert, TitleTagUpdate,
		TitleTitleIdentifierInsert, TitleTitleIdentifierUpdate,
		TitleAssociationInsert, TitleAssociationTitleIdentifierInsert,
		TitleVariantInsert, ItemInsert, ItemUpdate, TitleItemInsert, PageInsert, PageUpdate,
		IndicatedPageInsert, IndicatedPageUpdate, PagePageTypeInsert, PagePageTypeUpdate,
		PageNameInsert, PageNameUpdate)
	VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Error', 
		ISNULL(@TitleInsert, 0), ISNULL(@TitleUpdate, 0), ISNULL(@CreatorInsert, 0), 
		ISNULL(@CreatorUpdate, 0), ISNULL(@TitleCreatorInsert, 0), 
		ISNULL(@TitleCreatorUpdate, 0), ISNULL(@TitleKeywordInsert, 0), 
		ISNULL(@TitleKeywordUpdate, 0), ISNULL(@TitleIdentifierInsert, 0),
		ISNULL(@TitleIdentifierUpdate, 0),	
		ISNULL(@TitleAssociationInsert, 0), ISNULL(@TitleAssociationTitleIdentifierInsert, 0),
		ISNULL(@TitleVariantInsert, 0), ISNULL(@ItemInsert, 0), ISNULL(@ItemUpdate, 0), 
		ISNULL(@TitleItemInsert, 0), ISNULL(@PageInsert, 0), ISNULL(@PageUpdate, 0), 
		ISNULL(@IndicatedPageInsert, 0), ISNULL(@IndicatedPageUpdate, 0), 
		ISNULL(@PagePageTypeInsert, 0), ISNULL(@PagePageTypeUpdate, 0), 
		ISNULL(@PageNameInsert, 0), ISNULL(@PageNameUpdate, 0))

	SELECT 0 AS Result
END CATCH

SET NOCOUNT OFF

END
