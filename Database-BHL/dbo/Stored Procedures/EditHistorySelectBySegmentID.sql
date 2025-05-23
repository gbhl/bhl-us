CREATE PROCEDURE [dbo].[EditHistorySelectBySegmentID]

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
		'dbo.Segment', x.SegmentID, '', 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment x  
		LEFT JOIN dbo.AspNetUsers u ON x.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), x.SegmentID) = @SegmentID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), i.CreationDate, 120)), 
		'dbo.Item', i.ItemID, '', 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
		LEFT JOIN dbo.AspNetUsers u ON i.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), si.CreationDate, 120)), 
		'dbo.ItemIdentifier', si.ItemIdentifierID, id.IdentifierLabel + ':' + si.IdentifierValue, 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.ItemIdentifier si ON s.ItemID = si.ItemID
		INNER JOIN dbo.Identifier id ON si.IdentifierID = id.IdentifierID
		LEFT JOIN dbo.AspNetUsers u ON si.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), sa.CreationDate, 120)), 
		'dbo.ItemAuthor', sa.ItemAuthorID, n.FullName, 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.ItemAuthor sa ON s.ItemID = sa.ItemID
		INNER JOIN dbo.AuthorName n ON sa.AuthorID = n.AuthorID
		LEFT JOIN dbo.AspNetUsers u ON sa.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), sk.CreationDate, 120)), 
		'dbo.ItemKeyword', sk.ItemKeywordID, k.Keyword, 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.ItemKeyword sk ON s.ItemID = sk.ItemID
		INNER JOIN dbo.Keyword k ON sk.KeywordID = k.KeywordID
		LEFT JOIN dbo.AspNetUsers u ON sk.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), si.CreationDate, 120)), 
		'dbo.ItemInstitution', si.ItemInstitutionID, r.InstitutionRoleLabel + ':' + i.InstitutionName, 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.ItemInstitution si ON s.ItemID = si.ItemID
		INNER JOIN dbo.Institution i ON si.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON si.InstitutionRoleID = r.InstitutionRoleID
		LEFT JOIN dbo.AspNetUsers u ON si.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), sr.CreationDate, 120)), 
		'dbo.SegmentExternalResource', sr.SegmentExternalResourceID, rt.ExternalResourceTypeLabel + ': ' + SUBSTRING(sr.UrlText, 1, 25) + '...', 'I', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Segment s
		INNER JOIN dbo.SegmentExternalResource sr ON s.SegmentID = sr.SegmentID
		INNER JOIN dbo.ExternalResourceType rt ON sr.ExternalResourceTypeID = rt.ExternalResourceTypeID
		LEFT JOIN dbo.AspNetUsers u ON sr.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), sp.CreationDate, 120)), 
		'dbo.ItemPage', sp.ItemPageID, CONVERT(nvarchar(20), sp.PageID), 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.ItemPage sp ON s.ItemID = sp.ItemID
		LEFT JOIN dbo.AspNetUsers u ON sp.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID


-- GET THE LAST MODIFIED DATES/USERS
INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), LastModifiedDate, 120)), 
		'dbo.Segment', s.SegmentID, '', 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		LEFT JOIN dbo.AspNetUsers u ON s.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID
AND		ISNULL(CreationDate, '1/1/1980') <> ISNULL(LastModifiedDate, '1/1/1980')

INSERT #History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), i.LastModifiedDate, 120)), 
		'dbo.Item', i.ItemID, '', 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
		LEFT JOIN dbo.AspNetUsers u ON i.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentiD) = @SegmentID
AND		ISNULL(i.CreationDate, '1/1/1980') <> ISNULL(i.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), si.LastModifiedDate, 120)), 
		'dbo.ItemIdentifier', si.ItemIdentifierID, id.IdentifierLabel + ':' + si.IdentifierValue, 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.ItemIdentifier si ON s.ItemID = si.ItemID
		INNER JOIN dbo.Identifier id ON si.IdentifierID = id.IdentifierID
		LEFT JOIN dbo.AspNetUsers u ON si.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID
AND		ISNULL(si.CreationDate, '1/1/1980') <> ISNULL(si.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), sa.LastModifiedDate, 120)), 
		'dbo.ItemAuthor', sa.ItemAuthorID, n.FullName, 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.ItemAuthor sa ON s.ItemID = sa.ItemID
		INNER JOIN dbo.AuthorName n ON sa.AuthorID = n.AuthorID
		LEFT JOIN dbo.AspNetUsers u ON sa.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID
AND		ISNULL(sa.CreationDate, '1/1/1980') <> ISNULL(sa.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), sk.LastModifiedDate, 120)), 
		'dbo.ItemKeyword', sk.ItemKeywordID, k.Keyword, 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.ItemKeyword sk ON s.ItemID = sk.ItemID
		INNER JOIN dbo.Keyword k ON sk.KeywordID = k.KeywordID
		LEFT JOIN dbo.AspNetUsers u ON sk.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID
