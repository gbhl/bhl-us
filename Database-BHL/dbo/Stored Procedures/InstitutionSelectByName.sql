CREATE PROCEDURE dbo.InstitutionSelectByName

@InstitutionName NVARCHAR(255)

AS 

SET NOCOUNT ON

SELECT	InstitutionCode,
		InstitutionName,
		Note,
		InstitutionUrl,
		BHLMemberLibrary,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.Institution
WHERE	InstitutionName = @InstitutionName

GO
