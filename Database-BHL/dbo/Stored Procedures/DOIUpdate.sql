CREATE PROCEDURE dbo.DOIUpdate

@DOIEntityTypeID int,
@EntityID int,
@DOIStatusID int,
@DOIName nvarchar(50),  -- expand DOI.DOIName to nvarchar(125)?
@IsValid smallint,
@ProcessName nvarchar(200) = '',
@DOIBatchID nvarchar(50) = '',
@StatusMessage nvarchar(1000) = '',
@UserID int = 1,
@ExcludeBHLDOI int = 1

AS
BEGIN
	SET NOCOUNT ON

	DECLARE @OriginalDOIName nvarchar(200)

	DECLARE @DOITitleEntityTypeID int, @DOISegmentEntityTypeID int
	SELECT @DOITitleEntityTypeID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Title'
	SELECT @DOISegmentEntityTypeID = DOIEntityTypeID FROM dbo.DOIEntityType WHERE DOIEntityTypeName = 'Segment'

	-- If a DOI is being replaced, get the original DOI's value
	IF @DOIEntityTypeID = @DOITitleEntityTypeID
	BEGIN
		SELECT @OriginalDOIName = IdentifierValue FROM dbo.Title_Identifier WHERE TitleID = @EntityID
	END
	IF @DOIEntityTypeID = @DOISegmentEntityTypeID
	BEGIN
		SELECT @OriginalDOIName = IdentifierValue FROM dbo.ItemIdentifier bti INNER JOIN dbo.Segment s ON bti.ItemID = s.ItemID WHERE s.SegmentID = @EntityID
	END

	-- Delete the existing DOI
	IF @OriginalDOIName IS NOT NULL
	BEGIN
		EXEC dbo.DOIDelete @DOIEntityTypeID, @EntityID, @UserID, @ExcludeBHLDOI
	END

	-- Insert a new DOI
	EXEC dbo.DOIInsert @DOIEntityTypeID, @EntityID, @DOIStatusID, @DOIName, @IsValid, @DOIBatchID, @StatusMessage, @UserID, @ExcludeBHLDOI

	-- Get the prefix of the replaced DOI
	DECLARE @OrigDOIPrefix nvarchar(30)
	SET @OrigDOIPrefix = SUBSTRING(@OriginalDOIName, 1, 
						CASE WHEN CHARINDEX('/', @OriginalDOIName) > 0 
							THEN CHARINDEX('/', @OriginalDOIName) - 1 
							ELSE LEN(@OriginalDOIName) 
						END)

	IF @OriginalDOIName IS NOT NULL AND @DOIName <> @OriginalDOIName AND @ProcessName <> '' AND 
		(@OrigDOIPrefix NOT IN (SELECT Prefix FROM dbo.DOIPrefix) OR @ExcludeBHLDOI = 0)
	BEGIN
		-- If an existing DOI has been changed by a data harvest process, log the before/after DOI values
		INSERT	dbo.BHLImportDOIHarvestLog (HarvesterName, DOIEntityTypeID, EntityID, OriginalDOIName, NewDOIName)
		VALUES (@ProcessName, @DOIEntityTypeID, @EntityID, @OriginalDOIName, @DOIName)
	END

END
GO
