
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[AuthorSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[AuthorSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for dbo.Author
-- Do not modify the contents of this procedure.
-- Generated 6/6/2019 11:14:00 AM

CREATE PROCEDURE [dbo].[AuthorSelectAuto]

@AuthorID INT

AS 

SET NOCOUNT ON

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
FROM	
	[dbo].[Author]
WHERE	
	[AuthorID] = @AuthorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.AuthorSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

