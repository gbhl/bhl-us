CREATE PROCEDURE [dbo].[ExportItem]

AS

BEGIN

SET NOCOUNT ON

DECLARE @RightsHolderRoleID int
SELECT @RightsHolderRoleID = InstitutionRoleID FROM dbo.InstitutionRole WHERE InstitutionRoleName = 'Rights Holder'

-- Accumulate initial result set
SELECT DISTINCT 
		b.BookID,
		b.ItemID, 
		it.TitleID, 
		c.FirstPageID AS ThumbnailPageID, 
		b.BarCode, 
		b.MARCItemID, 
		b.CallNumber, 
		b.Volume AS VolumeInfo,
		'https://www.biodiversitylibrary.org/item/' + CONVERT(nvarchar(20), i.ItemID) AS ItemURL, 
		b.IdentifierBib AS LocalID, 
		b.StartYear AS Year, 
		c.ItemContributors AS InstitutionName, 
		b.ZQuery,
		b.IsVirtual,
		c.HasLocalContent,
		c.HasExternalContent,
		CONVERT(nvarchar(16), i.CreationDate, 120) AS CreationDate,
		b.LicenseUrl AS LicenseType,
		b.Rights,
		b.CopyrightStatus,
		SPACE(255) AS RightsHolder
INTO	#item
FROM	dbo.Item i
		INNER JOIN dbo.ItemTitle it ON i.ItemID = it.ItemID
		INNER JOIN dbo.Title t ON it.TitleID = t.TitleID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c ON b.BookID = c.ItemID AND t.TitleID = c.TitleID;

-- Add the rights holder
UPDATE	i
SET		RightsHolder = inst.InstitutionName
FROM	#item i
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID 
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = @RightsHolderRoleID
		INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode;

-- Check segments related to any virtual items to determine if they have local/external content
WITH ItemCTE (ItemID, HasLocalContent, HasExternalContent)  
AS  
(  
	SELECT	t.ItemID, MAX(c.HasLocalContent), MAX(c.HasExternalContent)
	FROM	#item t
			INNER JOIN dbo.ItemRelationship ir ON t.ItemID = ir.ParentID
			INNER JOIN dbo.vwSegment s ON ir.ChildID = s.ItemID
			INNER JOIN dbo.SearchCatalogSegment c ON s.SegmentID = c.SegmentID
	WHERE	IsVirtual = 1
	GROUP BY t.ItemID
)  
UPDATE	#item
SET		HasLocalContent = cte.HasLocalContent,
		HasExternalContent = cte.HasExternalcontent
FROM	#item t INNER JOIN ItemCTE cte ON t.ItemID = cte.ItemID;

-- Return final result set
SELECT	BookID AS ItemID,
		TitleID, 
		ThumbnailPageID, 
		BarCode, 
		MARCItemID, 
		CallNumber, 
		VolumeInfo,
		ItemURL, 
		LocalID, 
		[Year], 
		InstitutionName, 
		ZQuery,
		IsVirtual,
		HasLocalContent,
		HasExternalContent,
		CreationDate,
		LicenseType,
		Rights,
		CopyrightStatus,
		LTRIM(RTRIM(RightsHolder)) AS RightsHolder
FROM	#item

END

GO
