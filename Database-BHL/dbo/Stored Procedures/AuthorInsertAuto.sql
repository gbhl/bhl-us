
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[AuthorInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[AuthorInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for dbo.Author
-- Do not modify the contents of this procedure.
-- Generated 6/6/2019 11:14:00 AM

CREATE PROCEDURE dbo.AuthorInsertAuto

@AuthorID INT OUTPUT,
@AuthorTypeID INT = null,
@StartDate NVARCHAR(25),
@EndDate NVARCHAR(25),
@Numeration NVARCHAR(300),
@Title NVARCHAR(200),
@Unit NVARCHAR(300),
@Location NVARCHAR(200),
@Note NVARCHAR(MAX),
@IsActive SMALLINT,
@RedirectAuthorID INT = null,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Author]
( 	[AuthorTypeID],
	[StartDate],
	[EndDate],
	[Numeration],
	[Title],
	[Unit],
	[Location],
	[Note],
	[IsActive],
	[RedirectAuthorID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@AuthorTypeID,
	@StartDate,
	@EndDate,
	@Numeration,
	@Title,
	@Unit,
	@Location,
	@Note,
	@IsActive,
	@RedirectAuthorID,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @AuthorID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.AuthorInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
		[Note],
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
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

