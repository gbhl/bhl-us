CREATE PROCEDURE [dbo].[OAIRecordPublishUpdateSegment]

@OAIRecordID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @ProductionSegmentID int
DECLARE @ProductionItemID int
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
DECLARE @IdentifierISBN int
DECLARE @IdentifierLCCN int
DECLARE @Right nvarchar(500)

SELECT @IdentifierOAI = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'OAI'
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

SELECT	@ProductionItemID = ItemID
FROM	dbo.BHLSegment
WHERE	SegmentID = @ProductionSegmentID

-- Start a new transaction while we update production
BEGIN TRAN

UPDATE	dbo.BHLSegment
SET		Title = @Title,
		ContainerTitle = @ContainerTitle,
		SortTitle = 
			dbo.fnGetSortString(
				CASE
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
				END
			),
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

-- Replace the ItemAuthor records if none have been added/updated by a user
IF NOT EXISTS(	SELECT	ItemAuthorID
				FROM	dbo.BHLItemAuthor ia
				WHERE	ia.ItemID = @ProductionItemID
				AND		ia.LastModifiedUserID <> 1)
BEGIN
	DELETE dbo.BHLItemAuthor WHERE ItemID = @ProductionItemID

	INSERT dbo.BHLItemAuthor (ItemID, AuthorID, SequenceOrder) 
	SELECT	@ProductionItemID, ProductionAuthorID, ROW_NUMBER() OVER (ORDER BY OAIRecordCreatorID)
	FROM	dbo.OAIRecordCreator WHERE OAIRecordID = @OAIRecordID
END
				
-- Replace the ItemKeyword records if none have been added/updated by a user
IF NOT EXISTS(	SELECT	ItemKeywordID
				FROM	dbo.BHLItemKeyword
				WHERE	ItemID = @ProductionItemID
				AND		LastModifiedUserID <> 1)
BEGIN
	DELETE FROM dbo.BHLItemKeyword WHERE ItemID = @ProductionItemID

	INSERT dbo.BHLItemKeyword (ItemID, KeywordID)
	SELECT @ProductionItemID, ProductionKeywordID FROM dbo.OAIRecordSubject WHERE OAIRecordID = @OAIRecordID
END
								
-- Replace the ItemIdentifier records if none have been added/updated by a user
IF NOT EXISTS(	SELECT	ItemIdentifierID
				FROM	dbo.BHLItemIdentifier
				WHERE	ItemID = @ProductionItemID
				AND		LastModifiedUserID <> 1)
BEGIN
	DELETE FROM dbo.BHLItemIdentifier WHERE ItemID = @ProductionItemID AND IdentifierID <> @IdentifierOAI

	INSERT dbo.BHLItemIdentifier (ItemID, IdentifierID, IdentifierValue)
	SELECT @ProductionItemID, dbo.BHLfnGetISSNID(Issn), dbo.BHLfnGetISSNValue(Issn) FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Issn <> ''

	INSERT dbo.BHLItemIdentifier (ItemID, IdentifierID, IdentifierValue)
	SELECT @ProductionItemID, @IdentifierISBN, Isbn FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Isbn <> ''

	INSERT dbo.BHLItemIdentifier (ItemID, IdentifierID, IdentifierValue)
	SELECT @ProductionItemID, @IdentifierLCCN, Lccn FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Lccn <> ''
END
				
-- Replace the DOI record
DECLARE @DOI nvarchar(50)
SELECT	@DOI = Doi 
FROM	dbo.OAIRecord
WHERE	OAIRecordID = @OAIRecordID AND Doi <> ''

exec dbo.BHLDOIUpdate @DOIEntityTypeID = 40, @EntityID = @ProductionSegmentID, @DOIStatusID = 200, @DOIName = @DOI, @IsValid = 1, @ExcludeBHLDOI = 1

-- Update the OAIRecordStatus of the just-updated record
UPDATE	OAIRecord
SET		OAIRecordStatusID = 20, LastModifiedDate = GETDATE()
WHERE	OAIRecordID = @OAIRecordID

-- Done with segment updates
COMMIT TRAN

END

GO
