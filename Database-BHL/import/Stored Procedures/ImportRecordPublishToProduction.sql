CREATE PROCEDURE [import].[ImportRecordPublishToProduction]

@ImportRecordID int,
@UserID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @IsSegment tinyint

-- Determine if we will be adding a Title/Item or a Segment
SELECT	@IsSegment = CASE WHEN Genre NOT IN ('Book', 'BookJournal', 'Journal', 'Monograph', 'Serial') THEN 1 ELSE 0 END
FROM	import.ImportRecord
WHERE	ImportRecordID = @ImportRecordID

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

DECLARE @AuthorRoleNotSpecifiedID int
DECLARE @SegmentStatusNewID int
DECLARE @SegmentGenreArticleID int
DECLARE @DOIEntityTypeSegmentID int
DECLARE @DOIEntityTypeTitleID int
DECLARE @TitleVariantTypeTranslatedID int
DECLARE @ItemStatusPublishedID int
DECLARE @ItemSourceCitationImportID int
DECLARE @BibliographicLevelMonographID int
DECLARE @BibliographicLevelSerialID int

IF (@IsSegment = 1)
BEGIN
	-- These lookup values only apply to segments
	SELECT @SegmentStatusNewID = SegmentStatusID FROM dbo.SegmentStatus WHERE StatusName = 'New'
	IF (@SegmentStatusNewID IS NULL) RAISERROR('SegmentStatus -New- not found', 0, 1)

	SELECT @SegmentGenreArticleID = SegmentGenreID FROM dbo.SegmentGenre WHERE GenreName = 'Article'
	IF (@SegmentGenreArticleID IS NULL) RAISERROR('SegmentGenre -Article- not found', 0, 1)

	SELECT @DOIEntityTypeSegmentID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Segment'
	IF (@DOIEntityTypeSegmentID IS NULL) RAISERROR('DOIEntityType -Segment- not found', 0, 1)
END
ELSE
BEGIN
	-- These lookup values only apply to title/items
	SELECT @AuthorRoleNotSpecifiedID = AuthorRoleID FROM dbo.AuthorRole WHERE RoleDescription = 'Not Specified'
	IF (@AuthorRoleNotSpecifiedID IS NULL) RAISERROR('AuthorRole -Not Specified- not found', 0, 1)

	SELECT @DOIEntityTypeTitleID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Title'
	IF (@DOIEntityTypeTitleID IS NULL) RAISERROR('DOIEntityType -Title- not found', 0, 1)

	SELECT @TitleVariantTypeTranslatedID = TitleVariantTypeID FROM dbo.TitleVariantType WHERE TitleVariantTypeName = 'Translated'
	IF (@TitleVariantTypeTranslatedID IS NULL) RAISERROR('TitleVariantType -Translated- not found', 0, 1)

	SELECT @ItemStatusPublishedID = ItemStatusID FROM dbo.ItemStatus WHERE ItemStatusName = 'Published'
	IF (@ItemStatusPublishedID IS NULL) RAISERROR('ItemStatus -Published- not found', 0, 1)

	SELECT @ItemSourceCitationImportID = ItemSourceID FROM dbo.ItemSource WHERE SourceName = 'Citation Import'
	IF (@ItemSourceCitationImportID IS NULL) RAISERROR('ItemSource -Citation Import- not found', 0, 1)

	SELECT @BibliographicLevelMonographID = BibliographicLevelID FROM dbo.BibliographicLevel WHERE BibliographicLevelName = 'Monograph/Item'
	IF (@BibliographicLevelMonographID IS NULL) RAISERROR('BibliographicLevel -Monograph/Item- not found', 0, 1)

	SELECT @BibliographicLevelSerialID = BibliographicLevelID FROM dbo.BibliographicLevel WHERE BibliographicLevelName = 'Serial'
	IF (@BibliographicLevelSerialID IS NULL) RAISERROR('BibliographicLevel -Serial- not found', 0, 1)
END

