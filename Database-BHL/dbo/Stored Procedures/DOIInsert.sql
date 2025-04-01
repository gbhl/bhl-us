CREATE PROCEDURE [dbo].[DOIInsert]

@DOIEntityTypeID int,
@EntityID int,
@DOIStatusID int,
@DOIName nvarchar(50),  -- expand DOI.DOIName to nvarchar(125)?
@IsValid smallint,
@DOIBatchID nvarchar(50) = '',
@StatusMessage nvarchar(1000) = '',
@UserID int = 1,
@ExcludeBHLDOI int = 1

AS
BEGIN
	SET NOCOUNT ON

	-- Get the prefix of the DOI being inserted
	DECLARE @DOIPrefix nvarchar(30)
	SET @DOIPrefix = SUBSTRING(@DOIName, 1, 
						CASE WHEN CHARINDEX('/', @DOIName) > 0 
							THEN CHARINDEX('/', @DOIName) - 1 
							ELSE LEN(@DOIName) 
						END)

	IF @DOIName <> '' AND @DOIName IS NOT NULL AND (@DOIPrefix NOT IN (SELECT Prefix FROM dbo.DOIPrefix) OR @ExcludeBHLDOI = 0)
	BEGIN
		DECLARE @IdentifierDOIID int
		SELECT @IdentifierDOIID = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'DOI'

		DECLARE @DOITitleEntityTypeID int, @DOISegmentEntityTypeID int
		SELECT @DOITitleEntityTypeID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Title'
		SELECT @DOISegmentEntityTypeID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Segment'

		DECLARE @DOIExternalStatusID int, @DOIApprovedStatusID int
		SELECT @DOIApprovedStatusID = DOIStatusID FROM dbo.DOIStatus WHERE DOIStatusName = 'DOI Approved'
		SELECT @DOIExternalStatusID = DOIStatusID FROM dbo.DOIStatus WHERE DOIStatusName = 'External DOI'

		-- Add a new record to the DOI table if this is not an external DOI
		IF @DOIStatusID <> @DOIExternalStatusID
		BEGIN
			INSERT	dbo.DOI (DOIEntityTypeID, EntityID, DOIStatusID, DOIBatchID, DOIName, StatusDate, StatusMessage, IsValid, CreationUserID, LastModifiedUserID)
			VALUES	(@DOIEntityTypeID, @EntityID, @DOIStatusID, @DOIBatchID, @DOIName, GETDATE(), @StatusMessage, @IsValid, @UserID, @UserID)
		END

		-- If the DOI is a valid external DOI, then remove any DOI Queue entries for this entity
		IF (@DOIStatusID = @DOIExternalStatusID AND @IsValid = 1)
		BEGIN
			DELETE FROM dbo.DOI WHERE DOIEntityTypeID = @DOIEntityTypeID AND EntityID = @EntityID
		END

		-- If DOI is approved or external, and is valid, then insert a record in the appropriate Identifier table
		IF (@DOIStatusID = @DOIApprovedStatusID OR @DOIStatusID = @DOIExternalStatusID) AND @IsValid = 1
		BEGIN
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
	END
END

GO
