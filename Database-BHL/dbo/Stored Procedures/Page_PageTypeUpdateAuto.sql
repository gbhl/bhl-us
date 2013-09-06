
-- Page_PageTypeUpdateAuto PROCEDURE
-- Generated 1/18/2008 11:10:47 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for Page_PageType

CREATE PROCEDURE Page_PageTypeUpdateAuto

@PageID INT /* Unique identifier for each Page record. */,
@PageTypeID INT /* Unique identifier for each Page Type record. */,
@Verified BIT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Page_PageType]

SET

	[PageID] = @PageID,
	[PageTypeID] = @PageTypeID,
	[Verified] = @Verified,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[PageID] = @PageID AND
	[PageTypeID] = @PageTypeID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Page_PageTypeUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

