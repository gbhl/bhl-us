CREATE PROCEDURE dbo.APIKeyInsert

@ContactName NVARCHAR(200),
@EmailAddress NVARCHAR(200)

AS 

SET NOCOUNT ON

DECLARE @ApiKeyID INT
INSERT INTO dbo.APIKey (ContactName, EmailAddress) VALUES(@ContactName, @EmailAddress)
SET @ApiKeyID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure APIKeyInsert. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT	ApiKeyID,
			ContactName,
			EmailAddress,
			ApiKeyValue,
			IsActive
	FROM	dbo.APIKey
	WHERE	ApiKeyID = @ApiKeyID
	
	RETURN -- insert successful
END


