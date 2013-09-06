
CREATE PROCEDURE [dbo].[ApiItemSelectUnpublished]

AS

SET NOCOUNT ON

-- Get all items that are unpublished and not redirected to another item
SELECT	ItemID
FROM	dbo.Item
WHERE	ItemStatusID <> 40
AND		RedirectItemID IS NULL

UNION

-- Get the item that are unpublished and redirected to other unpublished items
SELECT	x.OrigItemID as ItemID
FROM	(
		-- Get the replacement ItemID for the unpublished and redirected items.
		-- Look at up to 10 levels of redirection.
		SELECT	i1.ItemID AS OrigItemID,
				COALESCE(i10.ItemID, i9.ItemID, i8.ItemID, i7.ItemID, i6.ItemID,
						i5.ItemID, i4.ItemID, i3.ItemID, i2.ItemID) AS ReplacementItemID
		FROM	dbo.Item i1
				LEFT JOIN dbo.Item i2 ON i1.RedirectItemID = i2.ItemID
				LEFT JOIN dbo.Item i3 ON i2.RedirectItemID = i3.ItemID
				LEFT JOIN dbo.Item i4 ON i3.RedirectItemID = i4.ItemID
				LEFT JOIN dbo.Item i5 ON i4.RedirectItemID = i5.ItemID
				LEFT JOIN dbo.Item i6 ON i5.RedirectItemID = i6.ItemID
				LEFT JOIN dbo.Item i7 ON i6.RedirectItemID = i7.ItemID
				LEFT JOIN dbo.Item i8 ON i7.RedirectItemID = i8.ItemID
				LEFT JOIN dbo.Item i9 ON i8.RedirectItemID = i9.ItemID
				LEFT JOIN dbo.Item i10 ON i9.RedirectItemID = i10.ItemID
		WHERE	i1.ItemStatusID <> 40
		AND		i1.RedirectItemID IS NOT NULL
		) x INNER JOIN dbo.Item i
			ON x.ReplacementItemID = i.ItemID
WHERE	i.ItemStatusID <> 40
ORDER BY ItemID

