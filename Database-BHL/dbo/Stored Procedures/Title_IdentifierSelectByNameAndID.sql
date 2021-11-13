CREATE PROCEDURE [dbo].[Title_IdentifierSelectByNameAndID]

@IdentifierName nvarchar(40),
@TitleID int

AS 

SET NOCOUNT ON

SELECT	ti.TitleIdentifierID,
		ti.TitleID,
		ti.IdentifierID,
		i.[IdentifierName],
		i.[IdentifierLabel],
		i.[Prefix],
		ti.[IdentifierValue]
FROM	[dbo].[Title_Identifier] ti 
		INNER JOIN [dbo].[Identifier] i ON ti.IdentifierID = i.IdentifierID
WHERE	ti.TitleID = @TitleID
AND		i.IdentifierName = @IdentifierName

GO
