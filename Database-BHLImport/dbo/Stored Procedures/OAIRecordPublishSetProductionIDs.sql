CREATE PROCEDURE [dbo].[OAIRecordPublishSetProductionIDs]

@HarvestLogID int = NULL

AS

BEGIN

SET NOCOUNT ON

-- Get the list of new records harvested from OAI providers
SELECT	r.OAIRecordID, r.OAIIdentifier, repo.ImportSourceID
INTO	#tmpNewOAIRecords
FROM	dbo.OAIRecord r
		INNER JOIN OAIHarvestLog l ON r.HarvestLogID = l.HarvestLogID
		INNER JOIN OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
		INNER JOIN OAIRepositoryFormat f ON s.RepositoryFormatID = f.RepositoryFormatID
		INNER JOIN OAIRepository repo ON f.RepositoryID = repo.RepositoryID
WHERE	OAIRecordStatusID = 10
AND		ProductionTitleID IS NULL
AND		ProductionItemID IS NULL
AND		ProductionSegmentID IS NULL
AND		(r.HarvestLogID = @HarvestLogID OR @HarvestLogID IS NULL)

-- Insert new authors into the production database
DECLARE @NewAuthorID int
DECLARE @OAIRecordCreatorID int
DECLARE @CreatorType nvarchar(50)
DECLARE @FullName nvarchar(300)
DECLARE @StartDate nvarchar(25)
DECLARE @EndDate nvarchar(25)

-- Temp table to hold initial set of partial author name matches
CREATE TABLE #PartialMatch (AuthorID int NOT NULL, AuthorNameID int NOT NULL)

DECLARE	curInsert CURSOR 
FOR SELECT	OAIRecordCreatorID, CreatorType, FullName, StartDate, EndDate 
	FROM	dbo.OAIRecordCreator c INNER JOIN #tmpNewOAIRecords t ON c.OAIRecordID = t.OAIRecordID
	WHERE	c.ProductionAuthorID IS NULL
		
OPEN curInsert
FETCH NEXT FROM curInsert INTO @OAIRecordCreatorID, @CreatorType, @FullName, @StartDate, @EndDate

