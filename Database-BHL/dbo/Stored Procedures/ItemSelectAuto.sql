
-- ItemSelectAuto PROCEDURE
-- Generated 6/18/2013 3:43:19 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Item

CREATE PROCEDURE ItemSelectAuto

@ItemID INT

AS 

SET NOCOUNT ON

SELECT 

	[ItemID],
	[PrimaryTitleID],
	[RedirectItemID],
	[ThumbnailPageID],
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
	[ExternalUrl]

FROM [dbo].[Item]

WHERE
	[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

