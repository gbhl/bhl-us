
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[AuthorUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[AuthorUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Update Procedure for dbo.Author
-- Do not modify the contents of this procedure.
-- Generated 6/6/2019 11:14:00 AM

CREATE PROCEDURE dbo.AuthorUpdateAuto

@AuthorID INT,
@AuthorTypeID INT,
@StartDate NVARCHAR(25),
@EndDate NVARCHAR(25),
@Numeration NVARCHAR(300),
@Title NVARCHAR(200),
@Unit NVARCHAR(300),
@Location NVARCHAR(200),
@Note NVARCHAR(MAX),
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
	[Note] = @Note,
	[IsActive] = @IsActive,
	[RedirectAuthorID] = @RedirectAuthorID,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[AuthorID] = @AuthorID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.AuthorUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

