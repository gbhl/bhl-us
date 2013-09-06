
-- ConfigurationUpdateAuto PROCEDURE
-- Generated 6/10/2011 4:56:25 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Configuration

CREATE PROCEDURE ConfigurationUpdateAuto

@ConfigurationID INT,
@ConfigurationName NVARCHAR(50),
@ConfigurationValue NVARCHAR(1000)

AS 

SET NOCOUNT ON

UPDATE [dbo].[Configuration]

SET

	[ConfigurationID] = @ConfigurationID,
	[ConfigurationName] = @ConfigurationName,
	[ConfigurationValue] = @ConfigurationValue

WHERE
	[ConfigurationID] = @ConfigurationID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ConfigurationUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ConfigurationID],
		[ConfigurationName],
		[ConfigurationValue]

	FROM [dbo].[Configuration]
	
	WHERE
		[ConfigurationID] = @ConfigurationID
	
	RETURN -- update successful
END

