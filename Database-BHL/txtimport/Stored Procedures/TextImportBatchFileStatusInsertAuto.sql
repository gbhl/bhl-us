
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[txtimport].[TextImportBatchFileStatusInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[TextImportBatchFileStatusInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for txtimport.TextImportBatchFileStatus
-- Do not modify the contents of this procedure.
-- Generated 9/17/2018 2:48:01 PM

CREATE PROCEDURE txtimport.TextImportBatchFileStatusInsertAuto

@TextImportBatchFileStatusID INT,
@StatusName NVARCHAR(50),
@StatusDescription NVARCHAR(500),
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [txtimport].[TextImportBatchFileStatus]
( 	[TextImportBatchFileStatusID],
	[StatusName],
	[StatusDescription],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@TextImportBatchFileStatusID,
	@StatusName,
	@StatusDescription,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure txtimport.TextImportBatchFileStatusInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

