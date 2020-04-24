CREATE PROCEDURE dbo.InstitutionSelectWithGroups

@InstitutionCode NVARCHAR(10)

AS BEGIN

SET NOCOUNT ON 

SELECT	InstitutionCode,
		InstitutionName,
		Note,
		InstitutionUrl,
		BHLMemberLibrary,
		dbo.fnGroupStringForInstitution(InstitutionCode) AS InstitutionGroups,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.Institution
WHERE	InstitutionCode = @InstitutionCode

END
