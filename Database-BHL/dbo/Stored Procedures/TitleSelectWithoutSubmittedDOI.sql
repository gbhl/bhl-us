CREATE PROCEDURE [dbo].[TitleSelectWithoutSubmittedDOI]

@NumberToReturn int = 10

AS

BEGIN

SET NOCOUNT ON

DECLARE @NumToReturn int
SET @NumToReturn = @NumberToReturn

SELECT TOP (@NumToReturn)
		d.DOIID,
		d.DOIEntityTypeID,
		d.EntityID,
		d.DOIStatusID,
		d.DOIName
FROM	dbo.DOI d WITH (NOLOCK)
		INNER JOIN dbo.Title t WITH (NOLOCK) ON d.EntityID = t.TitleID AND d.DOIEntityTypeID = 10 -- Title
--WHERE	t.PublishReady = 1
WHERE d.DOIStatusID = 30 -- Queued

END

GO
