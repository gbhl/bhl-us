SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PageDetailSelectForClassifierExport]

AS

BEGIN

SET NOCOUNT ON

SELECT	pd.PageDetailID,
		b.BookID AS ItemID,
		b.BarCode,
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
		b.Volume,
		ISNULL(b.CopyrightStatus, '') AS CopyrightStatus,
		p.Year AS PageYear,
		b.StartYear AS ItemYear,
		t.StartYear,
		dbo.fnGeographicKeywordStringForTitle(t.TitleID) AS Keywords,
		inst.InstitutionName,
		inst.BHLMemberLibrary
FROM	dbo.Page p 
		INNER JOIN dbo.PageDetail pd ON p.PageID = pd.PageID
		LEFT JOIN dbo.PageIllustration pil on pd.PageDetailID = pil.PageDetailID
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		INNER JOIN dbo.Item i on ip.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID AND it.IsPrimary = 1
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.Institution inst on ii.InstitutionCode = inst.InstitutionCode
WHERE	pd.PageDetailStatusID = 10 -- Extracted
AND		p.Active = 1
AND		i.ItemStatusID = 40
AND		t.PublishReady = 1
AND		r.InstitutionRoleName = 'Holding Institution'
AND		(pd.AbbyyHasImage = 1 OR pd.ContrastHasImage = 1)
ORDER BY
		b.BookID,
		ip.SequenceOrder,
		pd.PageDetailID

END


GO
