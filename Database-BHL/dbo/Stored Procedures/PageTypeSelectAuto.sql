
-- PageTypeSelectAuto PROCEDURE
-- Generated 1/18/2008 11:10:47 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for PageType

CREATE PROCEDURE PageTypeSelectAuto

@PageTypeID INT /* Unique identifier for each Page Type record. */

AS 

SET NOCOUNT ON

SELECT 

	[PageTypeID],
	[PageTypeName],
	[PageTypeDescription]

FROM [dbo].[PageType]

WHERE
	[PageTypeID] = @PageTypeID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageTypeSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

