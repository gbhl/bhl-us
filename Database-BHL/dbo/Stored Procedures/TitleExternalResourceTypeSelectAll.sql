CREATE PROCEDURE dbo.TitleExternalResourceTypeSelectAll

AS

BEGIN

SET NOCOUNT ON

SELECT	TitleExternalResourceTypeID,
		ExternalResourceTypeName,
		ExternalResourceTypeLabel,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	TitleExternalResourceType
ORDER BY
		TitleExternalResourceTypeID

END

GO
