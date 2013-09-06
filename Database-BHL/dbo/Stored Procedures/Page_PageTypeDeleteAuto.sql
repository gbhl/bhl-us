
-- Page_PageTypeDeleteAuto PROCEDURE
-- Generated 1/18/2008 11:10:47 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Page_PageType

CREATE PROCEDURE Page_PageTypeDeleteAuto

@PageID INT /* Unique identifier for each Page record. */,
@PageTypeID INT /* Unique identifier for each Page Type record. */

AS 

DELETE FROM [dbo].[Page_PageType]

WHERE

	[PageID] = @PageID AND
	[PageTypeID] = @PageTypeID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Page_PageTypeDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

