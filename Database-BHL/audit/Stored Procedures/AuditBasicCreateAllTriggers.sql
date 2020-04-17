CREATE PROCEDURE [audit].[AuditBasicCreateAllTriggers]

AS
-----------------------------------------------------------------------------
--  Create the triggers needed for auditing the core changes to the database.
--  Column-level changes are not logged, only table-level.  In other words,
--  the fact that a row has been inserted, updated, or deleted is captured,
--  but not (in the case of updates) specifically what changed.
-----------------------------------------------------------------------------

BEGIN

SET NOCOUNT ON

exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Vault', 'VaultID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Title', 'TitleID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'I'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Title', 'TitleID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'U'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleLanguage', 'TitleLanguageID', NULL, NULL, 'dbo', 'Title', 'TitleID', 'dbo', 'Language', 'LanguageCode'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleItem', 'TitleItemID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'I'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleItem', 'TitleItemID', NULL, NULL, 'dbo', 'Title', 'TitleID', 'dbo', 'Item', 'ItemID', 'D'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleAssociation', 'TitleAssociationID', NULL, NULL, 'dbo', 'Title', 'TitleID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleAssociation_TitleIdentifier', 'TitleAssociation_TitleIdentifierID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleVariant', 'TitleVariantID', NULL, NULL, 'dbo', 'Title', 'TitleID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Item', 'ItemID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'I'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Item', 'ItemID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'U'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'ItemLanguage', 'ItemLanguageID', NULL, NULL, 'dbo', 'Item', 'ItemID', 'dbo', 'Language', 'LanguageCode'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Page', 'PageID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'I'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Page', 'PageID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'U'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'IndicatedPage', 'PageID', 'Sequence', NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Page_PageType', 'PageID', 'PageTypeID', NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Collection', 'CollectionID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'I'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Collection', 'CollectionID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'U'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'ItemCollection', 'ItemCollectionID', NULL, NULL, 'dbo', 'Item', 'ItemID', 'dbo', 'Collection', 'CollectionID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleCollection', 'TitleCollectionID', NULL, NULL, 'dbo', 'Title', 'TitleID', 'dbo', 'Collection', 'CollectionID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'PageType', 'PageTypeID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'ItemStatus', 'ItemStatusID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'PaginationStatus', 'PaginationStatusID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Language', 'LanguageCode', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'BibliographicLevel', 'BibliographicLevelID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleVariantType', 'TitleVariantTypeID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleAssociationType', 'TitleAssociationTypeID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'DOI', 'DOIID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'I'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'DOI', 'DOIID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'U'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'DOIEntityType', 'DOIEntityTypeID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'DOIStatus', 'DOIStatusID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Institution', 'InstitutionCode', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'AspNetUsers', 'Id', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'InstitutionRole', 'InstitutionRoleID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'InstitutionGroup', 'InstitutionGroupID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'InstitutionGroupInstitution', 'InstitutionGroupInstitutionID', NULL, NULL, 'dbo', 'InstitutionGroup', 'InstitutionGroupID', 'dbo', 'Institution', 'InstitutionCode'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'ItemInstitution', 'ItemInstitutionID', NULL, NULL, 'dbo', 'Item', 'ItemID', 'dbo',' Institution', 'InstitutionCode'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'SegmentInstitution', 'SegmentInstitutionID', NULL, NULL, 'dbo', 'Segment', 'SegmentID', 'dbo',' Institution', 'InstitutionCode'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleInstitution', 'TitleInstitutionID', NULL, NULL, 'dbo', 'Title', 'TitleID', 'dbo',' Institution', 'InstitutionCode'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'NoteType', 'NoteTypeID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleNote', 'TitleNoteID', NULL, NULL, 'dbo', 'Title', 'TitleID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'MaterialType', 'MaterialTypeID', NULL, NULL

-- Added for Segment additions to data model
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Author', 'AuthorID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'I'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Author', 'AuthorID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'U'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'AuthorName', 'AuthorNameID', NULL, NULL, 'dbo', 'Author', 'AuthorID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'AuthorType', 'AuthorTypeID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'AuthorRole', 'AuthorRoleID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'AuthorIdentifier', 'AuthorIdentifierID', NULL, NULL, 'dbo', 'Author', 'AuthorID', 'dbo', 'Identifier', 'IdentifierID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleAuthor', 'TitleAuthorID', NULL, NULL, 'dbo', 'Title', 'TitleID', 'dbo', 'Author', 'AuthorID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Keyword', 'KeywordID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleKeyword', 'TitleKeywordID', NULL, NULL, 'dbo', 'Title', 'TitleID', 'dbo', 'Keyword', 'KeywordID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Identifier', 'IdentifierID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Title_Identifier', 'TitleIdentifierID', NULL, NULL, 'dbo', 'Title', 'TitleID', 'dbo', 'Identifier', 'IdentifierID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Segment', 'SegmentID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'I'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Segment', 'SegmentID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'U'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'SegmentGenre', 'SegmentGenreID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'SegmentPage', 'SegmentPageID', NULL, NULL, 'dbo', 'Segment', 'SegmentID', 'dbo', 'Page', 'PageID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'SegmentKeyword', 'SegmentKeywordID', NULL, NULL, 'dbo', 'Segment', 'SegmentID', 'dbo', 'Keyword', 'KeywordID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'SegmentIdentifier', 'SegmentIdentifierID', NULL, NULL, 'dbo', 'Segment', 'SegmentID', 'dbo', 'Identifier', 'IdentifierID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'SegmentAuthor', 'SegmentAuthorID', NULL, NULL, 'dbo', 'Segment', 'SegmentID', 'dbo', 'Author', 'AuthorID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'SegmentCluster', 'SegmentClusterID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'SegmentClusterSegment', 'SegmentID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'SegmentStatus', 'SegmentStatusID', NULL, NULL

-- Added for Name additions to data model
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Name', 'NameID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'NamePage', 'NamePageID', NULL, NULL, 'dbo', 'Name', 'NameID', 'dbo', 'Page', 'PageID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'NameResolved', 'NameResolvedID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'NameIdentifier', 'NameIdentifierID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'NameSegment', 'NameSegmentID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'NameSource', 'NameSourceID', NULL, NULL

END
