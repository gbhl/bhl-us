
-- PageFlickrUpdateAuto PROCEDURE
-- Generated 1/25/2012 9:05:46 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for PageFlickr

CREATE PROCEDURE [dbo].[PageFlickrUpdateAuto]

@PageFlickrID INT,
@PageID INT,
@FlickrURL VARCHAR(500)

AS 

SET NOCOUNT ON

UPDATE [dbo].[PageFlickr]

SET

	[PageID] = @PageID,
	[FlickrURL] = @FlickrURL

WHERE
	[PageFlickrID] = @PageFlickrID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageFlickrUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[PageFlickrID],
		[PageID],
		[FlickrURL],
		[CreationUserID],
		[CreationDate]

	FROM [dbo].[PageFlickr]
	
	WHERE
		[PageFlickrID] = @PageFlickrID
	
	RETURN -- update successful
END