WHILE (@@fetch_status <> -1)
BEGIN
	IF (@@fetch_status <> -2)
	BEGIN
		-- First look for a matching author identifier value
		IF NOT EXISTS (	SELECT	ai.AuthorID
						FROM	dbo.OAIRecordCreatorIdentifier oci
								INNER JOIN dbo.BHLAuthorIdentifier ai ON oci.IdentifierValue = ai.IdentifierValue
								INNER JOIN dbo.BHLIdentifier i ON ai.IdentifierID = i.IdentifierID AND i.IdentifierName = oci.IdentifierType
						WHERE	oci.OAIRecordCreatorID = @OAIRecordCreatorID				
					)
		BEGIN
			-- If no matching identifier, then look for matching names/dates

			-- Start looking for name/date matches by reducing the set of authors to be 
			-- considered.  This significantly improves overall performance.
			DECLARE @LimitLength int = 3
			TRUNCATE TABLE #PartialMatch

			INSERT	#PartialMatch
			SELECT	AuthorID, AuthorNameID
			FROM	dbo.BHLvwAuthorName
			WHERE (
					LEFT(FullNameToken, @LimitLength) = dbo.fnRemoveNonAlphaNumericCharacters(LEFT(@FullName, @LimitLength)) OR 
					LEFT(FullNameReversedToken, @LimitLength) = dbo.fnRemoveNonAlphaNumericCharacters(LEFT(@FullName, @LimitLength))
					)

			-- Now use that limited set of author names to look for matching authors
			IF NOT EXISTS(	SELECT a.AuthorID 
							FROM dbo.BHLAuthor a INNER JOIN dbo.BHLvwAuthorName n ON a.AuthorID = n.AuthorID
								INNER JOIN #PartialMatch m ON a.AuthorID = m.AuthorID AND n.AuthorNameID = m.AuthorNameID
							WHERE (
									n.FullNameToken = dbo.fnRemoveNonAlphaNumericCharacters(@FullName) OR 
									n.FullNameReversedToken = dbo.fnRemoveNonAlphaNumericCharacters(@FullName)
									)
							AND dbo.fnRemoveNonNumericCharacters(a.StartDate) = dbo.fnRemoveNonNumericCharacters(@StartDate) 
							AND dbo.fnRemoveNonNumericCharacters(a.EndDate) = dbo.fnRemoveNonNumericCharacters(@EndDate)
						)
			BEGIN
				BEGIN TRAN

				-- Insert a new Author record into the production database
				INSERT INTO dbo.BHLAuthor (AuthorTypeID, StartDate, EndDate, IsActive)
				VALUES (CASE WHEN @CreatorType = 'corporate' THEN 2 ELSE 1 END, @StartDate, @EndDate, 1)
						
				-- Save the ID of the newly inserted author record
				SELECT @NewAuthorID = SCOPE_IDENTITY()
				
				-- Insert a new AuthorName record
				INSERT INTO dbo.BHLAuthorName (AuthorID, FullName, IsPreferredName) VALUES (@NewAuthorID, dbo.BHLfnConvertToTitleCase(dbo.BHLfnAddAuthorNameSpaces(@FullName)), 1)

				-- Insert AuthorIdentifier records
				INSERT INTO dbo.BHLAuthorIdentifier (AuthorID, IdentifierID, IdentifierValue)
				SELECT	@NewAuthorID, i.IdentifierID, oci.IdentifierValue
				FROM	dbo.OAIRecordCreatorIdentifier oci
						INNER JOIN dbo.BHLIdentifier i ON i.IdentifierName = oci.IdentifierType
				WHERE	oci.OAIRecordCreatorID = @OAIRecordCreatorID

				-- Preserve the production identifier
				UPDATE	OAIRecordCreator
				SET		ProductionAuthorID = @NewAuthorID,
						LastModifiedDate = GETDATE()
				WHERE	OAIRecordCreatorID = @OAIRecordCreatorID

				COMMIT TRAN
			END
		END
	END
	FETCH NEXT FROM curInsert INTO @OAIRecordCreatorID, @CreatorType, @FullName, @StartDate, @EndDate
END

CLOSE curInsert
DEALLOCATE curInsert


-- Start a transaction while we gather the rest of the production identifiers
BEGIN TRAN

-- The any of the new records were harvested previously, update the
-- new records with the correct production identifiers
UPDATE	dbo.OAIRecord
SET		ProductionTitleID = old.ProductionTitleID,
		ProductionItemID = old.ProductionItemID,
		ProductionSegmentID = old.ProductionSegmentID,
		LastModifiedDate = GETDATE()
FROM	dbo.OAIRecord
		INNER JOIN #tmpNewOAIRecords t ON OAIRecord.OAIRecordID = t.OAIRecordID
		INNER JOIN dbo.OAIRecord old ON old.OAIIdentifier = t.OAIIdentifier AND old.OAIRecordStatusID = 20
		INNER JOIN OAIHarvestLog l ON old.HarvestLogID = l.HarvestLogID
		INNER JOIN OAIHarvestSet s ON l.HarvestSetID = s.HarvestSetID
		INNER JOIN OAIRepositoryFormat f ON s.RepositoryFormatID = f.RepositoryFormatID
		INNER JOIN OAIRepository repo ON f.RepositoryID = repo.RepositoryID
WHERE	t.ImportSourceID = repo.ImportSourceID	-- check this just in case two repositories use the same OAI IDs

-- If any of the production titles have been redirected to a different 
-- title, then use that title instead.  Follow the "redirect" chain up 
-- to ten levels.
UPDATE	dbo.OAIRecord
SET		ProductionTitleID = COALESCE(bt10.TitleID, bt9.TitleID, bt8.TitleiD, bt7.TitleID, bt6.TitleID,
									bt5.TitleID, bt4.TitleID, bt3.TitleID, bt2.TitleID, bt1.TitleID)
