CREATE PROCEDURE [dbo].[InstitutionSelectByItemID]

@ItemID int

AS 

SET NOCOUNT ON

SELECT	i.[InstitutionCode],
		i.[InstitutionName],
		i.[Note],
		i.[InstitutionUrl],
		i.BHLMemberLibrary,
		ii.ItemInstitutionID,
		r.InstitutionRoleName,
		r.InstitutionRoleLabel,
		i.CreationDate,
		i.LastModifiedDate,
		i.CreationUserID,
		i.LastModifiedUserID
FROM	dbo.ItemInstitution ii
		INNER JOIN dbo.Institution i ON ii.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	ii.ItemID = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionSelectByItemID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
