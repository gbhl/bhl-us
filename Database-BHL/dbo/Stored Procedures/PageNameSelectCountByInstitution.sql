
CREATE PROCEDURE [dbo].[PageNameSelectCountByInstitution] 

@Number INT = 100,
@InstitutionCode nvarchar(10) = ''

AS
BEGIN
	SET NOCOUNT ON

	SELECT DISTINCT	t.TitleID
	INTO #tmpTitle
	FROM dbo.Title t 
		INNER JOIN dbo.Item i	ON t.TitleID = i.PrimaryTitleID
	WHERE	
		t.PublishReady = 1 AND
		(t.InstitutionCode = @InstitutionCode OR
			i.InstitutionCode = @InstitutionCode OR
			@InstitutionCode = '')

	-- Make the total number of titles for the specified institution be
	-- the first item in the result set
	SELECT 
		NULL AS [NameConfirmed], 
		COUNT(DISTINCT t.TitleID) AS [Count]
	FROM dbo.Title t 
		INNER JOIN dbo.Item i ON t.TitleID = i.PrimaryTitleID
		INNER JOIN dbo.Page P ON P.ItemID = i.ItemID
		INNER JOIN dbo.PageName PN ON PN.PageID = P.PageID
	WHERE t.PublishReady = 1
	AND (t.InstitutionCode = @InstitutionCode OR 
		i.InstitutionCode = @InstitutionCode OR 
		@InstitutionCode = '')
	UNION
	SELECT 
		NameConfirmed, 
		[Count]
	FROM 
	(
		SELECT 
			TOP (@Number) PN.NameConfirmed, 
			COUNT(*) AS [Count] 
		FROM dbo.PageName PN 
			INNER JOIN dbo.Page P ON P.PageID = PN.PageID
			INNER JOIN dbo.Item I ON I.ItemID = P.ItemID
			INNER JOIN #tmpTitle t ON I.PrimaryTitleID = t.TitleID
		GROUP BY PN.NameConfirmed
		ORDER BY [Count] DESC
	) X
	ORDER BY [Count] DESC

END

