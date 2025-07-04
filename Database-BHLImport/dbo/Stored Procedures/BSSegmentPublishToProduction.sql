CREATE PROCEDURE [dbo].[BSSegmentPublishToProduction]

@ItemID int,
@SegmentID int,
@SegmentStatusID int OUTPUT

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

DECLARE @BSAuthorIdentifierID int
SELECT @BSAuthorIdentifierID = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'BioStor Author ID'

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

DECLARE @DOIStatusApprovedID int
SELECT @DOIStatusApprovedID = DOIStatusID FROM dbo.BHLDOIStatus WHERE DOIStatusName = 'DOI Approved'

DECLARE @DOIStatusExternalID int
SELECT @DOIStatusExternalID = DOIStatusID FROM dbo.BHLDOIStatus WHERE DOIStatusName = 'External DOI'

DECLARE @SegmentGenreID int
SELECT	@SegmentGenreID = SegmentGenreID
FROM	dbo.BHLSegmentGenre g INNER JOIN dbo.BSSegment s ON g.GenreName = CASE WHEN s.Genre = '' THEN 'article' ELSE s.Genre END
WHERE	s.SegmentID = @SegmentID

DECLARE @ContributorRoleID int
SELECT	@ContributorRoleID = InstitutionRoleID FROM dbo.BHLInstitutionRole WHERE InstitutionRoleName = 'Contributor'

DECLARE @BHLBookItemID int
SELECT	@BHLBookItemID = ItemID FROM dbo.BHLBook WHERE BookID = @ItemID

DECLARE @SegmentStatusPublishedID int
SELECT @SegmentStatusPublishedID = SegmentStatusID FROM dbo.BSSegmentStatus WHERE StatusName = 'Published'

DECLARE @SegmentStatusSkippedID int
SELECT @SegmentStatusSkippedID = SegmentStatusID FROM dbo.BSSegmentStatus WHERE StatusName = 'SkippedDOI'

DECLARE @SegmentStatusErrorID int
SELECT @SegmentStatusErrorID = SegmentStatusID FROM dbo.BSSegmentStatus WHERE StatusName = 'PublishError'

SET @SegmentStatusID = @SegmentStatusPublishedID

-- ******************************************************************************

-- Get the production ID for this segment, if one exists
DECLARE @BHLSegmentID int
DECLARE @BHLItemID int

SELECT	@BHLSegmentID = bseg.SegmentID, @BHLItemID = bseg.ItemID
FROM	dbo.BHLItemIdentifier ii
		INNER JOIN dbo.BHLSegment bseg ON ii.ItemID = bseg.ItemID
		INNER JOIN dbo.BSSegment s ON ii.IdentifierValue = s.BioStorReferenceID AND ii.IdentifierID = @BSIdentifierID
WHERE	s.SegmentID = @SegmentID

