
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[txtimport].[TextImportBatchStatusInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[TextImportBatchStatusInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for txtimport.TextImportBatchStatus
-- Do not modify the contents of this procedure.
-- Generated 9/17/2018 2:47:54 PM

CREATE PROCEDURE txtimport.TextImportBatchStatusInsertAuto

@TextImportBatchStatusID INT,
@StatusName NVARCHAR(50),
@StatusDescription NVARCHAR(500),
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [txtimport].[TextImportBatchStatus]
( 	[TextImportBatchStatusID],
	[StatusName],
	[StatusDescription],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@TextImportBatchStatusID,
	@StatusName,
	@StatusDescription,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure txtimport.TextImportBatchStatusInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

