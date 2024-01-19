CREATE PROCEDURE [dbo].[IAItemPublishToImportTables]

@ItemID int

AS
BEGIN

------------------------------------------------------------------------------
--
--	NOTES: This script assumes that biodiversity.org is the authoritative 
--	site, not Internet Archive.  This means that only INSERT operations
--	for new items are performed.  No UPDATES or DELETES are passed into the
--	import tables.
--
--	Once an item achieves an item status of "Approved", it should be loaded
--	into the import tables.  After that has been done, it WILL
--	NOT BE LOADED again (for inserts, updates, or deletes).  We will still
--	be monitoring the Internet Archive for changes to the metadata files
--	associated with that item, so at some point in the future we can
--	re-evalute whether or not we need to incorporate changes made at Internet
--	Archive after we have loaded the item into the biodiversity.org database.
--
--  UPDATE 9/2/2008: The above note is no longer entirely true.  It IS still 
--	true that biodiversity.org is the authoritative site, and with only a 
--	few exceptions, UPDATES/DELETES are not performed on IA data.  However, 
--	because title resolution does not happen here, ALL data is loaded to the 
--	import tables. The INSERT/UPDATE decision is made when titles are resolved
--	and data is ready to be moved from the import tables to the production 
--	tables.
--
------------------------------------------------------------------------------
SET NOCOUNT ON

