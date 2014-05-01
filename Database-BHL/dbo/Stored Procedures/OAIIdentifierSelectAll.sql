
CREATE PROCEDURE [dbo].[OAIIdentifierSelectAll]

@MaxIdentifiers int = 100,
@StartID int = 1,
@SetSpec varchar(30) = '',
@FromDate DATETIME = null,
@UntilDate DATETIME = null

AS

BEGIN

SET NOCOUNT ON

SELECT	TOP(@MaxIdentifiers) ID, SetSpec, LastModifiedDate
FROM	(
		SELECT DISTINCT i.ItemID AS ID, 'item' as SetSpec, i.LastModifiedDate
		FROM	dbo.Item i INNER JOIN dbo.SearchCatalog c ON i.ItemID = c.ItemID
		WHERE	(i.LastModifiedDate > @FromDate OR @FromDate IS NULL)
		AND		(i.LastModifiedDate < @UntilDate OR @UntilDate IS NULL)
		AND		(ItemStatusID = 40)
		UNION
		SELECT DISTINCT t.TitleID AS ID, 'title' as SetSpec, t.LastModifiedDate
		FROM	dbo.Title t INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID
		WHERE	(t.LastModifiedDate > @FromDate OR @FromDate IS NULL)
		AND		(t.LastModifiedDate < @UntilDate OR @UntilDate IS NULL)
		AND		(PublishReady = 1)
		UNION
		SELECT DISTINCT s.SegmentID AS ID, 'part' AS SetSpec, s.LastModifiedDate
		FROM	dbo.Segment s INNER JOIN dbo.SearchCatalogSegment c ON s.SegmentID = c.SegmentID
		WHERE	(s.LastModifiedDate > @FromDate OR @FromDate IS NULL)
		AND		(s.LastModifiedDate < @UntilDate OR @UntilDate IS NULL)
		AND		(s.SegmentStatusID IN (10, 20))
		AND		(s.ItemID IS NOT NULL OR ISNULL(Url, '') <> '')
		) X
WHERE	(SetSpec + ':' + RIGHT('0000000000' + CONVERT(varchar(10), ID), 10)) > (@SetSpec + ':' + RIGHT('0000000000' + CONVERT(varchar(10), @StartID), 10))
ORDER BY SetSpec, ID

END





