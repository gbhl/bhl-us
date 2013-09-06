
-- TitleKeywordDeleteAuto PROCEDURE
-- Generated 5/3/2012 1:28:21 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for TitleKeyword

CREATE PROCEDURE TitleKeywordDeleteAuto

@TitleKeywordID INT

AS 

DELETE FROM [dbo].[TitleKeyword]

WHERE

	[TitleKeywordID] = @TitleKeywordID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleKeywordDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END


