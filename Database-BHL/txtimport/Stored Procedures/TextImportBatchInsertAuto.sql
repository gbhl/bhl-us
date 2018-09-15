
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[txtimport].[TextImportBatchInsertAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[TextImportBatchInsertAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Insert Procedure for txtimport.TextImportBatch
-- Do not modify the contents of this procedure.
-- Generated 9/14/2018 11:08:59 AM

CREATE PROCEDURE txtimport.TextImportBatchInsertAuto

@TextImportBatchID INT OUTPUT,
@TextImportBatchStatusID INT,
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [txtimport].[TextImportBatch]
( 	[TextImportBatchStatusID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@TextImportBatchStatusID,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @TextImportBatchID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure txtimport.TextImportBatchInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

