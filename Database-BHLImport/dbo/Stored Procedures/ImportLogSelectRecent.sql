
CREATE PROCEDURE [dbo].[ImportLogSelectRecent]

@NumLogs INT = 10

AS
BEGIN

SET NOCOUNT ON

SELECT	l.ImportLogID,
		l.ImportDate,
		ISNULL(src.Source, ISNULL(l.BarCode, '')) AS [ImportSource],
		ISNULL(l.BarCode, '') AS [BarCode],
		l.ImportResult,
		l.TitleInsert,
		l.TitleUpdate,
		l.CreatorInsert,
		l.CreatorUpdate,
		l.TitleCreatorInsert,
		l.TitleCreatorUpdate,
		l.TitleTagInsert,
		l.TitleTagUpdate,
		l.TitleTitleIdentifierInsert,
		l.TitleTitleIdentifierUpdate,
		l.TitleAssociationInsert,
		l.TitleAssociationTitleIdentifierInsert,
		l.TitleVariantInsert,
		l.ItemInsert,
		l.ItemUpdate,
		l.TitleItemInsert,
		l.PageInsert,
		l.PageUpdate,
		l.IndicatedPageInsert,
		l.IndicatedPageUpdate,
		l.PagePageTypeInsert,
		l.PagePageTypeUpdate,
		l.PageNameInsert,
		l.PageNameUpdate
FROM	dbo.ImportLog l LEFT JOIN dbo.ImportSource src
			ON l.ImportSourceID = src.ImportSourceID
WHERE	DATEDIFF(day, l.ImportDate, GETDATE()) <= @NumLogs
ORDER BY
		l.ImportLogID DESC
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ImportLogSelectRecent. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END

