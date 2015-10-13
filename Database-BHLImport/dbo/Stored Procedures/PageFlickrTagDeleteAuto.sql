CREATE PROCEDURE [dbo].[PageFlickrTagDeleteAuto]

@PageFlickrTagID INT

AS 

DELETE FROM [dbo].[PageFlickrTag]

WHERE

	[PageFlickrTagID] = @PageFlickrTagID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageFlickrTagDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END
