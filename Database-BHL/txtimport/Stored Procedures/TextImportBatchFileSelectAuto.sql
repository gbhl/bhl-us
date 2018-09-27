
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[txtimport].[TextImportBatchFileSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [txtimport].[TextImportBatchFileSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for txtimport.TextImportBatchFile
-- Do not modify the contents of this procedure.
-- Generated 9/26/2018 8:45:51 PM

CREATE PROCEDURE [txtimport].[TextImportBatchFileSelectAuto]

@TextImportBatchFileID INT

AS 

SET NOCOUNT ON

SELECT	
	[TextImportBatchFileID],
	[TextImportBatchID],
	[TextImportBatchFileStatusID],
	[ItemID],
	[Filename],
	[FileFormat],
	[ErrorMessage],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[txtimport].[TextImportBatchFile]
WHERE	
	[TextImportBatchFileID] = @TextImportBatchFileID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure txtimport.TextImportBatchFileSelectAuto. No information was selected.', 16, 1)
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

