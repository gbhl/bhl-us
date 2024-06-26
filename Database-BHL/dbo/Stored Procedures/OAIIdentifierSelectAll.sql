SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
		SELECT DISTINCT b.BookID AS ID, 'item' as SetSpec, b.LastModifiedDate
		FROM	dbo.Book b 
				INNER JOIN dbo.Item i ON b.ItemID = i.ItemID
				INNER JOIN dbo.SearchCatalog c ON b.BookID = c.ItemID
		WHERE	(b.LastModifiedDate > @FromDate OR @FromDate IS NULL)
		AND		(b.LastModifiedDate < @UntilDate + 1 OR @UntilDate IS NULL)
		AND		(i.ItemStatusID = 40)
		UNION
		SELECT DISTINCT t.TitleID AS ID, 'title' as SetSpec, t.LastModifiedDate
		FROM	dbo.Title t INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID
		WHERE	(t.LastModifiedDate > @FromDate OR @FromDate IS NULL)
		AND		(t.LastModifiedDate < @UntilDate + 1 OR @UntilDate IS NULL)
		AND		(PublishReady = 1)
		UNION
		SELECT DISTINCT s.SegmentID AS ID, 'part' AS SetSpec, s.LastModifiedDate
		FROM	dbo.Segment s 
				INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
				LEFT JOIN dbo.ItemRelationship ir ON i.ItemID = ir.ChildID	-- Must have local content
				INNER JOIN dbo.SearchCatalogSegment c ON s.SegmentID = c.SegmentID
		WHERE	(s.LastModifiedDate > @FromDate OR @FromDate IS NULL)
		AND		(s.LastModifiedDate < @UntilDate + 1 OR @UntilDate IS NULL)
		AND		(i.ItemStatusID IN (30, 40))
		AND		(ir.RelationshipID IS NOT NULL OR ISNULL(Url, '') <> '')
		) X
WHERE	(SetSpec + ':' + RIGHT('0000000000' + CONVERT(varchar(10), ID), 10)) > (@SetSpec + ':' + RIGHT('0000000000' + CONVERT(varchar(10), @StartID), 10))
ORDER BY SetSpec, ID

END


GO