FROM	dbo.OAIRecord o 
		INNER JOIN #tmpNewOAIRecords t ON o.OAIRecordID = t.OAIRecordID
		INNER JOIN dbo.BHLTitle bt1 ON o.ProductionTitleID = bt1.TitleID
		LEFT JOIN dbo.BHLTitle bt2 ON bt1.RedirectTitleID = bt2.TitleID
		LEFT JOIN dbo.BHLTitle bt3 ON bt2.RedirectTitleID = bt3.TitleID
		LEFT JOIN dbo.BHLTitle bt4 ON bt3.RedirectTitleID = bt4.TitleID
		LEFT JOIN dbo.BHLTitle bt5 ON bt4.RedirectTitleID = bt5.TitleID
		LEFT JOIN dbo.BHLTitle bt6 ON bt5.RedirectTitleID = bt6.TitleID
		LEFT JOIN dbo.BHLTitle bt7 ON bt6.RedirectTitleID = bt7.TitleID
		LEFT JOIN dbo.BHLTitle bt8 ON bt7.RedirectTitleID = bt8.TitleID
		LEFT JOIN dbo.BHLTitle bt9 ON bt8.RedirectTitleID = bt9.TitleID
		LEFT JOIN dbo.BHLTitle bt10 ON bt9.RedirectTitleID = bt10.TitleID
WHERE	o.ProductionTitleID IS NOT NULl

-- If any of the production books have been redirected to a different 
-- book, then use that book instead.  Follow the "redirect" chain up 
-- to ten levels.
UPDATE	dbo.OAIRecord
SET		ProductionItemID = COALESCE(bi10.BookID, bi9.BookID, bi8.BookID, bi7.BookID, bi6.BookID,
									bi5.BookID, bi4.BookID, bi3.BookID, bi2.BookID, bi1.BookID)
FROM	dbo.OAIRecord o 
		INNER JOIN #tmpNewOAIRecords t ON o.OAIRecordID = t.OAIRecordID
		INNER JOIN dbo.BHLBook bi1 ON o.ProductionItemID = bi1.BookID
		LEFT JOIN dbo.BHLBook bi2 ON bi1.RedirectBookID = bi2.BookID
		LEFT JOIN dbo.BHLBook bi3 ON bi2.RedirectBookID = bi3.BookID
		LEFT JOIN dbo.BHLBook bi4 ON bi3.RedirectBookID = bi4.BookID
		LEFT JOIN dbo.BHLBook bi5 ON bi4.RedirectBookID = bi5.BookID
		LEFT JOIN dbo.BHLBook bi6 ON bi5.RedirectBookID = bi6.BookID
		LEFT JOIN dbo.BHLBook bi7 ON bi6.RedirectBookID = bi7.BookID
		LEFT JOIN dbo.BHLBook bi8 ON bi7.RedirectBookID = bi8.BookID
		LEFT JOIN dbo.BHLBook bi9 ON bi8.RedirectBookID = bi9.BookID
		LEFT JOIN dbo.BHLBook bi10 ON bi9.RedirectBookID = bi10.BookID
WHERE	o.ProductionItemID IS NOT NULL

-- If any of the production segments have been redirected to a different 
-- segment, then use that segment instead.  Follow the "redirect" chain up 
-- to ten levels.
UPDATE	dbo.OAIRecord
SET		ProductionSegmentID = COALESCE(bs10.SegmentID, bs9.SegmentID, bs8.SegmentID, bs7.SegmentID, bs6.SegmentID,
									bs5.SegmentID, bs4.SegmentID, bs3.SegmentID, bs2.SegmentID, bs1.SegmentID)
