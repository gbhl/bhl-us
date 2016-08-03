
-- Delete Procedure for dbo.Item
-- Do not modify the contents of this procedure.
-- Generated 8/3/2016 1:34:57 PM

CREATE PROCEDURE dbo.ItemDeleteAuto

@ItemID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[dbo].[Item]
WHERE	
	[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END
