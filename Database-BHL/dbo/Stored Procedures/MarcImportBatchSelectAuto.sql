
-- MarcImportBatchSelectAuto PROCEDURE
-- Generated 4/16/2009 11:23:06 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for MarcImportBatch

CREATE PROCEDURE MarcImportBatchSelectAuto

@MarcImportBatchID INT

AS 

SET NOCOUNT ON

SELECT 

	[MarcImportBatchID],
	[FileName],
	[InstitutionCode],
	[CreationDate]

FROM [dbo].[MarcImportBatch]

WHERE
	[MarcImportBatchID] = @MarcImportBatchID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcImportBatchSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

