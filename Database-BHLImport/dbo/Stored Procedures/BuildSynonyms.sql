
CREATE PROCEDURE [dbo].[BuildSynonyms]
	@DBName NVARCHAR(50)
AS
BEGIN

SET NOCOUNT ON

IF  EXISTS (SELECT * FROM sys.synonyms WHERE name = N'BHLTitle')
DROP SYNONYM [dbo].[BHLTitle]

IF  EXISTS (SELECT * FROM sys.synonyms WHERE name = N'BHLCreatorRoleType')
DROP SYNONYM [dbo].[BHLCreatorRoleType]

IF  EXISTS (SELECT * FROM sys.synonyms WHERE name = N'BHLConfiguration')
DROP SYNONYM [dbo].[BHLConfiguration]

IF  EXISTS (SELECT * FROM sys.synonyms WHERE name = N'BHLTitleTag')
DROP SYNONYM [dbo].[BHLTitleTag]

IF  EXISTS (SELECT * FROM sys.synonyms WHERE name = N'BHLCreator')
DROP SYNONYM [dbo].[BHLCreator]

IF  EXISTS (SELECT * FROM sys.synonyms WHERE name = N'BHLTitleCreator')
DROP SYNONYM [dbo].[BHLTitleCreator]

IF  EXISTS (SELECT * FROM sys.synonyms WHERE name = N'BHLItem')
DROP SYNONYM [dbo].[BHLItem]

IF  EXISTS (SELECT * FROM sys.synonyms WHERE name = N'BHLPage')
DROP SYNONYM [dbo].[BHLPage]

IF  EXISTS (SELECT * FROM sys.synonyms WHERE name = N'BHLPagePageType')
DROP SYNONYM [dbo].[BHLPagePageType]

IF  EXISTS (SELECT * FROM sys.synonyms WHERE name = N'BHLIndicatedPage')
DROP SYNONYM [dbo].[BHLIndicatedPage]

IF  EXISTS (SELECT * FROM sys.synonyms WHERE name = N'BHLPageName')
DROP SYNONYM [dbo].[BHLPageName]

EXEC ('CREATE SYNONYM [dbo].[BHLTitle] FOR ' + @DBName + '.[dbo].[Title]')
EXEC ('CREATE SYNONYM [dbo].[BHLCreatorRoleType] FOR ' + @DBName + '.[dbo].[CreatorRoleType]')
EXEC ('CREATE SYNONYM [dbo].[BHLConfiguration] FOR ' + @DBName + '.[dbo].[Configuration]')
EXEC ('CREATE SYNONYM [dbo].[BHLTitleTag] FOR ' + @DBName + '.[dbo].[TitleTag]')
EXEC ('CREATE SYNONYM [dbo].[BHLCreator] FOR ' + @DBName + '.[dbo].[Creator]')
EXEC ('CREATE SYNONYM [dbo].[BHLTitleCreator] FOR ' + @DBName + '.[dbo].[Title_Creator]')
EXEC ('CREATE SYNONYM [dbo].[BHLItem] FOR ' + @DBName + '.[dbo].[Item]')
EXEC ('CREATE SYNONYM [dbo].[BHLPage] FOR ' + @DBName + '.[dbo].[Page]')
EXEC ('CREATE SYNONYM [dbo].[BHLPagePageType] FOR ' + @DBName + '.[dbo].[Page_PageType]')
EXEC ('CREATE SYNONYM [dbo].[BHLIndicatedPage] FOR ' + @DBName + '.[dbo].[IndicatedPage]')
EXEC ('CREATE SYNONYM [dbo].[BHLPageName] FOR ' + @DBName + '.[dbo].[PageName]')

END
