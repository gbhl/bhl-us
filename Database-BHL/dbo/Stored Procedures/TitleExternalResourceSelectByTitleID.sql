CREATE PROCEDURE dbo.TitleExternalResourceSelectByTitleID

@TitleID int

AS

BEGIN

SET NOCOUNT ON

SELECT	r.TitleExternalResourceID,
		r.TitleID,
		r.TitleExternalResourceTypeID,
		rt.ExternalResourceTypeLabel,
		r.UrlText,
		r.Url,
		r.SequenceOrder,
		r.CreationDate,
		r.LastModifiedDate,
		r.CreationUserID,
		r.LastModifiedUserID
FROM	TitleExternalResource r
		INNER JOIN TitleExternalResourceType rt ON r.TitleExternalResourceTypeID = rt.TitleExternalResourceTypeID
WHERE	r.TitleID = @TitleID
ORDER BY
		r.SequenceOrder

END

GO
