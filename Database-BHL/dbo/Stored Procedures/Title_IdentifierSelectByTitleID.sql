CREATE PROCEDURE [dbo].[Title_IdentifierSelectByTitleID]

@TitleID INT,
@Display SMALLINT = NULL

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
AND		(i.Display = @Display OR @Display IS NULL)
ORDER BY i.IdentifierLabel, ti.IdentifierValue

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Title_IdentifierSelectByTitleID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
