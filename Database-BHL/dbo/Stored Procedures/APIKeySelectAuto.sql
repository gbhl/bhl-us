
-- APIKeySelectAuto PROCEDURE
-- Generated 2/4/2010 3:26:40 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for APIKey

CREATE PROCEDURE APIKeySelectAuto

@ApiKeyID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure APIKeySelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

