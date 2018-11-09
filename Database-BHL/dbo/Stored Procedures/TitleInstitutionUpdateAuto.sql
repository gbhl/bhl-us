CREATE PROCEDURE dbo.TitleInstitutionUpdateAuto

@TitleInstitutionID INT,
@TitleID INT,
@InstitutionCode NVARCHAR(10),
@InstitutionRoleID INT,
@Url NVARCHAR(500),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[TitleInstitution]
SET
	[TitleID] = @TitleID,
	[InstitutionCode] = @InstitutionCode,
	[InstitutionRoleID] = @InstitutionRoleID,
	[Url] = @Url,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[TitleInstitutionID] = @TitleInstitutionID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleInstitutionUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	FROM [dbo].[TitleInstitution]
	WHERE
		[TitleInstitutionID] = @TitleInstitutionID
	
	RETURN -- update successful
END
