
CREATE PROCEDURE [dbo].[PageNameCountUpdate] 
AS

SELECT DISTINCT P.PageID
INTO #TmpPage
FROM Title T 
	INNER JOIN Item I ON I.PrimaryTitleID = T.TitleID
	INNER JOIN Page P ON P.ItemID = I.ItemID
WHERE T.PublishReady = 1 AND
	P.Active = 1 AND
	I.ItemStatusID = 40

DELETE FROM PageNameCount

INSERT INTO PageNameCount
( NameConfirmed, Qty, RefreshDate )
	SELECT TOP 500 NameConfirmed, 
		COUNT(*) AS Qty,
		GetDate()
	FROM PageName PN
		INNER JOIN #TmpPage TP ON TP.PageID = PN.PageID
	WHERE PN.Active = 1 AND
		PN.NameConfirmed IS NOT NULL AND
		PN.NameBankID IS NOT NULL
	GROUP BY NameConfirmed
	ORDER BY Qty DESC

