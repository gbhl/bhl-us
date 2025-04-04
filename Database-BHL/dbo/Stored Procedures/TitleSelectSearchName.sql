SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TitleSelectSearchName]
@Name			varchar(1000),
@LanguageCode	nvarchar(10) = '',
@ReturnCount	int = 100
AS 

SET NOCOUNT ON

SELECT DISTINCT TOP (@ReturnCount)
		t.[TitleID],
		t.[FullTitle],
		t.[SortTitle],
		ISNULL(t.[PartNumber], '') AS PartNumber,
		ISNULL(t.[PartName], '') AS PartName,
		t.[PublicationDetails],
		c.TitleContributors AS InstitutionName
FROM	[dbo].[Title] t
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
		INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		LEFT JOIN dbo.TitleLanguage tl ON t.TitleID = tl.TitleID
		LEFT JOIN dbo.ItemLanguage il ON it.ItemID = il.ItemID
		INNER JOIN dbo.SearchCatalog c ON c.TitleID = t.TitleID AND c.ItemID = b.BookID
WHERE	t.FullTitle LIKE '%' + @Name + '%'
AND		t.PublishReady = 1
AND		(t.LanguageCode = @LanguageCode OR
		b.LanguageCode = @LanguageCode OR
		ISNULL(tl.LanguageCode, '') = @LanguageCode OR
		ISNULL(il.LanguageCode, '') = @LanguageCode OR
		@LanguageCode = '')
ORDER BY t.SortTitle

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleSelectSearchName. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


GO
