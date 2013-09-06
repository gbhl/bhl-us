
-- AuthorTypeInsertAuto PROCEDURE
-- Generated 5/18/2012 11:11:49 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AuthorType

CREATE PROCEDURE AuthorTypeInsertAuto

@AuthorTypeID INT OUTPUT,
@AuthorTypeName NVARCHAR(50),
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[AuthorType]
(
	[AuthorTypeName],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@AuthorTypeName,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @AuthorTypeID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorTypeInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AuthorTypeID],
		[AuthorTypeName],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[AuthorType]
	
	WHERE
		[AuthorTypeID] = @AuthorTypeID
	
	RETURN -- insert successful
END


