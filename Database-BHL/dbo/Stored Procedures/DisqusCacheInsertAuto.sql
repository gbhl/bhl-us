IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[DisqusCacheInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[DisqusCacheInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- DisqusCacheInsertAuto PROCEDURE
-- Generated 11/4/2015 10:49:28 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for DisqusCache

CREATE PROCEDURE DisqusCacheInsertAuto

@DisqusCacheID INT OUTPUT,
@ItemID INT,
@PageID INT,
@Count INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[DisqusCache]
(
	[ItemID],
	[PageID],
	[Count],
	[CreationDate]
)
VALUES
(
	@ItemID,
	@PageID,
	@Count,
	getdate()
)

SET @DisqusCacheID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure DisqusCacheInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[DisqusCacheID],
		[ItemID],
		[PageID],
		[Count],
		[CreationDate]	

	FROM [dbo].[DisqusCache]
	
	WHERE
		[DisqusCacheID] = @DisqusCacheID
	
	RETURN -- insert successful
END

GO
 
-- END OF PROCEDURE

SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

