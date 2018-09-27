
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[txtimport].[TextImportBatchFileDeleteAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [txtimport].[TextImportBatchFileDeleteAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Delete Procedure for txtimport.TextImportBatchFile
-- Do not modify the contents of this procedure.
-- Generated 9/26/2018 8:45:51 PM

CREATE PROCEDURE txtimport.TextImportBatchFileDeleteAuto

@TextImportBatchFileID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[txtimport].[TextImportBatchFile]
WHERE	
	[TextImportBatchFileID] = @TextImportBatchFileID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure txtimport.TextImportBatchFileDeleteAuto. No information was deleted as a result of this request.', 16, 1)
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

