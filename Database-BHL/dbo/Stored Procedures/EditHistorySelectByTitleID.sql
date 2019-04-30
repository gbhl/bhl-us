CREATE PROCEDURE dbo.EditHistorySelectByTitleID

@TitleID nvarchar(100)

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
		'dbo.Title', @TitleID, '', 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Title t  
		LEFT JOIN dbo.AspNetUsers u ON t.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ti.CreationDate, 120)), 
		'dbo.TitleIdentifier', ti.TitleIdentifierID, id.IdentifierLabel + ':' + ti.IdentifierValue, 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Title t
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier id ON ti.IdentifierID = id.IdentifierID
		LEFT JOIN dbo.AspNetUsers u ON ti.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ta.CreationDate, 120)), 
		'dbo.TitleAuthor', ta.TitleAuthorID, n.FullName, 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Title t
		INNER JOIN dbo.TitleAuthor ta ON t.TitleID = ta.TitleID
		INNER JOIN dbo.AuthorName n ON ta.AuthorID = n.AuthorID
		LEFT JOIN dbo.AspNetUsers u ON ta.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), tk.CreationDate, 120)), 
		'dbo.TitleKeyword', tk.TitleKeywordID, k.Keyword, 'I', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Title t
		INNER JOIN dbo.TitleKeyword tk ON t.TitleID = tk.TitleID
		INNER JOIN dbo.Keyword k ON tk.KeywordID = k.KeywordID
		LEFT JOIN dbo.AspNetUsers u ON tk.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ti.CreationDate, 120)), 
		'dbo.TitleInstitution', ti.TitleInstitutionID, r.InstitutionRoleLabel + ':' + i.InstitutionName, 'I', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Title t
		INNER JOIN dbo.TitleInstitution ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Institution i ON ti.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON ti.InstitutionRoleID = r.InstitutionRoleID
		LEFT JOIN dbo.AspNetUsers u ON ti.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), tn.CreationDate, 120)), 
		'dbo.TitleNote', tn.TitleNoteID, nt.MarcDataFieldTag + ' (' + nt.NoteTypeName + ') ' + SUBSTRING(tn.NoteText, 1, 15) + '...', 'I', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Title t
		INNER JOIN dbo.TitleNote tn ON t.TitleID = tn.TitleID
		INNER JOIN dbo.NoteType nt ON tn.NoteTypeID = nt.NoteTypeID
		LEFT JOIN dbo.AspNetUsers u ON tn.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ta.CreationDate, 120)), 
		'dbo.TitleAssociation', ta.TitleAssociationID, tat.TitleAssociationLabel + ':' + SUBSTRING(ta.Title, 1, 15) + '...', 'I', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Title t
		INNER JOIN dbo.TitleAssociation ta ON t.TitleID = ta.TitleID
		INNER JOIN dbo.TitleAssociationType tat ON ta.TitleAssociationTypeID = tat.TitleAssociationTypeID
		LEFT JOIN dbo.AspNetUsers u ON ta.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), tv.CreationDate, 120)), 
		'dbo.TitleVariant', tv.TitleVariantID, tvt.TitleVariantLabel + ':' + SUBSTRING(tv.Title, 1, 15) + '...', 'I', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Title t
		INNER JOIN dbo.TitleVariant tv ON t.TitleID = tv.TitleID
		INNER JOIN dbo.TitleVariantType tvt ON tv.TitleVariantTypeID = tvt.TitleVariantTypeID
		LEFT JOIN dbo.AspNetUsers u ON tv.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), tl.CreationDate, 120)), 
		'dbo.TitleLanguage', tl.TitleLanguageID, l.LanguageName, 'I', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Title t
		INNER JOIN dbo.TitleLanguage tl ON t.TitleID = tl.TitleID
		INNER JOIN dbo.Language l ON tl.LanguageCode = l.LanguageCode
		LEFT JOIN dbo.AspNetUsers u ON tl.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), tc.CreationDate, 120)), 
		'dbo.TitleCollection', tc.TitleCollectionID, c.CollectionName, 'I', 
		NULL, NULL, NULL
