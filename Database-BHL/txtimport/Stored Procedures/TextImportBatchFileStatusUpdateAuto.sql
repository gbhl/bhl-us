
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[txtimport].[TextImportBatchFileStatusUpdateAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [txtimport].[TextImportBatchFileStatusUpdateAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Update Procedure for txtimport.TextImportBatchFileStatus
-- Do not modify the contents of this procedure.
-- Generated 9/17/2018 2:48:01 PM

CREATE PROCEDURE txtimport.TextImportBatchFileStatusUpdateAuto

@TextImportBatchFileStatusID INT,
@StatusName NVARCHAR(50),
@StatusDescription NVARCHAR(500),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [txtimport].[TextImportBatchFileStatus]
SET
	[TextImportBatchFileStatusID] = @TextImportBatchFileStatusID,
	[StatusName] = @StatusName,
	[StatusDescription] = @StatusDescription,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[TextImportBatchFileStatusID] = @TextImportBatchFileStatusID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure txtimport.TextImportBatchFileStatusUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[TextImportBatchFileStatusID],
		[StatusName],
		[StatusDescription],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
	FROM [txtimport].[TextImportBatchFileStatus]
	WHERE
		[TextImportBatchFileStatusID] = @TextImportBatchFileStatusID
	
	RETURN -- update successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

