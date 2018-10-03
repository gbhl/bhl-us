
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[PageTextLogUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PageTextLogUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Update Procedure for dbo.PageTextLog
-- Do not modify the contents of this procedure.
-- Generated 10/1/2018 8:24:59 PM

CREATE PROCEDURE dbo.PageTextLogUpdateAuto

@PageTextLogID INT,
@PageID INT,
@TextSource NVARCHAR(50),
@TextImportBatchFileID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[PageTextLog]
SET
	[PageID] = @PageID,
	[TextSource] = @TextSource,
	[TextImportBatchFileID] = @TextImportBatchFileID
WHERE
	[PageTextLogID] = @PageTextLogID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.PageTextLogUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[PageTextLogID],
		[PageID],
		[TextSource],
		[TextImportBatchFileID],
		[CreationDate],
		[CreationUserID]
	FROM [dbo].[PageTextLog]
	WHERE
		[PageTextLogID] = @PageTextLogID
	
	RETURN -- update successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

