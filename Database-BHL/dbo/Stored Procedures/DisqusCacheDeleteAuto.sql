IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[DisqusCacheDeleteAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[DisqusCacheDeleteAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- DisqusCacheDeleteAuto PROCEDURE
-- Generated 11/4/2015 10:49:28 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for DisqusCache

CREATE PROCEDURE DisqusCacheDeleteAuto

@DisqusCacheID INT

AS 

DELETE FROM [dbo].[DisqusCache]

WHERE

	[DisqusCacheID] = @DisqusCacheID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure DisqusCacheDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

GO
 
-- END OF PROCEDURE


SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

