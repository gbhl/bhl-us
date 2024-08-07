CREATE PROCEDURE [dbo].[ExportKeyword]

AS

BEGIN

SET NOCOUNT ON

SELECT	tk.TitleID, 
		k.Keyword AS Subject,
		MAX(c.HasLocalContent) AS HasLocalContent,
		MAX(c.HasExternalContent) AS HasExternalContent,
		CONVERT(nvarchar(16), MIN(tk.CreationDate), 120) AS CreationDate
FROM	dbo.Title t
		INNER JOIN dbo.TitleKeyword tk ON t.TitleID = tk.TitleID
		INNER JOIN dbo.Keyword k ON tk.KeywordID = k.KeywordID
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	k.Keyword <> ''
GROUP BY
		tk.TitleID,
		k.Keyword

END

GO
