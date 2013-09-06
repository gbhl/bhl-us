
-- MarcImportBatchInsertAuto PROCEDURE
-- Generated 4/16/2009 11:23:06 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for MarcImportBatch

CREATE PROCEDURE MarcImportBatchInsertAuto

@MarcImportBatchID INT OUTPUT,
@FileName NVARCHAR(500),
@InstitutionCode NVARCHAR(10) = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[MarcImportBatch]
(
	[FileName],
	[InstitutionCode],
	[CreationDate]
)
VALUES
(
	@FileName,
	@InstitutionCode,
	getdate()
)

SET @MarcImportBatchID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcImportBatchInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

