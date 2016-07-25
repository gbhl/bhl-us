
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SegmentInstitutionInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[SegmentInstitutionInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for dbo.SegmentInstitution
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:31:45 AM

CREATE PROCEDURE dbo.SegmentInstitutionInsertAuto

@SegmentInstitutionID INT OUTPUT,
@SegmentID INT,
@InstitutionCode NVARCHAR(10),
@InstitutionRoleID INT,
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[SegmentInstitution]
( 	[SegmentID],
	[InstitutionCode],
	[InstitutionRoleID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@SegmentID,
	@InstitutionCode,
	@InstitutionRoleID,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @SegmentInstitutionID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.SegmentInstitutionInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

