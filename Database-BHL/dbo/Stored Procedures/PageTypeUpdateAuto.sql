
-- PageTypeUpdateAuto PROCEDURE
-- Generated 1/18/2008 11:10:47 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for PageType

CREATE PROCEDURE PageTypeUpdateAuto

@PageTypeID INT /* Unique identifier for each Page Type record. */,
@PageTypeName NVARCHAR(30) /* Name of a Page Type. */,
@PageTypeDescription NVARCHAR(255) /* Description of the Page Type. */

AS 

SET NOCOUNT ON

UPDATE [dbo].[PageType]

SET

	[PageTypeName] = @PageTypeName,
	[PageTypeDescription] = @PageTypeDescription

WHERE
	[PageTypeID] = @PageTypeID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageTypeUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[PageTypeID],
		[PageTypeName],
		[PageTypeDescription]

	FROM [dbo].[PageType]
	
	WHERE
		[PageTypeID] = @PageTypeID
	
	RETURN -- update successful
END

