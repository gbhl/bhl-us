
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InstitutionGroupInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InstitutionGroupInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for dbo.InstitutionGroup
-- Do not modify the contents of this procedure.
-- Generated 3/31/2020 12:15:04 PM

CREATE PROCEDURE dbo.InstitutionGroupInsertAuto

@InstitutionGroupID INT OUTPUT,
@InstitutionGroupName NVARCHAR(300),
@InstitutionGroupDescription NVARCHAR(MAX),
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[InstitutionGroup]
( 	[InstitutionGroupName],
	[InstitutionGroupDescription],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@InstitutionGroupName,
	@InstitutionGroupDescription,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @InstitutionGroupID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.InstitutionGroupInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[InstitutionGroupID],
		[InstitutionGroupName],
		[InstitutionGroupDescription],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	
	FROM [dbo].[InstitutionGroup]
	WHERE
		[InstitutionGroupID] = @InstitutionGroupID
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

