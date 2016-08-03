
-- Delete Procedure for dbo.IAItem
-- Do not modify the contents of this procedure.
-- Generated 8/3/2016 12:50:45 PM

CREATE PROCEDURE dbo.IAItemDeleteAuto

@ItemID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[dbo].[IAItem]
WHERE	
	[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IAItemDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END
