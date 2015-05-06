CREATE PROCEDURE [reqlog].[ApplicationSelectAuto]

@ApplicationID INT

AS 

SET NOCOUNT ON

SELECT 

	[ApplicationID],
	[ApplicationName]

FROM [reqlog].[Application]

WHERE
	[ApplicationID] = @ApplicationID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ApplicationSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
