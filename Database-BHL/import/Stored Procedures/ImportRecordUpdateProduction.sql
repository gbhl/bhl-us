CREATE PROCEDURE import.ImportRecordUpdateProduction

@ImportRecordID int,
@UserID int

AS

BEGIN

SET NOCOUNT ON

-- Get the lookup table values that we will need
DECLARE @ImportRecordImportedID int
SELECT @ImportRecordImportedID = ImportRecordStatusID FROM import.ImportRecordStatus WHERE StatusName = 'Imported'
IF (@ImportRecordImportedID IS NULL) RAISERROR('ImportRecordStatus -Imported- not found', 0, 1)

DECLARE @ImportRecordErrorID int
SELECT @ImportRecordErrorID = ImportRecordStatusID FROM import.ImportRecordStatus WHERE StatusName = 'Error'
IF (@ImportRecordErrorID IS NULL) RAISERROR('ImportRecordStatus -Error- not found', 0, 1)

DECLARE @IdentifierISSNID int
SELECT @IdentifierISSNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISSN'
IF (@IdentifierISSNID IS NULL) RAISERROR('Identifier -ISSN- not found', 0, 1)

DECLARE @IdentifierISBNID int
SELECT @IdentifierISBNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ISBN'
IF (@IdentifierISBNID IS NULL) RAISERROR('Identifier -ISBN- not found', 0, 1)

DECLARE @IdentifierOCLCID int
SELECT @IdentifierOCLCID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'OCLC'
IF (@IdentifierOCLCID IS NULL) RAISERROR('Identifier -OCLC- not found', 0, 1)

DECLARE @IdentifierLCCNID int
SELECT @IdentifierLCCNID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DLC'
IF (@IdentifierLCCNID IS NULL) RAISERROR('Identifier -LCCN- not found', 0, 1)

DECLARE @IdentifierARKID int
SELECT @IdentifierARKID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'ARK'
IF (@IdentifierARKID IS NULL) RAISERROR('Identifier -ARK- not found', 0, 1)

DECLARE @IdentifierBiostorID int
SELECT @IdentifierBiostorID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'Biostor'
IF (@IdentifierBiostorID IS NULL) RAISERROR('Identifier -Biostor- not found', 0, 1)

DECLARE @IdentifierJSTORID int
SELECT @IdentifierJSTORID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'JSTOR'
IF (@IdentifierJSTORID IS NULL) RAISERROR('Identifier -JSTOR- not found', 0, 1)

DECLARE @IdentifierTL2ID int
SELECT @IdentifierTL2ID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'TL2'
IF (@IdentifierTL2ID IS NULL) RAISERROR('Identifier -TL2- not found', 0, 1)

DECLARE @IdentifierWikidataID int
SELECT @IdentifierWikidataID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'Wikidata'
IF (@IdentifierWikidataID IS NULL) RAISERROR('Identifier -Wikidata- not found', 0, 1)


DECLARE @DOIStatusExternalID int
SELECT @DOIStatusExternalID = DOIStatusID FROM dbo.DOIStatus WHERE DOIStatusName = 'External DOI'
IF (@DOIStatusExternalID IS NULL) RAISERROR('DOIStatus -External DOI- not found', 0, 1)

DECLARE @InstitutionRoleContributorID int
SELECT @InstitutionRoleContributorID = InstitutionRoleID FROM dbo.InstitutionRole WHERE InstitutionRoleName = 'Contributor'
IF (@InstitutionRoleContributorID IS NULL) RAISERROR('InstitutionRole -Contributor- not found', 0, 1)

DECLARE @InstitutionRoleHoldingInstitutionID int
SELECT @InstitutionRoleHoldingInstitutionID = InstitutionRoleID FROM dbo.InstitutionRole WHERE InstitutionRoleName = 'Holding Institution'
IF (@InstitutionRoleHoldingInstitutionID IS NULL) RAISERROR('InstitutionRole -Holding Institution- not found', 0, 1)

DECLARE @SegmentStatusNewID int
SELECT @SegmentStatusNewID = ItemStatusID FROM dbo.ItemStatus WHERE ItemStatusName = 'New'
IF (@SegmentStatusNewID IS NULL) RAISERROR('ItemStatus -New- not found', 0, 1)

