
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InstitutionRoleInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InstitutionRoleInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for dbo.InstitutionRole
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:31:15 AM

CREATE PROCEDURE dbo.InstitutionRoleInsertAuto

@InstitutionRoleID INT OUTPUT,
@InstitutionRoleName NVARCHAR(100),
@InstitutionRoleLabel NVARCHAR(100),
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[InstitutionRole]
( 	[InstitutionRoleName],
	[InstitutionRoleLabel],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@InstitutionRoleName,
	@InstitutionRoleLabel,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @InstitutionRoleID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.InstitutionRoleInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

