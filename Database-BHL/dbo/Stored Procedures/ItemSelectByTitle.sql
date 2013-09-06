
CREATE PROCEDURE [dbo].[ItemSelectByTitle]
@TitleId int
AS 

SET NOCOUNT ON

SELECT 
	i.[ItemID],
	i.[PrimaryTitleID],
	i.[BarCode],
	ti.[ItemSequence],
	i.[MARCItemID],
	i.[CallNumber],
	i.[Volume],
	i.[InstitutionCode],
	i.[LanguageCode],
	i.[ItemDescription],
	i.[ScannedBy],
	i.[PDFSize],
	i.[VaultID],
	i.[NumberOfFiles],
	i.[Note],
	i.[CreationDate],
	i.[LastModifiedDate],
	i.[CreationUserID],
	i.[LastModifiedUserID],
	i.[ItemStatusID],
	i.[ItemSourceID],
	ISNULL(s.[DownloadUrl], '') AS [DownloadUrl],
	i.[ScanningDate],
	i.[Year],
	i.[IdentifierBib],
	i.[ZQuery],
	i.[PaginationCompleteUserID],
	i.[PaginationCompleteDate]
FROM [dbo].[Item] i LEFT JOIN [dbo].[ItemSource] s
		ON i.[ItemSourceID] = s.[ItemSourceID]
	INNER JOIN [dbo].[TitleItem] ti
		ON i.ItemID = ti.ItemID

WHERE ti.TitleId = @TitleId
ORDER BY ti.ItemSequence


