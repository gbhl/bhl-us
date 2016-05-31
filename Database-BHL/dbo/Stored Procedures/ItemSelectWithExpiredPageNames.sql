CREATE PROCEDURE [dbo].[ItemSelectWithExpiredPageNames]

@MaxAge INT  -- Maximum allowed age of page names (in days)

AS 

SET NOCOUNT ON

SELECT	[ItemID],
		[PrimaryTitleID],
		[BarCode],
		[MARCItemID],
		[CallNumber],
		[Volume],
		[LanguageCode],
		[ItemDescription],
		[ScannedBy],
		[PDFSize],
		[VaultID],
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
FROM	[dbo].[Item]
WHERE	DATEDIFF(day, [LastPageNameLookupDate], GETDATE()) > @MaxAge
AND		[ItemStatusID] = 40

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemSelectWithExpiredPageNames. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
