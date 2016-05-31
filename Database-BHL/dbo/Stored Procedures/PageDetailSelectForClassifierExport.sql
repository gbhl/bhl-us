CREATE PROCEDURE [dbo].[PageDetailSelectForClassifierExport]

AS

BEGIN

SET NOCOUNT ON

SELECT	pd.PageDetailID,
		p.ItemID,
		i.BarCode,
		p.PageID,
		p.SequenceOrder,
		pd.AbbyyHasImage,
		pd.ContrastHasImage,
		pd.PercentCoverage,
		pd.Height,
		pd.Width,
		pd.PixelDepth,
		pil.[Top],
		pil.Bottom,
		pil.[Left],
		pil.[Right],
		dbo.fnCOinSAuthorStringForTitle(t.titleid, 0) AS Authors,
		t.ShortTitle,
		t.PublicationDetails,
		i.Volume,
		ISNULL(i.CopyrightStatus, '') AS CopyrightStatus,
		p.Year AS PageYear,
		i.Year AS ItemYear,
		t.StartYear,
		dbo.fnGeographicKeywordStringForTitle(t.TitleID) AS Keywords,
		inst.InstitutionName,
		inst.BHLMemberLibrary
FROM	dbo.Page p 
		INNER JOIN dbo.PageDetail pd ON p.PageID = pd.PageID
		LEFT JOIN dbo.PageIllustration pil on pd.PageDetailID = pil.PageDetailID
		INNER JOIN dbo.Item i on p.ItemID = i.ItemID
		INNER JOIN dbo.Title t ON i.PrimaryTitleID = t.TitleID
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.Institution inst on ii.InstitutionCode = inst.InstitutionCode
WHERE	pd.PageDetailStatusID = 10 -- Extracted
AND		p.Active = 1
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
AND		r.InstitutionRoleName = 'Contributor'
AND		(pd.AbbyyHasImage = 1 OR pd.ContrastHasImage = 1)
ORDER BY
		p.ItemID,
		p.SequenceOrder,
		pd.PageDetailID

END
