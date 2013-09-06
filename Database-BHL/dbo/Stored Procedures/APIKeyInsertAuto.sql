
-- APIKeyInsertAuto PROCEDURE
-- Generated 2/4/2010 3:26:40 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for APIKey

CREATE PROCEDURE APIKeyInsertAuto

@ApiKeyID INT OUTPUT,
@ContactName NVARCHAR(200),
@EmailAddress NVARCHAR(200),
@ApiKeyValue UNIQUEIDENTIFIER,
@IsActive TINYINT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[APIKey]
(
	[ContactName],
	[EmailAddress],
	[ApiKeyValue],
	[IsActive],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@ContactName,
	@EmailAddress,
	@ApiKeyValue,
	@IsActive,
	getdate(),
	getdate()
)

SET @ApiKeyID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure APIKeyInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ApiKeyID],
		[ContactName],
		[EmailAddress],
		[ApiKeyValue],
		[IsActive],
		[CreationDate],
		[LastModifiedDate]	

	FROM [dbo].[APIKey]
	
	WHERE
		[ApiKeyID] = @ApiKeyID
	
	RETURN -- insert successful
END

