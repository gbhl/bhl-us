
-- MarcUpdateAuto PROCEDURE
-- Generated 4/21/2009 3:39:50 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Marc

CREATE PROCEDURE MarcUpdateAuto

@MarcID INT,
@MarcImportStatusID INT,
@MarcImportBatchID INT,
@MarcFileLocation NVARCHAR(500),
@InstitutionCode NVARCHAR(10),
@Leader NVARCHAR(200),
@TitleID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Marc]

SET

	[MarcImportStatusID] = @MarcImportStatusID,
	[MarcImportBatchID] = @MarcImportBatchID,
	[MarcFileLocation] = @MarcFileLocation,
	[InstitutionCode] = @InstitutionCode,
	[Leader] = @Leader,
	[TitleID] = @TitleID,
	[LastModifiedDate] = getdate()

WHERE
	[MarcID] = @MarcID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

