
-- NamePageInsertAuto PROCEDURE
-- Generated 10/29/2012 3:17:36 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for NamePage

CREATE PROCEDURE NamePageInsertAuto

@NamePageID INT OUTPUT,
@NameID INT,
@PageID INT,
@NameSourceID INT,
@IsFirstOccurrence SMALLINT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[NamePage]
(
	[NameID],
	[PageID],
	[NameSourceID],
	[IsFirstOccurrence],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@NameID,
	@PageID,
	@NameSourceID,
	@IsFirstOccurrence,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @NamePageID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NamePageInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[NamePageID],
		[NameID],
		[PageID],
		[NameSourceID],
		[IsFirstOccurrence],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[NamePage]
	
	WHERE
		[NamePageID] = @NamePageID
	
	RETURN -- insert successful
END

