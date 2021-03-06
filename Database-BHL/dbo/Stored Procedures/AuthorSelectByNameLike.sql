SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AuthorSelectByNameLike]

@FullName nvarchar(255),
@ReturnCount	int = 100

AS 

SET NOCOUNT ON

SELECT DISTINCT
		AuthorID
INTO	#tmpAuthor
FROM	dbo.AuthorName
WHERE	FullName LIKE (@FullName + '%')
AND		IsPreferredName = 1

SELECT	v.AuthorID,
		v.FullName,
		v.Numeration,
		v.Unit,
		v.Title,
		v.Location,
		v.StartDate + CASE WHEN v.StartDate <> '' THEN '-' ELSE '' END + v.EndDate AS Dates,
		v.FullerForm,
		v.StartDate,
		v.Enddate,
		v.IsActive
INTO	#tmpFinal
FROM	dbo.TitleAuthorView v INNER JOIN #tmpAuthor t ON v.AuthorID = t.AuthorID
WHERE	v.IsActive = 1
AND		v.IsPreferredName = 1
AND		v.PublishReady = 1

UNION

SELECT	a.AuthorID, 
		n.FullName, 
		a.Numeration, 
		a.Unit, 
		a.Title, 
		a.Location,
		a.StartDate + CASE WHEN a.StartDate <> '' THEN '-' ELSE '' END + a.EndDate AS Dates,
		n.FullerForm, 
		a.StartDate,
		a.EndDate,
		a.IsActive
FROM	#tmpAuthor t
		INNER JOIN dbo.Author a ON t.AuthorID = a.AuthorID
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
		INNER JOIN dbo.ItemAuthor ia ON a.AuthorID = ia.AuthorID
		INNER JOIN dbo.Item i ON ia.ItemID = i.ItemID
		INNER JOIN dbo.Segment s ON i.ItemID = s.ItemID
WHERE	i.ItemStatusID IN (30, 40)
AND		a.IsActive = 1
AND		n.IsPreferredName = 1

SELECT TOP (@ReturnCount) 
		AuthorID, 
		FullName, 
		Numeration, 
		Unit, 
		Title, 
		Location,
		Dates,
		FullerForm, 
		StartDate,
		EndDate,
		IsActive
FROM	#tmpFinal
ORDER BY
		FullName,
		Numeration,
		Unit,
		Title,
		Location,
		FullerForm,
		StartDate, 
		EndDate


GO
