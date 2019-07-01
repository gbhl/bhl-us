CREATE PROCEDURE dbo.TitleAuthorDeleteAuto

@TitleAuthorID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[dbo].[TitleAuthor]
WHERE	
	[TitleAuthorID] = @TitleAuthorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleAuthorDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END
