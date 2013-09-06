
CREATE PROCEDURE [dbo].[NameInsertComplete]

@NameSourceID int,
@NameString nvarchar(100),
@IsActive smallint,
@ResolvedNameString nvarchar(100),
@CanonicalNameString nvarchar(100),
@IsPreferred smallint,
@NameBankID nvarchar(100),
@EOLID nvarchar(100),
@CreationUserID int,
@LastModifiedUserID int

AS

BEGIN
/*
 *	This procedure should be called within the scope of a containing transaction.
 */

SET NOCOUNT ON

DECLARE @NameID int
DECLARE @NameResolvedID int

SET @NameResolvedID = NULL

IF (@ResolvedNameString <> '')
BEGIN
	INSERT dbo.NameResolved (ResolvedNameString, CanonicalNameString, 
		IsPreferred, CreationUserID, LastModifiedUserID)
	VALUES (@ResolvedNameString, @CanonicalNameString, 
		@IsPreferred, @CreationUserID, @LastModifiedUserID)

	SELECT @NameResolvedID = SCOPE_IDENTITY()

	-- If only a NameBank ID or only an EOL ID was supplied, see if we can identify the missing value
	IF (@NameBankID <> '' AND @EOLID = '')
	BEGIN
		SELECT @EOLID = EOLID FROM dbo.NameBankEOL WHERE NameBankID = @NameBankID
		SET @EOLID = ISNULL(@EOLID, '')
	END
	
	IF (@NameBankID = '' AND @EOLID <> '')
	BEGIN
		SELECT @NameBankID = NameBankID FROM dbo.NameBankEOL WHERE EOLID = @EOLID
		SET @NameBankID = ISNULL(@NameBankID, '')
	END
		
	-- Insert the identifier values
	IF (@NameBankID <> '')
	BEGIN
		DECLARE @NameBankIdentifierID int

		SELECT @NameBankIdentifierID = IdentifierID 
		FROM dbo.Identifier WHERE IdentifierName = 'NameBank'

		INSERT dbo.NameIdentifier (NameResolvedID, IdentifierID, IdentifierValue,
			CreationUserID, LastModifiedUserID)
		VALUES (@NameResolvedID, @NameBankIdentifierID, @NameBankID,
			@CreationUserID, @LastModifiedUserID)
	END
	
	IF (@EOLID <> '')
	BEGIN
		DECLARE @EOLIdentifierID int

		SELECT @EOLIdentifierID = IdentifierID 
		FROM dbo.Identifier WHERE IdentifierName = 'EOL'

		INSERT dbo.NameIdentifier (NameResolvedID, IdentifierID, IdentifierValue,
			CreationUserID, LastModifiedUserID)
		VALUES (@NameResolvedID, @EOLIdentifierID, @EOLID,
			@CreationUserID, @LastModifiedUserID)
	END
END

INSERT dbo.Name (NameSourceID, NameResolvedID, NameString, IsActive, CreationUserID, LastModifiedUserID)
VALUES (@NameSourceID, @NameResolvedID, @NameString, @IsActive, @CreationUserID, @LastModifiedUserID)

SELECT @NameID = SCOPE_IDENTITY()

-- Return the detail of the inserted name
SELECT	NameID,
		NameSourceID,
		NameString,
		IsActive,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.Name
WHERE	NameID = @NameID

END

