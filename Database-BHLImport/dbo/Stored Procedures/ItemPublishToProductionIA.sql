CREATE PROCEDURE [dbo].[ItemPublishToProductionIA]

@BarCode nvarchar(200) = NULL

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
DECLARE @RowCount int
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

	CREATE TABLE #tmpTitle_Creator (
		[TitleCreatorID] [int] NOT NULL,
		[ProductionAuthorID] [int] NULL,
		[ProductionAuthorNameID] [int] NULL,
		[ImportSourceID] [int] NOT NULL,
		[CreatorName] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
		[MARCDataFieldTag] [nvarchar](3) NULL,
		[MARCCreator_a] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_b] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_c] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_d] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_e] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_q] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[MARCCreator_t] [nvarchar](450) COLLATE SQL_Latin1_General_CP1_CI_AI NULL,
		[DOB] [nvarchar](50) NULL,
		[DOD] [nvarchar](50) NULL,
		[CreatorRoleTypeID] [int] NOT NULL,
		[SequenceOrder] [smallint] NOT NULL,
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
		[ProductionBookID] [int] NULL,
		[ProductionSegmentID] [int] NULL,
		[ImportSourceID] [int] NOT NULL,
		[MARCBibID] [nvarchar](50) NOT NULL,
		[BarCode] [nvarchar](200) NOT NULL,
		[ItemSequence] [smallint] NULL,
		[MaxExistingItemSequence] [smallint] NULL DEFAULT(0),	-- highest existing production sequence for this barcode
		[MARCItemID] [nvarchar](200) NULL,
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
		[EndPart] [nvarchar](10) NOT NULL,
		[PageProgression] [nvarchar](10) NOT NULL,
		[VirtualVolume] [nvarchar](100) NOT NULL,
		[VirtualVolumeKey] [nvarchar](100) NOT NULL,
		[VirtualVolumeBarCode] [nvarchar](200) NULL,
		[VirtualTitleID] [int] NULL,
		[Summary] [nvarchar](max) NULL,
		[SegmentGenreID] [int] NULL,
		[PublicationDetails] [nvarchar](400) NULL,
		[PublisherName] [nvarchar](250) NULL,
		[Issue] [nvarchar](100) NULL,
		[SegmentDate] [nvarchar](20) NULL,
		[StartPage] [nvarchar](20) NULL,
		[EndPage] [nvarchar](20) NULL,
		[Title] [nvarchar](2000) NULL,
		[SortTitle] [nvarchar](2000) NULL,
		[ContainerTitle] [nvarchar](2000) NULL
		)

	CREATE TABLE #tmpItemCreator (
		[ItemCreatorID] [int] NOT NULL,
		[ProductionAuthorID] [int] NULL,
		[ProductionAuthorNameID] [int] NULL,
		[ImportSourceID] [int] NOT NULL,
		[BarCode] [nvarchar](200) NOT NULL,
		[CreatorName] [nvarchar](255) NOT NULL,
		[CreatorRoleTypeID] [int] NOT NULL,
		[SequenceOrder] [smallint] NULL
	)

	CREATE TABLE #tmpItemCreatorIdentifier (
		[ItemCreatorIdentifierID] [int] NOT NULL,
		[BarCode] [nvarchar](200) NOT NULL,
		[SequenceOrder] [smallint] NULL,
		[IdentifierID] [int] NOT NULL,
		[IdentifierValue] [nvarchar](125) NOT NULL
	)

	CREATE TABLE #tmpItemIdentifier (
		[ItemIdentifierID] [int] NOT NULL,
		[BarCode] [nvarchar](200) NOT NULL,
		[IdentifierName] [nvarchar](40) NOT NULL,
		[IdentifierValue] [nvarchar](125) NOT NULL
	)

	CREATE TABLE #tmpItemKeyword (
		[ItemKeywordID] [int] NOT NULL,
		[BarCode] [nvarchar](200) NOT NULL,
		[Keyword] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL
	)

	CREATE TABLE #tmpItemLanguage
	(
		[ItemLanguageID] [int] NOT NULL,
		[ImportSourceID] [int] NULL,
		[BarCode] [nvarchar](200) NOT NULL,
		[LanguageCode] [nvarchar](10) NOT NULL DEFAULT('')
	)

	CREATE TABLE #tmpPage (
		[PageID] [int] NOT NULL,
		[BarCode] [nvarchar](200) NOT NULL,
		[FileNamePrefix] [nvarchar](200) NOT NULL,
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
		[BarCode] [nvarchar](200) NOT NULL,
		[FileNamePrefix] [nvarchar](200) NOT NULL,
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
		[BarCode] [nvarchar](200) NOT NULL,
		[FileNamePrefix] [nvarchar](200) NOT NULL,
		[SequenceOrder] [int] NULL,
		[PageTypeID] [int] NOT NULL,
		[Verified] [bit] NOT NULL,
		[ExternalCreationDate] [datetime] NULL,
		[ExternalLastModifiedDate] [datetime] NULL,
		[ExternalCreationUser] [int] NULL,
		[ExternalLastModifiedUser] [int] NULL
		)

	CREATE TABLE #tmpSegment (
		[SegmentID] [int] NOT NULL,
		[ImportSourceID] [int] NOT NULL,
		[BarCode] nvarchar(200) NOT NULL,
		[SequenceOrder] smallint NOT NULL DEFAULT ((1)),
		[SegmentStatusID] [int] NOT NULL,
		[SegmentGenreID] [int] NOT NULL,
		[Title] [nvarchar](2000) NOT NULL DEFAULT (''),
		[TranslatedTitle] [nvarchar](2000) NOT NULL DEFAULT (''),
		[SortTitle] [nvarchar](2000) NOT NULL DEFAULT (''),
		[ContainerTitle] [nvarchar](2000) NOT NULL DEFAULT (''),
		[PublicationDetails] [nvarchar](400) NOT NULL DEFAULT (''),
		[PublisherName] [nvarchar](250) NOT NULL DEFAULT (''),
		[PublisherPlace] [nvarchar](150) NOT NULL DEFAULT (''),
		[Notes] [nvarchar](max) NOT NULL DEFAULT (''),
		[Summary] [nvarchar](max) NOT NULL DEFAULT (''),
		[Volume] [nvarchar](100) NOT NULL DEFAULT (''),
		[Series] [nvarchar](100) NOT NULL DEFAULT (''),
		[Issue] [nvarchar](100) NOT NULL,
		[Edition] [nvarchar](400) NOT NULL DEFAULT (''),
		[Date] [nvarchar](20) NOT NULL DEFAULT (''),
		[PageRange] [nvarchar](50) NOT NULL DEFAULT (''),
		[StartPageNumber] [nvarchar](20) NOT NULL DEFAULT (''),
		[EndPageNumber] [nvarchar](20) NOT NULL DEFAULT (''),
		[StartPageID] [int] NULL,
		[InstitutionCode] [nvarchar](10) NULL,
		[LanguageCode] [nvarchar](10) NULL,
		[Url] [nvarchar](200) NOT NULL DEFAULT (''),
		[DownloadUrl] [nvarchar](200) NOT NULL DEFAULT (''),
		[RightsStatus] [nvarchar](500) NOT NULL DEFAULT (''),
		[RightsStatement] [nvarchar](500) NOT NULL DEFAULT (''),
		[LicenseName] [nvarchar](200) NOT NULL DEFAULT (''),
		[LicenseUrl] [nvarchar](200) NOT NULL DEFAULT ('')
	)

	CREATE TABLE #tmpSegmentPage (
		[SegmentPageID] int NOT NULL,
		[BarCode] nvarchar(200) NOT NULL,
		[SegmentSequenceOrder] smallint NOT NULL,
		[PageSequenceOrder] int NOT NULL,
		[SegmentPageSequenceOrder] int NOT NULL
	)

	CREATE TABLE #tmpSegmentIdentifier(
		[SegmentIdentifierID] [int] NOT NULL,
		[BarCode] nvarchar(200) NOT NULL,
		[SegmentSequenceOrder] smallint NOT NULL,
		[IdentifierName] [nvarchar](40) NOT NULL,
		[IdentifierValue] [nvarchar](125) NOT NULL
		)

	CREATE TABLE #tmpSegmentAuthor (
		[SegmentAuthorID] [int] NOT NULL,
		[BarCode] [nvarchar](200) NOT NULL DEFAULT (''),
		[SegmentSequenceOrder] [int] NOT NULL,
		[SequenceOrder] [int] NOT NULL,
		[FullName] [nvarchar](300) NOT NULL DEFAULT(''),
		[LastName] [nvarchar](150) NOT NULL DEFAULT (''),
		[FirstName] [nvarchar](150) NOT NULL DEFAULT (''),
		[StartDate] [nvarchar](25) NOT NULL DEFAULT (''),
		[EndDate] [nvarchar](25) NOT NULL DEFAULT (''),
		[ProductionAuthorID] [int] NULL,
		[ProductionAuthorNameID] [int] NULL
	)

	CREATE TABLE #tmpSegmentAuthorIdentifier (
		[SegmentAuthorIdentifierID] [int] NOT NULL,
		[BarCode] [nvarchar](200) NOT NULL DEFAULT (''),
		[SegmentSequenceOrder] [int] NOT NULL,
		[SequenceOrder] [int] NOT NULL,
		[ProductionIdentifierID] [int] NOT NULL,
		[IdentifierValue] [nvarchar](125) NOT NULL DEFAULT ('')
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
	SELECT	tc.TitleCreatorID,
			NULL,
			NULL,
			@ImportSourceID,
			tc.CreatorName,
			tc.MARCDataFieldTag,
			tc.MARCCreator_a,
			tc.MARCCreator_b,
			tc.MARCCreator_c,
			tc.MARCCreator_d,
			tc.MARCCreator_e,
			tc.MARCCreator_q,
			tc.MARCCreator_t,
			tc.DOB,
			tc.DOD,
			tc.CreatorRoleTypeID,
			tc.SequenceOrder,
			tc.ExternalCreationDate,
			tc.ExternalLastModifiedDate,
			tc.ExternalCreationUser,
			tc.ExternalLastModifiedUser,
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
	SELECT	t.TitleCreatorID, 
			MIN(n.AuthorID) AS ProductionAuthorID, 
			MIN(n.AuthorNameID) AS ProductionAuthorNameID
	INTO	#tmpTCUpdate
	FROM	#tmpTitle_Creator t 
			INNER JOIN dbo.BHLvwAuthorName n 
				ON dbo.fnRemoveNonAlphaNumericCharacters(t.CreatorName) = n.FullNameToken COLLATE SQL_Latin1_General_CP1_CI_AI
	WHERE	(  -- If b is blank, match records with blank Numeration/Unit values
			(ISNULL(t.MARCCreator_b, '') = '' AND ISNULL(n.Numeration, '') = '' AND ISNULL(n.Unit, '') = '') 
			OR  -- If b is not blank, find records with matching Numeration/Unit values
			(ISNULL(t.MARCCreator_b, '') <> '' AND
				(ISNULL(t.MARCCreator_b, '') = ISNULL(n.Numeration, '') COLLATE SQL_Latin1_General_CP1_CI_AI OR
				ISNULL(t.MARCCreator_b, '') = ISNULL(n.Unit, '') COLLATE SQL_Latin1_General_CP1_CI_AI))
			)
	AND		(  -- If c is blank, match records with blank Numeration/Unit values
			(ISNULL(t.MARCCreator_c, '') = '' AND ISNULL(n.Title, '') = '' AND ISNULL(n.Location, '') = '')
			OR  -- If c is not blank, find records with matching Numeration/Unit values
			(ISNULL(t.MARCCreator_c, '') <> '' AND
				(ISNULL(t.MARCCreator_c, '') = ISNULL(n.Title, '') COLLATE SQL_Latin1_General_CP1_CI_AI OR
				ISNULL(t.MARCCreator_c, '') = ISNULL(n.Location, '') COLLATE SQL_Latin1_General_CP1_CI_AI))
			)
	AND		ISNULL(dbo.fnRemoveNonNumericCharacters(t.DOB), '') = ISNULL(dbo.fnRemoveNonNumericCharacters(n.Startdate), '')
	AND		ISNULL(dbo.fnRemoveNonNumericCharacters(t.DOD), '') = ISNULL(dbo.fnRemoveNonNumericCharacters(n.EndDate), '')
	GROUP BY t.TitleCreatorID

	UPDATE	#tmpTitle_Creator
	SET		ProductionAuthorID = tu.ProductionAuthorID,
			ProductionAuthorNameID = tu.ProductionAuthorNameID
	FROM	#tmpTitle_Creator tc 
			INNER JOIN #tmpTCUpdate tu ON tc.TitleCreatorID = tu.TitleCreatorID

	DROP TABLE #tmpTCUpdate

	-- If there are still TitleCreators without Production Author IDs, try again
	-- to find a match using only name elements (no DOB/DOD).  There is a chance
	-- of a false positive match, but the large majority of matches will be
	-- correct.  Also, most records will already be matched by this point.
	IF EXISTS (SELECT TitleCreatorID FROM #tmpTitle_Creator WHERE ProductionAuthorID IS NULL)
	BEGIN
		SELECT	t.TitleCreatorID, 
				MIN(n.AuthorID) AS ProductionAuthorID, 
				MIN(n.AuthorNameID) AS ProductionAuthorNameID
		INTO	#tmpTCUpdate2
		FROM	#tmpTitle_Creator t 
				INNER JOIN dbo.BHLvwAuthorName n 
					ON dbo.fnRemoveNonAlphaNumericCharacters(t.CreatorName) = n.FullNameToken COLLATE SQL_Latin1_General_CP1_CI_AI
		WHERE	t.ProductionAuthorID IS NULL
		AND		
				(  -- If b is blank, match records with blank Numeration/Unit values
				(ISNULL(t.MARCCreator_b, '') = '' AND ISNULL(n.Numeration, '') = '' AND ISNULL(n.Unit, '') = '') 
				OR  -- If b is not blank, find records with matching Numeration/Unit values
				(ISNULL(t.MARCCreator_b, '') <> '' AND
					(ISNULL(dbo.fnRemoveNonAlphaNumericCharacters(t.MARCCreator_b), '') = ISNULL(dbo.fnRemoveNonAlphaNumericCharacters(n.Numeration), '') COLLATE SQL_Latin1_General_CP1_CI_AI OR
					ISNULL(dbo.fnRemoveNonAlphaNumericCharacters(t.MARCCreator_b), '') = ISNULL(dbo.fnRemoveNonAlphaNumericCharacters(n.Unit), '') COLLATE SQL_Latin1_General_CP1_CI_AI))
				)
		AND		(  -- If c is blank, match records with blank Numeration/Unit values
				(ISNULL(t.MARCCreator_c, '') = '' AND ISNULL(n.Title, '') = '' AND ISNULL(n.Location, '') = '')
				OR  -- If c is not blank, find records with matching Numeration/Unit values
				(ISNULL(t.MARCCreator_c, '') <> '' AND
					(ISNULL(dbo.fnRemoveNonAlphaNumericCharacters(t.MARCCreator_c), '') = ISNULL(dbo.fnRemoveNonAlphaNumericCharacters(n.Title), '') COLLATE SQL_Latin1_General_CP1_CI_AI OR
					ISNULL(dbo.fnRemoveNonAlphaNumericCharacters(t.MARCCreator_c), '') = ISNULL(dbo.fnRemoveNonAlphaNumericCharacters(n.Location), '') COLLATE SQL_Latin1_General_CP1_CI_AI))
				)
		GROUP BY t.TitleCreatorID

		UPDATE	#tmpTitle_Creator
		SET		ProductionAuthorID = tu.ProductionAuthorID,
				ProductionAuthorNameID = tu.ProductionAuthorNameID
		FROM	#tmpTitle_Creator tc 
				INNER JOIN #tmpTCUpdate2 tu ON tc.TitleCreatorID = tu.TitleCreatorID

		DROP TABLE #tmpTCUpdate2
	END

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
	-- Get Item Creators (including identifiers)

	INSERT INTO #tmpItemCreator
	SELECT	ItemCreatorID,
			NULL,
			NULL,
			@ImportSourceID,
			@BarCode,
			CreatorName,
			CreatorRoleTypeID,
			SequenceOrder
	FROM	dbo.ItemCreator
	WHERE	ImportStatusID = 10
	AND		Barcode = @BarCode
	AND		ImportSourceID = @ImportSourceID

	INSERT INTO #tmpItemCreatorIdentifier
	SELECT	ItemCreatorIdentifierID,
			@BarCode,
			SequenceOrder,
			IdentifierID,
			IdentifierValue
	FROM	dbo.ItemCreatorIdentifier
	WHERE	ImportStatusID = 10
	AND		Barcode = @BarCode
	AND		ImportSourceID = @ImportSourceID

	-- Look for production Author IDs for the selected authors
	-- First try to match identifiers
	UPDATE	#tmpItemCreator
	SET		ProductionAuthorID = bai.AuthorID,
			ProductionAuthorNameID = n.AuthorNameID
	FROM	#tmpItemCreator c INNER JOIN #tmpItemCreatorIdentifier i 
				ON c.BarCode = i.BarCode 
				AND c.SequenceOrder = i.SequenceOrder
			INNER JOIN dbo.BHLAuthorIdentifier bai
				ON i.IdentifierID = bai.IdentifierID
				AND i.IdentifierValue = bai.IdentifierValue
			INNER JOIN dbo.BHLvwAuthorName n
				ON bai.AuthorID = n.AuthorID AND n.IsPreferredName = 1

	-- Next match records on Name and blank Numeration/Unit/Title/Location values
	SELECT	t.ItemCreatorID, 
			MIN(n.AuthorID) AS ProductionAuthorID, 
			MIN(n.AuthorNameID) AS ProductionAuthorNameID
	INTO	#tmpICUpdate
	FROM	#tmpItemCreator t 
			INNER JOIN dbo.BHLvwAuthorName n 
				ON dbo.fnRemoveNonAlphaNumericCharacters(t.CreatorName) = n.FullNameToken COLLATE SQL_Latin1_General_CP1_CI_AI
	WHERE	ISNULL(n.Numeration, '') = '' 
	AND		ISNULL(n.Unit, '') = ''
	AND		ISNULL(n.Title, '') = '' 
	AND		ISNULL(n.Location, '') = ''
	AND		t.ProductionAuthorID IS NULL
	GROUP BY t.ItemCreatorID

	UPDATE	#tmpItemCreator
	SET		ProductionAuthorID = tu.ProductionAuthorID,
			ProductionAuthorNameID = tu.ProductionAuthorNameID
	FROM	#tmpItemCreator tc 
			INNER JOIN #tmpICUpdate tu ON tc.ItemCreatorID = tu.ItemCreatorID

	DROP TABLE #tmpICUpdate

	-- If a selected production author ID has been redirected to a different 
	-- author, then use that author instead.  Follow the "redirect" chain up 
	-- to ten levels.
	UPDATE	#tmpItemCreator
	SET		ProductionAuthorID = COALESCE(a10.AuthorID, a9.AuthorID, a8.AuthorID, a7.AuthorID, a6.AuthorID,
										a5.AuthorID, a4.AuthorID, a3.AuthorID, a2.AuthorID, a1.AuthorID)
	FROM	#tmpItemCreator c INNER JOIN dbo.BHLAuthor a1 ON c.ProductionAuthorID = a1.AuthorID
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
	-- Get Items

	INSERT INTO #tmpItem
	SELECT	i.[ItemID],
			NULL AS [ProductionBookID],
			NULL AS [ProductionSegmentID],
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
			i.[EndPart],
			i.[PageProgression],
			i.[VirtualVolume],
			[dbo].[fnFilterString](i.VirtualVolume, '[0-9a-zA-Z]', '') AS [VirtualVolumeKey],
			[dbo].[fnFilterString](
					'vi' + convert(varchar(20), i.VirtualTitleID) + i.VirtualVolume + convert(varchar(23), getdate(), 120),
					'[0-9a-zA-Z]', 
					'') AS [VirtualVolumeBarcode],
			i.[VirtualTitleID],
			i.[Summary],
			i.[SegmentGenreID],
			i.[PublicationDetails],
			i.[PublisherName],
			i.[Issue],
			i.[SegmentDate],
			i.[StartPage],
			i.[EndPage],
			i.[Title],
			i.[SortTitle],
			i.[ContainerTitle]
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
	-- Get Item Identifiers

	INSERT INTO #tmpItemIdentifier 
	SELECT	ItemIdentifierID,
			BarCode,
			IdentifierName,
			IdentifierValue
	FROM	dbo.ItemIdentifier
	WHERE	ImportStatusID = 10
	AND		BarCode = @BarCode
	AND		ImportSourceID = @ImportSourceID

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Item Keywords

	INSERT INTO #tmpItemKeyword
	SELECT	ItemKeywordID,
			BarCode,
			Keyword
	FROM	dbo.ItemKeyword
	WHERE	ImportStatusID = 10
	AND		Barcode = @BarCode
	AND		ImportSourceID = @ImportSourceID

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
	-- Get Segments

	INSERT INTO #tmpSegment
	SELECT	[SegmentID],
			[ImportSourceID],
			[BarCode],
			[SequenceOrder],
			[SegmentStatusID],
			[SegmentGenreID],
			[Title],
			[TranslatedTitle],
			[SortTitle],
			[ContainerTitle],
			[PublicationDetails],
			[PublisherName],
			[PublisherPlace],
			[Notes],
			[Summary],
			[Volume],
			[Series],
			[Issue],
			[Edition],
			[Date],
			[PageRange],
			[StartPageNumber],
			[EndPageNumber],
			[StartPageID],
			[InstitutionCode],
			CASE WHEN [LanguageCode] = '' THEN NULL ELSE [LanguageCode] END,
			[Url],
			[DownloadUrl],
			[RightsStatus],
			[RightsStatement],
			[LicenseName],
			[LicenseUrl]
	FROM	dbo.Segment
	WHERE	ImportStatusID = 10
	AND		ImportSourceID = @ImportSourceID
	AND		BarCode = @BarCode

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Segment Pages

	INSERT INTO #tmpSegmentPage
	SELECT	[SegmentPageID],
			[BarCode],
			[SegmentSequenceOrder],
			[PageSequenceOrder],
			ROW_NUMBER() OVER (ORDER BY PageSequenceOrder) as [SegmentPageSequenceOrder]
	FROM	dbo.SegmentPage
	WHERE	ImportStatusID = 10
	AND		ImportSourceID = @ImportSourceID
	AND		BarCode = @BarCode

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Segment Identifiers
	INSERT INTO #tmpSegmentIdentifier
	SELECT	si.[SegmentIdentifierID],
			si.[BarCode],
			si.[SegmentSequenceOrder],
			si.[IdentifierName],
			si.[IdentifierValue]
	FROM	dbo.SegmentIdentifier si
	WHERE	si.ImportStatusID = 10
	AND		si.ImportSourceID = @ImportSourceID
	AND		si.BarCode = @BarCode

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Segment Authors

	INSERT INTO #tmpSegmentAuthor
	SELECT	[SegmentAuthorID],
			[BarCode],
			[SegmentSequenceOrder],
			[SequenceOrder],
			[LastName] + ', ' + [FirstName],
			[LastName],
			[FirstName],
			[StartDate],
			[EndDate],
			[ProductionAuthorID],
			NULL
	FROM	dbo.SegmentAuthor
	WHERE	ImportStatusID = 10
	AND		ImportSourceID = @ImportSourceID
	AND		BarCode = @BarCode

	-- Look for production Author IDs for the selected authors
	-- First try to match identifiers
	UPDATE	#tmpSegmentAuthor
	SET		ProductionAuthorID = bai.AuthorID
	FROM	#tmpSegmentAuthor a INNER JOIN #tmpSegmentAuthorIdentifier i 
				ON a.BarCode = i.BarCode 
				AND a.SegmentSequenceOrder = i.SegmentSequenceOrder 
				AND a.SequenceOrder = i.SequenceOrder
			INNER JOIN dbo.BHLAuthorIdentifier bai
				ON i.ProductionIdentifierID = bai.IdentifierID
				AND i.IdentifierValue = bai.IdentifierValue
	WHERE	a.ProductionAuthorID IS NULL

	-- Next try to match names and dates
	SELECT	t.SegmentAuthorID, 
			MIN(n.AuthorID) AS ProductionAuthorID, 
			MIN(n.AuthorNameID) AS ProductionAuthorNameID
	INTO	#tmpSAUpdate
	FROM	#tmpSegmentAuthor t INNER JOIN dbo.BHLvwAuthorName n
				ON	ISNULL(dbo.fnRemoveNonNumericCharacters(t.StartDate), '') = ISNULL(dbo.fnRemoveNonNumericCharacters(n.Startdate), '')
				AND	ISNULL(dbo.fnRemoveNonNumericCharacters(t.EndDate), '') = ISNULL(dbo.fnRemoveNonNumericCharacters(n.EndDate), '')
				AND dbo.fnRemoveNonAlphaNumericCharacters(t.FullName) = n.FullNameToken COLLATE SQL_Latin1_General_CP1_CI_AI
				AND dbo.fnRemoveNonAlphaNumericCharacters(t.FirstName) = LTRIM(RTRIM(n.FirstNameToken))
				AND	dbo.fnRemoveNonAlphaNumericCharacters(t.LastName) = LTRIM(RTRIM(n.LastNameToken))
	WHERE		t.ProductionAuthorID IS NULL
	GROUP BY t.SegmentAuthorID

	UPDATE	#tmpSegmentAuthor
	SET		ProductionAuthorID = tu.ProductionAuthorID,
			ProductionAuthorNameID = tu.ProductionAuthorNameID
	FROM	#tmpSegmentAuthor sa 
			INNER JOIN #tmpSAUpdate tu ON sa.SegmentAuthorID = tu.SegmentAuthorID

	DROP TABLE #tmpSAUpdate

	-- If a selected production Author ID has been redirected to a different 
	-- author ID, then use that author ID instead.  Follow the "redirect" chain
	-- up to ten levels.
	UPDATE	#tmpSegmentAuthor
	SET		ProductionAuthorID = COALESCE(a10.AuthorID, a9.AuthorID, a8.AuthorID, a7.AuthorID, a6.AuthorID,
										a5.AuthorID, a4.AuthorID, a3.AuthorID, a2.AuthorID, a1.AuthorID)
	FROM	#tmpSegmentAuthor t 
			INNER JOIN dbo.SegmentAuthor a ON t.SegmentAuthorID = a.SegmentAuthorID
			INNER JOIN dbo.BHLAuthor a1 ON t.ProductionAuthorID = a1.AuthorID
			LEFT JOIN dbo.BHLAuthor a2 ON a1.RedirectAuthorID = a2.AuthorID
			LEFT JOIN dbo.BHLAuthor a3 ON a2.RedirectAuthorID = a3.AuthorID
			LEFT JOIN dbo.BHLAuthor a4 ON a3.RedirectAuthorID = a4.AuthorID
			LEFT JOIN dbo.BHLAuthor a5 ON a4.RedirectAuthorID = a5.AuthorID
			LEFT JOIN dbo.BHLAuthor a6 ON a5.RedirectAuthorID = a6.AuthorID
			LEFT JOIN dbo.BHLAuthor a7 ON a6.RedirectAuthorID = a7.AuthorID
			LEFT JOIN dbo.BHLAuthor a8 ON a7.RedirectAuthorID = a8.AuthorID
			LEFT JOIN dbo.BHLAuthor a9 ON a8.RedirectAuthorID = a9.AuthorID
			LEFT JOIN dbo.BHLAuthor a10 ON a9.RedirectAuthorID = a10.AuthorID
	WHERE	t.ProductionAuthorID IS NOT NULL
	AND		a.ProductionAuthorID IS NULL	-- Only do this for production IDs that were NOT user-supplied

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Segment Author Identifiers

	INSERT INTO #tmpSegmentAuthorIdentifier
	SELECT	[SegmentAuthorIdentifierID],
			[BarCode],
			[SegmentSequenceOrder],
			[SequenceOrder],
			[ProductionIdentifierID],
			[IdentifierValue]
	FROM	dbo.SegmentAuthorIdentifier
	WHERE	ImportStatusID = 10
	AND		ImportSourceID = @ImportSourceID
	AND		BarCode = @BarCode

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
	-- Resolve items and segments that are part of Virtual Items

	-- After items have been resolved, any records remaining in the #tmpItem
	-- table with a value in the VirtualTitleID/VirtualVolume columns but no
	-- value in the ProductionBookID column will result in the creation of
	-- new production Book/Item records.
	UPDATE	#tmpItem
	SET		ProductionBookID = b.BookID
	FROM	#tmpItem t
			INNER JOIN dbo.BHLBook b ON t.VirtualVolumeKey = b.VirtualVolumeKey
	WHERE	t.ProductionBookID IS NULL

	-- If the selected production Book has been redirected to a different 
	-- Book, then use that Book instead.  Follow the "redirect" chain up 
	-- to ten levels.
	UPDATE	#tmpItem
	SET		ProductionBookID = COALESCE(b10.BookID, b9.BookID, b8.BookID, b7.BookID, b6.BookID,
										b5.BookID, b4.BookID, b3.BookID, b2.BookID, b1.BookID)
	FROM	#tmpItem t INNER JOIN dbo.BHLBook b1
				ON t.ProductionBookID = b1.BookID
			LEFT JOIN dbo.BHLBook b2 ON b1.RedirectBookID = b2.BookID
			LEFT JOIN dbo.BHLBook b3 ON b2.RedirectBookID = b3.BookID
			LEFT JOIN dbo.BHLBook b4 ON b3.RedirectBookID = b4.BookID
			LEFT JOIN dbo.BHLBook b5 ON b4.RedirectBookID = b5.BookID
			LEFT JOIN dbo.BHLBook b6 ON b5.RedirectBookID = b6.BookID
			LEFT JOIN dbo.BHLBook b7 ON b6.RedirectBookID = b7.BookID
			LEFT JOIN dbo.BHLBook b8 ON b7.RedirectBookID = b8.BookID
			LEFT JOIN dbo.BHLBook b9 ON b8.RedirectBookID = b9.BookID
			LEFT JOIN dbo.BHLBook b10 ON b9.RedirectBookID = b10.BookID
	WHERE	t.ProductionBookID IS NOT NULL

	-- If any entries in #tmpItem resolve to existing BHL segments, those
	-- segments will be updated instead of new segments being created.

	-- Use associated DOIs to look for existing segments
	UPDATE	#tmpItem
	SET		ProductionSegmentID = s.SegmentID
	FROM	#tmpItem t
			INNER JOIN #tmpItemIdentifier tii ON t.BarCode = tii.BarCode
			INNER JOIN dbo.BHLIdentifier bi ON tii.IdentifierName = bi.IdentifierName
			INNER JOIN dbo.BHLItemIdentifier bii ON bi.IdentifierID = bii.IdentifierID AND tii.IdentifierValue = bii.IdentifierValue
			INNER JOIN dbo.BHLSegment s ON bii.ItemID = s.ItemID

	-- If the selected production Segment has been redirected to a different 
	-- Segment, then use that Segment instead.  Follow the "redirect" chain up 
	-- to ten levels.
	UPDATE	#tmpItem
	SET		ProductionSegmentID = COALESCE(s10.SegmentID, s9.SegmentID, s8.SegmentID, s7.SegmentID, s6.SegmentID,
											s5.SegmentID, s4.SegmentID, s3.SegmentID, s2.SegmentID, s1.SegmentID)
	FROM	#tmpItem t INNER JOIN dbo.BHLSegment s1
				ON t.ProductionSegmentID = s1.SegmentID
			LEFT JOIN dbo.BHLSegment s2 ON s1.RedirectSegmentID = s2.SegmentID
			LEFT JOIN dbo.BHLSegment s3 ON s2.RedirectSegmentID = s3.SegmentID
			LEFT JOIN dbo.BHLSegment s4 ON s3.RedirectSegmentID = s4.SegmentID
			LEFT JOIN dbo.BHLSegment s5 ON s4.RedirectSegmentID = s5.SegmentID
			LEFT JOIN dbo.BHLSegment s6 ON s5.RedirectSegmentID = s6.SegmentID
			LEFT JOIN dbo.BHLSegment s7 ON s6.RedirectSegmentID = s7.SegmentID
			LEFT JOIN dbo.BHLSegment s8 ON s7.RedirectSegmentID = s8.SegmentID
			LEFT JOIN dbo.BHLSegment s9 ON s8.RedirectSegmentID = s9.SegmentID
			LEFT JOIN dbo.BHLSegment s10 ON s9.RedirectSegmentID = s10.SegmentID
	WHERE	t.ProductionSegmentID IS NOT NULL

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

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Title', 'Update', @RowCount)
		END

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

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Title', 'Insert', @RowCount)
		END

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

		-- Add a material type for any just-added/updated titles that don't already have one
		UPDATE	dbo.BHLTitle
		SET		MaterialTypeID = m.MaterialTypeID
		FROM	dbo.BHLTitle t INNER JOIN dbo.BHLMaterialType m
					ON SUBSTRING(t.MarcLeader, 7, 1) = m.MARCCode
				INNER JOIN #tmpTitle tmp
					ON t.TitleID = tmp.ProductionTitleID
		WHERE	t.MaterialTypeID IS NULL

		-- =======================================================================

		-- Insert new authors of titles into the production database
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
		SET @RowCount = 0

		DECLARE	curInsert CURSOR 
		FOR SELECT DISTINCT
					MIN(MARCDataFieldTag), DOB, DOD, CreatorName, MarcCreator_b, 
					MarcCreator_c, MarcCreator_d, MarcCreator_q
			FROM	#tmpTitle_Creator
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
				
				UPDATE	#tmpTitle_Creator
				SET		ProductionAuthorID = @NewAuthorID
				WHERE	ISNULL(CreatorName, '') = ISNULL(@CreatorName, '')
				AND		ISNULL(MARCCreator_b, '') = ISNULL(@MarcCreator_b, '')
				AND		ISNULL(MARCCreator_c, '') = ISNULL(@MarcCreator_c, '')
				AND		ISNULL(MARCCreator_d, '') = ISNULL(@MarcCreator_d, '')
				AND		ISNULL(MARCCreator_q, '') = ISNULL(@MarcCreator_q, '')

				SET @RowCount = @Rowcount + 1
			END

			FETCH NEXT FROM curInsert INTO @MARCDataFieldTag, @DOB, @DOD, @CreatorName,
				@MarcCreator_b, @MarcCreator_c, @MarcCreator_d, @MarcCreator_q
		END

		CLOSE curInsert
		DEALLOCATE curInsert

		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Author', 'Insert', @RowCount)
		END

		-- Copy new production author IDs to #tmpItemCreator
		UPDATE	#tmpItemCreator
		SET		ProductionAuthorID = c.ProductionAuthorID
		FROM	#tmpItemCreator t INNER JOIN #tmpTitle_Creator c
					ON ISNULL(t.[CreatorName], '') = ISNULL(c.[CreatorName], '') COLLATE SQL_Latin1_General_CP1_CI_AI
		WHERE	t.ProductionAuthorID IS NULL

		-- Copy new production author IDs to #tmpSegmentAuthor
		UPDATE	#tmpSegmentAuthor
		SET		ProductionAuthorID = t.ProductionAuthorID
		FROM	#tmpSegmentAuthor a INNER JOIN #tmpTitle_Creator t
					ON ISNULL(t.CreatorName, '') = ISNULL(a.FullName, '') COLLATE SQL_Latin1_General_CP1_CI_AI
					AND ISNULL(dbo.fnRemoveNonNumericCharacters(t.DOB), '') = ISNULL(dbo.fnRemoveNonNumericCharacters(a.Startdate), '')
					AND	ISNULL(dbo.fnRemoveNonNumericCharacters(t.DOD), '') = ISNULL(dbo.fnRemoveNonNumericCharacters(a.EndDate), '')
		WHERE	a.ProductionAuthorID IS NULL

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
		FROM	#tmpTitle_Creator
		WHERE	ProductionAuthorNameID IS NULL

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Author Name', 'Insert', @RowCount)
		END

		-- =======================================================================

		-- Insert new TitleAuthor records into the production database
		INSERT INTO dbo.BHLTitleAuthor (TitleID, AuthorID, AuthorRoleID, SequenceOrder, Relationship, 
			TitleOfWork, CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
		SELECT	t.TitleID, 
				tmpC.ProductionAuthorID, 
				tmpC.CreatorRoleTypeID,
				tmpC.SequenceOrder,
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
				
		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Title Author', 'Insert', @RowCount)
		END

		-- =======================================================================

		-- Insert new title_identifier records into the production database
		INSERT INTO dbo.BHLTitle_Identifier (TitleID, IdentifierID,
			IdentifierValue, CreationDate, LastModifiedDate)
		SELECT DISTINCT t.TitleID, i.IdentifierID, tmp.IdentifierValue, 
			tmp.ExternalCreationDate, tmp.ExternalLastModifiedDate
		FROM	#tmpTitle_TitleIdentifier tmp 
				INNER JOIN dbo.BHLIdentifier i
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

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Title Identifier', 'Insert', @RowCount)
		END

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
					AND dbo.fnFilterString(tmp.Title, '[a-zA-Z0-9]', '') = dbo.fnFilterString(a.Title, '[a-zA-Z0-9]', '')
					AND dbo.fnFilterString(tmp.Section, '[a-zA-Z0-9]', '') = dbo.fnFilterString(a.Section, '[a-zA-Z0-9]', '')
					AND dbo.fnFilterString(tmp.Volume, '[a-zA-Z0-9]', '') = dbo.fnFilterString(a.Volume, '[a-zA-Z0-9]', '')
					AND dbo.fnFilterString(tmp.Heading, '[a-zA-Z0-9]', '') = dbo.fnFilterString(a.Heading, '[a-zA-Z0-9]', '')
					AND dbo.fnFilterString(tmp.Publication, '[a-zA-Z0-9]', '') = dbo.fnFilterString(a.Publication, '[a-zA-Z0-9]', '')
					AND dbo.fnFilterString(tmp.Relationship, '[a-zA-Z0-9]', '') = dbo.fnFilterString(a.Relationship, '[a-zA-Z0-9]', '')
		WHERE	a.TitleAssociationID IS NULL

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Title Association', 'Insert', @RowCount)
		END

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
					AND dbo.fnFilterString(tmp.Title, '[a-zA-Z0-9]', '') = dbo.fnFilterString(a.Title, '[a-zA-Z0-9]', '')
					AND dbo.fnFilterString(tmp.Section, '[a-zA-Z0-9]', '') = dbo.fnFilterString(a.Section, '[a-zA-Z0-9]', '')
					AND dbo.fnFilterString(tmp.Volume, '[a-zA-Z0-9]', '') = dbo.fnFilterString(a.Volume, '[a-zA-Z0-9]', '')
					AND dbo.fnFilterString(tmp.Heading, '[a-zA-Z0-9]', '') = dbo.fnFilterString(a.Heading, '[a-zA-Z0-9]', '')
					AND dbo.fnFilterString(tmp.Publication, '[a-zA-Z0-9]', '') = dbo.fnFilterString(a.Publication, '[a-zA-Z0-9]', '')
					AND dbo.fnFilterString(tmp.Relationship, '[a-zA-Z0-9]', '') = dbo.fnFilterString(a.Relationship, '[a-zA-Z0-9]', '')
				INNER JOIN dbo.BHLIdentifier ti
					ON tmp.IdentifierName = ti.IdentifierName
				LEFT JOIN dbo.BHLTitleAssociation_TitleIdentifier i
					ON a.TitleAssociationID = i.TitleAssociationID
					AND ti.IdentifierID = i.TitleIdentifierID
					AND tmp.IdentifierValue = i.IdentifierValue					
		WHERE	i.TitleAssociation_TitleIdentifierID IS NULL

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Title Association Identifier', 'Insert', @RowCount)
		END

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
					AND dbo.fnFilterString(tmp.Title, '[a-zA-Z0-9]', '') = dbo.fnFilterString(v.Title, '[a-zA-Z0-9]', '')
					AND dbo.fnFilterString(tmp.TitleRemainder, '[a-zA-Z0-9]', '') = dbo.fnFilterString(v.TitleRemainder, '[a-zA-Z0-9]', '')
					AND dbo.fnFilterString(tmp.PartNumber, '[a-zA-Z0-9]', '') = dbo.fnFilterString(v.PartNumber, '[a-zA-Z0-9]', '')
					AND dbo.fnFilterString(tmp.PartName, '[a-zA-Z0-9]', '') = dbo.fnFilterString(v.PartName, '[a-zA-Z0-9]', '')
		WHERE	v.TitleVariantID IS NULL

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Title Variant', 'Insert', @RowCount)
		END

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
					AND dbo.fnFilterString(tmp.NoteText, '[a-zA-Z0-9]', '') = dbo.fnFilterString(n.NoteText, '[a-zA-Z0-9]', '')
		WHERE	n.TitleNoteID IS NULL

		-- =======================================================================

		-- Insert new Keywords into the production database
		INSERT	dbo.BHLKeyword (Keyword, CreationDate, LastModifiedDate)
		SELECT	DISTINCT tmp.Keyword, tmp.ExternalCreationDate, tmp.ExternalLastModifiedDate
		FROM	#tmpKeyword2 tmp LEFT JOIN dbo.BHLKeyword k
					ON tmp.Keyword = k.Keyword
		WHERE	k.KeywordID IS NULL

		INSERT	dbo.BHLKeyword (Keyword)
		SELECT	DISTINCT tmp.Keyword
		FROM	#tmpItemKeyword tmp LEFT JOIN dbo.BHLKeyword k
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

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Title Keyword', 'Update', @RowCount)
		END

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
		
		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Title Keyword', 'Insert', @RowCount)
		END

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

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Title Language', 'Insert', @RowCount)
		END

		-- =======================================================================

		-- Insert new items into the production database

		DECLARE @BHLBookItemID int
		DECLARE @BHLSegmentItemID int

		IF EXISTS (SELECT ItemID FROM #tmpItem WHERE VirtualTitleID IS NULL)
		BEGIN
			-- Insert Book+Item records for a "regular" IA item

			-- Insert a new BHL Item record
			INSERT	dbo.BHLItem (ItemTypeID, ItemStatusID, ItemSourceID, VaultID, FileRootFolder, ItemDescription, Note,
						CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
			SELECT	10, tmp.ItemStatusID, isis.BHLItemSourceID, tmp.VaultID, tmp.MARCBibID, tmp.ItemDescription, tmp.Note,
						tmp.ExternalCreationDate, tmp.ExternalLastModifiedDate, tmp.ExternalCreationUser, tmp.ExternalLastModifiedUser
			FROM	#tmpItem tmp 
					INNER JOIN #tmpTitle tmpT ON tmp.ImportKey = tmpT.ImportKey
					INNER JOIN dbo.BHLTitle t ON tmpT.ProductionTitleID = t.TitleID
					LEFT JOIN dbo.BHLBook b ON tmp.BarCode = b.BarCode
					LEFT JOIN dbo.ImportSourceItemSource isis ON tmp.ImportSourceID = isis.ImportSourceID
			WHERE	b.BookID IS NULL

			SELECT @BHLBookItemID = SCOPE_IDENTITY(), @RowCount = @@ROWCOUNT
			IF (@RowCount > 0)
			BEGIN
				INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
				VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Item', 'Insert', @RowCount)
			END

			-- Insert a new BHL Book record
			INSERT INTO dbo.BHLBook (ItemID, LanguageCode, BarCode, MARCItemID, CallNumber, 
				Volume, StartYear, EndYear, StartVolume, EndVolume, StartIssue, 
				EndIssue, StartNumber, EndNumber, StartSeries, EndSeries, 
				StartPart, EndPart, IdentifierBib, ZQuery, Sponsor, 
				LicenseUrl, Rights, DueDiligence, CopyrightStatus, CopyrightRegion,
				CopyrightComment, CopyrightEvidence, ScanningUser, ScanningDate, 
				PaginationStatusID, PaginationStatusDate, PaginationStatusUserID, 
				PaginationCompleteDate, PaginationCompleteUserID, PageProgression,
				LastPageNameLookupDate, CreationDate, LastModifiedDate, CreationUserID, 
				LastModifiedUserID)
			SELECT	@BHLBookItemID, UPPER(tmp.LanguageCode), tmp.BarCode, tmp.MARCItemID, tmp.CallNumber, 
					tmp.Volume, tmp.[Year], tmp.EndYear, tmp.StartVolume, tmp.EndVolume, tmp.StartIssue, 
					tmp.EndIssue, tmp.StartNumber, tmp.EndNumber, tmp.StartSeries, tmp.EndSeries, 
					tmp.StartPart, tmp.EndPart, tmp.IdentifierBib, tmp.ZQuery, tmp.Sponsor, 
					tmp.LicenseUrl, tmp.Rights, tmp.DueDiligence, tmp.CopyrightStatus, tmp.CopyrightRegion, 
					tmp.CopyrightComment, tmp.CopyrightEvidence, tmp.ScanningUser, tmp.ScanningDate, 
					tmp.PaginationStatusID, tmp.PaginationStatusDate, tmp.PaginationStatusUserID, 
					tmp.PaginationCompleteDate, tmp.PaginationCompleteUserID, tmp.PageProgression,
					tmp.LastPageNameLookupDate, tmp.ExternalCreationDate, tmp.ExternalLastModifiedDate, 
					tmp.ExternalCreationUser, tmp.ExternalLastModifiedUser
			FROM	#tmpItem tmp 
					INNER JOIN #tmpTitle tmpT ON tmp.ImportKey = tmpT.ImportKey
					INNER JOIN dbo.BHLTitle t ON tmpT.ProductionTitleID = t.TitleID
					LEFT JOIN dbo.BHLBook b ON tmp.BarCode = b.BarCode
			WHERE	b.BookID IS NULL

			SELECT @RowCount = @@ROWCOUNT
			IF (@RowCount > 0)
			BEGIN
				INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
				VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Book', 'Insert', @RowCount)
			END

			-- Insert title->item relationships into the production TitleItem table
			INSERT INTO dbo.BHLTitleItem (ItemID, TitleID, ItemSequence, IsPrimary)
			SELECT DISTINCT @BHLBookItemID, t.TitleID, tmp.ItemSequence, 1
			FROM	#tmpItem tmp 
					INNER JOIN #tmpTitle tmpT ON tmp.ImportKey = tmpT.ImportKey
					INNER JOIN dbo.BHLTitle t ON tmpT.ProductionTitleID = t.TitleID
					LEFT JOIN dbo.BHLTitleItem ti ON t.TitleID = ti.TitleID AND @BHLBookItemID = ti.ItemID
			WHERE	ti.ItemTitleID IS NULL

			SELECT @RowCount = @@ROWCOUNT
			IF (@RowCount > 0)
			BEGIN
				INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
				VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Title Item', 'Insert', @RowCount)
			END

			-- Make sure the auto-assigned ItemSequence values are unique.
			-- Calculate the ItemSequence by ordering each title by the ItemTitleID.
			UPDATE	dbo.BHLTitleItem
			SET		ItemSequence = x.Sequence
			FROM	dbo.BHLTitleItem ti
					INNER JOIN (SELECT	(ROW_NUMBER() OVER (PARTITION BY t.TitleID 
															ORDER BY ti2.ItemTitleID)) + 9999 AS Sequence, 
										i.ItemID,
										t.TitleID
								FROM	#tmpItem tmp 
										INNER JOIN #tmpTitle tmpT ON tmp.ImportKey = tmpT.ImportKey
										INNER JOIN dbo.BHLTitle t ON tmpT.ProductionTitleID = t.TitleID
										INNER JOIN dbo.BHLTitleItem ti2 ON t.TitleID = ti2.TitleID
										INNER JOIN dbo.BHLItem i ON ti2.ItemID = i.ItemID 
								WHERE	tmp.ImportSourceID = 1
								AND		ti2.ItemSequence >= 10000
								) x
						ON ti.ItemID = x.ItemID
						AND ti.TitleID = x.TitleID

		END
		ELSE
		BEGIN
			-- This is a part of a Virtual Item
			-- Insert Segment+Item records and if necessary Book+Item records
			IF EXISTS(SELECT ItemID FROM #tmpItem WHERE ProductionBookID IS NULL)
			BEGIN
				DECLARE @VirtualVolumeBarcode nvarchar(200)
				SELECT @VirtualVolumeBarcode = VirtualVolumeBarCode FROM #tmpItem WHERE ProductionBookID IS NULL

				DECLARE @VirtualItemSourceID int
				SELECT	@VirtualItemSourceID = BHLItemSourceID
				FROM	dbo.ImportSourceItemSource isis INNER JOIN dbo.ImportSource s ON isis.ImportSourceID = s.ImportSourceID
				WHERE	s.[Source] = 'Virtual Item'

				-- Insert a new Item record for a new Virtual Item
				INSERT	dbo.BHLItem (ItemTypeID, ItemStatusID, ItemSourceID, ItemDescription, Note)
				SELECT	10, -- Book
						40, -- Published
						@VirtualItemSourceID,
						t.ItemDescription,
						t.Note
				FROM	#tmpItem t
				WHERE	ProductionBookID IS NULL

				SELECT @BHLBookItemID =  SCOPE_IDENTITY(), @RowCount = @@ROWCOUNT

				IF (@RowCount > 0)
				BEGIN
					INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
					VALUES (@ProductionDate, @ImportSourceID, @VirtualVolumeBarcode, 'Success', 'Item', 'Insert', @RowCount)
				END

				-- Insert a new Book record for a new Virtual Item
				INSERT	dbo.BHLBook (ItemID, LanguageCode, BarCode, MARCItemID, CallNumber,
							Volume, StartYear, EndYear, StartVolume, EndVolume, StartIssue, 
							EndIssue, StartNumber, EndNumber, StartSeries, EndSeries, 
							StartPart, EndPart, Sponsor, LicenseUrl, DueDiligence, CopyrightStatus, 
							Rights, PageProgression, VirtualVolumeKey, IsVirtual)
				SELECT	@BHLBookItemID, UPPER(t.LanguageCode), t.VirtualVolumeBarCode, t.VirtualVolumeBarCode, t.CallNumber,
							t.VirtualVolume, t.[Year], t.EndYear, t.StartVolume, t.EndVolume, t.StartIssue, 
							t.EndIssue, t.StartNumber, t.EndNumber, t.StartSeries, t.EndSeries, 
							t.StartPart, t.EndPart, t.Sponsor, t.LicenseUrl, t.DueDiligence, t.CopyrightStatus, 
							t.Rights, t.PageProgression, t.VirtualVolumeKey, 1
				FROM	#tmpItem t
				WHERE	ProductionBookID IS NULL

				SELECT @RowCount = @@ROWCOUNT
				IF (@RowCount > 0)
				BEGIN
					INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
					VALUES (@ProductionDate, @ImportSourceID, @VirtualVolumeBarcode, 'Success', 'Book', 'Insert', @RowCount)
				END

				-- Add the production BookID to the temp Item table
				UPDATE	#tmpItem
				SET		ProductionBookID = b.BookID
				FROM	#tmpItem t
						INNER JOIN dbo.BHLBook b ON t.VirtualVolumeBarCode = b.BarCode

				-- Insert title->item relationships into the production ItemTitle table
				INSERT INTO dbo.BHLTitleItem (ItemID, TitleID, ItemSequence, IsPrimary)
				SELECT DISTINCT @BHLBookItemID, t.VirtualTitleID, t.ItemSequence, 1
				FROM	#tmpItem t
						LEFT JOIN dbo.BHLTitleItem ti ON t.VirtualTitleID = ti.TitleID AND @BHLBookItemID = ti.ItemID
				WHERE	ti.ItemTitleID IS NULL

				SELECT @RowCount = @@ROWCOUNT
				IF (@RowCount > 0)
				BEGIN
					INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
					VALUES (@ProductionDate, @ImportSourceID, @VirtualVolumeBarcode, 'Success', 'Title Item', 'Insert', @RowCount)
				END

				-- Make sure the auto-assigned ItemSequence values are unique.
				-- Calculate the ItemSequence by ordering each title by the ItemTitleID.
				UPDATE	dbo.BHLTitleItem
				SET		ItemSequence = x.Sequence
				FROM	dbo.BHLTitleItem ti
						INNER JOIN (SELECT	(ROW_NUMBER() OVER (PARTITION BY tmp.VirtualTitleID 
																ORDER BY ti2.ItemTitleID)) + 9999 AS Sequence, 
											i.ItemID,
											tmp.VirtualTitleID
									FROM	#tmpItem tmp 
											INNER JOIN dbo.BHLTitleItem ti2 ON tmp.VirtualTitleID = ti2.TitleID
											INNER JOIN dbo.BHLItem i ON ti2.ItemID = i.ItemID 
									WHERE	tmp.ImportSourceID = 1
									AND		ti2.ItemSequence >= 10000
									) x
							ON ti.ItemID = x.ItemID
							AND ti.TitleID = x.VirtualTitleID

			END
			ELSE
			BEGIN
				-- Get the production ItemID for the Virtual Item
				SELECT	@BHLBookItemID = b.ItemID
				FROM	dbo.BHLBook b
						INNER JOIN #tmpItem i ON b.BookID = i.ProductionBookID
			END

			-- Insert or update a segment record, as appropriate
			IF EXISTS (SELECT * FROM #tmpItem WHERE ProductionSegmentID IS NULL)
			BEGIN
				-- Insert a new Item record for a part of a Virtual Item
				INSERT	dbo.BHLItem (ItemTypeID, ItemStatusID, ItemSourceID, VaultID, FileRootFolder, ItemDescription, Note)
				SELECT	20, -- Segment
						t.ItemStatusID,
						isis.BHLItemSourceID,
						t.VaultID,
						t.MARCBibID,
						t.ItemDescription,
						t.Note
				FROM	#tmpItem t
						LEFT JOIN dbo.ImportSourceItemSource isis ON t.ImportSourceID = isis.ImportSourceID
				WHERE	t.VirtualTitleID IS NOT NULL
				AND		t.ProductionSegmentID IS NULL	-- Insert new if no existing segment

				SELECT	@BHLSegmentItemID =  SCOPE_IDENTITY(), @RowCount = @@ROWCOUNT

				IF (@RowCount > 0)
				BEGIN
					INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
					VALUES (@ProductionDate, @ImportSourceID, @Barcode, 'Success', 'Item', 'Insert', @RowCount)
				END

				-- Insert a new Segment record for a part of a Virtual Item
				INSERT INTO dbo.BHLSegment (
							ItemID, SegmentGenreID, Barcode, MARCItemID, Title, SortTitle, 
							PublicationDetails,	PublisherName, Summary, Volume, Issue, 
							[Date], PageRange, StartPageNumber, EndPageNumber, 
							LanguageCode, RightsStatus, CopyrightStatus, LicenseUrl,
							PageProgression, ContainerTitle
							)
				SELECT	@BHLSegmentItemID, tmp.SegmentGenreID, tmp.Barcode, tmp.MARCItemID, tmp.Title, tmp.SortTitle, 
						tmp.PublicationDetails, tmp.PublisherName, tmp.Summary, tmp.Volume, tmp.Issue, 
						tmp.SegmentDate, tmp.StartPage + '--' + tmp.EndPage, tmp.StartPage, tmp.EndPage, 
						tmp.LanguageCode, tmp.Rights, tmp.CopyrightStatus, tmp.LicenseUrl,
						tmp.PageProgression, tmp.ContainerTitle
				FROM	#tmpItem tmp
				WHERE	VirtualTitleID IS NOT NULL
				AND		ProductionSegmentID IS NULL		-- Insert new if no existing segment

				SELECT	@RowCount = @@ROWCOUNT
				IF (@RowCount > 0)
				BEGIN
					INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
					VALUES (@ProductionDate, @ImportSourceID, @Barcode, 'Success', 'Segment', 'Insert', @RowCount)
				END
			END
			ELSE
			BEGIN
				SELECT	@BHLSegmentItemID = s.ItemID FROM #tmpItem t INNER JOIN dbo.BHLSegment s ON t.ProductionSegmentID = s.SegmentID

				-- Update an existing Item record that is part of a Virtual Item
				-- Except for ItemSourceID, don't overwrite existing values; only add new values (fill in the blanks)
				UPDATE	i
				SET		ItemSourceID = CASE WHEN isis.BHLItemSourceID IS NULL THEN i.ItemSourceID ELSE isis.BHLItemSourceID END,
						VaultID = CASE WHEN i.VaultID IS NULL THEN t.VaultID ELSE i.VaultID END, 
						FileRootFolder = CASE WHEN ISNULL(i.FileRootFolder, '') = '' THEN t.MARCBibID ELSE i.FileRootFolder END,
						ItemDescription = CASE WHEN ISNULL(i.ItemDescription, '') = '' THEN t.ItemDescription ELSE i.ItemDescription END, 
						Note = CASE WHEN ISNULL(i.Note, '') = '' THEN  t.Note ELSE i.Note END,
						LastModifiedDate = GETDATE(),
						LastModifiedUserID = 1
				FROM	#tmpItem t 
						INNER JOIN dbo.BHLSegment s ON t.ProductionSegmentID = s.SegmentID
						INNER JOIN dbo.BHLItem i ON s.ItemID = i.ItemID
						LEFT JOIN dbo.ImportSourceItemSource isis ON t.ImportSourceID = isis.ImportSourceID
				WHERE	t.VirtualTitleID IS NOT NULL
				AND		t.ProductionSegmentID IS NOT NULL

				SELECT	@RowCount = @@ROWCOUNT
				IF (@RowCount > 0)
				BEGIN
					INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
					VALUES (@ProductionDate, @ImportSourceID, @Barcode, 'Success', 'Item', 'Update', @RowCount)
				END

				-- Update an existing Segment record that is part of a virtual item
				-- Don't overwrite existing values; only add new values (fill in the blanks)
				UPDATE	s
				SET		Barcode = CASE WHEN ISNULL(s.Barcode, '') = '' THEN t.BarCode ELSE s.BarCode END,
						MARCItemID = CASE WHEN ISNULL(s.MARCItemID, '') = '' THEN t.MARCItemID ELSE s.MARCItemID END,
						Title = CASE WHEN ISNULL(s.Title, '') = '' THEN t.Title ELSE s.Title END,
						SortTitle = CASE WHEN ISNULL(s.SortTitle, '') = '' THEN t.SortTitle ELSE s.SortTitle END,
						PublicationDetails = CASE WHEN ISNULL(s.PublicationDetails, '') = '' THEN t.PublicationDetails ELSE s.PublicationDetails END,
						PublisherName = CASE WHEN ISNULL(s.PublisherName, '') = '' THEN t.PublisherName ELSE s.PublisherName END,
						Summary = CASE WHEN ISNULL(s.Summary, '') = '' THEN t.Summary ELSE s.Summary END,
						Volume = CASE WHEN ISNULL(s.Volume, '') = '' THEN t.Volume ELSE s.Volume END,
						Issue = CASE WHEN ISNULL(s.Issue, '') = '' THEN t.Issue ELSE s.Issue END,
						[Date] = CASE WHEN ISNULL(s.[Date], '') = '' THEN t.SegmentDate ELSE s.[Date] END,
						PageRange = CASE WHEN ISNULL(s.PageRange, '') = '' THEN t.StartPage + '--' + t.EndPage ELSE s.PageRange END,
						StartPageNumber = CASE WHEN ISNULL(s.StartPageNumber, '') = '' THEN t.StartPage ELSE s.StartPageNumber END,
						EndPageNumber = CASE WHEN ISNULL(s.EndPageNumber, '') = '' THEN t.EndPage ELSE s.EndPageNumber END,
						LanguageCode = CASE WHEN ISNULL(s.LanguageCode, '') = '' THEN t.LanguageCode ELSE s.LanguageCode END,
						RightsStatus = CASE WHEN ISNULL(s.RightsStatus, '') = '' THEN t.Rights ELSE s.RightsStatus END,
						CopyrightStatus = CASE WHEN ISNULL(s.CopyrightStatus, '') = '' THEN t.CopyrightStatus ELSE s.CopyrightStatus END,
						LicenseUrl = CASE WHEN ISNULL(s.LicenseUrl, '') = '' THEN t.LicenseUrl ELSE s.LicenseUrl END,
						PageProgression = CASE WHEN ISNULL(s.PageProgression, '') = '' THEN t.PageProgression ELSE s.PageProgression END,
						ContainerTitle = CASE WHEN ISNULL(s.ContainerTitle, '') = '' THEN t.ContainerTitle ELSE s.ContainerTitle END,
						LastModifiedDate = GETDATE(),
						LastModifiedUserID = 1
				FROM	#tmpItem t
						INNER JOIN dbo.BHLSegment s ON t.ProductionSegmentID = s.SegmentID
				WHERE	t.VirtualTitleID IS NOT NULL
				AND		t.ProductionSegmentID IS NOT NULL

				SELECT	@RowCount = @@ROWCOUNT
				IF (@RowCount > 0)
				BEGIN
					INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
					VALUES (@ProductionDate, @ImportSourceID, @Barcode, 'Success', 'Segment', 'Update', @RowCount)
				END
			END

			-- Insert segment->book relationships into the production ItemRelationship table
			INSERT dbo.BHLItemRelationship(ParentID, ChildID, SequenceOrder) 
			SELECT @BHLBookItemID, @BHLSegmentItemID, 10000
			WHERE NOT EXISTS (
				SELECT	1 
				FROM	dbo.BHLItemRelationship
				WHERE	ParentID = @BHLBookItemID
				AND		ChildID = @BHLSegmentItemID
				)

			SELECT @RowCount = @@ROWCOUNT
			IF (@RowCount > 0)
			BEGIN
				INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
				VALUES (@ProductionDate, @ImportSourceID, @Barcode, 'Success', 'Item Relationship', 'Insert', @RowCount)
			END

			-- Attempt to place the segments in the correct order.
			-- NOTE:  This may be problematic.  Revert to commented ORDER BY clause if necessary.
			UPDATE	dbo.BHLItemRelationship
			SET		SequenceOrder = x.Sequence
			FROM	dbo.BHLItemRelationship r
					INNER JOIN (SELECT	(ROW_NUMBER() OVER (PARTITION BY r2.ParentID
															ORDER BY RIGHT('0000' + s.Volume, 5), 
																	RIGHT('0000' + s.Issue, 5), 
																	RIGHT('0000' + s.StartPageNumber, 5),
																	s.Date,
																	s.SortTitle)) AS Sequence,
															--ORDER BY r2.RelationshipID)) + 9999 AS Sequence, 
										r2.ParentID,
										r2.ChildID
								FROM	dbo.BHLItemRelationship r2
										INNER JOIN dbo.BHLSegment s ON r2.ChildID = s.ItemID
								WHERE	r2.ParentID = @BHLBookItemID
								) x
						ON r.ParentID = x.ParentID
						AND r.ChildID = x.ChildID
		END

		-- =======================================================================

		-- Insert new iteminstitution records into the production database
		DECLARE @ContributorRoleID int
		DECLARE @HoldingInstitutionRoleID int
		DECLARE @ScanningInstitutionRoleID int
		DEClARE @RightsHolderRoleID int
		SELECT	@ContributorRoleID = InstitutionRoleID FROM dbo.BHLInstitutionRole WHERE InstitutionRoleName = 'Contributor'
		SELECT	@HoldingInstitutionRoleID = InstitutionRoleID FROM dbo.BHLInstitutionRole WHERE InstitutionRoleName = 'Holding Institution'
		SELECT	@ScanningInstitutionRoleID = InstitutionRoleID FROM dbo.BHLInstitutionRole WHERE InstitutionRoleName = 'Scanning Institution'
		SELECT	@RightsHolderRoleID = InstitutionRoleID FROM dbo.BHLInstitutionRole WHERE InstitutionRoleName = 'Rights Holder'
		SET @RowCount = 0

		-- Insert ItemInstitution records for each role

		-- Add a Contributor to a virtual item segment if one does not already exist
		IF NOT EXISTS (	SELECT	ItemInstitutionID
						FROM	dbo.BHLItemInstitution
						WHERE	ItemID = @BHLSegmentItemID 
						AND		InstitutionRoleID = @ContributorRoleID )
		BEGIN
			INSERT	dbo.BHLItemInstitution (ItemID, InstitutionCode, InstitutionRoleID)
			SELECT	@BHLSegmentItemID, tmp.InstitutionCode, @ContributorRoleID
			FROM	#tmpItem tmp LEFT JOIN dbo.BHLItemInstitution ii
						ON ii.ItemID = @BHLSegmentItemID
						AND ii.InstitutionCode = tmp.InstitutionCode
						AND ii.InstitutionRoleID = @ContributorRoleID
			WHERE	tmp.InstitutionCode IS NOT NULL
			AND		ii.ItemInstitutionID IS NULL
			AND		tmp.VirtualTitleID IS NOT NULL	-- Contributor only applies to IA items (Segments) that are part of Virtual Items

			SELECT @RowCount = @RowCount + @@ROWCOUNT
		END

		INSERT	dbo.BHLItemInstitution (ItemID, InstitutionCode, InstitutionRoleID)
		SELECT	@BHLBookItemID, tmp.InstitutionCode, @HoldingInstitutionRoleID
		FROM	#tmpItem tmp LEFT JOIN dbo.BHLItemInstitution ii 
					ON ii.ItemID = @BHLBookItemID 
					AND ii.InstitutionCode = tmp.InstitutionCode 
					AND ii.InstitutionRoleID = @HoldingInstitutionRoleID
		WHERE	tmp.InstitutionCode IS NOT NULL
		AND		ii.ItemInstitutionID IS NULL

		SELECT @RowCount = @RowCount + @@ROWCOUNT

		INSERT	dbo.BHLItemInstitution (ItemID, InstitutionCode, InstitutionRoleID)
		SELECT	@BHLBookItemID, tmp.ScanningInstitutionCode, @ScanningInstitutionRoleID
		FROM	#tmpItem tmp LEFT JOIN dbo.BHLItemInstitution ii 
					ON ii.ItemID = @BHLBookItemID 
					AND ii.InstitutionCode = tmp.ScanningInstitutionCode 
					AND ii.InstitutionRoleID = @ScanningInstitutionRoleID
		WHERE	tmp.ScanningInstitutionCode IS NOT NULL
		AND		ii.ItemInstitutionID IS NULL

		SELECT @RowCount = @RowCount + @@ROWCOUNT

		INSERT	dbo.BHLItemInstitution (ItemID, InstitutionCode, InstitutionRoleID)
		SELECT	@BHLBookItemID, tmp.RightsHolderCode, @RightsHolderRoleID
		FROM	#tmpItem tmp LEFT JOIN dbo.BHLItemInstitution ii 
					ON ii.ItemID = @BHLBookItemID 
					AND ii.InstitutionCode = tmp.RightsHolderCode
					AND ii.InstitutionRoleID = @RightsHolderRoleID
		WHERE	tmp.RightsHolderCode IS NOT NULL
		AND		ii.ItemInstitutionID IS NULL

		SELECT @RowCount = @RowCount + @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Item Institution', 'Insert', @RowCount)
		END

		-- =======================================================================

		-- Add Titles/Items to collections with Auto-Add criteria
		-- NOTE:  Virtual Items *will not* be auto-added to collections

		-- Find the new items that match the auto-add criteria for at least one item-based collection.
		INSERT	dbo.BHLItemCollection (ItemID, CollectionID)
		SELECT	x.ItemID, x.CollectionID
		FROM	(	-- Get a list of all new items and the collections they should be attached to 
					SELECT	b.ItemID, c.CollectionID
					FROM	#tmpItem tmp INNER JOIN dbo.BHLBook b
								ON tmp.BarCode = b.BarCode
							INNER JOIN dbo.BHLCollection c
								ON (tmp.InstitutionCode = c.InstitutionCode AND b.LanguageCode = c.LanguageCode)
								OR (tmp.InstitutionCode = c.InstitutionCode AND c.LanguageCode IS NULL)
								OR (c.InstitutionCode IS NULL AND b.LanguageCode = c.LanguageCode)
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

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Item Collection', 'Insert', @RowCount)
		END

		-- Find titles related to new items that match the auto-add criteria for at least one title-based collection.
		INSERT	dbo.BHLTitleCollection (TitleID, CollectionID)
		SELECT	x.TitleID, x.CollectionID
		FROM	(	-- Get a list of all titles associated with new items and the collections they should be attached to 
					SELECT DISTINCT 
							t.TitleID, c.CollectionID
					FROM	#tmpItem tmp 
							INNER JOIN dbo.BHLBook b ON tmp.BarCode = b.BarCode
							INNER JOIN dbo.BHLItem i ON b.ItemID = i.ItemID
							INNER JOIN dbo.BHLTitleItem ti ON i.ItemID = ti.ItemID
							INNER JOIN dbo.BHLTitle t ON ti.TitleID = t.TitleID
							INNER JOIN dbo.BHLCollection c
								ON (tmp.InstitutionCode = c.InstitutionCode AND b.LanguageCode = c.LanguageCode)
								OR (tmp.InstitutionCode = c.InstitutionCode AND c.LanguageCode IS NULL)
								OR (c.InstitutionCode IS NULL AND b.LanguageCode = c.LanguageCode)
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

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Title Collection', 'Insert', @RowCount)
		END

		-- =======================================================================

		-- Insert new authors of items into the production database
		DECLARE @ItemCreatorName nvarchar(255)
		SET @RowCount = 0

		DECLARE	curInsert CURSOR 
		FOR SELECT	CreatorName
			FROM	#tmpItemCreator
			WHERE	ProductionAuthorID IS NULL
			GROUP BY CreatorName
		
		OPEN curInsert
		FETCH NEXT FROM curInsert INTO @ItemCreatorName

		WHILE (@@fetch_status <> -1)
		BEGIN
			IF (@@fetch_status <> -2)
			BEGIN
				-- Insert a new author record into the production database
				INSERT INTO dbo.BHLAuthor (AuthorTypeID, IsActive, CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
				VALUES (1, 1, GETDATE(), GETDATE(), 1, 1)
						
				-- Save the ID of the newly inserted author record
				SELECT @NewAuthorID = SCOPE_IDENTITY()
				
				UPDATE	#tmpItemCreator
				SET		ProductionAuthorID = @NewAuthorID
				WHERE	CreatorName = @ItemCreatorName
				AND		ProductionAuthorID IS NULL

				SET @RowCount = @RowCount + 1
			END

			FETCH NEXT FROM curInsert INTO @ItemCreatorName
		END

		CLOSE curInsert
		DEALLOCATE curInsert

		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Author', 'Insert', @RowCount)
		END

		-- =======================================================================
		
		-- Insert new AuthorName records into the production database
		INSERT INTO dbo.BHLAuthorName (AuthorID, FullName, IsPreferredName,
			CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
		SELECT DISTINCT
				ProductionAuthorID,
				CreatorName,
				1,
				GETDATE(),
				GETDATE(),
				1, 1
		FROM	#tmpItemCreator
		WHERE	ProductionAuthorNameID IS NULL

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Author Name', 'Insert', @RowCount)
		END

		-- =======================================================================
		
		-- Insert new AuthorIdentifier records into the production database
		INSERT INTO dbo.BHLAuthorIdentifier (AuthorID, IdentifierID, IdentifierValue, 
			CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
		SELECT	c.ProductionAuthorID,
				i.IdentifierID,
				i.IdentifierValue,
				GETDATE(),
				GETDATE(),
				1, 1
		FROM	#tmpItemCreator c
				INNER JOIN #tmpItemCreatorIdentifier i 
					ON c.BarCode = i.BarCode 
					AND c.SequenceOrder = i.SequenceOrder
				LEFT JOIN dbo.BHLAuthorIdentifier bi 
					ON c.ProductionAuthorID = bi.AuthorID 
					AND i.IdentifierID = bi.IdentifierID 
					AND i.IdentifierValue = bi.IdentifierValue
		WHERE	bi.AuthorIdentifierID IS NULL

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Author Identifier', 'Insert', @RowCount)
		END

		-- =======================================================================

		-- Insert new ItemAuthor records into the production database if none exist
		IF NOT EXISTS (	SELECT ItemAuthorID FROM dbo.BHLItemAuthor WHERE ItemID = @BHLSegmentItemID )
		BEGIN
			INSERT INTO dbo.BHLItemAuthor (ItemID, AuthorID, SequenceOrder)
			SELECT	s.ItemID, 
					c.ProductionAuthorID, 
					c.SequenceOrder
			FROM	#tmpItemCreator c 
					INNER JOIN dbo.BHLSegment s ON c.BarCode = s.BarCode
					LEFT JOIN dbo.BHLItemAuthor ia 
						ON s.ItemID = ia.ItemID
						AND c.ProductionAuthorID = ia.AuthorID
			WHERE	ia.ItemAuthorID IS NULL
				
			SELECT @RowCount = @@ROWCOUNT
			IF (@RowCount > 0)
			BEGIN
				INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
				VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Item Author', 'Insert', @RowCount)
			END
		END

		-- =======================================================================

		-- Insert new ItemKeyword records into the production database if none exist
		IF NOT EXISTS (	SELECT ItemKeywordID FROM dbo.BHLItemKeyword WHERE ItemID = @BHLSegmentItemID )
		BEGIN
			INSERT INTO dbo.BHLItemKeyword (ItemID, KeywordID)
			SELECT DISTINCT @BHLSegmentItemID, k.KeywordID
			FROM	#tmpItemKeyword t
					INNER JOIN #tmpItem i ON t.Barcode = i.Barcode
					INNER JOIN dbo.BHLKeyword k ON t.Keyword = k.Keyword
					LEFT JOIN dbo.BHLItemKeyword ik ON ik.ItemID = @BHLSegmentItemID AND k.KeywordID = ik.KeywordID
			WHERE	ik.ItemKeywordID IS NULL

			SELECT @RowCount = @@ROWCOUNT
			IF (@RowCount > 0)
			BEGIN
				INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
				VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Item Keyword', 'Insert', @RowCount)
			END
		END

		-- =======================================================================

		-- Insert new ItemIdentifier records into the production database
		INSERT INTO dbo.BHLItemIdentifier (ItemID, IdentifierID, IdentifierValue)
		SELECT DISTINCT @BHLSegmentItemID, bi.IdentifierID, t.IdentifierValue
		FROM	#tmpItemIdentifier t
				INNER JOIN #tmpItem i ON t.Barcode = i.Barcode
				INNER JOIN dbo.BHLIdentifier bi ON t.IdentifierName = bi.IdentifierName
				LEFT JOIN dbo.BHLItemIdentifier ii 
					ON ii.ItemID = @BHLSegmentItemID 
					AND ii.IdentifierID = bi.IdentifierID 
					AND ii.IdentifierValue = t.IdentifierValue
		WHERE	ii.ItemIdentifierID IS NULL

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Item Identifier', 'Insert', @RowCount)
		END

		-- =======================================================================

		-- Insert new itemlanguage records into the production database
		-- "regular" items
		INSERT INTO dbo.BHLItemLanguage (ItemID, LanguageCode)
		SELECT DISTINCT b.ItemID, UPPER(tmp.LanguageCode)
		FROM	#tmpItemLanguage tmp 
				INNER JOIN dbo.BHLBook b ON tmp.BarCode = b.BarCode
				LEFT JOIN dbo.BHLItemLanguage il ON b.ItemID = il.ItemID AND tmp.LanguageCode = il.LanguageCode
		WHERE	il.ItemLanguageID IS NULL

		SELECT @RowCount = @@ROWCOUNT

		-- virtual items
		INSERT INTO dbo.BHLItemLanguage (ItemID, LanguageCode)
		SELECT DISTINCT @BHLSegmentItemID, UPPER(tmp.LanguageCode)
		FROM	#tmpItemLanguage tmp
				INNER JOIN #tmpItem i ON tmp.Barcode = i.Barcode
				LEFT JOIN dbo.BHLItemLanguage il ON @BHLSegmentItemID = il.ItemID AND tmp.LanguageCode = il.LanguageCode
		WHERE	il.ItemLanguageID IS NULL
		AND		@BHLSegmentItemID IS NOT NULL

		SELECT @RowCount = @RowCount + @@ROWCOUNT

		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Item Language', 'Insert', @RowCount)
		END

		-- =======================================================================

		-- Insert the new pages into the production database
		INSERT INTO dbo.BHLPage (FileNamePrefix, Illustration, Active, Year, Series, 
			Volume, Issue, ExternalURL, IssuePrefix, LastPageNameLookupDate, 
			PaginationUserID, PaginationDate, CreationDate, LastModifiedDate, 
			CreationUserID, LastModifiedUserID)
		SELECT	t.FileNamePrefix, t.Illustration, t.Active, t.Year, t.Series, 
				t.Volume, t.Issue, t.AltExternalURL, t.IssuePrefix, t.LastPageNameLookupDate, 
				t.PaginationUserID, t.PaginationDate, t.ExternalCreationDate, t.ExternalLastModifiedDate, 
				t.ExternalCreationUser, t.ExternalLastModifiedUser
		FROM	#tmpPage t 
				LEFT JOIN dbo.BHLPage p ON t.FileNamePrefix = p.FileNamePrefix
		WHERE	p.PageID IS NULL

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Page', 'Insert', @RowCount)
		END

		INSERT INTO dbo.BHLItemPage (ItemID, PageID, SequenceOrder, 
			CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
		SELECT	CASE WHEN i.VirtualTitleID IS NULL THEN @BHLBookItemID ELSE @BHLSegmentItemID END, 
				p.PageID, t.SequenceOrder, t.ExternalCreationDate, t.ExternalLastModifiedDate,
				t.ExternalCreationUser, t.ExternalLastModifiedUser
		FROM	#tmpPage t
				INNER JOIN #tmpItem i ON t.BarCode = i.BarCode
				INNER JOIN dbo.BHLPage p ON t.FileNamePrefix = p.FileNamePrefix

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'ItemPage', 'Insert', @RowCount)
		END

		-- =======================================================================

		-- Insert new indicated pages into the production database
		-- Non-virtual items
		INSERT INTO dbo.BHLIndicatedPage (PageID, Sequence, PagePrefix,
			PageNumber, Implied, CreationDate, LastModifiedDate, CreationUserID, 
			LastModifiedUserID)
		SELECT	p.PageID, t.Sequence, t.PagePrefix, t.PageNumber, t.Implied,
				t.ExternalCreationDate, t.ExternalLastModifiedDate, 
				t.ExternalCreationUser, t.ExternalLastModifiedUser
		FROM	#tmpIndicatedPage t 
				INNER JOIN dbo.BHLBook b ON t.BarCode = b.BarCode
				INNER JOIN dbo.BHLItemPage ip ON b.ItemID = ip.ItemID AND t.SequenceOrder = ip.SequenceOrder
				INNER JOIN dbo.BHLPage p ON ip.PageID = p.PageID AND t.FileNamePrefix = p.FileNamePrefix
				LEFT JOIN dbo.BHLIndicatedPage ipg ON p.PageID = ipg.PageID AND t.Sequence = ipg.Sequence
		WHERE	ipg.PageID IS NULL

		SELECT @RowCount = @@ROWCOUNT

		-- Segments on virtual items
		INSERT INTO dbo.BHLIndicatedPage (PageID, Sequence, PagePrefix,
			PageNumber, Implied, CreationDate, LastModifiedDate, CreationUserID, 
			LastModifiedUserID)
		SELECT	p.PageID, t.Sequence, t.PagePrefix, t.PageNumber, t.Implied,
				t.ExternalCreationDate, t.ExternalLastModifiedDate, 
				t.ExternalCreationUser, t.ExternalLastModifiedUser
		FROM	#tmpIndicatedPage t 
				INNER JOIN dbo.BHLSegment s ON t.BarCode = s.BarCode
				INNER JOIN dbo.BHLItemPage ip ON s.ItemID = ip.ItemID AND t.SequenceOrder = ip.SequenceOrder
				INNER JOIN dbo.BHLPage p ON ip.PageID = p.PageID AND t.FileNamePrefix = p.FileNamePrefix
				LEFT JOIN dbo.BHLIndicatedPage ipg ON p.PageID = ipg.PageID AND t.Sequence = ipg.Sequence
		WHERE	ipg.PageID IS NULL

		SELECT @RowCount = @RowCount + @@ROWCOUNT

		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Indicated Page', 'Insert', @RowCount)
		END

		-- =======================================================================

		-- Insert new page_pagetype records into the production database
		-- Non-virtual items
		INSERT INTO dbo.BHLPagePageType (PageID, PageTypeID, Verified,
			CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
		SELECT	p.PageID, t.PageTypeID, t.Verified, t.ExternalCreationDate, 
				t.ExternalLastModifiedDate, t.ExternalCreationUser, 
				t.ExternalLastModifiedUser
		FROM	#tmpPage_PageType t 
				INNER JOIN dbo.BHLBook b ON t.BarCode = b.BarCode
				INNER JOIN dbo.BHLItemPage ip ON b.ItemID = ip.ItemID AND t.SequenceOrder = ip.SequenceOrder
				INNER JOIN dbo.BHLPage p ON ip.PageID = p.PageID AND t.FileNamePrefix = p.FileNamePrefix
				LEFT JOIN dbo.BHLPagePageType ppt ON p.PageID = ppt.PageID AND t.PageTypeID = ppt.PageTypeID
		WHERE	ppt.PageID IS NULL
				
		SELECT @RowCount = @@ROWCOUNT

		-- Segments on virtual items
		INSERT INTO dbo.BHLPagePageType (PageID, PageTypeID, Verified,
			CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
		SELECT	p.PageID, t.PageTypeID, t.Verified, t.ExternalCreationDate, 
				t.ExternalLastModifiedDate, t.ExternalCreationUser, 
				t.ExternalLastModifiedUser
		FROM	#tmpPage_PageType t 
				INNER JOIN dbo.BHLSegment s ON t.BarCode = s.BarCode
				INNER JOIN dbo.BHLItemPage ip ON s.ItemID = ip.ItemID AND t.SequenceOrder = ip.SequenceOrder
				INNER JOIN dbo.BHLPage p ON ip.PageID = p.PageID AND t.FileNamePrefix = p.FileNamePrefix
				LEFT JOIN dbo.BHLPagePageType ppt ON p.PageID = ppt.PageID AND t.PageTypeID = ppt.PageTypeID
		WHERE	ppt.PageID IS NULL
				
		SELECT @RowCount = @RowCount + @@ROWCOUNT

		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Page PageType', 'Insert', @RowCount)
		END;

		-- =======================================================================

		-- Get the Start Page IDs for each item/segment that is a part of a Virtual Item

		WITH cteFirstPages AS (
			SELECT	s.SegmentID, MIN(ip.SequenceOrder) AS FirstPage
			FROM	#tmpItem i 
					INNER JOIN dbo.BHLSegment s ON i.BarCode = s.BarCode
					INNER JOIN dbo.BHLItemPage ip ON s.ItemID = ip.ItemID
			GROUP BY s.SegmentID
		)
		UPDATE	dbo.BHLSegment
		SET		StartPageID = ip.PageID,
				LastModifiedDate = GETDATE(),
				LastModifiedUserID = 1
		FROM	dbo.BHLSegment s 
				INNER JOIN cteFirstPages f ON s.SegmentID = f.SegmentID
				INNER JOIN dbo.BHLItemPage ip ON s.ItemID = ip.ItemID AND f.FirstPage = ip.SequenceOrder
		WHERE	s.StartPageID IS NULL

		-- =======================================================================

		-- Get the Start Page IDs for each non-Virtual-Item segment

		SELECT	s.SegmentID, MIN(p.PageSequenceOrder) AS FirstPage 
		INTO	#FirstPages
		FROM	#tmpSegment s INNER JOIN #tmpSegmentPage p 
					ON s.BarCode = p.BarCode 
					AND s.SequenceOrder = p.SegmentSequenceOrder
		GROUP BY s.SegmentID

		UPDATE	#tmpSegment
		SET		StartPageID = ip.PageID
		FROM	#tmpSegment t 
				INNER JOIN #tmpSegmentPage sp ON t.BarCode = sp.BarCode
				INNER JOIN #FirstPages f ON t.SegmentID = f.SegmentID AND sp.PageSequenceOrder = f.FirstPage
				INNER JOIN dbo.BHLBook b ON t.BarCode = b.BarCode
				INNER JOIN dbo.BHLItemPage ip ON b.ItemID = ip.ItemID AND f.FirstPage = ip.SequenceOrder				

		-- Insert new segment records into the production database

		DECLARE @SegmentItemID int
		DECLARE @SegmentStatusID int
		DECLARE @ItemSourceID int
		DECLARE @SegmentNote nvarchar(max)
		DECLARE @SegmentGenreID int
		DECLARE @SegmentTitle nvarchar(2000)
		DECLARE @SegmentTranslatedTitle nvarchar(2000)
		DECLARE @SegmentSortTitle nvarchar(2000)
		DECLARE @SegmentContainerTitle nvarchar(2000)
		DECLARE @SegmentPublicationDetails nvarchar(400)
		DECLARE @SegmentPublisherName nvarchar(250)
		DECLARE @SegmentPublisherPlace nvarchar(150)
		DECLARE @SegmentSummary nvarchar(max)
		DECLARE @SegmentVolume nvarchar(100)
		DECLARE @SegmentSeries nvarchar(100)
		DECLARE @SegmentIssue nvarchar(100)
		DECLARE @SegmentEdition nvarchar(400)
		DECLARE @SegmentDate nvarchar(20)
		DECLARE @SegmentPageRange nvarchar(50)
		DECLARE @SegmentStartPageNumber nvarchar(20)
		DECLARE @SegmentEndPageNumber nvarchar(20)
		DECLARE @SegmentStartPageID int
		DECLARE @SegmentLanguageCode nvarchar(10)
		DECLARE @SegmentUrl nvarchar(200)
		DECLARE @SegmentDownloadUrl nvarchar(200)
		DECLARE @SegmentRightsStatus nvarchar(500)
		DECLARE @SegmentRightsStatement nvarchar(500)
		DECLARE @SegmentLicenseName nvarchar(200)
		DECLARE @SegmentLicenseUrl nvarchar(200)

		SET @RowCount = 0

		DECLARE	curInsert CURSOR 
		FOR	SELECT	tmp.SegmentStatusID, isis.BHLItemSourceID, tmp.Notes, tmp.SegmentGenreID,
					tmp.Title, tmp.TranslatedTitle, tmp.SortTitle, tmp.ContainerTitle, tmp.PublicationDetails,
					tmp.PublisherName, tmp.PublisherPlace, tmp.Summary, tmp.Volume, tmp.Series, tmp.Issue,
					tmp.Edition, tmp.[Date], tmp.PageRange, tmp.StartPageNumber, tmp.EndPageNumber, tmp.StartPageID,
					tmp.LanguageCode, tmp.Url, tmp.DownloadUrl, tmp.RightsStatus, tmp.RightsStatement, tmp.LicenseName,
					tmp.LicenseUrl
			FROM	#tmpSegment tmp 
					INNER JOIN dbo.BHLBook b ON tmp.BarCode = b.BarCode
					LEFT JOIN dbo.BHLItemRelationship ir ON b.ItemID = ir.ParentID AND ir.SequenceOrder = tmp.SequenceOrder
					LEFT JOIN dbo.ImportSourceItemSource isis ON tmp.ImportSourceID = isis.ImportSourceID
			WHERE	ir.RelationshipID IS NULL
		
		OPEN curInsert
		FETCH NEXT FROM curInsert INTO @SegmentStatusID, @ItemSourceID, @SegmentNote,
			@SegmentGenreID, @SegmentTitle, @SegmentTranslatedTitle, @SegmentSortTitle, @SegmentContainerTitle,
			@SegmentPublicationDetails, @SegmentPublisherName, @SegmentPublisherPlace, @SegmentSummary, @SegmentVolume,
			@SegmentSeries, @SegmentIssue, @SegmentEdition, @SegmentDate, @SegmentPageRange, @SegmentStartPageNumber,
			@SegmentEndPageNumber, @SegmentStartPageID, @SegmentLanguageCode, @SegmentUrl, @SegmentDownloadUrl,
			@SegmentRightsStatus, @SegmentRightsStatement, @SegmentLicenseName, @SegmentLicenseUrl

		WHILE (@@fetch_status <> -1)
		BEGIN
			IF (@@fetch_status <> -2)
			BEGIN
				INSERT	dbo.BHLItem (ItemTypeID, ItemStatusID, ItemSourceID, Note,
							CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
				VALUES	(20, @SegmentStatusID, @ItemSourceID, @SegmentNote, GETDATE(), GETDATE(), 1, 1)

				SELECT @SegmentItemID = SCOPE_IDENTITY()

				INSERT INTO dbo.BHLSegment (ItemID, SegmentGenreID, 
					Title, TranslatedTitle, SortTitle, ContainerTitle, PublicationDetails,
					PublisherName, PublisherPlace, Summary, Volume, Series, Issue, Edition,
					[Date], PageRange, StartPageNumber, EndPageNumber, StartPageID, LanguageCode, 
					Url, DownloadUrl, RightsStatus, RightsStatement, LicenseName, LicenseUrl, 
					CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
				VALUES (@SegmentItemID, @SegmentGenreID, @SegmentTitle, @SegmentTranslatedTitle, 
						@SegmentSortTitle, @SegmentContainerTitle, @SegmentPublicationDetails,
						@SegmentPublisherName, @SegmentPublisherPlace, @SegmentSummary, @SegmentVolume, 
						@SegmentSeries, @SegmentIssue, @SegmentEdition, @SegmentDate, @SegmentPageRange, 
						@SegmentStartPageNumber, @SegmentEndPageNumber, @SegmentStartPageID, 
						@SegmentLanguageCode, @SegmentUrl, @SegmentDownloadUrl, @SegmentRightsStatus, 
						@SegmentRightsStatement, @SegmentLicenseName, @SegmentLicenseUrl, 
						GETDATE(), GETDATE(), 1, 1)

				SELECT @RowCount = @RowCount + 1
			END

			FETCH NEXT FROM curInsert INTO @SegmentStatusID, @ItemSourceID, @SegmentNote,
				@SegmentGenreID, @SegmentTitle, @SegmentTranslatedTitle, @SegmentSortTitle, @SegmentContainerTitle,
				@SegmentPublicationDetails, @SegmentPublisherName, @SegmentPublisherPlace, @SegmentSummary, @SegmentVolume,
				@SegmentSeries, @SegmentIssue, @SegmentEdition, @SegmentDate, @SegmentPageRange, @SegmentStartPageNumber,
				@SegmentEndPageNumber, @SegmentStartPageID, @SegmentLanguageCode, @SegmentUrl, @SegmentDownloadUrl,
				@SegmentRightsStatus, @SegmentRightsStatement, @SegmentLicenseName, @SegmentLicenseUrl
		END

		CLOSE curInsert
		DEALLOCATE curInsert

		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Item', 'Insert', @RowCount)

			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Segment', 'Insert', @RowCount)
		END

		-- =======================================================================

		-- Insert new segment institution records into the production database
		DECLARE @ContributorInstitutionRoleID int
		SELECT	@ContributorInstitutionRoleID = InstitutionRoleID FROM dbo.BHLInstitutionRole WHERE InstitutionRoleName = 'Contributor'

		INSERT INTO dbo.BHLItemInstitution (ItemID, InstitutionCode, InstitutionRoleID)
		SELECT	s.ItemID, t.InstitutionCode, @ContributorInstitutionRoleID
		FROM	#tmpSegment t
				INNER JOIN dbo.BHLBook b ON t.BarCode = b.BarCode
				INNER JOIN dbo.BHLItemRelationship ir ON b.ItemID = ir.ParentID AND t.SequenceOrder = ir.SequenceOrder
				INNER JOIN dbo.BHLSegment s ON ir.ChildID = s.ItemID
				LEFT JOIN dbo.BHLItemInstitution inst ON s.ItemID = inst.ItemID AND t.InstitutionCode = inst.InstitutionCode
		WHERE	inst.ItemInstitutionID IS NULL

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Item Institution', 'Insert', @RowCount)
		END

		-- =======================================================================

		-- Insert new segment page records into the production database
		INSERT INTO dbo.BHLItemPage (ItemID, PageID, SequenceOrder, CreationDate,
			LastModifiedDate, CreationUserID, LastModifiedUserID)
		SELECT	s.ItemID, bip.PageID, t.SegmentPageSequenceOrder, GETDATE(), GETDATE(), 1, 1
		FROM	#tmpSegmentPage t
				INNER JOIN dbo.BHLBook b ON t.BarCode = b.BarCode
				INNER JOIN dbo.BHLItemRelationship ir ON b.ItemID = ir.ParentID and t.SegmentSequenceOrder = ir.SequenceOrder
				INNER JOIN dbo.BHLSegment s ON ir.ChildID = s.ItemID
				INNER JOIN dbo.BHLItemPage bip ON b.ItemID = bip.ItemID AND t.PageSequenceOrder = bip.SequenceOrder
				LEFT JOIN dbo.BHLItemPage sip ON s.ItemID = sip.ItemID AND t.SegmentPageSequenceOrder = sip.SequenceOrder
		WHERE	sip.ItemPageID IS NULL

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Segment Page', 'Insert', @RowCount)
		END

		-- =======================================================================

		-- Insert new segmentidentifier records into the production database
		INSERT INTO dbo.BHLItemIdentifier (ItemID, IdentifierID,
			IdentifierValue, CreationDate, LastModifiedDate)
		SELECT DISTINCT s.ItemID, id.IdentifierID, t.IdentifierValue, GETDATE(), GETDATE()
		FROM	#tmpSegmentIdentifier t
				INNER JOIN dbo.BHLIdentifier id ON t.IdentifierName = id.IdentifierName
				INNER JOIN dbo.BHLBook b ON t.BarCode = b.BarCode
				INNER JOIN dbo.BHLItemRelationship ir ON b.ItemID = ir.ParentID AND ir.SequenceOrder = t.SegmentSequenceOrder
				INNER JOIN dbo.BHLSegment s ON ir.ChildID = s.ItemID
				LEFT JOIN dbo.BHLItemIdentifier si
					ON s.ItemID = si.ItemID
					AND id.IdentifierID = si.IdentifierID
					AND t.IdentifierValue = si.IdentifierValue
		WHERE	si.ItemIdentifierID IS NULL

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Segment Identifier', 'Insert', @RowCount)
		END

		-- =======================================================================

		-- Insert new authors of segments into the production database
		DECLARE @StartDate nvarchar(25)
		DECLARE @EndDate nvarchar(25)
		DECLARE @FullName nvarchar(300)
		DECLARE @LastName nvarchar(150)
		DECLARE @FirstName nvarchar(150)
		SET @RowCount = 0

		DECLARE	curInsert CURSOR 
		FOR SELECT	StartDate, EndDate, FullName, LastName, FirstName
			FROM	#tmpSegmentAuthor
			WHERE	ProductionAuthorID IS NULL
			GROUP BY StartDate, EndDate, FullName, LastName, FirstName
		
		OPEN curInsert
		FETCH NEXT FROM curInsert INTO @StartDate, @EndDate, @FullName, @LastName, @FirstName

		WHILE (@@fetch_status <> -1)
		BEGIN
			IF (@@fetch_status <> -2)
			BEGIN

				-- Insert a new author record into the production database
				INSERT INTO dbo.BHLAuthor (AuthorTypeID, StartDate, EndDate, IsActive, 
					CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
				VALUES (1, @StartDate, @EndDate, 1, GETDATE(), GETDATE(), 1, 1)
						
				-- Save the ID of the newly inserted author record
				SELECT @NewAuthorID = SCOPE_IDENTITY()
				
				UPDATE	#tmpSegmentAuthor
				SET		ProductionAuthorID = @NewAuthorID,
						ProductionAuthorNameID = 0
				WHERE	StartDate = @StartDate
				AND		EndDate = @EndDate
				AND		FullName = @FullName
				AND		LastName = @LastName
				AND		FirstName = @FirstName
				AND		ProductionAuthorID IS NULL

				SET @RowCount = @RowCount + 1
			END

			FETCH NEXT FROM curInsert INTO @StartDate, @EndDate, @FullName, @LastName, @FirstName
		END

		CLOSE curInsert
		DEALLOCATE curInsert

		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Author', 'Insert', @RowCount)
		END

		-- =======================================================================
		
		-- Insert new AuthorName records into the production database
		INSERT INTO dbo.BHLAuthorName (AuthorID, FullName, LastName, FirstName, IsPreferredName,
			CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
		SELECT DISTINCT
				ProductionAuthorID,
				FullName,
				LastName,
				FirstName,
				1,
				GETDATE(),
				GETDATE(),
				1, 1
		FROM	#tmpSegmentAuthor
		WHERE	ProductionAuthorNameID = 0

		-- =======================================================================

		-- Insert new segment author records into the production database
		INSERT INTO dbo.BHLItemAuthor (ItemID, AuthorID, SequenceOrder, CreationDate,
			LastModifiedDate, CreationUserID, LastModifiedUserID)
		SELECT	s.ItemID, t.ProductionAuthorID, MIN(t.SequenceOrder), GETDATE(), GETDATE(), 1, 1
		FROM	#tmpSegmentAuthor t
				INNER JOIN dbo.BHLBook b ON t.BarCode = b.BarCode
				INNER JOIN dbo.BHLItemRelationship ir ON b.ItemID = ir.ParentID AND ir.SequenceOrder = t.SegmentSequenceOrder
				INNER JOIN dbo.BHLSegment s ON ir.ChildID = s.ItemID
				LEFT JOIN dbo.BHLItemAuthor a ON s.ItemID = a.ItemID AND t.ProductionAuthorID = a.AuthorID
		WHERE a.ItemAuthorID IS NULL
		GROUP BY s.ItemID, t.ProductionAuthorID
		
		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Segment Author', 'Insert', @RowCount)
		END

		-- =======================================================================

		-- Insert new segment author identifier records into the production database
		INSERT INTO dbo.BHLAuthorIdentifier (AuthorID, IdentifierID, IdentifierValue,
			CreationDate, LastModifiedDate, CreationUserID, LastModifiedUserID)
		SELECT	a.ProductionAuthorID, t.ProductionIdentifierID, t.IdentifierValue, 
				GETDATE(), GETDATE(), 1, 1
		FROM	#tmpSegmentAuthorIdentifier t
				INNER JOIN #tmpSegmentAuthor a 
					ON t.BarCode = a.BarCode 
					AND t.SegmentSequenceOrder = a.SegmentSequenceOrder
					AND t.SequenceOrder = a.Sequenceorder
				LEFT JOIN dbo.BHLAuthorIdentifier i
					ON a.ProductionAuthorID = i.AuthorID
					AND t.ProductionIdentifierID = i.IdentifierID
					AND t.IdentifierValue = i.IdentifierValue
		WHERE	i.AuthorIdentifierID IS NULL

		SELECT @RowCount = @@ROWCOUNT
		IF (@RowCount > 0)
		BEGIN
			INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
			VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Success', 'Segment Author Identifier', 'Insert', @RowCount)
		END

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

		UPDATE	dbo.ItemCreator
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.ItemCreator c INNER JOIN #tmpItemCreator t ON c.ItemCreatorID = t.ItemCreatorID

		UPDATE	dbo.ItemCreatorIdentifier
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.ItemCreatorIdentifier i INNER JOIN #tmpItemCreatorIdentifier t ON i.ItemCreatorIdentifierID = t.ItemCreatorIdentifierID

		UPDATE	dbo.ItemIdentifier
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.ItemIdentifier ii INNER JOIN #tmpItemIdentifier t ON ii.ItemIdentifierID = t.ItemIdentifierID

		UPDATE	dbo.ItemKeyword
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.ItemKeyword k INNER JOIN #tmpItemKeyword t ON k.ItemKeywordID = t.ItemKeywordID

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

		UPDATE	dbo.Segment
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.Segment s INNER JOIN #tmpSegment t ON s.SegmentID = t.SegmentID

		UPDATE	dbo.SegmentPage
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.SegmentPage p INNER JOIN #tmpSegmentPage t ON p.SegmentPageID = t.SegmentPageID

		UPDATE	dbo.SegmentIdentifier
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.SegmentIdentifier i INNER JOIN #tmpSegmentIdentifier t ON i.SegmentIdentifierID = t.SegmentIdentifierID

		UPDATE	dbo.SegmentAuthor
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.SegmentAuthor a INNER JOIN #tmpSegmentAuthor t ON a.SegmentAuthorID = t.SegmentAuthorID

		UPDATE	dbo.SegmentAuthorIdentifier
		SET		ImportStatusID = @StatusComplete, ProductionDate = @ProductionDate
		FROM	dbo.SegmentAuthorIdentifier i INNER JOIN #tmpSegmentAuthorIdentifier t ON i.SegmentAuthorIdentifierID = t.SegmentAuthorIdentifierID

		-- =======================================================================

		COMMIT TRAN

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
		INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
		VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Error', '', '', 0)

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
	SELECT * FROM #tmpTitleLanguage
	SELECT * FROM #tmpTitle_Creator
	SELECT * FROM #tmpItem
	SELECT * FROM #tmpItemCreator
	SELECT * FROM #tmpItemCreatorIdentifier
	SELECT * FROM #tmpItemIdentifier
	SELECT * FROM #tmpItemKeyword
	SELECT * FROM #tmpItemLanguage
	SELECT * FROM #tmpPage
	SELECT * FROM #tmpIndicatedPage
	SELECT * FROM #tmpPage_PageType
	SELECT * FROM #tmpSegment
	SELECT * FROM #tmpSegmentPage
	SELECT * FROM #tmpSegmentIdentifier
	SELECT * FROM #tmpSegmentAuthor
	SELECT * FROM #tmpSegmentAuthorIdentifier
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
	DROP TABLE #tmpTitleLanguage
	DROP TABLE #tmpTitle_Creator
	DROP TABLE #tmpItem
	DROP TABLE #tmpItemCreator
	DROP TABLE #tmpItemCreatorIdentifier
	DROP TABLE #tmpItemIdentifier
	DROP TABLE #tmpItemKeyword
	DROP TABLE #tmpItemLanguage
	DROP TABLE #tmpPage
	DROP TABLE #tmpIndicatedPage
	DROP TABLE #tmpPage_PageType
	DROP TABLE #tmpSegment
	DROP TABLE #tmpSegmentPage
	DROP TABLE #tmpSegmentIdentifier
	DROP TABLE #tmpSegmentAuthor
	DROP TABLE #tmpSegmentAuthorIdentifier
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 ROLLBACK TRAN

	-- Record the error
	INSERT INTO dbo.ImportError (KeyType, KeyValue, ErrorDate, Number, Severity, 
		State, [Procedure], Line, [Message])
	SELECT	'BarCode', @BarCode, GETDATE(), ERROR_NUMBER(), ERROR_SEVERITY(),
		ERROR_STATE(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE()

	-- Log results of import
	INSERT dbo.ImportLog (ImportDate, ImportSourceID, BarCode, ImportResult, TableName, [Action], [Rows]) 
	VALUES (@ProductionDate, @ImportSourceID, @BarCode, 'Error', '', '', 0)

	SELECT 0 AS Result
END CATCH

SET NOCOUNT OFF

END

GO
