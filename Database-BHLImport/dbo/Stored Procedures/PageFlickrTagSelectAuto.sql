﻿CREATE PROCEDURE [dbo].[PageFlickrTagSelectAuto]

@PageFlickrTagID INT

AS 

SET NOCOUNT ON

SELECT 

	[PageFlickrTagID],
	[PageID],
	[PhotoID],
	[IsMachineTag],
	[TagValue],
	[FlickrAuthorID],
	[FlickrAuthorName],
	[IsActive],
	[CreationDate],
	[LastModifiedDate],
	[DeleteDate]

FROM [dbo].[PageFlickrTag]

WHERE
	[PageFlickrTagID] = @PageFlickrTagID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageFlickrTagSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
