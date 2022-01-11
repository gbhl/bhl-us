CREATE PROCEDURE [dbo].[ApiTitleSelectByIdentifier]

@IdentifierType nvarchar(40),
@IdentifierValue nvarchar(125)

AS 

BEGIN

SET NOCOUNT ON

DECLARE @IdentifierIDDOI int
SELECT @IdentifierIDDOI = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI';

SELECT	COALESCE(t10.TitleID, t9.TitleID, t8.TitleiD, t7.TitleID, t6.TitleID, 
			t5.TitleID, t4.TitleID, t3.TitleID, t2.TitleID, t1.TitleID) AS TitleID
INTO	#Title
FROM	dbo.Title t1 
		INNER JOIN dbo.Title_Identifier ti ON t1.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier i ON ti.IdentifierID = i.IdentifierID
		LEFT JOIN dbo.Title t2 ON t1.RedirectTitleID = t2.TitleID
		LEFT JOIN dbo.Title t3 ON t2.RedirectTitleID = t3.TitleID
		LEFT JOIN dbo.Title t4 ON t3.RedirectTitleID = t4.TitleID
		LEFT JOIN dbo.Title t5 ON t4.RedirectTitleID = t5.TitleID
		LEFT JOIN dbo.Title t6 ON t5.RedirectTitleID = t6.TitleID
		LEFT JOIN dbo.Title t7 ON t6.RedirectTitleID = t7.TitleID
		LEFT JOIN dbo.Title t8 ON t7.RedirectTitleID = t8.TitleID
		LEFT JOIN dbo.Title t9 ON t8.RedirectTitleID = t9.TitleID
		LEFT JOIN dbo.Title t10 ON t9.RedirectTitleID = t10.TitleID
WHERE	i.IdentifierType = @IdentifierType
AND		ti.IdentifierValue = @IdentifierValue

SELECT	t.TitleID,
		ISNULL(b.BibliographicLevelName, '') AS BibliographicLevelName,
		ISNULL(m.MaterialTypeLabel, '') AS MaterialTypeLabel,
		t.MARCBibID,
		t.MARCLeader,
		t.TropicosTitleID,
		t.RedirectTitleID,
		t.FullTitle,
		t.ShortTitle,
		t.UniformTitle,
		t.SortTitle,
		t.PartNumber,
		t.PartName,
		t.CallNumber,
		t.PublicationDetails,
		t.StartYear,
		t.EndYear,
		t.Datafield_260_a,
		t.Datafield_260_b,
		t.Datafield_260_c,
		t.LanguageCode,
		t.TitleDescription,
		t.TL2Author,
		t.PublishReady,
		t.RareBooks,
		t.Note,
		t.CreationDate,
		t.LastModifiedDate,
		t.CreationUserID,
		t.LastModifiedUserID,
		t.OriginalCatalogingSource,
		t.EditionStatement,
		t.CurrentPublicationFrequency,
		ti.IdentifierValue AS DOIName
FROM	dbo.Title t 
		INNER JOIN #Title tmp on t.TitleID = tmp.TitleID
		LEFT JOIN dbo.BibliographicLevel b ON t.BibliographicLevelID = b.BibliographicLevelID
		LEFT JOIN dbo.MaterialType m ON t.MaterialTypeID = m.MaterialTypeID
		LEFT JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID AND ti.IdentifierID = @IdentifierIDDOI
WHERE	t.PublishReady = 1

END

GO
