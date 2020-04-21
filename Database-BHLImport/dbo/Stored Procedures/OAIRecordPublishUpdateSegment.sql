CREATE PROCEDURE [dbo].[OAIRecordPublishUpdateSegment]

@OAIRecordID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @ProductionSegmentID int
DECLARE @Title nvarchar(2000)
DECLARE @ContainerTitle nvarchar(2000)
DECLARE @Publisher nvarchar(250)
DECLARE @PublicationPlace nvarchar(150)
DECLARE @Date nvarchar(100)
DECLARE @Volume nvarchar(100)
DECLARE @Issue nvarchar(100)
DECLARE @StartPage nvarchar(20)
DECLARE @EndPage nvarchar(20)
DECLARE @LanguageCode nvarchar(10)
DECLARE @Url nvarchar(200)

DECLARE @IdentifierOAI int
DECLARE @IdentifierISSN int
DECLARE @IdentifierISBN int
DECLARE @IdentifierLCCN int
DECLARE @Right nvarchar(500)

SELECT @IdentifierOAI = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'OAI'
SELECT @IdentifierISSN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'ISSN'
SELECT @IdentifierISBN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'ISBN'
SELECT @IdentifierLCCN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'DLC'

SET @Right = ''
SELECT TOP 1 @Right = [Right] FROM dbo.OAIRecordRight WHERE OAIRecordID = @OAIRecordID

-- Get the values to be updated
SELECT	@ProductionSegmentID = o.ProductionSegmentID,
		@Title = o.Title,
		@ContainerTitle = o.ContainerTitle,
		@Publisher = o.Publisher,
		@PublicationPlace = o.PublicationPlace,
		@Volume = o.Volume,
		@Issue = o.Issue,
		@Date = CASE WHEN o.PublicationDate = '' THEN o.Date ELSE o.PublicationDate END,
		@StartPage = o.StartPage,
		@EndPage = o.EndPage,
		@LanguageCode = l.BHLLanguageCode,
		@Url = o.Url
FROM	dbo.OAIRecord o LEFT JOIN dbo.OAIRecordLanguage l ON o.Language = l.OAILanguage
WHERE	o.OAIRecordID = @OAIRecordID

-- Start a new transaction while we update production
BEGIN TRAN