BEGIN TRY
	BEGIN TRAN

	IF (@BHLSegmentID IS NULL)
	BEGIN
		-- ******************************************************************************
		
		-- Segment not in production, so insert it.

		-- Insert a new BHL Item record
		INSERT	dbo.BHLItem (ItemTypeID, ItemStatusID, ItemDescription, Note, CreationUserID, LastModifiedUserID) 
		VALUES (20, 30, '', '', @UserID, @UserID)

		SET @BHLItemID = SCOPE_IDENTITY()

		-- Insert a new BHL ItemRelationship record
		INSERT	dbo.BHLItemRelationship (ParentID, ChildID, SequenceOrder, CreationUserID, LastModifiedUserID)
		SELECT	@BHLBookItemID, @BHLItemID, SequenceOrder, @UserID, @UserID FROM dbo.BSSegment WHERE SegmentID = @SegmentID

		-- Insert a new BHL Segment record
		INSERT	dbo.BHLSegment (ItemID, SegmentGenreID, Title, ContainerTitle, PublisherName, 
				PublisherPlace, Volume, Series, Issue, [Date], 
				PageRange, StartPageNumber, EndPageNumber, StartPageID, 
				SortTitle, CreationUserID, LastModifiedUserID)
		SELECT	@BHLItemID, @SegmentGenreID, Title,ContainerTitle, PublisherName, 
				PublisherPlace, Volume, Series, Issue, CASE WHEN dbo.fnBSIsDate([Date]) = 1 THEN [Date] ELSE [Year] END,
				StartPageNumber + '--' + EndPageNumber, StartPageNumber, EndPageNumber, StartPageID, 
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
				) AS SortTitle,
				@UserID, @UserID
		FROM	dbo.BSSegment
		WHERE	SegmentID = @SegmentID
		
		SET @BHLSegmentID = SCOPE_IDENTITY()

		-- Insert new BHL SegmentInstitution records
		-- Every segment harvested by this procedure has a BSTOR contributor
		INSERT	dbo.BHLItemInstitution (ItemID, InstitutionCode, InstitutionRoleID, CreationUserID, LastModifiedUserID)
		SELECT	@BHLItemID, 'BSTOR', @ContributorRoleID, @UserID, @UserID
		FROM	dbo.BSSegment
		WHERE	SegmentID = @SegmentID

		INSERT	dbo.BHLItemInstitution (ItemID, InstitutionCode, InstitutionRoleID,CreationUserID, LastModifiedUserID)
		SELECT	@BHLItemID, i.InstitutionCode, @ContributorRoleID, @UserID, @UserID
		FROM	dbo.BSSegment s
				INNER JOIN dbo.BHLInstitution i 
					ON s.ContributorName = i.InstitutionName COLLATE Latin1_general_CI_AI -- ignore diacritics for this comparison
		WHERE	SegmentID = @SegmentID

		-- Insert new BHL SegmentIdentifier record for BioStor ID
		INSERT	dbo.BHLItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
		SELECT	@BHLItemID, @BSIdentifierID, BioStorReferenceID, @UserID, @UserID
		FROM	dbo.BSSegment
		WHERE	SegmentID = @SegmentID

		-- Per 2020 Data Model Updates, container IDs are no longer stored with the Segment
		/*
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
		*/

		-- Insert new BHL SegmentIdentifier record for JSTOR
		INSERT	dbo.BHLItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
		SELECT	@BHLItemID, @JSTORIdentifierID, JSTOR, @UserID, @UserID
		FROM	dbo.BSSegment
		WHERE	SegmentID = @SegmentID
		AND		JSTOR <> ''	

		-- Insert new BHL DOI record
		DECLARE @DOINameInsert nvarchar(50)
		SELECT	@DOINameInsert = DOI
		FROM	dbo.BSSegment
		WHERE	SegmentID = @SegmentID AND DOI <> '' 

		exec dbo.BHLDOIInsert @DOIEntityTypeID, @BHLSegmentID, @DOIStatusExternalID, @DOINameInsert, @IsValid = 1, @ExcludeBHLDOI = 1
		
		-- Insert BHL SegmentPage records
		INSERT	dbo.BHLItemPage (ItemID, PageID, SequenceOrder, CreationUserID, LastModifiedUserID)
		SELECT	@BHLItemID, sp.BHLPageID, sp.SequenceOrder, @UserID, @UserID
		FROM	dbo.BSSegmentPage sp INNER JOIN dbo.BHLPage p ON sp.BHLPageID = p.PageID
		WHERE	sp.SegmentID = @SegmentID
	END
	ELSE
	BEGIN
		-- ******************************************************************************
		
		-- Segment already in production, so do an update of the Segment sequence, the Page 
		-- data, and the Author data. Don't worry about updating any other data, per a 
		-- conversation with the BHL Technical Director.  July 13, 2012

		-- NOTE:  Only perform the updates if NO BHL-managed DOI has been assigned to the 
		-- segment.  April 6, 2021 

		-- NOTE: DOI has been added to the data that is updated for production Segments.
		-- April 30, 2021

		DECLARE @DOIIdentifierID int
		SELECT @DOIIdentifierID = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'DOI'
		
		IF EXISTS (	SELECT	IdentifierValue
					FROM	dbo.BHLItemIdentifier ii
							INNER JOIN dbo.BHLSegment s ON ii.ItemID = s.ItemID AND ii.IdentifierID = @DOIIdentifierID
					WHERE	s.SegmentID = @BHLSegmentID
					AND		SUBSTRING(	IdentifierValue, 1, 
								CASE WHEN CHARINDEX('/', IdentifierValue) > 0 
									THEN CHARINDEX('/', IdentifierValue) - 1 
									ELSE LEN(IdentifierValue) 
								END) IN (SELECT Prefix FROM dbo.BHLDOIPrefix)	-- BHL-managed DOIs
				)
		BEGIN
			SET @SegmentStatusID = @SegmentStatusSkippedID
		END
		ELSE
		BEGIN
			DECLARE @SegmentSequence smallint
			DECLARE @StartPageID int
		
			-- If the specified StartPageID is not one of the segment's pages, use the BHLPageID of the first page instead
			SELECT	@SegmentSequence = s.SequenceOrder,
					@StartPageID = COALESCE(p.BHLPageID, p2.BHLPageID)
			FROM	dbo.BSSegment s
					LEFT JOIN dbo.BSSegmentPage p ON s.SegmentID = p.SegmentID AND s.StartPageID = p.BHLPageID
					LEFT JOIN dbo.BSSegmentPage p2 ON s.SegmentID = p2.SegmentID AND p2.SequenceOrder = 1
			WHERE	s.SegmentID = @SegmentID

			UPDATE	dbo.BHLSegment
			SET		StartPageID = @StartPageID,
					LastModifiedDate = GETDATE(),
					LastModifiedUserID = @UserID
			WHERE	SegmentID = @BHLSegmentID
			AND		StartPageID <> @StartPageID

			UPDATE	dbo.BHLItemRelationship
			SET		ParentID = @BHLBookItemID,
					SequenceOrder = @SegmentSequence
			WHERE	ChildID = @BHLItemID
			AND		(ParentID <> @BHLBookItemID OR
					SequenceOrder <> @SegmentSequence)

			-- Replace the DOI record
			DECLARE @DOINameUpdate nvarchar(50)
			SELECT	@DOINameUpdate = DOI 
			FROM	dbo.BSSegment
			WHERE	SegmentID = @SegmentID AND DOI <> ''

			exec dbo.BHLDOIUpdate @DOIEntityTypeID, @BHLSegmentID, @DOIStatusExternalID, @DOINameUpdate, @IsValid = 1, @ExcludeBHLDOI = 1

			-- Replace the SegmentPage records
			DELETE FROM dbo.BHLItemPage WHERE ItemId = @BHLItemID
		
			INSERT	dbo.BHLItemPage (ItemID, PageID, SequenceOrder, CreationUserID, LastModifiedUserID)
			SELECT	@BHLItemID, sp.BHLPageID, sp.SequenceOrder, @UserID, @UserID
			FROM	dbo.BSSegmentPage sp INNER JOIN dbo.BHLPage p ON sp.BHLPageID = p.PageID
			WHERE	sp.SegmentID = @SegmentID
		END
	END

	-- Add the BHL Segment ID and Status to the BSSegment record
	UPDATE dbo.BSSegment SET BHLSegmentID = @BHLSegmentID, SegmentStatusID = @SegmentStatusID WHERE SegmentID = @SegmentID

	-- Make sure any added/updated segments do not result in duplicate ItemRelationship.SequenceOrder values.
	-- If there are duplicates, insert the new segment into the sequence.  That is, if adding "3" into the 
	-- sequence 1, 2, 3, 4, 5, do the following:  keep the existing "3", add the new segment with sequenceorder "4",
	-- change the existing "4" to "5" and the existing "5" to "6".  The complete sequence will then be this:
	-- 1 (unchanged), 2 (unchanged), 3 (unchanged), 4 (new segment), 5 (changed from 4), 6 (changed from 5)
	UPDATE	dbo.BHLItemRelationship
	SET		SequenceOrder = x.SequenceOrder
	FROM	dbo.BHLItemRelationship br
			INNER JOIN (
				SELECT	RelationshipID,
						ROW_NUMBER() OVER (ORDER BY SequenceOrder, RelationshipID) AS SequenceOrder
				FROM	dbo.BHLItemRelationship
				WHERE	ParentID = @BHLBookItemID				
			) x ON br.RelationshipID = x.RelationshipID

	-- Only update page and author metadata if the segment was published
	IF @SegmentStatusID = @SegmentStatusPublishedID
	BEGIN

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
				dbo.BHLfnConvertToTitleCase(dbo.BHLfnAddAuthorNameSpaces(LastName + CASE WHEN LastName <> '' AND FirstName <> '' THEN ', ' ELSE '' END + FirstName)),
				dbo.BHLfnConvertToTitleCase(dbo.BHLfnAddAuthorNameSpaces(LastName)), dbo.BHLfnConvertToTitleCase(dbo.BHLfnAddAuthorNameSpaces(FirstName)), 0, @UserID, @UserID
		FROM	dbo.BSSegmentAuthor sa 
				INNER JOIN dbo.ImportSource src ON sa.ImportSourceID = src.ImportSourceID AND src.Source = 'BioStor'
		WHERE	sa.SegmentID = @SegmentID
		AND		sa.BHLAuthorID IS NOT NULL
		AND		NOT EXISTS (SELECT	AuthorNameID 
							FROM	dbo.BHLAuthorName 
							WHERE	AuthorID = sa.BHLAuthorID
							AND		LastName = sa.LastName
							AND		FirstName = sa.FirstName
							)

		-- Update the SequenceOrder of existing BHL ItemAuthor records (if necessary)
		UPDATE	dbo.BHLItemAuthor
		SET		SequenceOrder = sa.SequenceOrder,
				LastModifiedDate = GETDATE(),
				LastModifiedUserID = 1
		FROM	dbo.BSSegmentAuthor sa INNER JOIN dbo.ImportSource src
					ON sa.ImportSourceID = src.ImportSourceID AND src.Source = 'BioStor'
				INNER JOIN dbo.BHLItemAuthor bsa
					ON bsa.ItemID = @BHLItemID
					AND bsa.AuthorID = sa.BHLAuthorID
		WHERE	sa.SegmentID = @SegmentID
		AND		sa.BHLAuthorID IS NOT NULL
		AND		bsa.SequenceOrder <> sa.SequenceOrder

		-- Add BHL ItemAuthor records for any authors with existing records (if the 
		-- ItemAuthor records don't already exist)
		INSERT dbo.BHLItemAuthor(ItemID, AuthorID, SequenceOrder, CreationUserID, LastModifiedUserID)
		SELECT	@BHLItemID, sa.BHLAuthorID, sa.SequenceOrder, @UserID, @UserID
		FROM	dbo.BSSegmentAuthor sa INNER JOIN dbo.ImportSource src 
					ON sa.ImportSourceID = src.ImportSourceID AND src.Source = 'BioStor'
				LEFT JOIN dbo.BHLItemAuthor bsa
					ON bsa.ItemID = @BHLItemID
					AND bsa.AuthorID = sa.BHLAuthorID
		WHERE	sa.SegmentID = @SegmentID
		AND		sa.BHLAuthorID IS NOT NULL
		AND		bsa.ItemAuthorID IS NULL

		-- Add new BHL Author records
		DECLARE curAuthors CURSOR FOR
		SELECT	SegmentAuthorID, BioStorID, LastName, FirstName, SequenceOrder, VIAFIdentifier
		FROM	dbo.BSSegmentAuthor sa
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
					INSERT dbo.BHLAuthorIdentifier (AuthorID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
					VALUES (@BHLAuthorID, @BSAuthorIdentifierID, @BioStorID, @UserID, @UserID)
				END

				-- Add a BHL AuthorIdentifier record for the author's VIAF ID
				IF (@VIAFIdentifier <> '')
				BEGIN
					INSERT dbo.BHLAuthorIdentifier (AuthorID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
					VALUES (@BHLAuthorID, @VIAFIdentifierID, @VIAFIdentifier, @UserID, @UserID)
				END

				-- Add a BHL AuthorName record
				INSERT dbo.BHLAuthorName (AuthorID, FullName, LastName, FirstName, IsPreferredName, CreationUserID, LastModifiedUserID)
				VALUES (@BHLAuthorID, 
						dbo.BHLfnConvertToTitleCase(dbo.BHLfnAddAuthorNameSpaces(@LastName + CASE WHEN @LastName <> '' AND @FirstName <> '' THEN ', ' ELSE '' END + @FirstName)),
						dbo.BHLfnConvertToTitleCase(dbo.BHLfnAddAuthorNameSpaces(@LastName)), dbo.BHLfnConvertToTitleCase(dbo.BHLfnAddAuthorNameSpaces(@FirstName)), 1, @UserID, @UserID)
					
				-- Add a BHL ItemAuthor record for the new Author record
				INSERT dbo.BHLItemAuthor (ItemID, AuthorID, SequenceOrder, CreationUserID, LastModifiedUserID)
				VALUES (@BHLItemID, @BHLAuthorID, @SequenceOrder, @UserID, @UserID)
			
				-- Update the SegmentAuthor table in the import DB with the BHL Author ID
				UPDATE	dbo.BSSegmentAuthor 
				SET		BHLAuthorID = @BHLAuthorID, LastModifiedDate = GETDATE() 
				WHERE	SegmentAuthorID = @SegmentAuthorID
			END
			FETCH NEXT FROM curAuthors INTO @SegmentAuthorID, @BioStorID, @LastName, @FirstName, @SequenceOrder, @VIAFIdentifier
		END

		CLOSE curAuthors
		DEALLOCATE curAuthors

		-- Delete SegmentAuthors from production that are no longer associated with the segment.  
		-- Only consider authors added by the automated ingest process (CreationUserID = 1).
		DELETE	dbo.BHLItemAuthor
		WHERE	ItemAuthorID IN (
			SELECT	p.ItemAuthorID
			FROM	(SELECT	s.SegmentID, ia.* 
					 FROM	dbo.BHLItemAuthor ia 
							INNER JOIN dbo.BHLSegment s ON ia.ItemID = s.ItemID
					 WHERE	ia.ItemID = @BHLItemID AND ia.CreationUserID = 1) p -- Production authors
					LEFT JOIN 
					(SELECT	sa.SegmentAuthorID, s.BHLSegmentID, sa.BHLAuthorID
					 FROM	dbo.BSSegmentAuthor sa INNER JOIN dbo.BSSegment s ON sa.SegmentID = s.SegmentID
							INNER JOIN dbo.ImportSource src ON sa.ImportSourceID = src.ImportSourceID AND src.Source = 'BioStor'
					 WHERE	s.SegmentID = @SegmentID) i  -- Imported authors
						ON p.SegmentID = i.BHLSegmentID
						AND p.AuthorID = i.BHLAuthorID
			WHERE	i.SegmentAuthorID IS NULL  -- Look for authors in production but not the import db
			)
	END

	COMMIT TRAN
END TRY
BEGIN CATCH
	-- Error, so roll back the transaction and return the error details to the calling application
	IF @@TRANCOUNT > 0 ROLLBACK TRAN
    SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();

	-- Add the Error Status to the BSSegment record
	UPDATE dbo.BSSegment SET SegmentStatusID = @SegmentStatusErrorID WHERE SegmentID = @SegmentID

    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH

END

GO
