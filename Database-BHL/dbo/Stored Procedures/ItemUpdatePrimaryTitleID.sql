CREATE PROCEDURE [dbo].[ItemUpdatePrimaryTitleID]

@ItemID INT,
@TitleID INT

AS 

SET NOCOUNT ON

UPDATE	[dbo].[Item]
SET		[PrimaryTitleID] = @TitleID,
		[LastModifiedDate] = GETDATE()
WHERE	[ItemID] = @ItemID
AND		[PrimaryTitleID] <> @TitleID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemUpdatePrimaryTitleID. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT 

		[ItemID],
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
		[ScanningUser],
		[ScanningDate],
		[PaginationCompleteUserID],
		[PaginationCompleteDate],
		[PaginationStatusID],
		[PaginationStatusUserID],
		[PaginationStatusDate],
		[LastPageNameLookupDate]

	FROM [dbo].[Item]

	WHERE
		[ItemID] = @ItemID
	
	RETURN -- update successful
END
