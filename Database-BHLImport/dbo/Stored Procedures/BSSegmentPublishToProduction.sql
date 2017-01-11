CREATE PROCEDURE [dbo].[BSSegmentPublishToProduction]

@ItemID int,
@SegmentID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @ErrorMessage NVARCHAR(4000);
DECLARE @ErrorSeverity INT;
DECLARE @ErrorState INT;

-- ******************************************************************************

-- Get all of the lookup values
DECLARE @UserID int
SET @UserID = 1	-- system user

DECLARE @BSIdentifierID int
SELECT @BSIdentifierID = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'BioStor'

DECLARE @VIAFIdentifierID int
SELECT @VIAFIdentifierID = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'VIAF'

DECLARE @ISSNIdentifierID int
SELECT @ISSNIdentifierID = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'ISSN'

DECLARE @OCLCIdentifierID int
SELECT @OCLCIdentifierID = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'OCLC'

DECLARE @JSTORIdentifierID int
SELECT @JSTORIdentifierID = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'JSTOR'

DECLARE @DOIEntityTypeID int
SELECT @DOIEntityTypeID = DOIEntityTypeID FROM dbo.BHLDOIEntityType WHERE DOIEntityTypeName = 'Segment'

DECLARE @DOIStatusID int
SELECT @DOIStatusID = DOIStatusID FROM dbo.BHLDOIStatus WHERE DOIStatusName = 'External DOI'

DECLARE @SegmentGenreID int
SELECT	@SegmentGenreID = SegmentGenreID
FROM	dbo.BHLSegmentGenre g INNER JOIN dbo.BSSegment s ON g.GenreName = s.Genre
WHERE	s.SegmentID = @SegmentID

DECLARE @ContributorRoleID int
SELECT	@ContributorRoleID = InstitutionRoleID FROM dbo.BHLInstitutionRole WHERE InstitutionRoleName = 'Contributor'

-- ******************************************************************************

-- Get the production ID for this segment, if one exists
DECLARE @BHLSegmentID int

SELECT	@BHLSegmentID = seg.SegmentID 
FROM	dbo.BHLSegmentIdentifier seg INNER JOIN dbo.BSSegment s 
			ON seg.IdentifierValue = s.BioStorReferenceID
			AND seg.IdentifierID = @BSIdentifierID
WHERE	s.SegmentID = @SegmentID


