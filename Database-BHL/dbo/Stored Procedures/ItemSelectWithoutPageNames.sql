
CREATE PROCEDURE [dbo].[ItemSelectWithoutPageNames]

AS 

SET NOCOUNT ON

SELECT 

	[ItemID],
	[PrimaryTitleID],
	[BarCode],
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
		[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[ItemStatusID],
	[ItemSourceID],
	[ScanningUser],
	[ScanningDate],
	[Year],
	[IdentifierBib],
	[PaginationCompleteUserID],
	[PaginationCompleteDate],
	[PaginationStatusID],
	[PaginationStatusUserID],
	[PaginationStatusDate],
	[LastPageNameLookupDate]

FROM [dbo].[Item]

WHERE
	[LastPageNameLookupDate] IS NULL
AND	[ItemStatusID] = 40

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemSelectWithoutPageNames. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