BEGIN TRY

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Validate that we have the correct data for publishing
	DECLARE @Msg varchar(400)

	IF (@ItemID IS NULL)
	BEGIN
		SET @Msg = 'No Item ID specified'
		RAISERROR (@Msg, 16, 1)
	END

	DECLARE @NumPages int
	DECLARE @NumScandata int

	SELECT @NumPages = COUNT(*) FROM IAPage WHERE ItemID = @ItemID
	SELECT @NumScandata = COUNT(*) FROM IAScandata WHERE ItemID = @ItemID

	IF @NumPages = 0
	BEGIN
		-- Remove all downloaded data for this item (we need to reload it)
		BEGIN TRY
			BEGIN TRANSACTION
			DELETE FROM dbo.IADCMetadata WHERE ItemID = @ItemID
			DELETE FROM dbo.IAItemIdentifier WHERE ItemID = @ItemID
			DELETE FROM dbo.IAMarcSubField WHERE MarcDataFieldID in (SELECT MarcDataFieldID FROM dbo.IAMarcDataField WHERE MARCID IN (SELECT MARCID FROM dbo.IAMarc WHERE ItemID = @ItemID))
			DELETE FROM dbo.IAMarcDataField WHERE MarcID IN (SELECT MarcID FROM dbo.IAMarc WHERE ItemID = @ItemID)
			DELETE FROM dbo.IAMarcControl WHERE MarcID IN (SELECT MarcID FROM dbo.IAMarc WHERE ItemID = @ItemID)
			DELETE FROM dbo.IAMarc WHERE ItemID = @ItemID
			DELETE FROM dbo.IAPage WHERE ItemID = @ItemID
			DELETE FROM dbo.IASegmentAuthor WHERE SegmentID IN (SELECT SegmentID FROM dbo.IASegment WHERE ItemID = @ItemID)
			DELETE FROM dbo.IASegmentPage WHERE SegmentID IN (SELECT SegmentID FROM dbo.IASegment WHERE ItemID = @ItemID)
			DELETE FROM dbo.IASegment WHERE ItemID = @ItemID
			DELETE FROM dbo.IAScandataAltPageNumber WHERE ScandataID IN (SELECT ScandataID FROM dbo.IAScandata WHERE ItemID = @ItemID)
			DELETE FROM dbo.IAScandataAltPageType WHERE ScandataID IN (SELECT ScandataID FROM dbo.IAScandata WHERE ItemID = @ItemID)
			DELETE FROM dbo.IAScandata WHERE ItemID = @ItemID
			UPDATE dbo.IAFile SET RemoteFileLastModifiedDate = '1/1/1980' WHERE ItemID = @ItemID AND RemoteFileLastModifiedDate IS NOT NULL
			UPDATE dbo.IAItem SET LastXMLDataHarvestDate = NULL, LastModifiedDate = GETDATE() WHERE ItemID = @ItemID
			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION
		END CATCH
	
		SET @Msg = '0 Page records'
		RAISERROR (@Msg, 16, 1)
	END

	IF @NumPages < @NumScandata 
	BEGIN
		-- Clean up "extra" scandata records
		DELETE FROM IAScandataAltPageNumber
		WHERE ScandataID IN (
			SELECT	s.ScandataID
			FROM	dbo.IAPage p RIGHT JOIN dbo.IAScandata s
						ON p.ItemID = s.ItemID
					AND p.Sequence = s.Sequence
			WHERE	p.PageID IS NULL
			AND		s.ItemID = @ItemID
			)

		DELETE FROM IAScandataAltPageType
		WHERE ScandataID IN (
			SELECT	s.ScandataID
			FROM	dbo.IAPage p RIGHT JOIN dbo.IAScandata s
						ON p.ItemID = s.ItemID
					AND p.Sequence = s.Sequence
			WHERE	p.PageID IS NULL
			AND		s.ItemID = @ItemID
			)
		
		DELETE FROM IAScandata 
		WHERE ScandataID IN (
			SELECT	s.ScandataID
			FROM	dbo.IAPage p RIGHT JOIN dbo.IAScandata s
						ON p.ItemID = s.ItemID
					AND p.Sequence = s.Sequence
			WHERE	p.PageID IS NULL
			AND		s.ItemID = @ItemID
			)

		-- Don't raise an error here (we want to continue processing), but do log what
		-- we've done to the error table.
		SET @Msg = 'Fewer Page records (' + CONVERT(VARCHAR(10), @NumPages) + 
				') than Scandata records (' + CONVERT(VARCHAR(10), @NumScandata) + '). ' +
				'Removed extra Scandata records.'
		INSERT INTO dbo.IAItemError (ItemID, ErrorDate, Number, Severity, State, 
			[Procedure], Line, [Message])
		SELECT	@ItemID, GETDATE(), NULL, NULL, NULL, 'IAItemPublishToImportTables', NULL, @Msg
	END

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Create temp tables

	CREATE TABLE #tmpTitle (
		[ItemID] int NOT NULL,
		[IAIdentifier] [nvarchar](200) NOT NULL,
		[MARCBibID] [nvarchar](50) NOT NULL,
		[MARCLeader] [nvarchar](24) NULL,
		[FullTitle] [ntext] NOT NULL,
		[ShortTitle] [nvarchar](255) NULL,
		[UniformTitle] [nvarchar](255) NULL,
		[SortTitle] [nvarchar](65) NULL,
		[PartNumber] [nvarchar](255) NULL,
		[PartName] [nvarchar](255) NULL,
		[CallNumber] [nvarchar](100) NULL,
		[PublicationDetails] [nvarchar](255) NULL,
		[StartYear] [smallint] NULL,
		[EndYear] [smallint] NULL,
		[Datafield_260_a] [nvarchar](150) NULL,
		[Datafield_260_b] [nvarchar](255) NULL,
		[Datafield_260_c] [nvarchar](100) NULL,
		[InstitutionCode] [nvarchar](10) NULL,
		[LanguageCode] [nvarchar](10) NULL,
		[OriginalCatalogingSource] [nvarchar](100) NULL,
		[EditionStatement] [nvarchar](450) NULL,
		[CurrentPublicationFrequency] [nvarchar](100) NULL
		)

	CREATE TABLE #tmpKeyword
		(
		[ItemID] int NOT NULL,
		[TitleID] [int] NOT NULL,
		[TagText] [nvarchar](200) NOT NULL,
		[MarcDataFieldTag] [nvarchar](50) NULL,
		[MarcSubFieldCode] [nvarchar](50) NULL,
		)

	CREATE TABLE #tmpIdentifier
		(
		[ItemID] int NOT NULL,
		[IdentifierName] [nvarchar](40) NOT NULL,
		[IdentifierValue] [nvarchar](125) NULL,
		)

	CREATE TABLE #tmpTitleAssociation(
		[Sequence] [int] NOT NULL,
		[ItemID] [int] NOT NULL,
		[MARCDataFieldID] [int] NOT NULL,
		[MARCTag] [nvarchar](20) NOT NULL,
		[MARCIndicator1] [nchar](1) NOT NULL DEFAULT (''),
		[MARCIndicator2] [nchar](1) NOT NULL DEFAULT (''),
		[Title] [nvarchar](500) NOT NULL DEFAULT (''),
		[Section] [nvarchar](500) NOT NULL DEFAULT (''),
		[Volume] [nvarchar](500) NOT NULL DEFAULT (''),
		[Heading] [nvarchar](500) NOT NULL DEFAULT (''),
		[Publication] [nvarchar](500) NOT NULL DEFAULT (''),
		[Relationship] [nvarchar](500) NOT NULL DEFAULT ('')
		)

	CREATE TABLE #tmpTitleVariant(
		[ItemID] [int] NOT NULL,
		[MARCDataFieldID] [int] NOT NULL,
		[MARCTag] [nvarchar](20) NOT NULL,
		[MARCIndicator2] [nchar](1) NOT NULL DEFAULT (''),
		[Title] [nvarchar](MAX) NOT NULL DEFAULT (''),
		[TitleRemainder] [nvarchar](MAX) NOT NULL DEFAULT(''),
		[PartNumber] [nvarchar](255) NOT NULL DEFAULT(''),
		[PartName] [nvarchar](255) NOT NULL DEFAULT('')
		)

	CREATE TABLE #tmpTitleNote (
		[ItemID] int NOT NULL,
		[MARCDataFieldID] int NOT NULL,
		[NoteText] nvarchar(max) NOT NULL,
		[MarcDataFieldTag] nvarchar(5) NULL,
		[MarcIndicator1] nvarchar(5) NULL,
		[NoteSequence] smallint NULL,
	)

	CREATE TABLE #tmpTitleLanguage(
		[ItemID] [int] NOT NULL,
		[LanguageCode] [nvarchar](10) NOT NULL DEFAULT('')
		)		

	CREATE TABLE #tmpCreator (
		[ItemID] [int] NOT NULL,
		[TitleID] [int] NOT NULL,
		[CreatorRoleTypeID] [int] NOT NULL,
		[CreatorName] [nvarchar](255) NOT NULL,
		[DOB] [nvarchar](50) NULL,
		[DOD] [nvarchar](50) NULL,
		[MARCDataFieldID] [int] NULL,
		[MARCDataFieldTag] [nvarchar](3) NULL,
		[MARCCreator_a] [nvarchar](450) NULL,
		[MARCCreator_b] [nvarchar](450) NULL,
		[MARCCreator_c] [nvarchar](450) NULL,
		[MARCCreator_d] [nvarchar](450) NULL,
		[MARCCreator_e] [nvarchar](450) NULL,
		[MARCCreator_q] [nvarchar](450) NULL,
		[MARCCreator_t] [nvarchar](450) NULL,
		[MARCCreator_5] [nvarchar](450) NULL,
		[MARCCreator_Full] [nvarchar](450) NULL,
		[SequenceOrder] [smallint] NULL
		)

	CREATE TABLE #tmpItem(
		[ItemID] int NOT NULL,
		[IAIdentifier] [nvarchar](200) NOT NULL,
		[MARCBibID] [nvarchar](50) NOT NULL,
		[TitleID] [int] NOT NULL,
		[BarCode] [nvarchar](200) NOT NULL,
		[ItemSequence] [smallint] NULL,
		[MaxExistingItemSequence] [smallint] NULL DEFAULT(0),	-- highest existing production sequence for this barcode
		[MARCItemID] [nvarchar](200) NULL,
		[Volume] [nvarchar](100) NULL,
		[Issue] [nvarchar](100) NOT NULL DEFAULT(''),
		[InstitutionCode] [nvarchar](10) NULL,
		[LanguageCode] [nvarchar](10) NULL,
		[Sponsor] [nvarchar](100) NULL,
		[VaultID] [int] NULL,
		[ItemStatusID] [int] NOT NULL DEFAULT(10),
		[ScanningUser] [nvarchar](100) NULL,
		[ScanningDate] [datetime] NULL,
		[PublicationDetails] [nvarchar](400) NOT NULL DEFAULT(''),
		[PublisherName] [nvarchar](250) NOT NULL DEFAULT(''),
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
		[ScanningInstitutionCode] [nvarchar](10) NULL,
		[RightsHolderCode] [nvarchar](10) NULL,
		[ItemDescription] [nvarchar](max) NULL,
		[EndYear] [nvarchar](20) NULL,
		[StartVolume] [nvarchar](10) NULL,
		[EndVolume] [nvarchar](10) NULL,
		[StartIssue] [nvarchar](10) NULL,
		[EndIssue] [nvarchar](10) NULL,
		[StartNumber] [nvarchar](10) NULL,
		[EndNumber] [nvarchar](10) NULL,
		[StartSeries] [nvarchar](10) NULL,
		[EndSeries] [nvarchar](10) NULL,
		[StartPart] [nvarchar](10) NULL,
		[EndPart] [nvarchar](10) NULL,
		[PageProgression] [nvarchar](10) NULL,
		[VirtualVolume] [nvarchar](100) NULL,
		[VirtualTitleID] [int] NULL,
		[VirtualVolumeSegmentDate] [nvarchar](20) NOT NULL DEFAULT(''),
		[Summary] [nvarchar](max) NOT NULL DEFAULT(''),
		[SegmentGenreID] int NULL
		)

	CREATE TABLE #tmpItemLanguage(
		[ItemID] [int] NOT NULL,
		[BarCode] [nvarchar](200) NOT NULL,
		[LanguageCode] [nvarchar](10) NOT NULL DEFAULT('')
		)		

	CREATE TABLE #tmpPage (
		[ItemID] [int] NOT NULL,
		[BHLItemID] [int] NOT NULL,
		[BarCode] [nvarchar](200) NOT NULL,
		[FileNamePrefix] [nvarchar](200) NOT NULL,
		[SequenceOrder] [int] NULL,
		[SequenceOrderCorrected] [int] NULL,
		[Year] nvarchar(20) NULL,
		[Volume] nvarchar(20) NULL,
		[Issue] nvarchar(20) NULL,
		[IssuePrefix] nvarchar(20) NULL,
		[LastModifiedUserID] [int] NULL,
		[ExternalURL] [nvarchar](500) NULL,
		[AltExternalURL] [nvarchar](500) NULL
		)

	CREATE TABLE #tmpPage_PageType (
		[BarCode] [nvarchar](200) NOT NULL,
		[FileNamePrefix] [nvarchar](200) NOT NULL,
		[SequenceOrder] [int] NOT NULL,
		[SequenceOrderCorrected] [int] NULL,
		[PageTypeID] [int] NOT NULL
		)

	CREATE TABLE #tmpIndicatedPage (
		[BarCode] [nvarchar](200) NOT NULL,
		[FileNamePrefix] [nvarchar](200) NOT NULL,
		[SequenceOrder] [int] NOT NULL,
		[SequenceOrderCorrected] [int] NULL,
		[PageID] [int] NOT NULL,
		[Sequence] [smallint] NOT NULL DEFAULT(1),
		[PagePrefix] [nvarchar](40) NULL,
		[PageNumber] [nvarchar](20) NULL,
		[Implied] [bit] NOT NULL DEFAULT(0)
		)

	CREATE TABLE #tmpSegment (
		ItemID int NOT NULL,
		BarCode nvarchar(200) NOT NULL,
		SequenceOrder smallint NOT NULL DEFAULT ((1)),
		SegmentGenreID int NOT NULL,
		Title nvarchar(2000) NOT NULL DEFAULT (''),
		SortTitle nvarchar(2000) NOT NULL DEFAULT (''),
		ContainerTitle nvarchar(2000) NOT NULL DEFAULT (''),
 		PublicationDetails nvarchar(400) NOT NULL DEFAULT (''),
		PublisherName nvarchar(250) NOT NULL DEFAULT (''),
		PublisherPlace nvarchar(150) NOT NULL DEFAULT (''),
		Volume nvarchar(100) NOT NULL DEFAULT (''),
		Series nvarchar(100) NOT NULL DEFAULT (''),
		Issue nvarchar(100) NOT NULL DEFAULT (''),
		Edition nvarchar(400) NOT NULL DEFAULT (''),
		[Date] nvarchar(20) NOT NULL DEFAULT (''),
		PageRange nvarchar(50) NOT NULL DEFAULT (''),
		StartPageNumber nvarchar(20) NOT NULL DEFAULT (''),
		EndPageNumber nvarchar(20) NOT NULL DEFAULT (''),
		InstitutionCode nvarchar(10) NULL,
		LanguageCode nvarchar(10) NULL,
		RightsStatus nvarchar(500) NOT NULL DEFAULT (''),
		RightsStatement nvarchar(500) NOT NULL DEFAULT (''),
		LicenseName nvarchar(200) NOT NULL DEFAULT (''),
		LicenseUrl nvarchar(200) NOT NULL DEFAULT ('')
		)

	CREATE TABLE #tmpSegmentPage (
		ItemID int NOT NULL,
		BarCode nvarchar(200) NOT NULL,
		SegmentSequenceOrder smallint NOT NULL,
		PageSequenceOrder int NULL
		)

	CREATE TABLE #tmpSegmentIdentifier
		(
		ItemID int NOT NULL,
		BarCode nvarchar(200) NOT NULL,
		SegmentSequenceOrder smallint NOT NULL,
		IdentifierName nvarchar(40) NOT NULL,
		IdentifierValue nvarchar(125) NULL,
		)

	CREATE TABLE #tmpSegmentAuthor
		(
		ItemID int NOT NULL,
		BarCode	nvarchar(200) NOT NULL DEFAULT(''),
		SegmentSequenceOrder int NOT NULL,
		SequenceOrder int NOT NULL,
		BHLAuthorID int NULL,
		LastName nvarchar(150) NOT NULL DEFAULT(''),
		FirstName nvarchar(150) NOT NULL DEFAULT(''),
		StartDate nvarchar(25) NOT NULL DEFAULT(''),
		EndDate nvarchar(25) NOT NULL DEFAULT('')
		)

	CREATE TABLE #tmpSegmentAuthorIdentifier
		(
		ItemID int NOT NULL,
		BarCode	nvarchar(200) NOT NULL DEFAULT(''),
		SegmentSequenceOrder int NOT NULL,
		SequenceOrder int NOT NULL,
		ProductionIdentifierID int NOT NULL,
		IdentifierValue nvarchar(125) NOT NULL DEFAULT('')
		)

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Titles

	-- Get the initial list of titles
	INSERT	#tmpTitle (ItemID, IAIdentifier, MARCBibID, FullTitle)
	SELECT	ItemID, IAIdentifier, '', '' 
	FROM	dbo.IAItem
	WHERE	ItemStatusID = 30	-- Approved
	AND		ItemID = @ItemID
	AND		VirtualTitleID IS NULL	-- If not NULL, this item is a segment in a Virtual Item

	-- Only harvest title data for non-Virtual Items
	IF EXISTS(SELECT ItemID FROM #tmpTitle)
	BEGIN

		-- Get the MARC leader and unique MARC BIB ID (this will change when 
		-- Internet Archive can provide us with a "real" unique title identifier)
		UPDATE	#tmpTitle
		SET		MARCBibID = REPLACE(REPLACE(m.Leader, ' ', 'x'), '|', 'x'),
				MARCLeader = m.Leader
		FROM	#tmpTitle t INNER JOIN dbo.IAMarc m
					ON t.ItemID = m.ItemID

		-- Get the start year, end year, and language code from the MARC control data.
		-- Only read the 2nd date (EndYear) if the Date Type is NOT one of p, r, s, t.
		UPDATE	#tmpTitle
		SET		StartYear = CASE WHEN ISNUMERIC(SUBSTRING(c.[Value], 8, 4)) = 1 THEN SUBSTRING(c.[Value], 8, 4) ELSE NULL END,
				EndYear = CASE WHEN ISNUMERIC(SUBSTRING(c.[Value], 12, 4)) = 1 AND SUBSTRING(c.[Value], 7, 1) NOT IN ('p', 'r', 's', 't') THEN SUBSTRING(c.[Value], 12, 4) ELSE NULL END,
				LanguageCode = SUBSTRING(c.[Value], 36, 3)
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcControl c
					ON t.ItemID = c.ItemID
		WHERE	c.Tag = '008' 

		-- Make sure the start and end years are within the valid ranges
		UPDATE	#tmpTitle
		SET		StartYear = CASE WHEN ((StartYear>=1400 AND StartYear<=2025) OR StartYear IS NULL) THEN StartYear ELSE NULL END,
				EndYear = CASE WHEN ((EndYear>=1400 AND EndYear<=2025) OR EndYear IS NULL) THEN EndYear ELSE NULL END

		-- Get the publication titles
		UPDATE	#tmpTitle
		SET		ShortTitle = dbo.BHLfnRemoveTrailingPunctuation(SUBSTRING(df.SubFieldValue, 1, 255), DEFAULT)
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField df
					ON t.ItemID = df.ItemID
		WHERE	df.DataFieldTag = '245'
		AND		df.Code = 'a'

		-- Get the uniform title (stored in either MARC 130 or MARC 240)
		UPDATE	#tmpTitle
		SET		UniformTitle = dbo.BHLfnRemoveTrailingPunctuation(SUBSTRING(df.SubFieldValue, 1, 255), DEFAULT)
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField df
					ON t.ItemID = df.ItemID
		WHERE	df.DataFieldTag in ('130', '240')
		AND		df.Code = 'a'

		-- Full Title
		UPDATE	#tmpTitle
		SET		FullTitle = dbo.BHLfnRemoveTrailingPunctuation(LTRIM(RTRIM(dfA.SubFieldValue + ' ' + ISNULL(dfB.SubFieldValue, ''))), DEFAULT)
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField dfA
					ON t.ItemID = dfA.ItemID
					AND dfA.DataFieldTag = '245' 
					AND dfA.Code = 'a'
				LEFT JOIN dbo.vwIAMarcDataField dfB
					ON t.ItemID = dfB.ItemID
					AND dfB.DataFieldTag = '245'
					AND dfB.Code = 'b'

		-- Part Number and Part Name
		UPDATE	#tmpTitle
		SET		PartNumber = dbo.BHLfnRemoveTrailingPunctuation(SUBSTRING(df.SubFieldValue, 1, 255), DEFAULT)
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField df
					ON t.ItemID = df.ItemID
		AND		df.DataFieldTag = '245'
		AND		df.Code = 'n'

		UPDATE	#tmpTitle
		SET		PartName = dbo.BHLfnRemoveTrailingPunctuation(SUBSTRING(df.SubFieldValue, 1, 255), DEFAULT)
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField df
					ON t.ItemID = df.ItemID
		AND		df.DataFieldTag = '245'
		AND		df.Code = 'p';

		-- Get datafield 260/264 information
		/*
		IF     * 260 with blank ind 1 or ind 1=0 or ind 1=1
		OR    ** 264 with blank ind 1 and ind 2=1
		OR    ** 264 with blank ind 1 and ind 2=0
		OR    ** 264 with blank ind 1 and ind 2=3
		THEN *** take first subfield a, b and c and/or 3 to populate the BHL database
		*/
		-- Start by getting the IDs of the appropriate 260/264 Marc fields
		WITH DataField (ItemID, MarcDataFieldID)
		AS
		(
			SELECT	t.ItemID, MIN(MarcDataFieldID) AS MarcDataFieldID
			FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField df
						ON t.ItemID = df.ItemID
			WHERE	(df.DataFieldTag = '260' AND df.Indicator1 IN ('', '0', '1'))
			OR		(df.DataFieldTag = '264' AND df.Indicator1 = '' AND df.Indicator2 IN ('0', '1', '3'))
			GROUP BY t.ItemID
		)
		SELECT	d.ItemID, d.MarcDataFieldID, MIN(v.MarcSubFieldID) AS MarcSubFieldID, v.Code
		INTO	#PublisherInfo
		FROM	DataField d INNER JOIN dbo.vwIAMarcDataField v
					ON d.MarcDataFieldID = v.MarcDataFieldID
		GROUP BY d.ItemID, d.MarcDataFieldID, v.Code

		-- Get the 260/264 values
		UPDATE	#tmpTitle
		SET		Datafield_260_a = dbo.BHLfnRemoveTrailingPunctuation(SUBSTRING(df.SubFieldValue, 1, 150), DEFAULT)
		FROM	#tmpTitle t 
				INNER JOIN #PublisherInfo p ON t.ItemID = p.ItemID
				INNER JOIN dbo.vwIAMarcDataField df	ON p.ItemID = df.ItemID	AND p.MarcSubFieldID = df.MarcSubFieldID
		WHERE	p.Code = 'a'

		UPDATE	#tmpTitle
		SET		Datafield_260_b = dbo.BHLfnRemoveTrailingPunctuation(SUBSTRING(df.SubFieldValue, 1, 255), DEFAULT)
		FROM	#tmpTitle t 
				INNER JOIN #PublisherInfo p ON t.ItemID = p.ItemID
				INNER JOIN dbo.vwIAMarcDataField df	ON p.ItemID = df.ItemID	AND p.MarcSubFieldID = df.MarcSubFieldID
		WHERE	p.Code = 'b'

		UPDATE	#tmpTitle
		SET		Datafield_260_c = dbo.BHLfnRemoveTrailingPunctuation(SUBSTRING(df.SubFieldValue, 1, 100), '[a-zA-Z0-9)\]?!>*%"''-]%')
		FROM	#tmpTitle t 
				INNER JOIN #PublisherInfo p ON t.ItemID = p.ItemID
				INNER JOIN dbo.vwIAMarcDataField df	ON p.ItemID = df.ItemID	AND p.MarcSubFieldID = df.MarcSubFieldID
		WHERE	p.Code = 'c'

		UPDATE	#tmpTitle
		SET		Datafield_260_c = dbo.BHLfnRemoveTrailingPunctuation(SUBSTRING(df.SubFieldValue, 1, 100), '[a-zA-Z0-9)\]?!>*%"''-]%')
		FROM	#tmpTitle t 
				INNER JOIN #PublisherInfo p ON t.ItemID = p.ItemID
				INNER JOIN dbo.vwIAMarcDataField df	ON p.ItemID = df.ItemID	AND p.MarcSubFieldID = df.MarcSubFieldID
		WHERE	p.Code = '3'
		AND		ISNULL(Datafield_260_c, '') = ''

		DROP TABLE #PublisherInfo

		-- Remove start and end brackets ( [ ] ) from publication information
		UPDATE	#tmpTitle
		SET		Datafield_260_a = SUBSTRING(Datafield_260_a, 2, LEN(Datafield_260_a) - 1),
				Datafield_260_b = CASE WHEN ISNULL(Datafield_260_c, '') = '' THEN SUBSTRING(Datafield_260_b, 1, LEN(Datafield_260_b) - 1) ELSE Datafield_260_b END,
				Datafield_260_c = CASE WHEN ISNULL(Datafield_260_c, '') <> '' THEN SUBSTRING(Datafield_260_c, 1, LEN(Datafield_260_c) - 1) ELSE Datafield_260_c END
		WHERE	LEFT(Datafield_260_a, 1) = '['
		AND     CHARINDEX(']', Datafield_260_a, 1) = 0
		AND     (
					(
					RIGHT(Datafield_260_c, 1) = ']'
					AND CHARINDEX('[', Datafield_260_c, 1) = 0
					)
				OR
					(
					RIGHT(Datafield_260_b, 1) = ']'
					AND CHARINDEX('[', Datafield_260_b, 1) = 0
					AND ISNULL(Datafield_260_c, '') = ''
					)
				)

		-- Get publication details
		UPDATE	#tmpTitle
		SET		PublicationDetails = dbo.BHLfnRemoveTrailingPunctuation(RTRIM(
					SUBSTRING(
						ISNULL(Datafield_260_a, '') + CASE WHEN LEN(Datafield_260_a) > 0 THEN ', ' ELSE '' END + 
						ISNULL(Datafield_260_b, '') + CASE WHEN LEN(Datafield_260_b) > 0 THEN ', ' ELSE '' END + 
						ISNULL(Datafield_260_c, ''),
						1, 255
					)
				), DEFAULT)

		-- Get the call number (first check the 050 record, then the 090... use the 050 value if both exist)
		UPDATE	#tmpTitle
		SET		CallNumber = dfA.SubFieldValue + ' ' + ISNULL(dfB.SubFieldValue, '')
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField dfA
					ON t.ItemID = dfA.ItemID
					AND dfA.DataFieldTag = '050' 
					AND dfA.Code = 'a'
				LEFT JOIN dbo.vwIAMarcDataField dfB
					ON t.ItemID = dfB.ItemID
					AND dfB.DataFieldTag = '050'
					AND dfB.Code = 'b'

		UPDATE	#tmpTitle
		SET		CallNumber = dfA.SubFieldValue + ' ' + ISNULL(dfB.SubFieldValue, '')
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField dfA
					ON t.ItemID = dfA.ItemID
					AND dfA.DataFieldTag = '090' 
					AND dfA.Code = 'a'
				LEFT JOIN dbo.vwIAMarcDataField dfB
					ON t.ItemID = dfB.ItemID
					AND dfB.DataFieldTag = '090'
					AND dfB.Code = 'b'
		WHERE	t.CallNumber IS NULL

		-- Get the institution code.  First use the IAScanCenterInstitution
		-- table, which maps IA "contributor" strings to entries in the BHL
		-- Institution table.  If no match is found there, then attempt to 
		-- find a match by comparing the IA "contributor" string to the 
		-- Insitution.InstitutionName values.  Anything left over is assigned
		-- to the "UNKNOWN" contributor.
		UPDATE	#tmpTitle
		SET		InstitutionCode = s.InstitutionCode
		FROM	#tmpTitle t INNER JOIN dbo.IADCMetadata m
					ON t.ItemID = m.ItemID
					AND m.DCElementName = 'contributor'
				INNER JOIN dbo.IAScanCenterInstitution s
					ON m.DCElementValue = s.ScanningCenterCode
	
		UPDATE	#tmpTitle
		SET		InstitutionCode = i.InstitutionCode
		FROM	#tmpTitle t INNER JOIN dbo.IADCMetadata m
					ON t.ItemID = m.ItemID
					AND m.DCElementName = 'contributor'
				INNER JOIN dbo.BHLInstitution i
					ON m.DCElementValue = i.InstitutionName COLLATE Latin1_general_CI_AI -- ignore diacritics for this comparison
		WHERE	t.InstitutionCode IS NULL
	
		-- Don't raise an error for an unknown contributor.  Instead, set
		-- the contributor to "UNKNOWN".  This will allow items to be moved
		-- into production, where they can be corrected by BHL staff.
		UPDATE #tmpTitle SET InstitutionCode = 'UNKNOWN' WHERE InstitutionCode IS NULL		

		-- Get the Original Cataloging Source (only pull the original agency, not any
		-- modifying agencies)
		UPDATE	#tmpTitle
		SET 	OriginalCatalogingSource = m.SubFieldValue
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField m
					ON t.ItemID = m.ItemID
		WHERE	m.DataFieldTag = '040'
		AND		m.Code = 'a'

		-- Get the Edition Statement
		UPDATE	#tmpTitle
		SET		EditionStatement = SUBSTRING(m.SubFieldValue, 1, 450)
		FROM	#tmpTitle t INNER JOIN (
						SELECT	ItemID, MarcDataFieldID, 
								LTRIM(ISNULL(MIN([a]), '') + ' ' + ISNULL(MIN([b]), '')) AS SubFieldValue
						FROM	(
								SELECT	ItemID, MarcDataFieldID, [a], [b]
								FROM	(SELECT * FROM dbo.vwIAMarcDataField
										WHERE DataFieldTag = '250' AND Code IN ('a', 'b')) AS z
								PIVOT	(MIN(SubFieldValue) FOR Code IN ([a], [b])) AS Pvt
								) X
						GROUP BY ItemID, MarcDataFieldID
						) m
					ON t.ItemID = m.ItemID

		-- Get the Current Publication Frequency
		UPDATE	#tmpTitle
		SET		CurrentPublicationFrequency = m.SubFieldValue
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField m
					ON t.ItemID = m.ItemID
		WHERE	m.DataFieldTag = '310'
		AND		m.Code = 'a'

		-- =======================================================================
		-- If any title is still missing key information, see if what we need is
		-- in the metadata tables (instead of the MARC tables)
		UPDATE	#tmpTitle
		SET		LanguageCode = SUBSTRING(m.DCElementValue, 1, 10)
		FROM	#tmpTitle t INNER JOIN dbo.IADCMetadata m
					ON t.ItemID = m.ItemID
					AND m.DCElementName = 'language'
		WHERE	t.LanguageCode = ''
		AND		m.DCElementValue <> ''

		UPDATE	#tmpTitle
		SET		FullTitle = dbo.BHLfnRemoveTrailingPunctuation(m.DCElementValue, DEFAULT),
				ShortTitle = dbo.BHLfnRemoveTrailingPunctuation(SUBSTRING(m.DCElementValue, 1, 255), DEFAULT)
		FROM	#tmpTitle t INNER JOIN dbo.IADCMetadata m
					ON t.ItemID = m.ItemID
					AND m.DCElementName = 'title'
		WHERE	CONVERT(nvarchar(max), t.FullTitle) = ''
		AND		m.DCElementValue <> ''

		UPDATE	#tmpTitle
		SET		PublicationDetails = dbo.BHLfnRemoveTrailingPunctuation(SUBSTRING(m.DCElementValue, 1, 255), DEFAULT),
				Datafield_260_b = dbo.BHLfnRemoveTrailingPunctuation(SUBSTRING(m.DCElementValue, 1, 255), DEFAULT)
		FROM	#tmpTitle t INNER JOIN dbo.IADCMetadata m
					ON t.ItemID = m.ItemID
					AND m.DCElementName = 'publisher'
		WHERE	t.PublicationDetails = ''
		AND		m.DCElementValue <> ''

		UPDATE	#tmpTitle
		SET		StartYear = CASE WHEN ISNUMERIC(SUBSTRING(m.DCElementValue, 1, 4)) = 1 
							THEN SUBSTRING(m.DCElementValue, 1, 4) END
		FROM	#tmpTitle t INNER JOIN dbo.IADCMetadata m
					ON t.ItemID = m.ItemID
					AND m.DCElementName = 'date'
		WHERE	t.StartYear = ''
		AND		m.DCElementValue <> ''
			
		UPDATE	#tmpTitle
		SET		MARCBibID = LEFT(REPLACE(IAIdentifier, '-', ''), 50)
		WHERE	MARCBibID = ''

		-- =======================================================================
		-- If any language codes are unrecognized, replace them with NULL.  In
		-- the vast majority of cases, the code is unrecognized because it is
		-- mal-formed in the MARC.  Replacing it with NULL allows the item to be
		-- moved to production, where it can be corrected by BHL staff.
		UPDATE	#tmpTitle
		SET		LanguageCode = NULL
		FROM	#tmpTitle t LEFT JOIN dbo.BHLLanguage l
					ON t.LanguageCode = l.LanguageCode
		WHERE	l.LanguageCode IS NULL

		-- =======================================================================

		-- Use Indicator2 of the 245a field to build the appropriate SortTitle for all titles
		UPDATE	#tmpTitle
		SET		SortTitle = dbo.fnGetSortString(SUBSTRING(
													FullTitle, 
													CASE WHEN ISNUMERIC(ISNULL(Indicator2, '')) = 1 THEN 
														CASE WHEN Indicator2 = '0' THEN 1 ELSE CONVERT(int, Indicator2) END
													ELSE 1 END, 
													60))
		FROM	#tmpTitle t LEFT JOIN dbo.vwIAMarcDataField v
					ON t.ItemID = v.ItemID
					AND v.DataFieldTag = '245'
					AND v.Code = 'a';

		/*	
		-- Get the sort titles for all titles
		-- Remove keywords from the full title
		UPDATE	#tmpTitle
		SET		SortTitle = SUBSTRING(
								LTRIM(RTRIM(
								REPLACE(
								REPLACE(
								REPLACE(
								REPLACE(
								REPLACE(
								REPLACE(CONVERT(NVARCHAR(4000), FullTitle), 
									' A ', ' '),
									' An ', ' '),
									' Les ', ' '),
									' Las ', ' '),
									' Los ', ' '),
									' L'' ', ' ')
								))
							, 1, 65)

		-- Remove keywords from the beginning of the sort titles
		UPDATE	#tmpTitle
		SET		SortTitle = CASE 
							WHEN SUBSTRING(SortTitle, 1, 4) = 'The ' THEN SUBSTRING(SortTitle, 5, 60)
							WHEN SUBSTRING(SortTitle, 1, 2) = 'A ' THEN SUBSTRING(SortTitle, 3, 60)
							WHEN SUBSTRING(SortTitle, 1, 3) = 'An ' THEN SUBSTRING(SortTitle, 4, 60)
							WHEN SUBSTRING(SortTitle, 1, 4) = 'Les ' THEN SUBSTRING(SortTitle, 5, 60)
							WHEN SUBSTRING(SortTitle, 1, 4) = 'Las ' THEN SUBSTRING(SortTitle, 5, 60)
							WHEN SUBSTRING(SortTitle, 1, 4) = 'Los ' THEN SUBSTRING(SortTitle, 5, 60)
							WHEN SUBSTRING(SortTitle, 1, 3) = 'L'' ' THEN SUBSTRING(SortTitle, 4, 60)
							WHEN SUBSTRING(SortTitle, 1, 3) = '...' THEN LTRIM(SUBSTRING(SortTitle, 4, 60))
							WHEN SUBSTRING(SortTitle, 1, 1) = '[' THEN SUBSTRING(SortTitle, 2, 60)
							ELSE SUBSTRING(SortTitle, 1, 60)
							END
		*/

		-- =======================================================================
		-- =======================================================================
		-- =======================================================================
		-- Get Title Notes

		WITH basetable AS
		(
			-- Select all note fields, excluding those with a code '5'
			SELECT	ROW_NUMBER() OVER (PARTITION BY MarcDataFieldID ORDER BY t.ItemID, MarcSubFieldID) AS RowNum,
					COUNT(*) OVER (PARTITION BY MarcDataFieldID) NumRows,
					t.ItemID, MarcDataFieldID, MarcSubFieldID, DataFieldTag, Indicator1, Code, 
					CAST(SubFieldValue AS NVARCHAR(MAX)) SubFieldValue
			FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField m ON t.ItemID = m.ItemID
			WHERE	m.DataFieldTag IN ('500','502','505','510','515','520','525','545','546','547','550','580')
			AND		m.SubFieldValue <> ''
			AND		m.MarcDataFieldID NOT IN (
						SELECT	DISTINCT v.MarcDataFieldID
						FROM	dbo.vwIAMarcDataField v INNER JOIN #tmpTitle tt ON v.ItemID = tt.ItemID
						WHERE	v.DataFieldTag IN ('500','502','505','510','515','520','525','545','546','547','550','580')
						AND		v.Code = '5'
						)
		),
		rCTE AS (
			SELECT	RowNum, NumRows, ItemID, MarcDataFieldID, MarcSubFieldID, DataFieldTag, Indicator1, SubFieldValue
			FROM	basetable
			WHERE	RowNum = 1
			UNION ALL
			SELECT	r.RowNum + 1, b.NumRows, r.ItemID, r.MarcDataFieldID, r.MarcSubFieldID, r.DataFieldTag, 
					r.Indicator1, r.SubFieldValue + ' | ' + b.SubFieldValue AS SubFieldValue
			FROM	basetable b 
					INNER JOIN rCTE r ON b.ItemID = r.ItemID
					AND b.MarcDataFieldID = r.MarcDataFieldID
					AND b.RowNum = r.RowNum + 1
		)
		INSERT #tmpTitleNote
		SELECT	ItemID, MarcDataFieldID, SubFieldValue AS NoteText, DataFieldTag, Indicator1, 
				ROW_NUMBER() OVER (PARTITION BY ItemID ORDER BY MarcSubFieldID) AS NoteSequence
		FROM	rCTE
		WHERE	NumRows = RowNum
		ORDER BY ItemID, MarcDataFieldID, RowNum

		-- =======================================================================
		-- =======================================================================
		-- =======================================================================
		-- Get Identifiers

		-- Get the OCLC numbers from the 035a and 010o MARC fields (in most cases it's located in one
		-- or the other of these)
		INSERT INTO #tmpIdentifier
		SELECT	t.ItemID,
				'OCLC',
				COALESCE(CONVERT(NVARCHAR(30), CONVERT(BIGINT, dbo.fnFilterString(m.subfieldvalue, '[0-9]', ''))), 
						CONVERT(NVARCHAR(30), CONVERT(BIGINT, dbo.fnFilterString(m2.subfieldvalue, '[0-9]', ''))))
		FROM	#tmpTitle t 
				LEFT JOIN (SELECT * FROM dbo.vwIAMarcDataField 
							WHERE DataFieldTag = '035' AND code = 'a' AND 
							(SubFieldValue LIKE '(OCoLC)%' OR SubFieldValue LIKE 'ocm%' OR SubFieldValue LIKE 'ocn%' OR SubFieldValue LIKE 'on%')
							) m
					ON t.ItemID = m.ItemID
				LEFT JOIN (SELECT * FROM dbo.vwIAMarcDataField
							WHERE DataFieldTag = '010' AND Code = 'o') m2
					ON t.ItemID = m2.ItemID
	
		-- Next check MARC control 001 record for the OCLC number (not too many of these)
		INSERT INTO #tmpIdentifier
		SELECT	t.ItemID,
				'OCLC',
				CONVERT(NVARCHAR(30), CONVERT(INT, dbo.fnFilterString(mc.value, '[0-9]', '')))
		FROM	#tmpTitle t 
				LEFT JOIN (SELECT * FROM dbo.vwIAMarcControl WHERE tag = '001' AND [value] NOT LIKE 'Catkey%') mc
					ON t.ItemID = mc.ItemID
				LEFT JOIN (SELECT * FROM dbo.vwIAMarcControl WHERE tag = '003' AND [value] = 'OCoLC') mc2
					ON t.ItemID = mc2.ItemID
		WHERE	(mc.[Value] LIKE 'oc%' OR mc.[Value] LIKE 'on%' OR mc2.[value] = 'OCoLC')
		AND		NOT EXISTS (SELECT IdentifierValue FROM #tmpIdentifier 
							WHERE ItemID = t.ItemID
							AND IdentifierValue IS NOT NULL)

		-- Get the Library Of Congress Control numbers
		INSERT INTO #tmpIdentifier
		SELECT DISTINCT
				t.ItemID,
				'DLC',
				dbo.BHLfnGetLCCNValue(m.SubFieldValue)
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField m
					ON t.ItemID = m.ItemID
		WHERE	DataFieldTag = '010'
		AND		Code = 'a'

		-- Get the ISBN identifiers
		INSERT INTO #tmpIdentifier
		SELECT DISTINCT
				t.ItemID,
				'ISBN',
				m.SubFieldValue
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField m
					ON t.ItemID = m.ItemID
		WHERE	m.DataFieldTag = '020'
		AND		m.Code = 'a'
		AND		LEN(m.SubFieldValue) <= 125

		-- Get the ISSN identifiers
		INSERT INTO #tmpIdentifier
		SELECT DISTINCT
				t.ItemID,
				dbo.BHLfnGetISSNName(m.SubFieldValue),
				dbo.BHLfnGetISSNValue(m.SubFieldValue)
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField m
					ON t.ItemID = m.ItemID
		WHERE	m.DataFieldTag = '022'
		AND		m.Code = 'a'
		AND		LEN(m.SubFieldValue) <= 125

		-- Get the CODEN codes
		INSERT INTO #tmpIdentifier
		SELECT DISTINCT
				t.ItemID,
				'CODEN',
				m.SubFieldValue
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField m
					ON t.ItemID = m.ItemID
		WHERE	m.DataFieldTag = '030'
		AND		m.Code = 'a'

		-- Get the National Library of Medicine call numbers
		INSERT INTO #tmpIdentifier
		SELECT DISTINCT
				t.ItemID,
				'NLM', 
				Z.SubFieldValue
		FROM	#tmpTitle t INNER JOIN (
						SELECT	ItemID, MarcDataFieldID, 
								LTRIM(ISNULL(MIN([a]), '') + ' ' + ISNULL(MIN([b]), '')) AS SubFieldValue
						FROM	(
								SELECT	ItemID, MarcDataFieldID, [a], [b]
								FROM	(SELECT * FROM dbo.vwIAMarcDataField 
										WHERE DataFieldTag = '060' AND Code in ('a', 'b')) AS m
								PIVOT	(MIN(SubFieldValue) FOR Code IN ([a], [b])) AS Pvt
								) X
						GROUP BY ItemID, MarcDataFieldID
						) Z
					ON t.ItemID = Z.ItemID

		-- Get the National Agricultural Library call numbers
		INSERT INTO #tmpIdentifier
		SELECT DISTINCT
				t.ItemID,
				'NAL', 
				Z.SubFieldValue
		FROM	#tmpTitle t INNER JOIN (
						SELECT	ItemID, MarcDataFieldID, 
								LTRIM(ISNULL(MIN([a]), '') + ' ' + ISNULL(MIN([b]), '')) AS SubFieldValue
						FROM	(
								SELECT	ItemID, MarcDataFieldID, [a], [b]
								FROM	(SELECT * FROM dbo.vwIAMarcDataField 
										WHERE DataFieldTag = '070' AND Code in ('a', 'b')) AS m
								PIVOT	(MIN(SubFieldValue) FOR Code IN ([a], [b])) AS Pvt
								) X
						GROUP BY ItemID, MarcDataFieldID
						) Z
					ON t.ItemID = Z.ItemID

		-- Get the Government Printing Office
		INSERT INTO #tmpIdentifier
		SELECT DISTINCT
				t.ItemID,
				'GPO',
				m.SubFieldValue
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField m
					ON t.ItemID = m.ItemID
		WHERE	m.DataFieldTag = '074'
		AND		m.Code = 'a'

		-- Get the Dewey Decimal Classifiers
		INSERT INTO #tmpIdentifier
		SELECT DISTINCT
				t.ItemID,
				'DDC',
				m.SubFieldValue
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField m
					ON t.ItemID = m.ItemID
		WHERE	m.DataFieldTag = '082'
		AND		m.Code = 'a'

		-- Get the WonderFetch identifiers (first look for a titleid, then look for a MARC
		-- 001 control record with a value including 'catkey')
		INSERT INTO #tmpIdentifier
		SELECT DISTINCT
				t.ItemID,
				'WonderFetch',
				i.TitleID
		FROM	#tmpTitle t INNER JOIN dbo.IAItem i
					ON t.ItemID = i.ItemID
		WHERE	i.TitleID <> ''

		INSERT INTO #tmpIdentifier
		SELECT DISTINCT 
				t.ItemID,
				'WonderFetch',
				LTRIM(RTRIM(REPLACE(m.[Value], 'catkey', ''))) 
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcControl m
					ON t.ItemID = m.ItemID
				LEFT JOIN #tmpIdentifier ti
					ON t.ItemID = ti.ItemID
					AND 'Local' = ti.IdentifierName
		WHERE	m.Tag = '001' 
		AND		m.[Value] LIKE 'catkey%'
		AND		ti.IdentifierValue IS NULL

		-- Get the non-OCLC and non-WonderFetch local identifiers from the 
		-- MARC 001 control record
		INSERT INTO #tmpIdentifier
		SELECT DISTINCT
				t.ItemID,
				'MARC001',
				m.[Value]
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcControl m
					ON t.ItemID = m.ItemID
		WHERE	m.Tag = '001'
		AND		m.[Value] NOT LIKE 'catkey%'
		AND		m.[Value] NOT LIKE 'oc%'

		-- =======================================================================
		-- =======================================================================
		-- =======================================================================
		-- Get Title Associations

		-- Get 440 and 490 tag with an 'a' code
		INSERT INTO #tmpTitleAssociation
		SELECT	ROW_NUMBER() OVER (PARTITION BY m.MarcDataFieldID
									ORDER BY m.MarcSubFieldID) AS Sequence,
				m.ItemID, 
				m.MarcDataFieldID,
				m.DataFieldTag, 
				m.Indicator1 AS MARCIndicator1,
				'' AS MARCIndicator2, 
				dbo.BHLfnRemoveTrailingPunctuation(m.SubFieldValue, DEFAULT) AS Title, 
				'' AS Section, 
				'' AS Volume,
				'' AS Heading,
				'' AS Publication,
				'' AS Relationship
		FROM	#tmpTitle t INNER JOIN vwIAMarcDataField m
					ON t.ItemID = m.ItemID
		WHERE	m.DataFieldTag IN ('440', '490')
		AND		m.Code = 'a'
		AND		m.SubFieldValue <> ''

		-- Add the section and volume information to the original data set.
		-- Use generated sequence numbers to match the sections/volumes
		-- with titles (there's no guaranteed relational way of making the
		-- matches, so this is the best guess approach).
		UPDATE	#tmpTitleAssociation
		SET		Section = dbo.BHLfnRemoveTrailingPunctuation(x.SubFieldValue, DEFAULT)
		FROM	#tmpTitleAssociation t INNER JOIN (
						SELECT	ROW_NUMBER() OVER (PARTITION BY m.MarcDataFieldID
													ORDER BY m.MarcSubFieldID) AS NewSequence,
								m.*
						FROM	#tmpTitle ti INNER JOIN vwIAMarcDataField m
									ON ti.ItemID = m.ItemID
						WHERE	m.DataFieldTag IN ('440', '490')
						AND		m.Code = 'p'
						AND		m.SubFieldValue <> ''
						) x
					ON t.MarcDataFieldID = x.MarcDataFieldID
					AND t.Sequence = x.NewSequence

		UPDATE	#tmpTitleAssociation
		SET		Volume = dbo.BHLfnRemoveTrailingPunctuation(x.SubFieldValue, DEFAULT)
		FROM	#tmpTitleAssociation t INNER JOIN (
						SELECT	ROW_NUMBER() OVER (PARTITION BY m.MarcDataFieldID
													ORDER BY m.MarcSubFieldID) AS NewSequence,
								m.*
						FROM	#tmpTitle ti INNER JOIN vwIAMarcDataField m
									ON ti.ItemID = m.ItemID
						WHERE	m.DataFieldTag IN ('440', '490')
						AND		m.Code = 'v'
						AND		m.SubFieldValue <> ''
						) x
					ON t.MarcDataFieldID = x.MarcDataFieldID
					AND t.Sequence = x.NewSequence

		-- Get the 830 records (these will be used to replace or augment certain 490 records)
		SELECT	x.ItemID, 
				x.MarcDataFieldID, 
				x.DataFieldTag, 
				MIN([a]) AS Title, 
				MIN([p]) AS Section, 
				MIN([v]) AS Volume
		INTO	#tmp830
		FROM	(
				SELECT	ItemID, MarcDataFieldID, DataFieldTag, Indicator1, [a], [p], [v]
				FROM	(SELECT * FROM dbo.vwIAMarcDataField
						WHERE DataFieldTag IN ('830')) AS m
				PIVOT	(MIN(SubFieldValue) FOR Code IN ([a], [p], [v])) AS Pvt
				) x INNER JOIN #tmpTitle t
					ON x.ItemID = t.ItemID
		GROUP BY
				x.ItemID, x.MarcDataFieldID, x.DataFieldTag

		-- Delete the 490 records for which we have an 830 record, UNLESS there is
		-- an identifier (code = x) associated with the 490.  The identifier gives
		-- us a known, exact way to identify the series, and we don't want to throw
		-- that away.
		DELETE	#tmpTitleAssociation
		FROM	#tmpTitleAssociation ta INNER JOIN #tmp830 t8
					ON ta.ItemID = t8.ItemID
				LEFT JOIN dbo.vwIAMarcDataField m
					ON ta.ItemID = m.ItemID
					AND '490' = m.DataFieldTag
					AND 'x' = m.Code
		WHERE	ta.MARCTag = '490'
		AND		ta.MARCIndicator1 = '1'
		AND		m.MarcDataFieldID IS NULL

		-- Insert the 830 title associations when we haven't already collected a 490 record.
		INSERT INTO #tmpTitleAssociation
		SELECT DISTINCT
				0 AS Sequence,
				t8.ItemID,
				t8.MarcDatafieldID,
				t8.DataFieldTag,
				'',
				'',
				dbo.BHLfnRemoveTrailingPunctuation(ISNULL(t8.Title, ''), DEFAULT),
				dbo.BHLfnRemoveTrailingPunctuation(ISNULL(t8.Section, ''), DEFAULT),
				dbo.BHLfnRemoveTrailingPunctuation(ISNULL(t8.Volume, ''), DEFAULT),
				'',
				'',
				''
		FROM	#tmp830 t8 LEFT JOIN #tmpTitleAssociation ta
					ON t8.ItemID = ta.ItemID
					AND '490' = ta.MARCTag
					AND '1' = ta.MARCIndicator1
				INNER JOIN #tmpTitle t
					ON t8.ItemID = t.ItemID
		WHERE	ta.MARCDataFieldID IS NULL

		DROP TABLE #tmp830

		-- Get 780 and 785 tags with 't' or 'a' code (give preference to 't')
		INSERT INTO #tmpTitleAssociation
		SELECT DISTINCT
				0 AS Sequence, 
				m.ItemID, 
				m.MarcDataFieldID, 
				m.DataFieldTag, 
				'',
				m.Indicator2, 
				'' AS Title, 
				'' AS Section, 
				'' AS Volume,
				'' AS Heading,
				'' AS Publication,
				'' AS Relationship
		FROM	#tmpTitle t INNER JOIN vwIAMarcDataField m
					ON t.ItemID = m.ItemID
		WHERE	m.DataFieldTag IN ('780', '785')
		AND		m.Code IN ('t','a')
		AND		m.SubFieldValue <> ''

		UPDATE	#tmpTitleAssociation
		SET		Title = dbo.BHLfnRemoveTrailingPunctuation(m.SubFieldValue, DEFAULT)
		FROM	#tmpTitleAssociation t INNER JOIN vwIAMarcDataField m
					ON t.MarcDataFieldID = m.MarcDataFieldID
		WHERE	m.Code = 't'
		AND		m.DataFieldTag IN ('780', '785')

		UPDATE	#tmpTitleAssociation
		SET		Title = dbo.BHLfnRemoveTrailingPunctuation(CONVERT(NVARCHAR(200), m.SubFieldValue + ' ' + Title), DEFAULT)
		FROM	#tmpTitleAssociation t INNER JOIN vwIAMarcDataField m
					ON t.MarcDataFieldID = m.MarcDataFieldID
		WHERE	m.Code = 'a'
		AND		m.DataFieldTag IN ('780', '785')

		-- Get 770, 772, 773, 775, 777, 787 tags 
		-- 770 are Supplement/Special Issue items
		-- 772 are Supplement Parent items
		-- 773 is Host Item information... defines "container" items for titles that are also articles
		-- 775 are other editions
		-- 777 are Issue With items
		-- 787 are other relationships
		INSERT INTO #tmpTitleAssociation
		SELECT	0 AS Sequence,
				x.ItemID,
				x.MarcDataFieldID,
				x.DataFieldTag,
				'',
				'',
				dbo.BHLfnRemoveTrailingPunctuation(ISNULL(MIN([t]), ''), DEFAULT) AS Title,
				'' AS Section,
				'' AS Volume,
				-- As these fields may contain date range values (ex. "1990-"), don't remove hyphens when cleaning trailing punctuation
				dbo.BHLfnRemoveTrailingPunctuation(ISNULL(MIN([a]), ''), '[a-zA-Z0-9)\]?!>*%"''-]%') AS Heading, 
				dbo.BHLfnRemoveTrailingPunctuation(ISNULL(MIN([d]), ''), '[a-zA-Z0-9)\]?!>*%"''-]%') AS Publication, 
				dbo.BHLfnRemoveTrailingPunctuation(ISNULL(MIN([g]), ''), '[a-zA-Z0-9)\]?!>*%"''-]%') AS Relationship
		FROM	(
				SELECT ItemID, MarcDataFieldID, DataFieldTag, [a], [d], [g], [t]
				FROM	(SELECT * FROM dbo.vwIAMarcDataField
						WHERE DataFieldTag IN ('770', '772', '773', '775', '777', '787')) AS m
				PIVOT	(MIN(SubFieldValue) FOR Code in ([a], [d], [g], [t])) AS Pvt
				) x INNER JOIN #tmpTitle ti
					ON x.ItemID = ti.ItemID
		GROUP BY x.ItemID, x.MarcDataFieldID, x.DataFieldTag

		-- Get the 740 tags
		INSERT INTO #tmpTitleAssociation
		SELECT	0 AS Sequence,
				x.ItemID, 
				x.MarcDataFieldID, 
				x.DataFieldTag, 
				'',
				'',
				dbo.BHLfnRemoveTrailingPunctuation(ISNULL(MIN([a]), ''), DEFAULT) AS Title, 
				dbo.BHLfnRemoveTrailingPunctuation(ISNULL(MIN([n]), ''), DEFAULT) AS Section, 
				dbo.BHLfnRemoveTrailingPunctuation(ISNULL(MIN([p]), ''), DEFAULT) AS Volume,
				'',
				'',
				''
		FROM	(
				SELECT	ItemID, MarcDataFieldID, DataFieldTag, [a], [p], [n]
				FROM	(SELECT * FROM dbo.vwIAMarcDataField
						WHERE DataFieldTag = '740') AS m
				PIVOT	(MIN(SubFieldValue) FOR Code IN ([a], [p], [n])) AS Pvt
				) x INNER JOIN #tmpTitle t
					ON x.ItemID = t.ItemID
		GROUP BY x.ItemID, x.MarcDataFieldID, x.DataFieldTag

		-- =======================================================================
		-- =======================================================================
		-- =======================================================================
		-- Get Title Variants

		-- Get 210, 242, and 246 tags with an 'a' code
		INSERT INTO #tmpTitleVariant
		SELECT	m.ItemID, 
				m.MarcDataFieldID,
				m.DataFieldTag, 
				CASE WHEN m.DataFieldTag = '246' AND m.Indicator2 = '1' THEN '1' ELSE '' END AS MARCIndicator2,
				dbo.BHLfnRemoveTrailingPunctuation(m.SubFieldValue, DEFAULT) AS Title, 
				'' AS TitleRemainder, 
				'' AS PartNumber,
				'' AS PartName
		FROM	#tmpTitle t INNER JOIN vwIAMarcDataField m
					ON t.ItemID = m.ItemID
		WHERE	m.DataFieldTag IN ('210', '242', '246')
		AND		m.Code = 'a'
		AND		m.SubFieldValue <> ''

		-- Add the title remainders to the original data set.
		-- As this field may contain date range values (ex. "1990-"), don't remove hyphens when cleaning up trailing punctuation.
		UPDATE	#tmpTitleVariant
		SET		TitleRemainder = dbo.BHLfnRemoveTrailingPunctuation(x.SubFieldValue, '[a-zA-Z0-9)\]?!>*%"''-]%')
		FROM	#tmpTitleVariant t INNER JOIN (
						SELECT	m.*
						FROM	#tmpTitle ti INNER JOIN vwIAMarcDataField m
									ON ti.ItemID = m.ItemID
						WHERE	m.DataFieldTag IN ('210', '242', '246')
						AND		m.Code = 'b'
						AND		m.SubFieldValue <> ''
						) x
					ON t.MarcDataFieldID = x.MarcDataFieldID

		-- Add the part numbers to the original data set.
		UPDATE	#tmpTitleVariant
		SET		PartNumber = dbo.BHLfnRemoveTrailingPunctuation(x.SubFieldValue, DEFAULT)
		FROM	#tmpTitleVariant t INNER JOIN (
						SELECT	m.*
						FROM	#tmpTitle ti INNER JOIN vwIAMarcDataField m
									ON ti.ItemID = m.ItemID
						WHERE	m.DataFieldTag IN ('242', '246')
						AND		m.Code = 'n'
						AND		m.SubFieldValue <> ''
						) x
					ON t.MarcDataFieldID = x.MarcDataFieldID

		-- Add the part names to the original data set.
		UPDATE	#tmpTitleVariant
		SET		PartName = dbo.BHLfnRemoveTrailingPunctuation(x.SubFieldValue, DEFAULT)
		FROM	#tmpTitleVariant t INNER JOIN (
						SELECT	m.*
						FROM	#tmpTitle ti INNER JOIN vwIAMarcDataField m
									ON ti.ItemID = m.ItemID
						WHERE	m.DataFieldTag IN ('242', '246')
						AND		m.Code = 'p'
						AND		m.SubFieldValue <> ''
						) x
					ON t.MarcDataFieldID = x.MarcDataFieldID

		-- =======================================================================
		-- =======================================================================
		-- =======================================================================
		-- Get Title Languages
		INSERT INTO #tmpTitleLanguage (ItemID, LanguageCode)
		SELECT DISTINCT @ItemID, LanguageCode
		FROM	dbo.vwSplitLanguage(@ItemID)
		WHERE	LanguageCode IN (SELECT LanguageCode FROM dbo.BHLLanguage)

		-- =======================================================================
		-- =======================================================================
		-- =======================================================================
		-- Get Creators from MARC

		-- Get the initial creator information (MARC subfield code 'a')
		INSERT INTO #tmpCreator (ItemID, TitleID, CreatorName,
								CreatorRoleTypeID, MARCDataFieldID, MARCDataFieldTag, MARCCreator_a, SequenceOrder)
		SELECT	t.ItemID,
				0,
				m.SubFieldValue,
				0,
				m.MARCDataFieldID, 
				m.DataFieldTag,
				m.SubFieldValue,
				ROW_NUMBER() OVER (PARTITION BY m.ItemID ORDER BY m.DataFieldTag, m.MARCSubFieldID) AS SequenceOrder
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField m
					ON t.ItemID = m.ItemID
					AND m.DataFieldTag IN ('100', '110', '111', '700', '710', '711')
					AND m.Code = 'a'
					AND LTRIM(RTRIM(ISNULL(m.SubFieldValue, ''))) <> ''

		-- Get creator MARC subfield 'b'
		UPDATE	#tmpCreator
		SET		MARCCreator_b = 
					SUBSTRING(
						LTRIM(
							REPLACE(
								STUFF(
									(
									SELECT	'. ' + LTRIM(RTRIM(m.SubFieldValue))
									FROM	#tmpCreator t2 INNER JOIN dbo.vwIAMarcDataField m
												ON t2.ItemID = m.ItemID
												AND t2.MarcDataFieldID = m.MarcDataFieldID
												AND t2.MarcDataFieldTag = m.DataFieldTag
												AND m.Code = 'b'
									WHERE	t2.MARCDataFieldID = t1.MARCDataFieldID
									ORDER BY MARCSubFieldID
									FOR XML PATH('')
									)  -- FOR XML contatenation of 'b' values
								,1,1,'') -- STUFF to remove leading '. '
							,'..', '.')	-- REPLACE to replace duplicated periods
						) -- LTRIM to remove leading spaces
					, 1, 450) -- SUBSTRING to limit total length of the value
		FROM	#tmpCreator t1


		-- Get creator MARC subfield 'c'
		UPDATE	#tmpCreator
		SET		MARCCreator_c = m.SubFieldValue
		FROM	#tmpCreator t INNER JOIN dbo.vwIAMarcDataField m
					ON t.ItemID = m.ItemID
					AND t.MarcDataFieldID = m.MarcDataFieldID
					AND t.MarcDataFieldTag = m.DataFieldTag
					AND m.Code = 'c'

		-- Get creator MARC subfield 'd'
		UPDATE	#tmpCreator
		SET		MARCCreator_d = m.SubFieldValue
		FROM	#tmpCreator t INNER JOIN dbo.vwIAMarcDataField m
					ON t.ItemID = m.ItemID
					AND t.MarcDataFieldID = m.MarcDataFieldID
					AND t.MarcDataFieldTag = m.DataFieldTag
					AND m.Code = 'd'

		-- Get creator MARC subfield 'e'
		UPDATE	#tmpCreator
		SET		MARCCreator_e = 
					SUBSTRING(
						LTRIM(
							STUFF(
								(
								SELECT	' ' + LTRIM(RTRIM(m.SubFieldValue))
								FROM	#tmpCreator t2 INNER JOIN dbo.vwIAMarcDataField m
											ON t2.ItemID = m.ItemID
											AND t2.MarcDataFieldID = m.MarcDataFieldID
											AND t2.MarcDataFieldTag = m.DataFieldTag
											AND m.Code = 'e'
								WHERE	t2.MARCDataFieldID = t1.MARCDataFieldID
								ORDER BY MARCSubFieldID
								FOR XML PATH('')
								)  -- FOR XML contatenation of 'e' values
							,1,1,'') -- STUFF to remove leading ' '
						) -- LTRIM to remove leading spaces
					, 1, 450) -- SUBSTRING to limit total length of the value
		FROM	#tmpCreator t1

		-- Get creator MARC subfield 'q'
		UPDATE	#tmpCreator
		SET		MARCCreator_q = m.SubFieldValue
		FROM	#tmpCreator t INNER JOIN dbo.vwIAMarcDataField m
					ON t.ItemID = m.ItemID
					AND t.MarcDataFieldID = m.MarcDataFieldID
					AND t.MarcDataFieldTag = m.DataFieldTag
					AND m.Code = 'q'

		-- Get creator MARC subfield 't'
		UPDATE	#tmpCreator
		SET		MARCCreator_t = m.SubFieldValue
		FROM	#tmpCreator t INNER JOIN dbo.vwIAMarcDataField m
					ON t.ItemID = m.ItemID
					AND t.MarcDataFieldID = m.MarcDataFieldID
					AND t.MarcDataFieldTag = m.DataFieldTag
					AND m.Code = 't'

		-- Get creator MARC subfield '5'
		UPDATE	#tmpCreator
		SET		MARCCreator_5 = m.SubFieldValue
		FROM	#tmpCreator t INNER JOIN dbo.vwIAMarcDataField m
					ON t.ItemID = m.ItemID
					AND t.MarcDataFieldID = m.MarcDataFieldID
					AND t.MarcDataFieldTag = m.DataFieldTag
					AND m.Code = '5'

		-- Get the creator DOB and DOD values
		SELECT	ItemID,
				MARCCreator_a,
				MARCCreator_b,
				MARCCreator_c,
				MARCCreator_d,
				MARCCreator_d AS Dates
		INTO	#tmpCreatorDates
		FROM	#tmpCreator
		WHERE	ISNULL(MARCCreator_d, '') <> ''

		UPDATE	#tmpCreator
		SET		DOB = u.StartDate,
				DOD = u.EndDate
		FROM	#tmpCreator c INNER JOIN #tmpCreatorDates d
					ON c.ItemID = d.ItemID
					AND ISNULL(c.MARCCreator_a, '') = ISNULL(d.MARCCreator_a, '')
					AND ISNULL(c.MARCCreator_b, '') = ISNULL(d.MARCCreator_b, '')
					AND ISNULL(c.MARCCreator_c, '') = ISNULL(d.MARCCreator_c, '')
					AND ISNULL(c.MARCCreator_d, '') = ISNULL(d.MARCCreator_d, '')
				CROSS APPLY dbo.fnGetDatesFromString(d.Dates) u

		DROP TABLE #tmpCreatorDates

		-- =======================================================================
		-- =======================================================================
		-- =======================================================================
		-- Get Keywords from MARC

		INSERT INTO #tmpKeyword
		SELECT	t.ItemID,
				0,
				SUBSTRING(m.SubFieldValue, 1, 200),
				m.DataFieldTag,
				m.Code
		FROM	#tmpTitle t INNER JOIN dbo.vwIAMarcDataField m
					ON t.ItemID = m.ItemID
		WHERE	m.DataFieldTag IN ('600', '610', '611', '630', '648', '650', '651', '652', 
									'653', '654', '655', '656', '657', '658', '662', '690')
		AND		m.Indicator2 <> '6'	-- skip non-english-language subjects
		AND		m.Code in ('a', 'b', 'c', 'd', 'g', 'q', 't', 'v', 'x', 'y', 'z')
		/*
		AND		m.Code <> 'e' -- skip sources of tags
		AND		m.Code <> '4' -- skip sources of tags
		AND		m.Code <> '3' -- skip sources of tags
		AND		m.Code <> '2' -- skip sources of tags
		AND		m.Code <> '0' -- skip authority record control numbers
		*/
	END

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Items

	-- Get the information we've already collected with the title data
	INSERT INTO #tmpItem (ItemID, IAIdentifier, MARCBibID, MarcItemID, TitleID, BarCode, ItemSequence, InstitutionCode, LanguageCode, ItemStatusID)
	SELECT	ItemID, IAIdentifier, MARCBibID, IAIdentifier, 0, IAIdentifier, 10000, InstitutionCode, LanguageCode, 40 
	FROM	#tmpTitle

	-- If no title info, get the metadata for a Virtual Item from the IAItem and IADCMetdata tables
	IF NOT EXISTS(SELECT ItemID FROM #tmpItem)
	BEGIN
		INSERT	#tmpItem (ItemID, IAIdentifier, MARCBibID, MarcItemID, TitleID, BarCode, ItemSequence, ItemStatusID)
		SELECT	ItemID, IAIdentifier, LEFT(REPLACE(IAIdentifier, '-', ''), 50), IAIdentifier, 0, IAIdentifier, 10000, 40
		FROM	dbo.IAItem
		WHERE	ItemStatusID = 30	-- Approved
		AND		ItemID = @ItemID

		-- Get the publisher metadata for a segment in a Virtual Item
		UPDATE	#tmpItem
		SET		PublicationDetails = m.DCElementValue
		FROM	#tmpItem t INNER JOIN dbo.IADCMetadata m
					ON t.ItemID = m.ItemID
					AND m.DCElementName = 'source'
		WHERE	m.DCElementValue <> ''

		UPDATE	#tmpItem
		SET		PublisherName = m.DCElementValue
		FROM	#tmpItem t INNER JOIN dbo.IADCMetadata m
					ON t.ItemID = m.ItemID
					AND m.DCElementName = 'publisher'
		WHERE	m.DCElementValue <> ''

		-- Get the date value for a segment in a Virtual Item
		UPDATE	#tmpItem
		SET		VirtualVolumeSegmentDate = m.DCElementValue
		FROM	#tmpItem t INNER JOIN dbo.IADCMetadata m
					ON t.ItemID = m.ItemID
					AND m.DCElementName = 'date'
		WHERE	m.DCElementValue <> ''

		-- Get the language code from the DC metadata, ignoring any unrecognized codes
		UPDATE	#tmpItem
		SET		LanguageCode = SUBSTRING(m.DCElementValue, 1, 10)
		FROM	#tmpItem t INNER JOIN dbo.IADCMetadata m ON t.ItemID = m.ItemID AND m.DCElementName = 'language'
		WHERE	ISNULL(t.LanguageCode, '') = '' AND m.DCElementValue <> ''

		UPDATE	#tmpItem
		SET		LanguageCode = NULL
		FROM	#tmpItem t LEFT JOIN dbo.BHLLanguage l ON t.LanguageCode = l.LanguageCode
		WHERE	l.LanguageCode IS NULL

		-- Get the institution code, following the same rules used to get the
		-- codes for titles.
		UPDATE	#tmpItem
		SET		InstitutionCode = s.InstitutionCode
		FROM	#tmpItem t INNER JOIN dbo.IADCMetadata m ON t.ItemID = m.ItemID AND m.DCElementName = 'contributor'
				INNER JOIN dbo.IAScanCenterInstitution s ON m.DCElementValue = s.ScanningCenterCode
	
		UPDATE	#tmpTitle
		SET		InstitutionCode = i.InstitutionCode
		FROM	#tmpItem t INNER JOIN dbo.IADCMetadata m ON t.ItemID = m.ItemID AND m.DCElementName = 'contributor'
				INNER JOIN dbo.BHLInstitution i ON m.DCElementValue = i.InstitutionName COLLATE Latin1_general_CI_AI -- ignore diacritics for this comparison
		WHERE	t.InstitutionCode IS NULL
	
		UPDATE #tmpItem SET InstitutionCode = 'UNKNOWN' WHERE InstitutionCode IS NULL		

		-- Get the Segment Genre ID.  Default to Genre='Article' if no match.
		UPDATE	#tmpItem
		SET		SegmentGenreID = COALESCE(g1.SegmentGenreID, g2.SegmentGenreID)
		FROM	#tmpItem t
				INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
				INNER JOIN dbo.BHLSegmentGenre g1 ON i.Genre = g1.GenreName
				INNER JOIN dbo.BHLSegmentGenre g2 ON 'Article' = g2.GenreName
	END
	
	-- Get the current vault ID to assign to the items
	DECLARE @VaultID INT
	SELECT	@VaultID = CONVERT(INT, ConfigurationValue)
	FROM	dbo.BHLConfiguration
	WHERE	ConfigurationName = 'CurrentIAVaultID'

	-- Set the vault identifier
	UPDATE	#tmpItem
	SET		VaultID = ISNULL(v.VaultID, @VaultID)
	FROM	#tmpItem t INNER JOIN dbo.IAItem i
				ON t.ItemID = i.ItemID
			INNER JOIN dbo.BHLVault v
				ON i.LocalFileFolder = v.OCRFolderShare + '\'

	-- Get additional information from the Item table
	UPDATE	#tmpItem
	SET		Volume = i.Volume,
			Issue = i.Issue,
			Sponsor = i.Sponsor,
			ScanningUser = i.ScanOperator,
			ScanningDate = CASE 
							WHEN LEN(i.ScanDate) = 14
							THEN CONVERT(datetime, SUBSTRING(i.ScanDate, 1, 4) + '/' + 
													SUBSTRING(i.ScanDate, 5, 2) + '/' +
													SUBSTRING(i.ScanDate, 7, 2) + ' ' +
													SUBSTRING(i.ScanDate, 9, 2) + ':' +
													SUBSTRING(i.ScanDate, 11, 2) + ':' +
													SUBSTRING(i.ScanDate, 13, 2))
							WHEN LEN(i.ScanDate) = 4
							THEN CONVERT(datetime, i.ScanDate)
							END,
			[Year] = i.[Year],
			IdentifierBib = i.IdentifierBib,
			ZQuery = i.ZQuery,
			LicenseUrl = i.LicenseUrl,
			Rights = i.Rights,
			DueDiligence = i.DueDiligence,
			CopyrightStatus = i.PossibleCopyrightStatus,
			CopyrightRegion = i.CopyrightRegion,
			CopyrightComment = i.CopyrightComment,
			CopyrightEvidence = i.CopyrightEvidence,
			CopyrightEvidenceOperator = i.CopyrightEvidenceOperator,
			CopyrightEvidenceDate = i.CopyrightEvidenceDate,
			ItemDescription = i.ItemDescription,
			EndYear = i.EndYear,
			StartVolume = i.StartVolume,
			EndVolume = i.EndVolume,
			StartIssue = i.StartIssue,
			EndIssue = i.EndIssue,
			StartNumber = i.StartNumber,
			EndNumber = i.EndNumber,
			StartSeries = i.StartSeries,
			EndSeries = i.EndSeries,
			StartPart = i.StartPart,
			EndPart = i.EndPart,
			PageProgression = ISNULL(i.PageProgression, ''),
			VirtualTitleID = i.VirtualTitleID, 
			VirtualVolume = i.VirtualVolume,
			Summary = i.Summary
	FROM	#tmpItem t INNER JOIN dbo.IAItem i
				ON t.ItemID = i.ItemID

	-- Get the scanning institution code.  Look in the IASCanCenterInstitution
	-- table first.  If no match is found there, then look in the Institution
	-- table in the BHL database.  Anything left over is assigned "UNKNOWN".
	UPDATE	#tmpItem
	SET		ScanningInstitutionCode = s.InstitutionCode
	FROM	#tmpItem t 
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
			INNER JOIN dbo.IAScanCenterInstitution s ON i.ScanningInstitution = s.ScanningCenterCode
	
	UPDATE	#tmpItem
	SET		ScanningInstitutionCode = inst.InstitutionCode
	FROM	#tmpItem t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
			INNER JOIN dbo.BHLInstitution inst ON i.ScanningInstitution = inst.InstitutionName COLLATE Latin1_general_CI_AI
	WHERE	t.ScanningInstitutionCode IS NULL

	UPDATE	#tmpItem 
	SET		ScanningInstitutionCode = 'UNKNOWN' 
	FROM	#tmpItem t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
	WHERE	t.ScanningInstitutionCode IS NULL
	AND		i.ScanningInstitution <> ''

	-- Get the rights holder code.  Look in the IASCanCenterInstitution
	-- table first.  If no match is found there, then look in the Institution
	-- table in the BHL database.  Anything left over is assigned "UNKNOWN".
	UPDATE	#tmpItem
	SET		RightsHolderCode = s.InstitutionCode
	FROM	#tmpItem t 
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
			INNER JOIN dbo.IAScanCenterInstitution s ON i.RightsHolder = s.ScanningCenterCode
	
	UPDATE	#tmpItem
	SET		RightsHolderCode = inst.InstitutionCode
	FROM	#tmpItem t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
			INNER JOIN dbo.BHLInstitution inst ON i.RightsHolder = inst.InstitutionName COLLATE Latin1_general_CI_AI
	WHERE	t.RightsHolderCode IS NULL

	UPDATE	#tmpItem 
	SET		RightsHolderCode = 'UNKNOWN' 
	FROM	#tmpItem t
			INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
	WHERE	t.RightsHolderCode IS NULL
	AND		i.RightsHolder <> ''
	
	-- Add a default Copyright Status if none is provided
	UPDATE	#tmpItem
	SET		CopyrightStatus = 'Not provided. Contact Holding Institution to verify copyright status.'
	WHERE	ISNULL(CopyrightStatus, '') = ''

	-- Use the AddedDate as the ScanningDate if no Scan Date was specified
	UPDATE	#tmpItem
	SET		ScanningDate = i.IAAddedDate
	FROM	#tmpItem t INNER JOIN dbo.IAItem i ON t.ItemID = i.ItemID
	WHERE	t.ScanningDate IS NULL
	AND		i.IAAddedDate IS NOT NULL
	
	/*
	-- Add the ItemSequence by ordering each title by the item volume
	UPDATE	#tmpItem
	SET		ItemSequence = x.Sequence
	FROM	#tmpItem t 
			INNER JOIN (SELECT	ROW_NUMBER() OVER (PARTITION BY ItemID ORDER BY Volume) AS Sequence, 
								ItemID 
						FROM	#tmpItem) x
				ON t.ItemID = x.ItemID
	*/

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Item Languages
	INSERT INTO #tmpItemLanguage (ItemID, BarCode, LanguageCode)
	SELECT DISTINCT @ItemID, t.BarCode, l.LanguageCode
	FROM	#tmpItem t CROSS JOIN dbo.vwSplitLanguage(@ItemID) l
	WHERE	t.ItemID = @ItemID
	AND		l.LanguageCode IN (SELECT LanguageCode FROM dbo.BHLLanguage)

		-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Item Identifiers

	-- Get identifiers specified in the _META.XML file
	INSERT INTO #tmpIdentifier
	SELECT DISTINCT
			t.ItemID,
			'DOI',
			i.IdentifierValue
	FROM	#tmpItem t INNER JOIN dbo.IAItemIdentifier i
				ON t.ItemID = i.ItemID
			LEFT JOIN #tmpIdentifier ti
				ON t.ItemID = ti.ItemID
				AND 'DOI' = ti.IdentifierName
				AND i.IdentifierValue = ti.IdentifierValue
	WHERE	i.IdentifierDescription = 'identifier-doi'
	AND		ti.IdentifierValue IS NULL

	/*
	-- Consider using ARK ids from the meta.xml file in the future
	INSERT INTO #tmpIdentifier
	SELECT DISTINCT
			t.ItemID,
			'ARK',
			i.IdentifierValue
	FROM	#tmpItem t INNER JOIN dbo.IAItemIdentifier i
				ON t.ItemID = i.ItemID
			LEFT JOIN #tmpIdentifier ti
				ON t.ItemID = ti.ItemID
				AND 'ARK' = ti.IdentifierName
				AND i.IdentifierValue = ti.IdentifierValue
	WHERE	i.IdentifierDescription = 'identifier-ark'
	AND		ti.IdentifierValue IS NULL
	*/

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Creators from _META.XML

	-- If we don't have a MARC record, then see if there are any creators in the metadata tables
	IF NOT EXISTS (SELECT t.ItemID FROM #tmpTitle t INNER JOIN dbo.IAMarc m ON t.ItemID = m.ItemID)
	BEGIN
		INSERT INTO #tmpCreator (ItemID, TitleID, CreatorName,
								CreatorRoleTypeID, MARCDataFieldID, MARCDataFieldTag, MARCCreator_a, SequenceOrder)
		SELECT	ItemID,
				0,
				CreatorName,
				0,
				RowNum,
				CASE WHEN RowNum = 1 THEN '100' ELSE '700' END,
				CreatorName,
				RowNum
		FROM	(
				SELECT	ROW_NUMBER() OVER (ORDER BY m.DCMetadataID) AS RowNum,
						t.ItemID,
						SUBSTRING(m.DCElementValue, 1, 255) AS CreatorName
				FROM	#tmpItem t INNER JOIN dbo.IADCMetadata m
							ON t.ItemID = m.ItemID
							AND m.DCElementName = 'creator'
				) X
	END

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get final Creator metadata

	-- Build the MarcCreatorFull from the information we now have
	UPDATE	#tmpCreator
	SET		MARCCreator_Full = LEFT(ISNULL(MarcCreator_a, '') + ' ' + 
									ISNULL(MarcCreator_b, '') + ' ' +
									ISNULL(MarcCreator_c, '') + ' ' + 
									ISNULL(MarcCreator_q + ' ', '') + 
									ISNULL(MarcCreator_d, ''), 450)

	-- Get the creator role type identifier
	UPDATE	#tmpCreator
	SET		CreatorRoleTypeID = r.AuthorRoleID
	FROM	#tmpCreator t INNER JOIN dbo.BHLAuthorRole r
				ON t.MARCDataFieldTag = r.MARCDataFieldTag

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Keywords from _META.XML

	-- If we don't have a MARC record, then see if there are any subjects in the metadata tables
	IF NOT EXISTS (SELECT t.ItemID FROM #tmpTitle t INNER JOIN dbo.IAMarc m ON t.ItemID = m.ItemID)
	BEGIN
		INSERT INTO #tmpKeyword
		SELECT DISTINCT
				t.ItemID,
				0,
				SUBSTRING(m.DCElementValue, 1, 50),
				'650',
				'a'
		FROM	#tmpItem t INNER JOIN dbo.IADCMetadata m
					ON t.ItemID = m.ItemID
					AND m.DCElementName = 'subject'
	END

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Pages

	INSERT INTO #tmpPage (ItemID, BHLItemID, BarCode, FileNamePrefix, SequenceOrder, 
						[Year], Volume, Issue, IssuePrefix, LastModifiedUserID, ExternalURL)
	SELECT	p.ItemID, 
			0, 
			i.BarCode, 
			ISNULL(REPLACE(p.LocalFileName, '.txt', ''), ''), 
			p.Sequence, 
			p.[Year],
			p.Volume,
			p.Issue,
			p.IssuePrefix,
			1,
			p.ExternalURL
	FROM	dbo.vwIAPage p INNER JOIN #tmpItem i
				ON p.ItemID = i.ItemID

	-- Add the 'corrected' sequence order to the table
	UPDATE	#tmpPage
	SET		SequenceOrderCorrected = newp.NewSequenceOrder
	FROM	#tmpPage p INNER JOIN (	SELECT	ROW_NUMBER() OVER (PARTITION BY ItemID ORDER BY SequenceOrder) AS NewSequenceOrder,
											*
									FROM	#tmpPage
								) newp
				ON p.BarCode = newp.BarCode
				AND p.FileNamePrefix = newp.FileNamePrefix
				AND p.SequenceOrder = newp.SequenceOrder

	-- Use the 'corrected' sequence order to 'correct' the external url for each page.
	-- The ExternalUrl is the address of the flippy images.  The flippy images are
	-- derived from the JP2s and renumbered so no gaps exist in the sequence; that's
	-- why their numbering should match the 'corrected' sequence order.
	UPDATE	#tmpPage
	SET		ExternalUrl = CASE WHEN ExternalUrl <> '' THEN 
							LEFT(ExternalUrl, CHARINDEX('.jpg', ExternalUrl) - 5) +
							RIGHT('0000' + CONVERT(nvarchar(4), SequenceOrderCorrected), 4) + '.jpg'
							ELSE ExternalURL END
	WHERE	SequenceOrder <> SequenceOrderCorrected

	/*
	-- Use the 'original' sequence order to build the alternate external url for each page.
	-- The 'original' sequence order is also used to number the FileNamePrefix, and this 
	-- should match the JP2 images (OCR... i.e. FileNamePrefix... is derived directly from
	-- the JP2s, so their numbering should match).
	UPDATE	#tmpPage
	SET		AltExternalURL = 'http://www.archive.org/download/' + 
				BarCode + '/' + BarCode + '_jp2.zip/' + 
				BarCode + '_jp2/' + BarCode + '_' + 
				RIGHT('0000' + CONVERT(nvarchar(4), SequenceOrder), 4) + '.jp2'
	*/

	-- 2010-08-16  Use the new IA method for addressing images.  This addressing
	-- scheme masks the type of the source images (JP2, TIF).  For example:
	-- http://www.archive.org/download/seasourknowledge49russ/page/n194.jpg
	-- The number used in the file name is a zero-based sequential indexer.  So
	-- the pages for a given 100-page book are numbered n0, n1, n2,... n99.
	UPDATE	#tmpPage
	SET		AltExternalURL = '/download/' +
				BarCode + '/page/n' + CONVERT(nvarchar(4), SequenceOrderCorrected - 1) -- + '.jpg'


	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Page Types

	DECLARE @PageTypeTextID INT
	SELECT DISTINCT @PageTypeTextID = BHLPageTypeID FROM dbo.IAScandataPageType WHERE BHLPageTypeName = 'Text'

	-- Get the alternate page types
	INSERT INTO #tmpPage_PageType
	SELECT DISTINCT
			t.BarCode,
			t.FileNamePrefix,
			t.SequenceOrder,
			NULL,
			ISNULL(map.BHLPageTypeID, @PageTypeTextID)	-- Default to 'text' page type
	FROM	#tmpPage t INNER JOIN dbo.IAScandata s
				ON t.ItemID = s.ItemID
				AND t.SequenceOrder = s.Sequence
			INNER JOIN dbo.IAScandataAltPageType pt
				ON s.ScandataID = pt.ScandataID
			LEFT JOIN dbo.IAScandataPageType map
				ON pt.PageType = map.ExternalPageType

	-- Get the "normal" page types for any pages still without page types
	INSERT INTO #tmpPage_PageType
	SELECT DISTINCT
			t.BarCode,
			t.FileNamePrefix,
			t.SequenceOrder,
			NULL,
			ISNULL(pt.BHLPageTypeID, @PageTypeTextID)	-- Default to 'text' page type
	FROM	#tmpPage t INNER JOIN dbo.vwIAPage p
				ON t.ItemID = p.ItemID
				AND t.SequenceOrder = p.Sequence
			LEFT JOIN dbo.IAScandataPageType pt
				ON p.PageType = pt.ExternalPageType
			LEFT JOIN #tmpPage_PageType tp
				ON t.BarCode = tp.BarCode
				AND t.FileNamePrefix = tp.FileNamePrefix
				AND t.SequenceOrder = tp.SequenceOrder
	WHERE	ISNULL(p.PageType, '') <> ''
	AND		tp.BarCode IS NULL
					
	-- Add the 'corrected' sequence order to the table
	UPDATE	#tmpPage_PageType
	SET		SequenceOrderCorrected = p.SequenceOrderCorrected
	FROM	#tmpPage_PageType pt INNER JOIN #tmpPage p
				ON pt.BarCode = p.BarCode
				AND pt.FileNamePrefix = p.FileNamePrefix
				AND pt.SequenceOrder = p.SequenceOrder
	
	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Indicated Pages

	-- Use the alternate page numbers
	INSERT INTO #tmpIndicatedPage
	SELECT	t.BarCode,
			t.FileNamePrefix,
			t.SequenceOrder,
			NULL,
			0,
			n.Sequence,
			n.PagePrefix,
			n.PageNumber,
			n.Implied
	FROM	#tmpPage t INNER JOIN dbo.IAScandata s
				ON t.ItemID = s.ItemID
				AND t.SequenceOrder = s.Sequence
			INNER JOIN dbo.IAScandataAltPageNumber n
				ON s.ScandataID = n.ScandataID

	-- Use the "normal" page number for any pages still without page numbers
	INSERT INTO #tmpIndicatedPage
	SELECT	t.BarCode,
			t.FileNamePrefix,
			t.SequenceOrder,
			NULL,
			0,
			1,
			'Page',
			p.PageNumber,
			0
	FROM	#tmpPage t INNER JOIN dbo.vwIAPage p
				ON t.ItemID = p.ItemID
				AND t.SequenceOrder = p.Sequence
			LEFT JOIN #tmpIndicatedPage ip
				ON t.BarCode = ip.BarCode
				AND t.FileNamePrefix = ip.FileNamePrefix
				AND t.SequenceOrder = ip.SequenceOrder
	WHERE	ISNULL(p.PageNumber, '') <> ''
	AND		ip.BarCode IS NULL
	
	-- Add the 'corrected' sequence order to the table
	UPDATE	#tmpIndicatedPage
	SET		SequenceOrderCorrected = p.SequenceOrderCorrected
	FROM	#tmpIndicatedPage ip INNER JOIN #tmpPage p
				ON ip.BarCode = p.BarCode
				AND ip.FileNamePrefix = p.FileNamePrefix
				AND ip.SequenceOrder = p.SequenceOrder

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Get Segments

	INSERT INTO #tmpSegment (ItemID, BarCode, SequenceOrder, SegmentGenreID, Title, SortTitle, ContainerTitle, PublicationDetails,
		PublisherName, PublisherPlace, Volume, Series, Issue, Edition, [Date], InstitutionCode, LanguageCode, RightsStatus,
		RightsStatement, LicenseUrl
		)
	SELECT	s.ItemID,
			i.BarCode,
			s.Sequence,
			s.BHLSegmentGenreID,
			s.Title,
			dbo.fnGetSortString(
				CASE
					WHEN LEFT(s.Title, 1) = '"' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 1))
					WHEN LEFT(s.Title, 1) = '''' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 1))
					WHEN LEFT(s.Title, 1) = '[' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 1)) 
					WHEN LEFT(s.Title, 1) = '(' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 1))
					WHEN LEFT(s.Title, 1) = '|' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 1))
					WHEN LOWER(LEFT(s.Title, 2)) = 'a ' AND s.Title <> 'a' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 2)) 
					WHEN LOWER(LEFT(s.Title, 3)) = 'an ' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 3)) 
					WHEN LOWER(LEFT(s.Title, 3)) = 'el ' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 3)) 
					WHEN LOWER(LEFT(s.Title, 3)) = 'il ' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 3)) 
					WHEN LOWER(LEFT(s.Title, 3)) = 'la ' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 3)) 
					WHEN LOWER(LEFT(s.Title, 3)) = 'le ' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 3)) 
					WHEN LOWER(LEFT(s.Title, 4)) = 'das ' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 4)) 
					WHEN LOWER(LEFT(s.Title, 4)) = 'der ' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 4)) 
					WHEN LOWER(LEFT(s.Title, 4)) = 'die ' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 4)) 
					WHEN LOWER(LEFT(s.Title, 4)) = 'ein ' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 4)) 
					WHEN LOWER(LEFT(s.Title, 4)) = 'las ' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 4)) 
					WHEN LOWER(LEFT(s.Title, 4)) = 'les ' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 4)) 
					WHEN LOWER(LEFT(s.Title, 4)) = 'los ' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 4)) 
					WHEN LOWER(LEFT(s.Title, 4)) = 'the ' THEN LTRIM(RIGHT(s.Title, LEN(s.Title) - 4)) 
					ELSE s.Title
				END 
			) AS SortTitle,
			SUBSTRING(t.FullTitle, 1, 2000) AS ContainerTitle,
			ISNULL(t.PublicationDetails, ''),
			SUBSTRING(ISNULL(t.Datafield_260_b, ''), 1, 250) AS PublisherName,
			ISNULL(t.Datafield_260_a, '') AS PublisherPlace,
			s.Volume,
			s.Series,
			s.Issue,
			SUBSTRING(ISNULL(t.EditionStatement, ''), 1, 400),
			s.[Date],
			i.InstitutionCode,
			s.LanguageCode,
			ISNULL(i.CopyrightStatus, ''),
			ISNULL(i.Rights, ''),
			ISNULL(i.LicenseUrl, '')
	FROM	dbo.IASegment s 
			INNER JOIN #tmpItem i ON s.ItemID = i.ItemID
			INNER JOIN #tmpTitle t ON s.ItemID = t.ItemID

	-- Get Segment Pages
	INSERT INTO #tmpSegmentPage (ItemID, BarCode, SegmentSequenceOrder, PageSequenceOrder )
	SELECT	s.ItemID,
			i.BarCode,
			s.Sequence,
			p.PageSequence
	FROM	dbo.IASegmentPage p
			INNER JOIN dbo.IASegment s ON p.SegmentID = s.SegmentID
			INNER JOIN #tmpItem i ON s.ItemID = i.ItemID;

	-- Add page range information to the segment metadata
	WITH StartEndPages (ItemID, SegmentSequenceOrder, StartPageSeq, EndPageSeq)
	AS (
		SELECT	ItemID, SegmentSequenceOrder, MIN(PageSequenceOrder), MAX(PageSequenceOrder)
		FROM	#tmpSegmentPage
		GROUP BY ItemID, SegmentSequenceOrder
	)
	UPDATE	#tmpSegment
	SET		PageRange =	
				CASE
					WHEN scstart.PageNumber <> '' OR scend.PageNumber <> '' 
					THEN scstart.PageNumber + '--' + scend.PageNumber
					ELSE ''
				END,
			StartPageNumber = scstart.PageNumber,
			EndPageNumber = scend.PageNumber
	FROM	#tmpSegment s
			INNER JOIN StartEndPages p ON s.ItemID = p.ItemID AND s.SequenceOrder = p.SegmentSequenceOrder
			INNER JOIN dbo.IAScandata scstart ON p.ItemID = scstart.ItemID AND p.StartPageSeq = scstart.Sequence
			INNER JOIN dbo.IAScandata scend ON p.ItemID = scend.ItemID AND p.EndPageSeq = scend.Sequence

	-- Get Segment Identifiers
	INSERT INTO #tmpSegmentIdentifier
	SELECT DISTINCT
			s.ItemID,
			i.BarCode,
			s.Sequence,
			'DOI',
			s.DOI
	FROM	dbo.IASegment s
			INNER JOIN #tmpItem i ON s.ItemID = i.ItemID

	-- Get Segment Authors
	INSERT INTO #tmpSegmentAuthor ( ItemID, BarCode, SegmentSequenceOrder, SequenceOrder, BHLAuthorID, LastName, FirstName, StartDate, EndDate )
	SELECT	s.ItemID,
			i.BarCode,
			s.Sequence,
			ROW_NUMBER() OVER (PARTITION BY i.BarCode, s.Sequence ORDER BY i.BarCode, s.Sequence, a.Sequence),
			a.BHLAuthorID,
			a.LastName,
			a.FirstName,
			a.StartDate,
			a.EndDate
	FROM	dbo.IASegmentAuthor a
			INNER JOIN dbo.IASegment s ON a.SegmentID = s.SegmentID
			INNER JOIN #tmpItem i ON s.ItemID = i.ItemID
	WHERE	(a.LastName <> '' OR a.FirstName <> '')

	-- Get Segment Author Identifiers
	INSERT INTO #tmpSegmentAuthorIdentifier
		(
		ItemID,
		BarCode,
		SegmentSequenceOrder,
		SequenceOrder,
		ProductionIdentifierID,
		IdentifierValue
		)
	SELECT	s.ItemID,
			i.BarCode,
			s.Sequence,
			ROW_NUMBER() OVER (PARTITION BY i.BarCode, s.Sequence ORDER BY i.BarCode, s.Sequence, a.Sequence),
			a.BHLIdentifierID,
			a.IdentifierValue
	FROM	dbo.IASegmentAuthor a
			INNER JOIN dbo.IASegment s ON a.SegmentID = s.SegmentID
			INNER JOIN #tmpItem i ON s.ItemID = i.ItemID
	WHERE	a.BHLIdentifierID IS NOT NULL
	AND		(a.LastName <> '' OR a.FirstName <> '')

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================
	-- Update the import tables
	BEGIN TRY
		DECLARE @ImportSourceID int
		SET @ImportSourceID = 1	-- Internet Archive

		BEGIN TRAN

		-- Insert new titles into the import tables
		INSERT INTO dbo.Title (ImportStatusID, ImportSourceID, MARCBibID, MARCLeader, FullTitle, 
			ShortTitle, UniformTitle, SortTitle, PartNumber, PartName, CallNumber, PublicationDetails, 
			StartYear, EndYear, Datafield_260_a, Datafield_260_b, Datafield_260_c, InstitutionCode, 
			LanguageCode, OriginalCatalogingSource, EditionStatement, CurrentPublicationFrequency,
			PublishReady, ImportKey)
		SELECT DISTINCT 10, @ImportSourceID, tmp.MARCBibID, tmp.MARCLeader, 
			CONVERT(nvarchar(4000), tmp.FullTitle),	tmp.ShortTitle, tmp.UniformTitle,
			SUBSTRING(tmp.SortTitle, 1, 60), tmp.PartNumber, tmp.PartName, tmp.CallNumber, 
			tmp.PublicationDetails, tmp.StartYear, tmp.EndYear,	tmp.Datafield_260_a, 
			tmp.Datafield_260_b, tmp.Datafield_260_c, tmp.InstitutionCode, tmp.LanguageCode,
			tmp.OriginalCatalogingSource, tmp.EditionStatement, tmp.CurrentPublicationFrequency,
			1, CONVERT(nvarchar(50), tmp.ItemID)
		FROM #tmpTitle tmp

		-- =======================================================================

		-- Insert new title tags into the import tables
		INSERT INTO dbo.TitleTag (TagText, ImportStatusID, ImportSourceID,
				MarcDataFieldTag, MarcSubFieldCode, ImportKey)
		SELECT	LTRIM(RTRIM(SUBSTRING(tt.TagText, 1, 50))), 10, @ImportSourceID, 
				tt.MarcDataFieldTag, tt.MarcSubFieldCode, CONVERT(nvarchar(50), tt.ItemID)
		FROM	#tmpKeyword tt
				INNER JOIN #tmpItem i ON tt.ItemID = i.ItemID AND i.VirtualTitleID IS NULL

		-- =======================================================================

		-- Insert new title notes into the import tables
		INSERT INTO dbo.TitleNote (NoteText, ImportStatusID, ImportSourceID,
				MarcDataFieldTag, MarcIndicator1, NoteSequence, ImportKey)
		SELECT	LTRIM(RTRIM(NoteText)), 10, @ImportSourceID, MarcDataFieldTag, 
				MarcIndicator1, NoteSequence, CONVERT(nvarchar(50), ItemID)
		FROM	#tmpTitleNote

		-- =======================================================================

		-- Insert new title identifiers into the import tables
		INSERT INTO dbo.Title_TitleIdentifier (ImportStatusID, ImportSourceID,
			IdentifierName, IdentifierValue, ImportKey)
		SELECT	10, @ImportSourceID, t.IdentifierName,
				t.IdentifierValue, CONVERT(nvarchar(50), t.ItemID)
		FROM	#tmpIdentifier t
				INNER JOIN #tmpItem i ON t.ItemID = i.ItemID AND i.VirtualTitleID IS NULL
		WHERE	ISNULL(t.IdentifierValue, '') <> ''

		-- =======================================================================

		-- Insert new title associations into the import tables
		INSERT INTO dbo.TitleAssociation (ImportStatusID, ImportSourceID, 
			MARCTag, MARCIndicator2, Title, Section, Volume, Heading,
			Publication, Relationship, Active, ImportKey)
		SELECT	10, @ImportSourceID, t.MARCTag, 
				t.MARCIndicator2, t.Title, t.Section, t.Volume, t.Heading,
				t.Publication, t.Relationship, 1, CONVERT(nvarchar(50), t.ItemID)
		FROM	#tmpTitleAssociation t

		-- Get the various title association title identifiers
		-- OCLC
		INSERT INTO dbo.TitleAssociation_TitleIdentifier (ImportStatusID, ImportSourceID, 
			MARCTag, MARCIndicator2, Title, Section, Volume, 
			Heading, Publication, Relationship, IdentifierName, IdentifierValue,
			ImportKey)
		SELECT	10, @ImportSourceID, t.MARCTag, 
				t.MARCIndicator2, t.Title, t.Section, t.Volume, t.Heading,
				t.Publication, t.Relationship, 'OCLC' AS IdentifierName, 
				LTRIM(RTRIM(REPLACE(m.SubFieldValue, '(OCoLC)', ''))) AS IdentifierValue,
				CONVERT(nvarchar(50), t.ItemID)
		FROM	#tmpTitleAssociation t INNER JOIN vwIAMarcDataField m
					ON t.MarcDataFieldID = m.MarcDataFieldID
		WHERE	m.Code = 'w' 
		AND		m.SubFieldValue LIKE '(OCoLC)%'

		-- DLC (Library of Congress)
		INSERT INTO dbo.TitleAssociation_TitleIdentifier (ImportStatusID, ImportSourceID, 
			MARCTag, MARCIndicator2, Title, Section, Volume, 
			Heading, Publication, Relationship, IdentifierName, IdentifierValue,
			ImportKey)
		SELECT	10, @ImportSourceID, t.MARCTag, 
				t.MARCIndicator2, t.Title, t.Section, t.Volume, 
				t.Heading, t.Publication, t.Relationship, 'DLC' as IdentifierName, 
				dbo.BHLfnGetLCCNValue(REPLACE(m.SubFieldValue, '(DLC)', '')) AS IdentifierValue,
				CONVERT(nvarchar(50), t.ItemID)
		FROM	#tmpTitleAssociation t INNER JOIN vwIAMarcDataField m
					ON t.MarcDataFieldID = m.MarcDataFieldID
		WHERE	m.Code = 'w' 
		AND		m.SubFieldValue LIKE '(DLC)%'

		-- ISSN
		INSERT INTO dbo.TitleAssociation_TitleIdentifier (ImportStatusID, ImportSourceID, 
			MARCTag, MARCIndicator2, Title, Section, Volume, 
			Heading, Publication, Relationship, IdentifierName, IdentifierValue,
			ImportKey)
		SELECT	10, @ImportSourceID, t.MARCTag, 
				t.MARCIndicator2, t.Title, t.Section, t.Volume, 
				t.Heading, t.Publication, t.Relationship, 
				dbo.BHLfnGetISSNName(REPLACE(m.SubFieldValue, ';', '')) AS IdentifierName, 
				dbo.BHLfnGetISSNValue(REPLACE(m.SubFieldValue, ';', '')) AS IdentifierValue,
				CONVERT(nvarchar(50), t.ItemID)
		FROM	#tmpTitleAssociation t INNER JOIN vwIAMarcDataField m
					ON t.MarcDataFieldID = m.MarcDataFieldID
		WHERE	Code = 'x'

		-- =======================================================================

		-- Insert new title variants into the import tables
		INSERT INTO dbo.TitleVariant (ImportStatusID, ImportSourceID, 
			MARCTag, MARCIndicator2, Title, TitleRemainder, PartNumber,
			PartName, ImportKey)
		SELECT	10, @ImportSourceID, 
				t.MARCTag, t.MARCIndicator2, t.Title, t.TitleRemainder, t.PartNumber,
				t.PartName, CONVERT(nvarchar(50), t.ItemID)
		FROM	#tmpTitleVariant t

		-- =======================================================================

		-- Insert new creators into the import tables
		INSERT INTO dbo.Creator (ImportStatusID, ImportSourceID, CreatorName, DOB, DOD, 
			MARCDataFieldTag, MARCCreator_a, MARCCreator_b, MARCCreator_c, MARCCreator_d, 
			MARCCreator_q, MARCCreator_Full)
		SELECT	10, @ImportSourceID, dbo.BHLfnConvertToTitleCase(dbo.BHLfnAddAuthorNameSpaces(t.CreatorName)), 
				t.DOB, t.DOD, t.MARCDataFieldTag, t.MARCCreator_a, t.MARCCreator_b, t.MARCCreator_c, 
				t.MARCCreator_d, t.MARCCreator_q, t.MARCCreator_Full
		FROM	#tmpCreator t --LEFT JOIN dbo.Creator c
					--ON ISNULL(t.MARCCreator_a, '') = ISNULL(c.MARCCreator_a, '')
					--AND ISNULL(t.MARCCreator_b, '') = ISNULL(c.MARCCreator_b, '')
					--AND ISNULL(t.MARCCreator_c, '') = ISNULL(c.MARCCreator_c, '')
					--AND ISNULL(t.MARCCreator_d, '') = ISNULL(c.MARCCreator_d, '')
					--AND ISNULL(t.MARCCreator_q, '') = ISNULL(c.MARCCreator_q, '')
		WHERE	t.MARCCreator_5 IS NULL
		--AND		c.CreatorID IS NULL

		-- Insert new title_creator records into the import tables
		INSERT INTO dbo.Title_Creator (CreatorName, MARCCreator_a, 
			MARCCreator_b, MARCCreator_c, MARCCreator_d, MARCCreator_e,
			MARCCreator_q, MARCCreator_t, CreatorRoleTypeID, SequenceOrder,
			ImportStatusID, ImportSourceID, ImportKey)
		SELECT	dbo.BHLfnConvertToTitleCase(dbo.BHLfnAddAuthorNameSpaces(c.CreatorName)),
				c.MARCCreator_a, c.MARCCreator_b, c.MARCCreator_c, c.MARCCreator_d, 
				c.MARCCreator_e, c.MARCCreator_q, c.MARCCreator_t, c.CreatorRoleTypeID, 
				c.SequenceOrder, 10, @ImportSourceID, CONVERT(nvarchar(50), c.ItemID)
		FROM	#tmpCreator c
				INNER JOIN #tmpItem i ON c.ItemID = i.ItemID AND i.VirtualTitleID IS NULL
		WHERE	c.MARCCreator_5 IS NULL

		-- =======================================================================

		-- Insert new title languages into the import tables
		INSERT INTO dbo.TitleLanguage (ImportStatusID, ImportSourceID, LanguageCode, ImportKey)
		SELECT	10, @ImportSourceID, t.LanguageCode, t.ItemID
		FROM	#tmpTitleLanguage t
		WHERE	ISNULL(t.LanguageCode, '') <> ''

		-- =======================================================================

		-- Insert new items into the import tables
		INSERT INTO dbo.Item (ImportStatusID, ImportSourceID, MARCBibID, Sponsor,
			BarCode, ItemSequence, MARCItemID, Volume, Issue, InstitutionCode, LanguageCode, 
			VaultID, ItemStatusID, ScanningUser, ScanningDate, [Year], IdentifierBib,
			ZQuery, LicenseUrl, Rights, DueDiligence, CopyrightStatus, CopyrightRegion,
			CopyrightComment, CopyrightEvidence, CopyrightEvidenceOperator,
			CopyrightEvidenceDate, ImportKey, ScanningInstitutionCode, RightsHolderCode,
			ItemDescription, EndYear, StartVolume, EndVolume, StartIssue, EndIssue,
			StartNumber, EndNumber, StartSeries, EndSeries, StartPart, EndPart, 
			PageProgression, VirtualVolume, VirtualTitleID, Summary, SegmentGenreID,
			PublicationDetails, PublisherName, SegmentDate)
		SELECT	10, @ImportSourceID, t.MARCBibID, t.Sponsor, t.BarCode,
				t.MaxExistingItemSequence + t.ItemSequence, t.MARCItemID, t.Volume, 
				t.Issue, t.InstitutionCode, t.LanguageCode, t.VaultID, t.ItemStatusID, 
				t.ScanningUser, t.ScanningDate, t.[Year], t.IdentifierBib, t.ZQuery,
				t.LicenseUrl, t.Rights, t.DueDiligence, t.CopyrightStatus, t.CopyrightRegion,
				t.CopyrightComment, t.CopyrightEvidence, t.CopyrightEvidenceOperator,
				t.CopyrightEvidenceDate, CONVERT(nvarchar(50), t.ItemID), 
				ScanningInstitutionCode, RightsHolderCode, ItemDescription, EndYear, 
				StartVolume, EndVolume, StartIssue, EndIssue, StartNumber, EndNumber, 
				StartSeries, EndSeries, StartPart, EndPart, PageProgression, VirtualVolume,
				VirtualTitleID, Summary, SegmentGenreID, PublicationDetails, PublisherName,
				VirtualVolumeSegmentDate
		FROM	#tmpItem t

		-- =======================================================================

		-- Insert new item languages into the import tables
		INSERT INTO dbo.ItemLanguage (ImportStatusID, ImportSourceID, BarCode, LanguageCode)
		SELECT	10, @ImportSourceID, t.BarCode, t.LanguageCode
		FROM	#tmpItemLanguage t
		WHERE	ISNULL(t.LanguageCode, '') <> ''

		-- =======================================================================

		-- Insert new item identifiers into the import tables
		INSERT INTO dbo.ItemIdentifier (ImportStatusID, ImportSourceID, Barcode,
			IdentifierName, IdentifierValue)
		SELECT	10, @ImportSourceID, i.BarCode, t.IdentifierName, t.IdentifierValue
		FROM	#tmpIdentifier t
				INNER JOIN #tmpItem i ON t.ItemID = i.ItemID AND i.VirtualTitleID IS NOT NULL
		WHERE	ISNULL(t.IdentifierValue, '') <> ''

		-- =======================================================================

		-- Insert new item keywords into the import tables
		INSERT INTO dbo.ItemKeyword (Barcode, ImportStatusID, ImportSourceID, Keyword)
		SELECT	i.Barcode, 10, @ImportSourceID, LTRIM(RTRIM(SUBSTRING(k.TagText, 1, 50)))
		FROM	#tmpKeyword k
				INNER JOIN #tmpItem i ON k.ItemID = i.ItemID AND i.VirtualTitleID IS NOT NULL
		AND		k.TagText <> ''

		-- =======================================================================

		-- Insert new item creator records into the import tables
		INSERT INTO dbo.ItemCreator (Barcode, ImportStatusID, ImportSourceID, CreatorName, 
			CreatorRoleTypeID, SequenceOrder)
		SELECT	i.BarCode, 10, @ImportSourceID, 
				dbo.BHLfnConvertToTitleCase(dbo.BHLfnAddAuthorNameSpaces(c.CreatorName)),
				c.CreatorRoleTypeID, c.SequenceOrder
		FROM	#tmpCreator c
				INNER JOIN #tmpItem i ON c.ItemID = i.ItemID AND i.VirtualTitleID IS NOT NULL

		-- =======================================================================

		-- Insert the new pages into the import tables
		INSERT INTO dbo.Page (ImportStatusID, ImportSourceID, BarCode, FileNamePrefix, 
			SequenceOrder, [Year], Volume, Issue, IssuePrefix, ExternalLastModifiedUser, 
			ExternalURL, AltExternalURL)
		SELECT	10, @ImportSourceID, t.BarCode, t.FileNamePrefix, t.SequenceOrderCorrected, 
				t.[Year], t.Volume, t.Issue, t.IssuePrefix, t.LastModifiedUserID, 
				t.ExternalURL, t.AltExternalURL
		FROM	#tmpPage t LEFT JOIN dbo.Page p
					ON t.BarCode = p.BarCode
					AND t.FileNamePrefix = p.FileNamePrefix
		WHERE	p.PageID IS NULL

		-- =======================================================================

		-- Insert new page_pagetype records into the import tables
		INSERT INTO dbo.Page_PageType (BarCode, FileNamePrefix, SequenceOrder, PageTypeID, 
			ImportStatusID, ImportSourceID, Verified)
		SELECT	t.BarCode, t.FileNamePrefix, t.SequenceOrderCorrected, t.PageTypeID, 
			10, @ImportSourceID, 0
		FROM	#tmpPage_PageType t LEFT JOIN dbo.Page_PageType pt
 					ON t.BarCode = pt.BarCode
					AND t.FileNamePrefix = pt.FileNamePrefix
					AND t.PageTypeID = pt.PageTypeID
		WHERE	pt.PagePageTypeID IS NULL

		-- =======================================================================

		-- Insert new indicated pages into the import tables
		INSERT INTO dbo.IndicatedPage (BarCode, FileNamePrefix, SequenceOrder, Sequence, 
			ImportStatusID, ImportSourceID, PagePrefix, PageNumber, Implied)
		SELECT	t.BarCode, t.FileNamePrefix, t.SequenceOrderCorrected, t.Sequence, 
				10, @ImportSourceID, t.PagePrefix, t.PageNumber, t.Implied
		FROM	#tmpIndicatedPage t LEFT JOIN dbo.IndicatedPage ip
					ON t.BarCode = ip.BarCode
					AND t.FileNamePrefix = ip.FileNamePrefix
					AND t.Sequence = ip.Sequence
		WHERE	ip.IndicatedPageID IS NULL

		-- =======================================================================

		-- Insert new segments into the import tables
		INSERT INTO	dbo.Segment (ImportStatusID, ImportSourceID, BarCode, SequenceOrder, SegmentStatusID,
			SegmentGenreID, Title, SortTitle, ContainerTitle, PublicationDetails, PublisherName, 
			PublisherPlace, Volume, Series, Issue, Edition, [Date], PageRange, StartPageNumber, EndPageNumber, 
			InstitutionCode, LanguageCode, RightsStatus, RightsStatement, LicenseName, LicenseUrl)
		SELECT	10, @ImportSourceID, t.BarCode, t.SequenceOrder, 10, t.SegmentGenreID, t.Title, t.SortTitle, 
				t.ContainerTitle, t.PublicationDetails, t.PublisherName, t.PublisherPlace, t.Volume, t.Series, 
				t.Issue, t.Edition, t.[Date], t.PageRange, t.StartPageNumber, t.EndPageNumber, t.InstitutionCode, 
				t.LanguageCode, t.RightsStatus, t.RightsStatement, t.LicenseName, t.LicenseUrl
		FROM	#tmpSegment t LEFT JOIN dbo.Segment s
					ON t.BarCode = s.BarCode
					AND	t.SequenceOrder = s.SequenceOrder
		WHERE	s.SegmentID IS NULL

		-- =======================================================================

		-- Insert new segment pages into the import tables
		INSERT INTO dbo.SegmentPage (ImportStatusID, ImportSourceID, BarCode, SegmentSequenceOrder, 
			PageSequenceOrder)
		SELECT	10, @ImportSourceID, t.Barcode, t.SegmentSequenceOrder, p.SequenceOrderCorrected
		FROM	#tmpSegmentPage t 
				INNER JOIN #tmpPage p
					ON t.BarCode = p.BarCode
					AND t.PageSequenceOrder = p.SequenceOrder
				LEFT JOIN dbo.SegmentPage sp 
					ON t.BarCode = sp.BarCode 
					AND t.SegmentSequenceOrder = sp.SegmentSequenceOrder 
					AND p.SequenceOrderCorrected = sp.PageSequenceOrder
		WHERE	sp.SegmentPageID IS NULL

		-- =======================================================================

		-- Insert new segment identifiers into the import tables
		INSERT INTO dbo.SegmentIdentifier (ImportStatusID, ImportSourceID, BarCode, SegmentSequenceOrder,
			IdentifierName, IdentifierValue)
		SELECT	10, @ImportSourceID, BarCode, SegmentSequenceOrder, IdentifierName, IdentifierValue
		FROM	#tmpSegmentIdentifier
		WHERE	ISNULL(IdentifierValue, '') <> ''

		-- =======================================================================

		-- Insert new segment authors into the import tables
		INSERT INTO dbo.SegmentAuthor (ImportStatusID, ImportSourceID, BarCode, SegmentSequenceOrder, 
			SequenceOrder, LastName, FirstName, StartDate, EndDate, ProductionAuthorID)
		SELECT	10, @ImportSourceID, t.BarCode, t.SegmentSequenceOrder, t.SequenceOrder, t.LastName, 
				t.FirstName, t.Startdate, t.EndDate, t.BHLAuthorID
		FROM	#tmpSegmentAuthor t LEFT JOIN dbo.SegmentAuthor a
					ON t.BarCode = a.BarCode
					AND t.SegmentSequenceOrder = a.SegmentSequenceOrder
					AND t.SequenceOrder = a.SequenceOrder
		WHERE	a.SegmentAuthorID IS NULL

		-- =======================================================================

		-- Insert new segment author identifiers into the import tables
		INSERT INTO dbo.SegmentAuthorIdentifier (ImportStatusID, ImportSourceID, BarCode, SegmentSequenceOrder, 
			SequenceOrder, ProductionIdentifierID, IdentifierValue)
		SELECT	10, @ImportSourceID, t.BarCode, t.SegmentSequenceOrder, t.SequenceOrder, t.ProductionIdentifierID, 
				t.IdentifierValue
		FROM	#tmpSegmentAuthorIdentifier t LEFT JOIN dbo.SegmentAuthorIdentifier a
					ON t.BarCode = a.BarCode
					AND t.SegmentSequenceOrder = a.SegmentSequenceOrder
					AND t.SequenceOrder = a.SequenceOrder
					AND	t.ProductionIdentifierID = a.ProductionIdentifierID
					AND t.IdentifierValue = a.IdentifierValue
		WHERE	a.SegmentAuthorIdentifierID IS NULL

		-- =======================================================================

		-- Update the statuses of the items just loaded into the import tables.
		-- Also, save the identifiers with the original item information.
		UPDATE	dbo.IAItem
		SET		ShortTitle = t.ShortTitle,
				LastModifiedDate = GETDATE()
		FROM	dbo.IAItem i INNER JOIN #tmpTitle t
					ON i.ItemID = t.ItemID

		UPDATE	dbo.IAItem
		SET		ItemStatusID = 40,	-- Complete
				LastProductionDate = GETDATE(),
				MARCBibID = t.MARCBibID,
				BarCode = t.BarCode,
				LastModifiedDate = GETDATE()
		FROM	dbo.IAItem i INNER JOIN #tmpItem t
					ON i.ItemID = t.ItemID

		COMMIT TRAN

		SELECT 1 AS Result
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN

		-- Record the error
		INSERT INTO dbo.IAItemError (ItemID, ErrorDate, Number, Severity, State, 
			[Procedure], Line, [Message])
		SELECT	@ItemID, GETDATE(), ERROR_NUMBER(), ERROR_SEVERITY(),
			ERROR_STATE(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE()

		-- Mark the item as in error
		UPDATE	dbo.IAItem
		SET		ItemStatusID = 99,	-- Error
				LastModifiedDate = GETDATE()
		WHERE	ItemID = @ItemID

		SELECT 0 AS Result
	END CATCH

	-- =======================================================================
	-- =======================================================================
	-- =======================================================================

	-- Clean up temp tables
	DROP TABLE #tmpTitle
	DROP TABLE #tmpKeyword
	DROP TABLE #tmpIdentifier
	DROP TABLE #tmpTitleAssociation
	DROP TABLE #tmpTitleVariant
	DROP TABLE #tmpTitleNote
	DROP TABLE #tmpCreator
	DROP TABLE #tmpItem
	DROP TABLE #tmpPage
	DROP TABLE #tmpPage_PageType
	DROP TABLE #tmpIndicatedPage
	DROP TABLE #tmpSegmentAuthorIdentifier
	DROP TABLE #tmpSegmentAuthor
	DROP TABLE #tmpSegmentPage
	DROP TABLE #tmpSegmentIdentifier
	DROP TABLE #tmpSegment
END TRY
BEGIN CATCH
	-- Record the error
	INSERT INTO dbo.IAItemError (ItemID, ErrorDate, Number, Severity, State, 
		[Procedure], Line, [Message])
	SELECT	@ItemID, GETDATE(), ERROR_NUMBER(), ERROR_SEVERITY(),
		ERROR_STATE(), ERROR_PROCEDURE(), ERROR_LINE(), ERROR_MESSAGE()

	IF (ERROR_MESSAGE() = '0 Page records')
	BEGIN
		-- Mark the item as new (so it will be reloaded)
		UPDATE	dbo.IAItem
		SET		ItemStatusID = 10,	-- New
				LastModifiedDate = GETDATE()
		WHERE	ItemID = @ItemID
	END
	ELSE
	BEGIN
		-- Mark the item as in error
		UPDATE	dbo.IAItem
		SET		ItemStatusID = 99,	-- Error
				LastModifiedDate = GETDATE()
		WHERE	ItemID = @ItemID
	END

	SELECT 0 AS Result
END CATCH

SET NOCOUNT OFF

END

GO
