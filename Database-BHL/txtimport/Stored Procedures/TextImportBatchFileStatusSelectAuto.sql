
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[txtimport].[TextImportBatchFileStatusSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [txtimport].[TextImportBatchFileStatusSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for txtimport.TextImportBatchFileStatus
-- Do not modify the contents of this procedure.
-- Generated 9/17/2018 2:48:01 PM

CREATE PROCEDURE [txtimport].[TextImportBatchFileStatusSelectAuto]

@TextImportBatchFileStatusID INT

AS 

SET NOCOUNT ON

SELECT	
	[TextImportBatchFileStatusID],
	[StatusName],
	[StatusDescription],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[txtimport].[TextImportBatchFileStatus]
WHERE	
	[TextImportBatchFileStatusID] = @TextImportBatchFileStatusID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure txtimport.TextImportBatchFileStatusSelectAuto. No information was selected.', 16, 1)
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

