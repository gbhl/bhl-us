
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
		SELECT	ItemID AS ID, 'item' as SetSpec, LastModifiedDate
		FROM	dbo.Item
		WHERE	(LastModifiedDate > @FromDate OR @FromDate IS NULL)
		AND		(LastModifiedDate < @UntilDate OR @UntilDate IS NULL)
		AND		(ItemStatusID = 40)
		UNION
		SELECT	TitleID AS ID, 'title' as SetSpec, LastModifiedDate
		FROM	dbo.Title
		WHERE	(LastModifiedDate > @FromDate OR @FromDate IS NULL)
		AND		(LastModifiedDate < @UntilDate OR @UntilDate IS NULL)
		AND		(PublishReady = 1)
		UNION
		/*
		SELECT	PDFID AS ID, 'articlepdf' as SetSpec, LastModifiedDate
		FROM	dbo.PDF
		WHERE	(LastModifiedDate > @FromDate OR @FromDate IS NULL)
		AND		(LastModifiedDate < @UntilDate OR @UntilDate IS NULL)
		AND		(PdfStatusID = 30)
		AND		(ArticleTitle <> '')
		UNION
		*/
		SELECT	SegmentID AS ID, 'part' AS SetSpec, LastModifiedDate
		FROM	dbo.Segment
		WHERE	(LastModifiedDate > @FromDate OR @FromDate IS NULL)
		AND		(LastModifiedDate < @UntilDate OR @UntilDate IS NULL)
		AND		(SegmentID > @StartID)
		AND		(SegmentStatusID IN (10, 20))
		AND		(ItemID IS NOT NULL OR ISNULL(Url, '') <> '')
		) X
WHERE	(SetSpec + ':' + RIGHT('0000000000' + CONVERT(varchar(10), ID), 10)) > (@SetSpec + ':' + RIGHT('0000000000' + CONVERT(varchar(10), @StartID), 10))
ORDER BY SetSpec, ID

END





