
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InstitutionGroupInstitutionUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InstitutionGroupInstitutionUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Update Procedure for dbo.InstitutionGroupInstitution
-- Do not modify the contents of this procedure.
-- Generated 3/31/2020 12:15:15 PM

CREATE PROCEDURE dbo.InstitutionGroupInstitutionUpdateAuto

@InstitutionGroupInstitutionID INT,
@InstitutionGroupID INT,
@InstitutionCode NVARCHAR(10)

AS 

SET NOCOUNT ON

UPDATE [dbo].[InstitutionGroupInstitution]
SET
	[InstitutionGroupID] = @InstitutionGroupID,
	[InstitutionCode] = @InstitutionCode
WHERE
	[InstitutionGroupInstitutionID] = @InstitutionGroupInstitutionID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.InstitutionGroupInstitutionUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

