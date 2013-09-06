CREATE PROCEDURE dbo.ConfigurationSelectByName

@ConfigurationName nvarchar(50)

AS
BEGIN

SET NOCOUNT ON

SELECT	ConfigurationID,
		ConfigurationName,
		ConfigurationValue
FROM	dbo.Configuration
WHERE	ConfigurationName = @ConfigurationName

END