FROM	dbo.Title t
		INNER JOIN dbo.TitleCollection tc ON t.TitleID = tc.TitleID
		INNER JOIN dbo.Collection c ON tc.CollectionID = c.CollectionID
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ti.CreationDate, 120)), 
		'dbo.TitleItem', ti.TitleItemID, CONVERT(nvarchar(20), ti.ItemID), 'I', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Title t
		INNER JOIN dbo.TitleItem ti ON t.TitleID = ti.TitleID
		LEFT JOIN dbo.AspNetUsers u ON ti.CreationUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), d.CreationDate, 120)), 
		'dbo.DOI', d.DOIID, d.DOIName, 'I', 
		NULL, NULL, NULL
FROM	dbo.Title t
		INNER JOIN dbo.DOI d ON t.TitleID = d.EntityID AND d.DOIEntityTypeID = 10	-- Title
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID


-- GET THE LAST MODIFIED DATES/USERS
INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), LastModifiedDate, 120)), 
		'dbo.Title', @TitleID, '', 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Title t
		LEFT JOIN dbo.AspNetUsers u ON t.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID
AND		ISNULL(CreationDate, '1/1/1980') <> ISNULL(LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ti.LastModifiedDate, 120)), 
		'dbo.TitleIdentifier', ti.TitleIdentifierID, id.IdentifierLabel + ':' + ti.IdentifierValue, 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Title t
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier id ON ti.IdentifierID = id.IdentifierID
		LEFT JOIN dbo.AspNetUsers u ON ti.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID
AND		ISNULL(ti.CreationDate, '1/1/1980') <> ISNULL(ti.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ta.LastModifiedDate, 120)), 
		'dbo.TitleAuthor', ta.TitleAuthorID, n.FullName, 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Title t
		INNER JOIN dbo.TitleAuthor ta ON t.TitleID = ta.TitleID
		INNER JOIN dbo.AuthorName n ON ta.AuthorID = n.AuthorID
		LEFT JOIN dbo.AspNetUsers u ON ta.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID
AND		ISNULL(ta.CreationDate, '1/1/1980') <> ISNULL(ta.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), tk.LastModifiedDate, 120)), 
		'dbo.TitleKeyword', tk.TitleKeywordID, k.Keyword, 'U', 
		u.FirstName, u.LastName, u.Email 
FROM	dbo.Title t
		INNER JOIN dbo.TitleKeyword tk ON t.TitleID = tk.TitleID
		INNER JOIN dbo.Keyword k ON tk.KeywordID = k.KeywordID
		LEFT JOIN dbo.AspNetUsers u ON tk.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID
AND		ISNULL(tk.CreationDate, '1/1/1980') <> ISNULL(tk.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ti.LastModifiedDate, 120)), 
		'dbo.TitleInstitution', ti.TitleInstitutionID, r.InstitutionRoleLabel + ':' + i.InstitutionName, 'U', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Title t
		INNER JOIN dbo.TitleInstitution ti ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Institution i ON ti.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON ti.InstitutionRoleID = r.InstitutionRoleID
		LEFT JOIN dbo.AspNetUsers u ON ti.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID
AND		ISNULL(ti.CreationDate, '1/1/1980') <> ISNULL(ti.LastModifiedDate, '1/1/1980')

-- TitleNote table does not include LastModified columns!

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ta.LastModifiedDate, 120)), 
		'dbo.TitleAssociation', ta.TitleAssociationID, tat.TitleAssociationLabel + ':' + SUBSTRING(ta.Title, 1, 15) + '...', 'U', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Title t
		INNER JOIN dbo.TitleAssociation ta ON t.TitleID = ta.TitleID
		INNER JOIN dbo.TitleAssociationType tat ON ta.TitleAssociationTypeID = tat.TitleAssociationTypeID
		LEFT JOIN dbo.AspNetUsers u ON ta.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID
AND		ISNULL(ta.CreationDate, '1/1/1980') <> ISNULL(ta.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), tv.LastModifiedDate, 120)), 
		'dbo.TitleVariant', tv.TitleVariantID, tvt.TitleVariantLabel + ':' + SUBSTRING(tv.Title, 1, 15) + '...', 'U', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Title t
		INNER JOIN dbo.TitleVariant tv ON t.TitleID = tv.TitleID
		INNER JOIN dbo.TitleVariantType tvt ON tv.TitleVariantTypeID = tvt.TitleVariantTypeID
		LEFT JOIN dbo.AspNetUsers u ON tv.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID
