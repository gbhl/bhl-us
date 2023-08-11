CREATE VIEW dbo.vwAuthorName WITH SCHEMABINDING
AS
-------------------------------------------------------------------------
--
-- Provides a combined view of the Author and AuthorName tables.
-- 
-- The FullNameToken, LastNameToken, and FirstNameToken columns provide 
-- pre-computed indexed fields for use during author resolution.  It is 
-- much more efficient to provide these values in an indexed view than to
-- compute them on the fly.
--
-------------------------------------------------------------------------
SELECT	n.AuthorNameID,
		a.AuthorID, 
		a.RedirectAuthorID,
		AuthorTypeID, 
		StartDate,
		EndDate, 
		FullName,
		dbo.fnRemoveNonAlphaNumericCharacters(FullName) AS FullNameToken,
		dbo.fnRemoveNonAlphaNumericCharacters(dbo.fnReverseAuthorName(FullName)) AS FullNameReversedToken,
		LastName,
		dbo.fnRemoveNonAlphaNumericCharacters(LastName) AS LastNameToken,
		FirstName,
		dbo.fnRemoveNonAlphaNumericCharacters(FirstName) AS FirstNameToken,
		a.Numeration, 
		a.Title, 
		a.Unit, 
		a.Location, 
		n.FullerForm,
		a.IsActive,
		n.IsPreferredName,
		n.CreationDate AS NameCreationDate,
		n.LastModifiedDate AS NameLastModifiedDate,
		n.CreationUserID AS NameCreationUserID,
		n.LastModifiedUserID AS NameLastsModifiedUserID,
		a.CreationDate,
		a.LastModifiedDate,
		a.CreationUserID,
		a.LastModifiedUserID
FROM	dbo.Author a INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID

GO

CREATE UNIQUE CLUSTERED INDEX vwAuthorName_FullNameTokenAuthorNameID
	ON vwAuthorName(FullNameToken, AuthorNameID)
GO

CREATE INDEX vwAuthorName_LNameTokenFNameToken
	ON vwAuthorName(LastNameToken, FirstNameToken)
GO

CREATE INDEX vwAuthorName_FullNameReversedToken
	ON vwAuthorName(FullNameReversedToken)
GO
