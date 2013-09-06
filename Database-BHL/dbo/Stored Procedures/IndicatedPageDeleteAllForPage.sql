
CREATE PROCEDURE [dbo].[IndicatedPageDeleteAllForPage]

@PageID INT /* Unique identifier for each Page record. */
AS 

DELETE FROM [dbo].[IndicatedPage]

WHERE

	[PageID] = @PageID 

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IndicatedPageDeleteAllForPage. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

