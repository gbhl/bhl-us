CREATE PROCEDURE [dbo].[TitleInstitutionSelectAuto]

@TitleInstitutionID INT

AS 

SET NOCOUNT ON

SELECT	
	[TitleInstitutionID],
	[TitleID],
	[InstitutionCode],
	[InstitutionRoleID],
	[Url],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[dbo].[TitleInstitution]
WHERE	
	[TitleInstitutionID] = @TitleInstitutionID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleInstitutionSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
