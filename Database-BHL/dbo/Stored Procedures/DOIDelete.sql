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

	-- Get the prefix of the DOI being deleted
	DECLARE @DOIPrefix nvarchar(30)

	IF @DOIEntityTypeID = @DOITitleEntityTypeID
	BEGIN
		SELECT	@DOIPrefix = IdentifierValue
		FROM	dbo.Title_Identifier
		WHERE	TitleID = @EntityID 
		AND		IdentifierID = @IdentifierDOIID
	END
	IF @DOIEntityTypeID = @DOISegmentEntityTypeID
	BEGIN
		SELECT	@DOIPrefix = IdentifierValue
		FROM	dbo.ItemIdentifier ii
				INNER JOIN dbo.Segment s ON ii.ItemID = s.ItemID
		WHERE	s.SegmentID = @EntityID 
		AND		ii.IdentifierID = @IdentifierDOIID
	END
	SET @DOIPrefix = SUBSTRING(@DOIPrefix, 1, 
						CASE WHEN CHARINDEX('/', @DOIPrefix) > 0 
							THEN CHARINDEX('/', @DOIPrefix) - 1 
							ELSE LEN(@DOIPrefix) 
						END)

	-- If the DOI is not a BHL-managed DOI and has not been excluded, then proceed with the deletes
	IF (@DOIPrefix NOT IN (SELECT Prefix from dbo.DOIPrefix) OR @ExcludeBHLDOI = 0)
	BEGIN
		-- Mark the DOI record as invalid
		UPDATE 	dbo.DOI 
		SET 	IsValid = 0, LastModifiedDate = GETDATE(), LastModifiedUserID = @UserID 
		WHERE 	DOIEntityTypeID = @DOIEntityTypeID 
		AND 	EntityID = @EntityID 
		AND 	IsValid = 1

		-- Delete from the appropriate Identifier table
		IF @DOIEntityTypeID = @DOITitleEntityTypeID
		BEGIN
			DELETE	dbo.Title_Identifier 
			WHERE	TitleID = @EntityID 
			AND		IdentifierID = @IdentifierDOIID
		END
		IF @DOIEntityTypeID = @DOISegmentEntityTypeID
		BEGIN
			DELETE	ii
			FROM	dbo.ItemIdentifier ii
					INNER JOIN dbo.Segment s ON ii.ItemID = s.ItemID
			WHERE	s.SegmentID = @EntityID 
			AND		ii.IdentifierID = @IdentifierDOIID
		END
	END

END
GO
