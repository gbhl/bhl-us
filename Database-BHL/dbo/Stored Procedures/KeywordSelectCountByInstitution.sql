CREATE PROCEDURE [dbo].[KeywordSelectCountByInstitution] 

@Number INT = 100,
@InstitutionCode nvarchar(10) = '',
@LanguageCode nvarchar(10) = ''

AS
BEGIN
	SET NOCOUNT ON

	SELECT DISTINCT	t.TitleID
	INTO #tmpTitle
	FROM dbo.Title t LEFT JOIN dbo.TitleItem ti
			ON t.TitleID = ti.TitleID	
		LEFT JOIN dbo.Item i 
			ON ti.ItemID = i.ItemID
		LEFT JOIN dbo.TitleLanguage tl
			ON t.TitleID = tl.TitleID
		LEFT JOIN dbo.ItemLanguage il
			ON i.ItemID = il.ItemID
	WHERE	
		t.PublishReady = 1 AND
		(t.InstitutionCode = @InstitutionCode OR
			i.InstitutionCode = @InstitutionCode OR
			@InstitutionCode = '') AND
		(t.LanguageCode = @LanguageCode OR
			i.LanguageCode = @LanguageCode OR
			ISNULL(tl.LanguageCode, '') = @LanguageCode OR
			ISNULL(il.LanguageCode, '') = @LanguageCode OR
			@LanguageCode = '')

	-- Make the total number of titles for the specified institution and language be
	-- the first item in the result set
	SELECT
		NULL AS [Keyword],
		COUNT(TitleID) AS [Count]
	FROM #tmpTitle
	UNION
	SELECT 
		Keyword, 
		[Count] * 2
	FROM 
	(
		SELECT	TOP(@Number) v.Keyword,
				COUNT(*) AS [Count]
		FROM	(SELECT DISTINCT TitleID, Keyword
				 FROM dbo.TitleKeywordView) v INNER JOIN #tmpTitle t
					ON v.TitleID = t.TitleID
		GROUP BY v.Keyword
		ORDER BY [Count] DESC, v.Keyword
	) X
	ORDER BY Keyword
END




