
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[txtimport].[TextImportBatchSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [txtimport].[TextImportBatchSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for txtimport.TextImportBatch
-- Do not modify the contents of this procedure.
-- Generated 9/14/2018 11:08:59 AM

CREATE PROCEDURE [txtimport].[TextImportBatchSelectAuto]

@TextImportBatchID INT

AS 

SET NOCOUNT ON

SELECT	
	[TextImportBatchID],
	[TextImportBatchStatusID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[txtimport].[TextImportBatch]
WHERE	
	[TextImportBatchID] = @TextImportBatchID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure txtimport.TextImportBatchSelectAuto. No information was selected.', 16, 1)
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

