CREATE PROCEDURE [dbo].[InstitutionSelectByTitleID]

@TitleID int

AS 

SET NOCOUNT ON

SELECT	i.[InstitutionCode],
		i.[InstitutionName],
		i.[Note],
		i.[InstitutionUrl],
		i.BHLMemberLibrary,
		ti.TitleInstitutionID,
		ti.Url AS Url,
		r.InstitutionRoleName,
		r.InstitutionRoleLabel,
		i.CreationDate,
		i.LastModifiedDate,
		i.CreationUserID,
		i.LastModifiedUserID
FROM	dbo.TitleInstitution ti
		INNER JOIN dbo.Institution i ON ti.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON ti.InstitutionRoleID = r.InstitutionRoleID
WHERE	ti.TitleID = @TitleID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionSelectByTitleID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
