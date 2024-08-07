CREATE PROCEDURE [import].[ImportRecordPublishToProduction]

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

	-- Get Keyword IDs for existing records
	UPDATE	#tmpRecordKeyword
	SET		ProductionKeywordID = k.KeywordID
	FROM	#tmpRecordKeyword t
			INNER JOIN dbo.Keyword k ON t.Keyword = k.Keyword COLLATE SQL_Latin1_General_CP1_CI_AI

	--------------------------------------------------------------------

	-- Add new Item and Segment records to production, making note of the new IDs
	DECLARE @NewItemID int
	DECLARE @NewSegmentID int

	BEGIN TRAN

	-- Insert Segment record
	DECLARE @SegmentGenreID int
	SELECT	@SegmentGenreID = ISNULL(SegmentGenreID, @SegmentGenreArticleID)
	FROM	import.ImportRecord r LEFT JOIN dbo.SegmentGenre g ON r.Genre = g.GenreName
	WHERE	ImportRecordID = @ImportRecordID

	-- Insert a new item record
	INSERT	dbo.Item 
			(
			ItemTypeID, 
			ItemStatusID, 
			ItemSourceID, 
			Note,
			CreationUserID, 
			LastModifiedUserID
			)
	SELECT	@ItemTypeSegmentID,
			@SegmentStatusNewID,	-- New
			@ItemSourceCitationImportID,
			Notes,
			@UserID AS CreationUserID,
			@UserID AS LastModifiedUserID
	FROM	import.ImportRecord r
	WHERE	ImportRecordID = @ImportRecordID

	-- Preserve the production identifier for the new item
	SET @NewItemID = SCOPE_IDENTITY()

	-- Insert a new itemrelationship record
	INSERT	dbo.ItemRelationship
			(
			ParentID,
			ChildID,
			SequenceOrder
			)
	SELECT	b.ItemID AS ParentID,
			@NewItemID AS ChildID,
			10000 AS SequenceOrder
	FROM	import.ImportRecord r
			INNER JOIN dbo.Book b ON r.ItemID = b.BookID
	WHERE	ImportRecordID = @ImportRecordID

	-- Insert a new segment record
	INSERT	dbo.Segment
			(
			ItemID,
			SegmentGenreID,
			Title,
			TranslatedTitle,
			ContainerTitle,
			PublicationDetails,
			PublisherName,
			PublisherPlace,
			Volume,
			Series,
			Issue,
			Edition,
			Summary,
			[Date],
			PageRange,
			StartPageID,
			StartPageNumber,
			EndPageNumber,
			LanguageCode,
			Url,
			RightsStatus,
			RightsStatement,
			LicenseName,
			LicenseUrl,
			CreationUserID,
			LastModifiedUserID,
			SortTitle
			)
	SELECT	@NewItemID,
			@SegmentGenreID,
			Title,
			TranslatedTitle,
			JournalTitle AS ContainerTitle,
			PublicationDetails,
			PublisherName,
			PublisherPlace,
			Volume,
			Series,
			Issue,
			Edition,
			Summary,
			[Year] AS [Date],
			CASE
				WHEN PageRange <> '' THEN PageRange 
				WHEN StartPage <> '' AND EndPage <> '' THEN StartPage + '--' + EndPage
				WHEN StartPage <> '' THEN StartPage + '--'
				ELSE '--' + EndPage
			END AS PageRange,
			StartPageID,
			StartPage AS StartPageNumber,
			EndPage AS EndPageNumber,
			ISNULL(l1.LanguageCode, l2.LanguageCode) AS LanguageCode,
			Url,
			CopyrightStatus AS RightsStatus,
			Rights AS RightsStatement,
			License AS LicenseName,
			LicenseUrl,
			@UserID AS CreationUserID,
			@UserID AS LastModifiedUserID,
			dbo.[fnGetSortString](
				CASE
					WHEN LEFT(Title, 1) = '"' THEN LTRIM(RIGHT(Title, LEN(Title) - 1))
					WHEN LEFT(Title, 1) = '''' THEN LTRIM(RIGHT(Title, LEN(Title) - 1))
					WHEN LEFT(Title, 1) = '[' THEN LTRIM(RIGHT(Title, LEN(Title) - 1)) 
					WHEN LEFT(Title, 1) = '(' THEN LTRIM(RIGHT(Title, LEN(Title) - 1))
					WHEN LEFT(Title, 1) = '|' THEN LTRIM(RIGHT(Title, LEN(Title) - 1))
					WHEN LOWER(LEFT(Title, 2)) = 'a ' AND Title <> 'a' THEN LTRIM(RIGHT(Title, LEN(Title) - 2)) 
					WHEN LOWER(LEFT(Title, 3)) = 'an ' THEN LTRIM(RIGHT(Title, LEN(Title) - 3)) 
					WHEN LOWER(LEFT(Title, 3)) = 'el ' THEN LTRIM(RIGHT(Title, LEN(Title) - 3)) 
					WHEN LOWER(LEFT(Title, 3)) = 'il ' THEN LTRIM(RIGHT(Title, LEN(Title) - 3)) 
					WHEN LOWER(LEFT(Title, 3)) = 'la ' THEN LTRIM(RIGHT(Title, LEN(Title) - 3)) 
					WHEN LOWER(LEFT(Title, 3)) = 'le ' THEN LTRIM(RIGHT(Title, LEN(Title) - 3)) 
					WHEN LOWER(LEFT(Title, 4)) = 'das ' THEN LTRIM(RIGHT(Title, LEN(Title) - 4)) 
					WHEN LOWER(LEFT(Title, 4)) = 'der ' THEN LTRIM(RIGHT(Title, LEN(Title) - 4)) 
					WHEN LOWER(LEFT(Title, 4)) = 'die ' THEN LTRIM(RIGHT(Title, LEN(Title) - 4)) 
					WHEN LOWER(LEFT(Title, 4)) = 'ein ' THEN LTRIM(RIGHT(Title, LEN(Title) - 4)) 
					WHEN LOWER(LEFT(Title, 4)) = 'las ' THEN LTRIM(RIGHT(Title, LEN(Title) - 4)) 
					WHEN LOWER(LEFT(Title, 4)) = 'les ' THEN LTRIM(RIGHT(Title, LEN(Title) - 4)) 
					WHEN LOWER(LEFT(Title, 4)) = 'los ' THEN LTRIM(RIGHT(Title, LEN(Title) - 4)) 
					WHEN LOWER(LEFT(Title, 4)) = 'the ' THEN LTRIM(RIGHT(Title, LEN(Title) - 4)) 
					ELSE Title
				END 
			) AS SortTitle
	FROM	import.ImportRecord r
			LEFT JOIN dbo.Language l1 ON r.Language = l1.LanguageCode
			LEFT JOIN dbo.Language l2 ON r.Language = l2.LanguageName
	WHERE	ImportRecordID = @ImportRecordID

	-- Save the ID of the newly inserted segment record
	SELECT @NewSegmentID = SCOPE_IDENTITY()

	-- Save the production segment ID with the import record
	UPDATE	import.ImportRecord
	SET		SegmentID = @NewSegmentID
	WHERE	ImportRecordID = @ImportRecordID

	-- Make sure any new (unsequenced) segments attached to the same item as this new
	-- segment have unique SequenceOrder values
	UPDATE	dbo.ItemRelationship
	SET		SequenceOrder = x.SequenceOrder
	FROM	dbo.ItemRelationship irel
			INNER JOIN (SELECT	ir.RelationshipID,
								ROW_NUMBER() OVER (ORDER BY 
										RIGHT(SPACE(100) + s.Series, 100), 
										RIGHT(SPACE(100) + s.Volume, 100), 
										RIGHT(SPACE(100) + s.Issue, 100), 
										RIGHT(SPACE(20) + s.StartPageNumber, 20),
										r.ImportRecordID
									) + 9999 AS SequenceOrder
						FROM	dbo.Segment s
								INNER JOIN dbo.ItemRelationship ir ON s.ItemID = ir.ChildID
								INNER JOIN dbo.Book b ON ir.ParentID = b.ItemID
								INNER JOIN import.ImportRecord r ON b.BookID = r.ItemID
								INNER JOIN import.ImportRecord r2 ON r.ItemID = r2.ItemID AND s.SegmentID = r2.SegmentID
						WHERE	r.ImportRecordID = @ImportRecordID
						AND		ir.SequenceOrder >= 10000
						) x
					ON irel.RelationshipID = x.RelationshipID
				
	-- Insert ItemInstitution records (only include those that match records in the Institution table)
	INSERT	dbo.ItemInstitution (ItemID, InstitutionCode, InstitutionRoleID, CreationUserID, LastModifiedUserID)
	SELECT	@NewItemID, t.InstitutionCode, @InstitutionRoleContributorID, @UserID, @UserID
	FROM	import.ImportRecordContributor t
			INNER JOIN dbo.Institution i ON t.InstitutionCode = i.InstitutionCode
	WHERE	ImportRecordID = @ImportRecordID
	ORDER BY ImportRecordContributorID

	-- Insert ItemIdentifier records
	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@NewItemID, dbo.fnGetISSNID(ISSN), dbo.fnGetISSNValue(ISSN), @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND ISSN <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@NewItemID, @IdentifierISBNID, ISBN, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND ISBN <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@NewItemID, @IdentifierOCLCID, OCLC, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND OCLC <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@NewItemID, @IdentifierLCCNID, dbo.fnGetLCCNValue(LCCN), @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND LCCN <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@NewItemID, @IdentifierARKID, ARK, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND ARK <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@NewItemID, @IdentifierBiostorID, Biostor, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND Biostor <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@NewItemID, @IdentifierJSTORID, JSTOR, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND JSTOR <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@NewItemID, @IdentifierTL2ID, TL2, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND TL2 <> ''

	INSERT	dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@NewItemID, @IdentifierWikidataID, Wikidata, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND Wikidata <> ''

	-- Insert DOI record
	DECLARE @DOIName nvarchar(50)
	SELECT	@DOIName = DOI 
	FROM	import.ImportRecord 
	WHERE	ImportRecordID = @ImportRecordID AND DOI <> '' 

	exec dbo.DOIInsert @DOIEntityTypeSegmentID, @NewSegmentID, @DOIStatusExternalID, @DOIName, @IsValid = 1, @ExcludeBHLDOI = 1

	-- Add new ItemAuthor records to production
	INSERT	dbo.ItemAuthor (ItemID, AuthorID, SequenceOrder, CreationUserID, LastModifiedUserID)
	SELECT	@NewItemID, ProductionAuthorID, ROW_NUMBER() OVER (ORDER BY ImportRecordCreatorID), @UserID, @UserID
	FROM	import.ImportRecordCreator
	WHERE	ImportRecordID = @ImportRecordID

	-- Add new ItemKeyword records to production
	INSERT	dbo.ItemKeyword (ItemID, KeywordID, CreationUserID, LastModifiedUserID)
	SELECT	@NewItemID, ProductionKeywordID, @UserID, @UserID FROM #tmpRecordKeyword

	-- Add new ItemPage records to production
	INSERT	dbo.ItemPage (ItemID, PageID, SequenceOrder, CreationUserID, LastModifiedUserID)
	SELECT	@NewItemID, PageID, SequenceOrder, @UserID, @UserID
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
	INSERT	import.ImportRecordErrorLog (ImportRecordID, ErrorDate, ErrorMessage, CreationUserID, LastModifiedUserID)
	VALUES	(@ImportRecordID, GETDATE(), @ErrProc + ' (' + CONVERT(NVARCHAR(5), @ErrLine) + '): ' + @ErrMsg, @UserID, @UserID)
END CATCH

END

GO
