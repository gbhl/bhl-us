
-- NameInsertAuto PROCEDURE
-- Generated 12/10/2012 3:05:47 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Name

CREATE PROCEDURE NameInsertAuto

@NameID INT OUTPUT,
@NameSourceID INT,
@NameString NVARCHAR(100),
@IsActive SMALLINT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null,
@NameResolvedID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Name]
(
	[NameSourceID],
	[NameString],
	[IsActive],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[NameResolvedID]
)
VALUES
(
	@NameSourceID,
	@NameString,
	@IsActive,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID,
	@NameResolvedID
)

SET @NameID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[NameID],
		[NameSourceID],
		[NameString],
		[IsActive],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID],
		[NameResolvedID]	

	FROM [dbo].[Name]
	
	WHERE
		[NameID] = @NameID
	
	RETURN -- insert successful
END

