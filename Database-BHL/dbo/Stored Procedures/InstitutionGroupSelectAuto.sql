
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InstitutionGroupSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InstitutionGroupSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for dbo.InstitutionGroup
-- Do not modify the contents of this procedure.
-- Generated 3/31/2020 12:15:04 PM

CREATE PROCEDURE [dbo].[InstitutionGroupSelectAuto]

@InstitutionGroupID INT

AS 

SET NOCOUNT ON

SELECT	
	[InstitutionGroupID],
	[InstitutionGroupName],
	[InstitutionGroupDescription],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[dbo].[InstitutionGroup]
WHERE	
	[InstitutionGroupID] = @InstitutionGroupID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.InstitutionGroupSelectAuto. No information was selected.', 16, 1)
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

