
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[txtimport].[TextImportBatchStatusSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [txtimport].[TextImportBatchStatusSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for txtimport.TextImportBatchStatus
-- Do not modify the contents of this procedure.
-- Generated 9/17/2018 2:47:55 PM

CREATE PROCEDURE [txtimport].[TextImportBatchStatusSelectAuto]

@TextImportBatchStatusID INT

AS 

SET NOCOUNT ON

SELECT	
	[TextImportBatchStatusID],
	[StatusName],
	[StatusDescription],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[txtimport].[TextImportBatchStatus]
WHERE	
	[TextImportBatchStatusID] = @TextImportBatchStatusID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure txtimport.TextImportBatchStatusSelectAuto. No information was selected.', 16, 1)
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

