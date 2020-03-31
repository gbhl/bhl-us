
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InstitutionGroupDeleteAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InstitutionGroupDeleteAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Delete Procedure for dbo.InstitutionGroup
-- Do not modify the contents of this procedure.
-- Generated 3/31/2020 12:15:04 PM

CREATE PROCEDURE dbo.InstitutionGroupDeleteAuto

@InstitutionGroupID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[dbo].[InstitutionGroup]
WHERE	
	[InstitutionGroupID] = @InstitutionGroupID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.InstitutionGroupDeleteAuto. No information was deleted as a result of this request.', 16, 1)
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

