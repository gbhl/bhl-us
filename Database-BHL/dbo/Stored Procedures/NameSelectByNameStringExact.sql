
CREATE PROCEDURE [dbo].[NameSelectByNameStringExact]

@NameString nvarchar(100)

AS

BEGIN

SET NOCOUNT ON

SELECT	n.NameID,
		n.NameSourceID,
		n.NameString,
		nr.ResolvedNameString,
		nr.CanonicalNameString,
		n.IsActive,
		n.CreationDate,
		n.LastModifiedDate,
		n.CreationUserID,
		n.LastModifiedUserID
FROM	dbo.Name n LEFT JOIN dbo.NameResolved nr ON n.NameResolvedID = nr.NameResolvedID
WHERE	NameString = LTRIM(RTRIM(@NameString))
ORDER BY
		n.NameString

END



