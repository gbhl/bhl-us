
-- AuthorUpdateAuto PROCEDURE
-- Generated 5/29/2012 12:59:27 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Author

CREATE PROCEDURE AuthorUpdateAuto

@AuthorID INT,
@AuthorTypeID INT,
@StartDate NVARCHAR(25),
@EndDate NVARCHAR(25),
@Numeration NVARCHAR(300),
@Title NVARCHAR(200),
@Unit NVARCHAR(300),
@Location NVARCHAR(200),
@IsActive SMALLINT,
@RedirectAuthorID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Author]

SET

	[AuthorTypeID] = @AuthorTypeID,
	[StartDate] = @StartDate,
	[EndDate] = @EndDate,
	[Numeration] = @Numeration,
	[Title] = @Title,
	[Unit] = @Unit,
	[Location] = @Location,
	[IsActive] = @IsActive,
	[RedirectAuthorID] = @RedirectAuthorID,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[AuthorID] = @AuthorID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END


