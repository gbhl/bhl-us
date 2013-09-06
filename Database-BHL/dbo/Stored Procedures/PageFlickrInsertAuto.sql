
-- PageFlickrInsertAuto PROCEDURE
-- Generated 1/25/2012 9:05:46 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for PageFlickr

CREATE PROCEDURE [dbo].[PageFlickrInsertAuto]

@PageFlickrID INT OUTPUT,
@PageID INT,
@FlickrURL VARCHAR(500),
@CreationUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[PageFlickr]
(
	[PageID],
	[FlickrURL],
	[CreationUserID],
	[CreationDate]
)
VALUES
(
	@PageID,
	@FlickrURL,
	@CreationUserID,
	getdate()
)

SET @PageFlickrID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageFlickrInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

