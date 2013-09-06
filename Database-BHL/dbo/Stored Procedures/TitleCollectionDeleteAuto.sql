
-- TitleCollectionDeleteAuto PROCEDURE
-- Generated 7/30/2010 2:09:29 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for TitleCollection

CREATE PROCEDURE TitleCollectionDeleteAuto

@TitleCollectionID INT

AS 

DELETE FROM [dbo].[TitleCollection]

WHERE

	[TitleCollectionID] = @TitleCollectionID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleCollectionDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

