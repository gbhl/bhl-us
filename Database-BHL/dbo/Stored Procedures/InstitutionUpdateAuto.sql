
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InstitutionUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InstitutionUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Update Procedure for dbo.Institution
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:32:10 AM

CREATE PROCEDURE dbo.InstitutionUpdateAuto

@InstitutionCode NVARCHAR(10),
@InstitutionName NVARCHAR(255),
@Note NVARCHAR(255),
@InstitutionUrl NVARCHAR(255),
@BHLMemberLibrary BIT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Institution]
SET
	[InstitutionCode] = @InstitutionCode,
	[InstitutionName] = @InstitutionName,
	[Note] = @Note,
	[InstitutionUrl] = @InstitutionUrl,
	[BHLMemberLibrary] = @BHLMemberLibrary,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[InstitutionCode] = @InstitutionCode
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.InstitutionUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	FROM [dbo].[Institution]
	WHERE
		[InstitutionCode] = @InstitutionCode
	
	RETURN -- update successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

