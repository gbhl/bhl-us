
-- PageFlickrDeleteAuto PROCEDURE
-- Generated 1/25/2012 9:05:46 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for PageFlickr

CREATE PROCEDURE [dbo].[PageFlickrDeleteAuto]

@PageFlickrID INT

AS 

DELETE FROM [dbo].[PageFlickr]

WHERE

	[PageFlickrID] = @PageFlickrID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageFlickrDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

