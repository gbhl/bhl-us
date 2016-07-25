
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SegmentInstitutionUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[SegmentInstitutionUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Update Procedure for dbo.SegmentInstitution
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:31:45 AM

CREATE PROCEDURE dbo.SegmentInstitutionUpdateAuto

@SegmentInstitutionID INT,
@SegmentID INT,
@InstitutionCode NVARCHAR(10),
@InstitutionRoleID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[SegmentInstitution]
SET
	[SegmentID] = @SegmentID,
	[InstitutionCode] = @InstitutionCode,
	[InstitutionRoleID] = @InstitutionRoleID,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[SegmentInstitutionID] = @SegmentInstitutionID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.SegmentInstitutionUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[SegmentInstitutionID],
		[SegmentID],
		[InstitutionCode],
		[InstitutionRoleID],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
	FROM [dbo].[SegmentInstitution]
	WHERE
		[SegmentInstitutionID] = @SegmentInstitutionID
	
	RETURN -- update successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

