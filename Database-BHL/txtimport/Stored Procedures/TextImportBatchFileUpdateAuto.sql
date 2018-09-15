
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[txtimport].[TextImportBatchFileUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [txtimport].[TextImportBatchFileUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Update Procedure for txtimport.TextImportBatchFile
-- Do not modify the contents of this procedure.
-- Generated 9/14/2018 11:09:07 AM

CREATE PROCEDURE txtimport.TextImportBatchFileUpdateAuto

@TextImportBatchFileID INT,
@TextImportBatchID INT,
@TextImportBatchFileStatusID INT,
@ItemID INT,
@Filename NVARCHAR(500),
@FileFormat NVARCHAR(100),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [txtimport].[TextImportBatchFile]
SET
	[TextImportBatchID] = @TextImportBatchID,
	[TextImportBatchFileStatusID] = @TextImportBatchFileStatusID,
	[ItemID] = @ItemID,
	[Filename] = @Filename,
	[FileFormat] = @FileFormat,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[TextImportBatchFileID] = @TextImportBatchFileID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure txtimport.TextImportBatchFileUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

