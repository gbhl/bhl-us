CREATE PROCEDURE [dbo].[CollectionClearOtherFeatured]

@CollectionID int

AS 

SET NOCOUNT ON

-- If the specified collection is the featured collection,
-- turn off the Featured flag on all other collections
UPDATE	dbo.[Collection]
SET		Featured = 0
WHERE	Featured = 1
AND		CollectionID <> @CollectionID
AND		EXISTS (SELECT	Featured 
				FROM	dbo.[Collection]
				WHERE	CollectionID = @CollectionID 
				AND		Featured = 1)

