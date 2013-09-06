
-- ConfigurationDeleteAuto PROCEDURE
-- Generated 6/10/2011 4:56:25 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Configuration

CREATE PROCEDURE ConfigurationDeleteAuto

@ConfigurationID INT

AS 

DELETE FROM [dbo].[Configuration]

WHERE

	[ConfigurationID] = @ConfigurationID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ConfigurationDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

