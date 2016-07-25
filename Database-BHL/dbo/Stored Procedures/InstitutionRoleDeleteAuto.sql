
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InstitutionRoleDeleteAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InstitutionRoleDeleteAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Delete Procedure for dbo.InstitutionRole
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:31:15 AM

CREATE PROCEDURE dbo.InstitutionRoleDeleteAuto

@InstitutionRoleID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[dbo].[InstitutionRole]
WHERE	
	[InstitutionRoleID] = @InstitutionRoleID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.InstitutionRoleDeleteAuto. No information was deleted as a result of this request.', 16, 1)
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

