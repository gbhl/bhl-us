
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InstitutionRoleSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InstitutionRoleSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for dbo.InstitutionRole
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:31:15 AM

CREATE PROCEDURE [dbo].[InstitutionRoleSelectAuto]

@InstitutionRoleID INT

AS 

SET NOCOUNT ON

SELECT	
	[InstitutionRoleID],
	[InstitutionRoleName],
	[InstitutionRoleLabel],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[dbo].[InstitutionRole]
WHERE	
	[InstitutionRoleID] = @InstitutionRoleID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.InstitutionRoleSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

