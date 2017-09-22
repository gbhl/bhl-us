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
SELECT @SegmentStatusNewID = SegmentStatusID FROM dbo.SegmentStatus WHERE StatusName = 'New'
IF (@SegmentStatusNewID IS NULL) RAISERROR('SegmentStatus -New- not found', 0, 1)

DECLARE @SegmentGenreArticleID int
SELECT @SegmentGenreArticleID = SegmentGenreID FROM dbo.SegmentGenre WHERE GenreName = 'Article'
IF (@SegmentGenreArticleID IS NULL) RAISERROR('SegmentGenre -Article- not found', 0, 1)

DECLARE @DOIEntityTypeSegmentID int
SELECT @DOIEntityTypeSegmentID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Segment'
IF (@DOIEntityTypeSegmentID IS NULL) RAISERROR('DOIEntityType -Segment- not found', 0, 1)

BEGIN TRY

	DECLARE @ContributorCode nvarchar(10)
	SELECT	@ContributorCode = f.ContributorCode
	FROM	import.ImportRecord r
			INNER JOIN import.ImportFile f ON r.ImportFileID = f.ImportFileID
	WHERE	r.ImportRecordID = @ImportRecordID

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

	-- Add new Segment records to production, making note of the new IDs
	DECLARE @NewSegmentID int

	BEGIN TRAN

	-- Insert Segment record
	DECLARE @SegmentGenreID int
	SELECT	@SegmentGenreID = ISNULL(SegmentGenreID, @SegmentGenreArticleID)
	FROM	import.ImportRecord r LEFT JOIN dbo.SegmentGenre g ON r.Genre = g.GenreName
	WHERE	ImportRecordID = @ImportRecordID
		
	INSERT	dbo.Segment
			(
			ItemID,
			SegmentStatusID,
			SequenceOrder,
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
			Notes,
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
	SELECT	ItemID,
			@SegmentStatusNewID,
			1 AS SequenceOrder,
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
			Notes,
			Summary,
			[Year] AS [Date],
			CASE
				WHEN PageRange <> '' THEN PageRange 
				WHEN StartPage <> '' AND EndPage <> '' THEN StartPage + '--' + EndPage
				WHEN StartPage <> '' THEN StartPage
				ELSE EndPage
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
			CASE
				WHEN LEFT(Title, 1) = '"' THEN LTRIM(RIGHT(Title, LEN(Title) - 1))
				WHEN LEFT(Title, 1) = '''' THEN LTRIM(RIGHT(Title, LEN(Title) - 1))
				WHEN LEFT(Title, 1) = '[' THEN LTRIM(RIGHT(Title, LEN(Title) - 1)) 
				WHEN LEFT(Title, 1) = '(' THEN LTRIM(RIGHT(Title, LEN(Title) - 1))
				WHEN LEFT(Title, 1) = '|' THEN LTRIM(RIGHT(Title, LEN(Title) - 1))
				WHEN LOWER(LEFT(Title, 2)) = 'a ' AND Title <> 'a' THEN LTRIM(RIGHT(Title, LEN(Title) - 2)) 
				WHEN LOWER(LEFT(Title, 3)) = 'an ' THEN LTRIM(RIGHT(Title, LEN(Title) - 3)) 
				WHEN LOWER(LEFT(Title, 3)) = 'de ' THEN LTRIM(RIGHT(Title, LEN(Title) - 3)) 
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
			END AS SortTitle
	FROM	import.ImportRecord r
			LEFT JOIN dbo.Language l1 ON r.Language = l1.LanguageCode
			LEFT JOIN dbo.Language l2 ON r.Language = l2.LanguageName
	WHERE	ImportRecordID = @ImportRecordID

	-- Save the ID of the newly inserted segment record
	SELECT @NewSegmentID = SCOPE_IDENTITY()
				
	-- Insert SegmentInstitution record
	INSERT	dbo.SegmentInstitution (SegmentID, InstitutionCode, InstitutionRoleID, CreationUserID, LastModifiedUserID)
	VALUES	(@NewSegmentID, @ContributorCode, @InstitutionRoleContributorID, @UserID, @UserID)

	-- Insert SegmentIdentifier records
	INSERT	dbo.SegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@NewSegmentID, @IdentifierISSNID, ISSN, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND ISSN <> ''

	INSERT	dbo.SegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@NewSegmentID, @IdentifierISBNID, ISBN, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND ISBN <> ''

	INSERT	dbo.SegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@NewSegmentID, @IdentifierOCLCID, OCLC, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND OCLC <> ''

	INSERT	dbo.SegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
	SELECT	@NewSegmentID, @IdentifierLCCNID, LCCN, @UserID, @UserID
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND LCCN <> ''

	-- Insert DOI record
	INSERT	dbo.DOI (DOIEntityTypeID, EntityID, DOIStatusID, DOIName, StatusDate, IsValid)
	SELECT	@DOIEntityTypeSegmentID, @NewSegmentID, @DOIStatusExternalID, DOI, GETDATE(), 1
	FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND DOI <> ''

	-- Add new SegmentAuthor records to production
	INSERT	dbo.SegmentAuthor (SegmentID, AuthorID, SequenceOrder, CreationUserID, LastModifiedUserID)
	SELECT	@NewSegmentID, AuthorID, ROW_NUMBER() OVER (ORDER BY ImportRecordCreatorID), @UserID, @UserID
	FROM	import.ImportRecordCreator
	WHERE	ImportRecordID = @ImportRecordID

	-- Add new SegmentKeyword records to production
	INSERT	dbo.SegmentKeyword (SegmentID, KeywordID, CreationUserID, LastModifiedUserID)
	SELECT	@NewSegmentID, ProductionKeywordID, @UserID, @UserID FROM #tmpRecordKeyword

	-- Add new SegmentPage records to production
	INSERT	dbo.SegmentPage (SegmentID, PageID, SequenceOrder, CreationUserID, LastModifiedUserID)
	SELECT	@NewSegmentID, PageID, SequenceOrder, @UserID, @UserID
	FROM	import.ImportRecordPage
	WHERE	ImportRecordID = @ImportRecordID

	-- Set the record to "Imported"
	UPDATE	import.ImportRecord
	SET		SegmentID = @NewSegmentID,
			ImportRecordStatusID = @ImportRecordImportedID,
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
