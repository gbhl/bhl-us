CREATE PROCEDURE dbo.BibliographicLevelDeleteAuto

@BibliographicLevelID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[dbo].[BibliographicLevel]
WHERE	
	[BibliographicLevelID] = @BibliographicLevelID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.BibliographicLevelDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END
