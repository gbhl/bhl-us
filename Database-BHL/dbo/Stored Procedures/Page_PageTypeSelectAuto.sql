
-- Page_PageTypeSelectAuto PROCEDURE
-- Generated 1/18/2008 11:10:47 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for Page_PageType

CREATE PROCEDURE Page_PageTypeSelectAuto

@PageID INT /* Unique identifier for each Page record. */,
@PageTypeID INT /* Unique identifier for each Page Type record. */

AS 

SET NOCOUNT ON

SELECT 

	[PageID],
	[PageTypeID],
	[Verified],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[Page_PageType]

WHERE
	[PageID] = @PageID AND
	[PageTypeID] = @PageTypeID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Page_PageTypeSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

