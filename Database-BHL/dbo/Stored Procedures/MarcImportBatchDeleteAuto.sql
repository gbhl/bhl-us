
-- MarcImportBatchDeleteAuto PROCEDURE
-- Generated 4/16/2009 11:23:06 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for MarcImportBatch

CREATE PROCEDURE MarcImportBatchDeleteAuto

@MarcImportBatchID INT

AS 

DELETE FROM [dbo].[MarcImportBatch]

WHERE

	[MarcImportBatchID] = @MarcImportBatchID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcImportBatchDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

