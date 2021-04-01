CREATE PROCEDURE [dbo].[ApiTitleSelectByDOI]

@DOIName nvarchar(50)

AS

BEGIN

SET NOCOUNT ON

DECLARE @TitleID int
DECLARE @RedirectTitleID int

SELECT	@TitleID = t.TitleID,
		@RedirectTitleID = t.RedirectTitleID
FROM	dbo.Title t INNER JOIN dbo.DOI d
			ON t.TitleID = d.EntityID
			AND d.IsValid = 1 
			AND d.DOIStatusID IN (100, 200)
		INNER JOIN dbo.DOIEntityType et
			ON d.DOIEntityTypeID = et.DOIEntityTypeID
			AND	et.DOIEntityTypeName = 'Title'
WHERE	d.DOIName = @DOIName

IF (@RedirectTitleID IS NOT NULL)
	exec dbo.ApiTitleSelectAuto @RedirectTitleID
ELSE
	exec dbo.ApiTitleSelectAuto @TitleID

END

GO
