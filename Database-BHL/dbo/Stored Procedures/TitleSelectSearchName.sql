
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
	I.InstitutionName
FROM [dbo].[Title] t
	LEFT OUTER JOIN Institution I ON I.InstitutionCode = t.InstitutionCode
	INNER JOIN TitleItem TI ON t.TitleID = TI.TitleID
	INNER JOIN Item IT ON TI.ItemID = IT.ItemID
	LEFT JOIN dbo.TitleLanguage tl ON t.TitleID = tl.TitleID
	LEFT JOIN dbo.ItemLanguage il ON it.ItemID = il.ItemID
WHERE t.FullTitle LIKE '%' + @Name + '%'
AND t.PublishReady = 1
AND (t.LanguageCode = @LanguageCode OR
		IT.LanguageCode = @LanguageCode OR
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

