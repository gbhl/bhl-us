CREATE PROCEDURE [dbo].[ItemSelectWithSuspectCharacters]

@InstitutionCode NVARCHAR(10) = '',
@MaxAge INT = 30

AS
BEGIN

SET NOCOUNT ON

-- Item
SELECT	t.TitleID,
		t.ShortTitle,
		i.ItemID,
		i.BarCode,
		dbo.fnContributorStringForTitle(t.TitleID, 0) AS InstitutionName,
		i.CreationDate,
		CHAR(dbo.fnContainsSuspectCharacter(i.Volume)) AS VolumeSuspect, 
		i.Volume,
		oclc.IdentifierValue AS OCLC,
		MIN(i.ZQuery) AS ZQuery
FROM	dbo.Item i INNER JOIN dbo.TitleItem ti
			ON i.ItemID = ti.ItemID
		INNER JOIN dbo.Title t
			ON ti.TitleID = t.TitleID
		LEFT JOIN (SELECT * FROM dbo.Title_Identifier WHERE IdentifierID = 1) AS oclc
			ON t.TitleID = oclc.TitleID
		INNER JOIN dbo.ItemInstitution ii
			ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r
			ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	dbo.fnContainsSuspectCharacter(i.Volume) > 0
AND		r.InstitutionRoleName = 'Contributor'
AND		(ii.InstitutionCode = @InstitutionCode OR @InstitutionCode = '')
AND		i.CreationDate > DATEADD(dd, (@MaxAge * -1), GETDATE())
GROUP BY
		CHAR(dbo.fnContainsSuspectCharacter(i.Volume)), 
		i.Volume,
		t.TitleID,
		t.ShortTitle,
		i.ItemID,
		i.CreationDate,
		i.BarCode,
		oclc.IdentifierValue,
		dbo.fnContributorStringForTitle(t.TitleID, 0)
ORDER BY
		InstitutionName, i.CreationDate DESC
END
