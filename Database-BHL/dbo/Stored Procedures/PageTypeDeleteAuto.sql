
-- PageTypeDeleteAuto PROCEDURE
-- Generated 1/18/2008 11:10:47 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for PageType

CREATE PROCEDURE PageTypeDeleteAuto

@PageTypeID INT /* Unique identifier for each Page Type record. */

AS 

DELETE FROM [dbo].[PageType]

WHERE

	[PageTypeID] = @PageTypeID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageTypeDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

