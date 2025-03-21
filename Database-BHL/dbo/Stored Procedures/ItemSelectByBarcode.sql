SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ItemSelectByBarcode]

@BarCode nvarchar(200)

AS 

SET NOCOUNT ON

SELECT	b.BookID AS ItemID,
		pt.TitleID AS PrimaryTitleID,
		b.BarCode,
		b.MARCItemID,
		b.CallNumber,
		b.Volume,
		b.LanguageCode,
		b.Sponsor,
		i.ItemDescription,
		CAST(NULL AS int) AS ScannedBy,
		CAST(NULL AS int) AS PDFSize,
		i.VaultID,
		i.Note,
		i.ItemStatusID,
		i.ItemSourceID,
		b.ScanningUser,
		b.ScanningDate,
		b.StartYear,
		b.IdentifierBib,
		b.LicenseUrl,
		b.Rights,
		b.DueDiligence,
		b.CopyrightStatus,
		CAST(NULL AS nvarchar(50)) AS CopyrightRegion,
		CAST(NULL AS nvarchar(max)) AS CopyrightComment,
		CAST(NULL AS nvarchar(max)) CopyrightEvidence,
		CAST(NULL AS nvarchar(100)) CopyrightEvidenceOperator,
		CAST(NULL AS nvarchar(30)) CopyrightEvidenceDate,
		b.PaginationCompleteUserID,
		b.PaginationCompleteDate,
		b.PaginationStatusID,
		b.PaginationStatusUserID,
		b.PaginationStatusDate,
		b.LastPageNameLookupDate,
		b.CreationDate,
		b.LastModifiedDate,
		b.CreationUserID,
		b.LastModifiedUserID
FROM	dbo.Item i
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.vwItemPrimaryTitle pt ON i.ItemID = pt.ItemID
WHERE	b.BarCode = @BarCode

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemSelectByBarcode. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


GO
