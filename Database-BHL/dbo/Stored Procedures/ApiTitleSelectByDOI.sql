CREATE PROCEDURE [dbo].[ApiTitleSelectByDOI]

@DOIName nvarchar(50)

AS

BEGIN

SET NOCOUNT ON

DECLARE @TitleID int
DECLARE @RedirectTitleID int

DECLARE @IdentifierIDDOI int
SELECT @IdentifierIDDOI = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

SELECT	@TitleID = t.TitleID,
		@RedirectTitleID = t.RedirectTitleID
FROM	dbo.Title t 
		INNER JOIN dbo.Title_Identifier ti ON t.TitleID = ti.TitleID AND ti.IdentifierID = @IdentifierIDDOI
WHERE	ti.IdentifierValue = @DOIName

IF (@RedirectTitleID IS NOT NULL)
	exec dbo.ApiTitleSelectAuto @RedirectTitleID
ELSE
	exec dbo.ApiTitleSelectAuto @TitleID

END

GO
