CREATE PROCEDURE [dbo].[OAIIdentifierSelectPDFs]

@MaxIdentifiers int = 100,
@StartID int = 1,
@FromDate DATETIME = null,
@UntilDate DATETIME = null

AS

BEGIN

SET NOCOUNT ON

SELECT	TOP(@MaxIdentifiers) PDFID AS ID, 'articlepdf' AS SetSpec, LastModifiedDate
FROM	dbo.PDF
WHERE	(LastModifiedDate > @FromDate OR @FromDate IS NULL)
AND		(LastModifiedDate < @UntilDate + 1 OR @UntilDate IS NULL)
AND		(PDFID > @StartID)
AND		(PdfStatusID = 30)
AND		(ArticleTitle <> '')
ORDER BY PDFID

END
