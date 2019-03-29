CREATE PROCEDURE [dbo].[ExportKeyword]

AS

BEGIN

SET NOCOUNT ON

SELECT	tk.TitleID, 
		k.Keyword AS Subject,
		MAX(c.HasLocalContent) AS HasLocalContent,
		MAX(c.HasExternalContent) AS HasExternalContent,
		CONVERT(nvarchar(16), tk.CreationDate, 120) AS CreationDate
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.TitleKeyword tk WITH (NOLOCK) ON t.TitleID = tk.TitleID
		INNER JOIN dbo.Keyword k WITH (NOLOCK) ON tk.KeywordID = k.KeywordID
		INNER JOIN dbo.Item i WITH (NOLOCK) ON t.TitleID = i.PrimaryTitleID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID
WHERE	t.PublishReady = 1
AND		k.Keyword <> ''
GROUP BY
		tk.TitleID,
		k.Keyword,
		tk.CreationDate

END
