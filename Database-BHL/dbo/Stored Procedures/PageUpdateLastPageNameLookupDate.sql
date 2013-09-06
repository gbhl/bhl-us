
CREATE PROCEDURE [dbo].[PageUpdateLastPageNameLookupDate]

@PageID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Page]

SET
	[LastPageNameLookupDate] = GETDATE(),
	[LastModifiedDate] = GETDATE(),
	[LastModifiedUserID] = 1	-- system update

WHERE
	[PageID] = @PageID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageUpdateLastPageNameLookupDate. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[PageID],
		[ItemID],
		[SequenceOrder],
		[Active]

	FROM [dbo].[Page]
	
	WHERE
		[PageID] = @PageID
	
	RETURN -- update successful
END

