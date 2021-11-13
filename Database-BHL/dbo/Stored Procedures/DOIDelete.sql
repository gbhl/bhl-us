CREATE PROCEDURE dbo.DOIDelete

@DOIEntityTypeID int,
@EntityID int,
@UserID int = 1,
@ExcludeBHLDOI int = 1

AS
BEGIN
	SET NOCOUNT ON

	DECLARE @IdentifierDOIID int
	SELECT @IdentifierDOIID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

	DECLARE @DOITitleEntityTypeID int, @DOISegmentEntityTypeID int
	SELECT @DOITitleEntityTypeID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Title'
	SELECT @DOISegmentEntityTypeID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Segment'

	-- Mark the DOI record as invalid
	UPDATE 	dbo.DOI 
	SET 	IsValid = 0, LastModifiedDate = GETDATE(), LastModifiedUserID = @UserID 
	WHERE 	DOIEntityTypeID = @DOIEntityTypeID 
	AND 	EntityID = @EntityID 
	AND 	IsValid = 1
	AND		(DOIName NOT LIKE '%10.5962%' OR @ExcludeBHLDOI = 0)

	-- Delete from the appropriate Identifier table
	IF @DOIEntityTypeID = @DOITitleEntityTypeID
	BEGIN
		DELETE	dbo.Title_Identifier 
		WHERE	TitleID = @EntityID 
		AND		IdentifierID = @IdentifierDOIID
		AND		(IdentifierValue NOT LIKE '%10.5962%' OR @ExcludeBHLDOI = 0)
	END
	IF @DOIEntityTypeID = @DOISegmentEntityTypeID
	BEGIN
		DELETE	ii
		FROM	dbo.ItemIdentifier ii
				INNER JOIN dbo.Segment s ON ii.ItemID = s.ItemID
		WHERE	s.SegmentID = @EntityID 
		AND		ii.IdentifierID = @IdentifierDOIID
		AND		(ii.IdentifierValue NOT LIKE '%10.5962%' OR @ExcludeBHLDOI = 0)
	END

END
GO
