CREATE PROCEDURE [dbo].[OAIRecordPublishInsertSegment]

@OAIRecordID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @ProductionItemID int
DECLARE @ProductionSegmentID int
DECLARE @IdentifierOAI int
DECLARE @IdentifierISBN int
DECLARE @IdentifierLCCN int
DECLARE @ContributorRoleID int
DECLARE @Right nvarchar(500)

SELECT @IdentifierOAI = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'OAI'
SELECT @IdentifierISBN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'ISBN'
SELECT @IdentifierLCCN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'DLC'
SELECT @ContributorRoleID = InstitutionRoleID FROM dbo.BHLInstitutionRole WHERE InstitutionRoleName = 'Contributor'

SET @Right = ''
SELECT TOP 1 @Right = [Right] FROM dbo.OAIRecordRight WHERE OAIRecordID = @OAIRecordID

-- Start a new transaction while we update production
BEGIN TRAN

-- Insert a new item record
INSERT	dbo.BHLItem 
		(
		ItemTypeID, 
		ItemStatusID, 
		ItemSourceID, 
		CreationUserID, 
		LastModifiedUserID
		)
SELECT	20 AS ItemTypeID,	-- Segment
		30 AS ItemStatusID,	-- New
		src.BHLItemSourceID AS ItemSourceID,
		1 AS CreationUserID, 
		1 AS LastModifiedUserID
FROM	dbo.OAIRecord o
		INNER JOIN dbo.OAIHarvestLog lg ON o.HarvestLogID = lg.HarvestLogID
		INNER JOIN dbo.OAIHarvestSet s ON lg.HarvestSetID = s.HarvestSetID
		INNER JOIN dbo.OAIRepositoryFormat rf ON s.RepositoryFormatID = rf.RepositoryFormatID
		INNER JOIN dbo.OAIRepository r ON rf.RepositoryID = r.RepositoryID
		INNER JOIN dbo.ImportSourceItemSource src ON r.ImportSourceID = src.ImportSourceID
WHERE	o.OAIRecordID = @OAIRecordID

-- Preserve the production identifier for the new item
SET @ProductionItemID = SCOPE_IDENTITY()

-- Insert a new segment record
INSERT	BHLSegment
		(
		ItemID,
		SegmentGenreID,
		Title,
		SortTitle,
		ContainerTitle,
		PublicationDetails,
		PublisherName,
		PublisherPlace,
		Volume,
		Issue,
		Date,
		PageRange,
		StartPageNumber,
		EndPageNumber,
		LanguageCode,
		Url,
		RightsStatement,
		CreationUserID,
		LastModifiedUserID
		)
