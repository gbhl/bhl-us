CREATE PROCEDURE dbo.TitleInstitutionInsertAuto

@TitleInstitutionID INT OUTPUT,
@TitleID INT,
@InstitutionCode NVARCHAR(10),
@InstitutionRoleID INT,
@Url NVARCHAR(500),
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleInstitution]
( 	[TitleID],
	[InstitutionCode],
	[InstitutionRoleID],
	[Url],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@TitleID,
	@InstitutionCode,
	@InstitutionRoleID,
	@Url,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @TitleInstitutionID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleInstitutionInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
