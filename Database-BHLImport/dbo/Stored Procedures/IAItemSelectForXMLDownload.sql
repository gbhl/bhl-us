﻿
CREATE PROCEDURE [dbo].[IAItemSelectForXMLDownload]

@IAIdentifier NVARCHAR(50) = ''

AS
SET NOCOUNT ON

SELECT 

	[ItemID],
	[ItemStatusID],
	[LocalFileFolder],
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
	[NoMARCOk],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IAItem]

WHERE
	([IAIdentifier] = @IAIdentifier OR @IAIdentifier = '')
AND	(ISNULL([IADateStamp], '1/1/1900 12:00:01') > ISNULL([LastXMLDataHarvestDate], '1/1/1900') OR ItemStatusID = 20)
AND	[ItemStatusID] <> 99	-- ignore items in error
AND [ItemStatusID] <> 98	-- ignore items in error
AND [ItemStatusID] <> 97	-- ignore items in error
AND [ItemStatusID] <> 90	-- ignore items in error

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemSelectForXMLDownload. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END



