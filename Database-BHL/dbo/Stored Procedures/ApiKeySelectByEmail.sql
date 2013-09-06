CREATE PROCEDURE dbo.ApiKeySelectByEmail

@EmailAddress nvarchar(200)

AS
BEGIN

SET NOCOUNT ON

SELECT	ApiKeyID,
		ContactName,
		EmailAddress,
		ApiKeyValue,
		IsActive
FROM	dbo.APIKey
WHERE	EmailAddress = @EmailAddress

END

