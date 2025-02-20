SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TitleCollectionInsertTitlesForCollection]

@CollectionID int

AS

BEGIN

DECLARE @InstitutionCode nvarchar(10)
DECLARE @LanguageCode nvarchar(10)

-- Get the institution and language for this collection
SELECT	@InstitutionCode = InstitutionCode,
		@LanguageCode = LanguageCode
FROM	dbo.Collection
WHERE	CanContainTitles = 1
AND		CollectionID = @CollectionID

-- Only add titles if an institution or language was specified
IF (@InstitutionCode IS NOT NULL OR @LanguageCode IS NOT NULL)
BEGIN
	-- Only add titles not already in the collection
	INSERT	dbo.TitleCollection (TitleID, CollectionID)
	SELECT DISTINCT
			t.TitleID, @CollectionID
	FROM	dbo.Title t 
			LEFT JOIN dbo.TitleCollection tc ON t.TitleID = tc.TitleID AND tc.CollectionID = @CollectionID
			INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
			INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
			INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
			INNER JOIN dbo.ItemInstitution ii ON i.ItemID = ii.ItemID
			INNER JOIN dbo.InstitutionRole r  ON ii.InstitutionRoleID = r.InstitutionRoleID  AND r.InstitutionRoleName = 'Holding Institution'
	WHERE	(ii.InstitutionCode = @InstitutionCode OR @InstitutionCode IS NULL)
	AND		(b.LanguageCode = @LanguageCode OR @LanguageCode IS NULL)
	AND		tc.TitleCollectionID IS NULL
END

END


GO
