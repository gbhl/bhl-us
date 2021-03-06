SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemSelectNonMemberMonograph]

@SinceDate datetime,
@IsMember int = 0,
@InstitutionCode nvarchar(10) = ''

AS

BEGIN

SET NOCOUNT ON

SELECT	b.BookID,
		i.ItemID,
		pt.TitleID AS PrimaryTitleID,
		ii.InstitutionCode,
		c.Authors,
		b.Volume,
		b.IdentifierBib
INTO	#Item
FROM	dbo.Item i 
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.vwItemPrimaryTitle pt ON i.ItemID = pt.ItemID
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.SearchCatalog c ON b.BookID = c.ItemID
WHERE	i.CreationDate > @SinceDate
AND		c.HasLocalContent = 1
AND		r.InstitutionRoleName = 'Holding Institution'

SELECT	t.TitleID,
		ISNULL(tti.IdentifierValue, '') AS OCLC,
		t.FullTitle,
		i.Authors,
		ISNULL(CONVERT(nvarchar(10), t.StartYear), '') AS StartYear,
		t.CallNumber,
		t.DataField_260_b AS Publisher,
		t.DataField_260_a AS PublisherPlace,
		i.BookID AS ItemID,
		i.Volume,
		i.IdentifierBib
FROM	#Item i
		--INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.Title t ON i.PrimaryTitleID = t.TitleID
		INNER JOIN dbo.ItemTitle it ON i.ItemID  = it.ItemID AND t.TitleID = it.TitleID
		INNER JOIN dbo.Institution inst ON i.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.BibliographicLevel bl ON t.BibliographicLevelID = bl.BibliographicLevelID
		LEFT JOIN dbo.Title_Identifier tti ON t.TitleID = tti.TitleID AND tti.IdentifierID = 1 -- OCLC
WHERE	ISNULL(inst.BHLMemberLibrary, 0) = @IsMember
AND		(i.InstitutionCode = @InstitutionCode OR @InstitutionCode = '')
AND		bl.MARCCode IN ('a', 'c', 'm') -- monographs and "collections"
ORDER BY
		t.TitleID

END


GO
