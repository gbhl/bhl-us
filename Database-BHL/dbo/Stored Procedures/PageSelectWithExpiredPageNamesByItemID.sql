
CREATE PROCEDURE [dbo].[PageSelectWithExpiredPageNamesByItemID]

@ItemID INT,
@MaxAge INT  -- Maximum allowed age of pages (in days)

AS 

SET NOCOUNT ON

SELECT 
	[PageID],
	[FileNamePrefix]
FROM [dbo].[Page]
WHERE
	[ItemID] = @ItemID
AND	[Active] = 1
AND	DATEDIFF(day, [LastPageNameLookupDate], GETDATE()) > @MaxAge
ORDER BY
	[SequenceOrder] ASC

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSelectWithExpiredPageNamesByItemID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
