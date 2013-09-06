
-- NameSourceInsertAuto PROCEDURE
-- Generated 9/18/2012 11:59:15 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for NameSource

CREATE PROCEDURE NameSourceInsertAuto

@NameSourceID INT OUTPUT,
@SourceName NVARCHAR(50),
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[NameSource]
(
	[SourceName],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@SourceName,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @NameSourceID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameSourceInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[NameSourceID],
		[SourceName],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[NameSource]
	
	WHERE
		[NameSourceID] = @NameSourceID
	
	RETURN -- insert successful
END

