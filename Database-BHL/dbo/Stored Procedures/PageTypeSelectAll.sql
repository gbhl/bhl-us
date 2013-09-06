

CREATE PROCEDURE [dbo].[PageTypeSelectAll]
AS 

SET NOCOUNT ON

SELECT 
	[PageTypeID],
	[PageTypeName],
	[PageTypeDescription]
FROM [dbo].[PageType]
ORDER BY PageTypeName

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageTypeSelectAll. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


