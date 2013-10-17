CREATE PROCEDURE dbo.OAIHarvestSetSelectAll

@OnlyActive smallint = 0

AS

BEGIN

SET NOCOUNT ON

SELECT	HarvestSetID,
		RepositoryName,
		BaseUrl,
		HarvestSetName,
		SetName,
		SetSpec, 
		Prefix, 
		DeletedRecord,
		Granularity,
		[Namespace],
		[Schema],
		AssemblyName
FROM	vwOAIHarvestSet
WHERE	IsActive = 1 
OR		@OnlyActive = 0

END