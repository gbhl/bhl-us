SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------

CREATE PROCEDURE [dbo].[BookSelectByItemID]

@ItemID INT

AS 

SET NOCOUNT ON

SELECT	[BookID],
		[ItemID],
		[RedirectBookID],
		[ThumbnailPageID],
		[LanguageCode],
		[BarCode],
		[MARCItemID],
		[CallNumber],
		[Volume],
		[StartYear],
		[EndYear],
		[StartVolume],
		[EndVolume],
		[StartIssue],
		[EndIssue],
		[StartNumber],
		[EndNumber],
		[StartSeries],
		[EndSeries],
		[StartPart],
		[EndPart],
		[IdentifierBib],
		[ZQuery],
		[Sponsor],
		[ExternalUrl],
		[LicenseUrl],
		[Rights],
		[DueDiligence],
		[CopyrightStatus],
		[CopyrightRegion],
		[CopyrightComment],
		[CopyrightEvidence],
		[ScanningUser],
		[ScanningDate],
		[PaginationStatusID],
		[PaginationStatusDate],
		[PaginationStatusUserID],
		[PaginationCompleteDate],
		[PaginationCompleteUserID],
		[LastPageNameLookupDate],
		[IsVirtual],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
FROM	[dbo].[Book]
WHERE	[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.BookSelectByItemID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


GO
