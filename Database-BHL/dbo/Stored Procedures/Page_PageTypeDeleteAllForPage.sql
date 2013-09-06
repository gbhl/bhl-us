

CREATE PROCEDURE [dbo].[Page_PageTypeDeleteAllForPage]

@PageID INT /* Unique identifier for each Page record. */
AS 

DELETE FROM [dbo].[Page_PageType]

WHERE

	[PageID] = @PageID 

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Page_PageTypeDeleteAllForPage. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END


