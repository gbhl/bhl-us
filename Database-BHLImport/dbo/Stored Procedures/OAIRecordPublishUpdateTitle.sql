﻿CREATE PROCEDURE [dbo].[OAIRecordPublishUpdateTitle]

@OAIRecordID int,
@ProductionTitleID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @IdentifierOAI int
DECLARE @IdentifierISBN int
DECLARE @IdentifierLCCN int
DECLARE @UniformTitle nvarchar(255)

SELECT @IdentifierOAI = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'OAI'
SELECT @IdentifierISBN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'ISBN'
SELECT @IdentifierLCCN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'DLC'

-- Get the uniform title, if one exists
SET @UniformTitle = ''
SELECT	@UniformTitle = LEFT(rt.Title, 255) 
FROM	dbo.OAIRecordRelatedTitle rt 
WHERE	rt.TitleType = 'uniform' AND rt.OAIRecordID = @OAIRecordID


-- Only update title information if it came from this OAI source (otherwise leave it alone)
IF EXISTS(	SELECT	TitleID 
			FROM	dbo.BHLTitle_Identifier
			WHERE	IdentifierValue IN (SELECT OAIIdentifier FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID)
			AND		IdentifierID = @IdentifierOAI
			AND		TitleID = @ProductionTitleID	)
