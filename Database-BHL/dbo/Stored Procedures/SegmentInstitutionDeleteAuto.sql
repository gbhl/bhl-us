
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SegmentInstitutionDeleteAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[SegmentInstitutionDeleteAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Delete Procedure for dbo.SegmentInstitution
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:31:45 AM

CREATE PROCEDURE dbo.SegmentInstitutionDeleteAuto

@SegmentInstitutionID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[dbo].[SegmentInstitution]
WHERE	
	[SegmentInstitutionID] = @SegmentInstitutionID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.SegmentInstitutionDeleteAuto. No information was deleted as a result of this request.', 16, 1)
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

