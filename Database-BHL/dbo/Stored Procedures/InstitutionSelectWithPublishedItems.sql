
CREATE PROCEDURE [dbo].[InstitutionSelectWithPublishedItems]

@OnlyMemberLibraries bit = 1

AS 

SET NOCOUNT ON

SELECT DISTINCT 
		ins.InstitutionCode,
		ins.InstitutionName,
		ins.Note,
		ISNULL(ins.InstitutionUrl, '') AS InstitutionUrl,
		ins.BHLMemberLibrary
FROM	dbo.Institution ins INNER JOIN dbo.Item it 
			ON ins.InstitutionCode = it.InstitutionCode
WHERE	it.ItemStatusID = 40
AND		((ins.BHLMemberLibrary = 1 AND @OnlyMemberLibraries = 1) OR	@OnlyMemberLibraries = 0)
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


