
CREATE PROCEDURE [dbo].[ApiNameResolvedCountUnique]

AS 

SET NOCOUNT ON

-- Count total names
SELECT	COUNT(*)
FROM	NameResolved WITH (NOLOCK)

