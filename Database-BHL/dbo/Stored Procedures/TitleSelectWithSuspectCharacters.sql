
CREATE PROCEDURE [dbo].[TitleSelectWithSuspectCharacters]

@InstitutionCode NVARCHAR(10) = '',
@MaxAge INT = 30

AS
BEGIN

SET NOCOUNT ON

-- Title
SELECT	t.TitleID, 
		t.InstitutionCode,
		ISNULL(inst.InstitutionName, '') AS InstitutionName,
		t.CreationDate,
		CHAR(dbo.fnContainsSuspectCharacter(FullTitle)) as FullTitleSuspect, FullTitle,
		CHAR(dbo.fnContainsSuspectCharacter(ShortTitle)) as ShortTitleSuspect, ShortTitle,
		CHAR(dbo.fnContainsSuspectCharacter(SortTitle)) as SortTitleSuspect, SortTitle,
		CHAR(dbo.fnContainsSuspectCharacter(DataField_260_a)) as DataField260aSuspect, DataField_260_a,
		CHAR(dbo.fnContainsSuspectCharacter(DataField_260_b)) as DataField260bSuspect, DataField_260_b,
		CHAR(dbo.fnContainsSuspectCharacter(PublicationDetails)) as PubDetailsSuspect, PublicationDetails,
		oclc.IdentifierValue as OCLC,
		MIN(i.ZQuery) AS ZQuery
FROM	dbo.Title t LEFT JOIN (SELECT * FROM dbo.Title_Identifier WHERE IdentifierID = 1) AS oclc
			ON t.TitleID = oclc.TitleID
		INNER JOIN dbo.TitleItem ti
			ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Item i
			ON ti.ItemID = i.ItemID
		LEFT JOIN dbo.Institution inst
			ON t.InstitutionCode = inst.InstitutionCode
WHERE	(dbo.fnContainsSuspectCharacter(FullTitle) > 0
OR		dbo.fnContainsSuspectCharacter(ShortTitle) > 0
OR		dbo.fnContainsSuspectCharacter(SortTitle) > 0
OR		dbo.fnContainsSuspectCharacter(Datafield_260_a) > 0
OR		dbo.fnContainsSuspectCharacter(Datafield_260_b) > 0
OR		dbo.fnContainsSuspectCharacter(PublicationDetails) > 0)
AND		(t.InstitutionCode = @InstitutionCode
OR		@InstitutionCode = '')
AND		t.CreationDate > DATEADD(dd, (@MaxAge * -1), GETDATE())
GROUP BY 
		t.TitleID, 
		t.InstitutionCode,
		inst.InstitutionName,
		t.CreationDate,
		CHAR(dbo.fnContainsSuspectCharacter(FullTitle)), FullTitle,
		CHAR(dbo.fnContainsSuspectCharacter(ShortTitle)), ShortTitle,
		CHAR(dbo.fnContainsSuspectCharacter(SortTitle)),  SortTitle,
		CHAR(dbo.fnContainsSuspectCharacter(DataField_260_a)), DataField_260_a,
		CHAR(dbo.fnContainsSuspectCharacter(DataField_260_b)), DataField_260_b, 
		CHAR(dbo.fnContainsSuspectCharacter(PublicationDetails)), PublicationDetails,
		oclc.IdentifierValue
ORDER BY
		inst.InstitutionName, t.CreationDate DESC

END


