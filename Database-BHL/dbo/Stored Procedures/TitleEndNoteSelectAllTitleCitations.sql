CREATE PROCEDURE [dbo].[TitleEndNoteSelectAllTitleCitations]
AS
BEGIN

SET NOCOUNT ON

SELECT 	DISTINCT
		t.TitleID,
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
			WHEN ISNULL(t.Datafield_260_c, '') = '' 
			THEN ISNULL(CONVERT(NVARCHAR(20), t.StartYear), '') 
			ELSE ISNULL(t.DataField_260_c, '') 
		END AS Year,
		t.FullTitle,
		ISNULL(t.Datafield_260_a, '') AS PublisherPlace,
		ISNULL(t.Datafield_260_b, '') AS PublisherName,
		t.ShortTitle,
		abbrev.IdentifierValue AS Abbreviation,
		isbn.IdentifierValue AS ISBN,
		t.CallNumber,
		c.Subjects AS Keywords,
		l.LanguageName,
		ISNULL(t.Note, '') + CASE WHEN ISNULL(t.Note, '') = '' THEN dbo.fnNoteStringForTitle(t.TitleID, '') ELSE dbo.fnNoteStringForTitle(t.TitleID, ' --- ') END AS Note,
		t.EditionStatement,
		d.DOIName AS DOI
INTO	#EndNote
FROM	dbo.Title t WITH (NOLOCK) 
		LEFT JOIN dbo.Title_Identifier isbn WITH (NOLOCK)
			ON t.TitleID = isbn.TitleID
			AND isbn.IdentifierID = 3 -- isbn
		LEFT JOIN dbo.Title_Identifier abbrev WITH (NOLOCK)
			ON t.TitleID = abbrev.TitleID
			AND abbrev.IdentifierID = 6 -- abbrev
		LEFT JOIN dbo.Language l WITH (NOLOCK)
			ON t.LanguageCode = l.LanguageCode
		LEFT JOIN dbo.DOI d WITH (NOLOCK)
			ON t.TitleID = d.EntityID
			AND d.DOIEntityTypeID = 10 -- Title
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK)
			ON t.TitleID = c.TitleID
WHERE	PublishReady = 1

SELECT 	TitleID,
		PublicationType,
		Authors,
		[Year],
		FullTitle,
		PublisherPlace,
		PublisherName,
		ShortTitle,
		MIN(Abbreviation) AS Abbreviation,
		MIN(ISBN) AS ISBN,
		CallNumber,
		Keywords,
		LanguageName,
		Note,
		EditionStatement,
		DOI
FROM	#EndNote
GROUP BY
		TitleID,
		PublicationType,
		Authors,
		[Year],
		FullTitle,
		PublisherPlace,
		PublisherName,
		ShortTitle,
		CallNumber,
		Keywords,
		LanguageName,
		Note,
		EditionStatement,
		DOI

END
