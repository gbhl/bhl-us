
-- IAItemDeleteAuto PROCEDURE
-- Generated 10/14/2011 12:13:11 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for IAItem

CREATE PROCEDURE IAItemDeleteAuto

@ItemID INT

AS 

DELETE FROM [dbo].[IAItem]

WHERE

	[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

