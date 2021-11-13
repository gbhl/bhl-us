CREATE PROCEDURE [dbo].[OAIRecordPublishInsertTitle]

@OAIRecordID int,
@ProductionTitleID int OUTPUT

AS

BEGIN

SET NOCOUNT ON

DECLARE @IdentifierOAI int
DECLARE @IdentifierISSN int
DECLARE @IdentifierISBN int
DECLARE @IdentifierLCCN int
DECLARE @UniformTitle nvarchar(255)

SELECT @IdentifierOAI = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'OAI'
SELECT @IdentifierISSN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'ISSN'
SELECT @IdentifierISBN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'ISBN'
SELECT @IdentifierLCCN = IdentifierID FROM dbo.BHLIdentifier WHERE IdentifierName = 'DLC'

-- Get the uniform title, if one exists
SET @UniformTitle = ''
SELECT	@UniformTitle = LEFT(rt.Title, 255) 
FROM	dbo.OAIRecordRelatedTitle rt 
WHERE	rt.TitleType = 'uniform' AND rt.OAIRecordID = @OAIRecordID

BEGIN TRAN

-- Insert a new Title record
INSERT	dbo.BHLTitle
		(
		MarcBibID,
		FullTitle,
		ShortTitle,
		SortTitle,
		UniformTitle,
		CallNumber,
		PublicationDetails,
		StartYear,
		EndYear,
		Datafield_260_a,
		Datafield_260_b,
		Datafield_260_c,
		LanguageCode,
		PublishReady,
		RareBooks,
		EditionStatement,
		BibliographicLevelID
		)
SELECT	r.BHLInstitutionCode + REPLACE(REPLACE(o.OAIIdentifier, ':', ''), '/', '') AS MarcBibID,
		Title AS FullTitle,
		LEFT(Title, 255) AS ShortTitle,
		dbo.fnGetSortString (
			LEFT(CASE
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
				END, 60) 
		) AS SortTitle,
		@UniformTitle,
		CallNumber,
		LEFT(o.PublicationPlace + ' ' + o.Publisher + ' ' + o.PublicationDate, 255) AS PublicationDetails,
		CONVERT(int, 
			CASE WHEN ISNUMERIC(LEFT([Date], 4)) = 1 THEN 
				CASE WHEN CONVERT(int, LEFT([Date], 4)) BETWEEN 1400 AND 2025 THEN
					LEFT([Date], 4) 
				ELSE
					NULL
				END
			ELSE 
				NULL 
			END) AS StartYear,
		CONVERT(int, 
			CASE WHEN LEN([Date]) >= 9 THEN
				CASE WHEN ISNUMERIC(SUBSTRING([Date], 6, 4)) = 1 THEN 
					CASE WHEN CONVERT(int, SUBSTRING([Date], 6, 4)) BETWEEN 1400 AND 2025 THEN
						SUBSTRING([Date], 6, 4) 
					ELSE 
						NULL
					END
				ELSE 
					NULL 
				END
			ELSE
				NULL
			END) AS EndYear,
		o.PublicationPlace AS Datafield_260_a,
		o.Publisher AS Datafield_260_b,
		[Date] AS Datafield_260_c,
		l.BHLLanguageCode AS LanguageCode,
		1 AS PublishReady,
		0 AS RareBooks,
		o.Edition AS EditionStatement,
		4 AS BibliographicLevelID					
FROM	dbo.OAIRecord o
		LEFT JOIN dbo.OAIRecordLanguage l ON o.Language = l.OAILanguage
		INNER JOIN dbo.OAIHarvestLog lg ON o.HarvestLogID = lg.HarvestLogID
		INNER JOIN dbo.OAIHarvestSet s ON lg.HarvestSetID = s.HarvestSetID
		INNER JOIN dbo.OAIRepositoryFormat rf ON s.RepositoryFormatID = rf.RepositoryFormatID
		INNER JOIN dbo.OAIRepository r ON rf.RepositoryID = r.RepositoryID
WHERE	o.OAIRecordID = @OAIRecordID

-- Preserve the production identifier for the new title
SET @ProductionTitleID = SCOPE_IDENTITY()

-- Insert TitleIdentifier records
INSERT dbo.BHLTitle_Identifier (TitleID, IdentifierID, IdentifierValue)
SELECT @ProductionTitleID, @IdentifierOAI, OAIIdentifier FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID

INSERT dbo.BHLTitle_Identifier (TitleID, IdentifierID, IdentifierValue)
SELECT @ProductionTitleID, @IdentifierISSN, Issn FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Issn <> ''

INSERT dbo.BHLTitle_Identifier (TitleID, IdentifierID, IdentifierValue)
SELECT @ProductionTitleID, @IdentifierISBN, Isbn FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Isbn <> ''

INSERT dbo.BHLTitle_Identifier (TitleID, IdentifierID, IdentifierValue)
SELECT @ProductionTitleID, @IdentifierLCCN, Lccn FROM dbo.OAIRecord WHERE OAIRecordID = @OAIRecordID AND Lccn <> ''
			
-- Insert DOI record
DECLARE @DOI nvarchar(50)
SELECT	@DOI = Doi
FROM	dbo.OAIRecord
WHERE	OAIRecordID = @OAIRecordID AND Doi <> '' 

exec dbo.BHLDOIInsert @DOIEntityTypeID = 10, @EntityID = @ProductionTitleID, @DOIStatusID = 200, @DOIName = @DOI, @IsValid = 1, @ExcludeBHLDOI = 1

-- Insert TitleKeyword records
INSERT dbo.BHLTitleKeyword (TitleID, KeywordID)
SELECT @ProductionTitleID, ProductionKeywordID FROM dbo.OAIRecordSubject WHERE OAIRecordID = @OAIRecordID

-- Insert TitleAuthor records
INSERT	dbo.BHLTitleAuthor (TitleID, AuthorID, AuthorRoleID, SequenceOrder) 
SELECT	@ProductionTitleID, ProductionAuthorID, 0, ROW_NUMBER() OVER (ORDER BY OAIRecordCreatorID)
FROM	dbo.OAIRecordCreator WHERE OAIRecordID = @OAIRecordID

-- Insert TitleAssociation records
INSERT	dbo.BHLTitleAssociation (TitleID, TitleAssociationTypeID, Title)
SELECT	@ProductionTitleID, a.BHLTitleAssociationTypeID, r.Title
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

-- Insert TitleVariant records
INSERT	dbo.BHLTitleVariant (TitleID, TitleVariantTypeID, Title, CreationUserID, LastModifiedUserID)
SELECT	@ProductionTitleID, a.BHLTitleVariantTypeID, r.Title, 1, 1
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

COMMIT TRAN

END

GO