AND		ISNULL(sk.CreationDate, '1/1/1980') <> ISNULL(sk.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), si.LastModifiedDate, 120)), 
		'dbo.ItemInstitution', si.ItemInstitutionID, r.InstitutionRoleLabel + ':' + i.InstitutionName, 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.ItemInstitution si ON s.ItemID = si.ItemID
		INNER JOIN dbo.Institution i ON si.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON si.InstitutionRoleID = r.InstitutionRoleID
		LEFT JOIN dbo.AspNetUsers u ON si.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID
AND		ISNULL(si.CreationDate, '1/1/1980') <> ISNULL(si.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), sr.LastModifiedDate, 120)), 
		'dbo.TitleExternalResource', sr.SegmentExternalResourceID, rt.ExternalResourceTypeLabel + ': ' + SUBSTRING(sr.UrlText, 1, 25) + '...', 'U', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Segment s
		INNER JOIN dbo.SegmentExternalResource sr ON s.SegmentID = sr.SegmentID
		INNER JOIN dbo.ExternalResourceType rt ON sr.ExternalResourceTypeID = rt.ExternalResourceTypeID
		LEFT JOIN dbo.AspNetUsers u ON sr.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID
AND		ISNULL(sr.CreationDate, '1/1/1980') <> ISNULL(sr.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), sp.LastModifiedDate, 120)), 
		'dbo.ItemPage', sp.ItemPageID, CONVERT(nvarchar(20), sp.PageID), 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Segment s
		INNER JOIN dbo.ItemPage sp ON s.ItemID = sp.ItemID
		LEFT JOIN dbo.AspNetUsers u ON sp.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), s.SegmentID) = @SegmentID
AND		ISNULL(sp.CreationDate, '1/1/1980') <> ISNULL(sp.LastModifiedDate, '1/1/1980')


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
		h.EntityName, h.EntityKey1, '', b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
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
		h.EntityName, h.EntityKey1, id.IdentifierLabel + ':' + si.IdentifierValue, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemIdentifier si ON si.ItemIdentifierID = b.EntityKey1
		INNER JOIN dbo.Identifier id ON si.IdentifierID = id.IdentifierID
WHERE	h.EntityName = 'dbo.ItemIdentifier'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, id.IdentifierLabel + ':' + si.IdentifierValue, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemIdentifier si ON si.ItemIdentifierID = b.EntityKey1
		INNER JOIN dbo.Identifier id ON si.IdentifierID = id.IdentifierID
WHERE	h.EntityName = 'dbo.ItemIdentifier'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, n.FullName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemAuthor sa ON sa.ItemAuthorID = b.EntityKey1
		INNER JOIN dbo.AuthorName n ON sa.AuthorID = n.AuthorID
WHERE	h.EntityName = 'dbo.ItemAuthor'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, n.FullName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemAuthor sa ON sa.ItemAuthorID = b.EntityKey1
		INNER JOIN dbo.AuthorName n ON sa.AuthorID = n.AuthorID
WHERE	h.EntityName = 'dbo.ItemAuthor'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, k.Keyword, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemKeyword sk ON sk.ItemKeywordID = b.EntityKey1
		INNER JOIN dbo.Keyword k ON sk.KeywordID = k.KeywordID
WHERE	h.EntityName = 'dbo.ItemKeyword'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, k.Keyword, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemKeyword sk ON sk.ItemKeywordID = b.EntityKey1
		INNER JOIN dbo.Keyword k ON sk.KeywordID = k.KeywordID
WHERE	h.EntityName = 'dbo.ItemKeyword'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, r.InstitutionRoleLabel + ':' + i.InstitutionName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemInstitution si ON si.ItemInstitutionID = b.EntityKey1
		INNER JOIN dbo.Institution i ON si.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON si.InstitutionRoleID = r.InstitutionRoleID
WHERE	h.EntityName = 'dbo.ItemInstitution'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, r.InstitutionRoleLabel + ':' + i.InstitutionName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemInstitution si ON si.ItemInstitutionID = b.EntityKey1
		INNER JOIN dbo.Institution i ON si.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON si.InstitutionRoleID = r.InstitutionRoleID
WHERE	h.EntityName = 'dbo.ItemInstitution'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, rt.ExternalResourceTypeLabel + ': ' + SUBSTRING(sr.UrlText, 1, 25) + '...', b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.SegmentExternalResource sr ON sr.SegmentExternalResourceID = b.EntityKey1
		INNER JOIN dbo.ExternalResourceType rt ON sr.ExternalResourceTypeID = rt.ExternalResourceTypeID
WHERE	h.EntityName = 'dbo.SegmentExternalResource'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, rt.ExternalResourceTypeLabel + ': ' + SUBSTRING(sr.UrlText, 1, 25) + '...', b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.SegmentExternalResource sr ON sr.SegmentExternalResourceID = b.EntityKey1
		INNER JOIN dbo.ExternalResourceType rt ON sr.ExternalResourceTypeID = rt.ExternalResourceTypeID
WHERE	h.EntityName = 'dbo.SegmentExternalResource'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, CONVERT(nvarchar(20), sp.PageID), b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemPage sp ON sp.ItemPageID = b.EntityKey1
WHERE	h.EntityName = 'dbo.ItemPage'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, CONVERT(nvarchar(20), sp.PageID), b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.ItemPage sp ON sp.ItemPageID = b.EntityKey1
WHERE	h.EntityName = 'dbo.ItemPage'

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

GO
