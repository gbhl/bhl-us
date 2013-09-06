
-- Page_PageTypeInsertAuto PROCEDURE
-- Generated 1/18/2008 11:10:47 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Page_PageType

CREATE PROCEDURE Page_PageTypeInsertAuto

@PageID INT /* Unique identifier for each Page record. */,
@PageTypeID INT /* Unique identifier for each Page Type record. */,
@Verified BIT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Page_PageType]
(
	[PageID],
	[PageTypeID],
	[Verified],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@PageID,
	@PageTypeID,
	@Verified,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Page_PageTypeInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[PageID],
		[PageTypeID],
		[Verified],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[Page_PageType]
	
	WHERE
		[PageID] = @PageID AND
		[PageTypeID] = @PageTypeID
	
	RETURN -- insert successful
END

