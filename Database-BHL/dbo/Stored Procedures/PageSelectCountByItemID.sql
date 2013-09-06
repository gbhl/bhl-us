
CREATE PROCEDURE [dbo].[PageSelectCountByItemID]

@ItemID INT

AS 

SET NOCOUNT ON

SELECT COUNT(*)
FROM [dbo].[Page]
WHERE
	[ItemID] = @ItemID
AND	[Active] = 1

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSelectCountByItemID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