AND		ISNULL(tv.CreationDate, '1/1/1980') <> ISNULL(tv.LastModifiedDate, '1/1/1980')

-- TitleLanguage table does not include LastModified columns!
-- TitleCollection table does not include LastModified columns!

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), ti.LastModifiedDate, 120)), 
		'dbo.TitleItem', ti.TitleItemID, CONVERT(nvarchar(20), ti.ItemID), 'U', 
		u.FirstName, u.LastName, u.Email
FROM	dbo.Title t
		INNER JOIN dbo.TitleItem ti ON t.TitleID = ti.TitleID
		LEFT JOIN dbo.AspNetUsers u ON ti.LastModifiedUserID = u.Id
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID
AND		ISNULL(ti.CreationDate, '1/1/1980') <> ISNULL(ti.LastModifiedDate, '1/1/1980')

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), d.LastModifiedDate, 120)), 
		'dbo.DOI', d.DOIID, d.DOIName, 'U', 
		NULL, NULL, NULL
FROM	dbo.Title t
		INNER JOIN dbo.DOI d ON t.TitleID = d.EntityID AND d.DOIEntityTypeID = 10	-- Title
WHERE	CONVERT(nvarchar(max), t.TitleID) = @TitleID
AND		ISNULL(d.CreationDate, '1/1/1980') <> ISNULL(d.LastModifiedDate, '1/1/1980')


-- GET THE REST OF THE HISTORY
INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, '', b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
WHERE	h.EntityName = 'dbo.Title'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, '', b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
WHERE	h.EntityName = 'dbo.Title'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, id.IdentifierLabel + ':' + ti.IdentifierValue, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.Title_Identifier ti ON ti.TitleIdentifierID = b.EntityKey1
		INNER JOIN dbo.Identifier id ON ti.IdentifierID = id.IdentifierID
WHERE	h.EntityName = 'dbo.TitleIdentifier'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, id.IdentifierLabel + ':' + ti.IdentifierValue, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.Title_Identifier ti ON ti.TitleIdentifierID = b.EntityKey1
		INNER JOIN dbo.Identifier id ON ti.IdentifierID = id.IdentifierID
WHERE	h.EntityName = 'dbo.TitleIdentifier'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, n.FullName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleAuthor ta ON ta.TitleAuthorID = b.EntityKey1
		INNER JOIN dbo.AuthorName n ON ta.AuthorID = n.AuthorID
WHERE	h.EntityName = 'dbo.TitleAuthor'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, n.FullName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleAuthor ta ON ta.TitleAuthorID = b.EntityKey1
		INNER JOIN dbo.AuthorName n ON ta.AuthorID = n.AuthorID
WHERE	h.EntityName = 'dbo.TitleAuthor'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, k.Keyword, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleKeyword tk ON tk.TitleKeywordID = b.EntityKey1
		INNER JOIN dbo.Keyword k ON tk.KeywordID = k.KeywordID
WHERE	h.EntityName = 'dbo.TitleKeyword'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, k.Keyword, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleKeyword tk ON tk.TitleKeywordID = b.EntityKey1
		INNER JOIN dbo.Keyword k ON tk.KeywordID = k.KeywordID
WHERE	h.EntityName = 'dbo.TitleKeyword'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, r.InstitutionRoleLabel + ':' + i.InstitutionName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleInstitution ti ON ti.TitleInstitutionID = b.EntityKey1
		INNER JOIN dbo.Institution i ON ti.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON ti.InstitutionRoleID = r.InstitutionRoleID
WHERE	h.EntityName = 'dbo.TitleInstitution'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, r.InstitutionRoleLabel + ':' + i.InstitutionName, b.Operation, 
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleInstitution ti ON ti.TitleInstitutionID = b.EntityKey1
		INNER JOIN dbo.Institution i ON ti.InstitutionCode = i.InstitutionCode
		INNER JOIN dbo.InstitutionRole r ON ti.InstitutionRoleID = r.InstitutionRoleID
