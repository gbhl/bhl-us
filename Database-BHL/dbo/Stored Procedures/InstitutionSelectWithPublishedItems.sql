CREATE PROCEDURE [dbo].[InstitutionSelectWithPublishedItems]

@OnlyMemberLibraries bit = 1,
@InstitutionRoleName nvarchar(100) = null

AS 

SET NOCOUNT ON

SELECT	ins.InstitutionCode
INTO	#tmpInstitution
FROM	dbo.Institution ins WITH (NOLOCK)
		INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON ins.InstitutionCode = ii.InstitutionCode
		INNER JOIN dbo.Item it WITH (NOLOCK) ON ii.ItemID = it.ItemID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON it.ItemID = b.ItemID
		INNER JOIN dbo.InstitutionRole r WITH (NOLOCK) ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	it.ItemStatusID = 40
AND		((ins.BHLMemberLibrary = 1 AND @OnlyMemberLibraries = 1) OR	@OnlyMemberLibraries = 0)
AND		(r.InstitutionRoleName = @InstitutionRoleName OR @InstitutionRoleName IS NULL)

SELECT	InstitutionCode,
		InstitutionName,
		Note,
		ISNULL(InstitutionUrl, '') AS InstitutionUrl,
		BHLMemberLibrary
FROM	dbo.Institution
WHERE	InstitutionCode IN (SELECT DISTINCT InstitutionCode FROM #tmpInstitution)
ORDER BY InstitutionName

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionSelectWithPublishedItems. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
