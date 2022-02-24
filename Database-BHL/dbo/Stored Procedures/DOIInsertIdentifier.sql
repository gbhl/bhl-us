CREATE PROCEDURE dbo.DOIInsertIdentifier

@DOIEntityTypeID int,
@EntityID int,
@DOIName nvarchar(50),  -- expand DOI.DOIName to nvarchar(125)?
@UserID int = 1

AS

BEGIN
	SET NOCOUNT ON

	DECLARE @IdentifierDOIID int
	SELECT @IdentifierDOIID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

	DECLARE @DOITitleEntityTypeID int, @DOISegmentEntityTypeID int
	SELECT @DOITitleEntityTypeID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Title'
	SELECT @DOISegmentEntityTypeID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Segment'

	IF (@UserID IS NULL) SET @UserID = 1

	-- If DOI is approved or external, and is valid, then insert a record in the appropriate Identifier table
	IF @DOIEntityTypeID = @DOITitleEntityTypeID
	BEGIN
		INSERT dbo.Title_Identifier (TitleID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID) VALUES (@EntityID, @IdentifierDOIID, @DOIName, @UserID, @UserID)
	END
	IF @DOIEntityTypeID = @DOISegmentEntityTypeID
	BEGIN
		INSERT dbo.ItemIdentifier (ItemID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
		SELECT ItemID, @IdentifierDOIID, @DOIName, @UserID, @UserID FROM dbo.Segment WHERE SegmentID = @EntityID
	END
END

GO
