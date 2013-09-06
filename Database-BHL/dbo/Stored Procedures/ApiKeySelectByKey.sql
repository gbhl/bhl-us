CREATE PROCEDURE dbo.ApiKeySelectByKey

@ApiKeyValue uniqueidentifier

AS

BEGIN

SET NOCOUNT ON

SELECT	ApiKeyID,
		ContactName,
		EmailAddress,
		IsActive
FROM	APIKey
WHERE	ApiKeyValue = @ApiKeyValue

END
