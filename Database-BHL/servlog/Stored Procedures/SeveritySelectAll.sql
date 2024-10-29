CREATE PROCEDURE servlog.SeveritySelectAll

AS

BEGIN

SET NOCOUNT ON

SELECT	SeverityID,
		[Name],
		[Label],
		FGColorHexCode,
		CreationDate,
		CreationUserID,
		LastModifiedDate,
		LastModifiedUserID
FROM	servlog.Severity
ORDER BY [Label]

END

GO