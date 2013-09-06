
-- IdentifierInsertAuto PROCEDURE
-- Generated 5/1/2012 2:41:41 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Identifier

CREATE PROCEDURE IdentifierInsertAuto

@IdentifierID INT OUTPUT,
@IdentifierName NVARCHAR(40),
@IdentifierLabel NVARCHAR(50),
@Display SMALLINT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Identifier]
(
	[IdentifierName],
	[IdentifierLabel],
	[Display],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@IdentifierName,
	@IdentifierLabel,
	@Display,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @IdentifierID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IdentifierInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[IdentifierID],
		[IdentifierName],
		[IdentifierLabel],
		[Display],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[Identifier]
	
	WHERE
		[IdentifierID] = @IdentifierID
	
	RETURN -- insert successful
END


