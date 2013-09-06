
-- APIKeyUpdateAuto PROCEDURE
-- Generated 2/4/2010 3:26:40 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for APIKey

CREATE PROCEDURE APIKeyUpdateAuto

@ApiKeyID INT,
@ContactName NVARCHAR(200),
@EmailAddress NVARCHAR(200),
@ApiKeyValue UNIQUEIDENTIFIER,
@IsActive TINYINT

AS 

SET NOCOUNT ON

UPDATE [dbo].[APIKey]

SET

	[ContactName] = @ContactName,
	[EmailAddress] = @EmailAddress,
	[ApiKeyValue] = @ApiKeyValue,
	[IsActive] = @IsActive,
	[LastModifiedDate] = getdate()

WHERE
	[ApiKeyID] = @ApiKeyID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure APIKeyUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