DECLARE @SegmentGenreArticleID int
SELECT @SegmentGenreArticleID = SegmentGenreID FROM dbo.SegmentGenre WHERE GenreName = 'Article'
IF (@SegmentGenreArticleID IS NULL) RAISERROR('SegmentGenre -Article- not found', 0, 1)

DECLARE @DOIEntityTypeSegmentID int
SELECT @DOIEntityTypeSegmentID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Segment'
IF (@DOIEntityTypeSegmentID IS NULL) RAISERROR('DOIEntityType -Segment- not found', 0, 1)

DECLARE @ItemTypeBookID int
SELECT @ItemTypeBookID = ItemTypeID FROM dbo.ItemType WHERE ItemTypeName = 'Book'
IF (@ItemTypeBookID IS NULL) RAISERROR('ItemType -Book- not found', 0, 1)

DECLARE @ItemTypeSegmentID int
SELECT @ItemTypeSegmentID = ItemTypeID FROM dbo.ItemType WHERE ItemTypeName = 'Segment'
IF (@ItemTypeSegmentID IS NULL) RAISERROR('ItemType -Segment- not found', 0, 1)

DECLARE @ItemSourceCitationImportID int
SELECT @ItemSourceCitationImportID = ItemSourceID FROM dbo.ItemSource WHERE SourceName = 'Citation Import'
IF (@ItemSourceCitationImportID IS NULL) RAISERROR('ItemSource -Citation Import- not found', 0, 1)

