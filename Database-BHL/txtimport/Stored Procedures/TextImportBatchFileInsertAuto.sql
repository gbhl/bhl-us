
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[txtimport].[TextImportBatchFileInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[TextImportBatchFileInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for txtimport.TextImportBatchFile
-- Do not modify the contents of this procedure.
-- Generated 9/14/2018 11:09:07 AM

CREATE PROCEDURE txtimport.TextImportBatchFileInsertAuto

@TextImportBatchFileID INT OUTPUT,
@TextImportBatchID INT,
@TextImportBatchFileStatusID INT,
@ItemID INT = null,
@Filename NVARCHAR(500),
@FileFormat NVARCHAR(100),
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [txtimport].[TextImportBatchFile]
( 	[TextImportBatchID],
	[TextImportBatchFileStatusID],
	[ItemID],
	[Filename],
	[FileFormat],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@TextImportBatchID,
	@TextImportBatchFileStatusID,
	@ItemID,
	@Filename,
	@FileFormat,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @TextImportBatchFileID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure txtimport.TextImportBatchFileInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[TextImportBatchFileID],
		[TextImportBatchID],
		[TextImportBatchFileStatusID],
		[ItemID],
		[Filename],
		[FileFormat],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	
	FROM [txtimport].[TextImportBatchFile]
	WHERE
		[TextImportBatchFileID] = @TextImportBatchFileID
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

