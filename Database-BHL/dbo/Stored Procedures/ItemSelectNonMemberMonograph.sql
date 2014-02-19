CREATE PROCEDURE [dbo].[ItemSelectNonMemberMonograph]

@SinceDate datetime,
@IsMember int = 0,
@InstitutionCode nvarchar(10) = ''

AS

BEGIN

SET NOCOUNT ON

SELECT	i.ItemID,
		i.PrimaryTitleID,
		i.InstitutionCode,
		i.Volume,
		i.IdentifierBib,
		c.Authors
INTO	#Item
FROM	dbo.Item i INNER JOIN dbo.SearchCatalog c ON i.ItemID = c.ItemID
WHERE	i.CreationDate > @SinceDate
AND		c.HasLocalContent = 1

SELECT	t.TitleID,
		ISNULL(tti.IdentifierValue, '') AS OCLC,
		t.FullTitle,
		i.Authors,
		ISNULL(CONVERT(nvarchar(10), t.StartYear), '') AS StartYear,
		t.CallNumber,
		t.DataField_260_b AS Publisher,
		t.DataField_260_a AS PublisherPlace,
		i.ItemID,
		i.Volume,
		i.IdentifierBib
FROM	#Item i
		INNER JOIN dbo.Title t ON i.PrimaryTitleID = t.TitleID
		INNER JOIN dbo.TitleItem ti ON t.TitleID = ti.TitleID AND ti.ItemID = i.ItemID
		LEFT JOIN dbo.Institution inst ON i.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.BibliographicLevel b ON t.BibliographicLevelID = b.BibliographicLevelID
		LEFT JOIN dbo.Title_Identifier tti ON t.TitleID = tti.TitleID AND tti.IdentifierID = 1 -- OCLC
WHERE	ISNULL(inst.BHLMemberLibrary, 0) = @IsMember
AND		(inst.InstitutionCode = @InstitutionCode OR @InstitutionCode = '')
AND		b.MARCCode IN ('a', 'c', 'm') -- monographs and "collections"
ORDER BY
		t.TitleID

END
