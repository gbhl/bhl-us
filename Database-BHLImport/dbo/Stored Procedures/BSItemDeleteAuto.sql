
-- BSItemDeleteAuto PROCEDURE
-- Generated 10/23/2012 3:54:50 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for BSItem

CREATE PROCEDURE BSItemDeleteAuto

@ItemID INT

AS 

DELETE FROM [dbo].[BSItem]

WHERE

	[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BSItemDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

