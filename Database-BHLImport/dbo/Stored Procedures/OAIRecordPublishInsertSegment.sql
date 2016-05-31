CREATE PROCEDURE [dbo].[OAIRecordPublishInsertSegment]

@OAIRecordID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @ProductionSegmentID int
DECLARE @IdentifierOAI int
DECLARE @IdentifierISSN int
DECLARE @IdentifierISBN int
DECLARE @IdentifierLCCN int
DECLARE @ContributorRoleID int
DECLARE @Right nvarchar(500)

SELECT @IdentifierOAI = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'OAI'
SELECT @IdentifierISSN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'ISSN'
SELECT @IdentifierISBN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'ISBN'
SELECT @IdentifierLCCN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'DLC'
SELECT @ContributorRoleID = InstitutionRoleID FROM dbo.BHLInstitutionRole WHERE InstitutionRoleName = 'Contributor'

SET @Right = ''
SELECT TOP 1 @Right = [Right] FROM dbo.OAIRecordRight WHERE OAIRecordID = @OAIRecordID

-- Start a new transaction while we update production
BEGIN TRAN
		
-- Insert a new segment record
INSERT	BHLSegment
		(
		SegmentStatusID,
		SequenceOrder,
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
SELECT	10 AS SegmentStatusID,
		1 AS SequenceOrder,
		1 AS SegmentGenreID,
		o.Title,
		CASE
		WHEN LEFT(o.Title, 1) = '"' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 1))
		WHEN LEFT(o.Title, 1) = '''' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 1))
		WHEN LEFT(o.Title, 1) = '[' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 1)) 
		WHEN LEFT(o.Title, 1) = '(' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 1))
		WHEN LEFT(o.Title, 1) = '|' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 1))
		WHEN LOWER(LEFT(o.Title, 2)) = 'a ' AND Title <> 'a' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 2)) 
		WHEN LOWER(LEFT(o.Title, 3)) = 'an ' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 3)) 
		WHEN LOWER(LEFT(o.Title, 3)) = 'de ' THEN LTRIM(RIGHT(o.Title, LEN(o.Title) - 3)) 
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
		END AS SortTitle,
		o.ContainerTitle,
		LEFT(o.PublicationPlace + ' ' + o.Publisher + ' ' + o.PublicationDate, 400) AS PublicationDetails,
		o.Publisher,
		o.PublicationPlace,
		o.Volume,
		o.Issue,
		CASE WHEN o.PublicationDate = '' THEN o.Date ELSE o.PublicationDate END AS Date,
		CASE WHEN o.StartPage <> '' THEN o.StartPage + '-' + o.EndPage ELSE o.EndPage END AS PageRange,
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

-- Insert a SegmentInstitution record for the contributor
INSERT	dbo.BHLSegmentInstitution (SegmentID, InstitutionCode, InstitutionRoleID)
SELECT	@ProductionSegmentID, r.BHLInstitutionCode, @ContributorRoleID
FROM	dbo.OAIRecord o
		INNER JOIN dbo.OAIHarvestLog lg ON o.HarvestLogID = lg.HarvestLogID
		INNER JOIN dbo.OAIHarvestSet s ON lg.HarvestSetID = s.HarvestSetID
		INNER JOIN dbo.OAIRepositoryFormat rf ON s.RepositoryFormatID = rf.RepositoryFormatID
		INNER JOIN dbo.OAIRepository r ON rf.RepositoryID = r.RepositoryID
WHERE	o.OAIRecordID = @OAIRecordID

-- Insert SegmentIdentifier records
INSERT dbo.BHLSegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, IsContainerIdentifier)
SELECT @ProductionSegmentID, @IdentifierOAI, OAIIdentifier, 0 FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID

INSERT dbo.BHLSegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, IsContainerIdentifier)
SELECT @ProductionSegmentID, @IdentifierISSN, Issn, 0 FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Issn <> ''

INSERT dbo.BHLSegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, IsContainerIdentifier)
SELECT @ProductionSegmentID, @IdentifierISBN, Isbn, 0 FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Isbn <> ''

INSERT dbo.BHLSegmentIdentifier (SegmentID, IdentifierID, IdentifierValue, IsContainerIdentifier)
SELECT @ProductionSegmentID, @IdentifierLCCN, Lccn, 0 FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Lccn <> ''

-- Insert DOI record
INSERT dbo.BHLDOI (DOIEntityTypeID, EntityID, DOIStatusID, DOIName, StatusDate, IsValid)
SELECT 40, @ProductionSegmentID, 200, Doi, GETDATE(), 1 FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Doi <> ''

-- Insert SegmentKeyword records
INSERT dbo.BHLSegmentKeyword (SegmentID, KeywordID, CreationDate, LastModifiedDate)
SELECT @ProductionSegmentID, ProductionKeywordID, GETDATE(), GETDATE() FROM dbo.OAIRecordSubject WHERE OAIRecordID = @OAIRecordID

-- Insert SegmentAuthor records
INSERT	dbo.BHLSegmentAuthor (SegmentID, AuthorID, SequenceOrder)
SELECT	@ProductionSegmentID, ProductionAuthorID, ROW_NUMBER() OVER (ORDER BY OAIRecordCreatorID)
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