BEGIN TRY
	BEGIN TRAN

	IF (@BHLSegmentID IS NULL)
	BEGIN
		-- ******************************************************************************
		
		-- Segment not in production, so insert it.

		-- Insert a new BHL Segment record
		INSERT	dbo.BHLSegment (ItemID, SegmentStatusID, SequenceOrder, SegmentGenreID, 
			Title, ContainerTitle, PublisherName, PublisherPlace, Volume, Series, Issue, 
			[Date], StartPageNumber, EndPageNumber, StartPageID, ContributorCreationDate,  
			ContributorLastModifiedDate, SortTitle)
		SELECT	@ItemID, 10, SequenceOrder, @SegmentGenreID, Title,
				ContainerTitle, PublisherName, PublisherPlace, Volume, Series, Issue, 
				CASE WHEN ISDATE([Date]) = 1 THEN [Date] ELSE Year END,
				StartPageNumber, EndPageNumber, StartPageID, ContributorCreationDate,
				ContributorLastModifiedDate,
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
		FROM	dbo.BSSegment
		WHERE	SegmentID = @SegmentID
		
		SET @BHLSegmentID = SCOPE_IDENTITY()

		-- Insert new BHL SegmentInstitution records
		-- Every segment harvested by this procedure has a BSTOR contributor
		INSERT	dbo.BHLSegmentInstitution (SegmentID, InstitutionCode, InstitutionRoleID,
			CreationUserID, LastModifiedUserID)
		SELECT	@BHLSegmentID, 'BSTOR', @ContributorRoleID, @UserID, @UserID
		FROM	dbo.BSSegment
		WHERE	SegmentID = @SegmentID

		INSERT	dbo.BHLSegmentInstitution (SegmentID, InstitutionCode, InstitutionRoleID,
			CreationUserID, LastModifiedUserID)
		SELECT	@BHLSegmentID, i.InstitutionCode, @ContributorRoleID, @UserID, @UserID
		FROM	dbo.BSSegment s
				INNER JOIN dbo.BHLInstitution i 
					ON s.ContributorName = i.InstitutionName COLLATE Latin1_general_CI_AI -- ignore diacritics for this comparison
		WHERE	SegmentID = @SegmentID

		-- Insert new BHL SegmentIdentifier record for BioStor ID
		INSERT	dbo.BHLSegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, 
			IsContainerIdentifier, CreationUserID, LastModifiedUserID)
		SELECT	@BHLSegmentID, @BSIdentifierID, BioStorReferenceID, 0, @UserID, @UserID
		FROM	dbo.BSSegment
		WHERE	SegmentID = @SegmentID

		-- Insert new BHL SegmentIdentifier record for ISSN
		INSERT	dbo.BHLSegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, 
			IsContainerIdentifier, CreationUserID, LastModifiedUserID)
		SELECT	@BHLSegmentID, @ISSNIdentifierID, ISSN, 1, @UserID, @UserID
		FROM	dbo.BSSegment
		WHERE	SegmentID = @SegmentID
		AND		ISSN <> ''	

		-- Insert new BHL SegmentIdentifier record for OCLC
		INSERT	dbo.BHLSegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, 
			IsContainerIdentifier, CreationUserID, LastModifiedUserID)
		SELECT	@BHLSegmentID, @OCLCIdentifierID, OCLC, 1, @UserID, @UserID
		FROM	dbo.BSSegment
		WHERE	SegmentID = @SegmentID
		AND		OCLC <> ''	

		-- Insert new BHL SegmentIdentifier record for JSTOR
		INSERT	dbo.BHLSegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, 
			IsContainerIdentifier, CreationUserID, LastModifiedUserID)
		SELECT	@BHLSegmentID, @JSTORIdentifierID, JSTOR, 0, @UserID, @UserID
		FROM	dbo.BSSegment
		WHERE	SegmentID = @SegmentID
		AND		JSTOR <> ''	

		-- Insert new BHL DOI record
		INSERT	dbo.BHLDOI (DOIEntityTypeID, EntityID, DOIStatusID, DOIName, StatusDate, IsValid)
		SELECT	@DOIEntityTypeID, @BHLSegmentID, @DOIStatusID, DOI, GETDATE(), 1
		FROM	dbo.BSSegment
		WHERE	SegmentID = @SegmentID
		AND		DOI <> ''	
		
		-- Insert BHL SegmentPage records
		INSERT	dbo.BHLSegmentPage (SegmentID, PageID, SequenceOrder, CreationUserID, LastModifiedUserID)
		SELECT	@BHLSegmentID, sp.BHLPageID, sp.SequenceOrder, @UserID, @UserID
		FROM	dbo.BSSegmentPage sp INNER JOIN dbo.BHLPage p ON sp.BHLPageID = p.PageID
		WHERE	sp.SegmentID = @SegmentID
	END
	ELSE
	BEGIN
		-- ******************************************************************************
		
		-- Segment already in production, so do an update of the Segment sequence, the Page 
		-- data, and the Author data. Don't worry about updating any other data, per a 
		-- conversation with Chris Freeland on July 13, 2012.

		DECLARE @SegmentSequence smallint
		DECLARE @StartPageID int
		
		SELECT	@SegmentSequence = SequenceOrder,
				@StartPageID = StartPageID
		FROM	dbo.BSSegment
		WHERE	SegmentID = @SegmentID

		UPDATE	dbo.BHLSegment 
		SET		ItemID = @ItemID,
				SequenceOrder = @SegmentSequence,
				StartPageID = @StartPageID,
				LastModifiedDate = GETDATE(),
				LastModifiedUserID = 1
		WHERE	SegmentID = @BHLSegmentID
		AND		(ItemID <> @ItemID OR
				SequenceOrder <> @SegmentSequence OR
				StartPageID <> @StartPageID)
		
		-- Replace the SegmentPage records
		DELETE FROM dbo.BHLSegmentPage WHERE SegmentID = @BHLSegmentID
		
		INSERT	dbo.BHLSegmentPage (SegmentID, PageID, SequenceOrder, CreationUserID, LastModifiedUserID)
		SELECT	@BHLSegmentID, sp.BHLPageID, sp.SequenceOrder, @UserID, @UserID
		FROM	dbo.BSSegmentPage sp INNER JOIN dbo.BHLPage p ON sp.BHLPageID = p.PageID
		WHERE	sp.SegmentID = @SegmentID
	END

	-- Add the BHL Segment ID to the BSSegment record
	UPDATE dbo.BSSegment SET BHLSegmentID = @BHLSegmentID WHERE SegmentID = @SegmentID

	-- ******************************************************************************

	-- Add the page metadata from the segment to the production Page records
	UPDATE	dbo.BHLPage
	SET		Volume = CASE WHEN s.Volume <> '' AND ISNULL(p.Volume, '') = '' THEN s.Volume ELSE p.Volume END,
			Series = CASE WHEN s.Series <> '' AND ISNULL(p.Series, '') = '' THEN s.Series ELSE p.Series END,
			Issue = CASE WHEN s.Issue <> '' AND ISNULL(p.Issue, '') = '' THEN s.Issue ELSE p.Issue END,
			[Year] = CASE WHEN s.[Year] <> '' AND ISNULL(p.[Year], '') = '' THEN s.[Year] ELSE p.[Year] END,
			LastModifiedDate = GETDATE(),
			LastModifiedUserID = 1
	FROM	dbo.BSSegment s 
			INNER JOIN dbo.BSSegmentPage sp ON s.SegmentID = sp.SegmentID
			INNER JOIN dbo.BHLPage p ON sp.BHLPageID = p.PageID
	WHERE	s.SegmentID = @SegmentID
	AND		(s.Volume <> '' OR s.Series <> '' OR s.Issue <> '' OR s.[Year] <> '')

	-- ******************************************************************************

	-- Add BHL AuthorName records to existing BHL Authors
	INSERT	dbo.BHLAuthorName (AuthorID, FullName, LastName, FirstName, 
				IsPreferredName, CreationUserID, LastModifiedUserID)
	SELECT	BHLAuthorID, 
			LastName + CASE WHEN LastName <> '' AND FirstName <> '' THEN ', ' ELSE '' END + FirstName, 
			LastName, FirstName, 0, @UserID, @UserID
	FROM	dbo.SegmentAuthor sa 
			INNER JOIN dbo.ImportSource src ON sa.ImportSourceID = src.ImportSourceID AND src.Source = 'BioStor'
	WHERE	sa.SegmentID = @SegmentID
	AND		sa.BHLAuthorID IS NOT NULL
	AND		NOT EXISTS (SELECT	AuthorNameID 
						FROM	dbo.BHLAuthorName 
						WHERE	AuthorID = sa.BHLAuthorID
						AND		LastName = sa.LastName
						AND		FirstName = sa.FirstName
						)

	-- Update the SequenceOrder of existing BHL SegmentAuthor records (if necessary)
	UPDATE	dbo.BHLSegmentAuthor
	SET		SequenceOrder = sa.SequenceOrder,
			LastModifiedDate = GETDATE(),
			LastModifiedUserID = 1
	FROM	dbo.SegmentAuthor sa INNER JOIN dbo.ImportSource src
				ON sa.ImportSourceID = src.ImportSourceID AND src.Source = 'BioStor'
			INNER JOIN dbo.BHLSegmentAuthor bsa
				ON bsa.SegmentID = @BHLSegmentID
				AND bsa.AuthorID = sa.BHLAuthorID
	WHERE	sa.SegmentID = @SegmentID
	AND		sa.BHLAuthorID IS NOT NULL
	AND		bsa.SequenceOrder <> sa.SequenceOrder

	-- Add BHL SegmentAuthor records for any authors with existing records (if the 
	-- SegmentAuthor records don't already exist)
	INSERT dbo.BHLSegmentAuthor(SegmentID, AuthorID, SequenceOrder, CreationUserID, LastModifiedUserID)
	SELECT	@BHLSegmentID, sa.BHLAuthorID, sa.SequenceOrder, @UserID, @UserID
	FROM	dbo.SegmentAuthor sa INNER JOIN dbo.ImportSource src 
				ON sa.ImportSourceID = src.ImportSourceID AND src.Source = 'BioStor'
			LEFT JOIN dbo.BHLSegmentAuthor bsa
				ON bsa.SegmentID = @BHLSegmentID
				AND bsa.AuthorID = sa.BHLAuthorID
	WHERE	sa.SegmentID = @SegmentID
	AND		sa.BHLAuthorID IS NOT NULL
	AND		bsa.SegmentAuthorID IS NULL

	-- Add new BHL Author records
	DECLARE curAuthors CURSOR FOR
	SELECT	SegmentAuthorID, BioStorID, LastName, FirstName, SequenceOrder, VIAFIdentifier
	FROM	dbo.SegmentAuthor sa
			INNER JOIN dbo.ImportSource src ON sa.ImportSourceID = src.ImportSourceID AND src.Source = 'BioStor'
	WHERE	sa.SegmentID = @SegmentID
	AND		sa.BHLAuthorID IS NULL

	DECLARE @SegmentAuthorID int
	DECLARE @BioStorID nvarchar(100)
	DECLARE @LastName nvarchar(150)
	DECLARE @FirstName nvarchar(150)
	DECLARE @SequenceOrder int
	DECLARE @VIAFIdentifier nvarchar(20)
	DECLARE @BHLAuthorID int

	OPEN curAuthors

	FETCH NEXT FROM curAuthors INTO @SegmentAuthorID, @BioStorID, @LastName, @FirstName, @SequenceOrder, @VIAFIdentifier
	WHILE (@@fetch_status <> -1)
	BEGIN
		IF (@@fetch_status <> -2)
		BEGIN
			-- Add the BHL Author record
			INSERT dbo.BHLAuthor (AuthorTypeID, IsActive, CreationUserID, LastModifiedUserID)
			VALUES (1, 1, @UserID, @UserID)

			SET @BHLAuthorID = SCOPE_IDENTITY()

			-- Add a BHL AuthorIdentifier record for the author's BioStor ID
			IF (@BioStorID <> '')
			BEGIN
				INSERT dbo.BHLAuthorIdentifier (AuthorID, IdentifierID, IdentifierValue,
					CreationUserID, LastModifiedUserID)
				VALUES (@BHLAuthorID, @BSIdentifierID, @BioStorID, @UserID, @UserID)
			END

			-- Add a BHL AuthorIdentifier record for the author's VIAF ID
			IF (@VIAFIdentifier <> '')
			BEGIN
				INSERT dbo.BHLAuthorIdentifier (AuthorID, IdentifierID, IdentifierValue,
					CreationUserID, LastModifiedUserID)
				VALUES (@BHLAuthorID, @VIAFIdentifierID, @VIAFIdentifier, @UserID, @UserID)
			END

			-- Add a BHL AuthorName record
			INSERT dbo.BHLAuthorName (AuthorID, FullName, LastName, FirstName, IsPreferredName, 
				CreationUserID, LastModifiedUserID)
			VALUES (@BHLAuthorID, 
					@LastName + CASE WHEN @LastName <> '' AND @FirstName <> '' THEN ', ' ELSE '' END + @FirstName, 
					@LastName, @FirstName, 1, @UserID, @UserID)
					
			-- Add a BHL SegmentAuthor record for the new Author record
			INSERT dbo.BHLSegmentAuthor (SegmentID, AuthorID, SequenceOrder, CreationUserID, LastModifiedUserID)
			VALUES (@BHLSegmentID, @BHLAuthorID, @SequenceOrder, @UserID, @UserID)
			
			-- Update the SegmentAuthor table in the import DB with the BHL Author ID
			UPDATE	dbo.SegmentAuthor 
			SET		BHLAuthorID = @BHLAuthorID, LastModifiedDate = GETDATE() 
			WHERE	SegmentAuthorID = @SegmentAuthorID
		END
		FETCH NEXT FROM curAuthors INTO @SegmentAuthorID, @BioStorID, @LastName, @FirstName, @SequenceOrder, @VIAFIdentifier
	END

	CLOSE curAuthors
	DEALLOCATE curAuthors

	-- Delete SegmentAuthors from production that are no longer associated with the segment.  
	-- Only consider authors added by the automated ingest process (CreationUserID = 1).
	DELETE	dbo.BHLSegmentAuthor
	WHERE	SegmentAuthorID IN (
		SELECT	p.SegmentAuthorID
		FROM	(SELECT	* FROM dbo.BHLSegmentAuthor WHERE SegmentID = @BHLSegmentID AND CreationUserID = 1) p -- Production authors
				LEFT JOIN 
				(SELECT	sa.SegmentAuthorID, s.BHLSegmentID, sa.BHLAuthorID
				 FROM	dbo.SegmentAuthor sa INNER JOIN dbo.BSSegment s ON sa.SegmentID = s.SegmentID
						INNER JOIN dbo.ImportSource src ON sa.ImportSourceID = src.ImportSourceID AND src.Source = 'BioStor'
				 WHERE	s.SegmentID = @SegmentID) i  -- Imported authors
					ON p.SegmentID = i.BHLSegmentID
					AND p.AuthorID = i.BHLAuthorID
		WHERE	i.SegmentAuthorID IS NULL  -- Look for authors in production but not the import db
		)

	COMMIT TRAN
END TRY
BEGIN CATCH
	-- Error, so roll back the transaction and return the error details to the calling application
	IF @@TRANCOUNT > 0 ROLLBACK TRAN
    SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH

END
