﻿CREATE PROCEDURE dbo.NamePageInsert

@PageID int,
@NameString nvarchar(100),
@ResolvedNameString nvarchar(100),
@CanonicalNameString nvarchar(100),
@IdentifierList IdentifierTableType READONLY,
@SourceName nvarchar(50),
@IsFirstOccurrence smallint = 0,
@CreationUserID int = 1,
@LastModifiedUserID int = 1

AS
/*
 *	Use the specified values to insert NamePage, Name, NameResolved, 
 *	and NameIdentifier records.  Check for existence of records before 
 *	inserting new ones.
 * 
 *  @IdentifierString should be a table of identifier name-value pairs
 *		Example:	NameBank	12345
 *					EOL			67890
 *					GNI			456-645-456
 *
 *  Identifier names (i.e. 'NameBank') should match the IdentifierName 
 *	values in the dbo.Identifier table.
 */
BEGIN

SET NOCOUNT ON

DECLARE @NameID int
DECLARE @NameResolvedID int
DECLARE @NameIdentifierID int
DECLARE @NamePageID int
DECLARE @NameSourceID int

SET @NamePageID = 0
SET @NameSourceID = NULL
SET @NameResolvedID = NULL

-- If a resolved name string is available
IF (ISNULL(@ResolvedNameString, '') <> '')
BEGIN

	-- See if we already have a NameResolved record for this resolved name string
	SELECT	@NameResolvedID = NameResolvedID 
	FROM	dbo.NameResolved 
	WHERE	ResolvedNameString = @ResolvedNameString

	IF (@NameResolvedID IS NULL)
	BEGIN
		INSERT dbo.NameResolved (ResolvedNameString, CanonicalNameString, CreationUserID, LastModifiedUserID)
		VALUES (@ResolvedNameString, @CanonicalNameString, @CreationUserID, @LastModifiedUserID)

		SET @NameResolvedID = SCOPE_IDENTITY()
	END

	-- If identifiers were supplied
	IF EXISTS(SELECT * FROM @IdentifierList)
	BEGIN
		-- Move identifiers into an updateable temp table
		SELECT	IdentifierName AS Name, IdentifierValue as Value
		INTO	#Identifiers
		FROM	@IdentifierList

		-- Convert Identifier Names to BHL-preferred values
		UPDATE #Identifiers SET Name = 'EOL' WHERE Name = 'Encyclopedia of Life'
		UPDATE #Identifiers SET Name = 'NameBank' WHERE Name = 'uBio NameBank'
		UPDATE #Identifiers SET Value = REPLACE(Value, 'urn:lsid:ubio.org:namebank:', '') WHERE Name = 'NameBank'
		
		-- Get the IdentifierID for each item in the @IdentifierString
		SELECT	i.IdentifierID, t.Value
		INTO	#NameIDs
		FROM	#Identifiers t INNER JOIN dbo.Identifier i ON t.Name = i.IdentifierName

		-- Add new identifiers to the NameIdentifier table
		INSERT	dbo.NameIdentifier (NameResolvedID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
		SELECT	@NameResolvedID, t.IdentifierID, Value, @CreationUserID, @LastModifiedUserID
		FROM	#NameIDs t LEFT JOIN dbo.NameIdentifier ni
					ON ni.NameResolvedID = @NameResolvedID
					AND t.IdentifierID = ni.IdentifierID
					AND t.Value = ni.IdentifierValue
		WHERE	ni.NameIdentifierID IS NULL
	END
END

-- See if we already have a Name record for this namestring
SELECT @NameID = NameID FROM dbo.Name WHERE NameString = @NameString
SELECT @NameSourceID = NameSourceID FROM dbo.NameSource WHERE SourceName = @SourceName

IF (@NameID IS NULL)
BEGIN
	-- Insert a new Name record
	INSERT dbo.Name (NameSourceID, NameResolvedID, NameString, IsActive, CreationUserID, LastModifiedUserID)
	VALUES (@NameSourceID, @NameResolvedID, @NameString, 1, @CreationUserID, @LastModifiedUserID)
	
	SET @NameID = SCOPE_IDENTITY()
END
ELSE
BEGIN
	-- Update the existing Name record
	UPDATE	dbo.Name
	SET		NameResolvedID = @NameResolvedID,
			NameSourceID = @NameSourceID,
			LastModifiedDate = GETDATE(),
			LastModifiedUserID = @LastModifiedUserID
	WHERE	NameID = @NameID
	AND		ISNULL(NameResolvedID, -1) <> ISNULL(@NameResolvedID, -1)
END

-- Insert a NamePage record if one does not already exists for this Name/Page combination
IF NOT EXISTS(SELECT NamePageID FROM dbo.NamePage WHERE PageID = @PageID AND NameID = @NameID)
BEGIN
	INSERT dbo.NamePage (NameID, PageID, NameSourceID, IsFirstOccurrence, CreationUserID, LastModifiedUserID)
	VALUES (@NameID, @PageID, @NameSourceID, @IsFirstOccurrence, @CreationUserID, @LastModifiedUserID)
	
	SET @NamePageID = SCOPE_IDENTITY()
END

-- Return the newly inserted NamePage record (if an insert was performed)
SELECT	NamePageID,
		NameID,
		PageID,
		NameSourceID,
		IsFirstOccurrence,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.NamePage
WHERE	NamePageID = @NamePageID

END

GO
