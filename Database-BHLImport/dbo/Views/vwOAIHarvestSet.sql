CREATE VIEW [dbo].[vwOAIHarvestSet]
AS
SELECT	hs.HarvestSetID,
		r.RepositoryName,
		r.BaseUrl,
		hs.HarvestSetName,
		s.SetName,
		s.SetSpec,
		f.Prefix,
		rf.[Namespace],
		rf.[Schema],
		f.AssemblyName
FROM	dbo.OAIHarvestSet hs
		LEFT JOIN dbo.OAISet s ON hs.SetID = s.SetID
		INNER JOIN dbo.OAIRepositoryFormat rf ON hs.RepositoryFormatID = rf.RepositoryFormatID
		INNER JOIN dbo.OAIFormat f ON rf.FormatID = f.FormatID
		INNER JOIN dbo.OAIRepository r ON rf.RepositoryID = r.RepositoryID
	