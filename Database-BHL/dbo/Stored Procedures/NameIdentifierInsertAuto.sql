
-- NameIdentifierInsertAuto PROCEDURE
-- Generated 9/18/2012 11:59:15 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for NameIdentifier

CREATE PROCEDURE NameIdentifierInsertAuto

@NameIdentifierID INT OUTPUT,
@NameResolvedID INT,
@IdentifierID INT,
@IdentifierValue NVARCHAR(100),
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[NameIdentifier]
(
	[NameResolvedID],
	[IdentifierID],
	[IdentifierValue],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@NameResolvedID,
	@IdentifierID,
	@IdentifierValue,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @NameIdentifierID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameIdentifierInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[NameIdentifierID],
		[NameResolvedID],
		[IdentifierID],
		[IdentifierValue],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[NameIdentifier]
	
	WHERE
		[NameIdentifierID] = @NameIdentifierID
	
	RETURN -- insert successful
END

