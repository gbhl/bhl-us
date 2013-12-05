CREATE PROCEDURE [dbo].[NamePageUpdate]

@NamePageID int,
@NameString nvarchar(100),
@ResolvedNameString nvarchar(100),
@NameBankID nvarchar(100),
@EOLID nvarchar(100),
@IsFirstOccurrence smallint = 0,
@CreationUserID int = 1,
@LastModifiedUserID int = 1

AS
/*
 *	Use the specified values to update and (if necessary) delete NamePage, 
 *  Name, NameResolved, and NameIdentifier records.
 */
BEGIN

SET NOCOUNT ON

DECLARE @NameID int
DECLARE @OldNameResolvedID int
DECLARE @NameResolvedID int

SET @NameResolvedID = NULL
IF @NameBankID = '' SET @NameBankID = NULL
IF @EOLID = '' SET @EOLID = NULL

-- Get some IDs for use later
SELECT	@NameID = np.NameID, @OldNameResolvedID = n.NameResolvedID 
FROM	dbo.NamePage np INNER JOIN dbo.Name n ON np.NameID = n.NameID
WHERE	np.NamePageID = @NamePageID

-- Update the FirstOccurrence flag in the NamePage table
UPDATE	dbo.NamePage
SET		IsFirstOccurrence = @IsFirstOccurrence,
		LastModifiedDate = GETDATE(),
		LastModifiedUserID = @LastModifiedUserID
WHERE	NamePageID = @NamePageID
AND		IsFirstOccurrence <> @IsFirstOccurrence

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
		VALUES (@ResolvedNameString, @ResolvedNameString, @CreationUserID, @LastModifiedUserID)

		SET @NameResolvedID = SCOPE_IDENTITY()
	END

	DECLARE @NameBank int
	DECLARE @EOL int
	SELECT @NameBank = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'NameBank'
	SELECT @EOL = IdentifierID FROM dbo.Identifier WHERE IdentifierName = 'EOL'

	-- If a NameBank ID was supplied
	IF (ISNULL(@NameBankID, '') <> '')
	BEGIN
		IF EXISTS(	SELECT NameIdentifierID FROM dbo.NameIdentifier 
					WHERE NameResolvedID = @NameResolvedID AND IdentifierID = @NameBank	)
		BEGIN
			-- Update the existing NameIdentifier record
			UPDATE	dbo.NameIdentifier 
			SET		IdentifierValue = @NameBankID,
					LastModifiedDate = GETDATE(),
					LastModifiedUserID = @LastModifiedUserID
			WHERE	NameResolvedID = @NameResolvedID 
			AND		IdentifierID = @NameBank
			AND		IdentifierValue <> @NameBankID
		END
		ELSE
		BEGIN
			-- Add a new NameIdentifier record
			INSERT dbo.NameIdentifier (NameResolvedID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
			VALUES (@NameResolvedID, @NameBank, @NameBankID, @CreationUserID, @LastModifiedUserID)
		END

		-- If there is an EOL ID that matches the specified NameBankID, use it rather than the user-supplied value
		SELECT	@EOLID = CONVERT(nvarchar(100), MIN(EOLID))
		FROM	dbo.NameBankEOL
		WHERE	NameBankID = @NameBankID
	END
	ELSE
	BEGIN
		-- Remove any existing NameBank IDs for this name
		DELETE dbo.NameIdentifier WHERE NameResolvedID = @NameResolvedID AND IdentifierID = @NameBank
	END
	
	-- If we have an EOL ID
	IF (ISNULL(@EOLID, '') <> '')
	BEGIN
		IF EXISTS(	SELECT NameIdentifierID FROM dbo.NameIdentifier 
					WHERE NameResolvedID = @NameResolvedID AND IdentifierID = @EOL	)
		BEGIN
			-- Update the existing NameIdentifier record
			UPDATE	dbo.NameIdentifier 
			SET		IdentifierValue = @EOLID,
					LastModifiedDate = GETDATE(),
					LastModifiedUserID = @LastModifiedUserID
			WHERE	NameResolvedID = @NameResolvedID 
			AND		IdentifierID = @EOL
			AND		IdentifierValue <> @EOLID
		END
		ELSE
		BEGIN
			-- Add a new NameIdentifier record
			INSERT dbo.NameIdentifier (NameResolvedID, IdentifierID, IdentifierValue, CreationUserID, LastModifiedUserID)
			VALUES (@NameResolvedID, @EOL, @EOLID, @CreationUserID, @LastModifiedUserID)
		END
	END
	ELSE
	BEGIN
		-- Remove any existing EOL IDs for this name
		DELETE dbo.NameIdentifier WHERE NameResolvedID = @NameResolvedID AND IdentifierID = @EOL
	END
END

-- If the name string or resolved name has been updated, update the Name record
UPDATE	dbo.Name
SET		NameString = @NameString,
		NameResolvedID = @NameResolvedID,
		LastModifiedDate = GETDATE(),
		LastModifiedUserID = @LastModifiedUserID
WHERE	NameID = @NameID
AND		(NameString <> @NameString OR ISNULL(NameResolvedID, -1) <> ISNULL(@NameResolvedID, -1))

-- If the resolved name previously referenced by the Name record is no longer used by ANY Name record, remove it
IF (@NameResolvedID IS NULL AND 
	@OldNameResolvedID IS NOT NULL AND
	NOT EXISTS(SELECT NameID FROM dbo.Name WHERE NameResolvedID = @OldNameResolvedID))
BEGIN
	DELETE dbo.NameIdentifier WHERE NameResolvedID = @OldNameResolvedID
	DELETE dbo.NameResolved WHERE NameResolvedID = @OldNameResolvedID
END

END

