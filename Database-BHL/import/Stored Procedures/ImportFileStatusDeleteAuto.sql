CREATE PROCEDURE [import].[ImportFileStatusDeleteAuto]

@ImportFileStatusID INT

AS 

DELETE FROM [import].[ImportFileStatus]

WHERE

	[ImportFileStatusID] = @ImportFileStatusID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ImportFileStatusDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END


GO