BEGIN
	-- Only perform updates if no user has modified the records (LastModifiedUserID = 1) 

	-- Update title
	DECLARE @Title nvarchar(2000)
	DECLARE @CallNumber nvarchar(100)
	DECLARE @Date nvarchar(20)
	DECLARE @PublicationPlace nvarchar(150)
	DECLARE @Publisher nvarchar(250)
	DECLARE @PublicationDate nvarchar(100)
	DECLARE @LanguageCode nvarchar(10)
	DECLARE @EditionStatement nvarchar(450)

	SELECT	@Title = o.Title,
			@CallNumber = o.CallNumber,
			@Date = o.[Date],
			@PublicationPlace = o.PublicationPlace,
			@Publisher = o.Publisher,
			@PublicationDate = CASE WHEN o.PublicationDate = '' THEN o.Date ELSE o.PublicationDate END,
			@LanguageCode = l.BHLLanguageCode,
			@EditionStatement = o.Edition
	FROM	dbo.OAIRecord o LEFT JOIN dbo.OAIRecordLanguage l ON o.Language = l.OAILanguage
	WHERE	o.OAIRecordID = @OAIRecordID

	-- Start a new transaction while we update production
	BEGIN TRAN

	UPDATE	dbo.BHLTitle
	SET		FullTitle = dbo.BHLfnRemoveTrailingPunctuation(@Title, DEFAULT),
			ShortTitle = dbo.BHLfnRemoveTrailingPunctuation(LEFT(@Title, 255), DEFAULT),
			SortTitle = 
				dbo.fnGetSortString(
				LEFT(
					CASE
						WHEN LEFT(@Title, 1) = '"' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 1))
						WHEN LEFT(@Title, 1) = '''' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 1))
						WHEN LEFT(@Title, 1) = '[' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 1)) 
						WHEN LEFT(@Title, 1) = '(' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 1))
						WHEN LEFT(@Title, 1) = '|' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 1))
						WHEN LOWER(LEFT(@Title, 2)) = 'a ' AND @Title <> 'a' THEN LTRIM(RIGHT(@Title, LEN(@Title) - 2)) 
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
					END, 60)
				),
			UniformTitle = dbo.BHLfnRemoveTrailingPunctuation(@UniformTitle, DEFAULT),
			CallNumber = @CallNumber,
			PublicationDetails = dbo.BHLfnRemoveTrailingPunctuation(LEFT(
				ISNULL(@PublicationPlace, '') + CASE WHEN LEN(@PublicationPlace) > 0 THEN ', ' ELSE '' END + 
				ISNULL(@Publisher, '') + CASE WHEN LEN(@Publisher) > 0 THEN ', ' ELSE '' END + 
				ISNULL(@PublicationDate, '')
				, 255), DEFAULT),
			StartYear =	CONVERT(int, CASE WHEN ISNUMERIC(LEFT(@Date, 4)) = 1 THEN 
										CASE WHEN CONVERT(int, LEFT(@Date, 4)) BETWEEN 1400 AND 2025 THEN
											LEFT(@Date, 4) 
										ELSE
											NULL
										END
									ELSE 
										NULL 
									END),
			EndYear = CONVERT(int,  CASE WHEN LEN(@Date) >= 9 THEN
										CASE WHEN ISNUMERIC(SUBSTRING(@Date, 6, 4)) = 1 THEN 
											CASE WHEN CONVERT(int, SUBSTRING(@Date, 6, 4)) BETWEEN 1400 AND 2025 THEN
												SUBSTRING(@Date, 6, 4) 
											ELSE 
												NULL
											END
										ELSE 
											NULL 
										END
									ELSE
										NULL
									END),
			Datafield_260_a = dbo.BHLfnRemoveTrailingPunctuation(@PublicationPlace, DEFAULT),
			Datafield_260_b = dbo.BHLfnRemoveTrailingPunctuation(@Publisher, DEFAULT),
			-- As this field may contain date range values (ex. "1990-"), don't remove trailing hyphens when cleaning punctuation
			Datafield_260_c = dbo.BHLfnRemoveTrailingPunctuation(@PublicationDate, '[a-zA-Z0-9)\]?!>*%"''-]%'),
			LanguageCode = @LanguageCode,
			EditionStatement = @EditionStatement
	WHERE	TitleID = @ProductionTitleID
	AND		LastModifiedUserID = 1

	-- Replace the TitleAuthor records if none have been added/updated by a user
	IF NOT EXISTS(	SELECT	TitleAuthorID
					FROM	dbo.BHLTitleAuthor
					WHERE	TitleID = @ProductionTitleID
					AND		LastModifiedUserID <> 1)
	BEGIN
		DELETE FROM dbo.BHLTitleAuthor WHERE TitleID = @ProductionTitleID

		INSERT	dbo.BHLTitleAuthor (TitleID, AuthorID, AuthorRoleID, SequenceOrder) 
		SELECT	@ProductionTitleID, ProductionAuthorID, 0, ROW_NUMBER() OVER (ORDER BY OAIRecordCreatorID)
		FROM	dbo.OAIRecordCreator WHERE OAIRecordID = @OAIRecordID

	END
				
	-- Replace the TitleKeyword records if none have been added/updated by a user
	IF NOT EXISTS(	SELECT	TitleKeywordID
					FROM	dbo.BHLTitleKeyword
					WHERE	TitleID = @ProductionTitleID
					AND		LastModifiedUserID <> 1)
	BEGIN
		DELETE FROM dbo.BHLTitleKeyword WHERE TitleID = @ProductionTitleID

		INSERT dbo.BHLTitleKeyword (TitleID, KeywordID)
		SELECT @ProductionTitleID, ProductionKeywordID FROM dbo.OAIRecordSubject WHERE OAIRecordID = @OAIRecordID
	END
								
	-- Replace the TitleIdentifier records if none have been added/updated by a user
	IF NOT EXISTS(	SELECT	TitleIdentifierID
					FROM	dbo.BHLTitle_Identifier
					WHERE	TitleID = @ProductionTitleID
					AND		LastModifiedUserID <> 1)
	BEGIN
		DELETE FROM dbo.BHLTitle_Identifier WHERE TitleID = @ProductionTitleID AND IdentifierID <> @IdentifierOAI

		INSERT dbo.BHLTitle_Identifier (TitleID, IdentifierID, IdentifierValue)
		SELECT @ProductionTitleID, dbo.BHLfnGetISSNID(Issn), dbo.BHLfnGetISSNValue(Issn) FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Issn <> ''

		INSERT dbo.BHLTitle_Identifier (TitleID, IdentifierID, IdentifierValue)
		SELECT @ProductionTitleID, @IdentifierISBN, Isbn FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Isbn <> ''

		INSERT dbo.BHLTitle_Identifier (TitleID, IdentifierID, IdentifierValue)
		SELECT @ProductionTitleID, @IdentifierLCCN, Lccn FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Lccn <> ''
	END
				
	-- Replace the DOI record
	DECLARE @DOI nvarchar(50)
	SELECT	@DOI = Doi 
	FROM	dbo.OAIRecord
	WHERE	OAIRecordID = @OAIRecordID AND Doi <> ''

	exec dbo.BHLDOIUpdate @DOIEntityTypeID = 10, @EntityID = @ProductionTitleID, @DOIStatusID = 200, @DOIName = @DOI, @IsValid = 1, @ExcludeBHLDOI = 1
				
	-- Replace the TitleAssociation records if none have been added/updated by a user
	IF NOT EXISTS(	SELECT	TitleAssociationID
					FROM	dbo.BHLTitleAssociation
					WHERE	TitleID = @ProductionTitleID
					AND		LastModifiedUserID <> 1)
	BEGIN
		DELETE FROM dbo.BHLTitleAssociation WHERE TitleID = @ProductionTitleID

		INSERT	dbo.BHLTitleAssociation (TitleID, TitleAssociationTypeID, Title)
		SELECT	@ProductionTitleID, a.BHLTitleAssociationTypeID, dbo.BHLfnRemoveTrailingPunctuation(r.Title, DEFAULT)
		FROM	dbo.OAIRecordRelatedTitle r INNER JOIN dbo.OAIRecordRelatedTitleTypeAssociation a ON r.TitleType = a.TitleType
		WHERE	r.OAIRecordID = @OAIRecordID

		UPDATE	dbo.OAIRecordRelatedTitle
		SET		ProductionEntityType = 'Association',
				ProductionEntityID = ta.TitleAssociationID
		FROM	dbo.OAIRecordRelatedTitle rt 
				INNER JOIN dbo.OAIRecordRelatedTitleTypeAssociation a ON rt.TitleType = a.TitleType			
				INNER JOIN dbo.BHLTitleAssociation ta 
					ON a.BHLTitleAssociationTypeID = ta.TitleAssociationTypeID AND rt.Title = ta.Title
		WHERE	ta.TitleID = @ProductionTitleID
		AND		rt.OAIRecordID = @OAIRecordID
	END			
			
	-- Replace the TitleVariant records if none have been added/updated by a user
	IF NOT EXISTS(	SELECT	TitleVariantID
					FROM	dbo.BHLTitleVariant
					WHERE	TitleID = @ProductionTitleID
					AND		LastModifiedUserID <> 1)
	BEGIN
		DELETE FROM dbo.BHLTitleVariant WHERE TitleID = @ProductionTitleID

		INSERT	dbo.BHLTitleVariant (TitleID, TitleVariantTypeID, Title, CreationUserID, LastModifiedUserID)
		SELECT	@ProductionTitleID, a.BHLTitleVariantTypeID, dbo.BHLfnRemoveTrailingPunctuation(r.Title, DEFAULT), 1, 1
		FROM	dbo.OAIRecordRelatedTitle r INNER JOIN dbo.OAIRecordRelatedTitleTypeVariant a ON r.TitleType = a.TitleType
		WHERE	r.OAIRecordID = @OAIRecordID

		UPDATE	dbo.OAIRecordRelatedTitle
		SET		ProductionEntityType = 'Variant',
				ProductionEntityID = tv.TitleVariantID
		FROM	dbo.OAIRecordRelatedTitle rt 
				INNER JOIN dbo.OAIRecordRelatedTitleTypeVariant v ON rt.TitleType = v.TitleType
				INNER JOIN dbo.BHLTitleVariant tv 
					ON v.BHLTitleVariantTypeID = tv.TitleVariantTypeID AND rt.Title = tv.Title COLLATE SQL_Latin1_General_CP1_CI_AI 
		WHERE	tv.TitleID = @ProductionTitleID
		AND		rt.OAIRecordID = @OAIRecordID
	END

	COMMIT TRAN
END

END 

GO