BEGIN TRY

	DECLARE @ContributorCode nvarchar(10)
	SELECT	@ContributorCode = f.ContributorCode
	FROM	import.ImportRecord r
			INNER JOIN import.ImportFile f ON r.ImportFileID = f.ImportFileID
	WHERE	r.ImportRecordID = @ImportRecordID

	--------------------------------------------------------------------

	-- Build temp tables
	CREATE TABLE #tmpRecordCreator
		(
		ImportRecordCreatorID int NOT NULL,
		ImportRecordID int NOT NULL,
		FullName nvarchar(300) NOT NULL,
		FirstName nvarchar(150) NOT NULL,
		LastName nvarchar(150) NOT NULL,
		StartYear nvarchar(25) NOT NULL,
		EndYear nvarchar(25) NOT NULL,
		AuthorType nvarchar(50) NOT NULL,
		ProductionAuthorID int NULL
		)

	CREATE TABLE #tmpRecordKeyword
		(
		ImportRecordKeywordID int NOT NULL,
		ImportRecordID int NOT NULL,
		Keyword nvarchar(50) NOT NULL,
		ProductionKeywordID int NULL
		)

	--------------------------------------------------------------------

	-- Get the creators to be imported
	INSERT	#tmpRecordCreator
	SELECT	ImportRecordCreatorID,
			ImportRecordID,
			FullName,
			FirstName,
			LastName,
			StartYear,
			EndYear,
			AuthorType,
			NULL
	FROM	import.ImportRecordCreator
	WHERE	ImportRecordID = @ImportRecordID

	-- Add the new creators to production (making note of the new AuthorIDs)
	DECLARE @NewAuthorID int
	DECLARE @ImportRecordCreatorID int
	DECLARE @FullName nvarchar(300)
	DECLARE @FirstName nvarchar(150)
	DECLARE @LastName nvarchar(150)
	DECLARE @StartYear nvarchar(25)
	DECLARE @EndYear nvarchar(25)
	DECLARE @AuthorType nvarchar(50)

	DECLARE	curInsert CURSOR 
	FOR SELECT ImportRecordCreatorID, FullName, FirstName, LastName, StartYear, EndYear, AuthorType FROM #tmpRecordCreator
		
	OPEN curInsert
	FETCH NEXT FROM curInsert INTO @ImportRecordCreatorID, @FullName, @FirstName, @LastName, @StartYear, @EndYear, @AuthorType

	WHILE (@@fetch_status <> -1)
	BEGIN
		IF (@@fetch_status <> -2)
		BEGIN

			IF NOT EXISTS(	SELECT a.AuthorID FROM dbo.Author a INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
							WHERE (n.FullName = @FullName OR dbo.fnReverseAuthorName(@FullName) = n.FullName OR
								(n.FirstName = @FirstName AND n.LastName = @LastName AND @FirstName <> '' AND @LastName <> ''))
							AND a.StartDate = @StartYear AND a.EndDate = @EndYear)
			BEGIN
				BEGIN TRAN

				-- Insert a new Author record into the production database
				INSERT INTO dbo.Author (AuthorTypeID, StartDate, EndDate, IsActive)
				VALUES (CASE WHEN @AuthorType = 'corporate' THEN 2 ELSE 1 END, @StartYear, @EndYear, 1)
						
				-- Save the ID of the newly inserted author record
				SELECT @NewAuthorID = SCOPE_IDENTITY()
				
				-- Insert a new AuthorName record
				INSERT INTO dbo.AuthorName (AuthorID, FullName, FirstName, LastName, IsPreferredName) 
				VALUES (@NewAuthorID, @FullName, @FirstName, @LastName, 1)

				-- Preserve the production identifier
				UPDATE	#tmpRecordCreator
				SET		ProductionAuthorID = @NewAuthorID
				WHERE	ImportRecordCreatorID = @ImportRecordCreatorID

				COMMIT TRAN
			END
		END
		FETCH NEXT FROM curInsert INTO @ImportRecordCreatorID, @FullName, @FirstName, @LastName, @StartYear, @EndYear, @AuthorType
	END

	CLOSE curInsert
	DEALLOCATE curInsert

	-- Get Author IDs for existing records
	UPDATE	#tmpRecordCreator
	SET		ProductionAuthorID = a.AuthorID
	FROM	#tmpRecordCreator t
			INNER JOIN dbo.Author a ON t.StartYear = a.StartDate AND t.EndYear = a.EndDate
			INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
	WHERE	n.FullName = t.FullName
	AND		t.ProductionAuthorID IS NULL

	UPDATE	#tmpRecordCreator
	SET		ProductionAuthorID = a.AuthorID
	FROM	#tmpRecordCreator t
			INNER JOIN dbo.Author a ON t.StartYear = a.StartDate AND t.EndYear = a.EndDate
			INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
	WHERE	dbo.fnReverseAuthorName(n.FullName) = t.FullName
	AND		t.ProductionAuthorID IS NULL

	UPDATE	#tmpRecordCreator
	SET		ProductionAuthorID = a.AuthorID
	FROM	#tmpRecordCreator t
			INNER JOIN dbo.Author a ON t.StartYear = a.StartDate AND t.EndYear = a.EndDate
			INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
	WHERE	n.FirstName = t.FirstName 
	AND		n.LastName = t.LastName
	AND		t.ProductionAuthorID IS NULL

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

	-- Add new Title/Item or Segment records to production, making note of the new IDs
	DECLARE @NewEntityID int

	BEGIN TRAN

	IF (@IsSegment = 1)
	BEGIN
		-- Insert Segment record
		DECLARE @SegmentGenreID int
		SELECT	@SegmentGenreID = ISNULL(SegmentGenreID, @SegmentGenreArticleID)
		FROM	import.ImportRecord r LEFT JOIN dbo.SegmentGenre g ON r.Genre = g.GenreName
		WHERE	ImportRecordID = @ImportRecordID
		
		INSERT	dbo.Segment
				(
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
		SELECT	@SegmentStatusNewID,
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
				CASE WHEN PageRange <> '' THEN PageRange 
					ELSE CASE WHEN StartPage <> '' THEN StartPage + '-' + EndPage ELSE EndPage END 
					END AS PageRange,
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
		SELECT @NewEntityID = SCOPE_IDENTITY()
				
		-- Insert SegmentInstitution record
		INSERT	dbo.SegmentInstitution (SegmentID, InstitutionCode, InstitutionRoleID, CreationUserID, LastModifiedUserID)
		VALUES	(@NewEntityID, @ContributorCode, @InstitutionRoleContributorID, @UserID, @UserID)

		-- Insert SegmentIdentifier records
		INSERT	dbo.SegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
		SELECT	@NewEntityID, @IdentifierISSNID, ISSN, @UserID, @UserID
		FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND ISSN <> ''

		INSERT	dbo.SegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
		SELECT	@NewEntityID, @IdentifierISBNID, ISBN, @UserID, @UserID
		FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND ISBN <> ''

		INSERT	dbo.SegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
		SELECT	@NewEntityID, @IdentifierOCLCID, OCLC, @UserID, @UserID
		FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND OCLC <> ''

		INSERT	dbo.SegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
		SELECT	@NewEntityID, @IdentifierLCCNID, LCCN, @UserID, @UserID
		FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND LCCN <> ''

		-- Insert DOI record
		INSERT	dbo.DOI (DOIEntityTypeID, EntityID, DOIStatusID, DOIName, StatusDate, IsValid)
		SELECT	@DOIEntityTypeSegmentID, @NewEntityID, @DOIStatusExternalID, DOI, GETDATE(), 1
		FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND DOI <> ''

	END
	ELSE
	BEGIN
		-- Insert Title/Item records
		DECLARE @NewItemID int

		-- =======================================================================
		-- Resolve title.  
		--
		-- Multiple attempts are made to find a matching title in production.  In
		-- order, the following criteria are used to find a match:
		--
		--	1) DOI
		--	2) Issn
		--	3) Isbn
		--  4) Lccn
		--  5) Oclc
		--
		-- After titles have been resolved, if a ProductionTitleID has not been found
		-- then a new title record will be inserted into the production database.

		-- Match on DOI
		SELECT	@NewEntityID = t.TitleID
		FROM	import.ImportRecord r
				INNER JOIN dbo.DOI d ON r.DOI = d.DOIName AND d.DOIEntityTypeID = @DOIEntityTypeTitleID
				INNER JOIN dbo.Title t ON d.EntityID = t.TitleID
		WHERE	r.ImportRecordID = @ImportRecordID

		-- Match on ISSN
		IF @NewEntityID IS NULL
		BEGIN
			SELECT	@NewEntityID = t.TitleID
			FROM	import.ImportRecord r
					INNER JOIN dbo.Title_Identifier ti ON r.ISSN = ti.IdentifierValue AND ti.IdentifierID = @IdentifierISSNID
					INNER JOIN dbo.Title t ON ti.TitleID = t.TitleID
			WHERE	r.ImportRecordID = @ImportRecordID
		END

		-- Match on ISBN
		IF @NewEntityID IS NULL
		BEGIN
			SELECT	@NewEntityID = t.TitleID
			FROM	import.ImportRecord r
					INNER JOIN dbo.Title_Identifier ti ON r.ISBN = ti.IdentifierValue AND ti.IdentifierID = @IdentifierISBNID
					INNER JOIN dbo.Title t ON ti.TitleID = t.TitleID
			WHERE	r.ImportRecordID = @ImportRecordID
		END

		-- Match on LCCN
		IF @NewEntityID IS NULL
		BEGIN
			SELECT	@NewEntityID = t.TitleID
			FROM	import.ImportRecord r
					INNER JOIN dbo.Title_Identifier ti ON r.LCCN = ti.IdentifierValue AND ti.IdentifierID = @IdentifierLCCNID
					INNER JOIN dbo.Title t ON ti.TitleID = t.TitleID
			WHERE	r.ImportRecordID = @ImportRecordID
		END

		-- Match on OCLC
		IF @NewEntityID IS NULL
		BEGIN
			SELECT	@NewEntityID = t.TitleID
			FROM	import.ImportRecord r
					INNER JOIN dbo.Title_Identifier ti ON r.OCLC = ti.IdentifierValue AND ti.IdentifierID = @IdentifierOCLCID
					INNER JOIN dbo.Title t ON ti.TitleID = t.TitleID
			WHERE	r.ImportRecordID = @ImportRecordID
		END

		-- If the selected production title has been redirected to a different 
		-- title, then use that title instead.  Follow the "redirect" chain up 
		-- to ten levels.
		SELECT	@NewEntityID = COALESCE(t10.TitleID, t9.TitleID, t8.TitleiD, t7.TitleID, t6.TitleID,
											t5.TitleID, t4.TitleID, t3.TitleID, t2.TitleID, t1.TitleID)
		FROM	dbo.Title t1
				LEFT JOIN dbo.Title t2 ON t1.RedirectTitleID = t2.TitleID
				LEFT JOIN dbo.Title t3 ON t2.RedirectTitleID = t3.TitleID
				LEFT JOIN dbo.Title t4 ON t3.RedirectTitleID = t4.TitleID
				LEFT JOIN dbo.Title t5 ON t4.RedirectTitleID = t5.TitleID
				LEFT JOIN dbo.Title t6 ON t5.RedirectTitleID = t6.TitleID
				LEFT JOIN dbo.Title t7 ON t6.RedirectTitleID = t7.TitleID
				LEFT JOIN dbo.Title t8 ON t7.RedirectTitleID = t8.TitleID
				LEFT JOIN dbo.Title t9 ON t8.RedirectTitleID = t9.TitleID
				LEFT JOIN dbo.Title t10 ON t9.RedirectTitleID = t10.TitleID
		WHERE	t1.TitleID = @NewEntityID

		IF (@NewEntityID IS NULL)
		BEGIN
			-- Insert Title record
			INSERT	dbo.Title
					(
					MarcBibID,
					FullTitle,
					ShortTitle,
					SortTitle,
					PublicationDetails,
					StartYear,
					EndYear,
					Datafield_260_a,
					Datafield_260_b,
					Datafield_260_c,
					EditionStatement,
					LanguageCode,
					PublishReady,
					RareBooks,
					BibliographicLevelID,
					CreationUserID,
					LastModifiedUserID
					)
			SELECT	@ContributorCode + '-CiteImport-' + CONVERT(nvarchar(10), @ImportRecordID) AS MarcBibID,
					Title AS FullTitle,
					LEFT(Title, 255) AS ShortTitle,
					LEFT(CASE
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
						END, 60) AS SortTitle,
					PublicationDetails,
					StartYear,
					EndYear,
					PublisherPlace AS Datafield_260_a,
					PublisherName AS Datafield_260_b,
					[Year] AS Datafield_260_c,
					Edition,
					ISNULL(l1.LanguageCode, l2.LanguageCode) AS LanguageCode,
					1 AS PublishReady,
					0 AS RareBooks,
					CASE WHEN Genre IN ('Serial', 'Journal') 
						THEN @BibliographicLevelSerialID 
						ELSE @BibliographicLevelMonographID 
						END AS BibliographicLevelID,
					@UserID AS CreationUserID,
					@UserID AS LastModifiedUserID
			FROM	import.ImportRecord r
					LEFT JOIN dbo.Language l1 ON r.Language = l1.LanguageCode
					LEFT JOIN dbo.Language l2 ON r.Language = l2.LanguageName
			WHERE	ImportRecordID = @ImportRecordID

			-- Save the ID of the newly inserted Title record
			SELECT @NewEntityID = SCOPE_IDENTITY()

			-- Insert Title_Identifier records
			INSERT	dbo.Title_Identifier (TitleID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
			SELECT	@NewEntityID, @IdentifierISSNID, ISSN, @UserID, @UserID
			FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND ISSN <> ''

			INSERT	dbo.Title_Identifier (TitleID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
			SELECT	@NewEntityID, @IdentifierISBNID, ISBN, @UserID, @UserID
			FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND ISBN <> ''

			INSERT	dbo.Title_Identifier (TitleID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
			SELECT	@NewEntityID, @IdentifierOCLCID, OCLC, @UserID, @UserID
			FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND OCLC <> ''

			INSERT	dbo.Title_Identifier (TitleID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
			SELECT	@NewEntityID, @IdentifierLCCNID, LCCN, @UserID, @UserID
			FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND LCCN <> ''

			-- Insert DOI record
			INSERT	dbo.DOI (DOIEntityTypeID, EntityID, DOIStatusID, DOIName, StatusDate, IsValid)
			SELECT	@DOIEntityTypeTitleID, @NewEntityID, @DOIStatusExternalID, DOI, GETDATE(), 1
			FROM	import.ImportRecord WHERE ImportRecordID = @ImportRecordID AND DOI <> ''

			-- Insert TitleVariant (TranslatedTitle) records
			INSERT dbo.TitleVariant (TitleID, TitleVariantTypeID, Title, CreationUserID, LastModifiedUserID)
			SELECT	@NewEntityID, @TitleVariantTypeTranslatedID, TranslatedTitle, @UserID, @UserID
			FROM	import.ImportRecord
			WHERE	ImportRecordID = @ImportRecordID
			AND		TranslatedTitle <> ''
		END

		-- Insert Item record
		INSERT	dbo.Item
				(
				PrimaryTitleID,
				BarCode,
				Volume,
				LanguageCode,
				ItemStatusID,
				ItemSourceID,
				[Year],
				StartSeries,
				StartIssue,
				LicenseUrl,
				Rights,
				DueDiligence,
				CopyrightStatus,
				ExternalUrl,
				CreationUserID,
				LastModifiedUserID
				)
		SELECT	@NewEntityID,
				@ContributorCode + '-CiteImport-' + CONVERT(nvarchar(10), @ImportRecordID) AS BarCode,
				Volume,
				ISNULL(l1.LanguageCode, l2.LanguageCode) AS LanguageCode,
				@ItemStatusPublishedID,
				@ItemSourceCitationImportID,
				[Year],
				Series,
				Issue,
				LicenseUrl,
				Rights,
				DueDiligence,
				CopyrightStatus,
				Url,
				@UserID,
				@UserID
		FROM	import.ImportRecord r
				LEFT JOIN dbo.Language l1 ON r.Language = l1.LanguageCode
				LEFT JOIN dbo.Language l2 ON r.Language = l2.LanguageName
		WHERE	ImportRecordID = @ImportRecordID

		-- Save the ID of the newly inserted Item record
		SELECT @NewItemID = SCOPE_IDENTITY()

		-- Insert ItemInstitution record
		INSERT dbo.ItemInstitution (ItemID, InstitutionCode, InstitutionRoleID, CreationUserID, LastModifiedUserID)
		VALUES (@NewItemID, @ContributorCode, @InstitutionRoleContributorID, @UserID, @UserID)

		-- Insert TitleItem record
		INSERT dbo.TitleItem (TitleID, ItemID, ItemSequence, CreationUserID, LastModifiedUserID)
		VALUES (@NewEntityID, @NewItemID, 1, @UserID, @UserID)
	END

	-- Add new TitleAuthor/SegmentAuthor records to production
	IF (@IsSegment = 1)
	BEGIN
		INSERT	dbo.SegmentAuthor (SegmentID, AuthorID, SequenceOrder, CreationUserID, LastModifiedUserID)
		SELECT	@NewEntityID, ProductionAuthorID, ROW_NUMBER() OVER (ORDER BY ImportRecordCreatorID), @UserID, @UserID
		FROM	#tmpRecordCreator
	END
	ELSE
	BEGIN
		INSERT	dbo.TitleAuthor (TitleID, AuthorID, AuthorRoleID, CreationUserID, LastModifiedUserID)
		SELECT	@NewEntityID, ProductionAuthorID, @AuthorRoleNotSpecifiedID, @UserID, @UserID
		FROM	#tmpRecordCreator		
	END

	-- Add new TitleKeyword/SegmentKeyword records to production
	IF (@IsSegment = 1)
	BEGIN
		INSERT	dbo.SegmentKeyword (SegmentID, KeywordID, CreationUserID, LastModifiedUserID)
		SELECT	@NewEntityID, ProductionKeywordID, @UserID, @UserID FROM #tmpRecordKeyword
	END
	ELSE
	BEGIN
		INSERT	dbo.TitleKeyword (TitleID, KeywordID, CreationUserID, LastModifiedUserID)
		SELECT	@NewEntityID, ProductionKeywordID, @UserID, @UserID FROM #tmpRecordKeyword
	END

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

	--RAISERROR (@ErrMsg, @ErrSeverity, @ErrState)
END CATCH

END
