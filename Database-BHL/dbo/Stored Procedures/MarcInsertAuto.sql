
-- MarcInsertAuto PROCEDURE
-- Generated 4/21/2009 3:39:50 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Marc

CREATE PROCEDURE MarcInsertAuto

@MarcID INT OUTPUT,
@MarcImportStatusID INT,
@MarcImportBatchID INT,
@MarcFileLocation NVARCHAR(500),
@InstitutionCode NVARCHAR(10) = null,
@Leader NVARCHAR(200),
@TitleID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Marc]
(
	[MarcImportStatusID],
	[MarcImportBatchID],
	[MarcFileLocation],
	[InstitutionCode],
	[Leader],
	[TitleID],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@MarcImportStatusID,
	@MarcImportBatchID,
	@MarcFileLocation,
	@InstitutionCode,
	@Leader,
	@TitleID,
	getdate(),
	getdate()
)

SET @MarcID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[MarcID],
		[MarcImportStatusID],
		[MarcImportBatchID],
		[MarcFileLocation],
		[InstitutionCode],
		[Leader],
		[TitleID],
		[CreationDate],
		[LastModifiedDate]	

	FROM [dbo].[Marc]
	
	WHERE
		[MarcID] = @MarcID
	
	RETURN -- insert successful
END

