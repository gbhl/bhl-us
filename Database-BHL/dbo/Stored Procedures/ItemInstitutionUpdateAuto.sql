
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[ItemInstitutionUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[ItemInstitutionUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Update Procedure for dbo.ItemInstitution
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:31:31 AM

CREATE PROCEDURE dbo.ItemInstitutionUpdateAuto

@ItemInstitutionID INT,
@ItemID INT,
@InstitutionCode NVARCHAR(10),
@InstitutionRoleID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[ItemInstitution]
SET
	[ItemID] = @ItemID,
	[InstitutionCode] = @InstitutionCode,
	[InstitutionRoleID] = @InstitutionRoleID,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[ItemInstitutionID] = @ItemInstitutionID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemInstitutionUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[ItemInstitutionID],
		[ItemID],
		[InstitutionCode],
		[InstitutionRoleID],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
	FROM [dbo].[ItemInstitution]
	WHERE
		[ItemInstitutionID] = @ItemInstitutionID
	
	RETURN -- update successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

