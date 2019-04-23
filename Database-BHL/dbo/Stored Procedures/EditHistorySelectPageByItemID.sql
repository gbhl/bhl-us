CREATE PROCEDURE dbo.EditHistorySelectPageByItemID

@ItemID nvarchar(100)

AS

BEGIN

SET NOCOUNT ON 

-- TEMP TABLE TO POPULATE WITH EDIT HISTORY
CREATE TABLE #History
	(
	EditDate datetime NULL,
	EntityName nvarchar(50) NOT NULL,
	EntityKey1 nvarchar(100) NOT NULL,
	EntityDetail nvarchar(max) NULL,
	Operation nchar(1) NOT NULL,
	FirstName nvarchar(max) NULL,
	LastName nvarchar(max) NULL,
	Email nvarchar(256) NULL
	)

-- GET THE HISTORY
INSERT #History
SELECT	CONVERT(datetime, CONVERT(nvarchar(30), b.AuditDate, 101)) AS AuditDate,
		b.EntityName, '', 'Page Updates', 'U',
		u.FirstName, u.LastName, u.Email
FROM	dbo.Page p
		INNER JOIN dbo.Page_PageType ppt ON p.PageID = ppt.PageID
		INNER JOIN BHLAuditArchive.audit.AuditBasic b ON p.PageID = b.EntityKey1 AND b.EntityName = 'dbo.Page_PageType'
		LEFT JOIN dbo.AspNetUsers u ON ppt.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), p.ItemID) = @ItemID
GROUP BY
		CONVERT(datetime, CONVERT(nvarchar(30), b.AuditDate, 101)),
		b.EntityName,
		u.FirstName,
		u.LastName,
		u.Email
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(30), b.AuditDate, 101)) AS AuditDate,
		b.EntityName, '', 'Page Updates', 'U',
		u.FirstName, u.LastName, u.Email
FROM	dbo.Page p
		INNER JOIN dbo.Page_PageType ppt ON p.PageID = ppt.PageID
		INNER JOIN audit.AuditBasic b ON p.PageID = b.EntityKey1 AND b.EntityName = 'dbo.Page_PageType'
		LEFT JOIN dbo.AspNetUsers u ON ppt.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), p.ItemID) = @ItemID
GROUP BY
		CONVERT(datetime, CONVERT(nvarchar(30), b.AuditDate, 101)),
		b.EntityName,
		u.FirstName,
		u.LastName,
		u.Email

INSERT #History
SELECT	CONVERT(datetime, CONVERT(nvarchar(30), b.AuditDate, 101)) AS AuditDate,
		b.EntityName, '', 'Page Updates', 'U',
		u.FirstName, u.LastName, u.Email
FROM	dbo.Page p
		INNER JOIN dbo.IndicatedPage ip ON p.PageID = ip .PageID
		INNER JOIN BHLAuditArchive.audit.AuditBasic b ON p.PageID = b.EntityKey1 AND b.EntityName = 'dbo.IndicatedPage'
		LEFT JOIN dbo.AspNetUsers u ON ip.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), p.ItemID) = @ItemID
GROUP BY
		CONVERT(datetime, CONVERT(nvarchar(30), b.AuditDate, 101)),
		b.EntityName,
		u.FirstName,
		u.LastName,
		u.Email
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(30), b.AuditDate, 101)) AS AuditDate,
		b.EntityName, '', 'Page Updates', 'U',
		u.FirstName, u.LastName, u.Email
FROM	dbo.Page p
		INNER JOIN dbo.IndicatedPage ip ON p.PageID = ip .PageID
		INNER JOIN audit.AuditBasic b ON p.PageID = b.EntityKey1 AND b.EntityName = 'dbo.IndicatedPage'
		LEFT JOIN dbo.AspNetUsers u ON ip.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), p.ItemID) = @ItemID
GROUP BY
		CONVERT(datetime, CONVERT(nvarchar(30), b.AuditDate, 101)),
		b.EntityName,
		u.FirstName,
		u.LastName,
		u.Email

-- FINAL RESULT SET
SELECT	EditDate, EntityName, EntityKey1, 
		MAX(EntityDetail) AS EntityDetail, 
		MIN(Operation) AS Operation, 
		MAX(FirstName) AS FirstName, 
		MAX(LastName) AS LastName, 
		MAX(Email) AS Email
FROM	#History 
WHERE	Operation IN ('I', 'U', 'D')
GROUP BY EditDate, EntityName, EntityKey1
ORDER BY EditDate DESC, Operation DESC

DROP TABLE #History

END 
