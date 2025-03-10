SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[NameSegmentSelectBySegmentID]

@SegmentID INT

AS

BEGIN

SET NOCOUNT ON

SELECT	CAST(NULL AS INT) AS NameSegmentID, 
		np.NameID,
		s.SegmentID,
		np.IsFirstOccurrence,
		np.CreationDate,
		np.LastModifiedDate,
		np.CreationUserID,
		np.LastModifiedUserID
INTO	#Name
FROM	dbo.Segment s
		INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
		INNER JOIN dbo.ItemPage ip ON i.ItemID = ip.ItemID
		INNER JOIN dbo.Page p ON ip.PageID = p.PageID
		INNER JOIN dbo.NamePage np ON p.PageID = np.PageID
WHERE	s.SegmentID = @SegmentID

SELECT	t.NameSegmentID, 
		t.NameID,
		t.SegmentID,
		t.IsFirstOccurrence,
		nr.NameResolvedID, 
		n.NameString, 
		ISNULL(nr.ResolvedNameString, '') AS ResolvedNameString, 
		ISNULL(nr.CanonicalNameString, '') AS CanonicalNameString,
		t.CreationDate,
		t.LastModifiedDate,
		t.CreationUserID,
		t.LastModifiedUserID
FROM	#Name t
		INNER JOIN dbo.Name n ON t.NameID = n.NameID
		LEFT JOIN dbo.NameResolved nr ON n.NameResolvedID = nr.NameResolvedID AND nr.IsPreferred = 1
END

GO
