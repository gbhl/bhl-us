
-- ItemSelectAuto PROCEDURE
-- Generated 12/18/2008 2:14:22 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Item

CREATE PROCEDURE ItemSelectAuto

@ItemID INT

AS 

SET NOCOUNT ON

SELECT 

	[ItemID],
	[ImportKey],
	[ImportStatusID],
	[ImportSourceID],
	[MARCBibID],
	[BarCode],
	[ItemSequence],
	[MARCItemID],
	[CallNumber],
	[Volume],
	[InstitutionCode],
	[LanguageCode],
	[Sponsor],
	[ItemDescription],
	[ScannedBy],
	[PDFSize],
	[VaultID],
	[NumberOfFiles],
	[Note],
	[ItemStatusID],
	[ScanningUser],
	[ScanningDate],
	[Year],
	[IdentifierBib],
	[ZQuery],
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
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ExternalCreationUser],
	[ExternalLastModifiedUser],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate]

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

