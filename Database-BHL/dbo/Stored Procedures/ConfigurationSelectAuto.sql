
-- ConfigurationSelectAuto PROCEDURE
-- Generated 6/10/2011 4:56:25 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Configuration

CREATE PROCEDURE ConfigurationSelectAuto

@ConfigurationID INT

AS 

SET NOCOUNT ON

SELECT 

	[ConfigurationID],
	[ConfigurationName],
	[ConfigurationValue]

FROM [dbo].[Configuration]

WHERE
	[ConfigurationID] = @ConfigurationID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ConfigurationSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

