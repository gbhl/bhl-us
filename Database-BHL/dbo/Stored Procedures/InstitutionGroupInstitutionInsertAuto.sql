
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InstitutionGroupInstitutionInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InstitutionGroupInstitutionInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for dbo.InstitutionGroupInstitution
-- Do not modify the contents of this procedure.
-- Generated 3/31/2020 12:15:15 PM

CREATE PROCEDURE dbo.InstitutionGroupInstitutionInsertAuto

@InstitutionGroupInstitutionID INT OUTPUT,
@InstitutionGroupID INT,
@InstitutionCode NVARCHAR(10),
@CreationUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[InstitutionGroupInstitution]
( 	[InstitutionGroupID],
	[InstitutionCode],
	[CreationDate],
	[CreationUserID] )
VALUES
( 	@InstitutionGroupID,
	@InstitutionCode,
	getdate(),
	@CreationUserID )

SET @InstitutionGroupInstitutionID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.InstitutionGroupInstitutionInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[InstitutionGroupInstitutionID],
		[InstitutionGroupID],
		[InstitutionCode],
		[CreationDate],
		[CreationUserID]	
	FROM [dbo].[InstitutionGroupInstitution]
	WHERE
		[InstitutionGroupInstitutionID] = @InstitutionGroupInstitutionID
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

