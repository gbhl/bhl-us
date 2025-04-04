
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InstitutionGroupInstitutionDeleteAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InstitutionGroupInstitutionDeleteAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Delete Procedure for dbo.InstitutionGroupInstitution
-- Do not modify the contents of this procedure.
-- Generated 3/31/2020 12:15:15 PM

CREATE PROCEDURE dbo.InstitutionGroupInstitutionDeleteAuto

@InstitutionGroupInstitutionID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[dbo].[InstitutionGroupInstitution]
WHERE	
	[InstitutionGroupInstitutionID] = @InstitutionGroupInstitutionID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.InstitutionGroupInstitutionDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

