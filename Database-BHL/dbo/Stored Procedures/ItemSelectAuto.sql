
IF EXISTS(SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[ItemSelectAuto]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[ItemSelectAuto]
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- Select Procedure for dbo.Item
-- Do not modify the contents of this procedure.
-- Generated 6/2/2016 9:32:20 AM

CREATE PROCEDURE [dbo].[ItemSelectAuto]

@ItemID INT

AS 

SET NOCOUNT ON

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
	[LastPageNameLookupDate],
	[ItemSourceID],
	[Year],
	[IdentifierBib],
	[FileRootFolder],
	[ZQuery],
	[Sponsor],
	[LicenseUrl],
	[Rights],
	[DueDiligence],
	[CopyrightStatus],
	[CopyrightRegion],
	[CopyrightComment],
	[CopyrightEvidence],
	[CopyrightEvidenceOperator],
	[CopyrightEvidenceDate],
	[ThumbnailPageID],
	[RedirectItemID],
	[ExternalUrl],
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
	[VolumeReviewed],
	[VolumeReviewedDate],
	[VolumeReviewedUserID]
FROM	
	[dbo].[Item]
WHERE	
	[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.ItemSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
 
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

