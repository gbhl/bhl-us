
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[txtimport].[TextImportBatchDeleteAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [txtimport].[TextImportBatchDeleteAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Delete Procedure for txtimport.TextImportBatch
-- Do not modify the contents of this procedure.
-- Generated 9/14/2018 11:08:59 AM

CREATE PROCEDURE txtimport.TextImportBatchDeleteAuto

@TextImportBatchID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[txtimport].[TextImportBatch]
WHERE	
	[TextImportBatchID] = @TextImportBatchID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure txtimport.TextImportBatchDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

