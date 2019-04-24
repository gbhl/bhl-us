CREATE PROCEDURE dbo.EditHistorySelectBySegmentID

@SegmentID nvarchar(100)

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
		'dbo.Segment', @SegmentID, '', 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment x  
		LEFT JOIN dbo.AspNetUsers u ON x.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), x.SegmentID) = @SegmentID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), si.CreationDate, 120)), 
		'dbo.SegmentIdentifier', si.SegmentIdentifierID, id.IdentifierLabel + ':' + si.IdentifierValue, 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.SegmentIdentifier si ON s.SegmentID = si.SegmentID
		INNER JOIN dbo.Identifier id ON si.IdentifierID = id.IdentifierID
		LEFT JOIN dbo.AspNetUsers u ON si.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), sa.CreationDate, 120)), 
		'dbo.SegmentAuthor', sa.SegmentAuthorID, n.FullName, 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.SegmentAuthor sa ON s.SegmentID = sa.SegmentID
		INNER JOIN dbo.AuthorName n ON sa.AuthorID = n.AuthorID
		LEFT JOIN dbo.AspNetUsers u ON sa.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), sk.CreationDate, 120)), 
		'dbo.SegmentKeyword', sk.SegmentKeywordID, k.Keyword, 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.SegmentKeyword sk ON s.SegmentID = sk.SegmentID
		INNER JOIN dbo.Keyword k ON sk.KeywordID = k.KeywordID
		LEFT JOIN dbo.AspNetUsers u ON sk.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), si.CreationDate, 120)), 
		'dbo.SegmentInstitution', si.SegmentInstitutionID, r.InstitutionRoleLabel + ':' + i.InstitutionName, 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.SegmentInstitution si ON s.SegmentID = si.SegmentID
		INNER JOIN dbo.Institution i ON si.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON si.InstitutionRoleID = r.InstitutionRoleID
		LEFT JOIN dbo.AspNetUsers u ON si.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), sp.CreationDate, 120)), 
		'dbo.SegmentPage', sp.SegmentPageID, CONVERT(nvarchar(20), sp.PageID), 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.SegmentPage sp ON s.SegmentID = sp.SegmentID
		LEFT JOIN dbo.AspNetUsers u ON sp.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), d.CreationDate, 120)), 
		'dbo.DOI', d.DOIID, d.DOIName, 'I', 
		NULL, NULL, NULL
FROM	dbo.Segment s
		INNER JOIN dbo.DOI d ON s.SegmentID = d.EntityID AND d.DOIEntityTypeID = 40	-- Segment
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID


-- GET THE LAST MODIFIED DATES/USERS
INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), LastModifiedDate, 120)), 
		'dbo.Segment', @SegmentID, '', 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		LEFT JOIN dbo.AspNetUsers u ON s.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID
AND		ISNULL(CreationDate, '1/1/1980') <> ISNULL(LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), si.LastModifiedDate, 120)), 
		'dbo.SegmentIdentifier', si.SegmentIdentifierID, id.IdentifierLabel + ':' + si.IdentifierValue, 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.SegmentIdentifier si ON s.SegmentID = si.SegmentID
		INNER JOIN dbo.Identifier id ON si.IdentifierID = id.IdentifierID
		LEFT JOIN dbo.AspNetUsers u ON si.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID
AND		ISNULL(si.CreationDate, '1/1/1980') <> ISNULL(si.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), sa.LastModifiedDate, 120)), 
		'dbo.SegmentAuthor', sa.SegmentAuthorID, n.FullName, 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.SegmentAuthor sa ON s.SegmentID = sa.SegmentID
		INNER JOIN dbo.AuthorName n ON sa.AuthorID = n.AuthorID
		LEFT JOIN dbo.AspNetUsers u ON sa.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID
AND		ISNULL(sa.CreationDate, '1/1/1980') <> ISNULL(sa.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), sk.LastModifiedDate, 120)), 
		'dbo.SegmentKeyword', sk.SegmentKeywordID, k.Keyword, 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.SegmentKeyword sk ON s.SegmentID = sk.SegmentID
		INNER JOIN dbo.Keyword k ON sk.KeywordID = k.KeywordID
		LEFT JOIN dbo.AspNetUsers u ON sk.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID
AND		ISNULL(sk.CreationDate, '1/1/1980') <> ISNULL(sk.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), si.LastModifiedDate, 120)), 
		'dbo.SegmentInstitution', si.SegmentInstitutionID, r.InstitutionRoleLabel + ':' + i.InstitutionName, 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.SegmentInstitution si ON s.SegmentID = si.SegmentID
		INNER JOIN dbo.Institution i ON si.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON si.InstitutionRoleID = r.InstitutionRoleID
		LEFT JOIN dbo.AspNetUsers u ON si.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID
