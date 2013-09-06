
CREATE PROCEDURE [dbo].[TitleAssociationSelectWithSuspectCharacters]

@InstitutionCode NVARCHAR(10) = '',
@MaxAge INT = 30

AS
BEGIN

SET NOCOUNT ON

SELECT	ta.TitleID,
		t.InstitutionCode,
		ISNULL(inst.InstitutionName, '') AS InstitutionName,
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
		INNER JOIN (SELECT DISTINCT ti.TitleID, i.InstitutionCode, i.ZQuery 
					FROM TitleItem ti INNER JOIN Item i ON ti.ItemID = i.ItemID) AS i
			ON t.TitleID = i.TitleID
		LEFT JOIN dbo.Institution inst
			ON t.InstitutionCode = inst.InstitutionCode
WHERE	(dbo.fnContainsSuspectCharacter(ta.Title) > 0
OR		dbo.fnContainsSuspectCharacter(ta.Section) > 0
OR		dbo.fnContainsSuspectCharacter(ta.Volume) > 0
OR		dbo.fnContainsSuspectCharacter(ta.Heading) > 0
OR		dbo.fnContainsSuspectCharacter(ta.Publication) > 0
OR		dbo.fnContainsSuspectCharacter(ta.Relationship) > 0)
AND		(t.InstitutionCode = @InstitutionCode OR @InstitutionCode = '')
AND		ta.CreationDate > DATEADD(dd, (@MaxAge * -1), GETDATE())
ORDER BY
		inst.InstitutionName, ta.CreationDate DESC
END


