IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[DisqusCacheUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[DisqusCacheUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- DisqusCacheUpdateAuto PROCEDURE
-- Generated 11/4/2015 10:49:28 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for DisqusCache

CREATE PROCEDURE DisqusCacheUpdateAuto

@DisqusCacheID INT,
@ItemID INT,
@PageID INT,
@Count INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[DisqusCache]

SET

	[ItemID] = @ItemID,
	[PageID] = @PageID,
	[Count] = @Count

WHERE
	[DisqusCacheID] = @DisqusCacheID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure DisqusCacheUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

GO
 
-- END OF PROCEDURE


SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

