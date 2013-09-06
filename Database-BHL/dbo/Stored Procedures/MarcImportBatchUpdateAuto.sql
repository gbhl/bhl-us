
-- MarcImportBatchUpdateAuto PROCEDURE
-- Generated 4/16/2009 11:23:06 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for MarcImportBatch

CREATE PROCEDURE MarcImportBatchUpdateAuto

@MarcImportBatchID INT,
@FileName NVARCHAR(500),
@InstitutionCode NVARCHAR(10)

AS 

SET NOCOUNT ON

UPDATE [dbo].[MarcImportBatch]

SET

	[FileName] = @FileName,
	[InstitutionCode] = @InstitutionCode

WHERE
	[MarcImportBatchID] = @MarcImportBatchID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcImportBatchUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[MarcImportBatchID],
		[FileName],
		[InstitutionCode],
		[CreationDate]

	FROM [dbo].[MarcImportBatch]
	
	WHERE
		[MarcImportBatchID] = @MarcImportBatchID
	
	RETURN -- update successful
END