FROM	dbo.OAIRecord o 
		INNER JOIN #tmpNewOAIRecords t ON o.OAIRecordID = t.OAIRecordID
		INNER JOIN dbo.BHLSegment bs1 ON o.ProductionSegmentID = bs1.SegmentID
		LEFT JOIN dbo.BHLSegment bs2 ON bs1.RedirectSegmentID = bs2.SegmentID
		LEFT JOIN dbo.BHLSegment bs3 ON bs2.RedirectSegmentID = bs3.SegmentID
		LEFT JOIN dbo.BHLSegment bs4 ON bs3.RedirectSegmentID = bs4.SegmentID
		LEFT JOIN dbo.BHLSegment bs5 ON bs4.RedirectSegmentID = bs5.SegmentID
		LEFT JOIN dbo.BHLSegment bs6 ON bs5.RedirectSegmentID = bs6.SegmentID
		LEFT JOIN dbo.BHLSegment bs7 ON bs6.RedirectSegmentID = bs7.SegmentID
		LEFT JOIN dbo.BHLSegment bs8 ON bs7.RedirectSegmentID = bs8.SegmentID
		LEFT JOIN dbo.BHLSegment bs9 ON bs8.RedirectSegmentID = bs9.SegmentID
		LEFT JOIN dbo.BHLSegment bs10 ON bs9.RedirectSegmentID = bs10.SegmentID
WHERE	o.ProductionSegmentID IS NOT NULL

-- Insert any new subjects
INSERT	dbo.BHLKeyword (Keyword)
SELECT DISTINCT s.Keyword
FROM	dbo.OAIRecordSubject s
		INNER JOIN #tmpNewOAIRecords t ON s.OAIRecordID = t.OAIRecordID
		LEFT JOIN dbo.BHLKeyword k ON s.Keyword = k.Keyword
WHERE	k.KeywordID IS NULL

-- Get subject production ids
UPDATE	dbo.OAIRecordSubject
SET		ProductionKeywordID = k.KeywordID,
		LastModifiedDate = GETDATE()
FROM	dbo.OAIRecordSubject s
		INNER JOIN #tmpNewOAIRecords t ON s.OAIRecordID = t.OAIRecordID
		INNER JOIN dbo.BHLKeyword k ON s.Keyword = k.Keyword

-- Get author production ids
UPDATE	dbo.OAIRecordCreator
SET		ProductionAuthorID = ai.AuthorID,
		LastModifiedDate = GETDATE()
FROM	dbo.OAIRecordCreator c
		INNER JOIN #tmpNewOAIRecords t ON c.OAIRecordID = t.OAIRecordID
		INNER JOIN dbo.OAIRecordCreatorIdentifier oci ON c.OAIRecordCreatorID = oci.OAIRecordCreatorID
		INNER JOIN dbo.BHLAuthorIdentifier ai ON oci.IdentifierValue = ai.IdentifierValue
		INNER JOIN dbo.BHLIdentifier i ON ai.IdentifierID = i.IdentifierID AND i.IdentifierName = oci.IdentifierType
WHERE	c.ProductionAuthorID IS NULL

UPDATE	dbo.OAIRecordCreator
SET		ProductionAuthorID = a.AuthorID,
		LastModifiedDate = GETDATE()
FROM	dbo.OAIRecordCreator c
		INNER JOIN #tmpNewOAIRecords t ON c.OAIRecordID = t.OAIRecordID
		INNER JOIN dbo.BHLAuthor a 
			ON dbo.fnRemoveNonNumericCharacters(c.StartDate) = dbo.fnRemoveNonNumericCharacters(a.StartDate) 
			AND dbo.fnRemoveNonNumericCharacters(c.EndDate) = dbo.fnRemoveNonNumericCharacters(a.EndDate)
		INNER JOIN dbo.BHLvwAuthorName n ON a.AuthorID = n.AuthorID
WHERE	n.FullNameToken = dbo.fnRemoveNonAlphaNumericCharacters(c.FullName)
AND		c.ProductionAuthorID IS NULL

UPDATE	dbo.OAIRecordCreator
SET		ProductionAuthorID = a.AuthorID,
		LastModifiedDate = GETDATE()
