
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[txtimport].[TextImportBatchStatusUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [txtimport].[TextImportBatchStatusUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Update Procedure for txtimport.TextImportBatchStatus
-- Do not modify the contents of this procedure.
-- Generated 9/17/2018 2:47:55 PM

CREATE PROCEDURE txtimport.TextImportBatchStatusUpdateAuto

@TextImportBatchStatusID INT,
@StatusName NVARCHAR(50),
@StatusDescription NVARCHAR(500),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [txtimport].[TextImportBatchStatus]
SET
	[TextImportBatchStatusID] = @TextImportBatchStatusID,
	[StatusName] = @StatusName,
	[StatusDescription] = @StatusDescription,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[TextImportBatchStatusID] = @TextImportBatchStatusID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure txtimport.TextImportBatchStatusUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[TextImportBatchStatusID],
		[StatusName],
		[StatusDescription],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
	FROM [txtimport].[TextImportBatchStatus]
	WHERE
		[TextImportBatchStatusID] = @TextImportBatchStatusID
	
	RETURN -- update successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

