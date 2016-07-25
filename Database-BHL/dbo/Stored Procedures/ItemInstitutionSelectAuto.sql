
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[ItemInstitutionSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[ItemInstitutionSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for dbo.ItemInstitution
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:31:31 AM

CREATE PROCEDURE [dbo].[ItemInstitutionSelectAuto]

@ItemInstitutionID INT

AS 

SET NOCOUNT ON

SELECT	
	[ItemInstitutionID],
	[ItemID],
	[InstitutionCode],
	[InstitutionRoleID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[dbo].[ItemInstitution]
WHERE	
	[ItemInstitutionID] = @ItemInstitutionID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemInstitutionSelectAuto. No information was selected.', 16, 1)
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

