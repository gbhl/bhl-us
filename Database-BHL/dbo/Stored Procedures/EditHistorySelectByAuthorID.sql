CREATE PROCEDURE dbo.EditHistorySelectByAuthorID

@AuthorID nvarchar(100)

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
		'dbo.Author', @AuthorID, '', 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Author x  
		LEFT JOIN dbo.AspNetUsers u ON x.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), x.AuthorID) = @AuthorID
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), n.CreationDate, 120)), 
		'dbo.AuthorName', n.AuthorNameID, n.FullName, 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Author a
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID  
		LEFT JOIN dbo.AspNetUsers u ON n.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), a.AuthorID) = @AuthorID
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ai.CreationDate, 120)), 
		'dbo.AuthorIdentifier', ai.AuthorIdentifierID, id.IdentifierLabel + ':' + ai.IdentifierValue, 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Author a
		INNER JOIN dbo.AuthorIdentifier ai ON a.AuthorID = ai.AuthorID
		INNER JOIN dbo.Identifier id ON ai.IdentifierID = id.IdentifierID
		LEFT JOIN dbo.AspNetUsers u ON ai.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), a.AuthorID) = @AuthorID

-- GET THE LAST MODIFIED DATES/USERS
INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), LastModifiedDate, 120)), 
		'dbo.Author', @AuthorID, '', 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Author x
		LEFT JOIN dbo.AspNetUsers u ON x.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), x.AuthorID) = @AuthorID
AND		ISNULL(CreationDate, '1/1/1980') <> ISNULL(LastModifiedDate, '1/1/1980')
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), n.LastModifiedDate, 120)), 
		'dbo.AuthorName', n.AuthorNameID, n.FullName, 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Author a
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID  
		LEFT JOIN dbo.AspNetUsers u ON n.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), a.AuthorID) = @AuthorID
AND		ISNULL(n.CreationDate, '1/1/1980') <> ISNULL(n.LastModifiedDate, '1/1/1980')
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ai.LastModifiedDate, 120)), 
		'dbo.AuthorIdentifier', ai.AuthorIdentifierID, id.IdentifierLabel + ':' + ai.IdentifierValue, 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Author a
		INNER JOIN dbo.AuthorIdentifier ai ON a.AuthorID = ai.AuthorID
		INNER JOIN dbo.Identifier id ON ai.IdentifierID = id.IdentifierID
		LEFT JOIN dbo.AspNetUsers u ON ai.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), a.AuthorID) = @AuthorID
AND		ISNULL(ai.CreationDate, '1/1/1980') <> ISNULL(ai.LastModifiedDate, '1/1/1980')

-- GET THE REST OF THE HISTORY
INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, '', b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN BHLAuditArchive.audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
WHERE	h.EntityName = 'dbo.Author'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, '', b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
WHERE	h.EntityName = 'dbo.Author'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, n.FullName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN BHLAuditArchive.audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.AuthorName n ON n.AuthorNameID = b.EntityKey1
WHERE	h.EntityName = 'dbo.AuthorName'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, n.FullName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.AuthorName n ON n.AuthorNameID = b.EntityKey1
WHERE	h.EntityName = 'dbo.AuthorName'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, id.IdentifierLabel + ':' + ai.IdentifierValue, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN BHLAuditArchive.audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.AuthorIdentifier ai ON ai.AuthorIdentifierID = b.EntityKey1
		INNER JOIN dbo.Identifier id ON ai.IdentifierID = id.IdentifierID
WHERE	h.EntityName = 'dbo.AuthorIdentifier'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, id.IdentifierLabel + ':' + ai.IdentifierValue, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.AuthorIdentifier ai ON ai.AuthorIdentifierID = b.EntityKey1
		INNER JOIN dbo.Identifier id ON ai.IdentifierID = id.IdentifierID
WHERE	h.EntityName = 'dbo.AuthorIdentifier'

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
