
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[txtimport].[TextImportBatchUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [txtimport].[TextImportBatchUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Update Procedure for txtimport.TextImportBatch
-- Do not modify the contents of this procedure.
-- Generated 9/14/2018 11:08:59 AM

CREATE PROCEDURE txtimport.TextImportBatchUpdateAuto

@TextImportBatchID INT,
@TextImportBatchStatusID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [txtimport].[TextImportBatch]
SET
	[TextImportBatchStatusID] = @TextImportBatchStatusID,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[TextImportBatchID] = @TextImportBatchID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure txtimport.TextImportBatchUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[TextImportBatchID],
		[TextImportBatchStatusID],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
	FROM [txtimport].[TextImportBatch]
	WHERE
		[TextImportBatchID] = @TextImportBatchID
	
	RETURN -- update successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

