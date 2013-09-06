
CREATE PROCEDURE [dbo].[PageFlickrSelectByPage]
@PageID int
AS 

SET NOCOUNT ON

SELECT	PageFlickrID,
		PageID,
		FlickrURL,
		CreationUserID,
		CreationDate
FROM	PageFlickr with (nolock)
WHERE	PageID = @PageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure [PageFlickrSelectByPage]. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


