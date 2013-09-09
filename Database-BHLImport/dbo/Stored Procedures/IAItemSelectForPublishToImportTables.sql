CREATE PROCEDURE [dbo].[IAItemSelectForPublishToImportTables]

@IAIdentifier NVARCHAR(50) = ''

AS
SET NOCOUNT ON

SELECT 

	[ItemID],
	[ItemStatusID],
	[IAIdentifierPrefix],
	[IAIdentifier],
	[Sponsor],
	[SponsorName],
	[ScanningCenter],
	[CallNumber],
	[ImageCount],
	[IdentifierAccessUrl],
	[Volume],
	[Note],
	[ScanOperator],
	[ScanDate],
	[ExternalStatus],
	[MARCBibID],
	[BarCode],
	[IADateStamp],
	[IAAddedDate],
	[LastOAIDataHarvestDate],
	[LastXMLDataHarvestDate],
	[LastProductionDate],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IAItem]

WHERE
	([IAIdentifier] = @IAIdentifier OR @IAIdentifier = '')
AND	([ItemStatusID] = 30)	-- Status = Approved


IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemSelectForPublishToImportTables. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

