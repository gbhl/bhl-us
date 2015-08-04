
CREATE PROCEDURE [audit].[AuditBasicDropAllTriggers]

AS

BEGIN

SET NOCOUNT ON

exec [audit].[AuditBasicDropTrigger] 'dbo', 'Vault'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'Title'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'TitleLanguage'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'TitleItem'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'TitleAssociation'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'TitleAssociation_TitleIdentifier'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'TitleVariant'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'Item'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'ItemLanguage'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'Page'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'IndicatedPage'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'Page_PageType'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'Location'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'Collection'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'ItemCollection'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'TitleCollection'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'PageType'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'ItemStatus'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'PaginationStatus'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'Language'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'BibliographicLevel'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'TitleVariantType'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'TitleAssociationType'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'DOI'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'DOIEntityType'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'DOIStatus'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'Institution'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'AspNetUsers'

-- Added for Segment additions to data model
exec [audit].[AuditBasicDropTrigger] 'dbo', 'Author'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'AuthorName'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'AuthorType'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'AuthorRole'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'AuthorIdentifier'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'TitleAuthor'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'Keyword'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'TitleKeyword'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'Identifier'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'Title_Identifier'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'Segment'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'SegmentGenre'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'SegmentPage'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'SegmentKeyword'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'SegmentAuthor'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'SegmentIdentifier'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'SegmentCluster'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'SegmentClusterSegment'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'SegmentStatus'

-- Added for Name additions to data model
exec [audit].[AuditBasicDropTrigger] 'dbo', 'Name'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'NamePage'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'NameResolved'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'NameIdentifier'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'NameSegment'
exec [audit].[AuditBasicDropTrigger] 'dbo', 'NameSource'

END

