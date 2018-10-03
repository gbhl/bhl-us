
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[PageTextLogSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PageTextLogSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for dbo.PageTextLog
-- Do not modify the contents of this procedure.
-- Generated 10/1/2018 8:24:59 PM

CREATE PROCEDURE [dbo].[PageTextLogSelectAuto]

@PageTextLogID INT

AS 

SET NOCOUNT ON

SELECT	
	[PageTextLogID],
	[PageID],
	[TextSource],
	[TextImportBatchFileID],
	[CreationDate],
	[CreationUserID]
FROM	
	[dbo].[PageTextLog]
WHERE	
	[PageTextLogID] = @PageTextLogID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.PageTextLogSelectAuto. No information was selected.', 16, 1)
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

