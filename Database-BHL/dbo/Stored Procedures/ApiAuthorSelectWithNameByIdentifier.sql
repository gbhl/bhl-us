CREATE PROCEDURE [dbo].[ApiAuthorSelectWithNameByIdentifier]

@IdentifierName nvarchar(40),
@IdentifierValue nvarchar(125)

AS 

SET NOCOUNT ON

DECLARE @IdentifierID int
DECLARE @RedirectID int

SELECT @IdentifierID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = @IdentifierName

SELECT	@RedirectID = RedirectAuthorID 
FROM	dbo.Author a INNER JOIN dbo.AuthorIdentifier ai ON a.AuthorID = ai.AuthorID
WHERE	ai.IdentifierID = @IdentifierID
AND		ai.IdentifierValue = @IdentifierValue

IF (@RedirectID IS NOT NULL)
	exec [dbo].[ApiAuthorSelectWithNameByAuthorID] @RedirectID
ELSE
	SELECT	a.AuthorID,
			a.AuthorTypeID,
			n.FullName,
			a.Numeration,
			a.Title,
			a.Unit,
			a.Location,
			n.FullerForm,
			a.StartDate,
			a.EndDate,
			a.StartDate + CASE WHEN a.StartDate <> '' THEN '-' ELSE '' END + a.EndDate AS Dates,
			a.IsActive,
			a.RedirectAuthorID,
			a.CreationDate,
			a.LastModifiedDate,
			a.CreationUserID,
			a.LastModifiedUserID
	FROM	dbo.Author a 
			INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
			INNER JOIN dbo.AuthorIdentifier ai ON a.AuthorID = ai.AuthorID
	WHERE	ai.IdentifierID = @IdentifierID
	AND		ai.IdentifierValue = @IdentifierValue
	AND		n.IsPreferredName = 1
	AND		a.IsActive = 1