UPDATE	dbo.BHLSegment
SET		Title = @Title,
		ContainerTitle = @ContainerTitle,
		SortTitle = CASE
			WHEN LEFT(@Title, 1) = '"' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 1))
			WHEN LEFT(@Title, 1) = '''' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 1))
			WHEN LEFT(@Title, 1) = '[' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 1)) 
			WHEN LEFT(@Title, 1) = '(' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 1))
			WHEN LEFT(@Title, 1) = '|' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 1))
			WHEN LOWER(LEFT(@Title, 2)) = 'a ' AND Title <> 'a' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 2)) 
			WHEN LOWER(LEFT(@Title, 3)) = 'an ' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 3)) 
			WHEN LOWER(LEFT(@Title, 3)) = 'el ' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 3)) 
			WHEN LOWER(LEFT(@Title, 3)) = 'il ' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 3)) 
			WHEN LOWER(LEFT(@Title, 3)) = 'la ' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 3)) 
			WHEN LOWER(LEFT(@Title, 3)) = 'le ' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 3)) 
			WHEN LOWER(LEFT(@Title, 4)) = 'das ' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 4)) 
			WHEN LOWER(LEFT(@Title, 4)) = 'der ' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 4)) 
			WHEN LOWER(LEFT(@Title, 4)) = 'die ' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 4)) 
			WHEN LOWER(LEFT(@Title, 4)) = 'ein ' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 4)) 
			WHEN LOWER(LEFT(@Title, 4)) = 'las ' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 4)) 
			WHEN LOWER(LEFT(@Title, 4)) = 'les ' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 4)) 
			WHEN LOWER(LEFT(@Title, 4)) = 'los ' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 4)) 
			WHEN LOWER(LEFT(@Title, 4)) = 'the ' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 4)) 
			ELSE @Title
			END,
		PublicationDetails = LEFT(@PublicationPlace + ' ' + @Publisher + ' ' + @Date, 400),
		PublisherName = @Publisher,
		PublisherPlace = @PublicationPlace,
		Volume = @Volume,
		Issue = @Issue,
		Date = @Date,
		PageRange = 
			CASE
			WHEN @StartPage <> '' AND @EndPage <> '' THEN @StartPage + '--' + @EndPage
			WHEN @StartPage <> '' THEN @StartPage
			ELSE @EndPage
			END,
		StartPageNumber = @StartPage,
		EndPageNumber = @EndPage,
		LanguageCode = @LanguageCode,
		Url = @Url,
		RightsStatement = LEFT(ISNULL(@Right, ''), 500)
WHERE	SegmentID = @ProductionSegmentID
AND		LastModifiedUserID = 1

-- Replace the SegmentAuthor records if none have been added/updated by a user
IF NOT EXISTS(	SELECT	SegmentAuthorID
				FROM	dbo.BHLSegmentAuthor
				WHERE	SegmentID = @ProductionSegmentID
				AND		LastModifiedUserID <> 1)
BEGIN
	DELETE FROM dbo.BHLSegmentAuthor WHERE SegmentID = @ProductionSegmentID

	INSERT dbo.BHLSegmentAuthor (SegmentID, AuthorID, SequenceOrder) 
	SELECT	@ProductionSegmentID, ProductionAuthorID, ROW_NUMBER() OVER (ORDER BY OAIRecordCreatorID)
	FROM	dbo.OAIRecordCreator WHERE OAIRecordID = @OAIRecordID
END
				
-- Replace the SegmentKeyword records if none have been added/updated by a user
IF NOT EXISTS(	SELECT	SegmentKeywordID
				FROM	dbo.BHLSegmentKeyword
				WHERE	SegmentID = @ProductionSegmentID
				AND		LastModifiedUserID <> 1)
BEGIN
	DELETE FROM dbo.BHLSegmentKeyword WHERE SegmentID = @ProductionSegmentID

	INSERT dbo.BHLSegmentKeyword (SegmentID, KeywordID)
	SELECT @ProductionSegmentID, ProductionKeywordID FROM dbo.OAIRecordSubject WHERE OAIRecordID = @OAIRecordID
END
								
-- Replace the SegmentIdentifier records if none have been added/updated by a user
IF NOT EXISTS(	SELECT	SegmentIdentifierID
				FROM	dbo.BHLSegmentIdentifier
				WHERE	SegmentID = @ProductionSegmentID
				AND		LastModifiedUserID <> 1)
BEGIN
	DELETE FROM dbo.BHLSegmentIdentifier WHERE SegmentID = @ProductionSegmentID AND IdentifierID <> @IdentifierOAI

	INSERT dbo.BHLSegmentIdentifier (SegmentID, IdentifierID, IdentifierValue)
	SELECT @ProductionSegmentID, @IdentifierISSN, Issn FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Issn <> ''

	INSERT dbo.BHLSegmentIdentifier (SegmentID, IdentifierID, IdentifierValue)
	SELECT @ProductionSegmentID, @IdentifierISBN, Isbn FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Isbn <> ''

	INSERT dbo.BHLSegmentIdentifier (SegmentID, IdentifierID, IdentifierValue)
	SELECT @ProductionSegmentID, @IdentifierLCCN, Lccn FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Lccn <> ''
END
				
-- Replace the DOI record
DELETE FROM dbo.BHLDOI WHERE DOIEntityTypeID = 40 AND EntityID = @ProductionSegmentID

INSERT dbo.BHLDOI (DOIEntityTypeID, EntityID, DOIStatusID, DOIName, StatusDate, IsValid)
SELECT 40, @ProductionSegmentID, 200, Doi, GETDATE(), 1 FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Doi <> ''
				

-- Update the OAIRecordStatus of the just-updated record
UPDATE	OAIRecord
SET		OAIRecordStatusID = 20, LastModifiedDate = GETDATE()
WHERE	OAIRecordID = @OAIRecordID

-- Done with segment updates
COMMIT TRAN

END
