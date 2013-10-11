CREATE PROCEDURE dbo.OAIHarvestSetSelectAll

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
		[Namespace],
		[Schema],
		AssemblyName
FROM	vwOAIHarvestSet

END