WHERE	h.EntityName = 'dbo.TitleInstitution'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, nt.MarcDataFieldTag + ' (' + nt.NoteTypeName + ') ' + SUBSTRING(tn.NoteText, 1, 15) + '...', b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleNote tn ON tn.TitleNoteID = b.EntityKey1
		INNER JOIN dbo.NoteType nt ON tn.NoteTypeID = nt.NoteTypeID
WHERE	h.EntityName = 'dbo.TitleNote'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, nt.MarcDataFieldTag + ' (' + nt.NoteTypeName + ') ' + SUBSTRING(tn.NoteText, 1, 15) + '...', b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleNote tn ON tn.TitleNoteID = b.EntityKey1
		INNER JOIN dbo.NoteType nt ON tn.NoteTypeID = nt.NoteTypeID
WHERE	h.EntityName = 'dbo.TitleNote'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, tat.TitleAssociationLabel + ':' + SUBSTRING(ta.Title, 1, 15) + '...' COLLATE SQL_Latin1_General_CP1_CI_AI, b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleAssociation ta ON ta.TitleAssociationID = b.EntityKey1
		INNER JOIN dbo.TitleAssociationType tat ON ta.TitleAssociationTypeID = tat.TitleAssociationTypeID
WHERE	h.EntityName = 'dbo.TitleAssociation'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, tat.TitleAssociationLabel + ':' + SUBSTRING(ta.Title, 1, 15) + '...' COLLATE SQL_Latin1_General_CP1_CI_AI, b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleAssociation ta ON ta.TitleAssociationID = b.EntityKey1
		INNER JOIN dbo.TitleAssociationType tat ON ta.TitleAssociationTypeID = tat.TitleAssociationTypeID
WHERE	h.EntityName = 'dbo.TitleAssociation'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, tvt.TitleVariantLabel + ':' + SUBSTRING(tv.Title, 1, 15) + '...', b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleVariant tv ON tv.TitleVariantID = b.EntityKey1
		INNER JOIN dbo.TitleVariantType tvt ON tv.TitleVariantTypeID = tvt.TitleVariantTypeID
WHERE	h.EntityName = 'dbo.TitleVariant'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, tvt.TitleVariantLabel + ':' + SUBSTRING(tv.Title, 1, 15) + '...', b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleVariant tv ON tv.TitleVariantID = b.EntityKey1
		INNER JOIN dbo.TitleVariantType tvt ON tv.TitleVariantTypeID = tvt.TitleVariantTypeID
WHERE	h.EntityName = 'dbo.TitleVariant'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, l.LanguageName, b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleLanguage tl ON tl.TitleLanguageID = b.EntityKey1
		INNER JOIN dbo.Language l ON tl.LanguageCode = l.LanguageCode
WHERE	h.EntityName = 'dbo.TitleLanguage'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, l.LanguageName, b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleLanguage tl ON tl.TitleLanguageID = b.EntityKey1
		INNER JOIN dbo.Language l ON tl.LanguageCode = l.LanguageCode
WHERE	h.EntityName = 'dbo.TitleLanguage'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, c.CollectionName, b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleCollection tc ON tc.TitleCollectionID = b.EntityKey1
		INNER JOIN dbo.Collection c ON tc.CollectionID = c.CollectionID
WHERE	h.EntityName = 'dbo.TitleCollection'
UNION
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, c.CollectionName, b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasic b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
		LEFT JOIN dbo.AspNetUsers u ON b.ApplicationUserID = u.Id
		INNER JOIN dbo.TitleCollection tc ON tc.TitleCollectionID = b.EntityKey1
		INNER JOIN dbo.Collection c ON tc.CollectionID = c.CollectionID
WHERE	h.EntityName = 'dbo.TitleCollection'

INSERT	#History
SELECT	CONVERT(datetime, CONVERT(nvarchar(50), b.AuditDate, 120)), 
		h.EntityName, h.EntityKey1, CONVERT(nvarchar(20), ti.ItemID), b.Operation,
		u.FirstName, u.LastName, u.Email
FROM	#History h
		INNER JOIN audit.AuditBasicArchive b ON h.EntityName = b.EntityName AND h.EntityKey1 = b.EntityKey1
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
