CREATE PROCEDURE [dbo].[NameIdentifierSelectForResolvedName]

@ResolvedNameString NVARCHAR(100)

AS 

SET NOCOUNT ON

SELECT	i.IdentifierName,
		ni.IdentifierValue
FROM	dbo.NameResolved nr WITH (NOLOCK)
		INNER JOIN dbo.NameIdentifier ni WITH (NOLOCK) ON nr.NameResolvedID = ni.NameResolvedID
		INNER JOIN dbo.Identifier i WITH (NOLOCK) ON ni.IdentifierID = i.IdentifierID
WHERE 	nr.ResolvedNameString = @ResolvedNameString

