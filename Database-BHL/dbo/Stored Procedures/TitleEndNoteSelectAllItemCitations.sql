
CREATE PROCEDURE [dbo].[TitleEndNoteSelectAllItemCitations]
AS
BEGIN

SET NOCOUNT ON

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
		ISNULL(i.Note, '') + CASE WHEN ISNULL(i.Note, '') = '' THEN dbo.fnNoteStringForTitle(t.TitleID, '') ELSE dbo.fnNoteStringForTitle(t.TitleID, ' --- ') END AS Note
FROM	dbo.Title t WITH (NOLOCK)
		LEFT JOIN dbo.Title_Identifier isbn WITH (NOLOCK)
			ON t.TitleID = isbn.TitleID
			AND isbn.IdentifierID = 3 -- isbn
		LEFT JOIN dbo.Title_Identifier abbrev WITH (NOLOCK)
			ON t.TitleID = abbrev.TitleID
			AND abbrev.IdentifierID = 6 -- abbrev
		INNER JOIN dbo.Item i WITH (NOLOCK)
			ON t.TitleID = i.PrimaryTitleID
			AND i.ItemStatusID = 40
		LEFT JOIN dbo.Language l WITH (NOLOCK)
			ON i.LanguageCode = l.LanguageCode
		INNER JOIN dbo.SearchCatalog c  WITH (NOLOCK)
			ON t.TitleID = c.TitleID
			AND i.ItemID = c.ItemID
WHERE	PublishReady = 1

END

