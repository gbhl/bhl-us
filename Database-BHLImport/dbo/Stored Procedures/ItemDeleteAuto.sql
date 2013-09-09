
-- ItemDeleteAuto PROCEDURE
-- Generated 12/18/2008 2:14:22 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Item

CREATE PROCEDURE ItemDeleteAuto

@ItemID INT

AS 

DELETE FROM [dbo].[Item]

WHERE

	[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

