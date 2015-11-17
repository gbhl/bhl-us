IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[DisqusCacheSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[DisqusCacheSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- DisqusCacheSelectAuto PROCEDURE
-- Generated 11/4/2015 10:49:28 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for DisqusCache

CREATE PROCEDURE DisqusCacheSelectAuto

@DisqusCacheID INT

AS 

SET NOCOUNT ON

SELECT 

	[DisqusCacheID],
	[ItemID],
	[PageID],
	[Count],
	[CreationDate]

FROM [dbo].[DisqusCache]

WHERE
	[DisqusCacheID] = @DisqusCacheID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure DisqusCacheSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
 
-- END OF PROCEDURE

SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

