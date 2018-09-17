
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[txtimport].[TextImportBatchFileStatusDeleteAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [txtimport].[TextImportBatchFileStatusDeleteAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Delete Procedure for txtimport.TextImportBatchFileStatus
-- Do not modify the contents of this procedure.
-- Generated 9/17/2018 2:48:01 PM

CREATE PROCEDURE txtimport.TextImportBatchFileStatusDeleteAuto

@TextImportBatchFileStatusID INT

AS 

SET NOCOUNT ON

DELETE 
FROM	
	[txtimport].[TextImportBatchFileStatus]
WHERE	
	[TextImportBatchFileStatusID] = @TextImportBatchFileStatusID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure txtimport.TextImportBatchFileStatusDeleteAuto. No information was deleted as a result of this request.', 16, 1)
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

