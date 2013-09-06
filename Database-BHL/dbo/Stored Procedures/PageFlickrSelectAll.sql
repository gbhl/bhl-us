
CREATE PROCEDURE [dbo].[PageFlickrSelectAll]
AS 

SET NOCOUNT ON

SELECT	PageFlickrID,
		PageID,
		FlickrURL,
		CreationUserID,
		CreationDate
FROM	PageFlickr with (nolock)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure [PageFlickrSelectAll]. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


