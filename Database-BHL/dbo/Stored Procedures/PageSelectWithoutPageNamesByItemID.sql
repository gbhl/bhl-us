
CREATE PROCEDURE [dbo].[PageSelectWithoutPageNamesByItemID]

@ItemID INT

AS 

SET NOCOUNT ON

SELECT 
	[PageID],
	[FileNamePrefix]
FROM [dbo].[Page]
WHERE
	[ItemID] = @ItemID
AND	[Active] = 1
AND	[LastPageNameLookupDate] IS NULL
ORDER BY
	[SequenceOrder] ASC

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSelectWithoutPageNamesByItemID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
