
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[PageTextLogDeleteAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PageTextLogDeleteAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Delete Procedure for dbo.PageTextLog
-- Do not modify the contents of this procedure.
-- Generated 10/1/2018 8:24:56 PM

CREATE PROCEDURE dbo.PageTextLogDeleteAuto

@PageTextLogID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[dbo].[PageTextLog]
WHERE	
	[PageTextLogID] = @PageTextLogID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.PageTextLogDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