FROM	dbo.OAIRecordCreator c
		INNER JOIN #tmpNewOAIRecords t ON c.OAIRecordID = t.OAIRecordID
		INNER JOIN dbo.BHLAuthor a 
			ON dbo.fnRemoveNonNumericCharacters(c.StartDate) = dbo.fnRemoveNonNumericCharacters(a.StartDate)
			AND dbo.fnRemoveNonNumericCharacters(c.EndDate) = dbo.fnRemoveNonNumericCharacters(a.EndDate)
		INNER JOIN dbo.BHLvwAuthorName n ON a.AuthorID = n.AuthorID
WHERE	n.FullNameReversedToken = dbo.fnRemoveNonAlphaNumericCharacters(c.FullName)
AND		c.ProductionAuthorID IS NULL

-- If the selected production author ID has been redirected to a different 
-- author, then use that author instead.  Follow the "redirect" chain up 
-- to ten levels.
UPDATE	dbo.OAIRecordCreator
SET		ProductionAuthorID = COALESCE(a10.AuthorID, a9.AuthorID, a8.AuthorID, a7.AuthorID, a6.AuthorID,
									a5.AuthorID, a4.AuthorID, a3.AuthorID, a2.AuthorID, a1.AuthorID)
FROM	dbo.OAIRecordCreator c INNER JOIN dbo.BHLAuthor a1 ON c.ProductionAuthorID = a1.AuthorID
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

-- Get related title ids
UPDATE	OAIRecordRelatedTitle
SET		ProductionEntityType = 'Association',
		ProductionEntityID = ta.TitleAssociationID,
		LastModifiedDate = GETDATE()
FROM	dbo.OAIRecordRelatedTitle rt
		INNER JOIN #tmpNewOAIRecords t ON rt.OAIRecordID = t.OAIRecordID
		INNER JOIN dbo.OAIRecord r ON rt.OAIRecordID = r.OAIRecordID
		INNER JOIN dbo.OAIRecordRelatedTitleTypeAssociation rta ON rt.TitleType = rta.TitleType
		INNER JOIN dbo.BHLTitleAssociation ta ON r.ProductionTitleID = ta.TitleID AND rta.BHLTitleAssociationTypeID = ta.TitleAssociationTypeID
WHERE	ta.Title = rt.Title

UPDATE	OAIRecordRelatedTitle
SET		ProductionEntityType = 'Variant',
		ProductionEntityID = tv.TitleVariantID,
		LastModifiedDate = GETDATE()
FROM	dbo.OAIRecordRelatedTitle rt
		INNER JOIN #tmpNewOAIRecords t ON rt.OAIRecordID = t.OAIRecordID
		INNER JOIN dbo.OAIRecord r ON rt.OAIRecordID = r.OAIRecordID
		INNER JOIN dbo.OAIRecordRelatedTitleTypeVariant rtv ON rt.TitleType = rtv.TitleType
		INNER JOIN dbo.BHLTitleVariant tv ON r.ProductionTitleID = tv.TitleID AND rtv.BHLTitleVariantTypeID = tv.TitleVariantTypeID
WHERE	tv.Title = rt.Title COLLATE SQL_Latin1_General_CP1_CI_AI

UPDATE	OAIRecordRelatedTitle
SET		ProductionEntityType = 'Title',
		ProductionEntityID = r.ProductionTitleID,
		LastModifiedDate = GETDATE()
FROM	dbo.OAIRecordRelatedTitle rt
		INNER JOIN #tmpNewOAIRecords t ON rt.OAIRecordID = t.OAIRecordID
		INNER JOIN OAIRecord r ON rt.OAIRecordID = r.OAIRecordID
WHERE	rt.TitleType = 'uniform'
AND		r.ProductionTitleID IS NOT NULL

DROP TABLE #tmpNewOAIRecords

-- Done gathering production IDs, so finish this transaction
COMMIT TRAN

END

GO
