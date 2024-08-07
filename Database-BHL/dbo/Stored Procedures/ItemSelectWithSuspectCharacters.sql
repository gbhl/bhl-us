SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemSelectWithSuspectCharacters]

@InstitutionCode NVARCHAR(10) = '',
@MaxAge INT = 30

AS
BEGIN

SET NOCOUNT ON

-- Item
SELECT	t.TitleID,
		t.ShortTitle,
		b.BookID AS ItemID,
		b.BarCode,
		dbo.fnContributorStringForTitle(t.TitleID, 0) AS InstitutionName,
		i.CreationDate,
		CHAR(dbo.fnContainsSuspectCharacter(b.Volume)) AS VolumeSuspect, 
		b.Volume,
		oclc.IdentifierValue AS OCLC,
		MIN(b.ZQuery) AS ZQuery
FROM	dbo.Item i 
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemTitle ti ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t ON ti.TitleID = t.TitleID
		LEFT JOIN (SELECT * FROM dbo.Title_Identifier WHERE IdentifierID = 1) AS oclc ON t.TitleID = oclc.TitleID
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	dbo.fnContainsSuspectCharacter(b.Volume) > 0
AND		r.InstitutionRoleName = 'Holding Institution'
AND		(ii.InstitutionCode = @InstitutionCode OR @InstitutionCode = '')
AND		i.CreationDate > DATEADD(dd, (@MaxAge * -1), GETDATE())
GROUP BY
		CHAR(dbo.fnContainsSuspectCharacter(b.Volume)), 
		b.Volume,
		t.TitleID,
		t.ShortTitle,
		b.BookID,
		i.CreationDate,
		b.BarCode,
		oclc.IdentifierValue,
		dbo.fnContributorStringForTitle(t.TitleID, 0)
ORDER BY
		InstitutionName, i.CreationDate DESC
END


GO
