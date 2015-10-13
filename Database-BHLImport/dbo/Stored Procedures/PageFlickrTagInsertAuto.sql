CREATE PROCEDURE [dbo].[PageFlickrTagInsertAuto]

@PageFlickrTagID INT OUTPUT,
@PageID INT,
@PhotoID NVARCHAR(50),
@IsMachineTag SMALLINT,
@TagValue NVARCHAR(1000),
@FlickrAuthorID NVARCHAR(100),
@FlickrAuthorName NVARCHAR(150),
@IsActive TINYINT,
@DeleteDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[PageFlickrTag]
(
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
)
VALUES
(
	@PageID,
	@PhotoID,
	@IsMachineTag,
	@TagValue,
	@FlickrAuthorID,
	@FlickrAuthorName,
	@IsActive,
	getdate(),
	getdate(),
	@DeleteDate
)

SET @PageFlickrTagID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageFlickrTagInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
