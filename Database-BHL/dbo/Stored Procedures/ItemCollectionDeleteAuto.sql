
-- ItemCollectionDeleteAuto PROCEDURE
-- Generated 7/30/2010 2:09:29 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for ItemCollection

CREATE PROCEDURE ItemCollectionDeleteAuto

@ItemCollectionID INT

AS 

DELETE FROM [dbo].[ItemCollection]

WHERE

	[ItemCollectionID] = @ItemCollectionID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemCollectionDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

