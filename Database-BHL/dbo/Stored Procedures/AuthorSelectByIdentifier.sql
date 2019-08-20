CREATE PROCEDURE [dbo].[AuthorSelectByIdentifier]

@IdentifierID int,
@IdentifierValue nvarchar(125)

AS 

SET NOCOUNT ON

SELECT	ai.AuthorIdentifierID,
		ai.AuthorID,
		n.FullName,
		a.Numeration,
		a.Title,
		a.Unit,
		a.Location,
		n.FullerForm,
		a.StartDate,
		a.EndDate
FROM	dbo.AuthorIdentifier ai
		INNER JOIN dbo.Author a ON ai.AuthorID = a.AuthorID
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
WHERE	IdentifierID = @IdentifierID
AND		IdentifierValue = @IdentifierValue
AND		n.IsPreferredName = 1
AND		a.IsActive = 1
