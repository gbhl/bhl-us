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

exec [audit].[AuditBasicCreateTrigger] 'dbo', 'AspNetUsers', 'Id', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Author', 'AuthorID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'I'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Author', 'AuthorID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'U'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'AuthorIdentifier', 'AuthorIdentifierID', NULL, NULL, 'dbo', 'Author', 'AuthorID', 'dbo', 'Identifier', 'IdentifierID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'AuthorName', 'AuthorNameID', NULL, NULL, 'dbo', 'Author', 'AuthorID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'AuthorRole', 'AuthorRoleID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'AuthorType', 'AuthorTypeID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'BibliographicLevel', 'BibliographicLevelID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Book', 'BookID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'I'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Book', 'BookID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'U'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Collection', 'CollectionID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'I'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Collection', 'CollectionID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'U'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'DOIEntityType', 'DOIEntityTypeID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'DOIStatus', 'DOIStatusID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Identifier', 'IdentifierID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'IndicatedPage', 'PageID', 'Sequence', NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Institution', 'InstitutionCode', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'InstitutionRole', 'InstitutionRoleID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'InstitutionGroup', 'InstitutionGroupID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'InstitutionGroupInstitution', 'InstitutionGroupInstitutionID', NULL, NULL, 'dbo', 'InstitutionGroup', 'InstitutionGroupID', 'dbo', 'Institution', 'InstitutionCode'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Item', 'ItemID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'I'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Item', 'ItemID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'U'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'ItemAuthor', 'ItemAuthorID', NULL, NULL, 'dbo', 'Item', 'ItemID', 'dbo', 'Author', 'AuthorID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'ItemCollection', 'ItemCollectionID', NULL, NULL, 'dbo', 'Item', 'ItemID', 'dbo', 'Collection', 'CollectionID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'ItemIdentifier', 'ItemIdentifierID', NULL, NULL, 'dbo', 'Item', 'ItemID', 'dbo',' Identifier', 'IdentifierID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'ItemInstitution', 'ItemInstitutionID', NULL, NULL, 'dbo', 'Item', 'ItemID', 'dbo',' Institution', 'InstitutionCode'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'ItemKeyword', 'ItemKeywordID', NULL, NULL, 'dbo', 'Item', 'ItemID', 'dbo', 'Keyword', 'KeywordID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'ItemLanguage', 'ItemLanguageID', NULL, NULL, 'dbo', 'Item', 'ItemID', 'dbo', 'Language', 'LanguageCode'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'ItemPage', 'ItemPageID', NULL, NULL, 'dbo', 'Item', 'ItemID', 'dbo', 'Page', 'PageID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'ItemRelationship', 'RelationshipID', NULL, NULL, 'dbo', 'Item', 'ParentID', 'dbo', 'Item', 'ChildID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'ItemStatus', 'ItemStatusID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'ItemTitle', 'ItemTitleID', NULL, NULL, 'dbo', 'Title', 'TitleID', 'dbo', 'Item', 'ItemID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Keyword', 'KeywordID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Language', 'LanguageCode', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'MaterialType', 'MaterialTypeID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Name', 'NameID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'NameIdentifier', 'NameIdentifierID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'NamePage', 'NamePageID', NULL, NULL, 'dbo', 'Name', 'NameID', 'dbo', 'Page', 'PageID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'NameResolved', 'NameResolvedID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'NameSource', 'NameSourceID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'NoteType', 'NoteTypeID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Page', 'PageID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'I'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Page', 'PageID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'U'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Page_PageType', 'PageID', 'PageTypeID', NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'PageType', 'PageTypeID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'PaginationStatus', 'PaginationStatusID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Segment', 'SegmentID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'I'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Segment', 'SegmentID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'U'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'SegmentCluster', 'SegmentClusterID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'SegmentClusterSegment', 'SegmentID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'SegmentExternalResource', 'SegmentExternalResourceID', NULL, NULL, 'dbo', 'Segment', 'SegmentID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'SegmentGenre', 'SegmentGenreID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Title', 'TitleID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'I'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Title', 'TitleID', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'U'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Title_Identifier', 'TitleIdentifierID', NULL, NULL, 'dbo', 'Title', 'TitleID', 'dbo', 'Identifier', 'IdentifierID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleAssociation', 'TitleAssociationID', NULL, NULL, 'dbo', 'Title', 'TitleID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleAssociation_TitleIdentifier', 'TitleAssociation_TitleIdentifierID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleAssociationType', 'TitleAssociationTypeID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleAuthor', 'TitleAuthorID', NULL, NULL, 'dbo', 'Title', 'TitleID', 'dbo', 'Author', 'AuthorID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleCollection', 'TitleCollectionID', NULL, NULL, 'dbo', 'Title', 'TitleID', 'dbo', 'Collection', 'CollectionID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleDocument', 'TitleDocumentID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleKeyword', 'TitleKeywordID', NULL, NULL, 'dbo', 'Title', 'TitleID', 'dbo', 'Keyword', 'KeywordID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleLanguage', 'TitleLanguageID', NULL, NULL, 'dbo', 'Title', 'TitleID', 'dbo', 'Language', 'LanguageCode'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleNote', 'TitleNoteID', NULL, NULL, 'dbo', 'Title', 'TitleID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleExternalResource', 'TitleExternalResourceID', NULL, NULL, 'dbo', 'Title', 'TitleID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleVariant', 'TitleVariantID', NULL, NULL, 'dbo', 'Title', 'TitleID'
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'TitleVariantType', 'TitleVariantTypeID', NULL, NULL
exec [audit].[AuditBasicCreateTrigger] 'dbo', 'Vault', 'VaultID', NULL, NULL

END

GO
