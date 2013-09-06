
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
		i.InstitutionCode,
		ISNULL(inst.InstitutionName, '') AS InstitutionName,
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
		LEFT JOIN dbo.Institution inst
			ON i.InstitutionCode = inst.InstitutionCode
WHERE	dbo.fnContainsSuspectCharacter(i.Volume) > 0
AND		(i.InstitutionCode = @InstitutionCode OR @InstitutionCode = '')
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
		i.InstitutionCode,
		inst.InstitutionName
ORDER BY
		inst.InstitutionName, i.CreationDate DESC
END



