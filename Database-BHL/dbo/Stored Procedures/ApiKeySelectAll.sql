
CREATE PROCEDURE ApiKeySelectAll

AS

BEGIN

SET NOCOUNT ON

SELECT	ApiKeyID,
		ContactName,
		EmailAddress,
		ApiKeyValue,
		IsActive,
		CreationDate,
		LastModifiedDate
FROM	dbo.APIKey
ORDER BY
		ContactName
		
END

