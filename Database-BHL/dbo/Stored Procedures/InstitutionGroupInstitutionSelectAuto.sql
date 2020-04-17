
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InstitutionGroupInstitutionSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InstitutionGroupInstitutionSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for dbo.InstitutionGroupInstitution
-- Do not modify the contents of this procedure.
-- Generated 3/31/2020 12:15:15 PM

CREATE PROCEDURE [dbo].[InstitutionGroupInstitutionSelectAuto]

@InstitutionGroupInstitutionID INT

AS 

SET NOCOUNT ON

SELECT	
	[InstitutionGroupInstitutionID],
	[InstitutionGroupID],
	[InstitutionCode],
	[CreationDate],
	[CreationUserID]
FROM	
	[dbo].[InstitutionGroupInstitution]
WHERE	
	[InstitutionGroupInstitutionID] = @InstitutionGroupInstitutionID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.InstitutionGroupInstitutionSelectAuto. No information was selected.', 16, 1)
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

