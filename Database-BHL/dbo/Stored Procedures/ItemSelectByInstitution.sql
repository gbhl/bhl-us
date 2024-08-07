SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemSelectByInstitution]

@InstitutionCode nvarchar(10),
@ReturnCount int = 100,
@SortBy nvarchar(10) = 'Date',
@InstitutionRoleName nvarchar(100) = 'Holding Institution'

AS 

BEGIN

SET NOCOUNT ON

SELECT TOP (@ReturnCount)
		b.BookID AS ItemID, 
		b.Barcode, 
		t.FullTitle AS TitleName, 
		t.SortTitle,
		b.Volume, 
		ISNULL(CASE WHEN ISNULL(b.[StartYear], '') = '' THEN CONVERT(nvarchar(20), t.StartYear) ELSE b.[StartYear] END, '') AS [Year],
		c.Authors AS CreatorTextString,
		i.CreationDate,
		b.CopyrightStatus,
		b.Rights,
		b.LicenseUrl,
		b.DueDiligence
INTO	#Item
FROM	dbo.Item i  WITH (NOLOCK)
		INNER JOIN dbo.Book b WITH (NOLOCK) ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r WITH (NOLOCK) ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.ItemTitle it WITH (NOLOCK) ON i.ItemID = it.ItemID
		INNER JOIN dbo.Title t  WITH (NOLOCK)ON it.TitleID = t.TitleID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
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


GO
