CREATE PROCEDURE [dbo].[TitleAssociationSelectWithSuspectCharacters]

@InstitutionCode NVARCHAR(10) = '',
@MaxAge INT = 30

AS
BEGIN

SET NOCOUNT ON

SELECT	ta.TitleID,
		dbo.fnContributorStringForTitle(t.TitleID, 0) AS InstitutionName,
		ta.CreationDate,
		ta.TitleAssociationID,
		CHAR(dbo.fnContainsSuspectCharacter(ta.Title)) AS TitleSuspect, ta.Title,
		CHAR(dbo.fnContainsSuspectCharacter(ta.Section)) AS SectionSuspect, ta.Section,
		CHAR(dbo.fnContainsSuspectCharacter(ta.Volume)) AS VolumeSuspect, ta.Volume,
		CHAR(dbo.fnContainsSuspectCharacter(ta.Heading)) AS HeadingSuspect, ta.Heading,
		CHAR(dbo.fnContainsSuspectCharacter(ta.Publication)) AS PublicationSuspect, ta.Publication,
		CHAR(dbo.fnContainsSuspectCharacter(ta.Relationship)) AS RelationshipSuspect, ta.Relationship,
		oclc.IdentifierValue AS OCLC,
		i.ZQuery AS ZQuery
FROM	dbo.TitleAssociation ta INNER JOIN dbo.Title t
			ON ta.TitleID = t.TitleID
		LEFT JOIN (SELECT * FROM dbo.Title_Identifier WHERE IdentifierID = 1) AS oclc
			ON t.TitleID = oclc.TitleID
		INNER JOIN dbo.TitleItem ti
			ON t.TitleID = ti.TitleID
		INNER JOIN dbo.ItemInstitution ii
			ON ti.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r
			ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.Item i
			ON ti.ItemID = i.ItemID
WHERE	(dbo.fnContainsSuspectCharacter(ta.Title) > 0
OR		dbo.fnContainsSuspectCharacter(ta.Section) > 0
OR		dbo.fnContainsSuspectCharacter(ta.Volume) > 0
OR		dbo.fnContainsSuspectCharacter(ta.Heading) > 0
OR		dbo.fnContainsSuspectCharacter(ta.Publication) > 0
OR		dbo.fnContainsSuspectCharacter(ta.Relationship) > 0)
AND		r.InstitutionRoleName = 'Holding Institution'
AND		(ii.InstitutionCode = @InstitutionCode OR @InstitutionCode = '')
AND		ta.CreationDate > DATEADD(dd, (@MaxAge * -1), GETDATE())
GROUP BY 
		ta.TitleID,
		dbo.fnContributorStringForTitle(t.TitleID, 0),
		ta.CreationDate,
		ta.TitleAssociationID,
		CHAR(dbo.fnContainsSuspectCharacter(ta.Title)), ta.Title,
		CHAR(dbo.fnContainsSuspectCharacter(ta.Section)), ta.Section,
		CHAR(dbo.fnContainsSuspectCharacter(ta.Volume)), ta.Volume,
		CHAR(dbo.fnContainsSuspectCharacter(ta.Heading)), ta.Heading,
		CHAR(dbo.fnContainsSuspectCharacter(ta.Publication)), ta.Publication,
		CHAR(dbo.fnContainsSuspectCharacter(ta.Relationship)), ta.Relationship,
		oclc.IdentifierValue,
		i.ZQuery
ORDER BY
		InstitutionName, ta.CreationDate DESC
END