AND		ISNULL(si.CreationDate, '1/1/1980') <> ISNULL(si.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), sp.LastModifiedDate, 120)), 
		'dbo.SegmentPage', sp.SegmentPageID, CONVERT(nvarchar(20), sp.PageID), 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.SegmentPage sp ON s.SegmentID = sp.SegmentID
		LEFT JOIN dbo.AspNetUsers u ON sp.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID
AND		ISNULL(sp.CreationDate, '1/1/1980') <> ISNULL(sp.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), d.LastModifiedDate, 120)), 
		'dbo.DOI', d.DOIID, d.DOIName, 'U', 
		NULL, NULL, NULL
FROM	dbo.Segment s
		INNER JOIN dbo.DOI d ON s.SegmentID = d.EntityID AND d.DOIEntityTypeID = 40	-- Segment
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID
AND		ISNULL(d.CreationDate, '1/1/1980') <> ISNULL(d.LastModifiedDate, '1/1/1980')


-- GET THE REST OF THE HISTORY
INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, '', b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
WHERE	h.EntityName = 'dbo.Segment'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, '', b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
WHERE	h.EntityName = 'dbo.Segment'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, id.IdentifierLabel + ':' + si.IdentifierValue, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.SegmentIdentifier si ON si.SegmentIdentifierID = b.EntityKey1
		INNER JOIN dbo.Identifier id ON si.IdentifierID = id.IdentifierID
WHERE	h.EntityName = 'dbo.SegmentIdentifier'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, id.IdentifierLabel + ':' + si.IdentifierValue, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.SegmentIdentifier si ON si.SegmentIdentifierID = b.EntityKey1
		INNER JOIN dbo.Identifier id ON si.IdentifierID = id.IdentifierID
WHERE	h.EntityName = 'dbo.SegmentIdentifier'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, n.FullName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.SegmentAuthor sa ON sa.SegmentAuthorID = b.EntityKey1
		INNER JOIN dbo.AuthorName n ON sa.AuthorID = n.AuthorID
WHERE	h.EntityName = 'dbo.SegmentAuthor'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, n.FullName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.SegmentAuthor sa ON sa.SegmentAuthorID = b.EntityKey1
		INNER JOIN dbo.AuthorName n ON sa.AuthorID = n.AuthorID
WHERE	h.EntityName = 'dbo.SegmentAuthor'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, k.Keyword, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.SegmentKeyword sk ON sk.SegmentKeywordID = b.EntityKey1
		INNER JOIN dbo.Keyword k ON sk.KeywordID = k.KeywordID
WHERE	h.EntityName = 'dbo.SegmentKeyword'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, k.Keyword, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.SegmentKeyword sk ON sk.SegmentKeywordID = b.EntityKey1
		INNER JOIN dbo.Keyword k ON sk.KeywordID = k.KeywordID
WHERE	h.EntityName = 'dbo.SegmentKeyword'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, r.InstitutionRoleLabel + ':' + i.InstitutionName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.SegmentInstitution si ON si.SegmentInstitutionID = b.EntityKey1
		INNER JOIN dbo.Institution i ON si.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON si.InstitutionRoleID = r.InstitutionRoleID
WHERE	h.EntityName = 'dbo.SegmentInstitution'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, r.InstitutionRoleLabel + ':' + i.InstitutionName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.SegmentInstitution si ON si.SegmentInstitutionID = b.EntityKey1
		INNER JOIN dbo.Institution i ON si.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON si.InstitutionRoleID = r.InstitutionRoleID
WHERE	h.EntityName = 'dbo.SegmentInstitution'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, CONVERT(nvarchar(20), sp.PageID), b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.SegmentPage sp ON sp.SegmentPageID = b.EntityKey1
WHERE	h.EntityName = 'dbo.SegmentPage'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, CONVERT(nvarchar(20), sp.PageID), b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.SegmentPage sp ON sp.SegmentPageID = b.EntityKey1
WHERE	h.EntityName = 'dbo.SegmentPage'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, d.DOIName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.DOI d ON d.DOIID = b.EntityKey1
WHERE	h.EntityName = 'dbo.DOI'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, d.DOIName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.DOI d ON d.DOIID = b.EntityKey1
WHERE	h.EntityName = 'dbo.DOI'

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
