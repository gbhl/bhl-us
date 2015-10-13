CREATE PROCEDURE [dbo].[PageFlickrTagUpdateAuto]

@PageFlickrTagID INT,
@PageID INT,
@PhotoID NVARCHAR(50),
@IsMachineTag SMALLINT,
@TagValue NVARCHAR(1000),
@FlickrAuthorID NVARCHAR(100),
@FlickrAuthorName NVARCHAR(150),
@IsActive TINYINT,
@DeleteDate DATETIME

AS 

SET NOCOUNT ON

UPDATE [dbo].[PageFlickrTag]

SET

	[PageID] = @PageID,
	[PhotoID] = @PhotoID,
	[IsMachineTag] = @IsMachineTag,
	[TagValue] = @TagValue,
	[FlickrAuthorID] = @FlickrAuthorID,
	[FlickrAuthorName] = @FlickrAuthorName,
	[IsActive] = @IsActive,
	[LastModifiedDate] = getdate(),
	[DeleteDate] = @DeleteDate

WHERE
	[PageFlickrTagID] = @PageFlickrTagID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageFlickrTagUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END
