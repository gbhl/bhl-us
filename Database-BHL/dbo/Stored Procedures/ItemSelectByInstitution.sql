﻿CREATE PROCEDURE [dbo].[ItemSelectByInstitution]

@InstitutionCode nvarchar(10),
@ReturnCount int = 100,
@SortBy nvarchar(10) = 'Date',
@InstitutionRoleName nvarchar(100) = 'Holding Institution'

AS 

BEGIN

SET NOCOUNT ON

SELECT TOP (@ReturnCount)
		i.ItemID, 
		i.Barcode, 
		t.FullTitle AS TitleName, 
		t.SortTitle,
		i.Volume, 
		ISNULL(CASE WHEN ISNULL(i.[Year], '') = '' THEN CONVERT(nvarchar(20), t.StartYear) ELSE i.[Year] END, '') AS [Year],
		c.Authors AS CreatorTextString,
		i.CreationDate,
		i.CopyrightStatus,
		i.Rights,
		i.LicenseUrl,
		i.DueDiligence
INTO	#Item
FROM	dbo.Item i  WITH (NOLOCK)
		INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r WITH (NOLOCK) ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.Title t  WITH (NOLOCK)ON i.PrimaryTitleID = t.TitleID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID
WHERE	ii.InstitutionCode = @InstitutionCode
AND		(r.InstitutionRoleName = @InstitutionRoleName OR @InstitutionRoleName IS NULL)
ORDER BY
		i.CreationDate DESC

IF (@SortBy = 'Title')
BEGIN
	SELECT	ItemID, BarCode, TitleName, Volume, [Year], CreatorTextString, CreationDate,
			CopyrightStatus, Rights, LicenseUrl, DueDiligence
	FROM	#Item 
	ORDER BY SortTitle, Volume
END
ELSE
BEGIN
	SELECT	ItemID, BarCode, TitleName, Volume, [Year], CreatorTextString, CreationDate,
			CopyrightStatus, Rights, LicenseUrl, DueDiligence
	FROM	#Item 
	ORDER BY CreationDate DESC
END

END
