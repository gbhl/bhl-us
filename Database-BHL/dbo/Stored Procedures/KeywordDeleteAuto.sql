
-- KeywordDeleteAuto PROCEDURE
-- Generated 5/3/2012 1:28:21 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Keyword

CREATE PROCEDURE KeywordDeleteAuto

@KeywordID INT

AS 

DELETE FROM [dbo].[Keyword]

WHERE

	[KeywordID] = @KeywordID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure KeywordDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END


