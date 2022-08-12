CREATE PROCEDURE dbo.PageTypeInsertAuto

@PageTypeID INT OUTPUT,
@PageTypeName NVARCHAR(30),
@PageTypeDescription NVARCHAR(255) = null,
@Active TINYINT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[PageType]
( 	[PageTypeName],
	[PageTypeDescription],
	[Active],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@PageTypeName,
	@PageTypeDescription,
	@Active,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @PageTypeID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.PageTypeInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
 
