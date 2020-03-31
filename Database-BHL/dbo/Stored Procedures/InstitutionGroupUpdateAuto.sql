
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InstitutionGroupUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InstitutionGroupUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Update Procedure for dbo.InstitutionGroup
-- Do not modify the contents of this procedure.
-- Generated 3/31/2020 12:15:04 PM

CREATE PROCEDURE dbo.InstitutionGroupUpdateAuto

@InstitutionGroupID INT,
@InstitutionGroupName NVARCHAR(300),
@InstitutionGroupDescription NVARCHAR(MAX),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[InstitutionGroup]
SET
	[InstitutionGroupName] = @InstitutionGroupName,
	[InstitutionGroupDescription] = @InstitutionGroupDescription,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[InstitutionGroupID] = @InstitutionGroupID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.InstitutionGroupUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[InstitutionGroupID],
		[InstitutionGroupName],
		[InstitutionGroupDescription],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
	FROM [dbo].[InstitutionGroup]
	WHERE
		[InstitutionGroupID] = @InstitutionGroupID
	
	RETURN -- update successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

