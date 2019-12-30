CREATE PROCEDURE [dbo].[NameResolvedSelectByResolvedName]

@ResolvedNameString nvarchar(100)

AS 

SET NOCOUNT ON

SELECT	NameResolvedID,
		ResolvedNameString,
		CanonicalNameString,
		IsPreferred
FROM	dbo.NameResolved
WHERE	ResolvedNameString = @ResolvedNameString
