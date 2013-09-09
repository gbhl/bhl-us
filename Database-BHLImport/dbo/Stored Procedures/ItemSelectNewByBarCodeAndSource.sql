
CREATE PROCEDURE [dbo].[ItemSelectNewByBarCodeAndSource]

@BarCode NVARCHAR(40),
@ImportSourceID INT

AS 

SET NOCOUNT ON

SELECT 

	[ItemID],
	[ImportKey],
	[ImportStatusID],
	[ImportSourceID],
	[MARCBibID],
	[BarCode],
	[ItemSequence],
	[MARCItemID],
	[CallNumber],
	[Volume],
	[InstitutionCode],
	[LanguageCode],
	[ItemDescription],
	[ScannedBy],
	[PDFSize],
	[VaultID],
	[NumberOfFiles],
	[Note],
	[ItemStatusID],
	[ScanningUser],
	[ScanningDate],
	[Year],
	[IdentifierBib],
	[ZQuery],
	[LicenseUrl],
	[Rights],
	[DueDiligence],
	[CopyrightStatus],
	[CopyrightRegion],
	[CopyrightComment],
	[CopyrightEvidence],
	[CopyrightEvidenceOperator],
	[CopyrightEvidenceDate],
	[PaginationCompleteUserID],
	[PaginationCompleteDate],
	[PaginationStatusID],
	[PaginationStatusUserID],
	[PaginationStatusDate],
	[LastPageNameLookupDate],
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ExternalCreationUser],
	[ExternalLastModifiedUser],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[Item]

WHERE
	[BarCode] = @BarCode
AND [ImportSourceID] = @ImportSourceID
AND	[ImportStatusID] = 10  -- new only

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemSelectNewByBarCodeAndSource. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