SELECT	@ProductionItemID AS ItemID,
		1 AS SegmentGenreID,
		o.Title,
		dbo.fnGetSortString(
			CASE
				WHEN LEFT(o.Title, 1) = '"' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 1))
				WHEN LEFT(o.Title, 1) = '''' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 1))
				WHEN LEFT(o.Title, 1) = '[' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 1)) 
				WHEN LEFT(o.Title, 1) = '(' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 1))
				WHEN LEFT(o.Title, 1) = '|' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 1))
				WHEN LOWER(LEFT(o.Title, 2)) = 'a ' AND Title <> 'a' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 2)) 
				WHEN LOWER(LEFT(o.Title, 3)) = 'an ' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 3)) 
				WHEN LOWER(LEFT(o.Title, 3)) = 'el ' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 3)) 
				WHEN LOWER(LEFT(o.Title, 3)) = 'il ' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 3)) 
				WHEN LOWER(LEFT(o.Title, 3)) = 'la ' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 3)) 
				WHEN LOWER(LEFT(o.Title, 3)) = 'le ' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 3)) 
				WHEN LOWER(LEFT(o.Title, 4)) = 'das ' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 4)) 
				WHEN LOWER(LEFT(o.Title, 4)) = 'der ' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 4)) 
				WHEN LOWER(LEFT(o.Title, 4)) = 'die ' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 4)) 
				WHEN LOWER(LEFT(o.Title, 4)) = 'ein ' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 4)) 
				WHEN LOWER(LEFT(o.Title, 4)) = 'las ' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 4)) 
				WHEN LOWER(LEFT(o.Title, 4)) = 'les ' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 4)) 
				WHEN LOWER(LEFT(o.Title, 4)) = 'los ' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 4)) 
				WHEN LOWER(LEFT(o.Title, 4)) = 'the ' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 4)) 
				ELSE o.Title
			END
		) AS SortTitle,
		o.ContainerTitle,
		LEFT(o.PublicationPlace + ' ' + o.Publisher + ' ' + o.PublicationDate, 400) AS PublicationDetails,
		o.Publisher,
		o.PublicationPlace,
		o.Volume,
		o.Issue,
		CASE WHEN o.PublicationDate = '' THEN o.Date ELSE o.PublicationDate END AS Date,
		CASE
			WHEN o.StartPage <> '' AND o.EndPage <> '' THEN o.StartPage + '--' + o.EndPage
			WHEN o.StartPage <> '' THEN o.StartPage
			ELSE o.EndPage
		END AS PageRange,
		o.StartPage,
		o.EndPage,
		l.BHLLanguageCode,
		o.Url,
		LEFT(ISNULL(@Right, ''), 500) AS RightsStatement,
		1 AS CreationUserID,
		1 AS LastModifiedUserID
FROM	dbo.OAIRecord o
		LEFT JOIN dbo.OAIRecordLanguage l ON o.Language = l.OAILanguage
		INNER JOIN dbo.OAIHarvestLog lg ON o.HarvestLogID = lg.HarvestLogID
		INNER JOIN dbo.OAIHarvestSet s ON lg.HarvestSetID = s.HarvestSetID
		INNER JOIN dbo.OAIRepositoryFormat rf ON s.RepositoryFormatID = rf.RepositoryFormatID
		INNER JOIN dbo.OAIRepository r ON rf.RepositoryID = r.RepositoryID
WHERE	o.OAIRecordID = @OAIRecordID

-- Preserve the production identifier for the new segment
SET @ProductionSegmentID = SCOPE_IDENTITY()

-- Insert an ItemInstitution record for the contributor
INSERT	dbo.BHLItemInstitution (ItemID, InstitutionCode, InstitutionRoleID)
SELECT	@ProductionItemID, r.BHLInstitutionCode, @ContributorRoleID
FROM	dbo.OAIRecord o
		INNER JOIN dbo.OAIHarvestLog lg ON o.HarvestLogID = lg.HarvestLogID
		INNER JOIN dbo.OAIHarvestSet s ON lg.HarvestSetID = s.HarvestSetID
		INNER JOIN dbo.OAIRepositoryFormat rf ON s.RepositoryFormatID = rf.RepositoryFormatID
		INNER JOIN dbo.OAIRepository r ON rf.RepositoryID = r.RepositoryID
WHERE	o.OAIRecordID = @OAIRecordID

-- Insert ItemIdentifier records
INSERT dbo.BHLItemIdentifier (ItemID, IdentifierID, IdentifierValue)
SELECT @ProductionItemID, @IdentifierOAI, OAIIdentifier FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID

INSERT dbo.BHLItemIdentifier (ItemID, IdentifierID, IdentifierValue)
SELECT @ProductionItemID, dbo.BHLfnGetISSNID(Issn), dbo.BHLfnGetISSNValue(Issn) FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Issn <> ''

INSERT dbo.BHLItemIdentifier (ItemID, IdentifierID, IdentifierValue)
SELECT @ProductionItemID, @IdentifierISBN, Isbn FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Isbn <> ''

INSERT dbo.BHLItemIdentifier (ItemID, IdentifierID, IdentifierValue)
SELECT @ProductionItemID, @IdentifierLCCN, Lccn FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Lccn <> ''

-- Insert DOI record
DECLARE @DOI nvarchar(50)
SELECT	@DOI = Doi
FROM	dbo.OAIRecord
WHERE	OAIRecordID = @OAIRecordID AND Doi <> '' 

exec dbo.BHLDOIInsert @DOIEntityTypeID = 40, @EntityID = @ProductionSegmentID, @DOIStatusID = 200, @DOIName = @DOI, @IsValid = 1, @ExcludeBHLDOI = 1

-- Insert ItemKeyword records
INSERT dbo.BHLItemKeyword (ItemID, KeywordID, CreationDate, LastModifiedDate)
SELECT @ProductionItemID, ProductionKeywordID, GETDATE(), GETDATE() FROM dbo.OAIRecordSubject WHERE OAIRecordID = @OAIRecordID

-- Insert ItemAuthor records
INSERT	dbo.BHLItemAuthor (ItemID, AuthorID, SequenceOrder)
SELECT	@ProductionItemID, ProductionAuthorID, ROW_NUMBER() OVER (ORDER BY OAIRecordCreatorID)
FROM	dbo.OAIRecordCreator WHERE OAIRecordID = @OAIRecordID

-- Update the ProductionSegmentID and OAIRecordStatus of the just-inserted record
UPDATE	OAIRecord
SET		OAIRecordStatusID = 20,
		ProductionSegmentID = @ProductionSegmentID,
		LastModifiedDate = GETDATE()
WHERE	OAIRecordID = @OAIRecordID

-- Done with segment inserts
COMMIT TRAN

END

GO
