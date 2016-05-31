
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[InstitutionInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[InstitutionInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for dbo.Institution
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:32:10 AM

CREATE PROCEDURE dbo.InstitutionInsertAuto

@InstitutionCode NVARCHAR(10),
@InstitutionName NVARCHAR(255),
@Note NVARCHAR(255) = null,
@InstitutionUrl NVARCHAR(255) = null,
@BHLMemberLibrary BIT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Institution]
( 	[InstitutionCode],
	[InstitutionName],
	[Note],
	[InstitutionUrl],
	[BHLMemberLibrary],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@InstitutionCode,
	@InstitutionName,
	@Note,
	@InstitutionUrl,
	@BHLMemberLibrary,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.InstitutionInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

