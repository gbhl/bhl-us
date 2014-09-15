
CREATE PROCEDURE [dbo].[ItemSelectWithoutPageNames]

AS 

SET NOCOUNT ON

SELECT DISTINCT
		i.[ItemID],
		[PrimaryTitleID],
		[BarCode],
		[MARCItemID],
		[CallNumber],
		i.[Volume],
		[InstitutionCode],
		[LanguageCode],
		CONVERT(nvarchar(MAX), [ItemDescription]) AS ItemDescription,
		[ScannedBy],
		[PDFSize],
		[VaultID],
		[NumberOfFiles],
		i.[Note],
		i.[CreationDate],
		i.[LastModifiedDate],
		i.[CreationUserID],
		i.[LastModifiedUserID],
		[ItemStatusID],
		[ItemSourceID],
		[ScanningUser],
		[ScanningDate],
		i.[Year],
		[IdentifierBib],
		[PaginationCompleteUserID],
		[PaginationCompleteDate],
		[PaginationStatusID],
		[PaginationStatusUserID],
		[PaginationStatusDate],
		i.[LastPageNameLookupDate]
FROM	[dbo].[Item] i INNER JOIN dbo.Page p ON i.ItemID = p.ItemID
WHERE	i.[LastPageNameLookupDate] IS NULL
AND		[ItemStatusID] = 40

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemSelectWithoutPageNames. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
