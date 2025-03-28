SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
		b.ZQuery AS ZQuery
FROM	dbo.TitleAssociation ta 
		INNER JOIN dbo.Title t ON ta.TitleID = t.TitleID
		LEFT JOIN (SELECT * FROM dbo.Title_Identifier WHERE IdentifierID = 1) AS oclc ON t.TitleID = oclc.TitleID
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
		INNER JOIN dbo.ItemInstitution ii ON it.ItemID = ii.ItemID
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
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
		b.ZQuery
ORDER BY
		InstitutionName, ta.CreationDate DESC
END


GO
