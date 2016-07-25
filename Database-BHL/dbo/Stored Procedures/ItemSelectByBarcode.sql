CREATE PROCEDURE [dbo].[ItemSelectByBarcode]

@BarCode nvarchar(40)

AS 

SET NOCOUNT ON

SELECT	[ItemID],
		[PrimaryTitleID],
		[BarCode],
		[MARCItemID],
		[CallNumber],
		[Volume],
		[LanguageCode],
		[Sponsor],
		[ItemDescription],
		[ScannedBy],
		[PDFSize],
		[VaultID],
		[Note],
		[ItemStatusID],
		[ItemSourceID],
		[ScanningUser],
		[ScanningDate],
		[Year],
		[IdentifierBib],
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
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
FROM	[dbo].[Item] 
WHERE	[BarCode] = @BarCode

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemSelectByBarcode. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
