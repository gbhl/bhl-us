CREATE PROCEDURE [dbo].[InstitutionSelectWithPublishedItems]

@OnlyMemberLibraries bit = 1,
@InstitutionRoleName nvarchar(100) = null

AS 

SET NOCOUNT ON

SELECT DISTINCT 
		ins.InstitutionCode,
		ins.InstitutionName,
		ins.Note,
		ISNULL(ins.InstitutionUrl, '') AS InstitutionUrl,
		ins.BHLMemberLibrary
FROM	dbo.Institution ins WITH (NOLOCK)
		INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON ins.InstitutionCode = ii.InstitutionCode
		INNER JOIN dbo.Item it WITH (NOLOCK) ON ii.ItemID = it.ItemID
		INNER JOIN dbo.InstitutionRole r WITH (NOLOCK) ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	it.ItemStatusID = 40
AND		((ins.BHLMemberLibrary = 1 AND @OnlyMemberLibraries = 1) OR	@OnlyMemberLibraries = 0)
AND		(r.InstitutionRoleName = @InstitutionRoleName OR @InstitutionRoleName IS NULL)
ORDER BY
		ins.InstitutionName

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionSelectWithPublishedItems. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
