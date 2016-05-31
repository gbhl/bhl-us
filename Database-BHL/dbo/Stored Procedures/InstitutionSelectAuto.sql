
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InstitutionSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InstitutionSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for dbo.Institution
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:32:10 AM

CREATE PROCEDURE [dbo].[InstitutionSelectAuto]

@InstitutionCode NVARCHAR(10)

AS 

SET NOCOUNT ON

SELECT	
	[InstitutionCode],
	[InstitutionName],
	[Note],
	[InstitutionUrl],
	[BHLMemberLibrary],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[dbo].[Institution]
WHERE	
	[InstitutionCode] = @InstitutionCode

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.InstitutionSelectAuto. No information was selected.', 16, 1)
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

