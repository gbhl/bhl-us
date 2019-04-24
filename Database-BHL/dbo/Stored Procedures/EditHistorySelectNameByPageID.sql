CREATE PROCEDURE dbo.EditHistorySelectNameByPageID

@PageID nvarchar(100)

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
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), x.CreationDate, 120)), 
		'dbo.NamePage', x.NamePageID, n.NameString, 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.NamePage x  
		INNER JOIN dbo.Name n ON x.NameID = n.NameID
		LEFT JOIN dbo.AspNetUsers u ON x.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), x.PageID) = @PageID

-- GET THE LAST MODIFIED DATES/USERS
INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), x.LastModifiedDate, 120)), 
		'dbo.NamePage', x.NamePageID, n.NameString, 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.NamePage x  
		INNER JOIN dbo.Name n ON x.NameID = n.NameID
		LEFT JOIN dbo.AspNetUsers u ON x.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), x.PageID) = @PageID
AND		ISNULL(x.CreationDate, '1/1/1980') <> ISNULL(x.LastModifiedDate, '1/1/1980')


-- GET THE REST OF THE HISTORY
INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, n.NameString, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.NamePage np ON b.EntityKey1 = np.NamePageID
		INNER JOIN dbo.Name n ON np.NameID = n.NameID
WHERE	h.EntityName = 'dbo.NamePage'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, n.NameString, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.NamePage np ON b.EntityKey1 = np.NamePageID
		INNER JOIN dbo.Name n ON np.NameID = n.NameID
WHERE	h.EntityName = 'dbo.NamePage'

-- FINAL RESULT SET
SELECT	EditDate, EntityName, EntityKey1, 
		MAX(EntityDetail) AS EntityDetail, 
		MIN(Operation) AS Operation, 
		MAX(FirstName) AS FirstName, 
		MAX(LastName) AS LastName, 
		MAX(Email) AS Email
FROM	#History 
WHERE	Operation IN ('I', 'U', 'D')
GROUP BY EditDate, EntityName, EntityKey1, Operation 
ORDER BY EditDate DESC, Operation DESC

END
