CREATE PROCEDURE [dbo].[KeywordSelectWithSuspectCharacters]

@InstitutionCode NVARCHAR(10) = '',
@MaxAge INT = 30

AS
BEGIN

SET NOCOUNT ON

SELECT	t.TitleID, 
		t.InstitutionCode,
		ISNULL(inst.InstitutionName, '') AS InstitutionName,
		k.CreationDate,
		CHAR(dbo.fnContainsSuspectCharacter(k.Keyword)) AS KeywordSuspect, 
		k.Keyword,
		oclc.IdentifierValue as OCLC,
		MIN(i.ZQuery) AS ZQuery
FROM	dbo.Keyword k INNER JOIN dbo.TitleKeyword tk
			ON k.KeywordID = tk.KeywordID
		INNER JOIN dbo.Title t 
			ON tk.TitleID = t.TitleID
		LEFT JOIN (SELECT * FROM dbo.Title_Identifier WHERE IdentifierID = 1) as oclc
			ON t.TitleID = oclc.TitleID
		INNER JOIN dbo.TitleItem ti
			ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Item i
			ON ti.ItemID = i.ItemID
		LEFT JOIN dbo.Institution inst
			ON t.InstitutionCode = inst.InstitutionCode
WHERE	dbo.fnContainsSuspectCharacter(k.Keyword) > 0
AND		(t.InstitutionCode = @InstitutionCode OR @InstitutionCode = '')
AND		k.CreationDate > DATEADD(dd, (@MaxAge * -1), GETDATE())
GROUP BY
		CHAR(dbo.fnContainsSuspectCharacter(k.Keyword)), 
		t.TitleID, 
		k.Keyword,
		t.InstitutionCode,
		inst.InstitutionName,
		k.CreationDate,
		oclc.IdentifierValue
ORDER BY
		inst.InstitutionName, k.CreationDate DESC
END