BEGIN TRY

	--------------------------------------------------------------------

	-- Build temp tables
	CREATE TABLE #tmpRecordKeyword
		(
		ImportRecordKeywordID int NOT NULL,
		ImportRecordID int NOT NULL,
		Keyword nvarchar(50) NOT NULL,
		ProductionKeywordID int NULL
		)

	--------------------------------------------------------------------

	-- Get the keywords to be imported
	INSERT	#tmpRecordKeyword
	SELECT	ImportRecordKeywordID,
			ImportRecordID,
			Keyword,
			NULL
	FROM	import.ImportRecordKeyword
	WHERE	ImportRecordID = @ImportRecordID

	-- Add the new keywords to production (making note of the new KeywordIDs)
	INSERT	dbo.Keyword (Keyword)
	SELECT DISTINCT t.Keyword
	FROM	#tmpRecordKeyword t
			LEFT JOIN dbo.Keyword k ON t.Keyword = k.Keyword COLLATE SQL_Latin1_General_CP1_CI_AI
	WHERE	k.KeywordID IS NULL
	AND		t.Keyword <> 'NULL'

	-- Get Keyword IDs for existing records
	UPDATE	#tmpRecordKeyword
	SET		ProductionKeywordID = k.KeywordID
	FROM	#tmpRecordKeyword t
			INNER JOIN dbo.Keyword k ON t.Keyword = k.Keyword COLLATE SQL_Latin1_General_CP1_CI_AI

	--------------------------------------------------------------------

	-- Add new Item and Segment records to production, making note of the new IDs
	DECLARE @ItemID int
	DECLARE @SegmentID int

	BEGIN TRAN

	-- Update segment metadata
	DECLARE @SegmentGenreID int
	SELECT	@SegmentGenreID = ISNULL(SegmentGenreID, @SegmentGenreArticleID)
	FROM	import.ImportRecord r LEFT JOIN dbo.SegmentGenre g ON r.Genre = g.GenreName
	WHERE	ImportRecordID = @ImportRecordID

	-- Update the related Item record
	UPDATE	i
	SET		Note = CASE WHEN r.Notes = 'NULL' THEN '' WHEN r.Notes = '' THEN i.Note ELSE r.Notes END,
			LastModifiedUserID = @UserID,
			LastModifiedDate = GETDATE()
	FROM	import.ImportRecord r 
			INNER JOIN dbo.vwSegment s ON r.SegmentID = s.SegmentID
			INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
	WHERE	ImportRecordID = @ImportRecordID

	-- Update the Segment record
	UPDATE	s
	SET		SegmentGenreID = @SegmentGenreID,
			Title = CASE WHEN r.Title = 'NULL' THEN '' WHEN r.Title = '' THEN s.Title ELSE r.Title END,
			TranslatedTitle = CASE WHEN r.TranslatedTitle = 'NULL' THEN '' WHEN r.TranslatedTitle = '' THEN s.TranslatedTitle ELSE r.TranslatedTitle END,
			ContainerTitle = CASE WHEN r.JournalTitle = 'NULL' THEN '' WHEN r.JournalTitle = '' THEN s.ContainerTitle ELSE r.JournalTitle END,
			PublicationDetails = CASE WHEN r.PublicationDetails = 'NULL' THEN '' WHEN r.PublicationDetails = '' THEN s.PublicationDetails ELSE r.PublicationDetails END,
			PublisherName = CASE WHEN r.PublisherName = 'NULL' THEN '' WHEN r.PublisherName = '' THEN s.PublisherName ELSE r.PublisherName END,
			PublisherPlace = CASE WHEN r.PublisherPlace = 'NULL' THEN '' WHEN r.PublisherPlace= '' THEN s.PublisherPlace ELSE r.PublisherPlace END,
			Volume = CASE WHEN r.Volume = 'NULL' THEN '' WHEN r.Volume = '' THEN s.Volume ELSE r.Volume END,
			Series = CASE WHEN r.Series = 'NULL' THEN '' WHEN r.Series = '' THEN s.Series ELSE r.Series END,
			Issue = CASE WHEN r.Issue = 'NULL' THEN '' WHEN r.Issue = '' THEN s.Issue ELSE r.Issue END,
			Edition = CASE WHEN r.Edition = 'NULL' THEN '' WHEN r.Edition = '' THEN s.Edition ELSE r.Edition END,
			Summary = CASE WHEN r.Summary = 'NULL' THEN '' WHEN r.Summary = '' THEN s.Summary ELSE r.Summary END,
			[Date] = CASE WHEN r.[Year] = 'NULL' THEN '' WHEN r.[Year] = '' THEN s.[Date] ELSE r.[Year] END,
			PageRange = 
				CASE 
					WHEN r.PageRange = 'NULL' THEN '' 
					WHEN r.StartPage = 'NULL' AND r.EndPage = 'NULL' THEN '' 
					WHEN r.PageRange <> '' THEN r.PageRange 
					WHEN r.StartPage <> '' OR r.EndPage <> '' THEN 
						CASE WHEN r.StartPage = 'NULL' THEN '' ELSE CASE WHEN r.StartPage = '' THEN s.StartPageNumber ELSE r.StartPage END END +
						'--' +
						CASE WHEN r.EndPage = 'NULL' THEN '' ELSE CASE WHEN r.EndPage = '' THEN s.EndPageNumber ELSE r.EndPage END END
					ELSE s.PageRange
				END,
			StartPageID = CASE WHEN r.StartPageID IS NULL THEN s.StartpageID ELSE r.StartPageID END,
			StartPageNumber = CASE WHEN r.StartPage = 'NULL' THEN '' WHEN r.StartPage = '' THEN s.StartPageNumber ELSE r.StartPage END,
			EndPageNumber = CASE WHEN r.EndPage = 'NULL' THEN '' WHEN r.EndPage = '' THEN s.EndPageNumber ELSE r.EndPage END,
			LanguageCode = ISNULL(l1.LanguageCode, l2.LanguageCode),
			Url = CASE WHEN r.Url = 'NULL' THEN '' WHEN r.Url = '' THEN s.Url ELSE r.Url END,
			RightsStatus = CASE WHEN r.CopyrightStatus = 'NULL' THEN '' WHEN r.CopyrightStatus = '' THEN s.RightsStatus ELSE r.CopyrightStatus END,
			RightsStatement = CASE WHEN r.Rights = 'NULL' THEN '' WHEN r.Rights = '' THEN s.RightsStatement ELSE r.Rights END,
			LicenseName = CASE WHEN r.License = 'NULL' THEN '' WHEN r.License = '' THEN s.LicenseName ELSE r.License END,
			LicenseUrl = CASE WHEN r.LicenseUrl = 'NULL' THEN '' WHEN r.LicenseUrl = '' THEN s.LicenseUrl ELSE r.LicenseUrl END,
			LastModifiedUserID = @UserID,
			LastModifiedDate = GETDATE(),
			SortTitle = 
				CASE 
					WHEN r.Title IS NULL THEN '' 
					WHEN r.Title = '' THEN s.SortTitle 
					ELSE dbo.[fnGetSortString](
							CASE
								WHEN LEFT(r.Title, 1) = '"' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 1))
								WHEN LEFT(r.Title, 1) = '''' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 1))
								WHEN LEFT(r.Title, 1) = '[' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 1)) 
								WHEN LEFT(r.Title, 1) = '(' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 1))
								WHEN LEFT(r.Title, 1) = '|' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 1))
								WHEN LOWER(LEFT(r.Title, 2)) = 'a ' AND r.Title <> 'a' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 2)) 
								WHEN LOWER(LEFT(r.Title, 3)) = 'an ' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 3)) 
								WHEN LOWER(LEFT(r.Title, 3)) = 'el ' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 3)) 
								WHEN LOWER(LEFT(r.Title, 3)) = 'il ' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 3)) 
								WHEN LOWER(LEFT(r.Title, 3)) = 'la ' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 3)) 
								WHEN LOWER(LEFT(r.Title, 3)) = 'le ' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 3)) 
								WHEN LOWER(LEFT(r.Title, 4)) = 'das ' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 4)) 
								WHEN LOWER(LEFT(r.Title, 4)) = 'der ' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 4)) 
								WHEN LOWER(LEFT(r.Title, 4)) = 'die ' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 4)) 
								WHEN LOWER(LEFT(r.Title, 4)) = 'ein ' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 4)) 
								WHEN LOWER(LEFT(r.Title, 4)) = 'las ' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 4)) 
								WHEN LOWER(LEFT(r.Title, 4)) = 'les ' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 4)) 
								WHEN LOWER(LEFT(r.Title, 4)) = 'los ' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 4)) 
								WHEN LOWER(LEFT(r.Title, 4)) = 'the ' THEN LTRIM(RIGHT(r.Title, LEN(r.Title) - 4)) 
								ELSE r.Title
							END 
						)
				END
	FROM	import.ImportRecord r
			INNER JOIN dbo.Segment s ON r.SegmentID = s.SegmentID
			LEFT JOIN dbo.Language l1 ON r.Language = l1.LanguageCode
			LEFT JOIN dbo.Language l2 ON r.Language = l2.LanguageName
	WHERE	ImportRecordID = @ImportRecordID
	
	-- Get the production Segment and Item IDs for the updated records
	SELECT	@ItemID = s.ItemID,
			@SegmentID = s.SegmentID
	FROM	import.ImportRecord r INNER JOIN dbo.vwSegment s ON r.SegmentID = s.SegmentID
	WHERE	ImportRecordID = @ImportRecordID

	-- Update the related Book, if it has changed
	DECLARE @BookItemID int
	SELECT @BookItemID = b.ItemID FROM dbo.Book b INNER JOIN import.ImportRecord r ON b.BookID = r.ItemID WHERE r.ImportRecordID = @ImportRecordID

	UPDATE	ir
	SET		ParentID = @BookItemID,
			SequenceOrder = (SELECT ISNULL(MAX(SequenceOrder), 0) + 1 FROM dbo.ItemRelationship WHERE ParentID = @BookItemID),
			LastModifiedUserID = @UserID,
			LastModifiedDate = GETDATE()
	FROM	dbo.ItemRelationship ir
			INNER JOIN dbo.Item i ON ir.ParentID = i.ItemID AND i.ItemTypeID = @ItemTypeBookID
	WHERE	ir.ChildID = @ItemID
	AND		ir.ParentID <> @BookItemID	
	
	-- Add updated ItemInstitution records to production
	IF EXISTS(SELECT ImportRecordContributorID FROM import.ImportRecordContributor WHERE ImportRecordID = @ImportRecordID)
	BEGIN
		-- Institutions are being updated, so remove the existing records
		DELETE FROM dbo.ItemInstitution WHERE ItemID = @ItemID
	END

	INSERT	dbo.ItemInstitution (ItemID, InstitutionCode, InstitutionRoleID, CreationUserID, LastModifiedUserID)
	SELECT	@ItemID, t.InstitutionCode, @InstitutionRoleContributorID, @UserID, @UserID
	FROM	import.ImportRecordContributor t
			INNER JOIN dbo.Institution i ON t.InstitutionCode = i.InstitutionCode
	WHERE	ImportRecordID = @ImportRecordID
	AND		t.InstitutionCode <> 'NULL'
	ORDER BY ImportRecordContributorID

	-- Delete existing ItemIdentifier records and insert new ones, unless the new values are blank (unchanged)
	DELETE	ii
	FROM	import.ImportRecord r INNER JOIN dbo.vwSegment s ON r.SegmentID = s.SegmentID
			INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @IdentifierISSNID
	WHERE	r.ImportRecordID = @ImportRecordErrorID AND ISSN <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@ItemID, @IdentifierISSNID, ISSN, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND ISSN <> '' AND ISSN <> 'NULL'

	DELETE	ii
	FROM	import.ImportRecord r INNER JOIN dbo.vwSegment s ON r.SegmentID = s.SegmentID
			INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @IdentifierISBNID
	WHERE	r.ImportRecordID = @ImportRecordErrorID AND ISBN <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@ItemID, @IdentifierISBNID, ISBN, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND ISBN <> '' AND ISBN <> 'NULL'

	DELETE	ii
	FROM	import.ImportRecord r INNER JOIN dbo.vwSegment s ON r.SegmentID = s.SegmentID
			INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @IdentifierOCLCID
	WHERE	r.ImportRecordID = @ImportRecordErrorID AND OCLC <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@ItemID, @IdentifierOCLCID, OCLC, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND OCLC <> '' AND OCLC <> 'NULL'

	DELETE	ii
	FROM	import.ImportRecord r INNER JOIN dbo.vwSegment s ON r.SegmentID = s.SegmentID
			INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @IdentifierLCCNID
	WHERE	r.ImportRecordID = @ImportRecordErrorID AND LCCN <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@ItemID, @IdentifierLCCNID, LCCN, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND LCCN <> '' AND LCCN <> 'NULL'

	DELETE	ii
	FROM	import.ImportRecord r INNER JOIN dbo.vwSegment s ON r.SegmentID = s.SegmentID
			INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @IdentifierARKID
	WHERE	r.ImportRecordID = @ImportRecordErrorID AND ARK <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@ItemID, @IdentifierARKID, ARK, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND ARK <> '' AND ARK <> 'NULL'

	DELETE	ii
	FROM	import.ImportRecord r INNER JOIN dbo.vwSegment s ON r.SegmentID = s.SegmentID
			INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @IdentifierBiostorID
	WHERE	r.ImportRecordID = @ImportRecordErrorID AND Biostor <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@ItemID, @IdentifierBiostorID, Biostor, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND Biostor <> '' AND BioStor <> 'NULL'

	DELETE	ii
	FROM	import.ImportRecord r INNER JOIN dbo.vwSegment s ON r.SegmentID = s.SegmentID
			INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @IdentifierJSTORID
	WHERE	r.ImportRecordID = @ImportRecordErrorID AND JSTOR <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@ItemID, @IdentifierJSTORID, JSTOR, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND JSTOR <> '' AND JSTOR <> 'NULL'

	DELETE	ii
	FROM	import.ImportRecord r INNER JOIN dbo.vwSegment s ON r.SegmentID = s.SegmentID
			INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @IdentifierTL2ID
	WHERE	r.ImportRecordID = @ImportRecordErrorID AND TL2 <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@ItemID, @IdentifierTL2ID, TL2, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND TL2 <> '' AND TL2 <> 'NULL'

	DELETE	ii
	FROM	import.ImportRecord r INNER JOIN dbo.vwSegment s ON r.SegmentID = s.SegmentID
			INNER JOIN dbo.ItemIdentifier ii ON s.ItemID = ii.ItemID AND ii.IdentifierID = @IdentifierWikidataID
	WHERE	r.ImportRecordID = @ImportRecordErrorID AND Wikidata <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@ItemID, @IdentifierWikidataID, Wikidata, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND Wikidata <> '' AND Wikidata <> 'NULL'

	-- Delete existing DOI record and insert new one, unless the new value is blank (unchanged)
	DELETE	d
	FROM	import.ImportRecord r INNER JOIN dbo.DOI d ON r.SegmentID = d.EntityID
	WHERE	r.ImportRecordID = @ImportRecordID
	AND		d.DOIEntityTypeID = @DOIEntityTypeSegmentID
	AND		d.DOIStatusID = @DOIStatusExternalID
	AND		r.DOI <> ''

	INSERT	dbo.DOI (DOIEntityTypeID, EntityID, DOIStatusID, DOIName, StatusDate, IsValid, CreationUserID, LastModifiedUserID)
	SELECT	@DOIEntityTypeSegmentID, @SegmentID, @DOIStatusExternalID, DOI, GETDATE(), 1, @UserID, @UserID
	FROM	import.ImportRecord 
	WHERE	ImportRecordID = @ImportRecordID 
	AND		DOI <> '' AND DOI <> 'NULL'
	AND		DOI NOT LIKE '%10.5962%' -- Do not add BHL DOIs via batch upload

	-- Add updated ItemAuthor records to production					
	IF EXISTS(SELECT ImportRecordCreatorID FROM import.ImportRecordCreator WHERE ImportRecordID = @ImportRecordID)
	BEGIN
		-- Authors are being updated, so remove the existing records
		DELETE FROM dbo.ItemAuthor WHERE ItemID = @ItemID
	END

	INSERT	dbo.ItemAuthor (ItemID, AuthorID, SequenceOrder, CreationUserID, LastModifiedUserID)
	SELECT	@ItemID, ProductionAuthorID, ROW_NUMBER() OVER (ORDER BY ImportRecordCreatorID), @UserID, @UserID
	FROM	import.ImportRecordCreator
	WHERE	ImportRecordID = @ImportRecordID
	AND		FullName <> 'NULL'

	-- Add updated ItemKeyword records to production
	IF EXISTS(SELECT ImportRecordKeywordID FROM #tmpRecordKeyword)
	BEGIN
		-- Keywords are being updated, so remove the existing records
		DELETE FROM dbo.ItemKeyword WHERE ItemID = @ItemID
	END

	INSERT	dbo.ItemKeyword (ItemID, KeywordID, CreationUserID, LastModifiedUserID)
	SELECT	@ItemID, ProductionKeywordID, @UserID, @UserID 
	FROM	#tmpRecordKeyword 
	WHERE	Keyword <> 'NULL'

	-- Add updated ItemPage records to production
	IF EXISTS(SELECT ImportRecordPageID FROM import.ImportRecordPage WHERE ImportRecordID = @ImportRecordID)
	BEGIN
		-- Pages are being updated, so remove the existing records
		DELETE FROM dbo.ItemPage WHERE ItemID = @ItemID
	END

	INSERT	dbo.ItemPage (ItemID, PageID, SequenceOrder, CreationUserID, LastModifiedUserID)
	SELECT	@ItemID, PageID, SequenceOrder, @UserID, @UserID
	FROM	import.ImportRecordPage
	WHERE	ImportRecordID = @ImportRecordID

	-- Set the record to "Imported"
	UPDATE	import.ImportRecord
	SET		ImportRecordStatusID = @ImportRecordImportedID,
			LastModifiedDate = GETDATE(),
			LastModifiedUserID = @UserID
	WHERE	ImportRecordID = @ImportRecordID

	COMMIT TRAN

END TRY
BEGIN CATCH
	DECLARE @ErrMsg NVARCHAR(4000)
	DECLARE @ErrProc NVARCHAR(128)
	DECLARE @ErrLine INT
	DECLARE @ErrSeverity INT
	DECLARE @ErrState INT
	
	SELECT	@ErrMsg = ERROR_MESSAGE(),
			@ErrProc = ERROR_PROCEDURE(),
			@ErrLine = ERROR_LINE(),
			@ErrSeverity = ERROR_SEVERITY(),
			@ErrState = ERROR_STATE()

	IF @@TRANCOUNT > 0 ROLLBACK TRAN

	-- Set the record to "Error"
	UPDATE	import.ImportRecord
	SET		ImportRecordStatusID = @ImportRecordErrorID,
			LastModifiedDate = GETDATE(),
			LastModifiedUserID = @UserID
	WHERE	ImportRecordID = @ImportRecordID

	-- Log the error
	INSERT	import.ImportRecordErrorLog (ImportRecordID, ErrorDate, ErrorMessage, Severity, CreationUserID, LastModifiedUserID)
	VALUES	(@ImportRecordID, GETDATE(), @ErrProc + ' (' + CONVERT(NVARCHAR(5), @ErrLine) + '): ' + @ErrMsg, 'Error', @UserID, @UserID)
END CATCH

END

GO
