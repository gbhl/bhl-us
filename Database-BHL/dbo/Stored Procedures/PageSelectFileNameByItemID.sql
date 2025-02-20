CREATE PROCEDURE [dbo].[PageSelectFileNameByItemID]

@ItemID INT

AS 

SET NOCOUNT ON

SELECT 	p.[PageID],
		[FileNamePrefix]
FROM	[dbo].[Page] p
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
WHERE	ip.ItemID = @ItemID
ORDER BY
		ip.[SequenceOrder] ASC

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSelectByItemID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
