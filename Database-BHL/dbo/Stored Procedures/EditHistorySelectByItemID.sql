CREATE PROCEDURE dbo.EditHistorySelectByItemID

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

-- GET THE INITIAL CREATION DATES/USERS
INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), CreationDate, 120)), 
		'dbo.Item', @ItemID, '', 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Item i  
		LEFT JOIN dbo.AspNetUsers u ON i.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), i.ItemID) = @ItemID

INSERT #History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ii.CreationDate, 120)), 
		'dbo.ItemInstitution', ii.ItemInstitutionID, r.InstitutionRoleLabel + ':' + inst.InstitutionName, 'I', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Item i
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		LEFT JOIN dbo.AspNetUsers u ON ii.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), i.ItemID) = @ItemID

INSERT #History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), il.CreationDate, 120)), 
		'dbo.ItemLanguage', il.ItemLanguageID, l.LanguageName, 'I', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Item i
		INNER JOIN dbo.ItemLanguage il ON i.Itemid = il.ItemID
		INNER JOIN dbo.Language l ON il.LanguageCode = l.LanguageCode
		LEFT JOIN dbo.AspNetUsers u ON il.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), i.ItemID) = @ItemID

INSERT #History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ic.CreationDate, 120)), 
		'dbo.ItemCollection', ic.ItemCollectionID, c.CollectionName, 'I', 
		NULL, NULL, NULL
FROM	dbo.Item i
		INNER JOIN dbo.ItemCollection ic ON i.ItemID = ic.ItemID
		INNER JOIN dbo.Collection c ON ic.CollectionID = c.CollectionID
WHERE	CONVERT(nvarchar(max), i.ItemID) = @ItemID

INSERT #History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ti.CreationDate, 120)), 
		'dbo.TitleItem', ti.TitleItemID, CONVERT(nvarchar(20), ti.TitleID), 'I', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Item i
		INNER JOIN dbo.TitleItem ti ON i.ItemID = ti.ItemID
		LEFT JOIN dbo.AspNetUsers u ON ti.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), i.ItemID) = @ItemID


-- GET THE LAST MODIFIED DATES/USERS
INSERT #History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), LastModifiedDate, 120)), 
		'dbo.Item', @ItemID, '', 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Item i
		LEFT JOIN dbo.AspNetUsers u ON i.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), i.ItemID) = @ItemID
AND		ISNULL(CreationDate, '1/1/1980') <> ISNULL(LastModifiedDate, '1/1/1980')

INSERT #History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ii.LastModifiedDate, 120)), 
		'dbo.ItemInstitution', ii.ItemInstitutionID, r.InstitutionRoleLabel + ':' + inst.InstitutionName, 'U', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Item i
		INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
		INNER JOIN dbo.Institution inst ON ii.InstitutionCode = inst.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
		LEFT JOIN dbo.AspNetUsers u ON ii.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), i.ItemID) = @ItemID
AND		ISNULL(ii.CreationDate, '1/1/1980') <> ISNULL(ii.LastModifiedDate, '1/1/1980')

-- ItemLanguage table does not include LastModified columns!
-- ItemCollection table does not include LastModified columns!

INSERT #History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ti.LastModifiedDate, 120)), 
		'dbo.TitleItem', ti.TitleItemID, CONVERT(nvarchar(20), ti.TitleID), 'U', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Item i
		INNER JOIN dbo.TitleItem ti ON i.ItemID = ti.ItemID
		LEFT JOIN dbo.AspNetUsers u ON ti.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), i.ItemID) = @ItemID
AND		ISNULL(ti.CreationDate, '1/1/1980') <> ISNULL(ti.LastModifiedDate, '1/1/1980')


-- GET THE REST OF THE HISTORY
INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, '', b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN BHLAuditArchive.audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
WHERE	h.EntityName = 'dbo.Item'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, '', b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
WHERE	h.EntityName = 'dbo.Item'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, r.InstitutionRoleLabel + ':' + i.InstitutionName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN BHLAuditArchive.audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemInstitution ii ON ii.ItemInstitutionID = b.EntityKey1
		INNER JOIN dbo.Institution i ON ii.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	h.EntityName = 'dbo.ItemInstitution'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, r.InstitutionRoleLabel + ':' + i.InstitutionName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemInstitution ii ON ii.ItemInstitutionID = b.EntityKey1
		INNER JOIN dbo.Institution i ON ii.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON ii.InstitutionRoleID = r.InstitutionRoleID
WHERE	h.EntityName = 'dbo.ItemInstitution'

INSERT #History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, l.LanguageName, b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN BHLAuditArchive.audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemLanguage il ON il.ItemLanguageID = b.EntityKey1
		INNER JOIN dbo.Language l ON il.LanguageCode = l.LanguageCode
WHERE	h.EntityName = 'dbo.ItemLanguage'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, l.LanguageName, b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemLanguage il ON il.ItemLanguageID = b.EntityKey1
		INNER JOIN dbo.Language l ON il.LanguageCode = l.LanguageCode
WHERE	h.EntityName = 'dbo.ItemLanguage'

INSERT #History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, c.CollectionName, b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN BHLAuditArchive.audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemCollection ic ON ic.ItemCollectionID = b.EntityKey1
		INNER JOIN dbo.Collection c ON ic.CollectionID = c.CollectionID
WHERE	h.EntityName = 'dbo.ItemCollection'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, c.CollectionName, b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemCollection ic ON ic.ItemCollectionID = b.EntityKey1
		INNER JOIN dbo.Collection c ON ic.CollectionID = c.CollectionID
WHERE	h.EntityName = 'dbo.ItemCollection'

INSERT #History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, CONVERT(nvarchar(20), ti.ItemID), b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN BHLAuditArchive.audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleItem ti ON ti.TitleItemID = b.EntityKey1
WHERE	h.EntityName = 'dbo.TitleItem'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, CONVERT(nvarchar(20), ti.ItemID), b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleItem ti ON ti.TitleItemID = b.EntityKey1
WHERE	h.EntityName = 'dbo.TitleItem'

/*
-- If page history is desired (WARNING:  Slow!)
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
*/

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

END
