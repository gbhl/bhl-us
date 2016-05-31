
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[ItemInstitutionInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[ItemInstitutionInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for dbo.ItemInstitution
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:31:31 AM

CREATE PROCEDURE dbo.ItemInstitutionInsertAuto

@ItemInstitutionID INT OUTPUT,
@ItemID INT,
@InstitutionCode NVARCHAR(10),
@InstitutionRoleID INT,
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[ItemInstitution]
( 	[ItemID],
	[InstitutionCode],
	[InstitutionRoleID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@ItemID,
	@InstitutionCode,
	@InstitutionRoleID,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @ItemInstitutionID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemInstitutionInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

