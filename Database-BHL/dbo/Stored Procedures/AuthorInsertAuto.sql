
-- AuthorInsertAuto PROCEDURE
-- Generated 5/29/2012 12:59:27 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Author

CREATE PROCEDURE AuthorInsertAuto

@AuthorID INT OUTPUT,
@AuthorTypeID INT = null,
@StartDate NVARCHAR(25),
@EndDate NVARCHAR(25),
@Numeration NVARCHAR(300),
@Title NVARCHAR(200),
@Unit NVARCHAR(300),
@Location NVARCHAR(200),
@IsActive SMALLINT,
@RedirectAuthorID INT = null,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Author]
(
	[AuthorTypeID],
	[StartDate],
	[EndDate],
	[Numeration],
	[Title],
	[Unit],
	[Location],
	[IsActive],
	[RedirectAuthorID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@AuthorTypeID,
	@StartDate,
	@EndDate,
	@Numeration,
	@Title,
	@Unit,
	@Location,
	@IsActive,
	@RedirectAuthorID,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @AuthorID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AuthorID],
		[AuthorTypeID],
		[StartDate],
		[EndDate],
		[Numeration],
		[Title],
		[Unit],
		[Location],
		[IsActive],
		[RedirectAuthorID],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[Author]
	
	WHERE
		[AuthorID] = @AuthorID
	
	RETURN -- insert successful
END


