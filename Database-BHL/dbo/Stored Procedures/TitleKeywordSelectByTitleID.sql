CREATE PROCEDURE [dbo].[TitleKeywordSelectByTitleID]
	@TitleID int
AS 

SET NOCOUNT ON

SELECT	tk.TitleKeywordID,
		tk.TitleID,
		tk.KeywordID,
		k.Keyword,
		tk.MarcDataFieldTag,
		tk.MarcSubFieldCode,
		tk.CreationDate,
		tk.LastModifiedDate
FROM	dbo.Keyword k INNER JOIN dbo.TitleKeyword tk ON k.KeywordID = tk.KeywordID
WHERE	TitleID = @TitleID
ORDER BY
		Keyword,
		MarcDatafieldTag,
		MarcSubFieldCode



