
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[PageTextLogInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PageTextLogInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for dbo.PageTextLog
-- Do not modify the contents of this procedure.
-- Generated 10/1/2018 8:24:58 PM

CREATE PROCEDURE dbo.PageTextLogInsertAuto

@PageTextLogID INT OUTPUT,
@PageID INT,
@TextSource NVARCHAR(50),
@TextImportBatchFileID INT = null,
@CreationUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[PageTextLog]
( 	[PageID],
	[TextSource],
	[TextImportBatchFileID],
	[CreationDate],
	[CreationUserID] )
VALUES
( 	@PageID,
	@TextSource,
	@TextImportBatchFileID,
	getdate(),
	@CreationUserID )

SET @PageTextLogID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.PageTextLogInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

