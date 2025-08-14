CREATE PROCEDURE dbo.WDEntityIdentifierPublishTitleIDs

AS

BEGIN

-- Get the IDs to add to production
SELECT	t.TitleID, 
		i.IdentifierID, 
		w.IdentifierValue
INTO	#TitleIDs
FROM	dbo.WDEntityIdentifier w
		INNER JOIN dbo.BHLTitle t ON w.BHLEntityID = t.TitleID
		INNER JOIN dbo.BHLIdentifier i ON w.IdentifierType = i.IdentifierName
		LEFT JOIN dbo.BHLTitle_Identifier ti ON t.TitleID = ti.TitleID AND i.IdentifierID = ti.IdentifierID AND w.IdentifierValue = ti.IdentifierValue
WHERE	w.BHLEntityType = 'Title'
AND		ti.TitleIdentifierID IS NULL
AND		SUBSTRING(	w.IdentifierValue, 1, 
			CASE WHEN CHARINDEX('/', w.IdentifierValue) > 0 
				THEN CHARINDEX('/', w.IdentifierValue) - 1 
				ELSE LEN(w.IdentifierValue) 
			END) NOT IN (SELECT Prefix FROM BHL.dbo.DOIPrefix)	-- Only insert Non-BHL DOIs

-- Add the IDs to production
INSERT	dbo.BHLTitle_Identifier (TitleID, IdentifierID, IdentifierValue)
SELECT	TitleID, IdentifierID, IdentifierValue FROM #TitleIDs

-- Return the list of newly added IDs
SELECT TitleID, IdentifierID, IdentifierValue FROM #TitleIDs

END
GO
