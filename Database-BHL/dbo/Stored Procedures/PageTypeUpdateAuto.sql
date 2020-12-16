CREATE PROCEDURE dbo.PageTypeUpdateAuto

@PageTypeID INT,
@PageTypeName NVARCHAR(30),
@PageTypeDescription NVARCHAR(255),
@Active TINYINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[PageType]
SET
	[PageTypeName] = @PageTypeName,
	[PageTypeDescription] = @PageTypeDescription,
	[Active] = @Active,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[PageTypeID] = @PageTypeID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.PageTypeUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[PageTypeID],
		[PageTypeName],
		[PageTypeDescription],
		[Active],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
	FROM [dbo].[PageType]
	WHERE
		[PageTypeID] = @PageTypeID
	
	RETURN -- update successful
END
GO
