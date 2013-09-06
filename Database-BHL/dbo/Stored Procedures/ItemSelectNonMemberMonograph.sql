
CREATE PROCEDURE [dbo].[ItemSelectNonMemberMonograph]

@SinceDate datetime,
@IsMember int = 0,
@InstitutionCode nvarchar(10) = ''

AS

BEGIN

SET NOCOUNT ON

SELECT	DISTINCT 
		t.TitleID,
		ISNULL(tti.IdentifierValue, '') AS OCLC,
		t.FullTitle,
		--dbo.fnAuthorStringForTitle(t.TitleID) AS Authors,
		c.Authors,
		ISNULL(CONVERT(nvarchar(10), t.StartYear), '') AS StartYear,
		t.CallNumber,
		t.DataField_260_b AS Publisher,
		t.DataField_260_a AS PublisherPlace,
		i.ItemID,
		i.Volume,
		i.IdentifierBib
FROM	dbo.Title t INNER JOIN dbo.Item i ON t.TitleID = i.PrimaryTitleID
		INNER JOIN dbo.TitleItem ti ON t.TitleID = ti.TitleID AND ti.ItemID = i.ItemID
		LEFT JOIN dbo.Institution inst ON i.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.BibliographicLevel b ON t.BibliographicLevelID = b.BibliographicLevelID
		LEFT JOIN dbo.Title_Identifier tti ON t.TitleID = tti.TitleID AND tti.IdentifierID = 1 -- OCLC
		INNER JOIN dbo.Page p ON ti.ItemID = p.ItemID
		INNER JOIN dbo.SearchCatalog c ON i.ItemID = c.ItemID
WHERE	ISNULL(inst.BHLMemberLibrary, 0) = @IsMember
AND		(inst.InstitutionCode = @InstitutionCode OR @InstitutionCode = '')
AND		b.MARCCode IN ('a', 'c', 'm') -- monographs and "collections"
AND		i.CreationDate > @SinceDate
ORDER BY
		t.TitleID

END

