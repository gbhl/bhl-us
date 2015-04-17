CREATE PROCEDURE dbo.ExportKeyword

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT 
		tk.TitleID, 
		k.Keyword AS Subject, 
		CONVERT(nvarchar(16), tk.CreationDate, 120) AS CreationDate
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN TitleKeyword tk WITH (NOLOCK) ON t.TitleID = tk.TitleID
		INNER JOIN Keyword k WITH (NOLOCK) ON tk.KeywordID = k.KeywordID
		INNER JOIN Item i WITH (NOLOCK) ON t.TitleID = i.PrimaryTitleID
WHERE	t.PublishReady = 1
AND		k.Keyword <> ''

END

