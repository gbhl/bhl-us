
CREATE PROCEDURE [dbo].[SearchCatalogCheckStatus]

AS

BEGIN

/*------------------------------------------------------------
 *
 * Checks whether or not the full-text searchcatalog tables
 * are ready for use.
 *
 * Returns 1 if the searchcatalog is ready for use
 * Returns 0 if the searchcatalog is offline or unpopulated
 *
 *-----------------------------------------------------------*/

SET NOCOUNT ON

DECLARE @Status int
SET @Status = 1

-- See if the catalog has been marked offline
DECLARE @IsOffline int
SELECT	@IsOffline = ConfigurationValue 
FROM	dbo.Configuration 
WHERE	ConfigurationName = 'SearchCatalogOffline'

-- See if SQL Server is performing full-text catalog population 
DECLARE @IsPopulating int
SELECT @IsPopulating = FULLTEXTCATALOGPROPERTY('BHLSearchCatalog', 'Populatestatus')

IF (@IsOffline = 1 OR @IsPopulating = 1) SET @Status = 0

RETURN @Status

END

