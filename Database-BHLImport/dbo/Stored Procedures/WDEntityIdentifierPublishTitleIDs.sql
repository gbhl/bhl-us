CREATE PROCEDURE dbo.WDEntityIdentifierPublishTitleIDs

AS

BEGIN

INSERT	dbo.BHLTitle_Identifier (TitleID, IdentifierID, IdentifierValue)
SELECT	t.TitleID, 
		i.IdentifierID, 
		w.IdentifierValue
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

END
GO
