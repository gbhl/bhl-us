
CREATE PROCEDURE [dbo].[InstitutionSelectAll]

AS 

SET NOCOUNT ON

SELECT	[InstitutionCode],
		[InstitutionName],
		[InstitutionUrl],
		[Note],
		[BHLMemberLibrary]
FROM	[dbo].[Institution]
ORDER BY
		[InstitutionName]

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure InstitutionSelectAll. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


