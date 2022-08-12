CREATE PROCEDURE [dbo].[DOISelectValidForTitle]

@TitleID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @DOIIdentifierID int
SELECT @DOIIdentifierID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

SELECT	ti.TitleIdentifierID,
		ti.IdentifierValue,
		ti.CreationDate,
		ti.LastModifiedDate,
		ti.CreationUserID,
		ti.LastModifiedUserID
FROM	dbo.Title_Identifier ti		
WHERE	ti.TitleID = @TitleID
AND		ti.IdentifierID = @DOIIdentifierID

END

GO
