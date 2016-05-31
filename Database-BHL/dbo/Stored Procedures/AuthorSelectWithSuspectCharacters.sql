CREATE PROCEDURE [dbo].[AuthorSelectWithSuspectCharacters]

@InstitutionCode NVARCHAR(10) = '',
@MaxAge INT = 30

AS
BEGIN

SET NOCOUNT ON

SELECT	t.TitleID,
		dbo.fnContributorStringForTitle(t.TitleID, 0) AS InstitutionName,
		ta.CreationDate,
		a.AuthorID,
		CHAR(dbo.fnContainsSuspectCharacter(n.FullName)) as NameSuspect, n.FullName,
		CHAR(dbo.fnContainsSuspectCharacter(a.Numeration)) as NumerationSuspect, a.Numeration,
		CHAR(dbo.fnContainsSuspectCharacter(a.Unit)) as UnitSuspect, a.Unit,
		CHAR(dbo.fnContainsSuspectCharacter(a.Title)) as TitleSuspect, a.Title,
		CHAR(dbo.fnContainsSuspectCharacter(a.Location)) as LocationSuspect, a.Location,
		CHAR(dbo.fnContainsSuspectCharacter(n.FullerForm)) as FullerFormSuspect, n.FullerForm,
		oclc.IdentifierValue as OCLC,
		MIN(i.ZQuery) AS ZQuery
FROM	dbo.Author a INNER JOIN dbo.AuthorName n
			ON a.AuthorID = n.AuthorID
		LEFT JOIN dbo.TitleAuthor ta
			ON a.AuthorID = ta.AuthorID
		INNER JOIN dbo.Title t 
			ON ta.TitleID = t.TitleID
		LEFT JOIN (SELECT * FROM dbo.Title_Identifier WHERE IdentifierID = 1) AS oclc
			ON t.TitleID = oclc.TitleID
		INNER JOIN dbo.TitleItem ti
			ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Item i
			ON ti.ItemID = i.ItemID
		INNER JOIN dbo.ItemInstitution ii
			ON i.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r
			ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	(dbo.fnContainsSuspectCharacter(n.FullName) > 0
OR		dbo.fnContainsSuspectCharacter(a.Numeration) > 0
OR		dbo.fnContainsSuspectCharacter(a.Unit) > 0
OR		dbo.fnContainsSuspectCharacter(a.Title) > 0
OR		dbo.fnContainsSuspectCharacter(a.Location) > 0
OR		dbo.fnContainsSuspectCharacter(n.FullerForm) > 0)
AND		r.InstitutionRoleName = 'Contributor'
AND		(ii.InstitutionCode = @InstitutionCode OR @InstitutionCode = '')
AND		ISNULL(a.CreationDate, '1/1/2005') > DATEADD(dd, (@MaxAge * -1), GETDATE())
GROUP BY 
		a.AuthorID,
		dbo.fnContributorStringForTitle(t.TitleID, 0),
		ta.CreationDate,
		CHAR(dbo.fnContainsSuspectCharacter(n.FullName)), n.FullName,
		CHAR(dbo.fnContainsSuspectCharacter(a.Numeration)), a.Numeration,
		CHAR(dbo.fnContainsSuspectCharacter(a.Unit)), a.Unit,
		CHAR(dbo.fnContainsSuspectCharacter(a.Title)), a.Title,
		CHAR(dbo.fnContainsSuspectCharacter(a.Location)), a.Location, 
		CHAR(dbo.fnContainsSuspectCharacter(n.FullerForm)), n.FullerForm, 
		t.TitleID, 
		oclc.IdentifierValue
ORDER BY
		InstitutionName, ta.CreationDate DESC
END
