
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SegmentInstitutionSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[SegmentInstitutionSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for dbo.SegmentInstitution
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:31:45 AM

CREATE PROCEDURE [dbo].[SegmentInstitutionSelectAuto]

@SegmentInstitutionID INT

AS 

SET NOCOUNT ON

SELECT	
	[SegmentInstitutionID],
	[SegmentID],
	[InstitutionCode],
	[InstitutionRoleID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[dbo].[SegmentInstitution]
WHERE	
	[SegmentInstitutionID] = @SegmentInstitutionID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.SegmentInstitutionSelectAuto. No information was selected.', 16, 1)
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

