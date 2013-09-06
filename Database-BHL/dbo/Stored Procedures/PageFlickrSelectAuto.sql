
-- PageFlickrSelectAuto PROCEDURE
-- Generated 1/25/2012 9:05:46 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for PageFlickr

CREATE PROCEDURE [dbo].[PageFlickrSelectAuto]

@PageFlickrID INT

AS 

SET NOCOUNT ON

SELECT 

	[PageFlickrID],
	[PageID],
	[FlickrURL],
	[CreationUserID],
	[CreationDate]

FROM [dbo].[PageFlickr]

WHERE
	[PageFlickrID] = @PageFlickrID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageFlickrSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

