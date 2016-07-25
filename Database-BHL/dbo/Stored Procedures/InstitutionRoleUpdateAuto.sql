
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InstitutionRoleUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InstitutionRoleUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Update Procedure for dbo.InstitutionRole
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:31:15 AM

CREATE PROCEDURE dbo.InstitutionRoleUpdateAuto

@InstitutionRoleID INT,
@InstitutionRoleName NVARCHAR(100),
@InstitutionRoleLabel NVARCHAR(100),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[InstitutionRole]
SET
	[InstitutionRoleName] = @InstitutionRoleName,
	[InstitutionRoleLabel] = @InstitutionRoleLabel,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[InstitutionRoleID] = @InstitutionRoleID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.InstitutionRoleUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[InstitutionRoleID],
		[InstitutionRoleName],
		[InstitutionRoleLabel],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
	FROM [dbo].[InstitutionRole]
	WHERE
		[InstitutionRoleID] = @InstitutionRoleID
	
	RETURN -- update successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

