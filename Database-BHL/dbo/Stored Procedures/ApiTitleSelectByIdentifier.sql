
CREATE PROCEDURE [dbo].[ApiTitleSelectByIdentifier]

@IdentifierName nvarchar(40),
@IdentifierValue nvarchar(125)

AS 

BEGIN

SET NOCOUNT ON

DECLARE @TitleID int
DECLARE @RedirectTitleID int

SELECT	@TitleID = t.TitleID,
		@RedirectTitleID = t.RedirectTitleID
FROM	dbo.Title t INNER JOIN dbo.Title_Identifier ti
			ON t.TitleID = ti.TitleID
		INNER JOIN dbo.Identifier i
			ON ti.IdentifierID = i.IdentifierID
WHERE	IdentifierName = @IdentifierName
AND		IdentifierValue = @IdentifierValue


IF (@RedirectTitleID IS NOT NULL)
	exec dbo.ApiTitleSelectAuto @RedirectTitleID
ELSE
	exec dbo.ApiTitleSelectAuto @TitleID

END

