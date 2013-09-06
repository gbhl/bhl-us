
CREATE PROCEDURE [dbo].[TitleEndNoteSelectForTitleID]

@TitleID INT

AS
BEGIN

SET NOCOUNT ON

DECLARE @RedirID int
SELECT @RedirID = RedirectTitleID FROM dbo.Title WHERE TitleID = @TitleID

IF (@RedirID IS NOT NULL)
	exec dbo.TitleEndNoteSelectForTitleID @RedirID
ELSE
	SELECT 	t.TitleID,
			i.ItemID,
			CASE SUBSTRING(t.MarcLeader, 8, 1)
				WHEN 'm' THEN 'Book'	-- monograph
				WHEN 'a' THEN 'Book'	-- monographic part
				WHEN 's' THEN 'Serial'	-- serial
				WHEN 'b' THEN 'Serial'	-- serial part
				WHEN 'c' THEN 'Serial'	-- collection
				ELSE 'Book'
			END AS PublicationType,
			c.Authors,
			CASE 
				WHEN ISNULL(i.Year, '') <> '' THEN i.Year
				WHEN ISNULL(CONVERT(NVARCHAR(20), t.StartYear), '') <> '' THEN CONVERT(NVARCHAR(20), t.StartYear)
				ELSE ISNULL(t.Datafield_260_c, '')
			END AS Year,
			t.FullTitle,
			LTRIM(ISNULL(t.PartNumber, '') + ' ' + ISNULL(PartName, '')) AS SecondaryTitle,
			ISNULL(t.Datafield_260_a, '') AS PublisherPlace,
			ISNULL(t.Datafield_260_b, '') AS PublisherName,
			i.Volume,
			t.ShortTitle,
			abbrev.IdentifierValue AS Abbreviation,
			isbn.IdentifierValue AS ISBN,
			CASE WHEN ISNULL(i.CallNumber, '') = '' THEN t.CallNumber ELSE i.CallNumber END AS CallNumber,
			c.Subjects AS Keywords,
			l.LanguageName,
			'' AS Note,
			d.DOIName AS DOI
	FROM	dbo.Title t LEFT JOIN dbo.Title_Identifier isbn
				ON t.TitleID = isbn.TitleID
				AND isbn.IdentifierID = 3 -- isbn
			LEFT JOIN dbo.Title_Identifier abbrev
				ON t.TitleID = abbrev.TitleID
				AND abbrev.IdentifierID = 6 -- abbrev
			INNER JOIN dbo.TitleItem ti
				ON t.TitleID = ti.TitleID
			INNER JOIN dbo.Item i
				ON ti.ItemID  = i.ItemID
				AND i.ItemStatusID = 40
			LEFT JOIN dbo.Language l
				ON i.LanguageCode = l.LanguageCode
			LEFT JOIN dbo.DOI d
				ON t.TitleID = d.EntityID
				AND d.DOIEntityTypeID = 10 -- Title
			INNER JOIN dbo.SearchCatalog c
				ON t.TitleID = c.TitleID
				AND i.ItemID = c.ItemID
	WHERE	t.PublishReady = 1
	AND		t.TitleID = @TitleID
	ORDER BY ti.ItemSequence

END






